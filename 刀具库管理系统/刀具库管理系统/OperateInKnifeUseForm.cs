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
    public partial class OperateInKnifeUseForm : Form, InterfaceSelect
    {
        OperateInOutBLL bll = new OperateInOutBLL();
        PlaceAreaBLL sbll = new PlaceAreaBLL();
        RuningDoListBLL dbll = new RuningDoListBLL();
        MaterielBLL mbll = new MaterielBLL();
        PlaceBLL pbll = new PlaceBLL();
        MaterielTypeBLL tbll = new MaterielTypeBLL();
        DataTable dt;
        DataTable dtBak;
        InOutType inOutType = InOutType.KNIFE_IN_USE;
        public T_JB_Materiel materielNow;
        string materielType = "0001";

        public OperateInKnifeUseForm()
        {
            InitializeComponent();
        }

        private void OperateInKnifeUseForm_Load(object sender, EventArgs e)
        {
            initData();

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

        //确认
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (checkInput())
            {
                setMain(false);
                addRowMult(dtBak);
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
                    if (bll.HasDoList())
                    {
                        MessageBox.Show("存在未完成的联机任务，不能进行出入库操作!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string result = bll.HandIn(dt, txtInMeno.Text, inOutType);

                    if (!string.IsNullOrEmpty(result))
                    {
                        MessageBox.Show("入库成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Log.saveLog("刀具使用入库成功！单号：" + result);
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
            //测试
            T_JB_Materiel mo = mbll.getMaterielById("001");
            SelectKnifeUse(mo);
        }

        /// <summary>
        /// 选择刀具
        /// </summary>
        /// <param name="mo"></param>
        private void SelectKnifeUse(T_JB_Materiel mo)
        {
            dtBak = dt.Clone();
            SelectKnifeUseForm select = new SelectKnifeUseForm(mo.C_id, dt, dtBak);
            select.ShowDialog();

            if (dtBak.Rows.Count > 0)
            {
                //addRowMult(dtBak);
                ModelToUI(mo);

                txtInPlace.Text = dtBak.Rows[0][4].ToString();
                txtMachine.Text = dtBak.Rows[0][7].ToString();
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
                    if (bll.HasDoList())
                    {
                        MessageBox.Show("存在未完成的联机任务，不能进行出入库操作!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string res = dbll.SaveDolist(dt, txtInMeno.Text, inOutType);

                    if (!string.IsNullOrEmpty(res))
                    {
                        MessageBox.Show("保存联机任务成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Log.saveLog("保存刀具使用出库联机任务成功！单号：" + res);
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
            dr[0] = txtId.Text;
            dr[1] = txtMaterielName.Text;
            dr[2] = txtStand.Text;
            dr[3] = 1;
            dr[4] = txtInPlace.Text.Trim();
            dr[5] = dtpIndate.Value.ToString("yyyy-MM-dd");
            dr[6] = Global.longid;
            dr[7] = lblTypeName.Text;

            dt.Rows.InsertAt(dr, 0);
        }

        private void addRowMult(DataTable dtBak)
        {
            for (int j = 0; j < dtBak.Rows.Count; j++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = dtBak.Rows[j][0];
                dr[1] = dtBak.Rows[j][1];
                dr[2] = dtBak.Rows[j][2];
                dr[3] = 1;
                dr[4] = dtBak.Rows[j][4];
                dr[5] = dtpIndate.Value.ToString("yyyy-MM-dd");
                dr[6] = dtBak.Rows[j][6];
                dr[7] = dtBak.Rows[j][7];
                dr[8] = dtBak.Rows[j][8];

                dt.Rows.InsertAt(dr, 0);
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void initData()
        {
            dt = new DataTable();

            for (int i = 0; i < 9; i++)
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
            dgv_Data.Columns[7].HeaderText = "机床";
            dgv_Data.Columns[8].HeaderText = "编码";
            dgv_Data.Columns[8].Visible = false;
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
            txtInPlace.Text = string.Empty;

            txtMaterielName.Text = string.Empty;
            txtId.Text = string.Empty;
            txtStand.Text = string.Empty;

            txtMT.Text = string.Empty;
            txtLT.Text = string.Empty;
            txtWT.Text = string.Empty;
            txtMeno.Text = string.Empty;
            txtId.Text = string.Empty;
            this.txtDim1.Text = string.Empty;
            this.txtDim2.Text = string.Empty;
            this.txtDim3.Text = string.Empty;
            this.txtAngle.Text = string.Empty;
            this.txtRL.Text = string.Empty;

            txtMachine.Text = string.Empty;
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
            this.txtMT.Text = materiel.I_thick.ToString();
            this.txtLT.Text = materiel.I_length.ToString();
            this.txtWT.Text = materiel.I_width.ToString();
            this.cmbArea.SelectedValue = materiel.C_area;

            this.txtMeno.Text = materiel.C_memo;
            this.txtDim1.Text = materiel.Dec_dimension1.ToString();
            this.txtDim2.Text = materiel.Dec_dimension2.ToString();
            this.txtDim3.Text = materiel.Dec_dimension3.ToString();
            this.txtAngle.Text = materiel.Dec_angle.ToString();
            this.txtRL.Text = materiel.C_regrinding_length;
        }




        #region InterfaceSelect 成员

        public void setMateriel(string name, string id)
        {
            throw new NotImplementedException();
        }
        public void setMateriel(string name, string id, string standard)
        {
            try
            {
                materielNow = mbll.getMaterielById(id);
                ModelToUI(materielNow);
            }
            catch (Exception)
            {
                MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            //扫码
            if (e.KeyChar == 13)
            {
                T_JB_Materiel mo = Utility.AnalyzeBarcodeMateriel(inOutType);

                if (mo != null)
                {
                    SelectKnifeUse(mo);
                }
                else
                {
                    MessageBox.Show("无法解析！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
        }
    }
}
