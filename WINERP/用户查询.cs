using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    public partial class 用户查询 : UserControl
    {
        String Conn = @"Server=DESKTOP-OB98F26\SERVER;database=WINERP;Persist Security Info=True;uid=sa;Password=guoang168168";
        SqlDataAdapter adapter = null;
        DataTable dt = new DataTable();
        public 用户查询()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
          
            SqlConnection connection = new SqlConnection(Conn);
            connection.Open();
            string sql = "select * from PO where CustomerID='"+ text + "'";
            SqlCommand cmd = new SqlCommand(sql, connection);
             adapter = new SqlDataAdapter(cmd);
           
            adapter.Fill(dt);
            this.dataGridView1.DataSource = dt;
        }

        private void 用户查询_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if(dt==null||dt.Rows.Count<1|| adapter==null)
                {
                    MessageBox.Show("保存失败");
                    return;
                }
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Update(dt);
                MessageBox.Show("保存成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
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
