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
    public partial class PlaceAreaModify : Form
    {
        PlaceAreaBLL bll = new PlaceAreaBLL();
        string id;

        public PlaceAreaModify(string id)
        {
            InitializeComponent();

            this.id = id;

            Init();

        }

        private void Init()
        {
            try
            {
                T_JB_PLACEAREA mo = bll.GetById(id);

                if (mo == null)
                {
                    MessageBox.Show("获取信息失败！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
                else
                {
                    lblId.Text = mo.C_ID;
                    txtName.Text = mo.C_NAME;
                    txtMemo.Text = mo.C_MEMO;
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
                    T_JB_PLACEAREA mo = new T_JB_PLACEAREA();

                    mo.C_ID = id;
                    mo.C_NAME = txtName.Text.Trim();
                    mo.C_MEMO = txtMemo.Text.Trim();


                    if (bll.Update(mo))
                    {
                        MessageBox.Show("保存成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Log.saveLog("修改货区成功！Id：" + lblId.Text);
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
