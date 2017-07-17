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

        bool hasEnd;
        string pid;
        PlaceForm parentForm;
        Dictionary<int, Dictionary<string, Control>> dicCtrl;
        int grade;

        public PlaceAdd(PlaceForm parentForm)
        {
            InitializeComponent();

            this.pid = parentForm.currentPlace.C_id;
            this.parentForm = parentForm;
            lblPid.Text = pid;
            this.grade = parentForm.currentPlace.I_grade;

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

            foreach (var key in dicCtrl.Keys)
            {
                dicCtrl[key]["end"].Tag = key;

                //根据级别，限制可增加的层数
                if (key + grade > 7)
                {
                    SetDicEnable(dicCtrl[key], false);
                }
            }
        }

        private void SetDicEnable(Dictionary<string, Control> dic, bool enabled)
        {
            foreach (var item in dic.Values)
            {
                item.Enabled = enabled;
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //todo:add try
            bool isEnd;

            foreach (var key in dicCtrl.Keys)
            {
                if (dicCtrl[key]["num"].Enabled)
                {
                    isEnd = IsEnd(dicCtrl[key]);

                    if (key == 1)
                    {
                        if (!IsNotNull(dicCtrl[key]))
                        {
                            MessageBox.Show("请完善第1行数据！");
                            return;
                        }
                    }
                    else
                    {
                        if (HasInput(dicCtrl[key]))
                        {
                            if (!IsNotNull(dicCtrl[key]))
                            {
                                MessageBox.Show("请完善第" + key + "行数据！");
                                return;
                            }
                        }
                    }
                }
            }

            List<List<object>> list = new List<List<object>>();

            foreach (var item in dicCtrl.Values)
            {
                if (item["num"].Enabled && !string.IsNullOrEmpty(item["num"].Text.Trim()))
                {
                    if (checkInput(item))
                    {
                        list.Add(new List<object> { item["num"].Text.Trim(), item["name"].Text.Trim(), item["length"].Text.Trim(), item["width"].Text.Trim(), (int)item["end"].Tag == 7 ? 1 : ((CheckBox)item["end"]).Checked ? 1 : 0 });
                    }
                    else
                    {
                        return;
                    }
                }
            }

            if (bll.SaveList(list, pid, parentForm.currentPlace.I_grade))
            {
                parentForm.isAdd = true;

                MessageBox.Show("保存成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log.saveLog("设置货位成功！");
                Close();
            }
            else
            {
                MessageBox.Show("保存失败！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private bool HasInput(Dictionary<string, Control> dic)
        {
            return !string.IsNullOrEmpty(dic["num"].Text.Trim()) || !string.IsNullOrEmpty(dic["name"].Text.Trim()) || !string.IsNullOrEmpty(dic["width"].Text.Trim()) || !string.IsNullOrEmpty(dic["length"].Text.Trim()) || ((CheckBox)dic["end"]).Checked;
        }

        private bool IsEnd(Dictionary<string, Control> dic)
        {
            return ((CheckBox)dic["end"]).Checked;
        }

        private bool IsNotNull(Dictionary<string, Control> dic)
        {
            return !(string.IsNullOrEmpty(dic["num"].Text.Trim()) | string.IsNullOrEmpty(dic["name"].Text.Trim()));
        }

        private void cbEnd_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;

            if (hasEnd && !cb.Checked)
            {
                //页面可用性复原    
                hasEnd = false;
                foreach (var key in dicCtrl.Keys)
                {
                    if (key + grade <= 7)
                    {
                        SetDicEnable(dicCtrl[key], true);
                    }
                }
            }
            else
            {
                hasEnd = true;
                int level = (int)cb.Tag;
                foreach (var key in dicCtrl.Keys)
                {
                    if (key > level)
                    {
                        SetDicEnable(dicCtrl[key], false);
                        CheckBox cb1 = (CheckBox)dicCtrl[key]["end"];
                        cb1.CheckedChanged -= cbEnd_CheckedChanged;
                        cb1.Checked = false;
                        cb1.CheckedChanged += cbEnd_CheckedChanged;
                    }
                }
            }
        }

        /// <summary>
        /// 保存时验证用户输入是否合法
        /// </summary>
        /// <returns></returns>
        private bool checkInput(Dictionary<string, Control> item)
        {
            int num = 0;
            if (!int.TryParse(item["num"].Text.Trim(),out num))
            {
                MessageBox.Show("数量必须为整数！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            int len = 0;
            if (!(string.IsNullOrEmpty(item["length"].Text.Trim()) || int.TryParse(item["length"].Text.Trim(), out len)))
            {
                MessageBox.Show("长度必须为整数或为空！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            int wid = 0;
            if (!(string.IsNullOrEmpty(item["width"].Text.Trim()) || int.TryParse(item["width"].Text.Trim(), out wid)))
            {
                MessageBox.Show("宽度必须为整数或为空！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}
