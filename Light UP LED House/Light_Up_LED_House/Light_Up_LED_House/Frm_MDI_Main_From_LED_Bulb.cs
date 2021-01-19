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
    public partial class Frm_MDI_Main_From_LED_Bulb : Form
    {
        public Frm_MDI_Main_From_LED_Bulb()
        {
            InitializeComponent();
            CustomizeDesign();
        }

        private void CustomizeDesign()
        {
            panel_Stock_SubMenu.Visible = false;
            panel_Report_SubMenu.Visible = false;
            Panel_Tools_SubMenu.Visible = false;
            panel_Help_SubMenu.Visible = false;
        }
        private void HideSubMenu()
        { 
            if(panel_Stock_SubMenu.Visible == true)
            {
                panel_Stock_SubMenu.Visible = false;
            }
            if(panel_Report_SubMenu.Visible == true)
            {
                panel_Report_SubMenu.Visible = false;
            }
            if(Panel_Tools_SubMenu.Visible == true)
            {
                Panel_Tools_SubMenu.Visible = false;
            }
            if(panel_Help_SubMenu.Visible == true)
            {
                panel_Help_SubMenu.Visible = false;
            }
        }
        private void ShowSubMenu(Panel SubMenu)
        {
            if(SubMenu.Visible == false)
            {
                HideSubMenu();
                SubMenu.Visible = true;
            }
            else
            {
                SubMenu.Visible = false;
            }
        }
        private void Frm_MDI_Main_From_LED_Bulb_Load(object sender, EventArgs e)
        {

        }

        private void btn_Stock_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panel_Stock_SubMenu);
        }

        private void btn_Report_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panel_Report_SubMenu);
        }

        private void btn_Tools_Click(object sender, EventArgs e)
        {
            ShowSubMenu(Panel_Tools_SubMenu);
        }

        private void btn_Add_Product_Click(object sender, EventArgs e)
        {
            Frm_Add_Category FACObj = new Frm_Add_Category();
            FACObj.WindowState = FormWindowState.Maximized;
            FACObj.MdiParent = this;
            FACObj.Show();
        }

        private void btn_Add_Stock_Click(object sender, EventArgs e)
        {
            Frm_Add_Stock FASObj = new Frm_Add_Stock();
            FASObj.WindowState = FormWindowState.Maximized;
            FASObj.MdiParent = this;
            FASObj.Show();
        }

        private void btn_View_Stock_Click(object sender, EventArgs e)
        {
            Frm_View_Product FVPObj = new Frm_View_Product();
            FVPObj.WindowState = FormWindowState.Maximized;
            FVPObj.MdiParent = this;
            FVPObj.Show();
        }

        private void btn_Billing_Report_Click(object sender, EventArgs e)
        {
            Frm_Billing_Part_Order FBPObj = new Frm_Billing_Part_Order();
            FBPObj.WindowState = FormWindowState.Maximized;
            FBPObj.MdiParent = this;
            FBPObj.Show();
        }

        private void btn_NotePad_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Notepad.exe");
        }

        private void btn_Calculator_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Calc.exe");
        }
    }
}
