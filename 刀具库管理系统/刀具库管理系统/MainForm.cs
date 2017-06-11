using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using BLL;
using 工具箱控件;
using Model;
using Util;

namespace UI
{
    public partial class MainForm : Form
    {
        #region 全局属性
        public LogInForm login;
        private string formName = "";
        public bool exit = true;
        LayoutBLL bll = new LayoutBLL();

        //当前打开的窗体
        private Form currentForm = null;

        public Form CurrentForm
        {
            get { return currentForm; }
            set { currentForm = value; }
        }
        //保存功能与窗体关联的数据字典
        private Hashtable dictionary = new Hashtable();

        public Hashtable Dictionary
        {
            get { return dictionary; }
            set { dictionary = value; }
        }

        LayoutBLL layoutBLL = new LayoutBLL();
        private Hashtable table = new Hashtable();
        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            init();
        }

        #region 事件处理程序
        /// <summary>
        /// 隐藏左侧工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblPlaceholder7_Click(object sender, EventArgs e)
        {
            if (lblPlaceholder7.Text == "＜")
            {
                lblPlaceholder7.Text = "＞";
                tbn_show.Text = "显示";
                tbn_show.Tag = "显示左侧菜单";
                lblPlaceholder7.Tag = "显示左侧菜单";
                MainToolBar.Width = 0;
                lblPlaceholder6.Width = 0;
            }
            else
            {
                lblPlaceholder7.Text = "＜";
                tbn_show.Text = "隐藏";
                tbn_show.Tag = "隐藏左侧菜单";
                lblPlaceholder7.Tag = "隐藏左侧菜单";
                MainToolBar.Width = 170;
                lblPlaceholder6.Width = 8;
            }
            if (CurrentForm != null)
            {
                currentForm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                if (!CurrentForm.IsDisposed)
                {
                    CurrentForm.Show();
                }
            }
        }

        /// <summary>
        /// 注销用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbnLogout_Click(object sender, EventArgs e)
        {
            if (Global.canExit == true)
            {
                if (MessageBox.Show("你确认注销用户吗？", "确认：", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //如果当前有窗体打开 则关闭该窗体
                    for (int i = 0; i < this.MdiChildren.Length; i++)
                    {
                        this.MdiChildren[i].Close();
                    }

                    foreach (Form f in this.MdiChildren)
                    {
                        f.Close();
                    }

                    MainToolBar.Controls.Clear();
                    MainToolBar.BackColor = System.Drawing.Color.LightBlue;

                    


                    if (login == null)
                    {
                        login = new LogInForm(this); //显示登录窗口

                        login.MdiParent = this;
                        login.WindowState = FormWindowState.Normal;
                        login.Show();
                    }
                }
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbnExit_Click(object sender, EventArgs e)
        {
            if (Global.canExit == true)
            {
                Application.Exit();
            }
        }
        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbnChangePassword_Click(object sender, EventArgs e)
        {
            ModPassword modpassword = new ModPassword(Global.longid);
            // modpassword.MdiParent = this;
            modpassword.ShowDialog();
        }
        #endregion

        #region 自定义方法

        private void openWatchMain()
        {
            try
            {
                string key = "设备调度";
                string formText = "  " + key;
                formName = Dictionary[key].ToString();
                if (!ShowChildrenForm(formText))
                {
                    //以下是通过反射 获得该窗体的实例
                    Type t = Type.GetType(Global.namespceName + "." + formName);
                    CurrentForm = (Form)Activator.CreateInstance(t);

                    CurrentForm.MdiParent = this;
                    //设置窗体的标题
                    CurrentForm.Text = formText;
                    if (!CurrentForm.IsDisposed)
                    {
                        CurrentForm.WindowState = FormWindowState.Maximized;
                        CurrentForm.Show();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 处理工具栏中相关项的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PanelEvent(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (sender is Label)
                {
                    try
                    {
                        string key = ((Label)sender).Text; //获得数据字典中的key 即功能项目的名称
                        string formText = "  " + key;
                        formName = Dictionary[key].ToString();  //获得数据字典中的 对应窗口的类名


                        if (!ShowChildrenForm(formText))
                        {
                            
                            //以下是通过反射 获得该窗体的实例
                           
                            Type t = Type.GetType(Global.namespceName + "." + formName);
                        
                            CurrentForm = (Form)Activator.CreateInstance(t);


                        

                            CurrentForm.MdiParent = this;
                            //设置窗体的标题
                            CurrentForm.Text = formText;
                            if (!CurrentForm.IsDisposed)
                            {
                                CurrentForm.WindowState = FormWindowState.Maximized;
                                CurrentForm.Show();
                            }
                        }
                       
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("该功能出现错误：" + ex.Message);
                    }
                }
                else
                {
                    PanelIcon pic = (PanelIcon)sender;
                    List<Outlook_Table_Jb_User> list = (List<Outlook_Table_Jb_User>)table[pic.iconPanel];
                    Outlook_Table_Jb_User jb_user = list[pic.Index];
                    try
                    {
                        string key = jb_user.Itemname; //获得数据字典中的key 即功能项目的名称
                        string formText = "  " + key;
                        formName = Dictionary[key].ToString();  //获得数据字典中的 对应窗口的类名

                        if (!ShowChildrenForm(formText))
                        {
                            //以下是通过反射 获得该窗体的实例
                            Type t = Type.GetType(Global.namespceName + "." + formName);
                            CurrentForm = (Form)Activator.CreateInstance(t);

                            CurrentForm.MdiParent = this;
                            //设置窗体的标题
                            CurrentForm.Text = "  " + key;
                            if (!CurrentForm.IsDisposed)
                            {
                                CurrentForm.WindowState = FormWindowState.Maximized;
                                CurrentForm.Show();
                            }
                        }
                       
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("该功能出现错误：" + ex.Message);
                    }
                }
            }
        }

        //防止打开多个窗体
        private bool ShowChildrenForm(string p_ChildrenFormText)
        {
            int i;
            //依次检测当前窗体的子窗体
            for (i = 0; i < this.MdiChildren.Length; i++)
            {
                //判断当前子窗体的Text属性值是否与传入的字符串值相同
                if (this.MdiChildren[i].Text == p_ChildrenFormText)
                {
                    currentForm = this.MdiChildren[i];
                    //如果值相同则表示此子窗体为想要调用的子窗体，激活此子窗体并返回true值
                    this.MdiChildren[i].Activate();
                    return true;
                }
            }
            //如果没有相同的值则表示要调用的子窗体还没有被打开，返回false值
            return false;
        }

        /// <summary>
        /// 系统初始化
        /// </summary>
        private void init()
        {
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            StartForm st = new StartForm(this);
            st.Show();


            login = new LogInForm(this); //显示登录窗口
            login.MdiParent = this;
            login.Show();
           
        }

        /// <summary>
        /// 初始化工具栏中的内容
        /// </summary>
        public void initToolBar()
        {
            List<string> groups = layoutBLL.getAllGroupName();
            for (int i = 0; i < groups.Count; i++)
            {
                IconPanel iconPanel1 = new IconPanel();

                MainToolBar.AddBand(groups[i].ToString(), iconPanel1);
                List<Outlook_Table_Jb_User> items = layoutBLL.getItemNameByGroupName(groups[i].ToString());
                table.Add(iconPanel1, items);
                for (int j = 0; j < items.Count; j++)
                {
                    Outlook_Table_Jb_User outlook = items[j];
                    iconPanel1.AddIcon(outlook.Itemname, imlMain.Images[outlook.Picname], new MouseEventHandler(PanelEvent));
                }
            }
            //if (groups.Count < 3)
            //{
            MainToolBar.SelectBand(0);
            //}
            //else
            //{
            //    MainToolBar.SelectBand(3);
            //    openWatchMain();
            //}
        }
        /// <summary>
        /// 初始化数据字典
        /// </summary>
        public void initDictionary()
        {
            this.Dictionary = layoutBLL.initDictionary();
        }
        #endregion

        private void btnBakData_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("数据库初始化之后信息将不能恢复，是否确认删除?", "提示:", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (bll.resetDataBase())
                {
                    MessageBox.Show("数据库初始化成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("数据库初始化失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

       

       
       
    }
}
