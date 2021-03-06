﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BLL;
using Util;
using Model;

namespace UI
{
    public partial class WarehouseModify : Form
    {
        WarehouseBLL bll = new WarehouseBLL();
        WHTypeBLL bllType = new WHTypeBLL();
        string id;

        public WarehouseModify(string id)
        {
            InitializeComponent();

            this.id = id;

            #region 初始化 类型
            DataTable dt = bllType.GetList(null);
            DataView dataView = dt.DefaultView;
            dataView.Sort = "C_ID asc";
            cmbType.DataSource = dataView.ToTable();
            cmbType.DisplayMember = "C_NAME";
            cmbType.ValueMember = "C_ID";
            #endregion

            Init();

        }

        private void Init()
        {
            try
            {
                T_JB_WAREHOUSE mo = bll.GetById(id);

                if (mo == null)
                {
                    MessageBox.Show("获取信息失败！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
                else
                {
                    lblId.Text = mo.C_ID;
                    txtName.Text = mo.C_NAME;
                    txtCom.Text = mo.C_COM;
                    txtBaudrate.Text = mo.C_BAUDRATE;
                    txtPort.Text = mo.C_PORT;
                    txtWritePort.Text = mo.C_WRITE_PORT;
                    txtReadPort.Text = mo.C_READ_PORT;
                    txtIpAddress.Text = mo.C_IP_ADDRESS;
                    cmbType.SelectedValue = mo.C_TYPE;
                    cbAuto.Checked = mo.I_AUTO == 0 ? false : true;
                    cbIn.Checked = mo.I_IN_MOBILE == 0 ? false : true;
                    cbOut.Checked = mo.I_OUT_MOBILE == 0 ? false : true;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (bll.IsExitNotSelf(id, txtName.Text.Trim()))
            {
                MessageBox.Show("名称重复！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    T_JB_WAREHOUSE mo = new T_JB_WAREHOUSE();

                    mo.C_ID = id;
                    mo.C_COM = txtCom.Text.Trim();
                    mo.C_NAME = txtName.Text.Trim();
                    mo.C_BAUDRATE = txtBaudrate.Text.Trim();
                    mo.C_PORT = txtPort.Text.Trim();
                    mo.C_WRITE_PORT = txtWritePort.Text.Trim();
                    mo.C_READ_PORT = txtReadPort.Text.Trim();
                    mo.C_IP_ADDRESS = txtIpAddress.Text.Trim();
                    mo.C_TYPE = cmbType.SelectedValue.ToString();
                    mo.I_AUTO = cbAuto.Checked ? 1 : 0;
                    mo.I_IN_MOBILE = cbIn.Checked ? 1 : 0;
                    mo.I_OUT_MOBILE = cbOut.Checked ? 1 : 0;

                    if (bll.Update(mo))
                    {
                        MessageBox.Show("保存成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Log.saveLog("修改库房成功！Id：" + lblId.Text);
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("保存失败！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
