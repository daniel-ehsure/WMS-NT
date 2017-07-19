using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BLL;

namespace UI
{
    public partial class OperateDoListForm : Form
    {
        RuningDoListBLL bll = new RuningDoListBLL();
        int controlType = -1;
        public OperateDoListForm()
        {
            InitializeComponent();
        }

        private void OperateDoListForm_Load(object sender, EventArgs e)
        {
            querryList();
        }


        //关闭
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       

        //删除
        private void button2_Click(object sender, EventArgs e)
        {
            if (dgv_Data.SelectedRows.Count <= 0)
            {
                MessageBox.Show("没有要删除的记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("删除之后信息将不能恢复，是否确认删除?", "提示:", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {


                try
                {
                    List<String> lists = new List<string>();

                    for (int i = 0; i < dgv_Data.SelectedRows.Count; i++)
                    {
                        string id = Convert.ToString(dgv_Data.SelectedRows[i].Cells[0].Value);
                        
                            lists.Add(id);                       
                    }
                    if (lists.Count > 0)
                    {
                        if (bll.deleteDoList(lists))
                        {

                            querryList();
                        }
                        else
                        {
                            MessageBox.Show("信息删除失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }


                }
                catch (Exception)
                {
                    MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //执行
        private void btnShouGong_Click(object sender, EventArgs e)
        {
            if (dgv_Data.SelectedRows.Count <= 0)
            {
                MessageBox.Show("没有要执行的记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                List<String> lists = new List<string>();
                bool flag = false;
                for (int i = 0; i < dgv_Data.SelectedRows.Count&& flag == false; i++)
                {
                    string id = Convert.ToString(dgv_Data.SelectedRows[i].Cells[0].Value);
                    string place = Convert.ToString(dgv_Data.SelectedRows[i].Cells[8].Value);
                    for (int j = 0; j < dgv_Data.Rows.Count; j++)
                    {
                        if (dgv_Data.Rows[j].Selected)
                        {
                            continue;
                        }
                        else
                        {
                            if (place.Equals(Convert.ToString(dgv_Data.Rows[j].Cells[8].Value)))
                            {
                                flag = true;
                                lists.Clear();
                                break;
                            }
                        }

                    }

                    lists.Add(id);
                }
                if (flag)
                {
                    MessageBox.Show("联机任务执行需要按照货位统一执行，请确保选择货位的全部数据都为选择状态!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (lists.Count > 0)
                {
                    if (bll.executeDoList(lists))
                    {

                        querryList();
                    }
                    else
                    {
                        MessageBox.Show("任务执行失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }


            }
            catch (Exception)
            {
                MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //刷新
        private void button3_Click(object sender, EventArgs e)
        {
            querryList();
        }
                
        private void querryList()
        {
            dgv_Data.DataSource = bll.getList(controlType);

            dgv_Data.Columns[0].Visible = false;
            dgv_Data.Columns[1].HeaderText = "单号";           
            dgv_Data.Columns[2].HeaderText = "出入库类型";
            dgv_Data.Columns[3].HeaderText = "物料编号";
            dgv_Data.Columns[4].HeaderText = "物料名称";
            dgv_Data.Columns[5].HeaderText = "数量";
            dgv_Data.Columns[6].HeaderText = "操作人";
            dgv_Data.Columns[7].HeaderText = "货位";
        }
    }
}
