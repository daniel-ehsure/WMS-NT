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
    public partial class JurisdictionMaterielForm : Form
    {
        LayoutBLL bll = new LayoutBLL();
        MaterielBLL mbll = new MaterielBLL();
        bool hasset = false;

        public JurisdictionMaterielForm()
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
                dataGridView1.DataSource = mbll.getAllInformations(comboBox1.Text);
                if (hasset == false)
                {
                    dataGridView1.Columns[0].HeaderText = "角色";
                    dataGridView1.Columns[0].ReadOnly = true;

                    dataGridView1.Columns[1].HeaderText = "是否可见";
                    dataGridView1.Columns[1].ReadOnly = false;

                    dataGridView1.Columns[2].HeaderText = "编号";
                    dataGridView1.Columns[2].ReadOnly = true;

                    dataGridView1.Columns[3].HeaderText = "名称";
                    dataGridView1.Columns[3].ReadOnly = true;

                    dataGridView1.Columns[4].HeaderText = "类型编号";
                    dataGridView1.Columns[4].ReadOnly = true;

                    dataGridView1.Columns[5].HeaderText = "类型名称";
                    dataGridView1.Columns[5].ReadOnly = true;

                    dataGridView1.Columns[6].HeaderText = "规格型号";
                    dataGridView1.Columns[6].ReadOnly = true;

                    dataGridView1.Columns[7].HeaderText = "货区编号";
                    dataGridView1.Columns[7].ReadOnly = true;

                    dataGridView1.Columns[8].HeaderText = "货区";
                    dataGridView1.Columns[8].ReadOnly = true;

                    dataGridView1.Columns[9].HeaderText = "备注";
                    dataGridView1.Columns[9].ReadOnly = true;

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

                List<string> isyes = new List<string>();
                List<string> isno = new List<string>();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    int temp = Convert.ToInt32(row.Cells[1].Value);
                    if (temp == 1)
                    {
                        isyes.Add(row.Cells[2].Value.ToString());
                    }
                    else
                    {
                        isno.Add(row.Cells[2].Value.ToString());
                    }

                }

                if (mbll.setJurisdiction(isyes, isno, jiaose))            
                {
                    MessageBox.Show("数据保存成功！", "信息!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
