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
    public partial class search : Form
    {
        String date,src,dest,train_name,email;
        int rowNo;
        public search()
        {
            InitializeComponent();
        }

        public search(String x, String s, String d, String mail)
        {
            InitializeComponent();
            rowNo = -1;
            date = x;
            src = s;
            dest = d;
            email = mail;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new UserHome(email).Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            rowNo = e.RowIndex;
        }

        private void book_Click(object sender, EventArgs e)
        {
            if (rowNo!=-1)
            {
                train_name = dataGridView1.Rows[rowNo].Cells[0].Value.ToString();
                this.Hide();
                new PassDetails(train_name,src,dest,date,email).Show();
            }
            else
            {
                MessageBox.Show("Please select a train!");
            }
        }

        private void search_Load(object sender, EventArgs e)
        {
            bindData();
        }

        public void bindData()
        {
            MySqlConnection con = new MySqlConnection("Data Source = localhost; user = root;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     password = lol ; database = rms");
            con.Open();

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = con;
            cmd.CommandText = "with ext( route_id,train_no,src,dest,price,arrivaltime,depttime, src_name) as (select   route_id,train_no,src,dest,price,arrivaltime,depttime,st_name as src_name from route natural join station where route.src=station.st_no) select tr.train_name,e.src_name as source,s.st_name as destination,price,arrivaltime,depttime,t_date as dept_date from ext e natural join train tr natural join station s,travel_date t where e.dest=s.st_no and e.train_no=t.train_no and t.t_date=STR_TO_DATE('"+date+"','%Y-%m-%d')and e.src_name='"+src+"'and s.st_name='"+dest+"' order by e.train_no;";
            cmd.CommandType = CommandType.Text;

            DataSet ds = new DataSet();

            MySqlDataAdapter da = new MySqlDataAdapter(cmd.CommandText, con);

            try
            {
                con.Close();
                da.Fill(ds, "route");

                DataTable dt = ds.Tables["route"];

                DataRow dr = dt.Rows[0];

                dataGridView1.DataSource = ds;

                dataGridView1.DataMember = "route";

            }
            catch(Exception e)
            {
                MessageBox.Show("No trains available on " + date + " for selected route");
                this.Hide();
                new train_filter(email).Show();
            }
        }
    }
}
