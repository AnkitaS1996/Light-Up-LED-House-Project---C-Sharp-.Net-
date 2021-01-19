using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Light_Up_LED_House
{
    public partial class Frm_Add_Stock : Form
    {
        Global_Variable_Code_Class GVObj = new Global_Variable_Code_Class();
        public Frm_Add_Stock()
        {
            InitializeComponent();
        }

        //Enable False Function
        private void EnableFalse()
        {
            txt_ID.Enabled = false;
            cmb_Product_Name.Enabled = false;
            cmb_Sub_Product.Enabled = false;
            txt_Unit_Price.Enabled = false;
            txt_P_Price.Enabled = false;
            txt_Quantity.Enabled = false;
            dtp_Date.Enabled = false;
            txt_GST.Enabled = false;
            txt_Total_Price.Enabled = false;
        }

        //Enable True Function
        private void EnabledTrue()
        {
            cmb_Product_Name.Enabled = true;
            cmb_Sub_Product.Enabled = true;
            txt_Unit_Price.Enabled = true;
            txt_P_Price.Enabled = true;
            txt_Quantity.Enabled = true;
            dtp_Date.Enabled = true;
            txt_GST.Enabled = true;
            //txt_Total_Price.Enabled = true;
            txt_ID.Focus();
        }

        //Clear_Control Function
        private void Clear_Control()
        {
            txt_ID.Text = Convert.ToString(GVObj.AutoIncrement("Select count(Product_ID) from LED_Bulb_Stock_db", "Select Max(Product_ID) from LED_Bulb_Stock_db", 101));
            cmb_Product_Name.SelectedIndex = -1;
            cmb_Sub_Product.Text = "";
            txt_Unit_Price.Clear();
            txt_P_Price.Clear();
            txt_Quantity.Clear();
            txt_GST.Clear();
            dtp_Date.Text = "";
            txt_Total_Price.Clear();
        }

        //From Loading Code
        private void Frm_Add_Stock_Load(object sender, EventArgs e)
        {

            EnableFalse();
            //btn_Search.Visible = false;
            GVObj.Con_Open();
            //txt_ID.Text = Convert.ToString(GVObj.AutoIncrement("Select Count(Product_ID) from LED_Bulb_Stock_db", "Select Max(Product_ID) from LED_Bulb_Stock_db", 101));
            GVObj.FillDataGridView("Select * From LED_Bulb_Stock_db", DGV_Data_View);
            try
            {
                GVObj.ComboboxFill("Select Distinct(Product_Name) from LED_Bulb_Add_SubProduct_db", "Product_Name", cmb_Product_Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //SubProduct ComboBox Fill SelectedIndex Changed Coding 
        private void cmb_Product_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GVObj.ComboboxFill("Select (Sub_Product) from LED_Bulb_Add_SubProduct_db where Product_Name = '" + cmb_Product_Name.Text + "'", "Sub_Product", cmb_Sub_Product);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Combobox Sub_Product Textchanged Clear Code
        private void cmb_Product_Name_TextChanged(object sender, EventArgs e)
        {
            cmb_Sub_Product.Items.Clear();
        }

        //New Button Click Code
        private void btn_New_Click(object sender, EventArgs e)
        {
            EnabledTrue();
            //txt_ID.Text = Convert.ToString(GVObj.AutoIncrement("Select count(Product_ID) from LED_Bulb_Stock_db", "Select Max(Product_ID) from LED_Bulb_Stock_db", 101));
            Clear_Control();
        }

        //Region start KeyDown Handling Code
        private void cmb_Sub_Product_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Unit_Price.Focus();
            }
        }
        
        private void txt_Sales_Price_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_P_Price.Focus();
            }
        }
        private void txt_P_Price_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Quantity.Focus();
            }
        }

        private void txt_Quantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_GST.Focus();
            }
        }

        //End KeyDown Region

        //Buton Refresh Click Code
        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            btn_Save.Text = "Save";
            txt_ID.Text = "";
            Clear_Control();
        }

        /// Total Price Code Handling Code to Txt_Quantity Changed Code
        private void txt_Quantity_TextChanged(object sender, EventArgs e)
        {            
            if (txt_Quantity.Text.Length > 0)
            {
                txt_Total_Price.Text = (Convert.ToDecimal(txt_Unit_Price.Text) * Convert.ToInt32(txt_Quantity.Text)).ToString();
            }
            if(txt_Quantity.Text == "")
            {
                txt_Total_Price.Clear();
            }
        }

        //Save / Update Button Click Code
        private void btn_Save_Click(object sender, EventArgs e)
        {
            decimal PurchasePrice = 0;
           
           
            if(btn_Save.Text == "Save")
            {
                if (txt_P_Price.Text != "")
                {
                    PurchasePrice = Convert.ToDecimal(txt_P_Price.Text);
                }
                if (txt_GST.Text == "")
                {
                    txt_GST.Text = "0";
                    //txt_GST.Text = DBNull.Value.ToString();
                }
                GVObj.Con_Open();
                
                if (txt_ID.Text != "" && cmb_Product_Name.Text != "" && cmb_Sub_Product.Text != "" && txt_Unit_Price.Text != "" && txt_Quantity.Text != "" && txt_Total_Price.Text != "")
                {
                    //GVObj.ExecuteSqlCommand("Insert into LED_Bulb_Stock_db Values (" + txt_ID.Text + ",'" + cmb_Product_Name.Text + "','" + cmb_Sub_Product.Text + "'," + txt_Sales_Price.Text + ",'" + dtp_Date.Text + "'," + txt_P_Price.Text + ", " + txt_Quantity.Text + "," + txt_GST.Text + "," + txt_Total_Price.Text + ")");
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = ("Insert into LED_Bulb_Stock_db Values(" + txt_ID.Text + ",'" + cmb_Product_Name.Text + "','" + cmb_Sub_Product.Text + "','" + dtp_Date.Text + "'," + txt_Unit_Price.Text + "," + PurchasePrice + "," + txt_Quantity.Text + "," + txt_GST.Text + "," + txt_Total_Price.Text + ")");
                    cmd.Connection = GVObj.con;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Save Successful..!!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GVObj.FillDataGridView("Select * From LED_Bulb_Stock_db", DGV_Data_View);
                    Clear_Control();
                }
                else
                {
                    MessageBox.Show("1st Fill All The Field..!!!", "Fill All Terms", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                GVObj.Con_Close();
            }
            else
            {
                GVObj.Con_Open();
                {
                    if (txt_P_Price.Text != "")
                    {
                        PurchasePrice = Convert.ToDecimal(txt_P_Price.Text);
                    }
                    if (txt_GST.Text == "")
                    {
                        txt_GST.Text = "0";
                        //txt_GST.Text = DBNull.Value.ToString();
                    }
                    if (txt_ID.Text != "" && cmb_Product_Name.Text != "" && cmb_Sub_Product.Text != "" && txt_Unit_Price.Text != "" && txt_Quantity.Text != "" && txt_Total_Price.Text != "")
                    {
                       // GVObj.ExecuteSqlCommand("Update LED_Bulb_Stock_db SET Product_Name = '" + cmb_Product_Name.Text + "', Sub_Product = '" + cmb_Sub_Product.Text + "',Date = '" + dtp_Date.Text + "',Unit_Price = " + txt_Unit_Price.Text + ", Purchase_Price = " + txt_P_Price.Text + ", Quantity= " + txt_Quantity.Text + ", GST = " + txt_GST.Text + ", Total_Price = " + txt_Total_Price.Text + " where Product_ID = " + txt_ID.Text + "");
                       SqlCommand cmd = new SqlCommand("Update LED_Bulb_Stock_db SET Product_Name = '" + cmb_Product_Name.Text + "', Sub_Product = '" + cmb_Sub_Product.Text + "',Date = '" + dtp_Date.Text + "',Unit_Price = " + txt_Unit_Price.Text + ", Purchase_Price = " + PurchasePrice + ", Quantity= " + txt_Quantity.Text + ", GST = " + txt_GST.Text + ", Total_Price = " + txt_Total_Price.Text + " where Product_ID = " + txt_ID.Text + "");
                        cmd.Connection = GVObj.con;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record Update Successful...!!!","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        GVObj.FillDataGridView("Select * From LED_Bulb_Stock_db", DGV_Data_View);
                        Clear_Control();
                    }
                    else
                    {
                        MessageBox.Show("Record Not Updated..!!!", "Not Update Record", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    btn_Save.Text = "Save";
                }
            }    
        }

        private void DGV_Data_View_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EnabledTrue();
            btn_Save.Text = "Update";
           // DateTime Date = Convert.ToDateTime(dtp_Date.Text);
            if(e.RowIndex >= 0)
            {
                DataGridViewRow Row = this.DGV_Data_View.Rows[e.RowIndex];
                txt_ID.Text = Row.Cells[0].Value.ToString();
                cmb_Product_Name.Text = Row.Cells[1].Value.ToString();
                cmb_Sub_Product.Text = Row.Cells[2].Value.ToString();
                dtp_Date.Text = Convert.ToDateTime(Row.Cells[3].Value).ToString("dd-MM-yyyy");
                txt_Unit_Price.Text = Row.Cells[4].Value.ToString();
                txt_P_Price.Text = Row.Cells[5].Value.ToString();
                txt_Quantity.Text = Row.Cells[6].Value.ToString();
                txt_GST.Text = Row.Cells[7].Value.ToString();
                txt_Total_Price.Text = Row.Cells[8].Value.ToString();

            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            GVObj.Con_Open();
            if(txt_ID.Text != "" && cmb_Product_Name.Text != "" && cmb_Sub_Product.Text != "" && txt_Unit_Price.Text != "" && txt_Quantity.Text != "" && txt_Total_Price.Text != "")
            {
                GVObj.ExecuteSqlCommand("Delete from LED_Bulb_Stock_db where Product_ID = " + txt_ID.Text + "");
                MessageBox.Show("Record Deleted Successfuly...!!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GVObj.FillDataGridView("Select * From LED_Bulb_Stock_db", DGV_Data_View);
                Clear_Control();
            }
            else
            {
                MessageBox.Show("Record Not Deleted...!!!");
            }
            GVObj.Con_Close();
        }

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(txt_Search.Text != "")
                {
                    GVObj.FillDataGridView("Select * from LED_Bulb_Stock_db", DGV_Data_View);
                }
                if(cmb_Search_By.Text == "Product ID")
                {
                    GVObj.FillDataGridView("Select * from LED_Bulb_Stock_db where Product_ID like " + txt_Search.Text + "", DGV_Data_View);
                }
                if(cmb_Search_By.Text == "Product Name")
                {
                    GVObj.FillDataGridView("Select * from LED_Bulb_Stock_db where Product_Name like '" + txt_Search.Text + "%' ", DGV_Data_View);
                }
                if(cmb_Search_By.Text == "Sub Product")
                {
                    GVObj.FillDataGridView("Select * from LED_Bulb_Stock_db where Sub_Product like '" + txt_Search.Text + "%' ", DGV_Data_View);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox_Search_Click(object sender, EventArgs e)
        {
            cmb_Search_By.Enabled = true;
            txt_Search.Enabled = true;
        }

        private void txt_Unit_Price_TextChanged(object sender, EventArgs e)
        {
            if (txt_Unit_Price.Text == "")
            {
                txt_Quantity.Text = "";
                txt_Total_Price.Text = "";
            }
            /*else
            {
                txt_Unit_Price.Text = txt_Total_Price.Text;
            }*/
            
        }
    }
}
