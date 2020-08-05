using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;
using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System.Net.Mail;

namespace RMS
{
    public partial class Ticket : Form
    {
        String train_name, src, dest, date, email, mobile;

        private void button2_Click(object sender, EventArgs e)
        {
            savepdf();

            String eid = textBox1.Text;

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("trdbms.nonreply@gmail.com");
                mail.To.Add(eid);
                mail.Subject = "Train Ticket";
                mail.Body = "Your train ticket has been attached below";

                string path = Environment.CurrentDirectory;

                Attachment attachment;
                attachment = new Attachment(path + "/trainticket" + label11.Text + ".pdf");
                mail.Attachments.Add(attachment);


                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("trdbms.nonreply@gmail.com", "Whatever1709");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                MessageBox.Show("mail Send");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not send email, Please try again later.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new UserHome(email).Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            savepdf();
            string path = Environment.CurrentDirectory;
            Process.Start(path + "/trainticket" + label11.Text + ".pdf");
        }

        

        public Ticket()
        {
            InitializeComponent();
        }

        public Ticket(String a, String b, String c, String d, String e, String f)
        {
            InitializeComponent();
            train_name = a;
            src = b;
            dest = c;
            date = d;
            email = e;
            mobile = f;

        }

        private void Ticket_Load(object sender, EventArgs e)
        {
            label16.Text = train_name;
            label14.Text = mobile;
            label11.Text = date;

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
            label12.Text = dr["pass_name"].ToString();
            label13.Text = dr["gender"].ToString();

            cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select st_no as sa from station where st_name='" + src + "'";
            cmd.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new MySqlDataAdapter(cmd.CommandText, con);
            da.Fill(ds, "sna");
            dt = ds.Tables["sna"];
            dr = dt.Rows[0];
            String sta = dr["sa"].ToString();

            cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select st_no as sb from station where st_name='" + dest + "'";
            cmd.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new MySqlDataAdapter(cmd.CommandText, con);
            da.Fill(ds, "snb");
            dt = ds.Tables["snb"];
            dr = dt.Rows[0];
            String stb = dr["sb"].ToString();

            cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select train_no as tn from train natural join route where train_name='" + train_name + "' and src = '" + sta + "' and dest = '" + stb + "'";
            cmd.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new MySqlDataAdapter(cmd.CommandText, con);
            da.Fill(ds, "sta");
            dt = ds.Tables["sta"];
            dr = dt.Rows[0];
            String tn = dr["tn"].ToString();
            label15.Text = tn;
            label17.Text = src;
            label18.Text = dest;


            cmd = new MySqlCommand();
            cmd.Connection = con;
            //MessageBox.Show("select route_id as ri from train natural join route where train_name='" + train_name + "' and src = '" + sta + "' and dest = '" + stb + "'");
            cmd.CommandText = "select route_id as ri from train natural join route where train_name='" + train_name + "' and src = '" + sta + "' and dest = '" + stb + "'";
            cmd.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new MySqlDataAdapter(cmd.CommandText, con);
            da.Fill(ds, "ritbl");
            dt = ds.Tables["ritbl"];
            dr = dt.Rows[0];
            int rid = int.Parse(dr["ri"].ToString());

            cmd = new MySqlCommand();
            cmd.Connection = con;
            //MessageBox.Show("insert into ticket values(" + 0 + ",'" + date + "','" + mobile + "','" + tn + "','" + email + "'," + rid + ",'Confirmed')");
            cmd.CommandText = "insert into ticket values(" + 0 + ",'" + date + "','" + mobile + "','" + tn + "','" + email + "'," + rid + ",'Confirmed')";
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();

            cmd = new MySqlCommand();
            cmd.Connection = con;
            //MessageBox.Show("select pnr_no from ticket where pmobile = '" + mobile + "' and route_id = " + rid);
            cmd.CommandText = "select pnr_no from ticket where pmobile = '" + mobile + "' and route_id = " + rid;
            cmd.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new MySqlDataAdapter(cmd.CommandText, con);
            da.Fill(ds, "pnrquest");
            dt = ds.Tables["pnrquest"];
            dr = dt.Rows[0];
            int pnr = int.Parse(dr["pnr_no"].ToString());

            label10.Text = pnr.ToString();

            con.Close();


        }

        private void savepdf()
        {
            var doc = new Document();

            string path = Environment.CurrentDirectory;

            PdfWriter.GetInstance(doc, new FileStream(path + "/trainticket" + label11.Text + ".pdf", FileMode.Create));

            doc.Open();
            
            doc.AddTitle("Train Ticket");

            doc.Add(new Paragraph("Train Ticket"));

            doc.Add(new Paragraph("\n"));

            doc.Add(new Paragraph(label1.Text + "    " + label10.Text));

            doc.Add(new Paragraph("\n"));

            doc.Add(new Paragraph(label2.Text + "    " + label11.Text));

            doc.Add(new Paragraph("\n"));

            doc.Add(new Paragraph(label3.Text + "    " + label12.Text));

            doc.Add(new Paragraph("\n"));

            doc.Add(new Paragraph(label4.Text + "    " + label13.Text));

            doc.Add(new Paragraph("\n"));

            doc.Add(new Paragraph(label5.Text + "    " + label14.Text));

            doc.Add(new Paragraph("\n"));

            doc.Add(new Paragraph(label6.Text + "    " + label15.Text));

            doc.Add(new Paragraph("\n"));

            doc.Add(new Paragraph(label7.Text + "    " + label16.Text));

            doc.Add(new Paragraph("\n"));

            doc.Add(new Paragraph(label8.Text + "    " + label17.Text));

            doc.Add(new Paragraph("\n"));

            doc.Add(new Paragraph(label9.Text + "    " + label18.Text));

            doc.Close();
        }
    }
}
