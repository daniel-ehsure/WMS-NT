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
    public partial class KnifeModify : Form
    {
        MaterielBLL bll = new MaterielBLL();
        string id = string.Empty;
        T_JB_Materiel materiel = null;
        MaterielTypeBLL tbll = new MaterielTypeBLL();
        PlaceAreaBLL abll = new PlaceAreaBLL();
        string mTypeId;

        public KnifeModify()
        {
            InitializeComponent();
        }
        public KnifeModify(string id, string mTypeId)
        {
            InitializeComponent();
            this.id = id;
            this.mTypeId = mTypeId;
        }

        private void ModMateriel_Load(object sender, EventArgs e)
        {
            this.Left = (Global.baseWidth) / 2;
            this.Top = Global.baseHeight / 4;

            #region 初始化 物料类别
            DataTable dt = tbll.GetList(null, null, null, 1);
            DataView dataView = dt.DefaultView;
            dataView.Sort = "C_ID asc";
            cmbType.DataSource = dataView.ToTable();
            cmbType.DisplayMember = "C_NAME";
            cmbType.ValueMember = "C_ID";
            cmbType.SelectedValue = mTypeId;
            #endregion

            #region 初始化 货区
            DataTable dtt = abll.GetList(null);
            DataView dataViewt = dtt.DefaultView;
            dataViewt.Sort = "C_ID asc";
            cmbArea.DataSource = dataViewt.ToTable();
            cmbArea.DisplayMember = "C_NAME";
            cmbArea.ValueMember = "C_ID";
            #endregion

            txtId.ReadOnly = true;
            materiel = bll.getMaterielById(id);
            if (materiel == null)
            {
                MessageBox.Show("获取物料信息失败！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
            else
            {
                this.txtId.Text = materiel.C_id;
                this.txtName.Text = materiel.C_name;
                this.cmbType.SelectedValue = materiel.C_type;
                this.txtStandard.Text = materiel.C_standerd;
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
        }

        //出库数量只能输入数字,小数点,回车和退格
        private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8) && (e.KeyChar != 13) && (e.KeyChar != 46))
            {
                e.Handled = true;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkInput())
                {
                    if (bll.isExit(txtName.Text.Trim(), cmbType.SelectedValue.ToString(), txtId.Text.Trim()))
                    {
                        MessageBox.Show("物料名称已经存在！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblName.Visible = true;
                    }
                    else
                    {
                        lblName.Visible = false;
                        T_JB_Materiel temp = new T_JB_Materiel();
                        temp.C_id = txtId.Text.Trim();
                        temp.C_name = txtName.Text.Trim();
                        temp.C_type = cmbType.SelectedValue.ToString();
                        temp.C_standerd = txtStandard.Text.Trim();
                        temp.I_length = string.Empty.Equals(txtLT.Text.Trim()) ? 0 : Convert.ToDecimal(txtLT.Text.Trim());
                        temp.I_width = string.Empty.Equals(txtWT.Text.Trim()) ? 0 : Convert.ToDecimal(txtWT.Text.Trim());
                        temp.I_thick = string.Empty.Equals(txtMT.Text.Trim()) ? 0 : Convert.ToDecimal(txtMT.Text.Trim());
                        temp.C_area = cmbArea.SelectedValue.ToString();

                        temp.C_memo = txtMeno.Text.Trim();

                        temp.Dec_dimension1 = string.Empty.Equals(txtDim1.Text.Trim()) ? 0 : Convert.ToDecimal(txtDim1.Text.Trim());
                        temp.Dec_dimension2 = string.Empty.Equals(txtDim2.Text.Trim()) ? 0 : Convert.ToDecimal(txtDim2.Text.Trim());
                        temp.Dec_dimension3 = string.Empty.Equals(txtDim3.Text.Trim()) ? 0 : Convert.ToDecimal(txtDim3.Text.Trim());
                        temp.Dec_angle = string.Empty.Equals(txtAngle.Text.Trim()) ? 0 : Convert.ToDecimal(txtAngle.Text.Trim());

                        temp.C_regrinding_length = txtRL.Text.Trim();


                        if (bll.update(temp))
                        {
                            MessageBox.Show("物料信息更改成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("物料信息更改失败！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 保存时验证用户输入是否合法
        /// </summary>
        /// <returns></returns>
        private bool checkInput()
        {
            bool flag = true;
            if (this.txtId.Text == null || string.Empty.Equals(this.txtId.Text.Trim()))
            {
                flag = false;
                label18.Visible = true;
            }
            else
            {
                label18.Visible = false;
            }
            if (this.txtName.Text == null || string.Empty.Equals(this.txtName.Text.Trim()))
            {
                this.lblName.Visible = true;
                flag = false;
            }
            else
            {
                this.lblName.Visible = false;
            }
            if (this.cmbType.SelectedValue == null || string.Empty.Equals(cmbType.SelectedValue.ToString().Trim()))
            {
                this.lblType.Visible = true;
                flag = false;
            }
            else
            {
                this.lblType.Visible = false;
            }
            if (this.cmbArea.SelectedValue == null || string.Empty.Equals(cmbArea.SelectedValue.ToString().Trim()))
            {
                this.lblArea.Visible = true;
                flag = false;
            }
            else
            {
                this.lblArea.Visible = false;
            }


            return flag;
        }

        private void reset()
        {
            txtName.Text = string.Empty;
            lblName.Visible = false;
            txtStandard.Text = string.Empty;
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
