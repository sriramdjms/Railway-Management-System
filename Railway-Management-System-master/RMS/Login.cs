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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        public void bindData()
        {
            MySqlConnection con = new MySqlConnection("Data Source = localhost; user = root;                                                                       password = lol; database = rms");
            con.Open();

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = con;
            cmd.CommandText = "SELECT email, password FROM USER";
            cmd.CommandType = CommandType.Text;

            DataSet ds = new DataSet();

            MySqlDataAdapter da = new MySqlDataAdapter(cmd.CommandText, con);

            da.Fill(ds, "user");

            DataTable dt = ds.Tables["user"];

            DataRow dr = dt.Rows[0];
            con.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String email = textBox1.Text;
            MySqlConnection con = new MySqlConnection("Data Source = localhost; user = root;                                                                       password = lol; database = rms");
            con.Open();
            try
            {
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM USER WHERE email = '" + email + "'";
                cmd.CommandType = CommandType.Text;

                DataSet ds = new DataSet();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd.CommandText, con);

                da.Fill(ds, "user");

                DataTable dt = ds.Tables["user"];

                DataRow dr = dt.Rows[0];

                String password = dr["password"].ToString();

                if (password == textBox2.Text)
                {
                    MessageBox.Show("Login Successful");
                    this.Hide();
                    new UserHome(email).Show();
                }
                else
                {
                    MessageBox.Show("Invalid Credentials");
                }
                con.Close();
            }
            catch(Exception qwe) { MessageBox.Show("User not present");
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bindData();
        }

        private void ealogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            new ealogin().Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Ticket().Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            new UserSignUp().Show();
        }
    }
}
