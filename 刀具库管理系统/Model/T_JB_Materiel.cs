﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
   public class T_JB_Materiel
    {
        private string c_id;
       /// <summary>
       /// 物料编码
       /// </summary>
        public string C_id
        {
            get { return c_id; }
            set { c_id = value; }
        }


        private string c_name;
       /// <summary>
       /// 物料名称
       /// </summary>
        public string C_name
        {
            get { return c_name; }
            set { c_name = value; }
        }
        private decimal i_thick;
       /// <summary>
       /// 物料厚度（毫米）
       /// </summary>
        public decimal I_thick
        {
            get { return i_thick; }
            set { i_thick = value; }
        }
        private int i_single;
       /// <summary>
       /// 物料存取方式 0 整盘存取，1单件存取
       /// </summary>
        public int I_single
        {
            get { return i_single; }
            set { i_single = value; }
        }
        private string c_standerd;
       /// <summary>
       /// 规格型号
       /// </summary>
        public string C_standerd
        {
            get { return c_standerd; }
            set { c_standerd = value; }
        }
        private decimal i_length;
       /// <summary>
        /// 物料长度（毫米）
       /// </summary>
        public decimal I_length
        {
            get { return i_length; }
            set { i_length = value; }
        }
        private decimal i_width;
       /// <summary>
        /// 物料宽度（毫米）
       /// </summary>
        public decimal I_width
        {
            get { return i_width; }
            set { i_width = value; }
        }

        private string c_type;
       /// <summary>
       /// 物料类型
       /// </summary>
        public string C_type
        {
            get { return c_type; }
            set { c_type = value; }
        }
        private string c_typeName;
       /// <summary>
       /// 物料类型名称
       /// </summary>
        public string C_typeName
        {
            get { return c_typeName; }
            set { c_typeName = value; }
        }
        private string c_area;
       /// <summary>
       /// 货区
       /// </summary>
        public string C_area
        {
            get { return c_area; }
            set { c_area = value; }
        }

        private int i_finish;
       /// <summary>
       /// 是否为成品
       /// </summary>
        public int I_finish
        {
            get { return i_finish; }
            set { i_finish = value; }
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

        private string c_piccode;
       /// <summary>
       /// 图号
       /// </summary>
        public string C_piccode
        {
            get { return c_piccode; }
            set { c_piccode = value; }
        }

        private int i_layOutCount;
       /// <summary>
       /// 单套用量
       /// </summary>
        public int I_layOutCount
        {
            get { return i_layOutCount; }
            set { i_layOutCount = value; }
        }

        private string c_surface;
       /// <summary>
       /// 表面处理
       /// </summary>
        public string C_surface
        {
            get { return c_surface; }
            set { c_surface = value; }
        }

        private string c_Science;
       /// <summary>
       /// 材料
       /// </summary>
        public string C_Science
        {
            get { return c_Science; }
            set { c_Science = value; }
        }

        private decimal dec_area;
       /// <summary>
       /// 面积
       /// </summary>
        public decimal Dec_area
        {
            get { return dec_area; }
            set { dec_area = value; }
        }
        private decimal dec_weight;
       /// <summary>
       /// 重量
       /// </summary>
        public decimal Dec_weight
        {
            get { return dec_weight; }
            set { dec_weight = value; }
        }

        private int i_buy;
       /// <summary>
       /// 是否外购
       /// </summary>
        public int I_buy
        {
            get { return i_buy; }
            set { i_buy = value; }
        }

        private decimal dec_production;
       /// <summary>
       /// 生产量
       /// </summary>
        public decimal Dec_production
        {
            get { return dec_production; }
            set { dec_production = value; }
        }
    }
}
