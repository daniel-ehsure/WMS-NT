﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BLL;
using Util;

namespace UI
{
    public partial class SelectMaterielForm : Form
    {
        PlaceAreaBLL areaBll = new PlaceAreaBLL();
        MaterielBLL mbll = new MaterielBLL();
        MaterielTypeBLL tBll = new MaterielTypeBLL();
        InterfaceSelect parent;
        string materielType;

        public SelectMaterielForm(InterfaceSelect parent, string materielType)
        {
            InitializeComponent();
            this.parent = parent;
            this.materielType = materielType;
        }

        private void SelectMaterielFinishForm_Load(object sender, EventArgs e)
        {
            queryList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void queryList()
        {
            string name = string.Empty.Equals(txtName.Text.Trim())?null:txtName.Text;
            string area = null;
            int finish = -1;
            string standerd = string.Empty.Equals(txtStand.Text.Trim())?null:txtStand.Text;;
            string userid = Global.longid;
            DataTable dt = mbll.getMaterielList(name, area, materielType, finish, standerd, userid);

            dgv_Data.DataSource = dt;

            dgv_Data.Columns[0].HeaderText = "物料编码";
            dgv_Data.Columns[1].HeaderText = "物料名称";
            dgv_Data.Columns[2].Visible = false;
            dgv_Data.Columns[3].HeaderText = "类别名称";
            dgv_Data.Columns[4].HeaderText = "规格型号";
            dgv_Data.Columns[5].Visible = false;
            dgv_Data.Columns[6].HeaderText = "货区";
          
            dgv_Data.Columns[7].Visible = false;
            dgv_Data.Columns[8].Visible = false;
            dgv_Data.Columns[9].Visible = false;
            dgv_Data.Columns[10].Visible = false;
            dgv_Data.Columns[11].Visible = false;
            dgv_Data.Columns[12].Visible = false;
        }

        //确定
        private void button3_Click(object sender, EventArgs e)
        {
            set();
        }

        private void set()
        {
            if (dgv_Data.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择物料!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dgv_Data.SelectedRows.Count > 1)
            {
                MessageBox.Show("一次只能修改一个信息!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dgv_Data.SelectedRows.Count == 1)
            {
                parent.setMateriel(dgv_Data.SelectedRows[0].Cells[1].Value.ToString(), dgv_Data.SelectedRows[0].Cells[0].Value.ToString(), dgv_Data.SelectedRows[0].Cells[4].Value.ToString());
                this.Close();
            }
        }

        private void dgv_Data_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            set();
        }
        //查询
        private void button4_Click(object sender, EventArgs e)
        {
            queryList();
        }



      
    }
}
