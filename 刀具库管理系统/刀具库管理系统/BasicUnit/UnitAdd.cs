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
    public partial class UnitAdd : Form
    {
        UnitBLL bll = new UnitBLL();

        string pid;
        UnitForm parentForm;

        public UnitAdd(UnitForm parentForm)
        {
            InitializeComponent();

            this.pid = parentForm.currentType.C_id;
            this.parentForm = parentForm;
            lblPid.Text = pid;

        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (bll.IsExit(lblPid.Text.Trim(), txtName.Text.Trim()))
            {
                MessageBox.Show("名称重复！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    T_DM_UNIT temp = new T_DM_UNIT();
                    temp.C_name = txtName.Text.Trim();
                    temp.C_pre_id = lblPid.Text.Trim();
                    temp.C_memo = txtMemo.Text.Trim();

                    string c_id = bll.Save(temp);

                    if (c_id != null && !(string.Empty.Equals(c_id.Trim())))
                    {
                        temp.C_id = c_id;
                        parentForm.addType = temp;

                        MessageBox.Show("保存成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Log.saveLog("添加部门成功！Id：" + c_id);
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("获取保存失败！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
