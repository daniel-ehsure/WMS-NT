using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BLL;
using Model;

namespace UI
{
    public partial class LogForm : Form
    {
        LayoutBLL bll = new LayoutBLL();
        public LogForm()
        {
            InitializeComponent();
        }

        private void JurisdictionForm_Load(object sender, EventArgs e)
        {
            dtpDate.Value = DateTime.Now;

            query();
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        void query()
        {
            try
            {
                dataGridView1.DataSource = bll.getLogByDate(dtpDate.Value.ToString("yyyy-MM-dd"));
            }
            catch (Exception)
            {
                MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //关闭
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            query();
        }
    }
}
