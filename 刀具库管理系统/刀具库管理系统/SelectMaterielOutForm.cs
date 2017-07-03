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
    public partial class SelectMaterielOutForm : Form
    {
       
        StocksBLL stockBll = new StocksBLL();
        InterfaceSelect parent = null;
        DataTable dt;       
        int index = 0;
        public SelectMaterielOutForm(InterfaceSelect parent, DataTable dt)
        {
            InitializeComponent();
            this.parent = parent;            
            this.dt = dt;
        }

        private void SelectMateriel_Load(object sender, EventArgs e)
        {
            querylist();
        }
        //重置
        private void button5_Click(object sender, EventArgs e)
        {
            this.textBox5.Text = string.Empty;
            this.textBox4.Text = string.Empty;
            this.textBox6.Text = string.Empty;
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

        //双击选择
        private void dgv_Data_DoubleClick(object sender, EventArgs e)
        {
            setParent();
        }
        //选择按钮
        private void button1_Click(object sender, EventArgs e)
        {
            setParent();
        }
       





        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //设置当前选择行
            index = e.RowIndex;
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
                if (this.textBox5.Text != null && !(string.Empty.Equals(this.textBox5.Text.Trim())))
                {
                    id = textBox5.Text.Trim();
                }
                if (this.textBox4.Text != null && !(string.Empty.Equals(this.textBox4.Text.Trim())))
                {
                    name = textBox4.Text.Trim();
                }
                if (this.textBox1.Text != null && !(string.Empty.Equals(this.textBox1.Text.Trim())))
                {
                    place = textBox1.Text.Trim();
                }
                if (this.textBox6.Text != null && !(string.Empty.Equals(this.textBox6.Text.Trim())))
                {
                    standerd = textBox6.Text.Trim();
                }

                DataTable st = stockBll.getStocksList(id, name, place, standerd, Global.longid);

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
                dgv_Data.Columns[1].HeaderText = "物料名称";
                dgv_Data.Columns[2].HeaderText = "物料类别";
                dgv_Data.Columns[3].HeaderText = "规格型号";
                dgv_Data.Columns[4].HeaderText = "货位";
                dgv_Data.Columns[5].HeaderText = "库存数量";
                dgv_Data.Columns[6].HeaderText = "可用数量";
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

                parent.setMaterielAndPlace(name, id, standard, pid, null, count, typeName);
                this.Close();
            }
        }

     
        
    }
}
