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
    public partial class ModifyRoutes : Form
    {
        public ModifyRoutes()
        {
            InitializeComponent();
        }

        private void ModifyRoutes_Load(object sender, EventArgs e)
        {
            bindData();
        }

        public void bindData()
        {
            MySqlConnection con = new MySqlConnection("Data Source = localhost; user = root;                                                                       password = lol; database = rms");
            con.Open();

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM ROUTE";
            cmd.CommandType = CommandType.Text;

            DataSet ds = new DataSet();

            MySqlDataAdapter da = new MySqlDataAdapter(cmd.CommandText, con);

            da.Fill(ds, "train");

            DataTable dt = ds.Tables["train"];

            DataRow dr = dt.Rows[0];

            dataGridView1.DataSource = ds;

            dataGridView1.DataMember = "train";

            con.Close();

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            String train_no, src, dest, atime, dtime;
            int route_id;
            double price;

            train_no = textBox1.Text;
            price = Double.Parse(textBox2.Text);
            src = textBox3.Text;
            dest = textBox4.Text;
            atime = textBox5.Text;
            dtime = textBox6.Text;

            MySqlConnection con = new MySqlConnection("Data Source = localhost; user = root;                                                                       password = lol; database = rms");
            con.Open();

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = con;
            cmd.CommandText = "Insert into route values (0,'"+train_no+"','"+src+"','"+dest+"',"+price+",'"+atime+"','"+dtime+"')";
            cmd.CommandType = CommandType.Text;

            cmd.ExecuteNonQuery();
            MessageBox.Show("Inserted!");
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bindData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox7.Text);

            MySqlConnection con = new MySqlConnection("Data Source = localhost; user = root;                                                                       password = lol; database = rms");
            con.Open();

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = con;
            cmd.CommandText = "delete from route where route_id = "+id;
            cmd.CommandType = CommandType.Text;

            cmd.ExecuteNonQuery();
            MessageBox.Show("Deleted!");
            con.Close();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            new EmpHome().Show();
        }
    }
}
