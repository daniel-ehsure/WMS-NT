using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Util;
using Model;
using System.Configuration;
using BLL;

namespace UI
{
    public partial class LogInForm : Form
    {
        #region 全局变量
        //主窗体对象
        private MainForm mainform = null;
        //登录次数
        private int loginTimes = 0;
        LayoutBLL layoutBLL = new LayoutBLL();
        #endregion

        public LogInForm(MainForm mainform)
        {
            InitializeComponent();
            this.mainform = mainform;
            //设置显示位置
            this.Left = (Global.baseWidth - this.Width) / 2;
            this.Top = Global.baseHeight / 8;

        }
        public LogInForm()
        {
            InitializeComponent();
        }

        #region 事件处理程序

        private void LogInForm_Load(object sender, EventArgs e)
        {
            this.txtUserName.Text = ConfigurationManager.AppSettings["UserName"];
            this.txtPassword.Focus();
        }



        //确定按钮
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Log.saveLog(txtUserName.Text.Trim() + " 登陆");
            if (validate())//验证成功
            {
                //封装数据对象
                T_Jb_Login_Pass user = new T_Jb_Login_Pass();
                user.C_loginID = txtUserName.Text;
                user.C_password = txtPassword.Text;
              //  user.C_jiaose = "管理员";

                bool flag = false;
                try
                {
                    flag = layoutBLL.login(user);
                }
                catch (Exception)
                {
                    MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //如果登录成功
                if (flag)
                {

                    Global.userName = user.C_chinesename; //保存用户的中文名
                    Global.role = user.C_jiaose;//保存用户角色
                    Global.longid = user.C_loginID;
                    Global.login = user;


                    try
                    {
                        //设置主窗体标题
                        mainform.Text ="点创科技——"+ Global.Corporation + "   当前用户：" + Global.userName + "  当前时间：" + DateTime.Now.ToLongDateString()
                                        + " ";
                        //mainform.MainToolBar.BackColor = pnlPlaceholder.BackColor;
                        mainform.initDictionary();
                        mainform.initToolBar();

                        Utility.writeConfig("UserName", Global.longid);

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    this.Close();
                    this.Dispose();
                    mainform.login = null;


                }
                else //登录失败 如果3次失败则退出 否则提示还有多少次
                {
                    if (loginTimes == Global.loginTime)
                    {
                        Application.Exit();
                    }
                    else
                    {
                        MessageBox.Show("你第[" + (loginTimes + 1) + "]次登录没有成功，请核对用户名称、登录密码！", "提示：你有" + (Global.loginTime - loginTimes) + "次机会", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loginTimes++;
                    }
                }
            }
        }
        //返回按钮
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            mainform.login = null;
            Application.Exit();
        }
        #endregion

        #region 自定义方法
        /// <summary>
        /// 验证用户输入是否合法
        /// </summary>
        private bool validate()
        {
            if (txtUserName.Text == null || "".Equals(txtUserName.Text))
            {
                MessageBox.Show("请输入登录名称！", "提示：", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (txtPassword.Text == null || "".Equals(txtPassword.Text))
            {
                MessageBox.Show("请输入登录密码！", "提示：", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }
        #endregion
    }
}
