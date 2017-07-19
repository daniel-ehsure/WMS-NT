using System;
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
    public partial class WarehouseAdd : Form
    {
        WarehouseBLL bll = new WarehouseBLL();
        WHTypeBLL bllType = new WHTypeBLL();

        public WarehouseAdd()
        {
            InitializeComponent();

            lblId.Text = bll.GetNextCode();

            #region 初始化 类型
            DataTable dt = bllType.GetList(null);
            DataView dataView = dt.DefaultView;
            dataView.Sort = "C_ID asc";
            cmbType.DataSource = dataView.ToTable();
            cmbType.DisplayMember = "C_NAME";
            cmbType.ValueMember = "C_ID";
            #endregion

        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (bll.IsExit(txtName.Text.Trim()))
            {
                MessageBox.Show("名称重复！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    T_JB_WAREHOUSE mo = new T_JB_WAREHOUSE();

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

                    if (bll.Save(mo))
                    {
                        MessageBox.Show("保存成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Log.saveLog("添加库房成功！Id：" + lblId.Text);
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
