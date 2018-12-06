using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace attendance_tracker
{
    public partial class email_user : Form
    {
        /// <summary>
        /// Global Variables
        /// </summary>
        private static CustomListBox.NormalListBoxItem _s;
        private static string _connection = null;
        private static string _profName;
        private static string _profId;
        private static string _userId;
        private static string _userName;
        private static string _userEmail;
        public static string ProfEmail { get; private set; }

        /// <inheritdoc />
        public email_user(CustomListBox.NormalListBoxItem sender, string uid)
        {
            InitializeComponent();
            _s = sender;
            _connection = GetConnectionStrings();
            _userId = uid;
            GetProfNameAndID();
            GetStudentName();
            this.Text = @"Emailing Instructor " + _profName.Split(' ')[0][0] + @" " + _profName.Split(' ')[1];
        }
        /// <summary>
        /// Default connection string
        /// </summary>
        static string GetConnectionStrings()
        {
            var settings = ConfigurationManager.ConnectionStrings["Connection"];
            return settings.ConnectionString;
        }
        /// <summary>
        /// Get the specific professor name and id using class_id
        /// </summary>
        private static void GetProfNameAndID()
        {
            using (var con = new MySqlConnection(_connection))
            {
                con.Open();
                const string query = "SELECT * FROM class WHERE class_id = @class_id";
                var comm = new MySqlCommand(query, con);
                comm.Parameters.AddWithValue("@class_id", _s.TextStatus);
                var reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    _profId = reader["prof_id"].ToString();
                    _profName = reader["instructor"].ToString();
                }
                con.Close();
            }
        }
        /// <summary>
        /// Get all the information from the student using student_id
        /// </summary>
        private static void GetStudentName()
        {
            using (var con = new MySqlConnection(_connection))
            {
                con.Open();
                const string query = "SELECT * FROM student WHERE student_id = @student_id";
                var comm = new MySqlCommand(query, con);
                comm.Parameters.AddWithValue("@student_id", _userId);
                var reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    _userName = reader["first_name"] + " " + reader["last_name"];
                    _userEmail = reader["email"].ToString();
                }
                con.Close();
            }
        }
        /// <summary>
        /// Emailing the instructor for a specific class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aetherButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(aetherTextbox1.Text) || string.IsNullOrEmpty(aetherTextbox2.Text))
                MessageBox.Show(@"Please fill out all fields before you can email the instructor");
            else
            {
                //IF EMAIL ISNT SENDING REMOVE THREADPOOL
                ThreadPool.QueueUserWorkItem(t =>
                {
                    using (var con = new MySqlConnection(_connection))
                    {
                        con.Open();
                        const string query = "SELECT * FROM professor WHERE prof_id = @prof_id";
                        var comm = new MySqlCommand(query, con);
                        comm.Parameters.AddWithValue("@prof_id", _profId);
                        var reader = comm.ExecuteReader();
                        while (reader.Read())
                        {
                            ProfEmail = reader["email"].ToString();
                        }
                        con.Close();
                    }
                    sendEMail(_profName.Split(' ')[0], _profName.Split(' ')[1], ProfEmail, _userName.Split(' ')[0], _userName.Split(' ')[1], _userEmail, aetherTextbox1.Text, aetherTextbox2.Text);

                });
            }
        }

        private void sendEMail(string fName, string lName, string eMail, string studentFname, string studentLname, string studentEmail, string sub, string bod)
        {
            //SMTP
            var smtpAddress = "smtp.gmail.com";
            const int portNumber = 587;

            var emailFrom = "attendance.worksmail@gmail.com";
            var password = "Atten1234";
            var emailTo = eMail;
            var subject = "New Student Message";
            var body = ("Hi " + fName + " " + lName + ",<br /> You have received a new message from the following student:" +
                           "<br /> First Name: " + studentFname + "<br /> Last Name: " + studentLname + "<br /> Email: " + studentEmail + 
                           "<br /> <br /> Subject: " + sub + "<br /> MESSAGE:" + "<br /> ---------" + bod);

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
    }
}
