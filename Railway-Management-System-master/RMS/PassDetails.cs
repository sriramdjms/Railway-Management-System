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
    public partial class PassDetails : Form
    {
        String date, src, dest, train_name,email;
        public PassDetails()
        {
            InitializeComponent();
        }

        public PassDetails(String a,String b,String c,String d,String e)
        {
            InitializeComponent();
            comboBox1.Items.Add("Male");
            comboBox1.Items.Add("Female");
            comboBox1.Items.Add("Others");
            train_name = a;
            src = b;
            dest = c;
            date = d;
            email = e;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            create_passenger();
            this.Hide();
            new Payement(train_name,src,dest,date,email,textBox4.Text).Show();
        }

        private void Booking_Load(object sender, EventArgs e)
        {

        }

        public void create_passenger()
        {
            MySqlConnection con = new MySqlConnection("Data Source = localhost; user = root;                                                                                                                                                                            password = lol; database = rms");
            con.Open();

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = con;
            cmd.CommandText = "Insert into passenger values ('" + email + "','" + textBox1.Text + "'," + int.Parse(textBox2.Text) + ",'" + this.comboBox1.GetItemText(this.comboBox1.SelectedItem).ToString() + "','" + textBox4.Text + "')";
            cmd.CommandType = CommandType.Text;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e) { MessageBox.Show("Please check entered data"); }
            con.Close();
        }
    }
}
