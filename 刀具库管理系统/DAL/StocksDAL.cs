using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.Common;
using Util;
using Model;
using DBCore;

namespace DAL
{
   public class StocksDAL
    {
       private DBHelper dbHelper = new SQLDBHelper();
        /// <summary>
        /// 获得全部库存信息
        /// </summary>
        /// <returns></returns>
       public DataTable getStocksList(string materiel,string materieName, string place, string stand,string userid)
        {
            string sql = @"select * from (
                            select a.C_MATERIEL_ID,b.C_NAME,c.C_NAME as C_TYPENAME,b.C_STANDARD,a.C_PLACE,a.DEC_COUNT,
                            a.DEC_COUNT -isnull( d.usecount,0) as canuse
                            from T_OPERATE_STOCKS a 
                            left join T_JB_MATERIEL b on a.C_MATERIEL_ID = b.C_ID
                            left join T_JB_TYPE c on b.C_TYPE = c.C_ID
                            left join  (select  isnull( DEC_COUNT,0) as usecount ,C_MATERIEL,C_PLACE from T_Runing_Dolist where I_INOUT = 1) d  on  d.C_MATERIEL = a.C_MATERIEL_ID
                            and d.C_PLACE = a.C_PLACE
                            ) g
                        where g.canuse >0 and g.C_PLACE not in (select C_PLACE from T_Runing_Dolist) and g.C_MATERIEL_ID in ( select C_MATERIEL from T_JB_MATERIEL_USER where JIAOSE = @JIAOSE  )  ";
            DataTable dt = new DataTable();
            try
            {

                Hashtable table = new Hashtable();
                table.Add("JIAOSE", userid);
                if (materiel != null && !(string.Empty.Equals(materiel)))
                {
                    sql += " and g.C_MATERIEL_ID like @materiel ";

                    table.Add("materiel", "%" + materiel + "%");
                }
                if (materieName != null && !(string.Empty.Equals(materieName)))
                {
                    sql += " and g.C_NAME like @materieName ";

                    table.Add("materieName", "%" + materieName + "%");
                }
                if (place != null && !(string.Empty.Equals(place)))
                {
                    sql += " and g.C_PLACE like @place  ";

                    table.Add("place", "%" + place + "%");
                }
                if (stand != null && !(string.Empty.Equals(stand)))
                {
                    sql += " and g.C_STANDARD like @stand  ";

                    table.Add("stand", "%" + stand + "%");
                }

                if (table.Count > 0)
                {
                    DbParameter[] parms = dbHelper.getParams(table);
                    dt = dbHelper.GetDataSet(sql, parms);
                }
                else
                {
                    dt = dbHelper.GetDataSet(sql);
                }

            }
            catch (Exception ex)
            {
                Log.write(ex.Message + "\r\n" + ex.StackTrace);
                throw ex;
            }
            finally
            {
                dbHelper.getConnection().Close();
            }
            return dt;

        }



       /// <summary>
       /// 获得全部货位信息
       /// </summary>
       /// <returns></returns>
       public DataTable queryStocksList(string planid, string jia, string lie, string ceng,string mid)
       {
           string sql = @" select a.C_DH ,a.C_MATERIEL_ID,b.c_name,a.DEC_COUNT,a.C_Tray,a.C_PLACE,a.c_people_id,d.c_name,a.D_END_TIME
                            from T_OPERATE_STOCKS a
                            left join T_JB_COMPONENT b on a.C_MATERIEL_ID = b.c_id 
                            left join T_JB_PROCEDURE d on a.c_Procedure = d.C_ID where 1=1";
                   //     where  g.C_MATERIEL_ID in ( select C_MATERIEL from T_JB_MATERIEL_USER where JIAOSE = @JIAOSE  )";


           DataTable dt = new DataTable();
           try
           {

               Hashtable table = new Hashtable();
               //table.Add("JIAOSE", userid);
               if (planid != null && !(string.Empty.Equals(planid)))
               {
                   sql += " and a.C_DH = @planid";

                   table.Add("planid",  planid );
               }


               if (jia != null && !(string.Empty.Equals(jia)))
               {
                   sql += " and a.C_PLACE like @jia";

                   table.Add("jia", jia + "____");
               }
               if (lie != null && !(string.Empty.Equals(lie)))
               {
                   sql += " and a.C_PLACE like @lie";

                   table.Add("lie", "__" + lie + "__");
               }
               if (ceng != null && !(string.Empty.Equals(ceng)))
               {
                   sql += " and a.C_PLACE like @ceng";

                   table.Add("ceng", "____" + ceng);
               }

               if (mid != null && !(string.Empty.Equals(mid)))
               {
                   sql += " and a.C_MATERIEL_ID = @mid";

                   table.Add("mid", mid);
               }

               if (table.Count > 0)
               {
                   DbParameter[] parms = dbHelper.getParams(table);
                   dt = dbHelper.GetDataSet(sql, parms);
               }
               else
               {
                   dt = dbHelper.GetDataSet(sql);
               }


           }
           catch (Exception ex)
           {
               Log.write(ex.Message + "\r\n" + ex.StackTrace);
               throw ex;
           }
           finally
           {
               dbHelper.getConnection().Close();
           }
           return dt;

       }
       /// <summary>
       /// 货位是否有货
       /// </summary>
       /// <param name="place"></param>
       /// <returns></returns>
       public bool isHaveGoods(string place)
       {
           string sql = "select count(*) from dbo.T_OPERATE_STOCKS where C_PLACE = '" + place + "' and C_MATERIEL_ID is not null";
           bool flag = false;
           try
           {
               object temp = dbHelper.GetScalar(sql);
               if (temp != null && !(DBNull.Value.Equals(temp)))
               {
                   int count = Convert.ToInt32(temp);
                   if (count > 0)
                   {
                       flag = true;
                   }
               }

           }
           catch (Exception ex)
           {
               Log.write(ex.Message + "\r\n" + ex.StackTrace);
               throw ex;
           }
           finally
           {
               dbHelper.getConnection().Close();
           }
           return flag;
       }

       /// <summary>
       /// 货位是否有货
       /// </summary>
       /// <param name="place"></param>
       /// <returns></returns>
       public bool isHaveEmptyTray(string place)
       {
           string sql = "select count(*) from dbo.T_OPERATE_STOCKS where C_PLACE = '" + place + "' and C_MATERIEL_ID is  null";
           bool flag = false;
           try
           {
               object temp = dbHelper.GetScalar(sql);
               if (temp != null && !(DBNull.Value.Equals(temp)))
               {
                   int count = Convert.ToInt32(temp);
                   if (count > 0)
                   {
                       flag = true;
                   }
               }

           }
           catch (Exception ex)
           {
               Log.write(ex.Message + "\r\n" + ex.StackTrace);
               throw ex;
           }
           finally
           {
               dbHelper.getConnection().Close();
           }
           return flag;
       }

       /// <summary>
       /// 获得全部货位信息
       /// </summary>
       /// <returns></returns>
       public DataTable queryScrapList(string planid,string mid)
       {
           string sql = @" select a.C_DH ,a.C_MATERIEL_ID,b.c_name,a.DEC_COUNT,a.c_people_id,d.c_name,a.D_END_TIME
                            from T_OPERATE_SCRAP a
                            left join T_JB_COMPONENT b on a.C_MATERIEL_ID = b.c_id 
                            left join T_JB_PROCEDURE d on a.c_Procedure = d.C_ID    where 1=1";
           //     where  g.C_MATERIEL_ID in ( select C_MATERIEL from T_JB_MATERIEL_USER where JIAOSE = @JIAOSE  )";


           DataTable dt = new DataTable();
           try
           {

               Hashtable table = new Hashtable();
               //table.Add("JIAOSE", userid);
               if (planid != null && !(string.Empty.Equals(planid)))
               {
                   sql += " and a.C_DH = @planid";

                   table.Add("planid", planid);
               }
               if (mid != null && !(string.Empty.Equals(mid)))
               {
                   sql += " and a.C_MATERIEL_ID = @mid";

                   table.Add("mid", mid);
               }


               sql += " order by a.D_END_TIME desc ";

               if (table.Count > 0)
               {
                   DbParameter[] parms = dbHelper.getParams(table);
                   dt = dbHelper.GetDataSet(sql, parms);
               }
               else
               {
                   dt = dbHelper.GetDataSet(sql);
               }


           }
           catch (Exception ex)
           {
               Log.write(ex.Message + "\r\n" + ex.StackTrace);
               throw ex;
           }
           finally
           {
               dbHelper.getConnection().Close();
           }
           return dt;

       }


       /// <summary>
       /// 获得全部货位信息
       /// </summary>
       /// <returns></returns>
       public DataTable queryOffLineStocksList(string planid,string mid)
       {
           string sql = @" select a.C_DH ,a.C_MATERIEL_ID,b.c_name,a.DEC_COUNT,a.C_Tray,a.C_PLACE,a.c_people_id,d.c_name,a.D_END_TIME
                            from T_OPERATE_OFFLINESTOCKS a
                            left join T_JB_COMPONENT b on a.C_MATERIEL_ID = b.c_id 
                            left join T_JB_PROCEDURE d on a.c_Procedure = d.C_ID where 1=1";
           //     where  g.C_MATERIEL_ID in ( select C_MATERIEL from T_JB_MATERIEL_USER where JIAOSE = @JIAOSE  )";


           DataTable dt = new DataTable();
           try
           {

               Hashtable table = new Hashtable();
               //table.Add("JIAOSE", userid);
               if (planid != null && !(string.Empty.Equals(planid)))
               {
                   sql += " and a.C_DH like @planid";

                   table.Add("planid", "%" + planid + "%");
               }
               if (mid != null && !(string.Empty.Equals(mid)))
               {
                   sql += " and a.C_MATERIEL_ID = @mid";

                   table.Add("mid", mid);
               }


               if (table.Count > 0)
               {
                   DbParameter[] parms = dbHelper.getParams(table);
                   dt = dbHelper.GetDataSet(sql, parms);
               }
               else
               {
                   dt = dbHelper.GetDataSet(sql);
               }


           }
           catch (Exception ex)
           {
               Log.write(ex.Message + "\r\n" + ex.StackTrace);
               throw ex;
           }
           finally
           {
               dbHelper.getConnection().Close();
           }
           return dt;

       }


       /// <summary>
       /// 获得全部货位信息
       /// </summary>
       /// <returns></returns>
       public DataTable queryProcedureStocksList(string planid, string procedure)
       {
           string psql = @" select a.C_DH ,a.C_MATERIEL_ID,b.c_name,a.DEC_COUNT,a.C_Tray,a.C_PLACE,a.c_people_id,d.c_name,a.D_END_TIME
                            from T_OPERATE_STOCKS a
                            left join T_JB_COMPONENT b on a.C_MATERIEL_ID = b.c_id 
                            left join T_JB_PROCEDURE d on a.c_Procedure = d.C_ID where a.c_Procedure is not null";
           //     where  g.C_MATERIEL_ID in ( select C_MATERIEL from T_JB_MATERIEL_USER where JIAOSE = @JIAOSE  )";
           string usql = @" union
                            select a.C_DH ,a.C_MATERIEL_ID,b.c_name,a.DEC_COUNT,a.C_Tray,a.C_PLACE,a.c_people_id,d.c_name,a.D_END_TIME
                            from T_OPERATE_OFFLINESTOCKS a
                            left join T_JB_COMPONENT b on a.C_MATERIEL_ID = b.c_id 
                            left join T_JB_PROCEDURE d on a.c_Procedure = d.C_ID where a.c_Procedure is not null ";
           string where = string.Empty;
           DataTable dt = new DataTable();
           try
           {

               Hashtable table = new Hashtable();
               //table.Add("JIAOSE", userid);
               if (planid != null && !(string.Empty.Equals(planid)))
               {
                   where += " and a.C_DH = @planid ";

                   table.Add("planid", planid);
               }


               if (procedure != null && !(string.Empty.Equals(procedure)))
               {
                   where += " and a.c_Procedure = @procedure ";

                   table.Add("procedure", procedure);
               }

               string sql = psql + where + usql + where;


               if (table.Count > 0)
               {
                   DbParameter[] parms = dbHelper.getParams(table);
                   dt = dbHelper.GetDataSet(sql, parms);
               }
               else
               {
                   dt = dbHelper.GetDataSet(sql);
               }


           }
           catch (Exception ex)
           {
               Log.write(ex.Message + "\r\n" + ex.StackTrace);
               throw ex;
           }
           finally
           {
               dbHelper.getConnection().Close();
           }
           return dt;

       }

       /// <summary>
       /// 获得全部货位信息
       /// </summary>
       /// <returns></returns>
       public DataTable queryBoardPriceList(string jia, string lie, string ceng, string mid)
       {
           string sql = @" select a.C_MATERIEL_ID,b.c_name,b.i_thick,b.d_length,b.d_width,b.d_acreage,
b.d_weight,b.d_price,a.DEC_COUNT,b.d_price*a.DEC_COUNT,a.C_Tray,a.C_PLACE,a.D_END_TIME
from T_OPERATE_STOCKS a,T_JB_COMPONENT b
where a.C_MATERIEL_ID = b.c_id and a.C_DH is null and a.c_people_id is null and a.c_Procedure is null ";
           string sql1 = @" select a.C_MATERIEL_ID,b.c_name,b.i_thick,b.d_length,b.d_width,b.d_acreage,
b.d_weight,b.d_price,a.DEC_COUNT,b.d_price*a.DEC_COUNT,a.C_Tray,a.C_PLACE,a.D_END_TIME
from T_OPERATE_OFFLINESTOCKS a,T_JB_COMPONENT b
where a.C_MATERIEL_ID = b.c_id and a.C_DH is null and a.c_people_id is null and a.c_Procedure is null ";


           //     where  g.C_MATERIEL_ID in ( select C_MATERIEL from T_JB_MATERIEL_USER where JIAOSE = @JIAOSE  )";


           DataTable dt = new DataTable();
           try
           {

               Hashtable table = new Hashtable();               

               if (jia != null && !(string.Empty.Equals(jia)))
               {
                   sql += " and a.C_PLACE like @jia";
                   sql1 += " and a.C_PLACE like @jia";
                   table.Add("jia", jia + "____");
               }
               if (lie != null && !(string.Empty.Equals(lie)))
               {
                   sql += " and a.C_PLACE like @lie";
                   sql1 += " and a.C_PLACE like @lie";
                   table.Add("lie", "__" + lie + "__");
               }
               if (ceng != null && !(string.Empty.Equals(ceng)))
               {
                   sql += " and a.C_PLACE like @ceng";
                   sql1 += " and a.C_PLACE like @ceng";
                   table.Add("ceng", "____" + ceng);
               }

               if (mid != null && !(string.Empty.Equals(mid)))
               {
                   sql += " and a.C_MATERIEL_ID = @mid";
                   sql1 += " and a.C_MATERIEL_ID = @mid";
                   table.Add("mid", mid);
               }
               string sql2 = sql + " union " + sql1;
               if (table.Count > 0)
               {
                   DbParameter[] parms = dbHelper.getParams(table);
                   dt = dbHelper.GetDataSet(sql2, parms);
               }
               else
               {
                   dt = dbHelper.GetDataSet(sql2);
               }


           }
           catch (Exception ex)
           {
               Log.write(ex.Message + "\r\n" + ex.StackTrace);
               throw ex;
           }
           finally
           {
               dbHelper.getConnection().Close();
           }
           return dt;

       }

       public DataTable queryProductPriceList(string jia, string lie, string ceng, string mid,string dh)
       {
           string sql = @" select a.C_DH,a.C_MATERIEL_ID,b.c_name,b.i_thick,b.d_length,b.d_width,b.d_acreage,
b.d_weight,b.d_price,a.DEC_COUNT,b.d_price*a.DEC_COUNT,a.c_people_id,a.C_Tray,a.C_PLACE,a.D_END_TIME
from T_OPERATE_STOCKS a,T_JB_COMPONENT b
where a.C_MATERIEL_ID = b.c_id and a.c_Procedure ='9999' ";
           string sql1 = @" select a.C_DH,a.C_MATERIEL_ID,b.c_name,b.i_thick,b.d_length,b.d_width,b.d_acreage,
b.d_weight,b.d_price,a.DEC_COUNT,b.d_price*a.DEC_COUNT,a.c_people_id,a.C_Tray,a.C_PLACE,a.D_END_TIME
from T_OPERATE_OFFLINESTOCKS a,T_JB_COMPONENT b
where a.C_MATERIEL_ID = b.c_id and a.c_Procedure ='9999' ";


           //     where  g.C_MATERIEL_ID in ( select C_MATERIEL from T_JB_MATERIEL_USER where JIAOSE = @JIAOSE  )";


           DataTable dt = new DataTable();
           try
           {

               Hashtable table = new Hashtable();

               if (jia != null && !(string.Empty.Equals(jia)))
               {
                   sql += " and a.C_PLACE like @jia";
                   sql1 += " and a.C_PLACE like @jia";
                   table.Add("jia", jia + "____");
               }
               if (lie != null && !(string.Empty.Equals(lie)))
               {
                   sql += " and a.C_PLACE like @lie";
                   sql1 += " and a.C_PLACE like @lie";
                   table.Add("lie", "__" + lie + "__");
               }
               if (ceng != null && !(string.Empty.Equals(ceng)))
               {
                   sql += " and a.C_PLACE like @ceng";
                   sql1 += " and a.C_PLACE like @ceng";
                   table.Add("ceng", "____" + ceng);
               }

               if (mid != null && !(string.Empty.Equals(mid)))
               {
                   sql += " and a.C_MATERIEL_ID = @mid";
                   sql1 += " and a.C_MATERIEL_ID = @mid";
                   table.Add("mid", mid);
               }
               if (dh != null && !(string.Empty.Equals(dh)))
               {
                   sql += " and a.C_DH = @dh";
                   sql1 += " and a.C_DH = @dh";
                   table.Add("dh", dh);
               }
               string sql2 = sql + " union " + sql1;
               if (table.Count > 0)
               {
                   DbParameter[] parms = dbHelper.getParams(table);
                   dt = dbHelper.GetDataSet(sql2, parms);
               }
               else
               {
                   dt = dbHelper.GetDataSet(sql2);
               }


           }
           catch (Exception ex)
           {
               Log.write(ex.Message + "\r\n" + ex.StackTrace);
               throw ex;
           }
           finally
           {
               dbHelper.getConnection().Close();
           }
           return dt;

       }


       public DataTable queryPlanPriceList(string mid, string dh, DateTime startDate, DateTime endDate)
       {
           string sql = @" select a.C_ID,C_PRODUCT_ID,b.c_name,a.I_COUNT,b.i_count,b.d_price,a.I_COUNT*b.i_count,b.d_price*a.I_COUNT*b.i_count,
CONVERT(varchar(12) ,  a.D_DATE, 111 ) as D_DATE , CONVERT(varchar(12) ,  a.D_PLAN_DATE, 111 ) as D_PLAN_DATE                   
from T_OPERATE_PRODUCE_PLAN a,T_JB_COMPONENT b
 where a.C_PRODUCT_ID = b.c_id  ";


           //     where  g.C_MATERIEL_ID in ( select C_MATERIEL from T_JB_MATERIEL_USER where JIAOSE = @JIAOSE  )";


           DataTable dt = new DataTable();
           try
           {

               Hashtable table = new Hashtable();
               if (string.IsNullOrEmpty(mid) && string.IsNullOrEmpty(dh))
               {
                   if (startDate != Global.minValue && endDate != Global.minValue)
                   {
                       sql += " and  convert(datetime, CONVERT(varchar(100), D_PLAN_DATE, 23),120) between @startDate and @endDate";


                       table.Add("startDate", startDate);
                       table.Add("endDate", endDate);
                   }
               }
               else
               {

                   if (mid != null && !(string.Empty.Equals(mid)))
                   {
                       sql += " and a.C_PRODUCT_ID = @mid";
                       table.Add("mid", mid);
                   }
                   if (dh != null && !(string.Empty.Equals(dh)))
                   {
                       sql += " and a.C_ID = @dh";
                       table.Add("dh", dh);
                   }
               }
               sql += "  order by a.C_ID desc ";
               if (table.Count > 0)
               {
                   DbParameter[] parms = dbHelper.getParams(table);
                   dt = dbHelper.GetDataSet(sql, parms);
               }
               else
               {
                   dt = dbHelper.GetDataSet(sql);
               }


           }
           catch (Exception ex)
           {
               Log.write(ex.Message + "\r\n" + ex.StackTrace);
               throw ex;
           }
           finally
           {
               dbHelper.getConnection().Close();
           }
           return dt;

       }
    }
}
