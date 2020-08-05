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
    public partial class Payement : Form
    {
        String train_name, src, dest, date, email, mobile;
        int route_id;
        public Payement()
        {
            InitializeComponent();
        }

        public Payement(String a,String b, String c,String d,String e,String f)
        {
            InitializeComponent();
            route_id = 2;

            train_name = a;
            src = b;
            dest = c;
            date = d;
            email = e;
            mobile = f;
        }

        private void Payement_Load(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("Data Source = localhost; user = root;                                                                       password = lol; database = rms");
            con.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT price FROM route WHERE route_id = " + route_id + "";
            cmd.CommandType = CommandType.Text;

            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd.CommandText, con);
            da.Fill(ds, "route");
            DataTable dt = ds.Tables["route"];
            DataRow dr = dt.Rows[0];

            label3.Text = "Rs. " + dr["price"].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("Data Source = localhost; user = root;                                                                       password = lol; database = rms");
            con.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT distinct pass_name,gender FROM PASSENGER WHERE email = '" + email + "' and pmobile = '" + mobile + "'";
            cmd.CommandType = CommandType.Text;

            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd.CommandText, con);
            da.Fill(ds, "pass");
            DataTable dt = ds.Tables["pass"];
            DataRow dr = dt.Rows[0];

            this.Hide();
            new Ticket(train_name,src,dest,date,email,mobile).Show();
        }
    }
}
