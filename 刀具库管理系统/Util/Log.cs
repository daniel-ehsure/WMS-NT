using System;
using System.IO;
using System.Configuration;
using System.Windows.Forms;
using DBCore;

namespace Util
{
    /// <summary>
    /// Log 的摘要说明。
    /// 这个类主要负责记录日志
    /// </summary>
    public class Log
    {
        private DBHelper dbHelper = new SQLDBHelper();
        public Log()
        {
        }

        /// <summary>
        /// 系统异常日志
        /// </summary>
        /// <param name="text"></param>
        public static void write(string text)
        {
            //从配置文件中获得日志保存的路径
            string path = ConfigurationManager.AppSettings["logpath"].ToString();

            //如果文件不存在则创建日志文件
            if (!File.Exists(path))
            {
                FileStream fs = File.Create(path);
                fs.Close();
            }

            //向日志中加如记录
            StreamWriter sw = File.AppendText(path);
            DateTime today = DateTime.Now;
            sw.WriteLine("*********************************************************");
            sw.WriteLine("发生错误时间： " + today.ToLongDateString() + "  " + today.ToLongTimeString());
            sw.WriteLine("操作人员： " + Global.userName);
            sw.WriteLine("错误信息：" + text);

            sw.Flush();
            sw.Close();
        }


        public static void writemess(string text)
        {
            //从配置文件中获得日志保存的路径
            string path = @"c:\Log.config";

            //如果文件不存在则创建日志文件
            if (!File.Exists(path))
            {
                FileStream fs = File.Create(path);
                fs.Close();
            }

            //向日志中加如记录
            StreamWriter sw = File.AppendText(path);
            DateTime today = DateTime.Now;
            sw.WriteLine("*********************************************************");
            sw.WriteLine("删除动作时间： " + today.ToLongDateString() + "  " + today.ToLongTimeString());
            sw.WriteLine("操作人员： " + Global.userName);
            sw.WriteLine("删除信息：" + text);

            sw.Flush();
            sw.Close();
        }

        /// <summary>
        /// 系统操作日志
        /// </summary>
        /// <param name="mess"></param>
        public static void saveLog(string mess)
        {
            try
            {
                string sql = " INSERT INTO [T_SYS_LOG]([username],[addtime],[mess]) " +
                                 "   VALUES ('" + Global.longid + "',getdate(),'" + mess + "')";
                new SQLDBHelper().ExecuteCommand(sql);
            }
            catch (Exception)
            {
                write("日志写入失败！内容：" + mess);
            }
        }
    }
}
