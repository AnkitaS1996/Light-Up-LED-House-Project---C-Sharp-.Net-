using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Light_Up_LED_House
{
    public partial class Frm_Login : Form
    {      
        /// Use Class File Global Variable Code Class      
        Global_Variable_Code_Class GVObj = new Global_Variable_Code_Class();
        public Frm_Login()
        {
            InitializeComponent(); 
        }
    
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_Username_TextChanged(object sender, EventArgs e)
        {
            txt_Username.Focus();
            txt_Password.Enabled = true;
        }

        private void txt_Password_TextChanged(object sender, EventArgs e)
        {
            btn_Submit.Enabled = true;
        }

        private void Frm_Login_Load(object sender, EventArgs e)
        {

        }
 
        /// Start Region KeyDown Handle Code
        private void txt_Username_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txt_Password.Focus();
            }
        }

        private void txt_Password_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btn_Submit.Focus();
            }
        }
        /// End Region

        //// Start Region Login Submit Code
        private void btn_Submit_Click(object sender, EventArgs e)
        {
            GVObj.Con_Open();

            SqlCommand cmd = new SqlCommand("Select ID from Login_db where Username = '" + txt_Username.Text + "' And Password = '" + txt_Password.Text + "'",GVObj.con);

            if(Convert.ToInt32(cmd.ExecuteScalar()) > 0)
            {
                MessageBox.Show("Login Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Hide();
                //Frm_MDI_Light_Up_LED Form = new Frm_MDI_Light_Up_LED();
                Frm_MDI_Main_From_LED_Bulb Form = new Frm_MDI_Main_From_LED_Bulb();
                Form.Show();
            }
            else
            {
                MessageBox.Show("Invalid Login And Password", "failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Clear_Control();

        }
        /// Login Submit End Region
        
        ///Start Region Login Clear Control Code
        private void Clear_Control()
        {
            txt_Username.Text = "";
            txt_Password.Text = "";
            txt_Password.Enabled = false;
            btn_Submit.Enabled = false;
            txt_Username.Focus();
        }
        /// End Region
    }
}
