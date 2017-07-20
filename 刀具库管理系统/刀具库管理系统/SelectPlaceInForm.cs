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
        InOutType inOutType;
        string placeArea;
        DataTable dtPar;

        int index = 0;

        public SelectPlaceInForm(InterfaceSelect parent, InOutType inOutType, string placeArea, DataTable dtPar)
        {
            InitializeComponent();
            this.parent = parent;
            this.inOutType = inOutType;
            this.placeArea = placeArea;
            this.dtPar = dtPar;
        }

        public SelectPlaceInForm(InterfaceSelect parent, Label lbl)
        {
            InitializeComponent();
            this.parent = parent;
            this.lbl = lbl;
        }
        private void SelectPlace_Load(object sender, EventArgs e)
        {
            setList(null, null);
        }

        //重置
        private void button5_Click(object sender, EventArgs e)
        {
            this.txtId.Text = string.Empty;
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
        public void setList(string id, string name)
        {
            try
            {
                DataTable dt = bll.getListByIN(id, name, placeArea, inOutType);

                if (inOutType.Equals(InOutType.KNIFE_IN))
                {//新刀具入库时，要去掉已选中的货位
                    List<int> listToDel = new List<int>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dtPar.Rows.Count; j++)
                        {
                            if (dt.Rows[i][0].Equals(dtPar.Rows[j][4]))
                            {
                                listToDel.Add(i);
                            }
                        }
                    }

                    listToDel.Sort();
                    listToDel.Reverse();

                    listToDel.ForEach(i => dt.Rows.RemoveAt(i));
                }

                dgv_Data.DataSource = dt;
                dgv_Data.Columns[0].HeaderText = "编码";
                dgv_Data.Columns[0].ReadOnly = true;
                dgv_Data.Columns[0].Width = 100;
                dgv_Data.Columns[1].HeaderText = "名称";
                dgv_Data.Columns[1].ReadOnly = true;
                dgv_Data.Columns[1].Width = 250;
                dgv_Data.Columns[2].HeaderText = "是否被占用";
                dgv_Data.Columns[2].ReadOnly = true;
                dgv_Data.Columns[2].Width = 150;
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
            string id = null;
            string name = null;

            if (this.txtId.Text != null && !(string.Empty.Equals(this.txtId.Text.Trim())))
            {
                id = txtId.Text.Trim();
            }
            if (this.txtName.Text != null && !(string.Empty.Equals(this.txtName.Text.Trim())))
            {
                name = txtName.Text.Trim();
            }

            setList(id, name);
        }

        private void setParent()
        {
            if (index >= 0 && dgv_Data.SelectedRows.Count > 0)
            {
                string id = this.dgv_Data.Rows[index].Cells[0].Value.ToString();
                string name = this.dgv_Data.Rows[index].Cells[1].Value.ToString();

                parent.setPlace(name, id, 0, 0);

                this.Close();
            }
        }
    }
}
