using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Util;
using System.IO;

namespace UI
{
    public partial class StartForm : Form
    {
       
        #region 全局变量
        private int width = 420; //窗体的最大宽度
        private int height = 400;//窗体的最大高度
        private int[] type = new int[] { 1, 2, 3, 4 }; //保存显示的类型数组
        private int flag = 1; //显示类型
        private bool show = false; //是否显示初始化窗体
        private MainForm mainform = null;//主窗体对象
        #endregion 
        public StartForm()
        {
            InitializeComponent();
        }

        public StartForm(MainForm mainform)
        {
            InitializeComponent();
            this.mainform = mainform;
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.label1.Text = Global.Corporation;

                //RegisterClass reg = new RegisterClass();
                //if (reg.isRegist() == false)
                //{
                //    if (File.Exists("CLIP.INF") == false)
                //    {
                //        MessageBox.Show("资源文件丢失，系统不能运行！");
                //        Application.Exit();
                //    }
                //    else
                //    {

                //        StreamReader sr = new StreamReader("CLIP.INF");
                //        string start = sr.ReadToEnd();
                //        sr.Close();
                //        if (start == null || string.Empty.Equals(start))
                //        {
                //            MessageBox.Show("资源文件被修改，系统不能运行！");
                //            Application.Exit();
                //        }
                //        else
                //        {
                //            EncryptionHelper help = new EncryptionHelper(EncryptionKeyEnum.KeyB);
                //            string[] keys = help.DecryptString(start).Split(' ');
                //            if (keys.Length >= 2)
                //            {
                //                DateTime begin = Convert.ToDateTime(keys[0]);
                //                DateTime end = Convert.ToDateTime(keys[1]);
                //                if (Convert.ToDateTime(DateTime.Now.ToLongDateString()) > begin)
                //                {
                //                    string s = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
                //                    string d = end.Year + "-" + end.Month + "-" + end.Day;
                //                    StreamWriter sw = new StreamWriter("CLIP.INF", false);
                //                    string ss = help.EncryptString(s + " " + d);
                //                    sw.Write(ss);
                //                    sw.Close();
                //                }
                //                if (DateTime.Now < begin)
                //                {
                //                    MessageBox.Show("系统时间不正常，请调整系统时间后重新运行程序！");
                //                    Application.Exit();
                //                }
                //                if (DateTime.Now > end)
                //                {
                //                    MessageBox.Show("重要文件丢失，系统不能运行！");
                //                    File.Delete("CLIP.INF");
                //                    Application.Exit();
                //                }
                //            }
                //            else
                //            {
                //                MessageBox.Show("系统文件被修改，系统不能运行！");
                //                Application.Exit();
                //            }

                //        }
                //    }
                //}




                //随机产生显示类型
                Random r = new Random();
                int index = r.Next(4);
                flag = type[index];
                //初始化窗体
                init(flag);

                this.timer1.Enabled = true;
                this.timer1.Start();
            }
            catch (Exception)
            {

                MessageBox.Show("资源文件被修改，系统不能运行！");
                Application.Exit();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (flag == 1 || flag == 2)
            {
                this.Width += 10;
                if (this.Width >= width)
                {
                    this.timer1.Enabled = false;
                    showCompany();
                }
            }
            else if (flag == 3 || flag == 4)
            {
                this.Height += 10;
                if (this.Height >= height)
                {
                    this.timer1.Enabled = false;

                    showCompany();
                }
            }
        }


        #region 自定义方法
        /// <summary>
        ///初始化窗体大小
        /// </summary>
        /// <param name="radom"></param>
        private void init(int radom)
        {
            switch (radom)
            {
                case 1:
                    this.Width = 20;
                    this.Height = 410;
                    break;
                case 2:
                    this.Width = 22;
                    this.Height = 410;
                    // this.RightToLeft = RightToLeft.Yes;
                    break;
                case 3:
                    this.Width = 420;
                    this.Height = 10;
                    break;
                case 4:
                    this.Width = 420;
                    this.Height = 10;
                    break;
            }
        }
        
        /// <summary>
        /// 显示初始化窗体
        /// </summary>
        private void showCompany()
        {
            bool setup = false;
            try
            {
                show = true;
                System.Threading.Thread.Sleep(1000);
                this.timer2.Enabled = true;
            }
            catch (Exception)
            {
                string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                if (constr == null || "".Equals(constr))
                {
                    setup = true;
                    MessageBox.Show("请设置数据库连接参数。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    mainform.exit = false;
                    Application.Exit();
                    return;
                }
            }
            if (setup == false)
            {
               
                    this.Close();
                    
                    Global.baseHeight = mainform.panel1.Height - 4;
                    Global.baseWidth = mainform.panel1.Width - 4;
                    mainform.Controls.Remove(mainform.panel1);



                    mainform.Opacity = 1;
                    mainform.Visible = true;
                }
            

        }
        #endregion

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.timer2.Enabled = false;
           
            this.Close();
        }

    }
}
