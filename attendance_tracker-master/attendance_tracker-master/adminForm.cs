using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace attendance_tracker
{
    public partial class adminForm : Form
    {
        private readonly string _connection = GetConnectionStrings();
        private string uID, activity;
        public adminForm()
        {
            InitializeComponent();
            label1.Text = "Attendance Works Administrator";
            label2.Text = "Application Administrator";
            RefreshGrid();
        }

        static string GetConnectionStrings()
        {
            var settings = ConfigurationManager.ConnectionStrings["Connection"];
            return settings.ConnectionString;
        }

        private void RefreshGrid()
        {
            string query = "SELECT uL.user_id, uL.username, uL.password, p.first_name, p.last_name, p.email, uL.role, uL.active " +
                            "FROM user_login as uL, professor as p " +
                            "WHERE uL.user_id = p.user_id " +
                            "UNION " +
                            "SELECT uL.user_id, uL.username, uL.password, s.first_name, s.last_name, s.email, uL.role, uL.active " +
                            "FROM user_login as uL, student as s " +
                            "WHERE uL.user_id = s.user_id " +
                            "ORDER BY user_id";
            MySqlConnection con = new MySqlConnection(_connection);
            con.Open();
            MySqlDataAdapter adapt = new MySqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            adapt.Fill(ds);
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            con.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            aetherTextbox1.Enabled = true;
            aetherTextbox2.Enabled = false;
            aetherTextbox3.Enabled = false;
            aetherTextbox4.Enabled = false;
            aetherTextbox5.Enabled = false;
            aetherTextbox6.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            aetherTextbox1.Enabled = false;
            aetherTextbox2.Enabled = true;
            aetherTextbox3.Enabled = false;
            aetherTextbox4.Enabled = false;
            aetherTextbox5.Enabled = false;
            aetherTextbox6.Enabled = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            aetherTextbox1.Enabled = false;
            aetherTextbox2.Enabled = false;
            aetherTextbox3.Enabled = false;
            aetherTextbox4.Enabled = false;
            aetherTextbox5.Enabled = false;
            aetherTextbox6.Enabled = true;
        }

        private void aetherButton1_Click(object sender, EventArgs e)
        {
            string query;
            string first = aetherTextbox1.Text;
            string last = aetherTextbox2.Text;
            string role = aetherTextbox3.Text;
            string password = aetherTextbox4.Text;
            string user = aetherTextbox5.Text;
            string email = aetherTextbox6.Text;
            MySqlConnection con = new MySqlConnection(_connection);
            con.Open();
            //string query = "UPDATE classattendance SET date = @date, present = @pres WHERE firstName = @fN and lastName = @lN";
            if(radioButton1.Checked && role == "Professor")
            {
                query = "UPDATE professor SET first_name = @fN WHERE user_id = @uId";
                MySqlCommand comm = new MySqlCommand(query, con);
                comm.Parameters.AddWithValue("@fN", first);
                comm.Parameters.AddWithValue("@uId", uID);
                comm.ExecuteNonQuery();
            }
            else if (radioButton1.Checked && role == "Student")
            {
                query = "UPDATE student SET first_name = @fN WHERE user_id = @uId";
                MySqlCommand comm = new MySqlCommand(query, con);
                comm.Parameters.AddWithValue("@fN", first);
                comm.Parameters.AddWithValue("@uId", uID);
                comm.ExecuteNonQuery();
            }
            else if (radioButton2.Checked && role == "Professor")
            {
                query = "UPDATE professor SET last_name = @lN WHERE user_id = @uId";
                MySqlCommand comm = new MySqlCommand(query, con);
                comm.Parameters.AddWithValue("@lN", last);
                comm.Parameters.AddWithValue("@uId", uID);
                comm.ExecuteNonQuery();
            }
            else if (radioButton2.Checked && role == "Student")
            {
                query = "UPDATE student SET last_name = @lN WHERE user_id = @uId";
                MySqlCommand comm = new MySqlCommand(query, con);
                comm.Parameters.AddWithValue("@lN", last);
                comm.Parameters.AddWithValue("@uId", uID);
                comm.ExecuteNonQuery();
            }
            else if (radioButton3.Checked && role == "Professor")
            {
                query = "UPDATE professor SET email = @eM WHERE user_id = @uId";
                MySqlCommand comm = new MySqlCommand(query, con);
                comm.Parameters.AddWithValue("@eM", email);
                comm.Parameters.AddWithValue("@uId", uID);
                comm.ExecuteNonQuery();
            }
            else if (radioButton3.Checked && role == "Student")
            {
                query = "UPDATE student SET email = @eM WHERE user_id = @uId";
                MySqlCommand comm = new MySqlCommand(query, con);
                comm.Parameters.AddWithValue("@eM", email);
                comm.Parameters.AddWithValue("@uId", uID);
                comm.ExecuteNonQuery();
            }
            else if (radioButton4.Checked)
            {
                query = "UPDATE user_login SET username = @uN WHERE user_id = @uId";
                MySqlCommand comm = new MySqlCommand(query, con);
                comm.Parameters.AddWithValue("@uN", user);
                comm.Parameters.AddWithValue("@uId", uID);
                comm.ExecuteNonQuery();
            }
            else if (radioButton5.Checked)
            {
                query = "UPDATE user_login SET password = @pw WHERE user_id = @uId";
                MySqlCommand comm = new MySqlCommand(query, con);
                comm.Parameters.AddWithValue("@pw", password);
                comm.Parameters.AddWithValue("@uId", uID);
                comm.ExecuteNonQuery();
            }
            con.Close();
            RefreshGrid();
            MessageBox.Show("User updated");
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            aetherTextbox3.Text = row.Cells["role"].Value.ToString();
            aetherTextbox1.Text = row.Cells["first_name"].Value.ToString();
            aetherTextbox2.Text = row.Cells["last_name"].Value.ToString();
            aetherTextbox4.Text = row.Cells["password"].Value.ToString();
            aetherTextbox5.Text = row.Cells["username"].Value.ToString();
            aetherTextbox6.Text = row.Cells["email"].Value.ToString();
            uID = row.Cells["user_id"].Value.ToString();
            aetherTextbox7.Text = uID;
            activity = row.Cells["active"].Value.ToString();

            if (activity == "1")
                aetherButton2.Text = "Deactivate";
            else if (activity == "0")
                aetherButton2.Text = "Activate";

            this.Refresh();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            aetherTextbox3.Text = row.Cells["role"].Value.ToString();
            aetherTextbox1.Text = row.Cells["first_name"].Value.ToString();
            aetherTextbox2.Text = row.Cells["last_name"].Value.ToString();
            aetherTextbox4.Text = row.Cells["password"].Value.ToString();
            aetherTextbox5.Text = row.Cells["username"].Value.ToString();
            aetherTextbox6.Text = row.Cells["email"].Value.ToString();
            uID = row.Cells["user_id"].Value.ToString();
            aetherTextbox7.Text = uID;
            activity = row.Cells["active"].Value.ToString();

            if (activity == "1")
                aetherButton2.Text = "Deactivate";
            else if (activity == "0")
                aetherButton2.Text = "Activate";

            this.Refresh();
        }

        private void aetherTextbox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void aetherTextbox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            aetherTextbox1.Enabled = false;
            aetherTextbox2.Enabled = false;
            aetherTextbox3.Enabled = false;
            aetherTextbox4.Enabled = false;
            aetherTextbox5.Enabled = true;
            aetherTextbox6.Enabled = false;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            aetherTextbox1.Enabled = false;
            aetherTextbox2.Enabled = false;
            aetherTextbox3.Enabled = false;
            aetherTextbox4.Enabled = true;
            aetherTextbox5.Enabled = false;
            aetherTextbox6.Enabled = false;
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void aetherTextbox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }

        private void aetherButton2_Click(object sender, EventArgs e)
        {
            string query;
            MySqlConnection con = new MySqlConnection(_connection);
            con.Open();

            if (aetherButton2.Text == "Activate")
            {
                if (MessageBox.Show("Do you want activate the account?", "Activate Account", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    query = "UPDATE user_login SET active = @param";
                    MySqlCommand comm = new MySqlCommand(query, con);
                    comm.Parameters.AddWithValue("@param", 1);
                    comm.Parameters.AddWithValue("@id", uID);
                    comm.ExecuteNonQuery();
                }
            }
            else if (aetherButton2.Text == "Deactivate")
            {
                if (MessageBox.Show("Do you want deactivate the account?", "Deactivate Account", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    query = "UPDATE user_login SET active = @param WHERE user_id = @id";
                    MySqlCommand comm = new MySqlCommand(query, con);
                    comm.Parameters.AddWithValue("@param", 0);
                    comm.Parameters.AddWithValue("@id", uID);
                    comm.ExecuteNonQuery();
                }
            }
            RefreshGrid();
            con.Close();
        }
    }
}
