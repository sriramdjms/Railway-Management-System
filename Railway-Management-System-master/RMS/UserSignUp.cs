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
    public partial class UserSignUp : Form
    {
        public UserSignUp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("Data Source = localhost; user = root;                                                                                                                                                                                                                                                password = lol; database = rms");
            con.Open();
            MySqlCommand cm = new MySqlCommand();
            cm.Connection = con;
            cm.CommandText = "insert into user values('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox6.Text + "','" + textBox4.Text + "'," + textBox5.Text + ",'" + textBox7.Text + "','" + textBox1.Text + "','" + textBox8.Text + "','" + maskedTextBox1.Text + "')";
            cm.CommandType = CommandType.Text;
            try
            {
                cm.ExecuteNonQuery();
                MessageBox.Show("Sign Up Successful!");
                con.Close();
                this.Hide();
                new Login().Show();
            }
            catch(Exception sd) { MessageBox.Show("Please check entered Values"); }
            con.Close();
        }
    }
}
