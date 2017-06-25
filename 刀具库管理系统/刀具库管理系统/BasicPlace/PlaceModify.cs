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
    public partial class PlaceModify : Form
    {
        PlaceBLL bll = new PlaceBLL();
        string id;

        public PlaceModify(string id)
        {
            InitializeComponent();

            this.id = id;

            Init();

        }

        private void Init()
        {
            try
            {
                T_JB_Place mo = bll.GetById(id);

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
                    cbInuse.Checked = mo.I_inuse == 0 ? false : true;
                    txtLength.Text = mo.I_length.ToString();
                    txtWidth.Text = mo.I_width.ToString();
                    txtMemo.Text = mo.C_memo;
                    lblChildren.Text = mo.I_children.ToString();
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

            if (int.Parse(lblChildren.Text)>0 && cbEnd.Checked)
            {
                MessageBox.Show("存在下级，不能设置为最小单元！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                T_JB_Place mo = new T_JB_Place();

                mo.C_id = id;
                mo.C_pre_id = lblPid.Text;
                mo.C_name = txtName.Text.Trim();
                mo.C_memo = txtMemo.Text.Trim();
                mo.I_end = cbEnd.Checked ? 1 : 0;
                mo.I_inuse = cbInuse.Checked ? 1 : 0;
                mo.I_length = int.Parse(txtLength.Text.Trim());
                mo.I_width = int.Parse(txtWidth.Text.Trim());

                if (bll.Update(mo))
                {
                    MessageBox.Show("保存成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Log.saveLog("修改货位成功！Id：" + lblId.Text);
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
