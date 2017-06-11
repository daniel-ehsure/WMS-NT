using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Configuration;
using Model;

/*************
 * 2008.06.20
 *************/
namespace Util
{
    /// <summary>
    /// 该类是保存全局变量的类
    /// </summary>
    public class Global
    {
        public static bool warning = true;
        public static int Warningday = 0;
        //命名空间名 
        public const string namespceName = "UI";
        //用户名
        public static string userName = string.Empty;
        //操作员id
        public static string longid;
        //用户角色
        public static string role = "管理员";
        //打开子窗体的高度
        public static int baseHeight = Screen.PrimaryScreen.WorkingArea.Height - 24 - 25 - 15 - 52;
        //打开子窗体的宽度
        public static int baseWidth = Screen.PrimaryScreen.WorkingArea.Width - 8 - 131 - 8 - 12;
        //系统名称
        public static string Corporation = "刀具库管理系统";

        public static int CheckSize = Convert.ToInt32(ConfigurationManager.AppSettings["CheckSize"]);
        public static int MaxThick = Convert.ToInt32(ConfigurationManager.AppSettings["MaxThick"]);
        public static int IsAutoPlanCode = Convert.ToInt32(ConfigurationManager.AppSettings["IsAutoPlanCode"]);
       
        //可以登录的次数
        public static int loginTime = 3;
        public static T_Jb_Login_Pass login;


        /// <summary>
        /// 数据库中插入的最小日期值，因为SQL Server的最小值为 1/1/1753 12:00:00 所以定义此值
        /// </summary>
        public static DateTime minValue = Convert.ToDateTime("1777-1-1 0:00:00");

        public static bool canExit = true;

        public static bool closed;






        /// <summary>
        /// 垛机PLC IP
        /// </summary>
        public static string StackingIpSet = Convert.ToString(ConfigurationManager.AppSettings["StackingIpSet"].Trim());

        /// <summary>
        /// 垛机PLC 写入命令端口
        /// </summary>
        public static int StackingWritePort = Convert.ToInt32(ConfigurationManager.AppSettings["StackingWritePort"].Trim());

        /// <summary>
        /// 垛机PLC 读取命令端口
        /// </summary>
        public static int StackingReadPort = Convert.ToInt32(ConfigurationManager.AppSettings["StackingReadPort"].Trim());


        /// <summary>
        /// 管理下任务 0
        /// </summary>
        public static int InOn = Convert.ToInt32(ConfigurationManager.AppSettings["InOn"].Trim());


        /// <summary>
        /// 管理下任务 0
        /// </summary>
        public static int runStart = Convert.ToInt32(ConfigurationManager.AppSettings["runStart"].Trim());
        /// <summary>
        /// 监控开始执行 1
        /// </summary>
        public static int runBegin = Convert.ToInt32(ConfigurationManager.AppSettings["runBegin"].Trim());
        /// <summary>
        /// 取盘货位发送成功 2
        /// </summary>
        public static int runGetOutSendPlace = Convert.ToInt32(ConfigurationManager.AppSettings["runGetOutSendPlace"].Trim());
        /// <summary>
        /// 取盘开始执行 3
        /// </summary>
        public static int runGetOutRuning = Convert.ToInt32(ConfigurationManager.AppSettings["runGetOutRuning"].Trim());
        /// <summary>
        /// 托盘放在传输线上 4
        /// </summary>
        public static int runGetOutFinish = Convert.ToInt32(ConfigurationManager.AppSettings["runGetOutFinish"].Trim());
        /// <summary>
        /// 托盘回送 5
        /// </summary>
        public static int runTakeBackBegin = Convert.ToInt32(ConfigurationManager.AppSettings["runTakeBackBegin"].Trim());
        /// <summary>
        /// 回送送盘货位发送成功 6
        /// </summary>
        public static int runTakeBackSendPlace = Convert.ToInt32(ConfigurationManager.AppSettings["runTakeBackSendPlace"].Trim());
        /// <summary>
        /// 回送送盘开始执行 7
        /// </summary>
        public static int runTakeBackRuning = Convert.ToInt32(ConfigurationManager.AppSettings["runTakeBackRuning"].Trim());
        /// <summary>
        /// 完成删除数据 9
        /// </summary>
        public static int runFinish = Convert.ToInt32(ConfigurationManager.AppSettings["runFinish"].Trim());



        public static int a = 23184;
        public  static int b = 16656;
        public static string k = "ADVAPI32.DLL";


        /// <summary>
        /// 打印纸宽度
        /// </summary>
        public static int PrintWidth = Convert.ToInt32(ConfigurationManager.AppSettings["PrintWidth"].Trim());
        /// <summary>
        /// 打印纸高度
        /// </summary>
        public static int PrintHeight = Convert.ToInt32(ConfigurationManager.AppSettings["PrintHeight"].Trim());

        public static int BarcodeType = Convert.ToInt32(ConfigurationManager.AppSettings["BarcodeType"].Trim());
        public static int ProductOurType = Convert.ToInt32(ConfigurationManager.AppSettings["ProductOurType"].Trim());
    }
}
