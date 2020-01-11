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

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

       

        private void 发运单证ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 库存品种查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string count = tab.Controls.Count.ToString();
            Image1 ding = new Image1();
            addTabControl(count, "国昂运营管理系统", tab, ding);
            //MessageBox.Show("启动自检系统！", "国昂运营管理系统", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void 订单录入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string count = tab.Controls.Count.ToString();
            订单录入 ding = new 订单录入();
            addTabControl(count, "订单录入", tab, ding);
        }

        /// <summary>
        /// 添加一个选项卡
        /// </summary>
        /// <param name="MainTabControlKey">选项卡的键值</param>
        /// <param name="MainTabControlName">选项卡要显示的名称</param>
        /// <param name="objTabControl">要添加到的TabControl对象</param>
        /// <param name="objfrm">要被添加到选项卡的用户控件对象</param>
        private void addTabControl(string MainTabControlKey, string MainTabControlName, TabControl objTabControl, UserControl objfrm)
        {
            try
            {
                if (ErgodicModiForm(MainTabControlKey, objTabControl))
                {
                    //声明一个选项卡对象
                    TabPage tabPage = new TabPage();
                    //选项卡的名称
                    tabPage.Name = MainTabControlKey;
                    //选项卡的文本
                    tabPage.Text = MainTabControlName;
                    //向选项卡集合添加新选项卡
                    objTabControl.Controls.Add(tabPage);
                    //子窗体显示
                    objfrm.Show();
                    //子窗体大小设置为选项卡大小
                    objfrm.Dock =DockStyle.Fill;
                    //将子窗体添加到选项卡中
                    tabPage.Controls.Add(objfrm);
                    //设置当前选项卡为新增选项卡
                    objTabControl.SelectedTab.Name = MainTabControlKey;
                }
                else
                {
                    //设为当前选中的选项
                    objTabControl.SelectTab(MainTabControlKey);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("提示信息", "添加选项卡时出错，请检查是否正确连接数据");
            }
        }
        private Boolean ErgodicModiForm(string MainTabControlKey, TabControl objTabControl)
        {
            //遍历选项卡判断是否存在该子窗体
            foreach (Control con in objTabControl.Controls)
            {
                TabPage tab = (TabPage)con;
                if (tab.Name == MainTabControlKey)
                {
                    return false;//存在
                }
            }
            return true;//不存在
        }

        private void 用户查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string count = tab.Controls.Count.ToString();
            用户查询 ding = new 用户查询();
            addTabControl(count, "用户查询", tab, ding);
        }

        private void 订单查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string count = tab.Controls.Count.ToString();
            订单查询 ding = new 订单查询();
            addTabControl(count, "订单查询", tab, ding);
        }

        private void 生产采购合同录入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string count = tab.Controls.Count.ToString();
            生产采购合同录入 ding = new 生产采购合同录入();
            addTabControl(count, "生产采购合同录入", tab, ding);
        }

        private void 供应商查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string count = tab.Controls.Count.ToString();
            供应商查询 ding = new 供应商查询();
            addTabControl(count, "供应商查询", tab, ding);
        }

        private void 生产采购合同查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string count = tab.Controls.Count.ToString();
            生产采购合同查询 ding = new 生产采购合同查询();
            addTabControl(count, "生产采购合同查询", tab, ding);
        }


        private void 产品入库单录入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string count = tab.Controls.Count.ToString();
            产品入库单录入 ding = new 产品入库单录入();
            addTabControl(count, "产品入库单录入", tab, ding);
        }

        private void 用户进度查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string count = tab.Controls.Count.ToString();
            用户进度查询 ding = new 用户进度查询();
            addTabControl(count, "用户进度查询", tab, ding);
        }

        private void 供应商进度查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string count = tab.Controls.Count.ToString();
            供应商进度查询 ding = new 供应商进度查询();
            addTabControl(count, "供应商进度查询", tab, ding);
        }

        private void 产品图片存取ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string count = tab.Controls.Count.ToString();
            ImageShow ding = new ImageShow();
            addTabControl(count, "产品图片存取", tab, ding);
        }

        private void 应付账款ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 供应商入库记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string count = tab.Controls.Count.ToString();
            供应商入库记录 ding = new 供应商入库记录();
            addTabControl(count, "供应商入库记录", tab, ding);
        }

        private void 库存品名查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string count = tab.Controls.Count.ToString();
            库存品名查询 ding = new 库存品名查询();
            addTabControl(count, "库存品名查询", tab, ding);
        }

        private void 入库单查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string count = tab.Controls.Count.ToString();
            入库单查询 ding = new 入库单查询();
            addTabControl(count, "入库单查询", tab, ding);
        }

        private void 用户发货信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string count = tab.Controls.Count.ToString();
            用户发货信息 ding = new 用户发货信息();
            addTabControl(count, "用户发货信息", tab, ding);
        }

        private void 发运号查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string count = tab.Controls.Count.ToString();
            发运号查询 ding = new 发运号查询();
            addTabControl(count, "发运号查询", tab, ding);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void tab_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void 产品信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string count = tab.Controls.Count.ToString();
            产品基础信息 ding = new 产品基础信息();
            addTabControl(count, "产品基础信息", tab, ding);

        }
    }

}
