using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using System.Threading;
using MySql.Data.MySqlClient;
using LB = CustomListBox;

namespace attendance_tracker
{
    public partial class StudentForm : Form
    {
        private static string _connection = null;
        private static string _userId = null;

        public StudentForm(string userId)
        {
            InitializeComponent();
            _userId = userId;
            _connection = GetConnectionStrings();
        }

        static string GetConnectionStrings()
        {
            var settings = ConfigurationManager.ConnectionStrings["Connection"];
            return settings.ConnectionString;
        }

        private void homeTabPage_Click(object sender, EventArgs e)
        {

        }

        private void StudentForm_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(@"default.png");
            GetUserDetail();
            
        }

        private void GetUserDetail()
        {
            if (_userId == null)
            {
                label1.Text = @"NULL";
                label2.Text = @"NULL";
                label3.Text = @"Null";
                label4.Text = @"NULL";
                return;
            }

            //get the user' name
            using (var con = new MySqlConnection(_connection))
            {
                con.Open();
                const string query = "SELECT * FROM student WHERE user_id = @Uid";
                var comm = new MySqlCommand(query, con);
                comm.Parameters.AddWithValue("@Uid", _userId);
                var reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    label1.Text = reader["first_name"]+ @" " + reader["last_name"];
                }
                con.Close();
            }

            //get the role
            using (var con = new MySqlConnection(_connection))
            {
                con.Open();
                const string query = "SELECT * FROM user_login WHERE user_id = @Uid";
                var comm = new MySqlCommand(query, con);
                comm.Parameters.AddWithValue("@Uid", _userId);
                var reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    label2.Text = reader["role"].ToString();
                }
                con.Close();
            }

            //get number of classes
            using (var con = new MySqlConnection(_connection))
            {
                con.Open();
                const string query = "SELECT * FROM studentclasses WHERE user_id = @Uid";
                var comm = new MySqlCommand(query, con);
                comm.Parameters.AddWithValue("@Uid", _userId);
                var reader = comm.ExecuteReader();
                var counter = 0;
                while (reader.Read())
                {
                    counter++;
                }

                label3.Text = counter.ToString();
                con.Close();
            }
        }

        private void classTabPage_Click(object sender, EventArgs e)
        {

        }

        private void classTabPage_Enter(object sender, EventArgs e)
        {
            
        }

        void otherForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void ListboxItemRightClicked(LB.NormalListBoxItem item, MouseEventArgs mousehit)
        {
            var cmm = new ContextMenu();
            var email = new MenuItem
            {
                Text = "Email Instructor",
            };

            email.Click += (senders, evnt) =>
            {
                Email_Click(item, evnt);
            };

            cmm.MenuItems.Add(email);
            item.ContextMenu = cmm;
        }

        private void Email_Click(LB.NormalListBoxItem sender, EventArgs e)
        {
            //MessageBox.Show(sender.TextTitle);
            var email = new email_user(sender);
            email.FormClosed += new FormClosedEventHandler(otherForm_FormClosed);
            this.Hide();
            email.Show();
        }

        private void ListboxItemDoubleClicked(LB.NormalListBoxItem item)
        {
            //MessageBox.Show(item.TextTitle);
            var stats = new class_breakdown(item);
            stats.FormClosed += new FormClosedEventHandler(otherForm_FormClosed);
            this.Hide();
            stats.Show();
        }

        private void listBoxEx1_Load(object sender, EventArgs e)
        {
            //get user classes
            var classes = new Dictionary<string[], LB.NormalListBoxItem.Rank[]>();
            using (var con = new MySqlConnection(_connection))
            {
                con.Open();
                const string query = "SELECT * FROM studentclasses WHERE user_id = @Uid";
                var comm = new MySqlCommand(query, con);
                comm.Parameters.AddWithValue("@Uid", _userId);
                var reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    //TODO: add a check to see if user in positive or negative status
                    classes.Add(new []
                    {
                        reader["title"].ToString(),
                        reader["class_id"].ToString()
                    }, new []
                    {
                        LB.NormalListBoxItem.Rank.Student, LB.NormalListBoxItem.Rank.Positive
                    });
                }

                con.Close();
            }

            foreach (var r in classes)
            {
                var title = r.Key[0];
                var status = r.Key[1];
                var rnk = r.Value;

                var litem = new LB.NormalListBoxItem()
                {
                    TextTitle = title,
                    TextStatus = status,
                    TextRank = rnk,
                    status = LB.NormalListBoxItem.Status.Online,
                };

                litem.DoubleClick += new EventHandler((senders, evnt) =>
                {
                    ListboxItemDoubleClicked(senders as LB.NormalListBoxItem);
                });

                litem.MouseDown += new MouseEventHandler((senders, evnt) =>
                {
                    if (evnt.Button == MouseButtons.Right)
                        ListboxItemRightClicked(senders as LB.NormalListBoxItem, evnt);
                });
                listBoxEx1.Add(litem);
            }
        }
    }
}
