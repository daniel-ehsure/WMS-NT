using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class T_JB_COMPONENT
    {
        private string _c_id;
        private string _c_name;
        private string _c_product_name;
        private int _i_count;
        private string _c_makings;
        private double _i_thick;
        private string _c_designer;
        private DateTime _d_date;
        private string _c_path;
        private double _d_acreage;
        private string _c_materiel_code;
        private string _c_customer_name;
        private string _c_version;
        private double _d_weight;
        private double _d_length;        
        private double _d_width;      
        private double _d_price;
        private int _i_Board;

        public int I_Board
        {
            get { return _i_Board; }
            set { _i_Board = value; }
        }

        private List<T_JB_COMPONENT_PROCEDURE> _procedures;

        public List<T_JB_COMPONENT_PROCEDURE> Procedures
        {
            get { return _procedures; }
            set { _procedures = value; }
        }
      

        


        /// <summary>
        /// 零件图号
        /// </summary>
        public string c_id
        {
            get
            {
                return this._c_id;
            }
            set
            {
                this._c_id = value;
            }
        }
        /// <summary>
        /// 零件名称
        /// </summary>
        public string c_name
        {
            get
            {
                return this._c_name;
            }
            set
            {
                this._c_name = value;
            }
        }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string c_product_name
        {
            get
            {
                return this._c_product_name;
            }
            set
            {
                this._c_product_name = value;
            }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public int i_count
        {
            get
            {
                return this._i_count;
            }
            set
            {
                this._i_count = value;
            }
        }
        /// <summary>
        /// 材料
        /// </summary>
        public string c_makings
        {
            get
            {
                return this._c_makings;
            }
            set
            {
                this._c_makings = value;
            }
        }
        /// <summary>
        /// 厚度
        /// </summary>
        public double i_thick
        {
            get
            {
                return this._i_thick;
            }
            set
            {
                this._i_thick = value;
            }
        }
        /// <summary>
        /// 设计人
        /// </summary>
        public string c_designer
        {
            get
            {
                return this._c_designer;
            }
            set
            {
                this._c_designer = value;
            }
        }
        /// <summary>
        /// 编制日期
        /// </summary>
        public DateTime d_date
        {
            get
            {
                return this._d_date;
            }
            set
            {
                this._d_date = value;
            }
        }
        /// <summary>
        /// 零件存放路径
        /// </summary>
        public string C_path
        {
            get { return _c_path; }
            set { _c_path = value; }
        }
        /// <summary>
        /// 面积
        /// </summary>
        public double D_acreage
        {
            get { return _d_acreage; }
            set { _d_acreage = value; }
        }
        public string c_materiel_code
        {
            get
            {
                return this._c_materiel_code;
            }
            set
            {
                this._c_materiel_code = value;
            }
        }

        public string c_customer_name
        {
            get
            {
                return this._c_customer_name;
            }
            set
            {
                this._c_customer_name = value;
            }
        }

        public string c_version
        {
            get
            {
                return this._c_version;
            }
            set
            {
                this._c_version = value;
            }
        }

        public double d_weight
        {
            get
            {
                return this._d_weight;
            }
            set
            {
                this._d_weight = value;
            }
        }

        public double D_length
        {
            get { return _d_length; }
            set { _d_length = value; }
        }
        public double D_width
        {
            get { return _d_width; }
            set { _d_width = value; }
        }
        public double D_price
        {
            get { return _d_price; }
            set { _d_price = value; }
        }
    }
}
