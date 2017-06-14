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
    public partial class WarehouseList : Form
    {
        WarehouseBLL bll = new WarehouseBLL();

        public WarehouseList()
        {
            InitializeComponent();
        }

        private void ProcedureForm_Load(object sender, EventArgs e)
        {
            setList(null);
            getName();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //保存
        private void button1_Click(object sender, EventArgs e)
        {
            WarehouseAdd wa = new WarehouseAdd();
            wa.ShowDialog();
            querryList();
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            reset();
        }

        //查询
        private void button4_Click(object sender, EventArgs e)
        {
            querryList();
        }

        //删除
        private void button8_Click(object sender, EventArgs e)
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

                        if (bll.IsUse(id)) //被使用
                        {
                            lists.Clear();
                            MessageBox.Show("该信息被使用，不能删除!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                        else
                        {
                            lists.Add(id);
                        }
                    }

                    if (lists.Count > 0)
                    {
                        bll.delete(lists);

                        querryList();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            mod();
        }

        /// <summary>
        /// dataGridView列的名称
        /// </summary>
        private void getName()
        {
            dgv_Data.Columns[0].HeaderText = "编码";
            dgv_Data.Columns[1].HeaderText = "名称";
            dgv_Data.Columns[2].HeaderText = "所属类型";
            dgv_Data.Columns[3].HeaderText = "串口";
            dgv_Data.Columns[4].HeaderText = "波特率";
            dgv_Data.Columns[5].HeaderText = "IP地址";
            dgv_Data.Columns[6].HeaderText = "端口";
            dgv_Data.Columns[7].HeaderText = "写端口";
            dgv_Data.Columns[8].HeaderText = "读端口";
            dgv_Data.Columns[9].HeaderText = "是否自动化仓库";
            dgv_Data.Columns[10].HeaderText = "入库使用移动终端";
            dgv_Data.Columns[11].HeaderText = "出库使用移动终端";
        }

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="name"></param>
        private void setList(string name)
        {
            this.dgv_Data.DataSource = bll.GetList(name);
            getName();
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void querryList()
        {
            string name = null;

            if (this.txtName.Text != null && !(string.Empty.Equals(txtName.Text.Trim())))
            {
                name = txtName.Text;
            }

            setList(name);
        }

        /// <summary>
        /// 重置
        /// </summary>
        private void reset()
        {
            this.txtName.Text = string.Empty;
        }

        /// <summary>
        /// 双击数据行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_Data_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                mod();
            }

        }

        /// <summary>
        /// 修改
        /// </summary>
        private void mod()
        {
            if (dgv_Data.SelectedRows.Count <= 0)
            {
                MessageBox.Show("没有要修改的记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dgv_Data.SelectedRows.Count > 1)
            {
                MessageBox.Show("一次只能修改一个信息!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dgv_Data.SelectedRows.Count == 1)
            {
                WarehouseModify mod = new WarehouseModify(Convert.ToString(dgv_Data.SelectedRows[0].Cells[0].Value));
                mod.ShowDialog();
                querryList();
            }
        }

        /// <summary>
        /// 格式化列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_Data_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 9 || e.ColumnIndex == 10 || e.ColumnIndex == 11)
            {
                e.FormattingApplied = true;
                DataGridViewRow row = dgv_Data.Rows[e.RowIndex];
                if (row != null)
                {
                    if (e.Value.Equals(1))
                    {
                        e.Value = "是";
                    }
                    else
                    {
                        e.Value = "否";
                    }
                }
            }
        }
    }
}
