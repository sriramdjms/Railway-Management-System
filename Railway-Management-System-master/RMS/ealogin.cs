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
    public partial class ealogin : Form
    {
        public ealogin()
        {
            InitializeComponent();
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

            //dataGridView1.DataSource = ds;

            //dataGridView1.DataMember = "user";

            con.Close();

        }

        private void ealogin_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String id = textBox1.Text;
            if (emprb.Checked)
            {
                MySqlConnection con = new MySqlConnection("Data Source = localhost; user = root;                                                                       password = lol; database = rms");
                con.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM EMPLOYEE WHERE emp_id = '" + id + "'";
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
                    new EmpHome(id).Show();
                }
                else
                {
                    MessageBox.Show("Invalid Credentials");
                }
            }
            if(adminrb.Checked)
            {
                MySqlConnection con = new MySqlConnection("Data Source = localhost; user = root;                                                                       password = lol; database = rms");
                con.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM ADMIN WHERE adm_id = '" + id + "'";
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
                    new AdmHome().Show();
                }
                else
                {
                    MessageBox.Show("Invalid Credentials");
                }
            }
        }
    }
}
