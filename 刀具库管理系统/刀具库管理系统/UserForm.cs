using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BLL;
using Model;
using Util;

namespace UI
{
    public partial class UserForm : Form
    {
        LayoutBLL layoutBLL = new LayoutBLL();
        public UserForm()
        {
            InitializeComponent();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            bool flag = true;

            #region 验证输入
            if (this.txtId.Text == null || string.Empty.Equals(this.txtId.Text.Trim()))
            {
                this.lblID.Visible = true;
                flag = false;
            }
            else
            {
                this.lblID.Visible = false;
            }
            if (this.txtPassword.Text == null || string.Empty.Equals(this.txtPassword.Text.Trim()))
            {
                this.lblPassword.Visible = true;
                flag = false;
            }
            else
            {
                this.lblPassword.Visible = false;
            }
            if (this.txtName.Text == null || string.Empty.Equals(this.txtName.Text.Trim()))
            {
                this.lblName.Visible = true;
                flag = false;
            }
            else
            {
                this.lblName.Visible = false;
            }
            if (this.comboBox1.Text == null || string.Empty.Equals(this.comboBox1.Text.Trim()))
            {
                this.label19.Visible = true;
                flag = false;
            }
            else
            {
                this.label19.Visible = false;
            }
            #endregion

            if (flag) //输入正确
            {
                try
                {
                    string pwd = layoutBLL.getPasswordByName(txtId.Text.Trim());
                    if (pwd == null)
                    {
                        T_Jb_Login_Pass user = new T_Jb_Login_Pass();
                        user.C_loginID = txtId.Text.Trim();
                        user.C_password = txtPassword.Text.Trim();
                        user.C_chinesename = txtName.Text.Trim();
                        user.C_jiaose = comboBox1.Text;
                        user.I_state = 0;
                        if (layoutBLL.save(user))
                        {
                            MessageBox.Show("用户信息保存成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            setUserList(null, null);
                            txtId.Text = string.Empty;
                            txtName.Text = string.Empty;
                            txtPassword.Text = string.Empty;
                        }
                        else
                        {
                            MessageBox.Show("用户信息保存失败！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("用户：" + txtId.Text + " 已经存在！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
               

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            this.comboBox1.DataSource = layoutBLL.getAllRols("超级管理员");
            this.comboBox1.DisplayMember = "C_JIAOSE";
            this.comboBox1.ValueMember = "C_JIAOSE";
            setUserList(null,null);
        }

        public void setUserList(string id, string name)
        {
            try
            {

                DataTable dt = layoutBLL.getUserList(id,name);
                dgv_Data.DataSource = dt;
                dgv_Data.Columns[0].HeaderText = "用户名";
                dgv_Data.Columns[0].ReadOnly = true;
                dgv_Data.Columns[0].Width = 150;
                dgv_Data.Columns[1].HeaderText = "用户中文名";
                dgv_Data.Columns[1].ReadOnly = true;
                dgv_Data.Columns[1].Width =150;
                dgv_Data.Columns[2].HeaderText = "角色";
                dgv_Data.Columns[2].ReadOnly = true;
                dgv_Data.Columns[2].Width = dgv_Data.Width - 115;

            }
            catch (Exception)
            {
                MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

           
        }

        private void dgv_Data_Resize(object sender, EventArgs e)
        {
          //  MessageBox.Show(dgv_Data.Width.ToString());
            if (dgv_Data.Columns.Count > 0)
            {
                dgv_Data.Columns[1].Width = dgv_Data.Width - 265;
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            queryUserlist();
        }

        private void queryUserlist()
        {
            string id = null;
            string name = null;
            if (this.textBox6.Text != null && !(string.Empty.Equals(this.textBox6.Text.Trim())))
            {
                id = textBox6.Text.Trim();
            }
            if (this.textBox5.Text != null && !(string.Empty.Equals(this.textBox5.Text.Trim())))
            {
                name = textBox5.Text.Trim();
            }
            setUserList(id, name);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("删除之后用户将不能恢复，是否确认删除?", "提示:", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
               
                if (dgv_Data.SelectedRows.Count <= 0)
                {
                    MessageBox.Show("没有要删除的记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                try
                {
                    List<String> lists = new List<string>();

                    for (int i = 0; i < dgv_Data.SelectedRows.Count; i++)
                    {
                        string id = Convert.ToString(dgv_Data.SelectedRows[i].Cells[0].Value);
                        if (!("sa".Equals(id.Trim())))
                        {                            
                            lists.Add(id);
                        }
                        else
                        {
                            lists.Clear();
                            MessageBox.Show("超级管理员账号不能删除!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    if (lists.Count >0)
                    {
                        layoutBLL.delte(lists);
                       queryUserlist();
                    }
                    
                    
                }
                catch (Exception)
                {
                    MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mod();
        }

        private void mod()
        {
            if (dgv_Data.SelectedRows.Count <= 0)
            {
                MessageBox.Show("没有要修改的记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dgv_Data.SelectedRows.Count > 1)
            {
                MessageBox.Show("一次只能修改一个用户信息!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dgv_Data.SelectedRows.Count == 1)
            {
                ModPassword mod = new ModPassword(Convert.ToString(dgv_Data.SelectedRows[0].Cells[0].Value));
                mod.ShowDialog();
                queryUserlist();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.textBox5.Text = string.Empty;
            this.textBox6.Text = string.Empty;
        }

        private void dgv_Data_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                mod();
            }
        }
        
    }
}
