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
    public partial class SelectKnifeOutForm : Form
    {

        StocksBLL stockBll = new StocksBLL();
        MachineBLL mBll = new MachineBLL();
        DataTable dt;
        DataTable dtBak;
        int index = 0;
        string ids;
        public string materielId = string.Empty;
        List<string> list;

        public SelectKnifeOutForm(string ids, DataTable dt, DataTable dtBak, List<string> listIds, List<string> listMachine)
        {
            InitializeComponent();
            this.ids = ids;
            this.dt = dt;
            this.dtBak = dtBak;

            #region 初始化 机床
            DataTable dtt = mBll.GetList();
            DataView dataViewt = dtt.DefaultView;
            dataViewt.Sort = "C_ID asc";
            cmbMachine.DataSource = dataViewt.ToTable();
            cmbMachine.DisplayMember = "C_ID";
            cmbMachine.ValueMember = "C_ID";
            #endregion
        }

        private void SelectMateriel_Load(object sender, EventArgs e)
        {
            try
            {
                string[] arr = ids.Split(new char[] { ',' });
                list = new List<string>(arr);

                DataTable st = stockBll.GetKnifeStocksList(list);

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
                dgv_Data.Columns[0].ReadOnly = true;
                dgv_Data.Columns[1].HeaderText = "物料名称";
                dgv_Data.Columns[1].ReadOnly = true;
                dgv_Data.Columns[2].HeaderText = "物料类别";
                dgv_Data.Columns[2].ReadOnly = true;
                dgv_Data.Columns[3].HeaderText = "规格型号";
                dgv_Data.Columns[3].ReadOnly = true;
                dgv_Data.Columns[4].HeaderText = "货位";
                dgv_Data.Columns[4].ReadOnly = true;
                dgv_Data.Columns[5].HeaderText = "已有出库使用";
                dgv_Data.Columns[5].ReadOnly = true;
            }
            catch (Exception)
            {
                MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
        }

        //关闭
        private void button3_Click(object sender, EventArgs e)
        {
            dt.Clear();
            this.Close();
        }

        //选择按钮
        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbMachine.Text.Trim().Length > 0)
            {
                if (dgv_Data.SelectedRows.Count > 0)
                {
                    List<string> listRep = new List<string>();

                    //for (int j = 0; j < dgv_Data.SelectedRows.Count; j++)
                    //{
                    //    if (listUseKnife.Contains(dgv_Data.SelectedRows[j].Cells[0].Value.ToString()))
                    //    {
                    //        listRep.Add(
                    //    }
                    //}

                    for (int j = 0; j < dgv_Data.SelectedRows.Count; j++)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = dgv_Data.SelectedRows[j].Cells[0].Value.ToString();
                        dr[1] = dgv_Data.SelectedRows[j].Cells[1].Value.ToString();
                        dr[2] = dgv_Data.SelectedRows[j].Cells[3].Value.ToString();
                        //dr[3] = Convert.ToInt32(dgv_Data.SelectedRows[j].Cells[8].Value);
                        dr[4] = dgv_Data.SelectedRows[j].Cells[4].Value.ToString();
                        //dr[5] = dtpIndate.Value.ToString("yyyy-MM-dd");
                        dr[6] = Global.longid;

                        dt.Rows.Add(dr);
                    }

                    Close();
                }
                else
                {
                    MessageBox.Show("请选择数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("机床不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
