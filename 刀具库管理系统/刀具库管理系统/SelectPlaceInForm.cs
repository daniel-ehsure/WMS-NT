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
            setList(null, null, null);
        }

        //重置
        private void button5_Click(object sender, EventArgs e)
        {
            this.comboBox1.Text = "所有";
            this.textBox1.Text = string.Empty;
            this.textBox2.Text = string.Empty;
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
        public void setList(string jia, string lie, string ceng)
        {
            try
            {
                //todo:why
                //DataTable dt = bll.SelectPlaceList(jia, lie, ceng,inOutType);
                DataTable dt = new DataTable();
                dgv_Data.DataSource = dt;
                dgv_Data.Columns[0].HeaderText = "编码";
                dgv_Data.Columns[0].ReadOnly = true;
                dgv_Data.Columns[0].Width = 100;
                dgv_Data.Columns[1].HeaderText = "名称";
                dgv_Data.Columns[1].ReadOnly = true;
                dgv_Data.Columns[1].Width = 250;

                dgv_Data.Columns[2].HeaderText = "是否可用";
                dgv_Data.Columns[2].ReadOnly = true;
                dgv_Data.Columns[2].Width = 150;
                dgv_Data.Columns[2].Visible = false;

                dgv_Data.Columns[3].HeaderText = "是否可用";
                dgv_Data.Columns[3].ReadOnly = true;
                dgv_Data.Columns[3].Width = 150;

                dgv_Data.Columns[4].HeaderText = "长度(毫米)";
                dgv_Data.Columns[4].ReadOnly = true;
                dgv_Data.Columns[4].Width = 150;
                dgv_Data.Columns[5].HeaderText = "宽度(毫米)";
                dgv_Data.Columns[5].ReadOnly = true;
                dgv_Data.Columns[5].Width = 150;
                dgv_Data.Columns[5].HeaderText = "占用列数";
                dgv_Data.Columns[5].ReadOnly = true;
                dgv_Data.Columns[5].Width = 150;


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
            string jia = null;
            string lie = null;
            string ceng = null;           

            if (this.comboBox1.Text != null && !(string.Empty.Equals(this.comboBox1.Text.Trim())))
            {
                if (!("所有".Equals(comboBox1.Text.Trim())))
                {
                    jia = comboBox1.Text.Trim();
                }
            }
            if (this.textBox2.Text != null && !(string.Empty.Equals(this.textBox2.Text.Trim())))
            {
                lie = textBox2.Text.Trim();
            }
            if (this.textBox1.Text != null && !(string.Empty.Equals(this.textBox1.Text.Trim())))
            {
                ceng = textBox1.Text.Trim();
            }

            setList(jia, lie, ceng);
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

       
       

      
    }
}
