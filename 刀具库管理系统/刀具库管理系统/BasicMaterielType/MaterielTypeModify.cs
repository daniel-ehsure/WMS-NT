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
    public partial class MaterielTypeModify : Form
    {
        MaterielTypeBLL bll = new MaterielTypeBLL();
        string id;

        public MaterielTypeModify(string id)
        {
            InitializeComponent();

            this.id = id;

            Init();

        }

        private void Init()
        {
            try
            {
                T_JB_MaterielType mo = bll.GetById(id);

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
                    cbEnd.Checked = mo.I_end == 0 ? false : true;
                    cbJx.Checked = mo.I_if_jx == 0 ? false : true;
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
                    T_JB_MaterielType mo = new T_JB_MaterielType();

                    mo.C_id = id;
                    mo.C_pre_id = lblPid.Text;
                    mo.C_name = txtName.Text.Trim();
                    mo.C_memo = txtMemo.Text.Trim();
                    mo.I_end = cbEnd.Checked ? 1 : 0;
                    mo.I_if_jx = cbJx.Checked ? 1 : 0;

                    if (bll.Update(mo))
                    {
                        MessageBox.Show("保存成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Log.saveLog("修改物料类型成功！Id：" + lblId.Text);
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
