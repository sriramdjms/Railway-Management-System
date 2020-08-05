using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RMS
{
    public partial class UserHome : Form
    {
        String email;
        public UserHome()
        {
            InitializeComponent();
        }

        public UserHome(String x)
        {
            InitializeComponent();
            email = x;
        }

        private void UserHome_Load(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("Data Source = localhost; user = root;                                                                       password = lol; database = rms");
            con.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT fname,lname FROM USER WHERE email = '" + email + "'";
            cmd.CommandType = CommandType.Text;

            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd.CommandText, con);
            da.Fill(ds, "user");
            DataTable dt = ds.Tables["user"];
            DataRow dr = dt.Rows[0];

            label1.Text = "Welcome " + dr["fname"].ToString() + " " + dr["lname"].ToString();
        }

        private void search_Click(object sender, EventArgs e)
        {
            this.Hide();
            new train_filter(email).Show();
        }

        private void bookings_Click(object sender, EventArgs e)
        {
            this.Hide();
            new PreviousBookings(email).Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new CancelTicket(email).Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Login().Show();
        }
    }
}
