using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BLL;
using Util;
using Model;

namespace UI
{
    public partial class ModPassword : Form
    {
        LayoutBLL layoutBLL = new LayoutBLL();
        string id = string.Empty;
        T_Jb_Login_Pass user = null;
        public ModPassword(string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void ModPassword_Load(object sender, EventArgs e)
        {
            this.Left = (Global.baseWidth) / 2;
            this.Top = Global.baseHeight / 4;
           

            this.comboBox1.DataSource = layoutBLL.getAllRols(Global.role);
            this.comboBox1.DisplayMember = "C_JIAOSE";
            this.comboBox1.ValueMember = "C_JIAOSE";

            if ("超级管理员".Equals(Global.role))
            {
                comboBox1.Visible = true;
            }
            else
            {
                comboBox1.Visible = false;
            }

             user = layoutBLL.getUserById(id);
            if (user == null)
            {
                MessageBox.Show("获取用户信息失败！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
            else
            {
                this.label2.Text = user.C_loginID;
                this.textBox4.Text = user.C_chinesename;
                this.comboBox1.Text = user.C_jiaose;
            }

           // 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                bool changePassword = false;
               // string password = layoutBLL.getPasswordByName(Global.longid);
                if (this.textBox4.Text == null || string.Empty.Equals(this.textBox4.Text.Trim()))
                {
                    this.lblID.Visible = true;
                    return;
                }
                else
                {
                    this.lblID.Visible = false;

                }

                if (!(this.textBox2.Text == null || string.Empty.Equals(this.textBox2.Text.Trim())) && !(this.textBox3.Text == null || string.Empty.Equals(this.textBox3.Text.Trim())))
                {
                    changePassword= true;
                    if (this.textBox1.Text != user.C_password.Trim())
                    {
                        MessageBox.Show("原密码不正确，请输入正确原密码！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox1.Focus();
                        return;
                    }
                    if (textBox2.Text != textBox3.Text)
                    {
                        MessageBox.Show("新密码和验证密码不一致！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox3.Focus();
                        return;
                    }
                }

                user.C_chinesename = this.textBox4.Text.Trim();
                user.C_jiaose = this.comboBox1.Text;
                if (changePassword)
                {
                    user.C_password = textBox3.Text.Trim();
                }
                if (layoutBLL.updateUser(user))
                {
                    MessageBox.Show("用户信息更改成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Log.saveLog(string.Format("修改用户{0}成功！", user.C_loginID));
                    this.Close();
                }
                else
                {
                    MessageBox.Show("用户信息更改失败！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
