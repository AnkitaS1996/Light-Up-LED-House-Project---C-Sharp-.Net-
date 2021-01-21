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
    public partial class Frm_View_Product : Form
    {
        Global_Variable_Code_Class GVObj = new Global_Variable_Code_Class();
        public Frm_View_Product()
        {
            InitializeComponent();
        }

        private void Frm_View_Product_Load(object sender, EventArgs e)
        {
            GVObj.Con_Open();
            GVObj.FillDataGridView("Select * from LED_Bulb_Stock_db", dgv_Data);

            try
            {
                GVObj.ComboboxFill("Select Distinct(Product_Name) from LED_Bulb_Stock_db", "Product_Name",cmb_Product_Name);
                GVObj.FillDataGridView("Select * from LED_Bulb_Stock_db", dgv_Data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            GVObj.Con_Close();
        }

        private void cmb_Product_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            GVObj.Con_Open();
            try
            {
                GVObj.ComboboxFill("Select (Sub_Product) from LED_Bulb_Stock_db where Product_Name = '" + cmb_Product_Name.Text + "'", "Sub_Product",cmb_Sub_Product);
                GVObj.FillDataGridView("Select * from LED_Bulb_Stock_db", dgv_Data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            GVObj.Con_Close();
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            cmb_Product_Name.SelectedIndex = -1;
            cmb_Sub_Product.SelectedIndex = -1;
            txt_Quantity.Clear();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
