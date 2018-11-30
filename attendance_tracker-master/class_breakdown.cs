using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using CustomListBox;
using MySql.Data.MySqlClient;

namespace attendance_tracker
{
    public partial class class_breakdown : Form
    {
        private CustomListBox.NormalListBoxItem _s;
        public static string StudentId { get; set; }
        private static string _connection = null;

        public class_breakdown(NormalListBoxItem item, string studentId)
        {
            InitializeComponent();
            _s = item;
            this.Text = _s.TextTitle + @" class breakdown";
            _connection = GetConnectionStrings();
            StudentId = studentId;
            GetData();
        }

        static string GetConnectionStrings()
        {
            var settings = ConfigurationManager.ConnectionStrings["Connection"];
            return settings.ConnectionString;
        }

        private void GetData()
        {
            using (var con = new MySqlConnection(_connection))
            {
                con.Open();
                const string query = "SELECT * FROM classattendance WHERE student_id = @Uid AND class_id = @class_id";
                var comm = new MySqlCommand(query, con);
                comm.Parameters.AddWithValue("@Uid", StudentId);
                comm.Parameters.AddWithValue("@class_id", _s.TextStatus);
                var reader = comm.ExecuteReader();
                var counter = 0;
                var notHere = 0;
                while (reader.Read())
                {
                    if (reader["present"].ToString() != "True")
                        notHere++;

                    counter++;
                }

                float notHerePerc = (int) Math.Round((double) (100 * notHere) / counter);
                var herePerc = 100 - notHerePerc;

                label1.Text = counter.ToString();
                label2.Text = (counter - notHere).ToString();
                label3.Text = notHere.ToString();

                //set the semester completion
                //establish DateTimes
                DateTime start = new DateTime(2018, 08, 21);
                DateTime end = new DateTime(2018, 12, 25);
                var current = DateTime.Now;

                var totalDays = end - start; //total days
                var daysLeft = end - current;
                var semPerc = (int)Math.Round((double)(100 * daysLeft.Days) / totalDays.Days);
                semPerc = 100 - semPerc;

                label4.Text = totalDays.Days.ToString();
                label5.Text = daysLeft.Days.ToString();

                label6.Text = herePerc >= 75 ? @"Positive" : @"Negative";



                if (herePerc >= 75)
                {
                    aetherCircular1.HatchPrimary = Color.Green;
                    aetherCircular1.HatchSecondary = Color.Green;
                    aetherCircular1.Max = 100;
                    aetherCircular1.Min = 0;
                    aetherCircular1.Progress = herePerc;
                    aetherCircular1.Percent = true;

                    aetherCircular2.HatchPrimary = Color.Blue;
                    aetherCircular2.HatchSecondary = Color.YellowGreen;
                    aetherCircular2.Max = 100;
                    aetherCircular2.Min = 0;
                    aetherCircular2.Progress = notHerePerc;
                    aetherCircular2.Percent = true;
                }
                else
                {
                    aetherCircular1.HatchPrimary = Color.Green;
                    aetherCircular1.HatchSecondary = Color.Red;
                    aetherCircular1.Max = 100;
                    aetherCircular1.Min = 0;
                    aetherCircular1.Progress = herePerc;
                    aetherCircular1.Percent = true;

                    aetherCircular2.HatchPrimary = Color.Green;
                    aetherCircular2.HatchSecondary = Color.Brown;
                    aetherCircular2.Max = 100;
                    aetherCircular2.Min = 0;
                    aetherCircular2.Progress = notHerePerc;
                    aetherCircular2.Percent = true;
                }

                aetherCircular3.HatchPrimary = Color.Gold;
                aetherCircular3.HatchSecondary = Color.Black;
                aetherCircular3.Max = totalDays.Days;
                aetherCircular3.Min = 1;
                aetherCircular3.Progress = semPerc;
            }
        }
    }
}
