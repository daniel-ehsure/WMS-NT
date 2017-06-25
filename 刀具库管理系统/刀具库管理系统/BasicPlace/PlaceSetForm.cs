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
    public partial class PlaceSetForm : Form
    {
        PlaceAreaBLL abll = new PlaceAreaBLL();
        PlaceBLL bll = new PlaceBLL();
        bool initFlag = false;
        /// <summary>
        /// 1、全部；2、未分配、3、已分配
        /// </summary>
        int type = 2;
        public PlaceSetForm()
        {
            InitializeComponent();
        }

        private void PlaceSetForm_Load(object sender, EventArgs e)
        {
            #region 设置货区 数据源
            this.dgv_area.DataSource = abll.GetList(null);
            dgv_area.Columns[0].HeaderText = "货区编码";
            dgv_area.Columns[1].HeaderText = "货区名称";
            dgv_area.Columns[2].HeaderText = "备注";
            #endregion

            #region combox
            DataTable dt = new WarehouseBLL().GetList(null);
            dt.Columns[0].ColumnName = "id";
            dt.Columns[1].ColumnName = "name";
            DataRow dr = dt.NewRow();
            dr["id"] = "";
            dr["name"] = "所有";
            dt.Rows.InsertAt(dr, 0);

            cmbJia.DataSource = dt;
            cmbJia.DisplayMember = "name";
            cmbJia.ValueMember = "id";
            cmbJia.SelectedIndex = 0;

            cmbLie.DataSource = initCC();
            cmbLie.DisplayMember = "name";
            cmbLie.ValueMember = "id";
            cmbLie.SelectedIndex = 0;

            cmbCeng.DataSource = initCC();
            cmbCeng.DisplayMember = "name";
            cmbCeng.ValueMember = "id";
            cmbCeng.SelectedIndex = 0;
            #endregion
            initFlag = true;
            setPlaceData();

        }

        //取消货区数数默认选中 （没使用）
        private void dgv_area_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgv_area.CurrentRow != null)
            {
                this.dgv_area.CurrentRow.Selected = false;
            }
        }
        //取消货位数据默认选中
        private void dgv_place_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgv_place.CurrentRow != null)
            {
                this.dgv_place.CurrentRow.Selected = false;
            }
        }


        private DataTable initCC()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", System.Type.GetType("System.String"));
            dt.Columns.Add("name", System.Type.GetType("System.String"));
            DataRow dr = dt.NewRow();
            dr["id"] = "";
            dr["name"] = "所有";
            dt.Rows.Add(dr);

            return dt;

        }

        //设置货位数据源
        private void setPlaceData()
        {
            //string jia =  string.Empty.Equals(cmbJia.SelectedValue)?null:cmbJia.SelectedValue.ToString();
            //string lie = string.Empty.Equals(cmbLie.SelectedValue) ? null : cmbLie.SelectedValue.ToString();
            //if (lie != null&&lie.Length<2)
            //{
            //    lie = "0" + lie;   
            //}
            string jia = string.Empty.Equals(cmbJia.SelectedValue) ? null : cmbJia.SelectedValue.ToString();
            string lie = string.Empty.Equals(cmbLie.SelectedValue) ? null : cmbLie.SelectedValue.ToString();
            string ceng = string.Empty.Equals(cmbCeng.SelectedValue) ? null : cmbCeng.SelectedValue.ToString();
            string preId = null;

            if (string.IsNullOrEmpty(ceng))
            {
                if (string.IsNullOrEmpty(lie))
                {
                    if (string.IsNullOrEmpty(jia))
                    {
                        preId = null;
                    }
                    else
                    {
                        preId = jia;
                    }
                }
                else
                {
                    preId = lie;
                }
            }
            else
            {
                preId = ceng;
            }

            string area = null;
            if (type == 3)
            {
                if (dgv_area.SelectedRows.Count > 0)
                {
                    area = dgv_area.SelectedRows[0].Cells[0].Value.ToString();
                }
                else
                {
                    return;
                }
            }

            dgv_place.DataSource = bll.GetPlaceList(preId, area, type);
            dgv_place.Columns[0].HeaderText = "货位编码";
            dgv_place.Columns[1].HeaderText = "货位名称";
            dgv_place.Columns[2].HeaderText = "是否可用";
            dgv_place.Columns[3].HeaderText = "是否可用";
            dgv_place.Columns[3].Visible = false;
            dgv_place.Columns[4].HeaderText = "长度";
            dgv_place.Columns[5].HeaderText = "高度";
            dgv_place.Columns[6].HeaderText = "货区";

            this.checkBox1.Checked = false;
            this.label8.Text = "合计：" + dgv_place.Rows.Count;

        }

        #region  单选按钮
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked)
                {
                    type = 1;
                    setPlaceData();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButton2.Checked)
                {
                    type = 2;
                    setPlaceData();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButton3.Checked)
                {
                    type = 3;
                    setPlaceData();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #region 选择货区
        private void dgv_area_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                setPlaceData();
            }
            catch (Exception)
            {
                MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 架列层
        private void cmbJia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (initFlag)
            {
                if (string.Empty.Equals(cmbJia.SelectedValue))
                {
                    cmbLie.DataSource = initCC();
                    cmbLie.Enabled = false;
                }
                else
                {
                    DataTable dt = bll.GetByPreId(cmbJia.SelectedValue.ToString());
                    DataRow dr = dt.NewRow();
                    dr["id"] = "";
                    dr["name"] = "所有";
                    dt.Rows.InsertAt(dr, 0);
                    cmbLie.DataSource = dt;
                    cmbLie.Enabled = true;
                }
                cmbLie.DisplayMember = "name";
                cmbLie.ValueMember = "id";
                cmbLie.SelectedIndex = 0;
            }
            //  setPlaceData();
        }

        private void cmbLie_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (initFlag)
            {
                if (string.Empty.Equals(cmbLie.SelectedValue))
                {
                    cmbCeng.DataSource = initCC();
                    cmbCeng.Enabled = false;
                }
                else
                {

                    DataTable dt = bll.GetByPreId(cmbLie.SelectedValue.ToString());
                    DataRow dr = dt.NewRow();
                    dr["id"] = "";
                    dr["name"] = "所有";
                    dt.Rows.InsertAt(dr, 0);
                    cmbCeng.DataSource = dt;
                    cmbCeng.Enabled = true;
                }
                cmbCeng.DisplayMember = "name";
                cmbCeng.ValueMember = "id";
                cmbCeng.SelectedIndex = 0;
            }
            // setPlaceData();
        }

        private void cmbCeng_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (initFlag)
            {
                setPlaceData();
            }
        }
        #endregion

        #region 货位选中
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.label7.Text = "选中：" + dgv_place.SelectedRows.Count;
        }



        private void dataGridView2_MouseMove(object sender, MouseEventArgs e)
        {
            this.label7.Text = "选中：" + dgv_place.SelectedRows.Count;
        }
        #endregion

        #region 全选
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                dgv_place.SelectAll();
                this.label7.Text = "选中：" + dgv_place.SelectedRows.Count;
            }
            else
            {
                CancelSelect();
                //   dataGridView1.CurrentCell = null;              
            }
        }

        private void CancelSelect()
        {
            foreach (DataGridViewRow row in dgv_place.SelectedRows)
            {
                row.Selected = false;
            }
            this.label7.Text = "选中：" + dgv_place.SelectedRows.Count;
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgv_area.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择货区！", "信息!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dgv_place.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择需要设置的货位！", "信息!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            List<string> collection = new List<string>();
            foreach (DataGridViewRow row in dgv_place.SelectedRows)
            {
                collection.Add(row.Cells[0].Value.ToString());
            }
            if (bll.UpdateArea(collection, dgv_area.SelectedRows[0].Cells[0].Value.ToString()))
            {
                MessageBox.Show("货区分配成功！", "信息!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                checkBox1.Checked = false;
                setPlaceData();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
