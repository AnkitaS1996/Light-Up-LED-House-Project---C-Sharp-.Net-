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
    public partial class Frm_Add_Category : Form
    {
        /// Attached Global Function variable Class File
        Global_Variable_Code_Class GVObj = new Global_Variable_Code_Class();

        public Frm_Add_Category()
        {
            InitializeComponent();
        }
        private void EnabledTrue()      ///Enabled True Function
        {
            txt_PName.Enabled = true;
            txt_Sub_Product.Enabled = true;
        }

        private void EnabledFslse()      ///Enabled False Function
        {
            txt_PName.Enabled = false;
            txt_Sub_Product.Enabled = false;
        }
        
        ///Start Region Clear Controls
        private void Clear_Controls()
        {
            txt_PName.Text = "";
            txt_Sub_Product.Text = "";
            txt_PID.Text = Convert.ToString(GVObj.AutoIncrement("Select Count(Product_ID) from LED_Bulb_Add_SubProduct_db", "Select MAX(Product_ID) from LED_Bulb_Add_SubProduct_db", 101));
            txt_PName.Focus();
        }
        //End Region

        //Region Start From Loading Code
        private void Frm_Add_Category_Load(object sender, EventArgs e)
        {
            txt_PName.Focus();
            GVObj.Con_Open();
            //txt_PID.Text = Convert.ToString(GVObj.AutoIncrement("Select Count(Product_ID) from LED_Bulb_Add_SubProduct_db", "Select MAX(Product_ID) from LED_Bulb_Add_SubProduct_db", 101));
            GVObj.FillDataGridView("Select * from LED_Bulb_Add_SubProduct_db",dgv_Data);
        }
        //End Region

        /// Region Start New Button Code
        private void btn_New_Click(object sender, EventArgs e)
        {
            Clear_Controls();
            btn_Save.Text = "Save";
            EnabledTrue();
            txt_PID.Text = Convert.ToString(GVObj.AutoIncrement("Select Count(Product_ID) from LED_Bulb_Add_SubProduct_db", "Select MAX(Product_ID) from LED_Bulb_Add_SubProduct_db", 101));
            txt_PName.Focus();
           
        }
        //End Region

        //Region Start Save Button Code
        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_Save.Text == "Save")
                {
                    GVObj.Con_Open();
                    if (txt_PID.Text != "" && txt_PName.Text != "" && txt_Sub_Product.Text != "")
                    {
                        GVObj.ExecuteSqlCommand("Insert into LED_Bulb_Add_SubProduct_db Values(" + txt_PID.Text + ", '" + txt_PName.Text + "', '" + txt_Sub_Product.Text + "')");
                        MessageBox.Show("Record Save Successfully..!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GVObj.FillDataGridView("Select * from LED_Bulb_Add_SubProduct_db", dgv_Data);
                        Clear_Controls();
                    }
                    else
                    {
                        MessageBox.Show("1st Fill All The Field..!!", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    GVObj.Con_Open();
                }
                else
                {
                    GVObj.Con_Open();
                    if (txt_PID.Text != "" && txt_PName.Text != "" && txt_Sub_Product.Text != "")
                    {
                        GVObj.ExecuteSqlCommand("Update LED_Bulb_Add_SubProduct_db SET Product_Name = '" + txt_PName.Text + "', Sub_Product = '" + txt_Sub_Product.Text + "' where Product_ID = " + txt_PID.Text + "");
                        MessageBox.Show("Record Update Successfully...!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GVObj.FillDataGridView("Select * from LED_Bulb_Add_SubProduct_db", dgv_Data);
                        Clear_Controls();
                        btn_Save.Text = "Save";
                    }
                    else
                    {
                        MessageBox.Show("First Fill All Field", "Incomplete", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    }
                    GVObj.Con_Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //End Region

       // Region Start Refresh Button Code
        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            btn_Save.Text = "Save";
            txt_PName.Text = "";
            txt_Sub_Product.Text = "";
            txt_PID.Text = Convert.ToString(GVObj.AutoIncrement("Select Count(Product_ID) from LED_Bulb_Add_SubProduct_db", "Select MAX(Product_ID) from LED_Bulb_Add_SubProduct_db", 101));
            txt_PName.Focus();
        }
        //End Region

        //Region Start Key Down Handle Code
        private void txt_PName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txt_Sub_Product.Focus();
            }
        }
        //End Region

        //DataGrid View Cell Event Handling Code
        private void dgv_Data_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //btn_Update.Visible = true;
            EnabledTrue();
            btn_Save.Text = "Update";
            if(e.RowIndex >= 0)
            {
                DataGridViewRow Row = this.dgv_Data.Rows[e.RowIndex];
                txt_PID.Text = Row.Cells[0].Value.ToString();
                txt_PName.Text = Row.Cells[1].Value.ToString();
                txt_Sub_Product.Text = Row.Cells[2].Value.ToString();
            }
        }
        //End Region

        //Region Start at Update Button Code
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if(txt_PID.Text != "" && txt_PName.Text != "" && txt_Sub_Product.Text != "")
                {
                    GVObj.Con_Open();
                    GVObj.ExecuteSqlCommand("Delete from LED_Bulb_Add_SubProduct_db where Product_ID = " + txt_PID.Text + "");
                    MessageBox.Show("Record Deleted Successfully..!!!", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    GVObj.FillDataGridView("Select * from LED_Bulb_Add_SubProduct_db", dgv_Data);
                    Clear_Controls();
                    btn_Save.Text = "Save";
                }
                else
                {
                    MessageBox.Show("There is No Record Deleted","Failure", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                GVObj.Con_Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //End Region
        //Start Region Search By Search Selected IndexChenged Code
        private void cmb_SearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_Search.Text = "";
            txt_Search.Enabled = true;
            txt_Search.Focus();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            /*lbl_SearchBy.Visible = true;
            lbl_Serch.Visible = true;*/
            cmb_SearchBy.Enabled = true;
            txt_Search.Enabled= true;
            
        }

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(txt_Search.Text != "")
                {
                    GVObj.FillDataGridView("Select * from LED_Bulb_Add_SubProduct_db", dgv_Data);
                }
                if(cmb_SearchBy.Text == "Product ID")
                {
                    GVObj.FillDataGridView("Select * from LED_Bulb_Add_SubProduct_db where Product_ID like " + txt_Search.Text + "", dgv_Data);
                }
                if(cmb_SearchBy.Text == "Product Name")
                {
                    GVObj.FillDataGridView("Select * from LED_Bulb_Add_SubProduct_db where Product_Name like '" + txt_Search.Text + "%'", dgv_Data);
                }
                if(cmb_SearchBy.Text == "Sub Product")
                {
                    GVObj.FillDataGridView("Select * from LED_Bulb_Add_SubProduct_db where Sub_Product like  '" + txt_Search.Text + "%'", dgv_Data);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //End Region


    }
}
