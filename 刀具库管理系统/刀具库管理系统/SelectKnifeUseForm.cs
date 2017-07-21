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
    public partial class SelectKnifeUseForm : Form
    {

        StocksBLL stockBll = new StocksBLL();
        OperateInOutBLL bll = new OperateInOutBLL();
        MachineBLL mBll = new MachineBLL();
        DataTable dt;
        DataTable dtBak;
        string id;
        public string materielId = string.Empty;

        public SelectKnifeUseForm(string id, DataTable dt, DataTable dtBak)
        {
            InitializeComponent();
            this.id = id;
            this.dt = dt;
            this.dtBak = dtBak;

            #region 初始化 机床
            DataTable dtt = mBll.GetList();
            //DataView dataViewt = dtt.DefaultView;
            //dataViewt.Sort = "C_ID asc";
            //cmbMachine.DataSource = dataViewt.ToTable();
            cmbMachine.DataSource = dtt.DefaultView;
            cmbMachine.DisplayMember = "C_ID";
            cmbMachine.ValueMember = "C_ID";
            cmbMachine.SelectedIndex = -1;
            #endregion
        }

        private void SelectMateriel_Load(object sender, EventArgs e)
        {
            queryList();
        }

        private void dgv_Data_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Set();
        }

        //关闭
        private void button3_Click(object sender, EventArgs e)
        {
            dtBak.Clear();
            this.Close();
        }

        //选择按钮
        private void button1_Click(object sender, EventArgs e)
        {
            Set();
        }

        private void Set()
        {
            if (dgv_Data.SelectedRows.Count > 0)
            {
                List<string> listRep = new List<string>();


                DataRow dr = dtBak.NewRow();
                dr[0] = dgv_Data.SelectedRows[0].Cells[0].Value.ToString();
                dr[1] = dgv_Data.SelectedRows[0].Cells[1].Value.ToString();
                dr[2] = dgv_Data.SelectedRows[0].Cells[3].Value.ToString();
                //dr[3] = Convert.ToInt32(dgv_Data.SelectedRows[j].Cells[8].Value);
                dr[4] = dgv_Data.SelectedRows[0].Cells[4].Value.ToString();
                //dr[5] = dtpIndate.Value.ToString("yyyy-MM-dd");
                dr[6] = Global.longid;
                dr[7] = dgv_Data.SelectedRows[0].Cells[5].Value.ToString();

                dtBak.Rows.Add(dr);

                Close();
            }
            else
            {
                MessageBox.Show("请选择数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            queryList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            cmbMachine.Text = string.Empty;
        }

        public void queryList()
        {
            try
            {
                DataTable st = bll.getKDList(Global.minValue, Global.minValue, textBox1.Text.Trim(), cmbMachine.Text.Trim(), InOutType.KNIFE_OUT_USE, id);

                List<int> listToDel = new List<int>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < st.Rows.Count; j++)
                    {
                        if (dt.Rows[i][0].Equals(st.Rows[j][0]) && dt.Rows[i][4].Equals(st.Rows[j][4]))
                        {
                            listToDel.Add(i);
                        }
                    }
                }

                listToDel.Sort();
                listToDel.Reverse();

                listToDel.ForEach(i => st.Rows.RemoveAt(i));

                dgv_Data.DataSource = st;

                dgv_Data.Columns[0].HeaderText = "物料编码";
                dgv_Data.Columns[1].HeaderText = "物料名称";
                dgv_Data.Columns[2].HeaderText = "物料类别";
                dgv_Data.Columns[3].HeaderText = "规格型号";
                dgv_Data.Columns[4].HeaderText = "货位";
                dgv_Data.Columns[5].HeaderText = "机床";
                dgv_Data.Columns[6].HeaderText = "日期";
                dgv_Data.Columns[6].Visible = false;
                dgv_Data.Columns[7].HeaderText = "编号";
                dgv_Data.Columns[7].Visible = false;
            }
            catch (Exception)
            {
                MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
        }
    }
}
