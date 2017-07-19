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
    public partial class SelectMaterielOutNumForm : Form
    {
       
        StocksBLL stockBll = new StocksBLL();
        InterfaceSelect parent = null;
        DataTable dt;
        DataTable dtBak;
        int index = 0;
        public string materielId = string.Empty;

        public SelectMaterielOutNumForm(InterfaceSelect parent, DataTable dt, DataTable dtBak, string materielId)
        {
            InitializeComponent();
            this.parent = parent;            
            this.dt = dt;
            this.dtBak = dtBak;
            this.materielId = materielId;
        }

        private void SelectMateriel_Load(object sender, EventArgs e)
        {
            querylist();
            this.dgv_Data.EditMode = DataGridViewEditMode.EditOnEnter;
            this.dgv_Data.DataError += delegate(object sender1, DataGridViewDataErrorEventArgs e1) { };  
        }
        //重置
        private void button5_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = string.Empty;
        }
        //查询
        private void button4_Click(object sender, EventArgs e)
        {
            querylist();
        }
        //关闭
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //选择按钮
        private void button1_Click(object sender, EventArgs e)
        {
            if (dgv_Data.SelectedRows.Count > 0)
            {
                for (int j = 0; j < dgv_Data.SelectedRows.Count; j++)
                {
                    float res;
                    if (float.TryParse(dgv_Data.SelectedRows[j].Cells[8].Value.ToString(), out res))
                    {
                        if (res > Convert.ToInt32(dgv_Data.SelectedRows[j].Cells[6].Value))
                        {
                            MessageBox.Show("出库数量不能大于可用数量！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("出库数量必须为数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                for (int j = 0; j < dgv_Data.SelectedRows.Count; j++)
                {
                    DataRow dr = dtBak.NewRow();
                    dr[0] = materielId;
                    //dr[1] = txtMaterielName.Text;
                    //dr[2] = txtStand.Text;
                    dr[3] = Convert.ToInt32(dgv_Data.SelectedRows[j].Cells[8].Value);
                    dr[4] = dgv_Data.SelectedRows[j].Cells[4].Value.ToString();
                    //dr[5] = dtpIndate.Value.ToString("yyyy-MM-dd");
                    dr[6] = Global.longid;
                    dr[7] = dgv_Data.SelectedRows[j].Cells[2].Value.ToString();
                    dr[8] = dgv_Data.SelectedRows[j].Cells[7].Value.ToString();
                    dtBak.Rows.Add(dr);
                }

                Close();
            }
            else
            {
                MessageBox.Show("请选择数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
     
        /// <summary>
        /// 查询物料列表
        /// </summary>
        private void querylist()
        {
            try
            {
                string id = null;
                string name = null;
                string place = null;
                string standerd = null;                
                if (this.textBox1.Text != null && !(string.Empty.Equals(this.textBox1.Text.Trim())))
                {
                    place = textBox1.Text.Trim();
                }

                DataTable st = stockBll.getStocksList(id, name, place, standerd, Global.longid, materielId);

                for (int i = 0; i < st.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {

                        if (st.Rows[i][0].Equals(dt.Rows[j][0]) && st.Rows[i][4].Equals(dt.Rows[j][4]))
                        {
                            st.Rows[i][6] = Convert.ToInt32(st.Rows[i][6]) - Convert.ToInt32(dt.Rows[j][3]);
                            break;
                        }
                    }
                }

                DataView dv = st.DefaultView;
                dv.RowFilter = "canuse > 0";

                dgv_Data.DataSource = dv;

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
                dgv_Data.Columns[5].HeaderText = "库存数量";
                dgv_Data.Columns[5].ReadOnly = true;
                dgv_Data.Columns[6].HeaderText = "可用数量";
                dgv_Data.Columns[6].ReadOnly = true;
                dgv_Data.Columns[7].HeaderText = "库存编码";
                dgv_Data.Columns[7].Visible = false;
                dgv_Data.Columns[8].HeaderText = "出库数量";
                dgv_Data.Columns[8].ReadOnly = false;
                
            }
            catch (Exception)
            {
                MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
             
                return;
            }
        }

        private void setParent()
        {
            if (index >= 0 && dgv_Data.SelectedRows.Count > 0)
            {
                string id = this.dgv_Data.Rows[index].Cells[0].Value.ToString();
                string name = this.dgv_Data.Rows[index].Cells[1].Value.ToString();              
                string standard = this.dgv_Data.Rows[index].Cells[3].Value.ToString(); 
                string pid = this.dgv_Data.Rows[index].Cells[4].Value.ToString();
                int count = Convert.ToInt32(this.dgv_Data.Rows[index].Cells[6].Value);
                string typeName = this.dgv_Data.Rows[index].Cells[2].Value.ToString();
                string stockId = this.dgv_Data.Rows[index].Cells[7].Value.ToString();

                parent.setMaterielAndPlace(name, id, standard, pid, stockId, count, typeName);
                this.Close();
            }
        }
    }
}
