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
    public partial class AddEmp : Form
    {

        MySqlConnection conn;
        MySqlCommand comm;
        MySqlDataAdapter da;
        DataSet ds;
        DataTable dt;
        DataRow dr;
        String tuple;
        String tuple2;
        int i = 0;
        public AddEmp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("Data Source = localhost; user = root;                                                                                                                                                                                                                                                                                                     password = lol; database = rms");
            con.Open();

            MySqlCommand cm = new MySqlCommand();
            cm.Connection = con;
            cm.CommandText = "insert into employee values('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox6.Text + "','" + textBox4.Text + "'," + textBox5.Text + ",'" + textBox7.Text + "','" + textBox1.Text + "','" + textBox8.Text + "','" + maskedTextBox1.Text + "','"+textBox9.Text+"')";
            cm.CommandType = CommandType.Text;
            try
            {
                cm.ExecuteNonQuery();
                MessageBox.Show("Inserted!");
            }
            catch(Exception sf) { MessageBox.Show("Please check enetered data"); }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AdmHome().Show();
        }
    }
}
