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
    public partial class OperateOutForm : Form, InterfaceSelect
    {
        OperateInOutBLL bll = new OperateInOutBLL();
        PlaceAreaBLL sbll = new PlaceAreaBLL();
        RuningDoListBLL dbll = new RuningDoListBLL();
        DataTable dt;
        InOutType inOutType = InOutType.MATERIEL_OUT;

        public OperateOutForm()
        {
            InitializeComponent();
        }

        private void OperateInForm_Load(object sender, EventArgs e)
        {
            #region 工位
            DataTable dtstat = sbll.GetList(null);
            DataView dataViewt = dtstat.DefaultView;
            dataViewt.Sort = "C_ID asc";
            DataTable dtt = dataViewt.ToTable();

            DataRow dr = dtt.NewRow();
            dr["c_id"] = string.Empty;
            dr["c_name"] = string.Empty;

            dtt.Rows.InsertAt(dr, 0);

            this.cmbStation.DataSource = dtt;
            this.cmbStation.DisplayMember = "c_name";
            this.cmbStation.ValueMember = "c_id";
            this.cmbStation.SelectedIndex = 0;
            #endregion
            initData();
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
                if (bll.handOut(dt, txtInMeno.Text, inOutType))
                {
                    MessageBox.Show("出库成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        }

        //选择物料
        private void button3_Click(object sender, EventArgs e)
        {
            SelectMaterielOutForm select = new SelectMaterielOutForm(this,dt);
            select.ShowDialog();
            txtCount.Focus();
        }
        //选择货位
        private void button7_Click(object sender, EventArgs e)
        {
            SelectPlaceInForm select = new SelectPlaceInForm(this);
            select.ShowDialog();
        }

        //联机出库
        private void button2_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("没有要入库的记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (cmbStation.SelectedValue == null || string.Empty.Equals(cmbStation.SelectedValue))
                {
                    MessageBox.Show("请选择出库使用的工位!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (dbll.saveDolist(dt, txtInMeno.Text, cmbStation.SelectedValue.ToString(), 1))
                    {
                        MessageBox.Show("保存联机任务成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            }
        }



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
            dr[7] = lblTypeName.Text;
            bool flag = false;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string m = Convert.ToString(dt.Rows[i][0]);
                string p = Convert.ToString(dt.Rows[i][4]);
                if (lblInMateriel.Text.Equals(m) && txtInPlace.Text.Trim().Equals(p))
                {
                    flag = true;
                    int old = Convert.ToInt32(dt.Rows[i][3]);
                    int total = old+ Convert.ToInt32(txtCount.Text.Trim());
                    dt.Rows[i][3] = total.ToString();
                    break;
                }
            }
            if (flag == false)
            {
                dt.Rows.InsertAt(dr, 0);
            }
        }



        private void initData()
        {
           dt  = new DataTable();

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

        private void getName()
        {
            dgv_Data.Columns[0].HeaderText ="物料编码";
            dgv_Data.Columns[1].HeaderText = "物料名称";
            dgv_Data.Columns[2].HeaderText = "规格型号";
            dgv_Data.Columns[3].HeaderText = "数量";
            dgv_Data.Columns[4].HeaderText = "货位";
            dgv_Data.Columns[5].HeaderText = "出库日期";
            dgv_Data.Columns[6].HeaderText = "操作员";
            dgv_Data.Columns[6].Visible = false;
            dgv_Data.Columns[7].Visible = false;
        }

        private bool checkInput()
        {
            bool flag = true;
            if(Convert.ToDateTime(dtpIndate.Value.ToShortDateString())>Convert.ToDateTime(DateTime.Now.ToShortDateString()))
            {
                MessageBox.Show("出库日期不能大于当前日期!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                flag = false;
            }
            if (txtInPlace.Text == null || string.Empty.Equals(txtInPlace.Text))
            {
                flag = false;
                this.lblInPlace.Visible = true;
            }
            else
            {
                this.lblInPlace.Visible = false;                
            }
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
                        if (count > Convert.ToInt32(lblMax.Text))
                        {
                            MessageBox.Show("出库数量大于最大可用数量：" + lblMax.Text + "", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            flag = false;
                            this.lblCount.Visible = true;
                        }
                        else
                        {
                            this.lblCount.Visible = false;
                        }
                    }
                }
                catch (Exception)
                {
                    flag = false;
                    this.lblCount.Visible = true;                    
                }
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
            cmbStation.Enabled = flag;
        }
        private void initMain()
        {           
            txtInMeno.Text = string.Empty;       
        }

        private void initSub()
        {
            txtMaterielName.Text = string.Empty;
            lblInMateriel.Text = string.Empty;
            txtCount.Text = string.Empty;
            txtStand.Text = string.Empty;
            txtInPlace.Text = string.Empty;
            lblMax.Text = string.Empty;
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
            this.lblInMateriel.Text = id;
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
            this.txtMaterielName.Text = mname;
            this.lblInMateriel.Text = mid;
            this.txtStand.Text = standard;
            this.txtInPlace.Text = pid;
            this.txtCount.Text = count.ToString();
            this.lblMax.Text = count.ToString();
            this.lblTypeName.Text = typeName;
        }

        public void setMaterielAndPlace(string mname, string mid, int thick, int single, string standard, int length, int width, string pname, string pid, int plength, int pwidth, int count)
        {
            throw new NotImplementedException();
        }

        #endregion

       
       

       
    }
}
