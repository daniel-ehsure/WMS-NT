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
    public partial class MaterielTypeAdd : Form
    {
        MaterielTypeBLL bll = new MaterielTypeBLL();

        string pid;
        MaterielTypeForm parentForm;

        public MaterielTypeAdd(MaterielTypeForm parentForm)
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
                T_JB_MATERIELTYPE temp = new T_JB_MATERIELTYPE();
                temp.C_name = txtName.Text.Trim();
                temp.C_pre_id = lblPid.Text.Trim();
                temp.I_grade = 1;
                temp.I_if_jx = cbJx.Checked ? 1 : 0;
                temp.I_end = cbEnd.Checked ? 1 : 0;
                temp.C_memo = txtMemo.Text.Trim();

                string c_id = bll.Save(temp);

                if (c_id != null && !(string.Empty.Equals(c_id.Trim())))
                {
                    temp.C_id = c_id;
                    parentForm.addType = temp;

                    MessageBox.Show("保存成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Log.saveLog("添加物料类型成功！Id：" + c_id);
                    Close();
                }
                else
                {
                    MessageBox.Show("获取保存失败！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
