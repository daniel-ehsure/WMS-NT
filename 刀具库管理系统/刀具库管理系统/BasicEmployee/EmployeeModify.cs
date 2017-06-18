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
    public partial class EmployeeModify : Form
    {
        EmployeeBLL bll = new EmployeeBLL();
        string id = string.Empty;
        T_JB_EMPLOYEE employee = null;
        UnitBLL tbll = new UnitBLL();
        string unitId;

        public EmployeeModify()
        {
            InitializeComponent();
        }
        public EmployeeModify(string id, string unitId)
        {
            InitializeComponent();
            this.id = id;
            this.unitId = unitId;
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
            cmbUnit.SelectedValue = unitId;
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

            employee = bll.getEmployeeById(id);
            if (employee == null)
            {
                MessageBox.Show("获取员工信息失败！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
            else
            {
                this.lblId.Text = employee.C_id;
                this.txtName.Text = employee.C_name;
                this.cmbUnit.SelectedValue = employee.C_unitId;
                this.cmbGW.Text = employee.C_gangWei;
                this.txtMemo.Text = employee.C_memo;
                this.cmbSex.SelectedValue = employee.C_sex;
                this.dtpbrithday.Value = employee.D_birthday;
                this.txtAddress.Text = employee.C_address;
                this.txtOt.Text = employee.C_office_tel;
                this.txtMp.Text = employee.C_move_tel;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkInput())
                {
                    lblName.Visible = false;
                    T_JB_EMPLOYEE temp = new T_JB_EMPLOYEE();
                    temp.C_id = lblId.Text;
                    temp.C_name = txtName.Text.Trim();
                    temp.C_unitId = cmbUnit.SelectedValue.ToString();
                    temp.C_gangWei = cmbGW.Text.Trim().ToString();
                    temp.C_sex = cmbSex.SelectedValue.ToString();
                    temp.C_memo = txtMemo.Text.Trim();
                    temp.C_address = this.txtAddress.Text.Trim();
                    temp.C_office_tel = this.txtOt.Text.Trim();
                    temp.C_move_tel = this.txtMp.Text.Trim();
                    temp.D_birthday = dtpbrithday.Value;

                    if (bll.update(temp))
                    {
                        MessageBox.Show("员工信息更改成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("员工信息更改失败！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
