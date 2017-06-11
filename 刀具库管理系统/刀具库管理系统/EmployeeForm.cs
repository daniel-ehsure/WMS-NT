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
    public partial class EmployeeForm : Form
    {
        CommonBLL bll = new CommonBLL();

        private string talbeName = "T_JB_EMPLOYEE";
        public EmployeeForm()
        {
            InitializeComponent();
        }

        private void PlaceAreaForm_Load(object sender, EventArgs e)
        {
            initData();
            setList(null, null);
            getName();
        }
        //关闭
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //保存
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtName.Text == null || string.Empty.Equals(txtName.Text.Trim()))
            {
                lblName.Visible = true;
            }
            else
            {
                //if (bll.isExit(talbeName, txtName.Text.Trim()))
                //{
                //    MessageBox.Show("名称重复！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //else
                //{
                //    lblName.Visible = false;
                //    if (bll.Save(talbeName, txtName.Text.Trim(), txtMeno.Text.Trim()))
                //    {
                //        setList(null, null);
                //        reset();
                //    }
                //    else
                //    {
                //        MessageBox.Show("获取保存失败！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    }
                //}
            }
        }

        //重置
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
                        lists.Add(id);
                    }
                    if (lists.Count > 0)
                    {
                        //bll.delte(talbeName,lists);

                        querryList();
                    }


                }
                catch (Exception)
                {
                    MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        //修改
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
                MessageBox.Show("一次只能修改一个信息!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dgv_Data.SelectedRows.Count == 1)
            {
                //ModPlaceAreaForm mod = new ModPlaceAreaForm(talbeName, Convert.ToString(dgv_Data.SelectedRows[0].Cells[0].Value), "修改员工信息");
                //mod.ShowDialog();
                querryList();
            }
        }




        private void initData()
        {
            DataTable temp = new DataTable();

            for (int i = 0; i < 3; i++)
            {
                DataColumn column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = i.ToString();

                temp.Columns.Add(column);
            }
            this.dgv_Data.DataSource = temp;
            getName();
        }

        /// <summary>
        /// 工装dataGridView　的名称
        /// </summary>
        private void getName()
        {
            dgv_Data.Columns[0].HeaderText = "编码";
            dgv_Data.Columns[1].HeaderText = "名称";
            dgv_Data.Columns[2].HeaderText = "备注";
            for (int i = 0; i < dgv_Data.Columns.Count; i++)
            {
                if (i > 2)
                {
                    dgv_Data.Columns[i].Visible = false;
                }
            }
        }


        private void setList(string name, string meno)
        {
            //this.dgv_Data.DataSource = bll.getList(talbeName, name, meno);
            getName();            
        }
        private void querryList()
        {
            string name = null;
            string meno = null;
            if (this.txtName.Text != null && !(string.Empty.Equals(txtName.Text.Trim())))
            {
                name = txtName.Text;
            }
            if (this.txtMeno.Text != null && !(string.Empty.Equals(txtMeno.Text.Trim())))
            {
                meno = txtMeno.Text;
            }
            setList(name, meno);
        }
        private void reset()
        {
            this.txtName.Text = string.Empty;
            this.lblName.Visible = false;
            this.txtMeno.Text = string.Empty;

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
