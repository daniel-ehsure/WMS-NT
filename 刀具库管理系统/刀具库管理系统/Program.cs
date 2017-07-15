using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Util;

namespace UI
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Log.saveLog("系统启动！");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show("系统异常，请联系管理员！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Log.write(ex.Message);
            }
        }
    }
}
