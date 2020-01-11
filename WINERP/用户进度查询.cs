using System;
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
using Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace WindowsFormsApplication1
{
    public partial class 用户进度查询 : UserControl
    {
        public 用户进度查询()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strsql = @"Server=DESKTOP-OB98F26\SERVER;database=WINERP;Persist Security Info=True;uid=sa;Password=guoang168168";//数据库链接字符串  
            string sql = "Proc_CustomerQuery";//要调用的存储过程名  
            SqlConnection conStr = new SqlConnection(strsql);//SQL数据库连接对象，以数据库链接字符串为参数  
            SqlCommand comStr = new SqlCommand(sql, conStr);//SQL语句执行对象，第一个参数是要执行的语句，第二个是数据库连接对象  
            comStr.CommandType = CommandType.StoredProcedure;//因为要使用的是存储过程，所以设置执行类型为存储过程  
            //依次设定存储过程的参数  
            comStr.Parameters.Add("@CustomerID", SqlDbType.Text).Value = textBox1.Text;
            comStr.Parameters.Add("@Rate_", SqlDbType.Text).Value = textBox2.Text;
            conStr.Open();//打开数据库连接  
                          //  MessageBox.Show(comStr.ExecuteNonQuery().ToString());//执行存储过程  
            SqlDataAdapter SqlDataAdapter1 = new SqlDataAdapter(comStr);
            System.Data.DataTable DT = new System.Data.DataTable();
            SqlDataAdapter1.Fill(DT);
            dataGridView1.DataSource = DT;
            conStr.Close();//关闭连接  

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)//判断是否有数据
                return;//返回
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();//实例化Excel对象
            excel.Application.Workbooks.Add(true);//在Excel中添加一个工作簿
            excel.Visible = true;//设置Excel显示
            //生成字段名称
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                excel.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;//将数据表格控件中的列表头填充到Excel中
            }
            //填充数据
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)//遍历数据表格控件的所有行
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)//遍历数据表格控件的所有列
                {
                    if (dataGridView1[j, i].ValueType == typeof(string))//判断遍历到的数据是否是字符串类型
                    {
                        excel.Cells[i + 2, j + 1] = "'" + dataGridView1[j, i].Value.ToString();//填充Excel表格
                    }
                    else
                    {
                        excel.Cells[i + 2, j + 1] = dataGridView1[j, i].Value.ToString();//填充Excel表格
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.printDialog1.ShowDialog() == DialogResult.OK)
            {
                this.printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //e.Graphics.DrawImage(dataGridView1, 0, 0, dataGridView1.Width, dataGridView1.Height);
        }
    }
}
