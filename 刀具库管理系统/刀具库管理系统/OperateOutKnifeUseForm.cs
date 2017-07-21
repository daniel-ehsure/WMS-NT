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
    public partial class OperateOutKnifeUseForm : Form, InterfaceSelect
    {
        OperateInOutBLL bll = new OperateInOutBLL();
        PlaceAreaBLL sbll = new PlaceAreaBLL();
        PlaceBLL pbll = new PlaceBLL();
        RuningDoListBLL dbll = new RuningDoListBLL();
        MaterielBLL mbll = new MaterielBLL();
        MaterielTypeBLL tbll = new MaterielTypeBLL();
        StocksBLL stockBll = new StocksBLL();
        MachineBLL mabll = new MachineBLL();
        DataTable dtBak;
        DataTable dt;
        InOutType inOutType = InOutType.KNIFE_OUT_USE;
        public T_JB_Materiel materielNow;
        string ids = "001,002,003";
        List<string> listIds = new List<string>();
        List<string> listMachine = new List<string>();

        public OperateOutKnifeUseForm()
        {
            InitializeComponent();
        }

        private void OperateInForm_Load(object sender, EventArgs e)
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

        //手工出库
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

                    listMachine.ForEach(item => mabll.Save(item));

                    string result = bll.handOut(dt, txtInMeno.Text, inOutType);

                    if (!string.IsNullOrEmpty(result))
                    {
                        MessageBox.Show("出库成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Log.saveLog("刀具使用出库成功！单号：" + result);
                        setMain(true);
                        initMain();
                        initSub();
                        dt.Rows.Clear();
                    }
                    else
                    {
                        MessageBox.Show("出库失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //test
        private void button3_Click(object sender, EventArgs e)
        {
            //扫码后执行 测试
            dtBak = dt.Clone();

            string mes = string.Empty;
            //验证码
            if (!CheckBarcode(ids, ref mes))
            {
                MessageBox.Show(mes, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SelectKnifeOutForm select = new SelectKnifeOutForm(ids, dt, dtBak, listIds, listMachine);
            select.ShowDialog();

            if (dtBak.Rows.Count > 0)
            {
                addRowMult(dtBak);
            }
        }

        private bool CheckBarcode(string ids, ref string mes)
        {
            if (ids.Length < 1)
            {
                mes = "码为空！";
                return false;
            }
            else
            {
                try
                {
                    string[] arr = ids.Split(new char[] { ',' });

                    for (int i = 0; i < arr.Length; i++)
                    {
                        T_JB_Materiel mo = mbll.getMaterielById(arr[i]);
                        if (mo == null)
                        {
                            mes = "码无法解析！";
                            return false;
                        }
                    }
                }
                catch
                {
                    mes = "码无法解析！";
                    return false;
                }
            }

            return true;
        }

        //联机出库
        private void button2_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("没有要出库的记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                dr[7] = listMachine[listMachine.Count - 1];

                dt.Rows.InsertAt(dr, 0);
            }
        }

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

        private void getName()
        {
            dgv_Data.Columns[0].HeaderText = "物料编码";
            dgv_Data.Columns[1].HeaderText = "物料名称";
            dgv_Data.Columns[2].HeaderText = "规格型号";
            dgv_Data.Columns[3].HeaderText = "数量";
            dgv_Data.Columns[4].HeaderText = "货位";
            dgv_Data.Columns[5].HeaderText = "出库日期";
            dgv_Data.Columns[6].HeaderText = "操作员";
            dgv_Data.Columns[6].Visible = false;
            dgv_Data.Columns[7].HeaderText = "机床";
        }

        private bool checkInput()
        {
            bool flag = true;
            if (Convert.ToDateTime(dtpIndate.Value.ToShortDateString()) > Convert.ToDateTime(DateTime.Now.ToShortDateString()))
            {
                MessageBox.Show("出库日期不能大于当前日期!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                flag = false;
            }

            if (txtMaterielName.Text == null || string.Empty.Equals(txtMaterielName.Text))
            {
                flag = false;
                this.lblMaterielName.Visible = true;
            }
            else
            {
                this.lblMaterielName.Visible = false;
            }

            return flag;
        }

        private void setMain(bool flag)
        {
            dtpIndate.Enabled = flag;
            txtInMeno.Enabled = flag;
        }
        private void initMain()
        {
            txtInMeno.Text = string.Empty;
        }

        private void initSub()
        {
            txtMaterielName.Text = string.Empty;
            txtId.Text = string.Empty;
            txtStand.Text = string.Empty;
            txtInPlace.Text = string.Empty;
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

        #region InterfaceSelect 成员

        public void setMateriel(string name, string id)
        {
            throw new NotImplementedException();
        }
        public void setMateriel(string name, string id, string standard)
        {
            this.txtMaterielName.Text = name;
            this.txtId.Text = id;
            this.txtStand.Text = standard;
        }
        public void setMateriel(string name, string id, int thick, int single, string standard, int length, int width)
        {
            throw new NotImplementedException();
        }

        public void setPlace(string name, string id, int length, int width)
        {
            this.txtInPlace.Text = id;
        }
        public void setMaterielAndPlace(string mname, string mid, string standard, string pid, string tray, int count, string typeName)
        {
            try
            {
                materielNow = mbll.getMaterielById(mid);
            }
            catch (Exception)
            {
                MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ModelToUI(materielNow);
        }

        public void setMaterielAndPlace(string mname, string mid, int thick, int single, string standard, int length, int width, string pname, string pid, int plength, int pwidth, int count)
        {
            throw new NotImplementedException();
        }

        #endregion

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

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                #region 测试
                //DataTable dtBak = dt.Clone();
                //SelectMaterielOutNumForm select1 = new SelectMaterielOutNumForm(this, dt, dtBak, "001001");
                //select1.ShowDialog();

                //addRowMult(dtBak,mbll.getMaterielById("001001")); 
                #endregion

                T_JB_Materiel mo = Utility.AnalyzeBarcodeMateriel(inOutType);

                if (mo != null)
                {
                    #region 无论多少，都要弹出框
                    //DataTable st = stockBll.getStocksList(null, null, null, null, Global.longid, mo.C_id);

                    //if (st.Rows.Count == 1)
                    //{//只有一个货位有该零件
                    //    //ModelToUI(mo);
                    //    //txtCount.Text = Convert.ToInt32(st.Rows[0][5]).ToString();
                    //    //txtInPlace.Text = st.Rows[0][4].ToString();
                    //}
                    //else
                    //{//多个货位，窗体选择

                    //} 
                    #endregion

                    DataTable dtBak = dt.Clone();
                    SelectMaterielOutNumForm select = new SelectMaterielOutNumForm(this, dt, dtBak, mo.C_id);
                    select.ShowDialog();

                    addRowMult(dtBak);
                }
                else
                {
                    MessageBox.Show("无法解析！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
