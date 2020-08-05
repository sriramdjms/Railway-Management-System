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
    public partial class CancelTicket : Form
    {
        String email;
        int rowNo;
        int pnr;
        public CancelTicket()
        {
            InitializeComponent();
        }

        public CancelTicket(String x)
        {
            InitializeComponent();
            email = x;
            rowNo = -1;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            rowNo = e.RowIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (rowNo != -1)
            {
                pnr = int.Parse(dataGridView1.Rows[rowNo].Cells[0].Value.ToString());

                MySqlConnection con = new MySqlConnection("Data Source = localhost; user = root;                                                                       password = lol; database = rms");
                con.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = con;
                cmd.CommandText = "Delete FROM ticket where pnr_no = " + pnr;
                cmd.CommandType = CommandType.Text;

                cmd.ExecuteNonQuery();
                MessageBox.Show("Ticket Cancelled!");
                con.Close();
            }
            else
            {
                MessageBox.Show("Please select a ticket!");
            }
        }

        private void CancelTicket_Load(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("Data Source = localhost; user = root;                                                                                                                                                                                                                                                password = lol; database = rms");
            con.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select pnr_no,t_date,pmobile as mobile from ticket where email = '" + email + "'";
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd.CommandText, con);
            da.Fill(ds, "tickets");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "tickets";
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new UserHome(email).Show();
        }
    }
}
