﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    public partial class 订单录入 : UserControl
    {
        private DataTable Shipping = new DataTable();
         DataTable common = new DataTable();

        public 订单录入()
        {
            InitializeComponent();
        }
        //选择
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog odXls = new OpenFileDialog();
                string folderPath = AppDomain.CurrentDomain.BaseDirectory + @"databackup\";
                odXls.InitialDirectory = folderPath;
                odXls.Filter = "Excel files office2003(*.xls)|*.xls|Excel office2013(*.xlsx)|*.xlsx|All files(*.*)|*.*";
                //包含各类文件fileName.Filter = "All files(*.*)|*.*|txt files(*.txt)|*.txt";
                odXls.FilterIndex = 2;
                odXls.RestoreDirectory = true;
                if (odXls.ShowDialog() == DialogResult.OK)
                {
                    this.textBox1.Text = odXls.FileName;
                    this.textBox1.ReadOnly = true;
                    string sConnString = string.Format("Provider=Microsoft.Ace.OLEDB.12.0;" + "Data Source={0};" + "Extended Properties='Excel 8.0;HDR=NO;IMEX=1';", odXls.FileName);
                    if ((System.IO.Path.GetExtension(textBox1.Text.Trim())).ToLower() == ".xls")
                    {
                        sConnString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + odXls.FileName + "Extended Properties=Excel 5.0;Persist Security Info=False";
                    }
                    using (OleDbConnection oleDbConn = new OleDbConnection(sConnString))
                    {
                        oleDbConn.Open();
                        DataTable dt = oleDbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                        if (cmb.Items.Count > 0)
                        {
                            cmb.DataSource = null;
                            cmb.Items.Clear();
                        }
                        foreach (DataRow dr in dt.Rows)
                        {
                            cmb.Items.Add((String)dr["TABLE_NAME"]);
                        }
                        if (cmb.Items.Count > 0)
                        {
                            cmb.SelectedIndex = 0;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //显示
        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection ole = null;
            OleDbDataAdapter da = null;

            string strConn = string.Format("Provider=Microsoft.Ace.OLEDB.12.0;" + "Data Source={0};" + "Extended Properties='Excel 8.0;HDR=NO;IMEX=1';", textBox1.Text.Trim());
            if ((System.IO.Path.GetExtension(textBox1.Text.Trim())).ToLower() == ".xls")
            {
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + textBox1.Text.Trim() + "Extended Properties=Excel 5.0;Persist Security Info=False";
            }
            string sTableName = cmb.Text.Trim();
            string strExcel = "select * from [" + sTableName + "]";
            try
            {
                ole = new OleDbConnection(strConn);
                ole.Open();
                da = new OleDbDataAdapter(strExcel, ole);
                Shipping = new DataTable();
                da.Fill(Shipping);
               

                this.dataGridView1.DataSource = Shipping;
               
                for (int i = 0; i < Shipping.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].HeaderCell.Value = Shipping.Rows[0][i].ToString();
                }
                dataGridView1.Rows.Remove(dataGridView1.Rows[0]);
               
                ole.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (ole != null)
                    ole.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OleDbConnection ole = null;
            OleDbDataAdapter da = null;
            DataTable dt = null;
            string strConn = string.Format("Provider=Microsoft.Ace.OLEDB.12.0;" + "Data Source={0};" + "Extended Properties='Excel 8.0;HDR=NO;IMEX=1';", textBox1.Text.Trim());
            if ((System.IO.Path.GetExtension(textBox1.Text.Trim())).ToLower() == ".xls")
            {
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + textBox1.Text.Trim() + "Extended Properties=Excel 5.0;Persist Security Info=False";
            }
            string sTableName = cmb.Text.Trim();
            string strExcel = "select * from [" + sTableName + "]";
            try
            {
                ole = new OleDbConnection(strConn);
                ole.Open();
                da = new OleDbDataAdapter(strExcel, ole);
                dt = new DataTable();
                da.Fill(dt);
                dt.Rows.Remove(dt.Rows[0]);
                String Conn = @"Server=DESKTOP-OB98F26\SERVER;database=WINERP;Persist Security Info=True;uid=sa;Password=guoang168168";
                SqlConnection connection = new SqlConnection(Conn);
                connection.Open();

                //判断一致性
                dt.Columns[0].ColumnName = "NO";
                dt.Columns[1].ColumnName = "CustomerID";
                dt.Columns[2].ColumnName = "PoNo";
                dt.Columns[3].ColumnName = "ProductID";
                dt.Columns[4].ColumnName = "Qtys";
                dt.Columns[5].ColumnName = "Price";
                dt.Columns[6].ColumnName = "PoDate";
                dt.Columns[7].ColumnName = "DeliveryDate";
                dt.Columns[8].ColumnName = "PoNote";
                dt.Columns[9].ColumnName = "Description";
                int index = 1;
                string text = "";
                foreach (DataRow dr in dt.Rows)
                {

                    index+= 1;
                    string sql = "select * from PO where CustomerID=@CustomerID and PoNo=@PoNo and ProductID=@ProductID";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.Add("@CustomerID", SqlDbType.NVarChar).Value = dr["CustomerID"].ToString();
                    cmd.Parameters.Add("@PoNo", SqlDbType.NVarChar).Value = dr["PoNo"].ToString();
                    cmd.Parameters.Add("@ProductID", SqlDbType.NVarChar).Value = dr["ProductID"].ToString();
                    //SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
                    //Adapter.Fill(dt_bidui);
                    //if(Adapter)
                    object result = cmd.ExecuteScalar();
                    if (result!=null)
                    {
                        text += "第" + index.ToString() + "行,";
                        continue;
                    }
                    
                }

                if (!string.IsNullOrEmpty(text))
                {
                    text += "有重复项，停止上传！";
                    MessageBox.Show(text);
                    return;
                }

                SqlBulkCopy Copy_connection = new SqlBulkCopy(connection);
                Copy_connection.DestinationTableName = "WINERP.dbo.PO";
                Copy_connection.WriteToServer(dt);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (ole != null)
                    ole.Close();
            }

        }

        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
