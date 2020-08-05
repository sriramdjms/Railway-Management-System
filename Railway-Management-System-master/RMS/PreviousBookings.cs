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
    public partial class PreviousBookings : Form
    {
        String email;
        public PreviousBookings()
        {
            InitializeComponent();
        }

        public PreviousBookings(String x)
        {
            InitializeComponent();
            email = x;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new UserHome(email).Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void PreviousBookings_Load(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("Data Source = localhost; user = root;                                                                                                                                                                                                                                                password = lol; database = rms");
            con.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select pnr_no,t_date as travel_date, pmobile as mobile_number from ticket where email = '"+email+"'";
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd.CommandText, con);
            da.Fill(ds, "tickets");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "tickets";
            con.Close();
        }
    }
}
