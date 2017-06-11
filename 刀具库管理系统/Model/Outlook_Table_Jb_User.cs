using System;
using System.Collections.Generic;
using System.Text;
/*************
 * 2008.06.19
 *************/
namespace Model
{
    /// <summary>
    /// 用户菜单
    /// 该类是 数据库表 OUTLOOK_TABLE_JB_USER 的实体类
    /// 
    /// 该类用于生成主窗体(Mainform)的左侧功能列表
    /// </summary>
    public class Outlook_Table_Jb_User
    {
        private string itemname;
        /// <summary>
        /// 项目名——功能名
        /// </summary>
        public string Itemname
        {
            get { return itemname; }
            set { itemname = value; }
        }
        private string groupname;
        /// <summary>
        /// 组名
        /// </summary>
        public string Groupname
        {
            get { return groupname; }
            set { groupname = value; }
        }
        private string picname;
        /// <summary>
        /// 图片名
        /// </summary>
        public string Picname
        {
            get { return picname; }
            set { picname = value; }
        }

        private decimal trig_id;
        /// <summary>
        /// 顺序
        /// </summary>
        public decimal Trig_id
        {
            get { return trig_id; }
            set { trig_id = value; }
        }
        private string objecname;
        /// <summary>
        /// 用户对象名
        /// 点击之后要打开的窗体名称
        /// </summary>
        public string Objecname
        {
            get { return objecname; }
            set { objecname = value; }
        }

        private string ob_name;
        /// <summary>
        /// 对象类型
        /// </summary>
        public string Ob_name
        {
            get { return ob_name; }
            set { ob_name = value; }
        }

        private string jiase;
        /// <summary>
        ///  角色
        /// </summary>
        public string Jiase
        {
            get { return jiase; }
            set { jiase = value; }
        }
        private string yesno;
        /// <summary>
        /// 是否显示
        /// </summary>
        public string Yesno
        {
            get { return yesno; }
            set { yesno = value; }
        }
    }
}
