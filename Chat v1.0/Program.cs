using System;
using System.Windows.Forms;
using System.Threading;

namespace Chat_v1._0
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //bool prog;
            //Mutex mtx = new Mutex(true, "Chat_v1.0", out prog);
            //if (!prog)
            //{
            //    MessageBox.Show("Bu program zaten çalışıyor.", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //GC.KeepAlive(mtx);

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new splash());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new splash());
        }
    }
}
