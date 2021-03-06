﻿using System;
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
    public partial class MaterielAdd : Form
    {
        MaterielBLL bll = new MaterielBLL();
        string id = string.Empty;
        T_JB_Materiel materiel = null;
        MaterielTypeBLL tbll = new MaterielTypeBLL();
        PlaceAreaBLL abll = new PlaceAreaBLL();
        string mTypeId;

        public MaterielAdd()
        {
            InitializeComponent();
        }
        public MaterielAdd(string id, string mTypeId)
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
            cmbArea.SelectedValue = "0002";
            #endregion
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
                    #region 增加
                    if (bll.isExit(txtName.Text.Trim(), cmbType.SelectedValue.ToString()))
                    {
                        MessageBox.Show("物料名称重复！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblName.Visible = true;
                    }
                    else
                    {
                        lblName.Visible = false;
                        T_JB_Materiel temp = new T_JB_Materiel();
                        temp.C_id = txtId.Text.Trim();
                        temp.C_name = txtName.Text.Trim();
                        temp.C_type = cmbType.SelectedValue.ToString();
                        temp.I_single = 0;
                        temp.C_standerd = txtStandard.Text.Trim();
                        temp.I_length = string.Empty.Equals(txtLength.Text.Trim()) ? 0 : Convert.ToDecimal(txtLength.Text.Trim());
                        temp.I_width = string.Empty.Equals(txtWidth.Text.Trim()) ? 0 : Convert.ToDecimal(txtWidth.Text.Trim());
                        temp.I_thick = string.Empty.Equals(txtThick.Text.Trim()) ? 0 : Convert.ToDecimal(txtThick.Text.Trim());
                        temp.C_area = cmbArea.SelectedValue.ToString();
                        temp.I_finish = 0;

                        if (this.checkBox1.Checked)
                        {
                            temp.I_finish = 1;
                        }
                        temp.C_memo = txtMeno.Text.Trim();


                        temp.C_piccode = this.textBox1.Text.Trim();
                        temp.I_layOutCount = string.Empty.Equals(textBox2.Text.Trim()) ? 0 : Convert.ToInt32(textBox2.Text.Trim());
                        temp.C_surface = this.textBox3.Text.Trim();
                        temp.C_Science = this.textBox4.Text.Trim();
                        temp.Dec_area = string.Empty.Equals(textBox5.Text.Trim()) ? 0 : Convert.ToDecimal(textBox5.Text.Trim());
                        temp.Dec_weight = string.Empty.Equals(textBox7.Text.Trim()) ? 0 : Convert.ToDecimal(textBox7.Text.Trim());
                        temp.I_buy = 0;
                        if (this.checkBox2.Checked)
                        {
                            temp.I_buy = 1;
                        }

                        temp.Dec_production = string.Empty.Equals(textBox6.Text.Trim()) ? 0 : Convert.ToDecimal(textBox6.Text.Trim());



                        if (bll.save(temp, Global.longid))
                        {
                            MessageBox.Show("物料保存成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Log.saveLog("添加物料成功！Id：" + txtId.Text);
                            reset();
                        }
                        else
                        {
                            MessageBox.Show("物料保存失败！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    #endregion
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
