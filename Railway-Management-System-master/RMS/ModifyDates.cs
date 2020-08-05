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
    public partial class ModifyDates : Form
    {
        int RowNo;
        public ModifyDates()
        {
            InitializeComponent();
            RowNo = 0;
        }

        private void ModifyDates_Load(object sender, EventArgs e)
        {
            bindData();
        }

        public void bindData()
        {
            MySqlConnection con = new MySqlConnection("Data Source = localhost; user = root;                                                                       password = lol; database = rms");
            con.Open();

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM TRAVEL_DATE";
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

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[RowNo].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[RowNo].Cells[1].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bindData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RowNo = e.RowIndex;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String id = textBox1.Text;
            String date = textBox2.Text;

            MySqlConnection con = new MySqlConnection("Data Source = localhost; user = root;                                                                       password = lol; database = rms");
            con.Open();

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = con;
            cmd.CommandText = "delete_date";
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlParameter pa1 = new MySqlParameter();
            pa1.ParameterName = "no";
            pa1.DbType = DbType.String;
            pa1.Value = id;
            cmd.Parameters.Add(pa1);

            MySqlParameter pa2 = new MySqlParameter();
            pa2.ParameterName = "dat";
            pa2.DbType = DbType.String;
            pa2.Value = date;
            cmd.Parameters.Add(pa2);

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted!");
            }
            catch (Exception ee) { MessageBox.Show("Please check entered values"); }
            con.Close();
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String id = textBox1.Text;
            String date = textBox2.Text;
            MessageBox.Show(id, date);
            MySqlConnection con = new MySqlConnection("Data Source = localhost; user = root;                                                                       password = lol; database = rms");
            con.Open();

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = con;
            cmd.CommandText = "insert into travel_date values('"+id+"','"+date+"',800,800)";
            cmd.CommandType = CommandType.Text;

            cmd.ExecuteNonQuery();
            MessageBox.Show("Inserted!");
            con.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            new EmpHome().Show();
        }
    }
}
