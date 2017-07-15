using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BLL;
using Model;
using System.Collections;
using Util;

namespace UI
{
    public partial class MaterielForm : Form
    {
        MaterielBLL mbll = new MaterielBLL();
        MaterielTypeBLL bll = new MaterielTypeBLL();
        PlaceAreaBLL abll = new PlaceAreaBLL();

        #region 初始化树需要
        public Hashtable messages = new Hashtable();
        private List<T_JB_MaterielType> code_Child = new List<T_JB_MaterielType>();
        private TreeNode currnetNode = null;
        private T_JB_MaterielType currentType = null;

        #endregion
        bool isQuery = false;

        private int index = 0;

        public MaterielForm()
        {
            InitializeComponent();
        }

        private void MaterielTypeForm_Load(object sender, EventArgs e)
        {
            #region 货区
            DataTable dtt = abll.GetList(null);
            DataView dataViewt = dtt.DefaultView;
            dataViewt.Sort = "C_ID asc";
            DataTable dt = dataViewt.ToTable();
            DataRow dr = dt.NewRow();
            dr["C_ID"] = "";
            dr["C_NAME"] = "所有";
            dt.Rows.InsertAt(dr, 0);
            cmbArea.DataSource = dt;
            cmbArea.DisplayMember = "C_NAME";
            cmbArea.ValueMember = "C_ID";
            #endregion

            initTree();
            initData();
            initNew();
           
        }

        //选择类别
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {               
                currnetNode = e.Node;
                currentType = (T_JB_MaterielType)messages[e.Node];
                if (currentType.I_end == 1)
                {
                    this.button1.Enabled = true;
                    initNew();
                    isQuery = false;
                    querryList();
                }
                else
                {
                    this.button1.Enabled = false;
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //查询
        private void button4_Click(object sender, EventArgs e)
        {
            isQuery = true;
            querryList();
        }
        //重置
        private void button5_Click(object sender, EventArgs e)
        {
            initNew();
        }
        //增加
        private void button1_Click(object sender, EventArgs e)
        {
            currentType = (T_JB_MaterielType)messages[currnetNode];
            if (currentType.C_id.Equals("0001"))
            {//刀具
                KnifeAdd mm = new KnifeAdd(null, currentType.C_id);
                mm.ShowDialog();
            }
            else
            {
                MaterielAdd mm = new MaterielAdd(null, currentType.C_id);
                mm.ShowDialog();
            }
            querryList();
        }
        //关闭
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //删除
        private void button8_Click(object sender, EventArgs e)
        {
            if (dgv_Data.SelectedRows.Count <= 0)
            {
                MessageBox.Show("没有要删除的记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("删除之后信息将不能恢复，是否确认删除?", "提示:", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

               
                try
                {
                    List<String> lists = new List<string>();

                    for (int i = 0; i < dgv_Data.SelectedRows.Count; i++)
                    {
                        string id = Convert.ToString(dgv_Data.SelectedRows[i].Cells[0].Value);
                        if (mbll.isInUse(id,Global.longid)) //物料被使用
                        {
                            lists.Clear();
                            MessageBox.Show("该信息被使用，不能删除!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                        else
                        {
                            lists.Add(id);
                        }
                    }
                    if (lists.Count > 0)
                    {
                        if (mbll.delete(lists))
                        {
                            
                            querryList();
                        }
                        else
                        {
                            MessageBox.Show("信息删除失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }


                }
                catch (Exception)
                {
                    MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
      
        //修改
        private void button3_Click(object sender, EventArgs e)
        {
            mod();
        }

        #region 初始化树

        /// <summary>
        /// 初始化树菜单
        /// </summary>
        public void initTree()
        {
            this.treeView1.Nodes.Clear();
            TreeNode root = new TreeNode("全部");
            root.ImageIndex = 2;

            T_JB_MaterielType all = new T_JB_MaterielType();
            all.C_name = "全部";
            all.C_id = "0";
            messages.Add(root, all);
           

            code_Child = bll.GetAllChild("0");
            for (int i = 0; i < code_Child.Count; i++)
            {
                TreeNode subNode = new TreeNode(code_Child[i].C_name);
                subNode.ImageIndex = 1;
                root.Nodes.Add(subNode);
                addTree(code_Child[i].C_id, subNode);
                messages.Add(subNode, code_Child[i]);
            }           
         
         
            this.treeView1.Nodes.Add(root);
            currnetNode = root;
            currentType = all;
            //展开根节点
            root.Expand();
        }

        /// <summary>
        /// 递归向树控件中添加节点
        /// </summary>
        /// <param name="id"></param>
        /// <param name="node"></param>
        private void addTree(string id, TreeNode node)
        {
            List<T_JB_MaterielType> list = bll.GetAllChild(id);

            if (list.Count <= 0)
            {
                return;
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    TreeNode subNode = new TreeNode(list[i].C_name);
                    subNode.ImageIndex = 1;
                    node.Nodes.Add(subNode);
                    addTree(list[i].C_id, subNode);
                    messages.Add(subNode, list[i]);
                }
            }
        }

        #endregion

        private void initData()
        {
            DataTable temp = new DataTable();

            for (int i = 0; i < 21; i++)
            {
                DataColumn column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = i.ToString();

                temp.Columns.Add(column);
            }
            this.dgv_Data.DataSource = temp;
            getName();
        }

        /// <summary>
        /// 工装dataGridView　的名称
        /// </summary>
        private void getName()
        {
            dgv_Data.Columns[0].HeaderText = "物料编码";
            dgv_Data.Columns[1].HeaderText = "物料名称";
            dgv_Data.Columns[2].HeaderText = "类别编码";
            dgv_Data.Columns[3].HeaderText = "类别名称";
            dgv_Data.Columns[4].HeaderText = "规格型号";
            dgv_Data.Columns[5].HeaderText = "货区编码";
            dgv_Data.Columns[6].HeaderText = "货区名称";
            dgv_Data.Columns[7].HeaderText = "备注";
        }

        private void initNew()
        {
            this.txtName.Text = string.Empty;
            this.cmbArea.SelectedIndex = 0;
            this.txtStand.Text = string.Empty;
            this.checkBox2.Checked = true;

        }

        private void querryList()
        {
            string name = null;
            string area = null;
            string type = null;
            string tuhao = null;
            string cid = null;
            int finish = -1;
            string standerd = null;

            if (this.txtName.Text != null && !(string.Empty.Equals(txtName.Text.Trim())))
            {
                name = txtName.Text;
            }
            if (this.cmbArea.SelectedValue != null && !(string.Empty.Equals(cmbArea.SelectedValue.ToString().Trim())))
            {
                area = cmbArea.SelectedValue.ToString();
            }
            if (this.textBox2.Text != null && !(string.Empty.Equals(textBox2.Text.Trim())))
            {
                cid = textBox2.Text;
            }
            if (isQuery == false)
            {
                currentType = (T_JB_MaterielType)messages[currnetNode];
                type = currentType.C_id;
            }
            else
            {
                if (checkBox2.Checked == false)
                {
                    currentType = (T_JB_MaterielType)messages[currnetNode];
                    type = currentType.C_id;
                }
            }
            if (this.txtStand.Text != null && !(string.Empty.Equals(txtStand.Text.Trim())))
            {
                standerd = txtStand.Text;
            }
            this.dgv_Data.DataSource = mbll.getMaterielList(name, area, type, finish, standerd,Global.longid,tuhao,cid);
            getName();
        }
        

      

       

        private void mod()
        {
            if (dgv_Data.SelectedRows.Count <= 0)
            {
                MessageBox.Show("没有要修改的记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dgv_Data.SelectedRows.Count > 1)
            {
                MessageBox.Show("一次只能修改一个信息!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dgv_Data.SelectedRows.Count == 1)
            {
                if (currentType.C_id.Equals("0001"))
                {//刀具
                    KnifeModify mm = new KnifeModify(Convert.ToString(dgv_Data.SelectedRows[0].Cells[0].Value), Convert.ToString(dgv_Data.SelectedRows[0].Cells[2].Value));
                    mm.ShowDialog();
                }
                else
                {
                    MaterielModify mm = new MaterielModify(Convert.ToString(dgv_Data.SelectedRows[0].Cells[0].Value), Convert.ToString(dgv_Data.SelectedRows[0].Cells[2].Value));
                    mm.ShowDialog();
                }
                querryList();
            }
        }


        /// <summary>
        /// 双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_Data_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            mod();
        }
     }
}
