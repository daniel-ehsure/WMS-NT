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
    public partial class EmployeeForm : Form
    {
        EmployeeBLL mbll = new EmployeeBLL();
        UnitBLL bll = new UnitBLL();

        #region 初始化树需要
        public Hashtable messages = new Hashtable();
        private List<T_DM_UNIT> code_Child = new List<T_DM_UNIT>();
        private TreeNode currnetNode = null;
        private T_DM_UNIT currentUnit = null;

        #endregion
        bool isQuery = false;

        public EmployeeForm()
        {
            InitializeComponent();
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            InitGW();

            initTree();
            initData();
            initNew();
        }

        /// <summary>
        /// 初始化岗位
        /// </summary>
        private void InitGW()
        {
            DataTable dtt = mbll.GetGWList();
            DataView dataViewt = dtt.DefaultView;
            DataTable dt = dataViewt.ToTable();
            DataRow dr = dt.NewRow();
            dr["C_ID"] = "";
            dr["C_NAME"] = "所有";
            dt.Rows.InsertAt(dr, 0);
            cmbGW.DataSource = dt;
            cmbGW.DisplayMember = "C_NAME";
            cmbGW.ValueMember = "C_ID";
        }

        //选择类别
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                currnetNode = e.Node;
                currentUnit = (T_DM_UNIT)messages[e.Node];

                this.button1.Enabled = true;
                initNew();
                isQuery = false;
                querryList();
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
            currentUnit = (T_DM_UNIT)messages[currnetNode];
            EmployeeAdd mm = new EmployeeAdd(null, currentUnit.C_id);
            mm.ShowDialog();
            InitGW();
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

                            lists.Add(id);
                    }
                    if (lists.Count > 0)
                    {
                        if (mbll.delete(lists))
                        {
                            InitGW();
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
            TreeNode root = new TreeNode("公司");
            root.ImageIndex = 2;

            T_DM_UNIT all = new T_DM_UNIT();
            all.C_name = "公司";
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
            currentUnit = all;
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
            List<T_DM_UNIT> list = bll.GetAllChild(id);

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
        /// dataGridView　的名称
        /// </summary>
        private void getName()
        {
            dgv_Data.Columns[0].HeaderText = "员工编码";
            dgv_Data.Columns[1].HeaderText = "员工姓名";
            dgv_Data.Columns[2].HeaderText = "部门编码";
            dgv_Data.Columns[3].HeaderText = "部门名称";
            dgv_Data.Columns[4].HeaderText = "性别";
            dgv_Data.Columns[5].HeaderText = "岗位";
            dgv_Data.Columns[6].HeaderText = "生日";
            dgv_Data.Columns[7].HeaderText = "地址";
            dgv_Data.Columns[8].HeaderText = "办公电话";
            dgv_Data.Columns[9].HeaderText = "手机";
            dgv_Data.Columns[10].HeaderText = "备注";
        }

        private void initNew()
        {
            this.textBox2.Text = string.Empty;
            this.txtName.Text = string.Empty;
            this.cmbGW.SelectedIndex = 0;

        }

        private void querryList()
        {
            string name = null;
            string gw = null;
            string unit = null;
            string cid = null;

            if (this.txtName.Text != null && !(string.Empty.Equals(txtName.Text.Trim())))
            {
                name = txtName.Text;
            }
            if (this.cmbGW.SelectedValue != null && !(string.Empty.Equals(cmbGW.SelectedValue.ToString().Trim())))
            {
                gw = cmbGW.SelectedValue.ToString();
            }
            if (this.textBox2.Text != null && !(string.Empty.Equals(textBox2.Text.Trim())))
            {
                cid = textBox2.Text;
            }
            if (isQuery == false)
            {
                currentUnit = (T_DM_UNIT)messages[currnetNode];
                unit = currentUnit.C_id;
            }

            this.dgv_Data.DataSource = mbll.getEmployeeList(name, gw, unit, cid);
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
                EmployeeModify mm = new EmployeeModify(Convert.ToString(dgv_Data.SelectedRows[0].Cells[0].Value), Convert.ToString(dgv_Data.SelectedRows[0].Cells[2].Value));
                mm.ShowDialog();
                InitGW();
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

        /// <summary>
        /// 格式化列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_Data_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                e.FormattingApplied = true;
                DataGridViewRow row = dgv_Data.Rows[e.RowIndex];
                if (row != null)
                {
                    e.Value = DateTime.Parse(e.Value.ToString()).ToString("yyyy-MM-dd");
                }
            }
        }
    }
}
