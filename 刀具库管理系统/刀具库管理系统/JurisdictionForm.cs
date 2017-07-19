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
    public partial class JurisdictionForm : Form
    {
        LayoutBLL bll = new LayoutBLL();
        bool hasset = false;
        public JurisdictionForm()
        {
            InitializeComponent();
        }

        private void JurisdictionForm_Load(object sender, EventArgs e)
        {
            this.comboBox1.DataSource = bll.getUserList();
            this.comboBox1.DisplayMember = "C_LOGID";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                checkBox1.Checked = false;
                dataGridView1.DataSource = bll.getAllInformations(comboBox1.Text);
                if (hasset == false)
                {
                    dataGridView1.Columns[0].HeaderText = "角色";
                    dataGridView1.Columns[0].ReadOnly = true;
                    dataGridView1.Columns[1].HeaderText = "是否可见";
                    dataGridView1.Columns[1].Width = 80;
                    dataGridView1.Columns[1].ReadOnly = false;
                    dataGridView1.Columns[2].HeaderText = "项目名";
                    dataGridView1.Columns[2].Width = 150;
                    dataGridView1.Columns[2].ReadOnly = true;
                    dataGridView1.Columns[3].HeaderText = "组名";
                    dataGridView1.Columns[3].Width = 200;
                    dataGridView1.Columns[3].ReadOnly = true;
                    dataGridView1.Columns[4].HeaderText = "图片文件名";
                    dataGridView1.Columns[4].Width = 150;
                    dataGridView1.Columns[4].ReadOnly = true;
                    dataGridView1.Columns[5].Visible = false;
                    dataGridView1.Columns[6].Visible = false;
                    dataGridView1.Columns[7].Visible = false;
                    hasset = true;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //关闭
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //全选
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[1].Value = 1;
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[1].Value = 0;
                }
            }
        }

        //保存
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string jiaose = comboBox1.Text;
                if (jiaose == "sa")
                {
                    MessageBox.Show("超级管理员权限不能被修改！", "信息!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                List<Outlook_Table_Jb_User> users = new List<Outlook_Table_Jb_User>();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    Outlook_Table_Jb_User user = new Outlook_Table_Jb_User();
                    user.Groupname = row.Cells[3].Value.ToString();
                    user.Itemname = row.Cells[2].Value.ToString();
                    user.Jiase = row.Cells[0].Value.ToString();
                    user.Ob_name = row.Cells[7].Value.ToString();
                    user.Objecname = row.Cells[6].Value.ToString();
                    user.Picname = row.Cells[4].Value.ToString();
                    user.Trig_id = Convert.ToDecimal(row.Cells[5].Value);
                    
                    user.Yesno = row.Cells[1].Value.ToString();
                    users.Add(user);
                    //     MessageBox.Show(row.Cells[1].Value.ToString());
                }

                if (bll.save(jiaose, users))
                {
                    MessageBox.Show("数据保存成功！", "信息!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Log.saveLog(string.Format("设置用户{0}权限成功！", jiaose));
                }
                else
                {
                    MessageBox.Show("数据保存失败！", "信息!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[0].Cells[1].Selected == true) 
            {
                dataGridView1.Rows[0].Cells[1].Selected = false;
            }
        }

    }
}
