using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Light_Up_LED_House
{
    class Global_Variable_Code_Class
    {
        //Region Database Connection
        string Connection = (@"Data Source=.\SqlExpress;Initial Catalog=LED_Bulb_db;Integrated Security=True");
        public SqlConnection con;

        //Region Database Connction Open
        public void Con_Open()
        {
            con = new SqlConnection(Connection);
            if(con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }
        // Database Connection Open End Region

        //Region Database Connection Close
        public void Con_Close()
        {
            //con = new SqlConnection(Connection);
            if(con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        // Database Connection Open End Region

        // Region AutoIncrement Function
        public int AutoIncrement(string Get_Count, string Get_Max, int StartNo)
        {
            int cnt = 0;
            Con_Open();
            try
            {
                SqlCommand cmd = new SqlCommand(Get_Count, con);
                cnt = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();
                if(cnt > 0)
                {
                    cmd.CommandText = Get_Max;
                    cmd.Connection = con;
                    cnt = Convert.ToInt32(cmd.ExecuteScalar());
                    cnt = cnt + 1;   /// cnt += 1;
                    cmd.Dispose();
                    Con_Close();
                }
                else
                {
                    cnt += StartNo;
                }
               

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return cnt;
        }
        //End Region Auto Increment
            
        //Region Database Connection Table Code
        public void FillTableDB(string get_Query)
        {
            try
            {
                Con_Open();
                SqlDataAdapter sda = new SqlDataAdapter(get_Query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                dt.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        //FillTable Code End Region

        // Region DataGridView Connection Code
        public void FillDataGridView(string Query, DataGridView DGV)
        {
            try
            {
                Con_Open();
                SqlDataAdapter sda = new SqlDataAdapter(Query, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                DGV.DataSource = dt;
                sda.Dispose();
                dt.Dispose();
                Con_Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }      
        }
        // DataGridView End Region

        //Region Combobox Fill Code
        public void ComboboxFill(string Get_Query, string ColumnName, ComboBox cmb)
        {
            try
            {
                Con_Open();
                SqlCommand cmd = new SqlCommand(Get_Query, con);
                var obj = cmd.ExecuteReader();
                while(obj.Read())
                {
                    cmb.Items.Add(obj[ColumnName].ToString());
                }
                cmd.Dispose();
                Con_Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //ComboBox Fill End Region

        //ExecuteSqlCommand Code 
        public void ExecuteSqlCommand(string sql_query)
        {
                Con_Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql_query;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
           
        }
        ///End Region ExecuteSqlCommand
    }

}
 