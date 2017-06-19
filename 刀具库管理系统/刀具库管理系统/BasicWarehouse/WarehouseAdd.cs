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

        public WarehouseAdd()
        {
            InitializeComponent();

            lblId.Text = bll.GetNextCode();
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
                T_JB_WAREHOUSE mo = new T_JB_WAREHOUSE();

                mo.C_COM = txtCom.Text.Trim();
                mo.C_NAME = txtName.Text.Trim();
                mo.C_BAUDRATE = txtBaudrate.Text.Trim();
                mo.C_PORT = txtPort.Text.Trim();
                mo.C_WRITE_PORT = txtWritePort.Text.Trim();
                mo.C_READ_PORT = txtReadPort.Text.Trim();
                mo.C_IP_ADDRESS = txtIpAddress.Text.Trim();
                mo.C_TYPE = txtType.Text.Trim();
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


        }
    }
}
