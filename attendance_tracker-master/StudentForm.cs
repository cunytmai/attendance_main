using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using MySql.Data.MySqlClient;
using LB = CustomListBox;

namespace attendance_tracker
{
    public partial class StudentForm : Form
    {
        private static string _connection = null;
        private static string _userId = null;
        public static string StudentId { get; set; }

        public StudentForm(string userId)
        {
            InitializeComponent();
            _userId = userId;
            _connection = GetConnectionStrings();
            GetStudentId();
            
        }

        private static void GetStudentId()
        {
            using (var con = new MySqlConnection(_connection))
            {
                con.Open();
                const string query = "SELECT * FROM student WHERE user_id = @Uid";
                var comm = new MySqlCommand(query, con);
                comm.Parameters.AddWithValue("@Uid", _userId);
                var reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    StudentId = reader["student_id"].ToString();
                }
                con.Close();
            }
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

            //tooltip
            var progrssToolTip = new ToolTip
            {
                ToolTipIcon = ToolTipIcon.Info,
                IsBalloon = true,
                ShowAlways = true
            };
            progrssToolTip.SetToolTip(aetherTag1, "You need to have over 75% attendance in all of your classes");

            var standingToolTip = new ToolTip
            {
                ToolTipIcon = ToolTipIcon.Info,
                IsBalloon = true,
                ShowAlways = true
            };
            standingToolTip.SetToolTip(aetherTag7, "Overall attendance summary of all enrolled classes");
            //end tooltip

            GetUserDetail();
            //set the form name
            this.Text = @"Welcome " + label1.Text;
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
                const string query = "SELECT * FROM student WHERE student_id = @Uid";
                var comm = new MySqlCommand(query, con);
                comm.Parameters.AddWithValue("@Uid", StudentId);
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
                const string query = "SELECT * FROM studentclasses WHERE student_id = @Uid";
                var comm = new MySqlCommand(query, con);
                comm.Parameters.AddWithValue("@Uid", StudentId);
                var reader = comm.ExecuteReader();
                var counter = 0;
                while (reader.Read())
                {
                    counter++;
                }

                label3.Text = counter.ToString();
                con.Close();
            }

            //calculation: this is where we average everything out. We arent worried about breakdown per class
            using (var con = new MySqlConnection(_connection))
            {
                con.Open();
                const string query = "SELECT * FROM classattendance WHERE student_id = @Uid";
                var comm = new MySqlCommand(query, con);
                comm.Parameters.AddWithValue("@Uid", StudentId);
                var reader = comm.ExecuteReader();
                var counter = 0;
                var notHere = 0;
                while (reader.Read())
                {
                    if (reader["present"].ToString() != "True")
                        notHere++;

                    counter++;
                }

                float notHerePerc = (int)Math.Round((double)(100 * notHere) / counter);
                var herePerc = 100 - notHerePerc;

                if (herePerc >= 75)
                {
                    label4.Text = @"Positive";
                    aetherCircular1.HatchPrimary = Color.Green;
                    aetherCircular1.HatchSecondary = Color.Green;
                    aetherCircular1.Max = 100;
                    aetherCircular1.Min = 0;
                    aetherCircular1.Progress = herePerc;
                    aetherCircular1.Percent = true;
                }
                else
                {
                    label4.Text = @"Negative";
                    aetherCircular1.HatchPrimary = Color.Green;
                    aetherCircular1.HatchSecondary = Color.Red;
                    aetherCircular1.Max = 100;
                    aetherCircular1.Min = 0;
                    aetherCircular1.Progress = herePerc;
                    aetherCircular1.Percent = true;
                }

                //set the semester completion
                //establish DateTimes
                DateTime start = new DateTime(2018, 08, 21);
                DateTime end = new DateTime(2018, 12, 25);
                var current = DateTime.Now;

                var totalDays = end - start; //total days
                var daysLeft = end - current;
                var semPerc = (int)Math.Round((double)(100 * daysLeft.Days) / totalDays.Days);
                semPerc = 100 - semPerc;
                aetherCircular2.HatchPrimary = Color.Blue;
                aetherCircular2.HatchSecondary = Color.Gray;
                aetherCircular2.Max = totalDays.Days;
                aetherCircular2.Min = 1;
                aetherCircular2.Progress = semPerc;
                //end set semester completion

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

        private void ListboxItemRightClicked(LB.NormalListBoxItem item, MouseEventArgs mousehit, string uid)
        {
            var cmm = new ContextMenu();
            var email = new MenuItem
            {
                Text = @"Email Instructor",
            };

            email.Click += (senders, evnt) =>
            {
                Email_Click(item, evnt, uid);
            };

            cmm.MenuItems.Add(email);
            item.ContextMenu = cmm;
        }

        private void Email_Click(LB.NormalListBoxItem sender, EventArgs e, string uid)
        {
            //MessageBox.Show(sender.TextTitle);
            var email = new email_user(sender, uid);
            email.FormClosed += new FormClosedEventHandler(otherForm_FormClosed);
            this.Hide();
            email.Show();
        }

        private void ListboxItemDoubleClicked(LB.NormalListBoxItem item, string studentId)
        {
            //MessageBox.Show(item.TextTitle);
            var stats = new class_breakdown(item, studentId);
            stats.FormClosed += new FormClosedEventHandler(otherForm_FormClosed);
            this.Hide();
            stats.Show();
        }

        private void listBoxEx1_Load(object sender, EventArgs e)
        {
            //get user classes
            var userClassList = new Dictionary<string[], string>();
            var classes = new Dictionary<string[], LB.NormalListBoxItem.Rank[]>();

            using (var con = new MySqlConnection(_connection))
            {
                con.Open();
                const string query = "SELECT * FROM studentclasses WHERE student_id = @Uid";
                var comm = new MySqlCommand(query, con);
                comm.Parameters.AddWithValue("@Uid", StudentId);
                var reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    userClassList.Add(new []
                    {
                        reader["class_name"].ToString(),
                        reader["class_id"].ToString()
                    }, "");
                }

                con.Close();
            }

            using (var con2 = new MySqlConnection(_connection))
            {
                foreach (var cl in userClassList)
                {
                    con2.Open();
                    const string query2 = "SELECT * FROM classattendance WHERE student_id = @Uid AND class_id = @class_id";
                    var comm2 = new MySqlCommand(query2, con2);
                    comm2.Parameters.AddWithValue("@Uid", StudentId);
                    comm2.Parameters.AddWithValue("@class_id", cl.Key[1]);
                    var reader2 = comm2.ExecuteReader();
                    var counter = 0;
                    var notHere = 0;
                    while (reader2.Read())
                    {
                        if (reader2["present"].ToString() != "True")
                            notHere++;

                        counter++;
                    }

                    float notHerePerc = (int)Math.Round((double)(100 * notHere) / counter);
                    var herePerc = 100 - notHerePerc;
                    if (herePerc >= 75)
                    {
                        classes.Add(new[]
                        {
                            cl.Key[0],
                            cl.Key[1]
                        }, new[]
                        {
                            LB.NormalListBoxItem.Rank.Student, LB.NormalListBoxItem.Rank.Positive
                        });
                    }
                    else
                    {
                        classes.Add(new[]
                        {
                            cl.Key[0],
                            cl.Key[1]
                        }, new[]
                        {
                            LB.NormalListBoxItem.Rank.Student, LB.NormalListBoxItem.Rank.Negative
                        });
                    }
                    con2.Close();
                }
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
                    ListboxItemDoubleClicked(senders as LB.NormalListBoxItem, StudentId);
                });

                litem.MouseDown += new MouseEventHandler((senders, evnt) =>
                {
                    if (evnt.Button == MouseButtons.Right)
                        ListboxItemRightClicked(senders as LB.NormalListBoxItem, evnt, StudentId);
                });
                listBoxEx1.Add(litem);
            }
        }
    }
}
