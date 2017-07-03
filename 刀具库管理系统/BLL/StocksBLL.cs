using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using System.Data;
using Model;

namespace BLL
{
  public  class StocksBLL
    {
        StocksDAL dal = new StocksDAL(); 
        /// <summary>
        /// 获得全部库存信息
        /// </summary>
        /// <returns></returns>
        public DataTable getStocksList(string materiel, string materieName, string place, string stand, string userid)
        {
            return dal.getStocksList(materiel, materieName, place, stand, userid);
        }
       /// <summary>
       /// 获得全部货位信息
       /// </summary>
       /// <returns></returns>
        public DataTable queryStocksList(string id, string warehouse, string place, string mid)
        {
            return dal.queryStocksList(id, warehouse, place, mid);
        }
        /// <summary>
       /// 货位是否有货
       /// </summary>
       /// <param name="place"></param>
       /// <returns></returns>
        public bool isHaveGoods(string place)
        {
            return dal.isHaveGoods(place);
        }
        /// <summary>
       /// 货位是否有货
       /// </summary>
       /// <param name="place"></param>
       /// <returns></returns>
        public bool isHaveEmptyTray(string place)
        {
            return dal.isHaveEmptyTray(place);
        }
        /// <summary>
       /// 获得全部货位信息
       /// </summary>
       /// <returns></returns>
        public DataTable queryScrapList(string planid,string mid)
        {
            return dal.queryScrapList(planid,mid);
        }

        public DataTable queryOffLineStocksList(string planid,string mid)
        {
            return dal.queryOffLineStocksList(planid,mid);
        }

       /// <summary>
       /// 获得全部货位信息
       /// </summary>
       /// <returns></returns>
        public DataTable queryProcedureStocksList(string planid, string procedure)
        {
            return dal.queryProcedureStocksList(planid, procedure);
        }

       /// <summary>
       /// 获得全部货位信息
       /// </summary>
       /// <returns></returns>
        public DataTable queryBoardPriceList(string jia, string lie, string ceng, string mid)
        {
            return dal.queryBoardPriceList(jia, lie, ceng, mid);
        }
        public DataTable queryProductPriceList(string jia, string lie, string ceng, string mid, string dh)
        {
            return dal.queryProductPriceList(jia, lie, ceng, mid, dh);
        }
        public DataTable queryPlanPriceList(string mid, string dh, DateTime startDate, DateTime endDate)
        {
            return dal.queryPlanPriceList(mid, dh, startDate, endDate);
        }
    }
}
