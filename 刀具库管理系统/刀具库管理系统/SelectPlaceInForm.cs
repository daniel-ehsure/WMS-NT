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
    public partial class SelectPlaceInForm : Form
    {
        PlaceBLL bll = new PlaceBLL();     
        InterfaceSelect parent = null;
        Label lbl = null;
      
        int inOutType = 2;
        int index = 0;
        public SelectPlaceInForm(InterfaceSelect parent)
        {
            InitializeComponent();
            this.parent = parent;          
        }
        public SelectPlaceInForm(InterfaceSelect parent,Label lbl)
        {
            InitializeComponent();
            this.parent = parent;
            this.lbl = lbl;
        }
        private void SelectPlace_Load(object sender, EventArgs e)
        {
            #region combox
            DataTable dt = new WarehouseBLL().GetList(null);
            dt.Columns[0].ColumnName = "id";
            dt.Columns[1].ColumnName = "name";
            DataRow dr = dt.NewRow();
            dr["id"] = "";
            dr["name"] = "所有";
            dt.Rows.InsertAt(dr, 0);

            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";
            comboBox1.SelectedIndex = 0;
            #endregion

            setList(null, null);
        }

        //重置
        private void button5_Click(object sender, EventArgs e)
        {
            this.comboBox1.Text = "所有";
            this.txtName.Text = string.Empty;
        }
        //查询
        private void button4_Click(object sender, EventArgs e)
        {
            querylist();
        }
        //关闭
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //选择按钮
        private void button1_Click(object sender, EventArgs e)
        {
            setParent();
        }
        //双击选择
        private void dgv_Data_DoubleClick(object sender, EventArgs e)
        {
            setParent();
        }
       


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //设置当前选择行
            index = e.RowIndex;
        }


        /// <summary>
        /// 设置货位列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="single"></param>
        /// <param name="standerd"></param>
        public void setList(string wh, string name)
        {
            try
            {
                DataTable dt = bll.GetList(wh, name, null,1, 1);
                dgv_Data.DataSource = dt;
                dgv_Data.Columns[0].HeaderText = "编码";
                dgv_Data.Columns[0].ReadOnly = true;
                dgv_Data.Columns[0].Width = 100;
                dgv_Data.Columns[1].HeaderText = "名称";
                dgv_Data.Columns[1].ReadOnly = true;
                dgv_Data.Columns[1].Width = 250;
                dgv_Data.Columns[2].Visible = false;
                dgv_Data.Columns[3].HeaderText = "是否可用";
                dgv_Data.Columns[3].ReadOnly = true;
                dgv_Data.Columns[3].Width = 150;
                dgv_Data.Columns[4].Visible = false;
                dgv_Data.Columns[5].Visible = false;
            }
            catch (Exception)
            {
                MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }


        }
      

        /// <summary>
        /// 查询货位列表
        /// </summary>
        private void querylist()
        {
            string wh = null;
            string name = null;

            if (this.comboBox1.Text != null && !(string.Empty.Equals(this.comboBox1.Text.Trim())))
            {
                if (!("所有".Equals(comboBox1.Text.Trim())))
                {
                    wh = comboBox1.SelectedValue.ToString();
                }
            }
            if (this.txtName.Text != null && !(string.Empty.Equals(this.txtName.Text.Trim())))
            {
                name = txtName.Text.Trim();
            }

            setList(wh, name);
        }

        private void setParent()
        {
            if (index >= 0 && dgv_Data.SelectedRows.Count > 0)
            {
                string id = this.dgv_Data.Rows[index].Cells[0].Value.ToString();
                string name = this.dgv_Data.Rows[index].Cells[1].Value.ToString();
                int length = Convert.ToInt32(this.dgv_Data.Rows[index].Cells[4].Value);
                int width = Convert.ToInt32(this.dgv_Data.Rows[index].Cells[5].Value);
                if (lbl != null)
                {
                    lbl.Text = Convert.ToString(this.dgv_Data.Rows[index].Cells[6].Value);
                }
                parent.setPlace(name, id, length, width);

                this.Close();
            }
        }

        /// <summary>
        /// 格式化列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_Data_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2)
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
