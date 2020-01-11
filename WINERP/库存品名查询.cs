using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection;

namespace WindowsFormsApplication1
{
    public partial class 库存品名查询 : UserControl
    {
        public 库存品名查询()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strsql = @"Server=DESKTOP-OB98F26\SERVER;database=SUM_BACKUP;Persist Security Info=True;uid=sa;Password=guoang168168;Connect Timeout=600";//数据库链接字符串  
            SqlConnection conStr = new SqlConnection(strsql);//SQL数据库连接对象，以数据库链接字符串为参数  
            conStr.Open();//打开数据库连接  

            string sql = "Proc_Parts2Blanks";//要调用的存储过程名  
            SqlCommand comStr = new SqlCommand(sql, conStr);//SQL语句执行对象，第一个参数是要执行的语句，第二个是数据库连接对象  
            comStr.CommandTimeout = 600;
            comStr.CommandType = CommandType.StoredProcedure;//因为要使用的是存储过程，所以设置执行类型为存储过程  
            //依次设定存储过程的参数  
            comStr.Parameters.Add("@L_RATE", SqlDbType.Text).Value = textBox3.Text;
            comStr.Parameters.Add("@S_RATE", SqlDbType.Text).Value = textBox4.Text;
            comStr.Parameters.Add("@M_RATE", SqlDbType.Text).Value =textBox5.Text;
            int result=comStr.ExecuteNonQuery();
            if (result > 0)
            {
                MessageBox.Show(result + "条记录运行成功!", "信息框");
            }
            else
            {
                MessageBox.Show("运行失败！");
            }
            //执行存储过程 MessageBox.Show(comStr.ExecuteNonQuery().ToString()); 

            string text = textBox1.Text;
            string dgvsql = "select * from TEST_P2B where PARTS='" + text + "'";
            SqlCommand cmd = new SqlCommand(dgvsql, conStr);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.SelectCommand.CommandTimeout = 500;
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            this.dataGridView1.DataSource = dt;
            //SqlDataAdapter SqlDataAdapter1 = new SqlDataAdapter(comStr);
            //System.Data.DataTable DT = new System.Data.DataTable();
            //SqlDataAdapter1.Fill(DT);
            //dataGridView1.DataSource = DT;
            //conStr.Close();//关闭连接  
        }
    }
}
