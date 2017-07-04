using System;
using System.IO;
using System.Configuration;
using System.Windows.Forms;
using DBCore;

namespace Util
{
    /// <summary>
    /// Log ��ժҪ˵����
    /// �������Ҫ�����¼��־
    /// </summary>
    public class Log
    {
        private DBHelper dbHelper = new SQLDBHelper();
        public Log()
        {
        }

        /// <summary>
        /// ϵͳ�쳣��־
        /// </summary>
        /// <param name="text"></param>
        public static void write(string text)
        {
            //�������ļ��л����־�����·��
            string path = ConfigurationManager.AppSettings["logpath"].ToString();

            //����ļ��������򴴽���־�ļ�
            if (!File.Exists(path))
            {
                FileStream fs = File.Create(path);
                fs.Close();
            }

            //����־�м����¼
            StreamWriter sw = File.AppendText(path);
            DateTime today = DateTime.Now;
            sw.WriteLine("*********************************************************");
            sw.WriteLine("��������ʱ�䣺 " + today.ToLongDateString() + "  " + today.ToLongTimeString());
            sw.WriteLine("������Ա�� " + Global.userName);
            sw.WriteLine("������Ϣ��" + text);

            sw.Flush();
            sw.Close();
        }


        public static void writemess(string text)
        {
            //�������ļ��л����־�����·��
            string path = @"c:\Log.config";

            //����ļ��������򴴽���־�ļ�
            if (!File.Exists(path))
            {
                FileStream fs = File.Create(path);
                fs.Close();
            }

            //����־�м����¼
            StreamWriter sw = File.AppendText(path);
            DateTime today = DateTime.Now;
            sw.WriteLine("*********************************************************");
            sw.WriteLine("ɾ������ʱ�䣺 " + today.ToLongDateString() + "  " + today.ToLongTimeString());
            sw.WriteLine("������Ա�� " + Global.userName);
            sw.WriteLine("ɾ����Ϣ��" + text);

            sw.Flush();
            sw.Close();
        }

        /// <summary>
        /// ϵͳ������־
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
                write("��־д��ʧ�ܣ����ݣ�" + mess);
            }
        }
    }
}
