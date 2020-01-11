using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;

namespace WindowsFormsApplication1
{

    public partial class ImageShow : UserControl
    {
        String Conn = @"Server=ASUS-PC;database=WINERP;Persist Security Info=True;uid=sa;Password=guoang168168";
        public ImageShow()
        {
            InitializeComponent();
        }

        private void Image_Load(object sender, EventArgs e)
        {

        }

        private void Btn存图片_Click(object sender, EventArgs e)
        {

            openFileDialog1.Filter = "所有文件|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //获取当前选择的图片
                this.pictureBox1.Image = Image.FromStream(this.openFileDialog1.OpenFile());
                //获取当前图片的路径
                string path = openFileDialog1.FileName.ToString();

               string Name= System.IO.Path.GetFileName(openFileDialog1.FileName);
                //将制定路径的图片添加到FileStream类中   
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                //通过FileStream对象实例化BinaryReader对象
                BinaryReader br = new BinaryReader(fs);
                //通过BinaryReader类对象的ReadBytes()方法将FileStream类对象转化为二进制数组
                byte[] imgBytesIn = br.ReadBytes(Convert.ToInt32(fs.Length));
                // 第二步 将图片添加到数据库中
                //将图片添加到数据库中

                SqlConnection connection = new SqlConnection(Conn);
                connection.Open();
                string sql = "  insert into Pics (Name,Value) values(@Name,@Value)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Name;
                cmd.Parameters.Add("@Value", SqlDbType.VarBinary).Value = imgBytesIn;
                cmd.ExecuteNonQuery();
            }

             
        }

        private void Btn取图片_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            //将图片从数据库中取出
            SqlConnection connection = new SqlConnection(Conn);
            string sql = "select * from Pics ";
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //SQL图片文本导入DT
            da.Fill(dt);
            //转为二进制文件
            byte[] bytes = (byte[])dt.Rows[0]["Value"];
            //二进制文本转为图片
            MemoryStream mss = null;
            mss = new MemoryStream(bytes);
            //图片读取
            this.pictureBox2.Image = Image.FromStream(mss);
        }
    }
}
