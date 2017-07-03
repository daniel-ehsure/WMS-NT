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
    public partial class QuerryOperateOutForm : Form,InterfaceSelect     
    {
        OperateInOutBLL bll = new OperateInOutBLL();
        int isFinished = -1;
        public QuerryOperateOutForm()
        {
            InitializeComponent();
        }

        private void QuerryOperateOutForm_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Value = DateTime.Now.AddDays(-7);
            this.dateTimePicker2.Value = DateTime.Now;
            initData();
            getName();
        }
        //查询
        private void button1_Click(object sender, EventArgs e)
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
                this.dataGridView1.DataSource = bll.getList( beGinTime, endTime,txtInName.Text.Trim(), 1,this.textBox1.Text);
                getName();
                if (this.dataGridView1.RowCount == 0)
                {
                    MessageBox.Show("很遗憾,没有查找到你想要的信息.请核对后重新输入", "注意", MessageBoxButtons.OK);
                    clearDataGridView();
                }
            }
        }
        //选择物料
        private void btnInName_Click(object sender, EventArgs e)
        {
            SelectMaterielForm select = new SelectMaterielForm(this);
            select.ShowDialog();
        }
        //关闭
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //导出
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog toExcel = new SaveFileDialog();
                toExcel.Filter = "Excel文件|*.xls";
                toExcel.Title = "导出文件：";
                if (toExcel.ShowDialog() == DialogResult.OK)
                {
                    string path = toExcel.FileName;
                    Utility.exportExcel3(dataGridView1, path, true);
                    MessageBox.Show("导出成功！", "提示:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
                MessageBox.Show("没有可以导出的数据!");
            }
        }


















        private void initData()
        {
            DataTable temp = new DataTable();
           
            for (int i = 0; i < 11; i++)
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
            dataGridView1.Columns[0].HeaderText = "订单号";
            dataGridView1.Columns[1].HeaderText = "入库类别";
            dataGridView1.Columns[2].HeaderText = "物料编码";
            dataGridView1.Columns[3].HeaderText = "物料名称";
            dataGridView1.Columns[4].HeaderText = "数量";
            dataGridView1.Columns[5].HeaderText = "货位";
            dataGridView1.Columns[6].HeaderText = "入库日期";

           
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
