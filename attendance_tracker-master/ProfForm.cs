﻿using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Drawing;

namespace attendance_tracker
{
    public partial class ProfForm : Form
    {
        private readonly string _connection = GetConnectionStrings();
        private readonly string _userId;
        private string _pId, _fN, _lN;
        private string _cId, _sId, _cName, _cInstruct, _cRoom;
        private readonly List<string> _studentFirst = new List<string>();
        private readonly List<string> _studentLast = new List<string>();
        private readonly List<string> _classStudents = new List<string>();
        private readonly List<string> _classIDs = new List<string>();
        private readonly List<string> _existClass = new List<string>(); //used to check existing classes
        CheckBox[] chk;
        private string pFN, pLN, totClass;

        static string GetConnectionStrings()
        {
            var settings = ConfigurationManager.ConnectionStrings["Connection"];
            return settings.ConnectionString;
        }

        private void aetherButton5_Click(object sender, EventArgs e)
        {
            string classID = aetherTextbox17.Text;
            getStudentInfo(classID);
            chk = new CheckBox[_studentFirst.Count];
            int height = 1;
            int padding = 10;
            int x = 10;
            for (int i = 0; i < _studentFirst.Count; i++)
            {
                int bounds = padding + height;
                chk[i] = new CheckBox();
                chk[i].Name = i.ToString(); //locator
                chk[i].Text = _studentFirst[i] + " " + _studentLast[i]; //text of the checkbox
                chk[i].TabIndex = i;
                chk[i].AutoSize = true;
                chk[i].AutoCheck = true;
                if(bounds < 363)
                    chk[i].Bounds = new Rectangle(x, padding + height, 40, 22);
                else
                {
                    height = 1;
                    x += 120;
                    chk[i].Bounds = new Rectangle(x, padding + height, 40, 22);
                }

                panel1.Controls.Add(chk[i]);
                height += 32;
            }
            label4.Text = classID;
        }

        private void aetherButton4_Click(object sender, EventArgs e)
        {
            string cName;
            _cId = aetherTextbox16.Text;
            _sId = aetherTextbox13.Text;
            _fN = aetherTextbox14.Text;
            _lN = aetherTextbox15.Text;
            getExistingClass(_pId);
            if (string.IsNullOrEmpty(_cId))
                MessageBox.Show(@"Please enter a class ID!");
            else if (string.IsNullOrEmpty(_sId))
                MessageBox.Show(@"Please enter student ID!");
            else if (string.IsNullOrEmpty(_fN))
                MessageBox.Show(@"Please enter student first name!");
            else if (string.IsNullOrEmpty(_lN))
                MessageBox.Show(@"Please enter student last name");
            else
            {
                if (_existClass.Exists(x => x == _cId))
                {
                    MySqlConnection con = new MySqlConnection(_connection);
                    con.Open();

                    //string query = "INSERT INTO attendance.classattendance(class_id, prof_id, student_id, firstName, lastName)" +
                    //                     "VALUES (@Cid, @Pid, @Sid, @fN, @lN)";

                    //MySqlCommand comm = new MySqlCommand(query, con);
                    //comm.Parameters.AddWithValue("@Cid", _cId);
                    //comm.Parameters.AddWithValue("@Pid", _pId);
                    //comm.Parameters.AddWithValue("@Sid", _sId);
                    //comm.Parameters.AddWithValue("@fN", _fN);
                    //comm.Parameters.AddWithValue("@lN", _lN);
                    //comm.ExecuteNonQuery();

                    string query = "SELECT class_name FROM class WHERE prof_id = @Pid and class_id = @Cid";
                    MySqlCommand comm = new MySqlCommand(query, con);
                    comm.Parameters.AddWithValue("@Pid", _pId);
                    comm.Parameters.AddWithValue("@Cid", _cId);
                    cName = comm.ExecuteScalar().ToString();

                    query = "INSERT INTO attendance.studentclasses(student_id, prof_id, class_id, class_name)" +
                                         "VALUES (@Sid, @Pid, @Cid, @cN)";
                    comm = new MySqlCommand(query, con);
                    comm.Parameters.AddWithValue("@Sid", _sId);
                    comm.Parameters.AddWithValue("@Pid", _pId);
                    comm.Parameters.AddWithValue("@Cid", _cId);
                    comm.Parameters.AddWithValue("@cN", cName);
                    comm.ExecuteNonQuery();

                    con.Close();
                    MessageBox.Show(@"You have enrolled " + _fN + " " + _lN + " for " + _cId);
                }
                else
                    MessageBox.Show("Class ID does not exist");
            }
        }

        private void aetherTextbox18_TextChanged(object sender, EventArgs e)
        {
        }

        private void aetherTextbox18_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void aetherButton6_Click(object sender, EventArgs e)
        {
            string cID = aetherTextbox19.Text;
            string sID = aetherTextbox20.Text;
            getClassInfo(cID);
            getClassIDs();
            if (_classStudents.Exists(x => x == sID) && _classIDs.Exists(y => y == cID))
            {
                MySqlConnection con = new MySqlConnection(_connection);
                con.Open();
                if (MessageBox.Show("Do you want to drop this student?", "Drop Student", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string query = "DELETE FROM classattendance WHERE student_id = '" + sID + "'";
                    MySqlCommand comm = new MySqlCommand(query, con);
                    comm.ExecuteNonQuery();

                    query = "DELETE FROM studentclasses WHERE student_id = '" + sID + "'";
                    comm = new MySqlCommand(query, con);
                    comm.ExecuteNonQuery();
                }
                con.Close();

                MessageBox.Show("Student Dropped");
            }
            else
                MessageBox.Show("Class ID or Student ID not found!");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            aetherTextbox13.Text = row.Cells["student_id"].Value.ToString();
            aetherTextbox14.Text = row.Cells["first_name"].Value.ToString();
            aetherTextbox15.Text = row.Cells["last_name"].Value.ToString();
        }
        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            aetherTextbox13.Text = row.Cells["student_id"].Value.ToString();
            aetherTextbox14.Text = row.Cells["first_name"].Value.ToString();
            aetherTextbox15.Text = row.Cells["last_name"].Value.ToString();
        }

        private void ProfForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'attendanceDataSet.student' table. You can move, or remove it, as needed.
            //this.studentTableAdapter.Fill(this.attendanceDataSet.student);

        }

        public ProfForm(string uId)
        {
            InitializeComponent();
            _userId = uId; //user id of professor
            GetPid();
            RefreshGrid();
            aetherTextbox18.Text = DateTime.Now.ToString("yyyy-MM-dd");

            //User Profile Info
            profInfo(uId);
            label1.Text = pFN + " " + pLN;
            label2.Text = "Professor";
            label3.Text = totClass;
        }

        private void aetherButton7_Click(object sender, EventArgs e)
        {
            string cid = aetherTextbox17.Text;
            string currDate = aetherTextbox18.Text;

            for (int i = 0; i < _studentFirst.Count; i++)
            {
                if (chk[i].Checked)
                {
                    string studentID;
                    string fullName = chk[i].Text;
                    string[] name = fullName.Split(' ');

                    MySqlConnection con = new MySqlConnection(_connection);
                    con.Open();

                    string query = "SELECT student_id from student where first_name = @fN and last_name = @lN";
                    MySqlCommand comm = new MySqlCommand(query, con);
                    comm.Parameters.AddWithValue("@fN", name[0]);
                    comm.Parameters.AddWithValue("@lN", name[1]);
                    studentID = comm.ExecuteScalar().ToString();

                    //string query = "UPDATE classattendance SET date = @date, present = @pres WHERE firstName = @fN and lastName = @lN";
                    query = "INSERT INTO attendance.classattendance(class_id, prof_id, student_id, firstName, lastName, date, present)" +
                                     "VALUES (@Cid, @Pid, @Sid, @fN, @lN, @date, @pres)";

                    comm = new MySqlCommand(query, con);
                    comm.Parameters.AddWithValue("@Cid", cid);
                    comm.Parameters.AddWithValue("@Pid", _pId);
                    comm.Parameters.AddWithValue("@Sid", studentID);
                    comm.Parameters.AddWithValue("@fN", name[0]);
                    comm.Parameters.AddWithValue("@lN", name[1]);
                    comm.Parameters.AddWithValue("@date", currDate);
                    comm.Parameters.AddWithValue("@pres", 1);
                    comm.ExecuteNonQuery();

                    con.Close();
                }
                else
                {
                    string studentID;
                    string fullName = chk[i].Text;
                    string[] name = fullName.Split(' ');

                    MySqlConnection con = new MySqlConnection(_connection);
                    con.Open();

                    string query = "SELECT student_id from student where first_name = @fN and last_name = @lN";
                    MySqlCommand comm = new MySqlCommand(query, con);
                    comm.Parameters.AddWithValue("@fN", name[0]);
                    comm.Parameters.AddWithValue("@lN", name[1]);
                    studentID = comm.ExecuteScalar().ToString();

                    //string query = "UPDATE classattendance SET date = @date, present = @pres WHERE firstName = @fN and lastName = @lN";
                    query = "INSERT INTO attendance.classattendance(class_id, prof_id, student_id, firstName, lastName, date, present)" +
                                     "VALUES (@Cid, @Pid, @Sid, @fN, @lN, @date, @pres)";

                    comm = new MySqlCommand(query, con);
                    comm.Parameters.AddWithValue("@Cid", cid);
                    comm.Parameters.AddWithValue("@Pid", _pId);
                    comm.Parameters.AddWithValue("@Sid", studentID);
                    comm.Parameters.AddWithValue("@fN", name[0]);
                    comm.Parameters.AddWithValue("@lN", name[1]);
                    comm.Parameters.AddWithValue("@date", currDate);
                    comm.Parameters.AddWithValue("@pres", 0);
                    comm.ExecuteNonQuery();

                    con.Close();
                }
            }
            //MessageBox.Show("Attendance Marked");
            label5.Text= "Attendance has" + Environment.NewLine + "been marked";
        }

        private void aetherButton8_Click(object sender, EventArgs e)
        {
            string cID = label4.Text;
            string currDate = aetherTextbox18.Text;
            //_pID;
            MySqlConnection con = new MySqlConnection(_connection);
            con.Open();
            if (MessageBox.Show("Do you want remove the attedance for the date written above?", "Remove Attendance", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string query = "DELETE FROM attendance.classattendance WHERE class_id = @Cid and prof_id = @Pid and date = @date";
                MySqlCommand comm = new MySqlCommand(query, con);
                comm.Parameters.AddWithValue("@Cid", cID);
                comm.Parameters.AddWithValue("@Pid", _pId);
                comm.Parameters.AddWithValue("@date", currDate);
                comm.ExecuteNonQuery();
                label5.Text = "Attendance for"+ Environment.NewLine+ "that date has" + Environment.NewLine + "been removed";
            }
            con.Close();
            //MessageBox.Show("Attendance removed");
        }

        private void aetherButton9_Click(object sender, EventArgs e)
        {
            string cid = aetherTextbox17.Text;
            string currDate = aetherTextbox18.Text;

            for (int i = 0; i < _studentFirst.Count; i++)
            {
                if (chk[i].Checked)
                {
                    string studentID;
                    string fullName = chk[i].Text;
                    string[] name = fullName.Split(' ');

                    MySqlConnection con = new MySqlConnection(_connection);
                    con.Open();

                    string query = "SELECT student_id from student where first_name = @fN and last_name = @lN";
                    MySqlCommand comm = new MySqlCommand(query, con);
                    comm.Parameters.AddWithValue("@fN", name[0]);
                    comm.Parameters.AddWithValue("@lN", name[1]);
                    studentID = comm.ExecuteScalar().ToString();

                    query = "UPDATE classattendance SET present = @pres WHERE firstName = @fN and lastName = @lN and date = @date and class_id = @Cid";

                    comm = new MySqlCommand(query, con);
                    comm.Parameters.AddWithValue("@pres", 1);
                    comm.Parameters.AddWithValue("@fN", name[0]);
                    comm.Parameters.AddWithValue("@lN", name[1]);
                    comm.Parameters.AddWithValue("@date", currDate);
                    comm.Parameters.AddWithValue("@Cid", cid);
                    comm.ExecuteNonQuery();

                    con.Close();
                }
                else
                {
                    string studentID;
                    string fullName = chk[i].Text;
                    string[] name = fullName.Split(' ');

                    MySqlConnection con = new MySqlConnection(_connection);
                    con.Open();

                    string query = "SELECT student_id from student where first_name = @fN and last_name = @lN";
                    MySqlCommand comm = new MySqlCommand(query, con);
                    comm.Parameters.AddWithValue("@fN", name[0]);
                    comm.Parameters.AddWithValue("@lN", name[1]);
                    studentID = comm.ExecuteScalar().ToString();

                    query = "UPDATE classattendance SET present = @pres WHERE firstName = @fN and lastName = @lN and date = @date and class_id = @Cid";

                    comm = new MySqlCommand(query, con);
                    comm.Parameters.AddWithValue("@pres", 0);
                    comm.Parameters.AddWithValue("@fN", name[0]);
                    comm.Parameters.AddWithValue("@lN", name[1]);
                    comm.Parameters.AddWithValue("@date", currDate);
                    comm.Parameters.AddWithValue("@Cid", cid);
                    comm.ExecuteNonQuery();

                    con.Close();
                }
            }
            //MessageBox.Show("Attendance Updated");
            label5.Text = "Attendance has" + Environment.NewLine + "been updated";
        }

        private void aetherButton3_Click(object sender, EventArgs e)
        {

        }

        private void aetherButton1_Click(object sender, EventArgs e)
        {
            string cID = aetherTextbox10.Text;

            getExistingClass(_pId);

            if (_existClass.Exists(x => x == cID))
            {
                MySqlConnection con = new MySqlConnection(_connection);
                con.Open();

                if (MessageBox.Show("Do you want to delete this class?", "Delete Class", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {     
                    string query = "DELETE FROM class WHERE class_id = '" + cID + "' and prof_id = '" + _pId + "'";
                    MySqlCommand comm = new MySqlCommand(query, con);
                    comm.ExecuteNonQuery();
                }
 
                con.Close();

                MessageBox.Show("Class Deleted");
            }
            else
                MessageBox.Show("Class does not exist");
            
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void aetherButton2_Click(object sender, EventArgs e)
        {
            _cId = aetherTextbox3.Text;
            _cName = aetherTextbox4.Text;
            _cInstruct = aetherTextbox2.Text;
            _cRoom = aetherTextbox1.Text;

            if (string.IsNullOrEmpty(_cId))
                MessageBox.Show(@"Please enter a class ID!");
            else if (string.IsNullOrEmpty(_cName))
                MessageBox.Show(@"Please enter a class name!");
            else if (string.IsNullOrEmpty(_cInstruct))
                MessageBox.Show(@"Please enter the instructor!");
            else if (string.IsNullOrEmpty(_cRoom))
                MessageBox.Show(@"Please enter a class room!");
            else
            {
                MySqlConnection con = new MySqlConnection(_connection);
                con.Open();

                const string query = "INSERT INTO attendance.class(class_id, class_name, prof_id, instructor, room)" +
                                     "VALUES (@Cid, @cN, @pID, @ins, @rM)";

                MySqlCommand comm = new MySqlCommand(query, con);
                comm.Parameters.AddWithValue("@Cid", _cId);
                comm.Parameters.AddWithValue("@cN", _cName);
                comm.Parameters.AddWithValue("@pID", _pId);
                comm.Parameters.AddWithValue("@ins", _cInstruct);
                comm.Parameters.AddWithValue("@rM", _cRoom);
                comm.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(@"You have created your class!");
            }
        }

        private void GetPid()
        {
            MySqlConnection con = new MySqlConnection(_connection);
            con.Open();

            string query = "SELECT prof_id FROM professor WHERE user_id = @Uid";
            MySqlCommand comm = new MySqlCommand(query, con);
            comm.Parameters.AddWithValue("@Uid", _userId);
            _pId = comm.ExecuteScalar().ToString();
        }
        private void RefreshGrid()
        {
            string query = "SELECT student_id, first_name, last_name FROM student";
            MySqlConnection con = new MySqlConnection(_connection);
            con.Open();
            MySqlDataAdapter adapt = new MySqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            adapt.Fill(ds);
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            con.Close();
        }
        private void getStudentInfo(string cID)
        {
            MySqlConnection con = new MySqlConnection(_connection);
            con.Open();

            string query = "SELECT first_name FROM student s, studentclasses c WHERE c.class_id = '" + cID + "' and c.student_id = s.student_id and prof_id = '" + _pId + "'";
            using (MySqlCommand command = new MySqlCommand(query, con))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        _studentFirst.Add(reader.GetString(0));
                    }
                }
            }

            query = "SELECT last_name FROM student s, studentclasses c WHERE c.class_id = '" + cID + "' and c.student_id = s.student_id and prof_id = '" + _pId + "'";
            using (MySqlCommand command = new MySqlCommand(query, con))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        _studentLast.Add(reader.GetString(0));
                    }
                }
            }
        }
        private void getClassInfo(string cID)
        {
            MySqlConnection con = new MySqlConnection(_connection);
            con.Open();

            string query = "SELECT student_id FROM studentclasses where class_id = '" + cID + "'";
            using (MySqlCommand command = new MySqlCommand(query, con))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        _classStudents.Add(reader.GetString(0));
                    }
                }
            }
        }
        private void getClassIDs()
        {
            MySqlConnection con = new MySqlConnection(_connection);
            con.Open();

            string query = "SELECT class_id FROM studentclasses";
            using (MySqlCommand command = new MySqlCommand(query, con))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        _classIDs.Add(reader.GetString(0));
                    }
                }
            }
        }
        private void profInfo(string uid)
        {
            MySqlConnection con = new MySqlConnection(_connection);
            con.Open();

            string query = "SELECT first_name FROM attendance.professor WHERE user_id = @Uid";
            MySqlCommand comm = new MySqlCommand(query, con);
            comm.Parameters.AddWithValue("@Uid", uid);
            pFN = comm.ExecuteScalar().ToString();

            query = "SELECT last_name FROM attendance.professor WHERE user_id = @Uid";
            comm = new MySqlCommand(query, con);
            comm.Parameters.AddWithValue("@Uid", uid);
            pLN = comm.ExecuteScalar().ToString();

            query = "SELECT COUNT(*) from attendance.class WHERE prof_id = @Pid";
            comm = new MySqlCommand(query, con);
            comm.Parameters.AddWithValue("@Pid", _pId);
            totClass = comm.ExecuteScalar().ToString();

            con.Close();
        }
        private void getExistingClass(string Pid)
        {
            MySqlConnection con = new MySqlConnection(_connection);
            con.Open();

            string query = "SELECT class_id FROM attendance.class WHERE prof_id = '" + _pId + "'";
            using (MySqlCommand command = new MySqlCommand(query, con))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        _existClass.Add(reader.GetString(0));
                    }
                }
            }
        }
    }
}
