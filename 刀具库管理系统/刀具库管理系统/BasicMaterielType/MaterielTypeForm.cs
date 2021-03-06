﻿using System;
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
    public partial class MaterielTypeForm : Form
    {
        MaterielTypeBLL bll = new MaterielTypeBLL();

        #region 初始化树需要
        public Hashtable messages = new Hashtable();
        private List<T_JB_MaterielType> code_Child = new List<T_JB_MaterielType>();
        private TreeNode currnetNode = null;
        public T_JB_MaterielType currentType = null;

        #endregion

        public T_JB_MaterielType addType = null;

        bool isQuery = false;

        public MaterielTypeForm()
        {
            InitializeComponent();
        }

        private void MaterielTypeForm_Load(object sender, EventArgs e)
        {
            initTree();
            initNew(currentType.C_id);
            setList(currentType.C_id, null, null, -1);
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
                //c_id = null;
                //c_name = null;
                currnetNode = e.Node;
                currentType = (T_JB_MaterielType)messages[e.Node];
                if (currentType.I_end == 1)
                {
                    this.button1.Enabled = false;
                }
                else
                {
                    this.button1.Enabled = true;
                }
                initNew(currentType.C_id);
                isQuery = false;
                setList(currentType.C_id, null, null, -1);
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
            initNew(currentType.C_id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            MaterielTypeAdd wa = new MaterielTypeAdd(this);
            wa.ShowDialog();

            if (addType !=null)
            {
                addNewType(addType);
                setList(currentType.C_id, null, null, -1);
                initNew(currentType.C_id);
                addType = null;
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

                        if (id.Equals("0001") || id.Equals("0002"))
                        {
                            MessageBox.Show("系统默认信息，不能删除!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        if (bll.IsHaveChild(id))
                        {
                            lists.Clear();
                            MessageBox.Show("要删除的类型下有子类型，请删除子类型后重试!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                        else
                        {
                            if (bll.IsInUse(id)) //类型被使用
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

        /// <summary>
        /// 设置列表
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="name"></param>
        /// <param name="meno"></param>
        /// <param name="end"></param>
        private void setList(string pid, string name, string meno, int end)
        {
            this.dgv_Data.DataSource = bll.GetList(pid, name, meno, end);
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
                if (this.txtName.Text != null && !(string.Empty.Equals(txtName.Text.Trim())))
                {
                    name = txtName.Text;
                }
                if (this.txtMeno.Text != null && !(string.Empty.Equals(txtMeno.Text.Trim())))
                {
                    meno = txtMeno.Text;
                }
                int end = -1;
                if (checkBox1.Checked)
                {
                    end = 1;
                }
                //else
                //{
                //    end = 0;
                //}
                setList(null, name, meno, end);
            }
            else
            {
                setList(currentType.C_id, null, null, -1);
            }
        }

        /// <summary>
        /// dataGridView　的名称
        /// </summary>
        private void getName()
        {
            dgv_Data.Columns[0].HeaderText = "编码";
            dgv_Data.Columns[1].HeaderText = "名称";
            dgv_Data.Columns[2].HeaderText = "上级编码";
            dgv_Data.Columns[3].HeaderText = "顺序";
            dgv_Data.Columns[3].Visible = false;
            dgv_Data.Columns[4].HeaderText = "是否定检";
            //dgv_Data.Columns[4].Visible = false;
            dgv_Data.Columns[5].HeaderText = "是否末级";
            dgv_Data.Columns[6].HeaderText = "备注";

        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="pid"></param>
        private void initNew(string pid)
        {
            this.txtName.Text = string.Empty;
            this.lblName.Visible = false;
            this.txtPid.Text = pid;
            this.checkBox1.Checked = false;
            this.txtMeno.Text = string.Empty;

        }

        /// <summary>
        /// 给树添加新增的类型节点
        /// </summary>
        /// <param name="dm_type"></param>
        public void addNewType(T_JB_MaterielType dm_type)
        {
            TreeNode subnode = new TreeNode(dm_type.C_name);
            subnode.ImageIndex = 1;
            currnetNode.Nodes.Add(subnode);
            messages.Add(subnode, dm_type);
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
            T_JB_MaterielType tempType = (T_JB_MaterielType)messages[tnParent];
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
            if (dgv_Data.SelectedRows.Count == 1)
            {
                if (dgv_Data.SelectedRows[0].Cells[0].Value.Equals("0001") || dgv_Data.SelectedRows[0].Cells[0].Value.Equals("0002"))
                {
                    MessageBox.Show("系统默认信息，不能修改!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                MaterielTypeModify mod = new MaterielTypeModify(Convert.ToString(dgv_Data.SelectedRows[0].Cells[0].Value));
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
            T_JB_MaterielType tempType = (T_JB_MaterielType)messages[tnParent];
            if (tempType.C_id == strValue)
            {
                messages.Remove(tnParent);

                T_JB_MaterielType newType = bll.GetById(strValue);
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
            if (e.ColumnIndex == 4 || e.ColumnIndex == 5)
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
    }
}
