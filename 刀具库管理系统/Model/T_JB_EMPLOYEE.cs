using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class T_JB_EMPLOYEE
    {
        private string c_id;
       /// <summary>
       /// 编码
       /// </summary>
        public string C_id
        {
            get { return c_id; }
            set { c_id = value; }
        }


        private string c_name;
       /// <summary>
       /// 姓名
       /// </summary>
        public string C_name
        {
            get { return c_name; }
            set { c_name = value; }
        }

        private string c_unitId;
       /// <summary>
       /// 部门
       /// </summary>
        public string C_unitId
        {
            get { return c_unitId; }
            set { c_unitId = value; }
        }

        private string c_unitName;
       /// <summary>
       /// 部门名称
       /// </summary>
        public string C_unitName
        {
            get { return c_unitName; }
            set { c_unitName = value; }
        }

        private string c_gangwei;
       /// <summary>
       /// 岗位
       /// </summary>
        public string C_gangWei
        {
            get { return c_gangwei; }
            set { c_gangwei = value; }
        }

        private string c_memo;
       /// <summary>
       /// 备注
       /// </summary>
        public string C_memo
        {
            get { return c_memo; }
            set { c_memo = value; }
        }

        private string c_sex;
       /// <summary>
       /// 性别
       /// </summary>
        public string C_sex
        {
            get { return c_sex; }
            set { c_sex = value; }
        }

        private DateTime d_birthday;
       /// <summary>
       /// 生日
       /// </summary>
        public DateTime D_birthday
        {
            get { return d_birthday; }
            set { d_birthday = value; }
        }

        private string c_address;
       /// <summary>
       /// 地址
       /// </summary>
        public string C_address
        {
            get { return c_address; }
            set { c_address = value; }
        }

        private string c_office_tel;
        /// <summary>
        /// 办公电话
        /// </summary>
        public string C_office_tel
        {
            get { return c_office_tel; }
            set { c_office_tel = value; }
        }

        private string c_move_tel;
        /// <summary>
        /// 移动电话
        /// </summary>
        public string C_move_tel
        {
            get { return c_move_tel; }
            set { c_move_tel = value; }
        }
    }
}
