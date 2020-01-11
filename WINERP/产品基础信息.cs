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
using System.Text.RegularExpressions;

namespace WindowsFormsApplication1
{
    public partial class 产品基础信息 : UserControl
    {
        String Conn = @"Server=DESKTOP-OB98F26\SERVER;database=WINERP;Persist Security Info=True;uid=sa;Password=guoang168168";
        SqlDataAdapter adapter = null;
        DataTable dt = new DataTable();
        DataTable dtErrorPO = new DataTable();
        public 产品基础信息()
        {
            InitializeComponent();
        }
        private void 产品基础信息_Load(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(Conn);
            connection.Open();
            string sql = "select ID,ProductID,PoNo,PoNote from IdPO";
            SqlCommand cmd = new SqlCommand(sql, connection);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            this.dataGridView2.DataSource = dt;
            String cmdTextInsert = "insert into ErrorPO (PoNo,ProductID) values (@PoNo,@ProductID)";
            int i = 0;
            while (i < dt.Rows.Count)
            {
                string @ProductID = dt.Rows[i][1].ToString();
                string @PoNo = dt.Rows[i][2].ToString();
                String pattern = @"^(\d{4}\-\d{2}\-\d{2})$";
                bool b = Regex.IsMatch(@ProductID, pattern);
                if (b == false)
                {
                    SqlCommand cmdInsert = new SqlCommand(cmdTextInsert, connection);
                    cmdInsert.Parameters.AddWithValue("@PoNo", dt.Rows[i][2]);
                    //cmdInsert.ExecuteNonQuery(); 
                    cmdInsert.Parameters.AddWithValue("@ProductID", dt.Rows[i][1]);
                    cmdInsert.ExecuteNonQuery();
                    i = i + 1;
                }
                else if (b == true)
                {
                    i = i + 1;
                }
            }
                string sqlErrorPO = "select * from ErrorPO";
                SqlCommand cmdErrorPO = new SqlCommand(sqlErrorPO, connection);
                adapter = new SqlDataAdapter(cmdErrorPO);
                adapter.Fill(dtErrorPO);
                this.dataGridView1.DataSource = dtErrorPO;
            }
        }
    }

