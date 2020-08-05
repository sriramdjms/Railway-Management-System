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
    public partial class RemEmp : Form
    {
        int rowNo;
        public RemEmp()
        {
            InitializeComponent();
            bindData();
            rowNo = -1;
        }

        public void bindData()
        {
            MySqlConnection con = new MySqlConnection("Data Source = localhost; user = root;                                                                       password = lol; database = rms");
            con.Open();

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = con;
            cmd.CommandText = "SELECT emp_id,fname as first_name,lname as last_name FROM EMPLOYEE";
            cmd.CommandType = CommandType.Text;

            DataSet ds = new DataSet();

            MySqlDataAdapter da = new MySqlDataAdapter(cmd.CommandText, con);

            da.Fill(ds, "employee");

            DataTable dt = ds.Tables["employee"];

            DataRow dr = dt.Rows[0];

            dataGridView1.DataSource = ds;

            dataGridView1.DataMember = "employee";

            con.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String id;
            if(checkBox1.Checked)
            {
                if (rowNo != -1)
                {
                    id = dataGridView1.Rows[rowNo].Cells[0].Value.ToString();

                    MySqlConnection con = new MySqlConnection("Data Source = localhost; user = root;                                                                       password = lol; database = rms");
                    con.Open();

                    MySqlCommand cmd = new MySqlCommand();

                    cmd.Connection = con;
                    cmd.CommandText = "Delete FROM employee where emp_id = '" + id + "'";
                    cmd.CommandType = CommandType.Text;

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Removed!");
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Please select an employee!");
                }
            }
            else
            {
                MessageBox.Show("Please check confirm box");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            rowNo = e.RowIndex;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AdmHome().Show();
        }
    }
}
