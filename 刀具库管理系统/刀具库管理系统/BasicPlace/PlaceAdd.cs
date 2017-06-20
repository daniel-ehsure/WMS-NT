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
    public partial class PlaceAdd : Form
    {
        PlaceBLL bll = new PlaceBLL();

        string pid;
        PlaceForm parentForm;
        Dictionary<int, Dictionary<string, Control>> dicCtrl;

        public PlaceAdd(PlaceForm parentForm)
        {
            InitializeComponent();

            this.pid = parentForm.currentPlace.C_id;
            this.parentForm = parentForm;
            lblPid.Text = pid;

            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            Dictionary<string, Control> dic1 = new Dictionary<string, Control> { { "num", txtNum1 }, { "name", txtName1 }, { "width", txtWidth1 }, { "length", txtLength1 }, { "end", cbEnd1 } };
            Dictionary<string, Control> dic2 = new Dictionary<string, Control> { { "num", txtNum2 }, { "name", txtName2 }, { "width", txtWidth2 }, { "length", txtLength2 }, { "end", cbEnd2 } };
            Dictionary<string, Control> dic3 = new Dictionary<string, Control> { { "num", txtNum3 }, { "name", txtName3 }, { "width", txtWidth3 }, { "length", txtLength3 }, { "end", cbEnd3 } };
            Dictionary<string, Control> dic4 = new Dictionary<string, Control> { { "num", txtNum4 }, { "name", txtName4 }, { "width", txtWidth4 }, { "length", txtLength4 }, { "end", cbEnd4 } };
            Dictionary<string, Control> dic5 = new Dictionary<string, Control> { { "num", txtNum5 }, { "name", txtName5 }, { "width", txtWidth5 }, { "length", txtLength5 }, { "end", cbEnd5 } };
            Dictionary<string, Control> dic6 = new Dictionary<string, Control> { { "num", txtNum6 }, { "name", txtName6 }, { "width", txtWidth6 }, { "length", txtLength6 }, { "end", cbEnd6 } };
            Dictionary<string, Control> dic7 = new Dictionary<string, Control> { { "num", txtNum7 }, { "name", txtName7 }, { "width", txtWidth7 }, { "length", txtLength7 }, { "end", cbEnd7 } };
            dicCtrl = new Dictionary<int, Dictionary<string, Control>> { { 1, dic1 }, { 2, dic2 }, { 3, dic3 }, { 4, dic4 }, { 5, dic5 }, { 6, dic6 }, { 7, dic7 } };


            for (int i = 0; i < parentForm.currentPlace.I_grade; i++)
            {
                SetDic(dicCtrl[dicCtrl.Count - 1 - i]);
            }
        }

        private void SetDic(Dictionary<string, Control> dic)
        {
            foreach (var item in dic.Values)
            {
                item.Enabled = false;
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //todo:validate

            List<List<object>> list = new List<List<object>>();

            foreach (var item in dicCtrl.Values)
            {
                if (string.IsNullOrEmpty(item["num"].Text.Trim()))
                {
                    list.Add(new List<object> { item["num"].Text.Trim(), item["name"].Text.Trim(), item["width"].Text.Trim(), item["length"].Text.Trim(), ((CheckBox)item["end"]).Checked ? 1 : 0 });
                }
            }



            

            if (bll.SaveList(list, pid, parentForm.currentPlace.I_grade))
            {
                //parentForm.addType = temp;

                MessageBox.Show("保存成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log.saveLog("添加物料类型成功！");
                Close();
            }
            else
            {
                MessageBox.Show("获取保存失败！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
