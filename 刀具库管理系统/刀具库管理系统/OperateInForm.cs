using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BLL;
using Model;
using Util;

namespace UI
{
    /// <summary>
    /// 零件入库
    /// </summary>
    public partial class OperateInForm : Form, InterfaceSelect
    {
        OperateInOutBLL bll = new OperateInOutBLL();
        PlaceAreaBLL sbll = new PlaceAreaBLL();
        PlaceBLL pbll = new PlaceBLL();
        RuningDoListBLL dbll = new RuningDoListBLL();
        MaterielBLL mbll = new MaterielBLL();
        MaterielTypeBLL tbll = new MaterielTypeBLL();
        DataTable dt;
        InOutType inOutType = InOutType.MATERIEL_IN;
        public T_JB_Materiel materielNow;
        List<T_JB_Materiel> listMateriel = new List<T_JB_Materiel>();

        public OperateInForm()
        {
            InitializeComponent();
        }

        private void OperateInForm_Load(object sender, EventArgs e)
        {
            initData();

            if (bll.HasDoList())
            {
                MessageBox.Show("存在未完成的联机任务，不能进行出入库操作!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnHand.Enabled = false;
                btnDoList.Enabled = false;
                btnOK.Enabled = false;
            }

            #region 初始化 物料类别
            DataTable dt = tbll.GetList(null, null, null, 1);
            DataView dataView = dt.DefaultView;
            dataView.Sort = "C_ID asc";
            cmbType.DataSource = dataView.ToTable();
            cmbType.DisplayMember = "C_NAME";
            cmbType.ValueMember = "C_ID";
            cmbType.SelectedIndex = -1;
            #endregion

            #region 初始化 货区
            DataTable dtt = sbll.GetList(null);
            DataView dataViewt = dtt.DefaultView;
            dataViewt.Sort = "C_ID asc";
            cmbArea.DataSource = dataViewt.ToTable();
            cmbArea.DisplayMember = "C_NAME";
            cmbArea.ValueMember = "C_ID";
            cmbArea.SelectedIndex = -1;
            #endregion
        }

        //出库数量只能输入数字,小数点,回车和退格
        private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8) && (e.KeyChar != 13))
            {
                e.Handled = true;
            }
        }

        //确认
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (checkInput())
            {
                setMain(false);
                addRow();
                initSub();
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //手工入库
        private void btnShouGong_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("没有要入库的记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                try
                {
                    if (bll.HandIn(dt, txtInMeno.Text, inOutType))
                    {
                        MessageBox.Show("入库成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        setMain(true);
                        initMain();
                        initSub();
                        dt.Rows.Clear();
                    }
                    else
                    {
                        MessageBox.Show("入库失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //选择物料
        private void button3_Click(object sender, EventArgs e)
        {
            SelectMaterielForm select = new SelectMaterielForm(this);
            select.ShowDialog();
            txtCount.Focus();
        }

        //选择货位
        private void button7_Click(object sender, EventArgs e)
        {
            if (txtId.Text.Trim().Length > 0)
            {
                SelectPlaceInForm select = new SelectPlaceInForm(this, inOutType, materielNow.C_area);
                select.ShowDialog();
            }
            else
            {
                MessageBox.Show("请先选择零件!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //联机入库
        private void button2_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("没有要入库的记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                try
                {
                    if (dbll.SaveDolist(dt, txtInMeno.Text, inOutType))
                    {
                        MessageBox.Show("保存联机任务成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                        setMain(true);
                        initMain();
                        initSub();
                        dt.Rows.Clear();
                    }
                    else
                    {
                        MessageBox.Show("保存联机任务失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 增加数据行
        /// </summary>
        private void addRow()
        {
            DataRow dr = dt.NewRow();
            dr[0] = lblInMateriel.Text;
            dr[1] = txtMaterielName.Text;
            dr[2] = txtStand.Text;
            dr[3] = txtCount.Text.Trim();
            dr[4] = txtInPlace.Text.Trim();
            dr[5] = dtpIndate.Value.ToString("yyyy-MM-dd");
            dr[6] = Global.longid;
            //dr[7] = lblTypeName.Text;
            bool flag = false;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string m = Convert.ToString(dt.Rows[i][0]);
                string p = Convert.ToString(dt.Rows[i][7]);
                if (lblInMateriel.Text.Equals(m) && lblTypeName.Text.Equals(p))
                {
                    flag = true;
                    int old = Convert.ToInt32(dt.Rows[i][3]);
                    int total = old + Convert.ToInt32(txtCount.Text.Trim());
                    dt.Rows[i][3] = total.ToString();
                    break;
                }
            }
            if (flag == false)
            {
                dt.Rows.InsertAt(dr, 0);
            }
        }


        /// <summary>
        /// 初始化数据
        /// </summary>
        private void initData()
        {
            dt = new DataTable();

            for (int i = 0; i < 8; i++)
            {
                DataColumn column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = i.ToString();

                dt.Columns.Add(column);
            }



            this.dgv_Data.DataSource = dt;
            getName();
        }

        /// <summary>
        /// 设置dg列名
        /// </summary>
        private void getName()
        {
            dgv_Data.Columns[0].HeaderText = "物料编码";
            dgv_Data.Columns[1].HeaderText = "物料名称";
            dgv_Data.Columns[2].HeaderText = "规格型号";
            dgv_Data.Columns[3].HeaderText = "数量";
            dgv_Data.Columns[4].HeaderText = "货位";
            dgv_Data.Columns[5].HeaderText = "入库日期";
            dgv_Data.Columns[6].HeaderText = "操作员";
            dgv_Data.Columns[6].Visible = false;
            dgv_Data.Columns[7].Visible = false;
        }

        /// <summary>
        /// 验证输入
        /// </summary>
        /// <returns></returns>
        private bool checkInput()
        {
            bool flag = true;
            if (Convert.ToDateTime(dtpIndate.Value.ToShortDateString()) > Convert.ToDateTime(DateTime.Now.ToShortDateString()))
            {
                MessageBox.Show("入库日期不能大于当前日期!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                flag = false;
            }

            if (flag)
            {
                if (txtInPlace.Text == null || string.Empty.Equals(txtInPlace.Text))
                {
                    flag = false;
                    this.lblPlace.Visible = true;
                }
            }

            if (flag)
            {
                if (txtCount.Text == null || string.Empty.Equals(txtCount.Text))
                {
                    flag = false;
                    this.lblCount.Visible = true;
                }
                else
                {
                    this.lblCount.Visible = false;
                    try
                    {
                        int count = Convert.ToInt32(txtCount.Text.Trim());
                        if (count <= 0)
                        {
                            flag = false;
                            this.lblCount.Visible = true;
                        }
                        else
                        {
                            this.lblCount.Visible = false;
                        }
                    }
                    catch (Exception)
                    {
                        flag = false;
                        this.lblCount.Visible = true;
                    }
                }
            }

            if (flag)
            {
                if (txtMaterielName.Text == null || string.Empty.Equals(txtMaterielName.Text))
                {
                    flag = false;
                    this.lblMaterielName.Visible = true;
                }
                else
                {
                    this.lblMaterielName.Visible = false;

                    try
                    {
                        T_JB_Materiel materiel = mbll.getMaterielById(txtId.Text);
                        if (materiel == null)
                        {//当前零件不存在，增加
                            mbll.save(materielNow, Global.longid);
                            this.lblMaterielName.Visible = true;
                            flag = false;
                        }
                        else
                        {//存在，更新
                            this.lblMaterielName.Visible = false;

                            //mbll.update(materielNow);

                            //txtMaterielName.Text = materiel.C_name;
                            //lblInMateriel.Text = materiel.C_id;
                            //txtStand.Text = materiel.C_standerd;
                            //lblTypeName.Text = materiel.C_typeName;
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            return flag;
        }

        private void setMain(bool flag)
        {
            dtpIndate.Enabled = flag;
            //txtInPlace.Enabled = flag;
            //button7.Enabled = flag;
            txtInMeno.Enabled = flag;
        }
        private void initMain()
        {
            txtInPlace.Text = string.Empty;
            txtInMeno.Text = string.Empty;
        }

        private void initSub()
        {
            lblInMateriel.Text = string.Empty;
            txtCount.Text = string.Empty;
            txtStand.Text = string.Empty;

            txtMaterielName.Text = string.Empty;
            txtId.Text = string.Empty;
            txtStand.Text = string.Empty;
            txtThick.Text = string.Empty;
            txtLength.Text = string.Empty;
            txtWidth.Text = string.Empty;
            txtMeno.Text = string.Empty;
            txtId.Text = string.Empty;
            this.textBox1.Text = string.Empty;
            this.textBox2.Text = string.Empty;
            this.textBox3.Text = string.Empty;
            this.textBox4.Text = string.Empty;
            this.textBox5.Text = string.Empty;
            this.textBox7.Text = string.Empty;
            this.textBox6.Text = string.Empty;
            this.checkBox2.Checked = false;
            this.checkBox1.Checked = false;
            cmbType.SelectedIndex = -1;
            cmbArea.SelectedIndex = -1;
        }

        private void dgv_Data_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string materiel = dt.Rows[e.RowIndex][0].ToString();
                int i = 0;
                for (; i < dt.Rows.Count; i++)
                {
                    if (materiel.Equals(dt.Rows[i][0]))
                    {
                        break;
                    }
                }
                if (i < dt.Rows.Count)
                {
                    dt.Rows.Remove(dt.Rows[i]);
                }
                if (dt.Rows.Count <= 0)
                {
                    setMain(true);
                }
            }
        }

        /// <summary>
        /// 显示model
        /// </summary>
        /// <param name="materiel"></param>
        void ModelToUI(T_JB_Materiel materiel)
        {
            this.txtId.Text = materiel.C_id;
            this.txtMaterielName.Text = materiel.C_name;
            this.cmbType.SelectedValue = materiel.C_type;
            this.txtStand.Text = materiel.C_standerd;
            this.txtThick.Text = materiel.I_thick.ToString();
            this.txtLength.Text = materiel.I_length.ToString();
            this.txtWidth.Text = materiel.I_width.ToString();
            this.cmbArea.SelectedValue = materiel.C_area;
            if (materiel.I_finish == 1)
            {
                this.checkBox1.Checked = true;
            }
            else
            {
                this.checkBox1.Checked = false;
            }
            this.txtMeno.Text = materiel.C_memo;
            this.textBox1.Text = materiel.C_piccode;
            this.textBox2.Text = materiel.I_layOutCount.ToString();
            this.textBox3.Text = materiel.C_surface;
            this.textBox4.Text = materiel.C_Science;
            this.textBox5.Text = materiel.Dec_area.ToString();
            this.textBox7.Text = materiel.Dec_weight.ToString();
            this.textBox6.Text = materiel.Dec_production.ToString();
            if (materiel.I_buy == 1)
            {
                this.checkBox2.Checked = true;
            }
            else
            {
                this.checkBox2.Checked = false;
            }
        }




        #region InterfaceSelect 成员

        public void setMateriel(string name, string id)
        {
            throw new NotImplementedException();
        }
        public void setMateriel(string name, string id, string standard)
        {
            materielNow = mbll.getMaterielById(id);
            ModelToUI(materielNow);
        }

        public void setMaterielAndPlace(string mname, string mid, string standard, string pid, string tray, int count, string typeName)
        {
            throw new NotImplementedException();
        }
        public void setMateriel(string name, string id, int thick, int single, string standard, int length, int width)
        {
            throw new NotImplementedException();
        }

        public void setPlace(string name, string id, int length, int width)
        {
            this.txtInPlace.Text = id;
        }

        public void setMaterielAndPlace(string mname, string mid, int thick, int single, string standard, int length, int width, string pname, string pid, int plength, int pwidth, int count)
        {
            throw new NotImplementedException();
        }

        #endregion

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                T_JB_Materiel mo = Utility.AnalyzeBarcodeMateriel(inOutType);
                ModelToUI(mo);

                if (txtInPlace.Text.Trim().Length > 0)
                {//无货位自动分配
                    string place = pbll.GetAutoPlace(mo.C_area);
                }
            }
        }
    }
}
