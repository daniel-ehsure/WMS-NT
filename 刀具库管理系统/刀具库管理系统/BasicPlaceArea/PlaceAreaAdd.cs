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
    public partial class PlaceAreaAdd : Form
    {
        PlaceAreaBLL bll = new PlaceAreaBLL();

        public PlaceAreaAdd()
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
                T_JB_PLACEAREA mo = new T_JB_PLACEAREA();

                mo.C_NAME = txtName.Text.Trim();
                mo.C_MEMO = txtMemo.Text.Trim();
  
                if (bll.Save(mo))
                {
                    MessageBox.Show("保存成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Log.saveLog("添加货区成功！Id：" + lblId.Text);
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
