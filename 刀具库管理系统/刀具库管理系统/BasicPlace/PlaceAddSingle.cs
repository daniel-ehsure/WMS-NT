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
    public partial class PlaceAddSingle : Form
    {
        PlaceBLL bll = new PlaceBLL();
        PlaceForm parForm;
        string pid;
        int grade;

        public PlaceAddSingle(PlaceForm parForm)
        {
            InitializeComponent();

            this.parForm = parForm;
            this.pid = parForm.currentPlace.C_id;
            this.grade = parForm.currentPlace.I_grade;
            lblId.Text = bll.GetNextCode(pid);
            lblPid.Text = pid;
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                T_JB_Place mo = new T_JB_Place();

                mo.C_pre_id = lblPid.Text;
                mo.C_name = txtName.Text.Trim();
                mo.C_memo = txtMemo.Text.Trim();
                mo.I_end = cbEnd.Checked ? 1 : 0;
                mo.I_inuse = cbInuse.Checked ? 1 : 0;
                mo.I_length = int.Parse(txtLength.Text.Trim());
                mo.I_width = int.Parse(txtWidth.Text.Trim());
                mo.I_grade = grade + 1;

                if (!string.IsNullOrEmpty(bll.Save(mo)))
                {
                    parForm.isAdd = true;

                    MessageBox.Show("保存成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Log.saveLog("增加货位成功！Id：" + lblId.Text);
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
