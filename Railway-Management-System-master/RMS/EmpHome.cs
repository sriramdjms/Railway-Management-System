using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMS
{
    public partial class EmpHome : Form
    {
        String id;
        public EmpHome()
        {
            InitializeComponent();
        }

        public EmpHome(String x)
        {
            InitializeComponent();
            id = x;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new ModifyRoutes().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new ModifyTrainDetails().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new ModifyDates().Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Login().Show();
        }
    }
}
