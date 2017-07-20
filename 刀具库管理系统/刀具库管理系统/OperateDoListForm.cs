using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BLL;
using Util;

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
                        string dh = Convert.ToString(dgv_Data.SelectedRows[0].Cells[1].Value);
                        if (bll.deleteDoList(lists, dh))
                        {
                            MessageBox.Show("删除成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Log.saveLog("删除联机任务成功！单号：" + dh + " 序号：" + string.Join(",", lists.ToArray()));
                            querryList();
                        }
                        else
                        {
                            MessageBox.Show("删除失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            querryList();
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
                for (int i = 0; i < dgv_Data.SelectedRows.Count; i++)
                {
                    string id = Convert.ToString(dgv_Data.SelectedRows[i].Cells[0].Value);

                    lists.Add(id);
                }
                if (lists.Count > 0)
                {
                        string dh = Convert.ToString(dgv_Data.SelectedRows[0].Cells[1].Value);
                    if (bll.executeDoList(lists, dh))
                    {
                        MessageBox.Show("执行成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Log.saveLog("执行联机任务成功！单号：" + dh + " 序号：" + string.Join(",", lists.ToArray()));
                        querryList();
                    }
                    else
                    {
                        MessageBox.Show("执行失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
