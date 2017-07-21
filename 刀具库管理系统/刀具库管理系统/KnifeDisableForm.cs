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
    public partial class KnifeDisableForm : Form, InterfaceSelect
    {
        OperateInOutBLL bll = new OperateInOutBLL();

        public KnifeDisableForm()
        {
            InitializeComponent();
        }

        private void KnifeDisalbeForm_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Value = DateTime.Now.AddDays(-7);
            this.dateTimePicker2.Value = DateTime.Now;
            initData();
            getName();

            Query();
        }

        //查询
        private void button1_Click(object sender, EventArgs e)
        {
            Query();
        }

        private void Query()
        {
            DateTime beGinTime = Convert.ToDateTime(this.dateTimePicker1.Value.ToShortDateString());
            DateTime endTime = Convert.ToDateTime(this.dateTimePicker2.Value.ToShortDateString());

            if (beGinTime > endTime)
            {
                MessageBox.Show("开始时间不能大于结束时间", "注意", MessageBoxButtons.OK);
                clearDataGridView();
            }
            else
            {
                //参数有问题
                this.dataGridView1.DataSource = bll.getKDList(beGinTime, endTime, txtInName.Text.Trim(),string.Empty, InOutType.KNIFE_OUT_USE, this.textBox1.Text);
                getName();
                if (this.dataGridView1.RowCount == 0)
                {
                    MessageBox.Show("很遗憾,没有查找到你想要的信息.请核对后重新输入", "注意", MessageBoxButtons.OK);
                    clearDataGridView();
                }
            }
        }

        //关闭
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// 报废
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    if (bll.HasDoList())
                    {
                        MessageBox.Show("存在未完成的联机任务，不能进行报废操作!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    List<string> list = new List<string>();
                    StringBuilder sb = new StringBuilder();

                    for (int j = 0; j < dataGridView1.SelectedRows.Count; j++)
                    {
                        list.Add(dataGridView1.SelectedRows[j].Cells[6].Value.ToString());
                        sb.Append(dataGridView1.SelectedRows[j].Cells[0].Value);
                        sb.Append(",");
                    }

                    if (bll.DisableKnife(list))
                    {
                        MessageBox.Show("刀具报废成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Log.saveLog("刀具报废成功！编码：" + sb.Remove(sb.Length-1,1));
                    }
                    else
                    {
                        MessageBox.Show("刀具报废失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    Query();
                }
                catch (Exception)
                {
                    MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("请选择数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void initData()
        {
            DataTable temp = new DataTable();

            for (int i = 0; i < 7; i++)
            {
                DataColumn column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = i.ToString();

                temp.Columns.Add(column);
            }
            this.dataGridView1.DataSource = temp;
            getName();
        }

        /// <summary>
        /// 工装dataGridView　的名称
        /// </summary>
        private void getName()
        {
            dataGridView1.Columns[0].HeaderText = "单号";
            dataGridView1.Columns[1].HeaderText = "物料编码";
            dataGridView1.Columns[2].HeaderText = "物料名称";
            dataGridView1.Columns[3].HeaderText = "机床";
            dataGridView1.Columns[4].HeaderText = "货位";
            dataGridView1.Columns[5].HeaderText = "出库日期";
            dataGridView1.Columns[6].HeaderText = "编码";
            dataGridView1.Columns[6].Visible = false;
        }

        /// <summary>
        /// 清空 dataGridView
        /// </summary>
        private void clearDataGridView()
        {
            this.dataGridView1.DataSource = "";
        }

        #region InterfaceSelect 成员

        public void setMateriel(string name, string id, int thick, int single, string standard, int length, int width)
        {
            throw new NotImplementedException();
        }
        public void setMateriel(string name, string id)
        {
            throw new NotImplementedException();
        }

        public void setMateriel(string name, string id, string standard)
        {
            this.txtInName.Text = name;
        }

        public void setMaterielAndPlace(string mname, string mid, string standard, string pid, string tray, int count, string typeName)
        {
            throw new NotImplementedException();
        }
        public void setPlace(string name, string id, int length, int width)
        {
            throw new NotImplementedException();
        }

        public void setMaterielAndPlace(string mname, string mid, int thick, int single, string standard, int length, int width, string pname, string pid, int plength, int pwidth, int count)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
