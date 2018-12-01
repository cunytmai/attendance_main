using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient; //mysql
using System.Text.RegularExpressions; //regex
using System.Net.Mail; //smtp
using System.Net; //smtp

namespace attendance_tracker
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var iconsList = new ImageList
            {
                TransparentColor = Color.Blue,
                ColorDepth = ColorDepth.Depth32Bit,
                ImageSize = new Size(150, 150)
            };
            iconsList.Images.Add(Image.FromFile(@"login.png"));
            iconsList.Images.Add(Image.FromFile(@"register.png"));


            aetherTabControl1.ImageList = iconsList;
            loginTabPage.ImageIndex = 0;
            registerTabPage.ImageIndex = 1;

            GetInfo();
        }

        static string GetConnectionStrings()
        {
            var settings = ConfigurationManager.ConnectionStrings["Connection"];
            return settings.ConnectionString;
        }

        private static readonly string Connection = GetConnectionStrings();

        private readonly List<string> _usernameList = new List<string>();
        private readonly List<string> _passwordList = new List<string>();
        private readonly List<string> _userIdList = new List<string>();
        //private List<string> studentIDList = new List<string>();
        // private List<string> profIDList = new List<string>();
        public readonly List<string> VerifiedListS = new List<string>();
        public readonly List<string> VerifiedListP = new List<string>();
        private readonly List<string> _roleList = new List<string>();
        private string _username, _password;
        private int _count = 1;
        private string _userId, _userRole, _userDb, _passDb; //for button 1
        private string _fName, _lName, _eMail, _role; //for button 2
        private string _code; //for button 3

        //TODO: fix this, not working as intended.
        private void aetherTextbox2_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
                aetherButton1_Click(this, new EventArgs());
        }

        private void aetherButton2_Click_1(object sender, EventArgs e)
        {
            GetInfo();

            _username = aetherTextbox9.Text;
            _password = aetherTextbox8.Text;
            _fName = aetherTextbox3.Text;
            _lName = aetherTextbox4.Text;
            _eMail = aetherTextbox5.Text;
            _role = aetherRadioButton1.Checked ? aetherRadioButton1.Text : aetherRadioButton2.Text;
            string key = string.Concat(_username, _count.ToString());
            string encryptPassword = Cryptography.Encrypt(_password, key);
            string decryptPassword = Cryptography.Decrypt(encryptPassword, key);

            MessageBox.Show(_password);
            MessageBox.Show(encryptPassword);
            MessageBox.Show(decryptPassword);
            MessageBox.Show(key);

            //if (string.IsNullOrEmpty(_fName))
            //    MessageBox.Show(@"Please enter a first name.");
            //else if (string.IsNullOrEmpty(_lName))
            //    MessageBox.Show(@"Please enter a last name.");
            //else if (string.IsNullOrEmpty(_eMail))
            //    MessageBox.Show(@"Please enter an email address.");
            //else if (string.IsNullOrEmpty(_username))
            //    MessageBox.Show(@"Please enter a username.");
            //else if (string.IsNullOrEmpty(_password))
            //    MessageBox.Show(@"Please enter a password.");
            //else if (!aetherRadioButton1.Checked && !aetherRadioButton2.Checked)
            //    MessageBox.Show(@"Please check a valid role.");
            //else if (!IsValidEmail(_eMail))
            //    MessageBox.Show(@"Please enter a valid email.");
            //else
            //{
            //    MessageBox.Show(@"Thank you for creating an account. As the next step, please verify your account." + @"\n"
            //        + @"A verification code was sent to your email, please enter it below.");

            //    var con = new MySqlConnection(Connection);
            //    con.Open();

            //    const string query = "INSERT INTO attendance.user_login(user_id, username, password, role, active) VALUE (@uID, @uN, @pw, @rl, @act)";
            //    var comm = new MySqlCommand(query, con);
            //    comm.Parameters.AddWithValue("@uID", _count.ToString());
            //    comm.Parameters.AddWithValue("@uN", _username);
            //    comm.Parameters.AddWithValue("@pw", _password);
            //    comm.Parameters.AddWithValue("@rl", _role);
            //    comm.Parameters.AddWithValue("@act", 1);
            //    comm.ExecuteNonQuery();
            //    //inserting the username / password ... etc
            //    if (_role == "Student")
            //    {
            //        string query1 = "INSERT INTO attendance.student(user_id, first_name, last_name, email, verified) VALUE (@uID, @fN, @lN, @em, @ver)";
            //        comm = new MySqlCommand(query1, con);
            //        comm.Parameters.AddWithValue("@uID", _count.ToString());
            //        comm.Parameters.AddWithValue("@fN", _fName);
            //        comm.Parameters.AddWithValue("@lN", _lName);
            //        comm.Parameters.AddWithValue("@em", _eMail);
            //        comm.Parameters.AddWithValue("@ver", 0);
            //        comm.ExecuteNonQuery();
            //    }
            //    else
            //    {
            //        string query1 = "INSERT INTO attendance.professor(user_id, first_name, last_name, email, verified) VALUE (@uID, @fN, @lN, @em, @ver)";
            //        comm = new MySqlCommand(query1, con);
            //        comm.Parameters.AddWithValue("@uID", _count.ToString());
            //        comm.Parameters.AddWithValue("@fN", _fName);
            //        comm.Parameters.AddWithValue("@lN", _lName);
            //        comm.Parameters.AddWithValue("@em", _eMail);
            //        comm.Parameters.AddWithValue("@ver", 0);
            //        comm.ExecuteNonQuery();
            //    }
            //    //inserting the information of student or teacher
            //    con.Close();

            //    //get a random verification code
            //    Random randomCode = new Random();
            //    _code = (randomCode.Next(11111, 99999)).ToString();

            //    sendEMail(_fName, _lName, _eMail, _code);
            //    MessageBox.Show(_code);
            //}
        }

        private void aetherButton3_Click(object sender, EventArgs e)
        {
            if (_eMail == aetherTextbox6.Text && _code == aetherTextbox7.Text)
            {
                string query;
                MessageBox.Show(@"Thank you for verifying your account!");
                //Todo:
                //UPDATE `attendance`.`student` SET `verified`= '0' WHERE `student_id`= '1';
                MySqlConnection con = new MySqlConnection(Connection);
                con.Open();
                if (_role == "Student")
                {
                    query = "UPDATE attendance.student SET verified=1 WHERE user_id = @Uid";
                    MySqlCommand comm = new MySqlCommand(query, con);
                    comm.Parameters.AddWithValue("@Uid", _count.ToString());
                    comm.ExecuteNonQuery();
                }
                else
                {
                    query = "UPDATE attendance.professor SET verified=1 WHERE user_id = @Uid";
                    MySqlCommand comm = new MySqlCommand(query, con);
                    comm.Parameters.AddWithValue("@Uid", _count.ToString());
                    comm.ExecuteNonQuery();
                }
                con.Close();
            }
            else
                MessageBox.Show(@"Wrong E-Mail or Code!");
        }

        private void aetherButton1_Click(object sender, EventArgs e)
        {
            _username = aetherTextbox1.Text;
            _password = aetherTextbox2.Text;
            GetInfo();

            //checks if the username and password exists
            if (_usernameList.Exists(x => x == _username) && _passwordList.Exists(y => y == _password))
            {
                for (var i = 0; i < _count - 1; i++)
                {
                    if (_usernameList[i] == _username)
                    {
                        _userId = _userIdList[i];
                        _userDb = _usernameList[i];
                        _passDb = _passwordList[i];
                        _userRole = _roleList[i];
                        //get the verified true or false
                    }
                }

                MySqlConnection con = new MySqlConnection(Connection);
                con.Open();

                string query;

                if (_userRole == "Student")
                    query = "SELECT verified FROM student where user_id = '" + _userId + "'";
                else
                    query = "SELECT verified FROM professor where user_id = '" + _userId + "'";

                MySqlCommand comm = new MySqlCommand(query, con);
                comm.Parameters.AddWithValue(@"Uid", _userId);
                var result = comm.ExecuteScalar().ToString();

                if (_userRole == "Professor" && _userDb == _username && _passDb == _password && result == "True")
                {
                    ProfForm pForm = new ProfForm(_userId); //pass over the user_id and use that to get the info
                    pForm.FormClosed += otherForm_FormClosed;
                    Hide();
                    pForm.Show();
                }
                else if (_userRole == "Student" && _userDb == _username && _passDb == _password && result == "True")
                {
                    StudentForm sForm = new StudentForm(_userId); //pass over the user_id and use that to get the info
                    sForm.FormClosed += otherForm_FormClosed;
                    Hide();
                    sForm.Show();
                }
                else
                {
                    MessageBox.Show(@"Your account is not activated!");
                }

                con.Close();
            }
            else if (_username == "admin" && _password == "admin")
            {
                adminForm aForm = new adminForm();
                aForm.FormClosed += otherForm_FormClosed;
                Hide();
                aForm.Show();
            }
            else
                MessageBox.Show(@"Please enter the correct username and password!");
        }

        void otherForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Show();
        }

        private void GetInfo()
        {
            //clear everything in every list
            _usernameList.Clear();
            _passwordList.Clear();
            _roleList.Clear();
            VerifiedListP.Clear();
            VerifiedListS.Clear();
            _userIdList.Clear();
            _count = 1;

            //select queries
            MySqlConnection con = new MySqlConnection(Connection);
            con.Open();
            string query = "SELECT username FROM user_login";
            using (MySqlCommand command = new MySqlCommand(query, con))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        _usernameList.Add(reader.GetString(0));
                        _count++;
                    }
                }
            }

            query = "SELECT password FROM user_login";
            using (MySqlCommand command = new MySqlCommand(query, con))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        _passwordList.Add(reader.GetString(0));
                    }
                }
            }

            query = "SELECT user_id FROM user_login";
            using (MySqlCommand command = new MySqlCommand(query, con))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        _userIdList.Add(reader.GetString(0));
                    }
                }
            }

            query = "SELECT role FROM user_login";
            using (MySqlCommand command = new MySqlCommand(query, con))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        _roleList.Add(reader.GetString(0));
                    }
                }
            }
            //might not need
            query = "SELECT verified FROM student";
            using (MySqlCommand command = new MySqlCommand(query, con))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VerifiedListS.Add(reader.GetString(0));
                    }
                }
            }
            //might not need
            query = "SELECT verified FROM professor";
            using (MySqlCommand command = new MySqlCommand(query, con))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VerifiedListP.Add(reader.GetString(0));
                    }
                }
            }
            con.Close();
        }
        private bool IsValidEmail(string email)
        {
            const string pattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                   + "@"
                                   + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";

            var regex = new Regex(pattern, RegexOptions.IgnoreCase);

            return regex.IsMatch(email);
        }

        private void sendEMail(string fName, string lName, string eMail, string code)
        {
            //SMTP
            var smtpAddress = "smtp.gmail.com";
            const int portNumber = 587;

            string emailFrom = "attendance.worksmail@gmail.com";
            string password = "Atten1234";
            string emailTo = eMail;
            string subject = "Account Verification";
            string body = ("Hi " + fName + " " + lName + ",<br /> Thank you for creating an account on Attendance Works. Below you will find your verification code:" +
                "<br /> Verification Code: " + code);

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
