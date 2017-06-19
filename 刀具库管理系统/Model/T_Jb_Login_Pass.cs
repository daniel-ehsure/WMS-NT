using System;
using System.Collections.Generic;
using System.Text;
/**********************
 * 2008.06.20
 **********************/
namespace Model
{
    /// <summary>
    /// 系统操作员
    /// 该类是数据库表 T_JB_LOGIN_PASS 的实体类
    /// </summary>
    public class T_Jb_Login_Pass
    {
        private string c_loginID = string.Empty;
        /// <summary>
        /// 用户名称
        /// </summary>
        public string C_loginID
        {
            get { return c_loginID; }
            set { c_loginID = value; }
        }
        private DateTime d_startdate;
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime D_startdate
        {
            get { return d_startdate; }
            set { d_startdate = value; }
        }
        private DateTime d_enddate;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime D_enddate
        {
            get { return d_enddate; }
            set { d_enddate = value; }
        }
        private string c_password;
        /// <summary>
        /// 密码
        /// </summary>
        public string C_password
        {
            get { return c_password; }
            set { c_password = value; }
        }
        private string c_chinesename;
        /// <summary>
        /// 中文名称
        /// </summary>
        public string C_chinesename
        {
            get { return c_chinesename; }
            set { c_chinesename = value; }
        }
        private string c_jiaose;
        /// <summary>
        /// 角色
        /// </summary>
        public string C_jiaose
        {
            get { return c_jiaose; }
            set { c_jiaose = value; }
        }
        private string c_cc_id;
        /// <summary>
        /// 库房编码
        /// </summary>
        public string C_cc_id
        {
            get { return c_cc_id; }
            set { c_cc_id = value; }
        }
        private int i_state;
        /// <summary>
        /// 标志
        /// </summary>
        public int I_state
        {
            get { return i_state; }
            set { i_state = value; }
        }
    }
}
