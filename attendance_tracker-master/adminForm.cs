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
            string query = "SELECT uL.user_id, p.first_name, p.last_name, uL.role " +
                            "FROM user_login as uL, professor as p " +
                            "WHERE uL.user_id = p.user_id " +
                            "UNION " +
                            "SELECT uL.user_id, s.first_name, s.last_name, uL.role " +
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
    }
}
