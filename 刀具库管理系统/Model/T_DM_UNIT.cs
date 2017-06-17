using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class T_DM_UNIT
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
        private string c_memo;
        /// <summary>
        /// 备注
        /// </summary>
        public string C_memo
        {
            get { return c_memo; }
            set { c_memo = value; }
        }
    }
}
