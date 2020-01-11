using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Text;

namespace WindowsFormsApplication1
{
    public delegate int ID1();
    class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]

        static int Thread1()
        {
            int id1 = Thread.CurrentThread.ManagedThreadId;
            return id1;
        }

        private void CallbackMethod(IAsyncResult ar)
        {
            //Retrieve the invoking delegate.
            ID1 dlgt = (ID1)ar.AsyncState;
            //Call EndInvoke to retrieve the results.
            int results = dlgt.EndInvoke(ar);
        }


        static void Main()
        {
            int ID = Thread.CurrentThread.ManagedThreadId;
            MessageBox.Show("主线程ID为" + Convert.ToString(ID) + "!");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //MessageBox.Show("启动自检系统！", "国昂运营管理系统", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            Application.Run(new Form1());

            //MessageBox.Show("主线程ID为"+Convert.ToString(ID)+"!");

            ID1 a = new ID1(Thread1);
            AsyncCallback callback = new AsyncCallback(Results);
            IAsyncResult aa = a.BeginInvoke();

        }
    }
}
