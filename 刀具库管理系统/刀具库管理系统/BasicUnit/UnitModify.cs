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
    public partial class UnitModify : Form
    {
        UnitBLL bll = new UnitBLL();
        string id;

        public UnitModify(string id)
        {
            InitializeComponent();

            this.id = id;

            Init();

        }

        private void Init()
        {
            try
            {
                T_DM_UNIT mo = bll.GetById(id);

                if (mo == null)
                {
                    MessageBox.Show("获取信息失败！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
                else
                {
                    this.lblId.Text = mo.C_id;
                    this.txtName.Text = mo.C_name;
                    this.lblPid.Text = mo.C_pre_id;
                    this.txtMemo.Text = mo.C_memo;
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

            if (bll.IsExit(lblPid.Text, txtName.Text.Trim(), id))
            {
                MessageBox.Show("名称重复！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    T_DM_UNIT mo = new T_DM_UNIT();

                    mo.C_id = id;
                    mo.C_pre_id = lblPid.Text;
                    mo.C_name = txtName.Text.Trim();
                    mo.C_memo = txtMemo.Text.Trim();

                    if (bll.Update(mo))
                    {
                        MessageBox.Show("保存成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Log.saveLog("修改部门成功！Id：" + lblId.Text);
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
