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
    public partial class train_filter : Form
    {
        String date,src,dest,email;
        Boolean x;
        public train_filter(String mail)
        {
            InitializeComponent();
            bindData1();
            bindData2();
            x = true;
            email = mail;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            date = monthCalendar1.SelectionRange.Start.Date.ToString("yyyy-MM-dd");
            label1.Text = "Selected Date: " + date;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if(label1.Text != null)
            {
                src = this.comboBox1.GetItemText(this.comboBox1.SelectedItem).ToString();
                dest = this.comboBox2.GetItemText(this.comboBox2.SelectedItem).ToString();
                exCheck();
                if (x)
                {
                    this.Hide();
                    new search(date, src, dest,email).Show();
                };
                x = true;
            }
        }

        private void train_filter_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new UserHome(email).Show();
        }

        public void bindData1()
        {
            MySqlConnection con = new MySqlConnection("Data Source = localhost; user = root;                                                                       password = lol; database = rms");
            con.Open();

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = con;
            cmd.CommandText = "select st_name from station";
            cmd.CommandType = CommandType.Text;

            DataSet ds = new DataSet();

            MySqlDataAdapter da = new MySqlDataAdapter(cmd.CommandText, con);

            da.Fill(ds, "station");

            DataTable dt = ds.Tables["station"];

            //DataRow dr = dt.Rows[0];

            comboBox1.DataSource = dt.DefaultView;

            comboBox1.DisplayMember = "st_name";

            con.Close();
        }

        public void bindData2()
        {
            MySqlConnection con = new MySqlConnection("Data Source = localhost; user = root;                                                                       password = lol; database = rms");
            con.Open();

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = con;
            cmd.CommandText = "select st_name from station";
            cmd.CommandType = CommandType.Text;

            DataSet ds = new DataSet();

            MySqlDataAdapter da = new MySqlDataAdapter(cmd.CommandText, con);

            da.Fill(ds, "station");

            DataTable dt = ds.Tables["station"];

            //DataRow dr = dt.Rows[0];

            comboBox2.DataSource = dt.DefaultView;

            comboBox2.DisplayMember = "st_name";

            con.Close();
        }

        public void exCheck()
        {
            MySqlConnection con = new MySqlConnection("Data Source = localhost; user = root;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     password = lol ; database = rms");
            con.Open();

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = con;
            cmd.CommandText = "select tr.train_name,e.src_name as source,s.st_name as destination,price,arrivaltime,depttime,t_date as dept_date from extra e natural join train tr natural join station s,travel_date t where e.dest = s.st_no and e.train_no = t.train_no and t.t_date = STR_TO_DATE('" + date + "', '%Y-%m-%d') and e.src_name = '" + src + "' and s.st_name = '" + dest + "' order by e.train_no";
            cmd.CommandType = CommandType.Text;

            DataSet ds = new DataSet();

            MySqlDataAdapter da = new MySqlDataAdapter(cmd.CommandText, con);

            try
            {
                con.Close();
                da.Fill(ds, "route");

                DataTable dt = ds.Tables["route"];

                DataRow dr = dt.Rows[0];

            }
            catch (Exception e)
            {
                MessageBox.Show("No trains available on " + date + " for selected route");
                x = false;
            }
        }
    }
}
