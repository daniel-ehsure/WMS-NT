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
    public partial class EmployeeAdd : Form
    {
        EmployeeBLL bll = new EmployeeBLL();
        string id = string.Empty;
        T_JB_EMPLOYEE Employee = null;
        UnitBLL tbll = new UnitBLL();
        string mTypeId;

        public EmployeeAdd()
        {
            InitializeComponent();
        }

        public EmployeeAdd(string id, string mTypeId)
        {
            InitializeComponent();
            this.id = id;
            this.mTypeId = mTypeId;
        }

        private void ModEmployee_Load(object sender, EventArgs e)
        {
            this.Left = (Global.baseWidth) / 2;
            this.Top = Global.baseHeight / 4;

            #region 初始化 部门
            DataTable dt = tbll.GetList(null, null, null);
            DataView dataView = dt.DefaultView;
            dataView.Sort = "C_ID asc";
            cmbUnit.DataSource = dataView.ToTable();
            cmbUnit.DisplayMember = "C_NAME";
            cmbUnit.ValueMember = "C_ID";
            cmbUnit.SelectedValue = mTypeId;
            #endregion

            #region 初始化 岗位
            DataTable dtt = bll.GetGWList();
            DataView dataViewt = dtt.DefaultView;
            cmbGW.DataSource = dataViewt.ToTable();
            cmbGW.DisplayMember = "C_NAME";
            cmbGW.ValueMember = "C_ID";
            #endregion

            //初始化 男女
            DataTable dtSex = new DataTable();
            dtSex.Columns.Add("C_ID");
            dtSex.Columns.Add("C_NAME");
            DataRow dr = dtSex.NewRow();
            dr["C_ID"] = "男";
            dr["C_NAME"] = "男";
            dtSex.Rows.InsertAt(dr, 0);
            DataRow dr1 = dtSex.NewRow();
            dr1["C_ID"] = "女";
            dr1["C_NAME"] = "女";
            dtSex.Rows.InsertAt(dr1, 1);
            cmbSex.DataSource = dtSex;
            cmbSex.DisplayMember = "C_NAME";
            cmbSex.ValueMember = "C_ID";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkInput())
                {
                    #region 增加

                    lblName.Visible = false;
                    T_JB_EMPLOYEE temp = new T_JB_EMPLOYEE();
                    temp.C_name = txtName.Text.Trim();
                    temp.C_unitId = cmbUnit.SelectedValue.ToString();
                    temp.C_gangWei = cmbGW.Text.Trim().ToString();
                    temp.C_sex = cmbSex.SelectedValue.ToString();
                    temp.C_memo = txtMemo.Text.Trim();
                    temp.C_address = this.txtAddress.Text.Trim();
                    temp.C_office_tel = this.txtOt.Text.Trim();
                    temp.C_move_tel = this.txtMp.Text.Trim();
                    temp.D_birthday = dtpbrithday.Value;

                    if (bll.save(temp, Global.longid))
                    {
                        MessageBox.Show("员工保存成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Log.saveLog("添加员工成功！姓名：" + temp.C_name);
                        reset();
                    }
                    else
                    {
                        MessageBox.Show("员工保存失败！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            if (this.txtName.Text == null || string.Empty.Equals(this.txtName.Text.Trim()))
            {
                this.lblName.Visible = true;
                flag = false;
            }
            else
            {
                this.lblName.Visible = false;
            }
            if (this.cmbUnit.SelectedIndex < 0)
            {
                this.lblUnit.Visible = true;
                flag = false;
            }
            else
            {
                this.lblUnit.Visible = false;
            }
            if (this.cmbGW.Text == null || string.Empty.Equals(this.cmbGW.Text.Trim()))
            {
                this.lblGW.Visible = true;
                flag = false;
            }
            else
            {
                this.lblGW.Visible = false;
            }
            return flag;
        }

        private void reset()
        {
            txtName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtOt.Text = string.Empty;
            txtMp.Text = string.Empty;
            txtMemo.Text = string.Empty;
            this.txtMp.Text = string.Empty;
            this.txtAddress.Text = string.Empty;
            this.txtOt.Text = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
