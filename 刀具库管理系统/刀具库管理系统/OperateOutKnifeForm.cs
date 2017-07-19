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
    public partial class OperateOutKnifeForm : Form, InterfaceSelect
    {
        OperateInOutBLL bll = new OperateInOutBLL();
        PlaceAreaBLL sbll = new PlaceAreaBLL();
        RuningDoListBLL dbll = new RuningDoListBLL();
        DataTable dt;
        InOutType inOutType = InOutType.MATERIEL_OUT;

        public OperateOutKnifeForm()
        {
            InitializeComponent();
        }

        private void OperateInForm_Load(object sender, EventArgs e)
        {
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
                try
                {
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

        //选择物料
        private void button3_Click(object sender, EventArgs e)
        {
            SelectMaterielOutForm select = new SelectMaterielOutForm(this, dt);
            select.ShowDialog();
            btnOK.Focus();
        }
        //选择货位
        private void button7_Click(object sender, EventArgs e)
        {
            SelectPlaceInForm select = new SelectPlaceInForm(this, inOutType, "");
            select.ShowDialog();
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
                    if (dbll.saveDolist(dt, txtInMeno.Text, 1))
                    {
                        MessageBox.Show("保存联机任务成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Log.saveLog("保存刀具使用出库联机任务成功！");
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
            dr[0] = lblInMateriel.Text;
            dr[1] = txtMaterielName.Text;
            dr[2] = txtStand.Text;
            dr[3] = 1;
            dr[4] = txtInPlace.Text.Trim();
            dr[5] = dtpIndate.Value.ToString("yyyy-MM-dd");
            dr[6] = Global.longid;
            dr[7] = lblTypeName.Text;

            dt.Rows.InsertAt(dr, 0);
        }



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
            dgv_Data.Columns[7].Visible = false;
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
            lblInMateriel.Text = string.Empty;
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
