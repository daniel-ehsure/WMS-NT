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

namespace UI
{
    public partial class PlaceForm : Form
    {
        PlaceBLL bll = new PlaceBLL();

        #region 初始化树需要
        public Hashtable messages = new Hashtable();
        private List<T_JB_Place> code_Child = new List<T_JB_Place>();
        private TreeNode currnetNode = null;
        public T_JB_Place currentPlace = null;

        #endregion

        public bool isAdd;

        bool isQuery = false;

        public PlaceForm()
        {
            InitializeComponent();
        }

        private void PlaceForm_Load(object sender, EventArgs e)
        {
            initTree();
            initNew(currentPlace.C_id);
            setList(currentPlace.C_id, null, null, -1,-1, -1);
        }

        /// <summary>
        /// 选择类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                currnetNode = e.Node;
                currentPlace = (T_JB_Place)messages[e.Node];
                if (currentPlace.I_end == 1)
                {
                    this.button1.Enabled = false;
                }
                else
                {
                    this.button1.Enabled = true;
                }
                initNew(currentPlace.C_id);
                isQuery = false;
                setList(currentPlace.C_id, null, null, -1, -1, currentPlace.I_grade);
            }
            catch (Exception)
            {
                MessageBox.Show("与数据库连接失败，请查看网络连接是否正常。如不能解决请与网络管理员联系！", "严重错误：", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            isQuery = true;
            querryList();
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            initNew(currentPlace.C_id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (currentPlace.I_grade < 0)
            {
                MessageBox.Show("不能在当前级别下添加！");
            }
            else if (currentPlace.I_end == 1)
            {
                MessageBox.Show("不能在最小控制单元下添加！");
            }
            else
            {
                PlaceAddSingle wa = new PlaceAddSingle(this);

                wa.ShowDialog();

                if (isAdd)
                {
                    addNewType();
                    setList(currentPlace.C_id, null, null, -1, -1, currentPlace.I_grade);
                    initNew(currentPlace.C_id);
                    isAdd = false;
                }
            }
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
                        if (bll.IsHaveChild(id))
                        {
                            lists.Clear();
                            MessageBox.Show("要删除的货位下有子货位，请删除子货位后重试!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                        else
                        {
                            if (bll.IsInUse(id)) //货位被使用
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
                    }
                    if (lists.Count > 0)
                    {
                        if (bll.Delete(lists))
                        {
                            foreach (string tempid in lists)
                            {
                                deleteNode(this.treeView1.Nodes[0], tempid);
                            }
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

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            T_JB_Place all = new T_JB_Place();
            all.C_name = "全部";
            all.C_id = "0";
            all.I_grade = -1;
            messages.Add(root, all);


            code_Child = bll.GetAllChild("0", 0);
            for (int i = 0; i < code_Child.Count; i++)
            {
                TreeNode subNode = new TreeNode(code_Child[i].C_name);
                subNode.ImageIndex = 1;
                root.Nodes.Add(subNode);
                addTree(code_Child[i].C_id, subNode, 0);
                messages.Add(subNode, code_Child[i]);
            }


            this.treeView1.Nodes.Add(root);
            currnetNode = root;
            currentPlace = all;
            //展开根节点
            root.Expand();
        }

        /// <summary>
        /// 递归向树控件中添加节点
        /// </summary>
        /// <param name="id"></param>
        /// <param name="node"></param>
        private void addTree(string id, TreeNode node, int grade)
        {
            List<T_JB_Place> list = bll.GetAllChild(id, grade++);

            if (list.Count <= 0)
            {
                return;
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    TreeNode subNode = new TreeNode(list[i].C_name_tree);
                    subNode.ImageIndex = 1;
                    node.Nodes.Add(subNode);
                    addTree(list[i].C_id, subNode, grade);
                    messages.Add(subNode, list[i]);
                }
            }
        }

        #endregion

        /// <summary>
        /// 设置列表
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="name"></param>
        /// <param name="meno"></param>
        /// <param name="end"></param>
        private void setList(string pid, string id, string meno, int end, int use, int grade)
        {
            this.dgv_Data.DataSource = bll.GetList(pid, id, meno, end, use, grade);
            getName();
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void querryList()
        {
            if (isQuery)
            {
                string name = null;
                string meno = null;
                if (this.txtId.Text != null && !(string.Empty.Equals(txtId.Text.Trim())))
                {
                    name = txtId.Text;
                }
                if (this.txtMeno.Text != null && !(string.Empty.Equals(txtMeno.Text.Trim())))
                {
                    meno = txtMeno.Text;
                }
                int end = -1;
                if (cbEnd.Checked)
                {
                    end = 1;
                }
                int use = -1;
                if (cbUse.Checked)
                {
                    use = 1;
                }
                setList(null, name, meno, end, use, 1);
            }
            else
            {
                setList(currentPlace.C_id, null, null, -1,-1, currentPlace.I_grade);
            }
        }

        /// <summary>
        /// dataGridView　的名称
        /// </summary>
        private void getName()
        {
            dgv_Data.Columns[0].HeaderText = "编码";
            dgv_Data.Columns[1].HeaderText = "名称";
            dgv_Data.Columns[2].HeaderText = "货区";
            dgv_Data.Columns[3].HeaderText = "是否可用";
            dgv_Data.Columns[4].HeaderText = "是否最小控制单元";
            dgv_Data.Columns[5].HeaderText = "级别";
            dgv_Data.Columns[5].Visible = false;
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="pid"></param>
        private void initNew(string pid)
        {
            this.txtId.Text = string.Empty;
            this.cbEnd.Checked = false;
            this.txtMeno.Text = string.Empty;

        }

        /// <summary>
        /// 给树添加新增的货位节点
        /// </summary>
        public void addNewType()
        {
            initTree();
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="tnParent"></param>
        /// <param name="strValue"></param>
        private void deleteNode(TreeNode tnParent, string strValue)
        {
            if (tnParent == null)
            {
                return;
            }
            T_JB_Place tempType = (T_JB_Place)messages[tnParent];
            if (tempType.C_id == strValue)
            {

                tnParent.Remove();
                messages.Remove(tnParent);
                return;
            }

            TreeNode tnRet = null;
            foreach (TreeNode tn in tnParent.Nodes)
            {
                deleteNode(tn, strValue);
                //if (tnRet != null) break;
            }

        }

        /// <summary>
        /// 修改
        /// </summary>
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

            if ((int)dgv_Data.SelectedRows[0].Cells[5].Value < 1)
            {
                MessageBox.Show("当前记录不能修改！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (dgv_Data.SelectedRows.Count == 1)
            {
                PlaceModify mod = new PlaceModify(Convert.ToString(dgv_Data.SelectedRows[0].Cells[0].Value));
                mod.ShowDialog();
                refreshNode(this.treeView1.Nodes[0], Convert.ToString(dgv_Data.SelectedRows[0].Cells[0].Value));
                querryList();
            }
        }

        /// <summary>
        /// 刷新节点
        /// </summary>
        /// <param name="tnParent"></param>
        /// <param name="strValue"></param>
        private void refreshNode(TreeNode tnParent, string strValue)
        {
            if (tnParent == null)
            {
                return;
            }
            T_JB_Place tempType = (T_JB_Place)messages[tnParent];
            if (tempType.C_id == strValue)
            {
                messages.Remove(tnParent);

                T_JB_Place newType = bll.GetById(strValue);
                tnParent.Text = newType.C_name;
                messages.Add(tnParent, newType);
                return;
            }

            TreeNode tnRet = null;
            foreach (TreeNode tn in tnParent.Nodes)
            {
                refreshNode(tn, strValue);
                //if (tnRet != null) break;
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
            if (e.ColumnIndex == 4 || e.ColumnIndex == 3)
            {
                e.FormattingApplied = true;
                DataGridViewRow row = dgv_Data.Rows[e.RowIndex];
                if (row != null)
                {
                    if (e.Value.Equals(1))
                    {
                        e.Value = "是";
                    }
                    else
                    {
                        e.Value = "否";
                    }
                }
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            if (currentPlace.I_grade < 0)
            {
                MessageBox.Show("不能在当前级别下添加！");
            }
            else if (currentPlace.I_end == 1)
            {
                MessageBox.Show("不能在最小控制单元下添加！");
            }
            else if (currnetNode.Nodes != null && currnetNode.Nodes.Count > 0)
            {
                MessageBox.Show("当前级别已存在下级！");
            }
            else
            {
                PlaceAdd wa = new PlaceAdd(this);
                wa.ShowDialog();

                if (isAdd)
                {
                    addNewType();
                    setList(currentPlace.C_id, null, null, -1, -1, currentPlace.I_grade);
                    initNew(currentPlace.C_id);
                    isAdd = false;
                }
            }
        }
    }
}
