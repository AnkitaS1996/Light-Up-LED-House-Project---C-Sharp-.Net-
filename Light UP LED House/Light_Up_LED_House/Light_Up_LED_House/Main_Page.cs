using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Light_Up_LED_House
{
    public partial class frm_Main_Page : Form
    {
        int Progress = 0;
        public frm_Main_Page()
        {
            InitializeComponent();
        }

        private void frm_Main_Page_Load(object sender, EventArgs e)
        {
            // this.timer1.Start();
            timer1.Enabled = true;
            timer1.Interval = 50;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Increment(1);
            //INCREMENT OUR PROGRESS VALUE
            Progress += 1;

            if(Progress >= 100)
            {
                timer1.Enabled = false;
                timer1.Stop();
                this.Hide();
                Frm_Login obj = new Frm_Login();
                obj.Show();
            }
        }

        
    }
}
