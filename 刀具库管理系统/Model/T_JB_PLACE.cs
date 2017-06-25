using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class T_JB_Place
    {      
        private string c_id;
        /// <summary>
        /// 编号
        /// </summary>
        public string C_id
        {
            get { return c_id; }
            set { c_id = value; }
        }
        private string c_name;
        /// <summary>
        /// 名称
        /// </summary>
        public string C_name
        {
            get { return c_name; }
            set { c_name = value; }
        }
        private string c_pre_id;
        /// <summary>
        /// 上级编码
        /// </summary>
        public string C_pre_id
        {
            get { return c_pre_id; }
            set { c_pre_id = value; }
        }
        private int i_grade;
        /// <summary>
        /// 级别
        /// </summary>
        public int I_grade
        {
            get { return i_grade; }
            set { i_grade = value; }
        }
        private int i_length;
        /// <summary>
        /// 长度
        /// </summary>
        public int I_length
        {
            get { return i_length; }
            set { i_length = value; }
        }
        private int i_width;
        /// <summary>
        /// 宽度
        /// </summary>
        public int I_width
        {
            get { return i_width; }
            set { i_width = value; }
        }
        private int i_inuse;
        /// <summary>
        /// 是否可用
        /// </summary>
        public int I_inuse
        {
            get { return i_inuse; }
            set { i_inuse = value; }
        }
        private int i_end;
        /// <summary>
        /// 是否末级
        /// </summary>
        public int I_end
        {
            get { return i_end; }
            set { i_end = value; }
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

        private int i_children;
        /// <summary>
        /// 备注
        /// </summary>
        public int I_children
        {
            get { return i_children; }
            set { i_children = value; }
        }
    }
}
