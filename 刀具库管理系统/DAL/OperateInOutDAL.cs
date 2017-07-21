using System;
using System.Collections.Generic;
using System.Text;
using Util;
using System.Collections;
using System.Data.Common;
using Model;
using System.Data;
using DBCore;

namespace DAL
{
    public class OperateInOutDAL
    {
        private DBHelper dbHelper = new SQLDBHelper();
        /// <summary>
        /// 获得新出入库单号
        /// </summary>     
        /// <returns></returns>
        public string getInOutID(int inOutType)
        {
            string id = null;
            int count = 0;
            string sql = "select  max( CONVERT(int,C_ID)) from T_OPERATE_INOUT where I_INOUT = @inOutType";
            try
            {
                Hashtable table = new Hashtable();
                table.Add("inOutType", inOutType);
                DbParameter[] parms = dbHelper.getParams(table);
                object temp = dbHelper.GetScalar(sql, parms);
                if (temp != null && !(DBNull.Value.Equals(temp)))
                {
                    count = Convert.ToInt32(temp);
                }
                if (inOutType == 2)
                {
                    sql = "select  max( CONVERT(int,C_DH)) from T_Runing_Dolist where I_INOUT =2";
                    int count2 = 0;
                    object temp2 = dbHelper.GetScalar(sql);
                    if (temp2 != null && !(DBNull.Value.Equals(temp2)))
                    {
                        count2 = Convert.ToInt32(temp2);
                    }
                    if (count2 > count)
                    {
                        count = count2;
                    }
                }
                if (count > 0)
                {

                    id = (count + 1).ToString();
                }
                else
                {
                    id = "1001";
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
            return id;
        }





        /// <summary>
        /// 保存出入库信息
        /// </summary>
        /// <param name="user">货位信息</param>
        /// <returns></returns>
        public bool save(int inOutType, string c_id, DateTime rq, string materiel, string place, int count, string czy, string meno)
        {
            int result = 0;
            DbConnection conn = dbHelper.getConnection();
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                Log.write(ex.Message + "\r\n" + ex.StackTrace);
                conn.Close();
                throw ex;
            }
            DbTransaction tran = conn.BeginTransaction();
            DbCommand com = conn.CreateCommand();
            string sql = string.Empty;
            try
            {
                com.Transaction = tran;
                sql = "INSERT INTO [T_OPERATE_INOUT]([C_ID], [D_RQ], [I_INOUT], [C_MATERIEL], [C_PLACE], [DEC_COUNT], [C_CZY], [C_MEMO]) " +
                     "  VALUES(@C_ID, @D_RQ, @I_INOUT, @C_MATERIEL, @C_PLACE, @DEC_COUNT, @C_CZY, @C_MEMO)";
                com.CommandText = sql;
                Hashtable table = new Hashtable();
                table.Add("C_ID", c_id);
                table.Add("D_RQ", rq);
                table.Add("I_INOUT", inOutType);
                table.Add("C_MATERIEL", materiel);
                table.Add("C_PLACE", place);
                table.Add("DEC_COUNT", count);
                table.Add("C_CZY", czy);
                table.Add("C_MEMO", meno);

                DbParameter[] parms = dbHelper.getParams(table);
                com.Parameters.Clear();
                com.Parameters.AddRange(parms);
                com.ExecuteNonQuery();
                if (inOutType == 2) //入库
                {


                    sql = "INSERT INTO [T_OPERATE_STOCKS]([C_MATERIEL_ID], [C_PLACE], [DEC_COUNT], [D_END_TIME])  VALUES (@C_MATERIEL_ID, @C_PLACE, @DEC_COUNT, @D_END_TIME)";
                    com.CommandText = sql;
                    Hashtable table2 = new Hashtable();
                    table2.Add("C_MATERIEL_ID", materiel);
                    table2.Add("C_PLACE", place);
                    table2.Add("DEC_COUNT", count);
                    table2.Add("D_END_TIME", rq);

                    DbParameter[] parms2 = dbHelper.getParams(table2);
                    com.Parameters.Clear();
                    com.Parameters.AddRange(parms2);
                    result = com.ExecuteNonQuery();
                }
                else
                {
                    sql = "UPDATE [T_OPERATE_STOCKS] SET  [DEC_COUNT]=[DEC_COUNT] - @DEC_COUNT where [C_MATERIEL_ID] = @C_MATERIEL_ID and [C_PLACE] = @C_PLACE";
                    com.CommandText = sql;
                    Hashtable table2 = new Hashtable();
                    table2.Add("C_MATERIEL_ID", materiel);
                    table2.Add("C_PLACE", place);
                    table2.Add("DEC_COUNT", count);

                    DbParameter[] parms2 = dbHelper.getParams(table2);
                    com.Parameters.Clear();
                    com.Parameters.AddRange(parms2);
                    result = com.ExecuteNonQuery();

                    sql = "DELETE FROM  [T_OPERATE_STOCKS] where [DEC_COUNT] <=0";
                    com.CommandText = sql;
                    com.ExecuteNonQuery();
                }

                tran.Commit();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                conn.Close();
                Log.write(ex.Message + "\r\n" + ex.StackTrace);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 获得出入库明细
        /// </summary>
        /// <param name="materile"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="inout"></param>
        /// <returns></returns>
        public DataTable getList(DateTime startDate, DateTime endDate, string inOutId, int inout, string mid)
        {
            string sql = @"select * from ( select a.C_ID,case a.C_CRK_LEIBIE when 1  then '物料出库' when 2 then '物料入库' 
                            when 3 then '刀具出库' when 4 then '刀具入库' else '出库' end as c_inout,a.C_MATERIEL,
                             c.c_name,a.DEC_COUNT,a.C_PLACE,CONVERT(varchar(12) ,  b.D_RQ, 111 ) as D_RQ
                             from T_OPERATE_INOUT_SUB a 
                            left join T_OPERATE_INOUT_MAIN b on a.C_ID = b.C_ID and a.C_CRK_LEIBIE =b.C_CRK_LEIBIE
                            left join T_JB_Materiel c on a.C_MATERIEL = c.C_ID ";
            if (inout == 2)
            {
                sql += " where (a.C_CRK_LEIBIE  = 2 or  a.C_CRK_LEIBIE = 4) ";
            }
            else
            {
                sql += " where (a.C_CRK_LEIBIE  = 1 or  a.C_CRK_LEIBIE = 3) ";
            }
            if (mid != null && !(string.Empty.Equals(mid)))
            {
                sql += " and a.C_MATERIEL like '%" + mid + "%'";

            }
            sql += "   ) h where 1=1";
            DataTable dt = new DataTable();
            try
            {

                Hashtable table = new Hashtable();

                if (startDate != Global.minValue && endDate != Global.minValue)
                {
                    sql += " and  convert(datetime, CONVERT(varchar(100), D_RQ, 23),120) between @startDate and @endDate";


                    table.Add("startDate", startDate);
                    table.Add("endDate", endDate);
                }
                if (!(string.IsNullOrEmpty(inOutId)))
                {
                    sql += " and  C_ID like @C_ID";

                    table.Add("C_ID", "%" + inOutId + "%");
                }

                sql += " order by D_RQ desc";
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
        /// 托盘是否被使用
        /// </summary>
        /// <param name="place"></param>
        /// <returns></returns>
        public bool isTrayInuse(string tray)
        {
            bool flag = false;
            string sql = "select count (*) from T_Runing_Dolist where C_Tray = @tray";
            Hashtable table = new Hashtable();
            table.Add("tray", tray);

            DbParameter[] parms1 = dbHelper.getParams(table);
            object obj1 = dbHelper.GetScalar(sql, parms1);
            if (obj1 != null && !(DBNull.Value.Equals(obj1)) && Convert.ToInt32(obj1) > 0)
            {
                return true;
            }
            else
            {
                sql = "select count (*) from T_OPERATE_STOCKS where C_Tray = @tray";
                DbParameter[] parms2 = dbHelper.getParams(table);
                object obj2 = dbHelper.GetScalar(sql, parms2);
                if (obj2 != null && !(DBNull.Value.Equals(obj2)) && Convert.ToInt32(obj2) > 0)
                {
                    return true;
                }
            }
            return flag;

        }

        /// <summary>
        /// 货位是否被使用
        /// </summary>
        /// <param name="place"></param>
        /// <returns></returns>
        public bool isPlaceInuse(string place)
        {
            bool flag = false;
            string sql = " select count (*) from T_JB_PLACE where C_ID = @place ";
            Hashtable table = new Hashtable();
            table.Add("place", place);
            DbParameter[] parms = dbHelper.getParams(table);
            object obj = dbHelper.GetScalar(sql, parms);
            if (obj == null || DBNull.Value.Equals(obj) || Convert.ToInt32(obj) == 0)
            {
                return true;
            }
            else
            {
                sql = "select count (*) from T_Runing_Dolist where C_PLACE = @place";
                DbParameter[] parms1 = dbHelper.getParams(table);
                object obj1 = dbHelper.GetScalar(sql, parms1);
                if (obj1 != null && !(DBNull.Value.Equals(obj1)) && Convert.ToInt32(obj1) > 0)
                {
                    return true;
                }
                else
                {
                    sql = "select count (*) from T_OPERATE_STOCKS where C_PLACE = @place";
                    DbParameter[] parms2 = dbHelper.getParams(table);
                    object obj2 = dbHelper.GetScalar(sql, parms2);
                    if (obj2 != null && !(DBNull.Value.Equals(obj2)) && Convert.ToInt32(obj2) > 0)
                    {
                        return true;
                    }
                }
            }
            return flag;

        }

        /// <summary>
        /// 获得物料的详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T_JB_Materiel getMaterielByIdOrName(string id)
        {
            T_JB_Materiel materiel = null;
            string sql = " SELECT a.[C_ID], a.[C_NAME], a.[I_THICK], a.[I_SINGLE], a.[C_STANDARD], a.[I_LENGTH], a.[I_WIDTH],b.C_NAME as C_TYPE_NAME " +
                        " FROM [T_JB_MATERIEL] a left join T_JB_TYPE b ON a.C_TYPE = b.C_ID  where a.C_ID = '" + id + "' or a.C_NAME= '" + id + "'";
            try
            {
                DataTable dt = dbHelper.GetDataSet(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    materiel = new T_JB_Materiel();
                    materiel.C_id = dt.Rows[0]["C_ID"].ToString();
                    materiel.C_name = dt.Rows[0]["C_NAME"].ToString();
                    materiel.I_thick = Convert.ToInt32(dt.Rows[0]["I_THICK"]);
                    materiel.I_single = Convert.ToInt32(dt.Rows[0]["I_SINGLE"]);
                    materiel.C_typeName = dt.Rows[0]["C_TYPE_NAME"].ToString();
                    object temp = dt.Rows[0]["C_STANDARD"];
                    if (temp == null || DBNull.Value.Equals(temp))
                    {
                        materiel.C_standerd = string.Empty;
                    }
                    else
                    {
                        materiel.C_standerd = dt.Rows[0]["C_STANDARD"].ToString();
                    }

                    materiel.I_length = Convert.ToInt32(dt.Rows[0]["I_LENGTH"]);
                    materiel.I_width = Convert.ToInt32(dt.Rows[0]["I_WIDTH"]);
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
            return materiel;
        }



        /// <summary>
        /// 手工入库
        /// </summary>
        /// <returns></returns>
        public string HandIn(DataTable dt, string mainMeno, InOutType type)
        {
            string dh;

            DateTime dtNow = DateTime.Now;
            int result = 0;
            DbConnection conn = dbHelper.getConnection();

            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                Log.write(ex.Message + "\r\n" + ex.StackTrace);
                conn.Close();
                throw ex;
            }

            DbTransaction tran = conn.BeginTransaction();
            DbCommand com = conn.CreateCommand();
            string sql = string.Empty;

            try
            {
                com.Transaction = tran;


                long dec_id = 0;
                string c_id = string.Empty;
                int count = 0;

                sql = "SELECT MAX(c_id) FROM T_OPERATE_INOUT_MAIN where datediff(day,[D_TIME],getdate()) = 0 AND C_CRK_LEIBIE = '" + (int)type + "'";

                com.CommandText = sql;
                object obj = com.ExecuteScalar();
                dec_id = Convert.IsDBNull(obj) ? 0 : Convert.ToInt64(obj.ToString().Substring(10));
                c_id = Common.GetInOutCode(type) + dtNow.ToString("yyyyMMdd") + (dec_id + 1).ToString().PadLeft(6, '0');
                dh = c_id;

                if (dt.Rows.Count > 0)
                {
                    sql = "INSERT INTO [T_OPERATE_INOUT_MAIN]([C_ID], [D_RQ], [C_CRK_LEIBIE],  [C_CZY], [C_MEMO], [D_TIME]) VALUES(@C_ID, @D_RQ, @C_CRK_LEIBIE,  @C_CZY,  @C_MEMO, @D_TIME)";
                    com.CommandText = sql;
                    Hashtable table = new Hashtable();

                    table.Add("C_ID", c_id);
                    table.Add("D_RQ", dt.Rows[0][5]);
                    table.Add("C_CZY", dt.Rows[0][6]);
                    table.Add("C_CRK_LEIBIE", (int)type);
                    table.Add("D_TIME", dtNow);

                    if (mainMeno == null || string.Empty.Equals(mainMeno.Trim()))
                    {
                        table.Add("C_MEMO", DBNull.Value);
                    }
                    else
                    {
                        table.Add("C_MEMO", mainMeno);
                    }

                    DbParameter[] parms = dbHelper.getParams(table);
                    com.Parameters.Clear();
                    com.Parameters.AddRange(parms);
                    com.ExecuteNonQuery();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sql = "INSERT INTO [T_OPERATE_INOUT_SUB]([C_ID], [C_CRK_LEIBIE],[C_MATERIEL], [C_PLACE], [DEC_COUNT]) " +
                             "  VALUES(@C_ID,@C_CRK_LEIBIE, @C_MATERIEL, @C_PLACE, @DEC_COUNT)";
                        com.CommandText = sql;
                        Hashtable table2 = new Hashtable();
                        table2.Add("C_ID", c_id);
                        table2.Add("C_CRK_LEIBIE", (int)type);
                        table2.Add("C_MATERIEL", dt.Rows[i][0]);
                        table2.Add("C_PLACE", dt.Rows[i][4]);
                        table2.Add("DEC_COUNT", dt.Rows[i][3]);

                        DbParameter[] parms2 = dbHelper.getParams(table2);
                        com.Parameters.Clear();
                        com.Parameters.AddRange(parms2);
                        com.ExecuteNonQuery();

                        //判断货位原来是否有同样零件
                        sql = "SELECT count(*) FROM T_OPERATE_STOCKS where [C_MATERIEL_ID] = @C_MATERIEL_ID and [C_PLACE] = @C_PLACE";
                        com.CommandText = sql;
                        Hashtable table4 = new Hashtable();
                        table4.Add("C_MATERIEL_ID", dt.Rows[i][0]);
                        table4.Add("C_PLACE", dt.Rows[i][4]);
                        DbParameter[] parms4 = dbHelper.getParams(table4);
                        com.Parameters.Clear();
                        com.Parameters.AddRange(parms4);
                        int stockCount = int.Parse(com.ExecuteScalar().ToString());

                        if (stockCount > 0)
                        {//有同样零件
                            sql = "UPDATE T_OPERATE_STOCKS SET [DEC_COUNT] = [DEC_COUNT] + @DEC_COUNT, [D_END_TIME] = @D_END_TIME, [C_DH] = @C_DH where [C_MATERIEL_ID] = @C_MATERIEL_ID and [C_PLACE] = @C_PLACE ";
                            com.CommandText = sql;
                            Hashtable table3 = new Hashtable();
                            table3.Add("C_MATERIEL_ID", dt.Rows[i][0]);
                            table3.Add("C_PLACE", dt.Rows[i][4]);
                            table3.Add("DEC_COUNT", dt.Rows[i][3]);
                            table3.Add("D_END_TIME", dt.Rows[i][5]);
                            table3.Add("C_DH", c_id);

                            DbParameter[] parms3 = dbHelper.getParams(table3);
                            com.Parameters.Clear();
                            com.Parameters.AddRange(parms3);
                            result = com.ExecuteNonQuery();
                        }
                        else
                        {//无同样零件
                            sql = "INSERT INTO [T_OPERATE_STOCKS]([C_MATERIEL_ID], [C_PLACE], [DEC_COUNT], [D_END_TIME], [C_DH])  VALUES (@C_MATERIEL_ID, @C_PLACE, @DEC_COUNT, @D_END_TIME, @C_DH)";
                            com.CommandText = sql;
                            Hashtable table3 = new Hashtable();
                            table3.Add("C_MATERIEL_ID", dt.Rows[i][0]);
                            table3.Add("C_PLACE", dt.Rows[i][4]);
                            table3.Add("DEC_COUNT", dt.Rows[i][3]);
                            table3.Add("D_END_TIME", dt.Rows[i][5]);
                            table3.Add("C_DH", c_id);

                            DbParameter[] parms3 = dbHelper.getParams(table3);
                            com.Parameters.Clear();
                            com.Parameters.AddRange(parms3);
                            result = com.ExecuteNonQuery();
                        }
                    }
                }

                tran.Commit();
                if (result > 0)
                {
                    return dh;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                conn.Close();
                Log.write(ex.Message + "\r\n" + ex.StackTrace);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }


        /// <summary>
        /// 手工出库
        /// </summary>
        /// <param name="user">货位信息</param>
        /// <returns></returns>
        public string handOut(DataTable dt, string mainMeno, InOutType type)
        {
            string dh;
            DateTime dtNow = DateTime.Now;
            int result = 0;
            DbConnection conn = dbHelper.getConnection();
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                Log.write(ex.Message + "\r\n" + ex.StackTrace);
                conn.Close();
                throw ex;
            }
            DbTransaction tran = conn.BeginTransaction();
            DbCommand com = conn.CreateCommand();
            string sql = string.Empty;
            try
            {
                com.Transaction = tran;


                long dec_id = 0;
                string c_id = string.Empty;
                int count = 0;

                sql = "SELECT MAX(c_id) FROM T_OPERATE_INOUT_MAIN where datediff(day,[D_TIME],getdate()) = 0 AND C_CRK_LEIBIE = '" + (int)type + "'";

                com.CommandText = sql;
                object obj = com.ExecuteScalar();
                dec_id = Convert.IsDBNull(obj) ? 0 : Convert.ToInt64(obj.ToString().Substring(10));
                c_id = Common.GetInOutCode(type) + dtNow.ToString("yyyyMMdd") + (dec_id + 1).ToString().PadLeft(6, '0');
                dh = c_id;

                if (dt.Rows.Count > 0)
                {
                    sql = "INSERT INTO [T_OPERATE_INOUT_MAIN]([C_ID], [D_RQ], [C_CRK_LEIBIE],  [C_CZY], [C_MEMO], [D_TIME]) VALUES(@C_ID, @D_RQ, @C_CRK_LEIBIE,  @C_CZY,  @C_MEMO, @D_TIME)";
                    com.CommandText = sql;
                    Hashtable table = new Hashtable();

                    table.Add("C_ID", c_id);
                    table.Add("D_RQ", dt.Rows[0][5]);
                    table.Add("C_CZY", dt.Rows[0][6]);
                    table.Add("C_CRK_LEIBIE", (int)type);
                    table.Add("D_TIME", dtNow);

                    if (mainMeno == null || string.Empty.Equals(mainMeno.Trim()))
                    {
                        table.Add("C_MEMO", DBNull.Value);
                    }
                    else
                    {
                        table.Add("C_MEMO", mainMeno);
                    }

                    DbParameter[] parms = dbHelper.getParams(table);
                    com.Parameters.Clear();
                    com.Parameters.AddRange(parms);
                    com.ExecuteNonQuery();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sql = "INSERT INTO [T_OPERATE_INOUT_SUB]([C_ID], [C_CRK_LEIBIE],[C_MATERIEL], [C_PLACE], [DEC_COUNT], [I_FLAG], [C_MACHINE]) " +
                             "  VALUES(@C_ID,@C_CRK_LEIBIE, @C_MATERIEL, @C_PLACE, @DEC_COUNT,  @I_FLAG, @C_MACHINE)";
                        com.CommandText = sql;
                        Hashtable table2 = new Hashtable();
                        table2.Add("C_ID", c_id);
                        table2.Add("C_CRK_LEIBIE", (int)type);
                        table2.Add("C_MATERIEL", dt.Rows[i][0]);
                        table2.Add("C_PLACE", dt.Rows[i][4]);
                        table2.Add("DEC_COUNT", dt.Rows[i][3]);

                        if (type.Equals(InOutType.KNIFE_OUT_USE))
                        {
                            table2.Add("I_FLAG", 1);
                            table2.Add("C_MACHINE", dt.Rows[0][7]);
                        }
                        else
                        {
                            table2.Add("I_FLAG", 0);
                            table2.Add("C_MACHINE", DBNull.Value);
                        }

                        DbParameter[] parms2 = dbHelper.getParams(table2);
                        com.Parameters.Clear();
                        com.Parameters.AddRange(parms2);
                        com.ExecuteNonQuery();

                        sql = "UPDATE [T_OPERATE_STOCKS] SET  [DEC_COUNT]=[DEC_COUNT] - @DEC_COUNT where [C_MATERIEL_ID] = @C_MATERIEL_ID and [C_PLACE] = @C_PLACE";
                        com.CommandText = sql;
                        Hashtable table3 = new Hashtable();
                        table3.Add("C_MATERIEL_ID", dt.Rows[i][0]);
                        table3.Add("C_PLACE", dt.Rows[i][4]);
                        table3.Add("DEC_COUNT", dt.Rows[i][3]);

                        DbParameter[] parms3 = dbHelper.getParams(table3);
                        com.Parameters.Clear();
                        com.Parameters.AddRange(parms3);
                        result = com.ExecuteNonQuery();

                        sql = "DELETE FROM  [T_OPERATE_STOCKS] where [DEC_COUNT] <=0";
                        com.CommandText = sql;
                        com.ExecuteNonQuery();
                    }
                }
                tran.Commit();
                if (result > 0)
                {
                    return dh;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                conn.Close();
                Log.write(ex.Message + "\r\n" + ex.StackTrace);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 是否为合法用户
        /// </summary>
        /// <param name="user">用户信息的数据封装对象</param>
        /// <returns>是否合法</returns>
        public DataTable getAllOutPlace(int inout)
        {
            DataTable ds = new DataTable();
            string sql = " select * from T_SYS_INPORT where i_inout = " + inout + " ";
            try
            {

                ds = dbHelper.GetDataSet(sql);
                dbHelper.getConnection().Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbHelper.getConnection().Close();
            }
            return ds;
        }


        /// <summary>
        /// 手工入库
        /// </summary>
        /// <param name="user">货位信息</param>
        /// <returns></returns>
        public bool handEmptyIn(string place, string tray, int uselie)
        {
            int result = 0;
            DbConnection conn = dbHelper.getConnection();
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                Log.write(ex.Message + "\r\n" + ex.StackTrace);
                conn.Close();
                throw ex;
            }
            DbTransaction tran = conn.BeginTransaction();
            DbCommand com = conn.CreateCommand();
            string sql = string.Empty;
            try
            {
                com.Transaction = tran;


                long dec_id = 0;
                string c_id = string.Empty;
                int count = 0;

                sql = "SELECT max(c_id) FROM   T_OPERATE_INOUT_MAIN where C_CRK_LEIBIE =2";

                com.CommandText = sql;
                object obj = com.ExecuteScalar();
                dec_id = Convert.IsDBNull(obj) ? 1000 : Convert.ToInt64(obj);
                c_id = (dec_id + 1).ToString();

                sql = "INSERT INTO [T_OPERATE_INOUT_MAIN]([C_ID], [D_RQ], [C_CRK_LEIBIE],  [C_CZY], [C_MEMO]) VALUES(@C_ID, @D_RQ, '2',  @C_CZY,  @C_MEMO)";
                com.CommandText = sql;
                Hashtable table = new Hashtable();

                table.Add("C_ID", c_id);
                table.Add("D_RQ", DateTime.Now);
                table.Add("C_CZY", Global.longid);
                table.Add("C_MEMO", "空托盘入库");

                DbParameter[] parms = dbHelper.getParams(table);
                com.Parameters.Clear();
                com.Parameters.AddRange(parms);
                com.ExecuteNonQuery();

                sql = "INSERT INTO [T_OPERATE_INOUT_SUB]([C_ID], [C_CRK_LEIBIE], [C_PLACE], [C_Tray], [DEC_COUNT],  [I_TYPE]) " +
                             "  VALUES(@C_ID,2, @C_PLACE, @C_Tray, @DEC_COUNT,0)";
                com.CommandText = sql;
                Hashtable table2 = new Hashtable();
                table2.Add("C_ID", c_id);

                table2.Add("C_PLACE", place);
                table2.Add("C_Tray", tray);
                table2.Add("DEC_COUNT", 0);
                DbParameter[] parms2 = dbHelper.getParams(table2);
                com.Parameters.Clear();
                com.Parameters.AddRange(parms2);
                com.ExecuteNonQuery();

                sql = "INSERT INTO [T_OPERATE_STOCKS]( [C_PLACE], [D_END_TIME], [C_Tray],[I_uselie])  VALUES ( @C_PLACE, @D_END_TIME, @C_Tray,@I_uselie)";
                com.CommandText = sql;
                Hashtable table3 = new Hashtable();

                table3.Add("C_PLACE", place);

                table3.Add("D_END_TIME", DateTime.Now);
                table3.Add("C_Tray", tray);
                table3.Add("I_uselie", uselie);
                DbParameter[] parms3 = dbHelper.getParams(table3);
                com.Parameters.Clear();
                com.Parameters.AddRange(parms3);
                result = com.ExecuteNonQuery();

                tran.Commit();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                conn.Close();
                Log.write(ex.Message + "\r\n" + ex.StackTrace);
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }


        /// 联机入库
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="meno"></param>
        /// <param name="station"></param>
        /// <returns></returns>
        public bool emptyInDolist(string tray, int userlie, string place, string inport)
        {

            bool flag = false;
            int result = 0;
            int i_run = 1;

            string sql = string.Empty;
            try
            {



                long dec_id = 0;
                string c_id = string.Empty;
                int count = 0;

                sql = "SELECT max(Dec_ID) FROM   T_Runing_Dolist ";


                object obj = dbHelper.GetScalar(sql);
                dec_id = Convert.IsDBNull(obj) ? 0 : Convert.ToInt64(obj);

                int take = userlie;

                string sorecePlace = inport;

                dec_id = dec_id + 1;
                c_id = (dec_id).ToString();



                sql = "INSERT INTO [T_Runing_Dolist]([Dec_ID],  [I_INOUT], [D_RQ], " +
                       "   [C_PLACE], [I_UseLie], [C_Tray],   [C_CZY], [I_RUN], [D_AddRQ],  [C_PLACE_source], [I_UseLie_source],[I_BACK])" +
                      "    VALUES('" + c_id + "', 2, getdate(),  '" + place + "', " + take + ", " +
                      "   '" + tray + "','" + Global.longid + "' , " + i_run + ", getdate(),  '" + sorecePlace + "', " + take + ",0)";


                result = dbHelper.ExecuteCommand(sql);
                if (result > 0)
                {
                    flag = true;
                }





            }
            catch (Exception ex)
            {

                throw ex;
            }

            return flag;

        }


        /// <summary>
        /// 手工出库
        /// </summary>
        /// <param name="user">货位信息</param>
        /// <returns></returns>
        public bool handEmptyOut(string place, string tray)
        {
            int result = 0;
            DbConnection conn = dbHelper.getConnection();
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                Log.write(ex.Message + "\r\n" + ex.StackTrace);
                conn.Close();
                throw ex;
            }
            DbTransaction tran = conn.BeginTransaction();
            DbCommand com = conn.CreateCommand();
            string sql = string.Empty;
            try
            {
                com.Transaction = tran;


                long dec_id = 0;
                string c_id = string.Empty;
                int count = 0;

                sql = "SELECT max(c_id) FROM   T_OPERATE_INOUT_MAIN where C_CRK_LEIBIE =1";

                com.CommandText = sql;
                object obj = com.ExecuteScalar();
                dec_id = Convert.IsDBNull(obj) ? 1000 : Convert.ToInt64(obj);
                c_id = (dec_id + 1).ToString();

                sql = "INSERT INTO [T_OPERATE_INOUT_MAIN]([C_ID], [D_RQ], [C_CRK_LEIBIE],  [C_CZY], [C_MEMO]) VALUES(@C_ID, @D_RQ, '1',  @C_CZY,  @C_MEMO)";
                com.CommandText = sql;
                Hashtable table = new Hashtable();

                table.Add("C_ID", c_id);
                table.Add("D_RQ", DateTime.Now);
                table.Add("C_CZY", Global.longid);
                table.Add("C_MEMO", "空托盘出库");

                DbParameter[] parms = dbHelper.getParams(table);
                com.Parameters.Clear();
                com.Parameters.AddRange(parms);
                com.ExecuteNonQuery();

                sql = "INSERT INTO [T_OPERATE_INOUT_SUB]([C_ID], [C_CRK_LEIBIE], [C_PLACE], [C_Tray], [DEC_COUNT],  [I_TYPE]) " +
                             "  VALUES(@C_ID,1, @C_PLACE, @C_Tray, @DEC_COUNT,0)";
                com.CommandText = sql;
                Hashtable table2 = new Hashtable();
                table2.Add("C_ID", c_id);

                table2.Add("C_PLACE", place);
                table2.Add("C_Tray", tray);
                table2.Add("DEC_COUNT", 0);
                DbParameter[] parms2 = dbHelper.getParams(table2);
                com.Parameters.Clear();
                com.Parameters.AddRange(parms2);
                com.ExecuteNonQuery();

                sql = "delete from [T_OPERATE_STOCKS] where C_PLACE = @C_PLACE and C_Tray = @C_Tray  ";
                com.CommandText = sql;
                Hashtable table3 = new Hashtable();

                table3.Add("C_PLACE", place);
                table3.Add("C_Tray", tray);

                DbParameter[] parms3 = dbHelper.getParams(table3);
                com.Parameters.Clear();
                com.Parameters.AddRange(parms3);
                result = com.ExecuteNonQuery();

                tran.Commit();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                conn.Close();
                Log.write(ex.Message + "\r\n" + ex.StackTrace);
                throw ex;
            }
            finally
            {
                conn.Close();
            }

        }
        /// <summary>
        /// 根据工位编码获得出口地址
        /// </summary>
        /// <param name="stationid"></param>
        /// <returns></returns>
        public string getOutPlacrSorece(string stationid)
        {
            string sql = "select C_PLACE from T_JB_STATION_PLACE where C_STATION_ID = '" + stationid + "' ";

            object temp = dbHelper.GetScalar(sql);
            if (temp != null && !(DBNull.Value.Equals(temp)))
            {
                string nowPlace = Convert.ToString(temp);

                return nowPlace;
            }
            else
            {
                return string.Empty;
            }
        }


        /// 联机入库
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="meno"></param>
        /// <param name="station"></param>
        /// <returns></returns>
        public bool emptyOutDolist(string tray, int userlie, string place, string inport)
        {

            bool flag = false;
            int result = 0;
            int i_run = 1;

            string sql = string.Empty;
            try
            {



                long dec_id = 0;
                string c_id = string.Empty;
                int count = 0;

                sql = "SELECT max(Dec_ID) FROM   T_Runing_Dolist ";


                object obj = dbHelper.GetScalar(sql);
                dec_id = Convert.IsDBNull(obj) ? 0 : Convert.ToInt64(obj);

                int take = userlie;




                string sorecePlace = inport;

                dec_id = dec_id + 1;
                c_id = (dec_id).ToString();



                sql = "INSERT INTO [T_Runing_Dolist]([Dec_ID],  [I_INOUT], [D_RQ], " +
                       "   [C_PLACE], [I_UseLie], [C_Tray],   [C_CZY], [I_RUN], [D_AddRQ],  [C_PLACE_source], [I_UseLie_source],[I_BACK])" +
                      "    VALUES('" + c_id + "', 1, getdate(),  '" + place + "', " + take + ", " +
                      "   '" + tray + "','" + Global.longid + "' , " + i_run + ", getdate(),  '" + sorecePlace + "', " + take + ",0)";


                result = dbHelper.ExecuteCommand(sql);
                if (result > 0)
                {
                    flag = true;
                }





            }
            catch (Exception ex)
            {

                throw ex;
            }

            return flag;

        }


        public DataTable getProductOutList(string planid, string mid)
        {
            string sql = string.Empty;
            sql = @" select * from (
                            select b.C_OPPOSITE_NO, d.C_CUSTOMER_NAME,a.C_MATERIEL,c.c_name,a.DEC_COUNT,
                            CONVERT(varchar(12) ,  b.D_RQ, 111 ) as D_RQ,c.c_makings
                            from T_OPERATE_INOUT_SUB a 
                            inner join T_OPERATE_INOUT_MAIN b on a.C_ID = b.C_ID and a.C_CRK_LEIBIE =b.C_CRK_LEIBIE
                            inner join ( select distinct C_ID,C_CUSTOMER_NAME  from T_OPERATE_PRODUCE_PLAN )  d on  b.C_OPPOSITE_NO = d.C_ID
                            inner join T_JB_COMPONENT c on a.C_MATERIEL = c.C_ID
                            where a.C_CRK_LEIBIE = 13 
                            ) h where 1=1  ";

            DataTable dt = new DataTable();
            try
            {

                Hashtable table = new Hashtable();


                if (!(string.IsNullOrEmpty(planid)))
                {
                    sql += " and  C_OPPOSITE_NO = @planid";

                    table.Add("planid", planid);
                }
                if (mid != null && !(string.Empty.Equals(mid)))
                {
                    sql += " and C_MATERIEL = @mid";

                    table.Add("mid", mid);
                }
                sql += " order by D_RQ desc";
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

        #region 统计工序工时需要
        public DataTable getInDetail(DateTime startDate, DateTime endDate)
        {

            string sql = @" select a.DEC_COUNT,p.c_name,a.c_people_id,a.C_MATERIEL,a.c_Procedure
                            from T_OPERATE_INOUT_SUB a
                            inner join T_OPERATE_INOUT_MAIN b on a.C_ID = b.C_ID and a.C_CRK_LEIBIE =b.C_CRK_LEIBIE                        
                            inner join T_JB_PROCEDURE p on a.c_Procedure = p.C_ID
                            where a.C_CRK_LEIBIE =21 and  convert(datetime,CONVERT(varchar(12) ,  b.D_RQ, 111 ))  between @startDate and @endDate  
                            union
                            select a.DEC_COUNT,p.c_name,a.c_people_id,a.C_MATERIEL,a.c_Procedure
                            from T_OPERATE_INOUT_SUB a
                            inner join T_OPERATE_INOUT_MAIN b on a.C_ID = b.C_ID and a.C_CRK_LEIBIE =b.C_CRK_LEIBIE                        
                            inner join T_JB_PROCEDURE p on a.c_Procedure = p.C_ID
                            where a.C_CRK_LEIBIE =13 and  convert(datetime,CONVERT(varchar(12) ,  b.D_RQ, 111 ))  between @startDate and @endDate  ";

            DataTable dt = new DataTable();
            try
            {

                Hashtable table = new Hashtable();

                if (startDate != Global.minValue && endDate != Global.minValue)
                {
                    table.Add("startDate", startDate);
                    table.Add("endDate", endDate);
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

        public int getValue(string id, string proid)
        {
            int count = 1;
            string sql = " select I_VALUE from T_JB_COMPONENT_PROCEDURE where C_COMPONENT_ID = '" + id + "' and C_PROCEDURE_ID = '" + proid + "' ";

            object temp = dbHelper.GetScalar(sql);
            if (temp != null && !(DBNull.Value.Equals(temp)))
            {
                count = Convert.ToInt32(temp);
            }
            return count;
        }

        #endregion
        #region 统计面积需要
        public DataTable getPlanList(string planid, DateTime startDate, DateTime endDate)
        {
            string sql = @" select * from T_OPERATE_PRODUCE_PLAN where 1=1 ";

            DataTable dt = new DataTable();
            try
            {

                Hashtable table = new Hashtable();


                if (!(string.IsNullOrEmpty(planid)))
                {
                    sql += " and  C_ID = @C_ID";

                    table.Add("C_ID", planid);
                }
                else
                {
                    if (startDate != Global.minValue && endDate != Global.minValue)
                    {
                        sql += " and  convert(datetime, CONVERT(varchar(100), D_PLAN_DATE, 23),120) between @startDate and @endDate";


                        table.Add("startDate", startDate);
                        table.Add("endDate", endDate);
                    }
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


        public DataTable getPlanList(string planid)
        {
            string sql = @" select * from T_OPERATE_PRODUCE_PLAN where 1=1 ";

            DataTable dt = new DataTable();
            try
            {

                Hashtable table = new Hashtable();


                if (!(string.IsNullOrEmpty(planid)))
                {
                    sql += " and  C_ID = @C_ID";

                    table.Add("C_ID", planid);
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

        public DataTable getSubList(string mainid)
        {
            string sql = @" select * from T_COMPONENT_LAYOUT where C_COMPONENT_MAIN = '" + mainid + "' ";

            DataTable dt = new DataTable();
            try
            {
                dt = dbHelper.GetDataSet(sql);

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

        public DataTable getSubMess(string subid)
        {
            string sql = @" select * from T_JB_COMPONENT where c_id = '" + subid + "' ";

            DataTable dt = new DataTable();
            try
            {
                dt = dbHelper.GetDataSet(sql);

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

        #endregion


        #region 统计工序时需要
        public DataTable getInAllDetail(string planid, DateTime startDate, DateTime endDate, string procedure)
        {

            DataTable dt = new DataTable();
            try
            {

                Hashtable table = new Hashtable();
                string where = string.Empty;
                if (!(string.IsNullOrEmpty(planid)))
                {
                    where += " and  b.C_OPPOSITE_NO = @C_ID ";

                    table.Add("C_ID", planid);
                }
                else
                {
                    if (startDate != Global.minValue && endDate != Global.minValue)
                    {
                        where += "and convert(datetime,CONVERT(varchar(12) ,  b.D_RQ, 111 ))  between @startDate and @endDate ";
                        table.Add("startDate", startDate);
                        table.Add("endDate", endDate);
                    }
                }
                if (!(string.IsNullOrEmpty(procedure)))
                {
                    where += " and   a.c_Procedure = @procedure ";

                    table.Add("procedure", procedure);
                }

                string sql = @" select a.DEC_COUNT,p.c_name,a.c_people_id,a.C_MATERIEL,a.c_Procedure
                            from T_OPERATE_INOUT_SUB a
                            inner join T_OPERATE_INOUT_MAIN b on a.C_ID = b.C_ID and a.C_CRK_LEIBIE =b.C_CRK_LEIBIE                        
                            inner join T_JB_PROCEDURE p on a.c_Procedure = p.C_ID
                            where a.C_CRK_LEIBIE =21 " + where +
                             @" union
                            select a.DEC_COUNT,p.c_name,a.c_people_id,a.C_MATERIEL,a.c_Procedure
                            from T_OPERATE_INOUT_SUB a
                            inner join T_OPERATE_INOUT_MAIN b on a.C_ID = b.C_ID and a.C_CRK_LEIBIE =b.C_CRK_LEIBIE                        
                            inner join T_JB_PROCEDURE p on a.c_Procedure = p.C_ID
                            where a.C_CRK_LEIBIE =13   " + where;


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
        #endregion

        /// <summary>
        /// 是否有联机任务
        /// </summary>
        /// <returns></returns>
        public bool HasDoList()
        {
            try
            {
                string sql = " select count(*) from T_Runing_Dolist";

                object obj2 = dbHelper.GetScalar(sql);
                int count = Convert.IsDBNull(obj2) ? 0 : Convert.ToInt32(obj2);

                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
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
        }

        public DataTable getKDList(DateTime startDate, DateTime endDate, string planid, InOutType type, string mid)
        {
            string sql = @"select a.C_ID,a.C_MATERIEL,
                             c.c_name,a.C_MACHINE,a.C_PLACE,CONVERT(varchar(12) ,  b.D_RQ, 111 ) as D_RQ, a.PRID
                             from T_OPERATE_INOUT_SUB a 
                            left join T_OPERATE_INOUT_MAIN b on a.C_ID = b.C_ID and a.C_CRK_LEIBIE =b.C_CRK_LEIBIE
                            left join T_JB_Materiel c on a.C_MATERIEL = c.C_ID where I_FLAG = 1 and a.C_CRK_LEIBIE = " + (int)type +
                            " and a.C_PLACE not in (select C_PLACE from T_Runing_Dolist)";

            DataTable dt = new DataTable();
            try
            {

                Hashtable table = new Hashtable();

                if (startDate != Global.minValue && endDate != Global.minValue)
                {
                    sql += " and  convert(datetime, CONVERT(varchar(100), D_RQ, 23),120) between @startDate and @endDate";


                    table.Add("startDate", startDate);
                    table.Add("endDate", endDate);
                }

                if (mid != null && !(string.Empty.Equals(mid)))
                {
                    sql += " and a.C_MATERIEL like '%" + mid + "%'";

                }
                sql += " order by D_RQ desc";
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

        public bool DisableKnife(List<string> list)
        {
            try
            {
                int count = 0;

                string sql = "UPDATE [T_OPERATE_INOUT_SUB] SET  [I_FLAG] = 10 where [PRID] in ('" + string.Join("','", list.ToArray()) + "') and [I_FLAG] = 1";

                count = dbHelper.ExecuteCommand(sql);

                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
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
        }
    }
}
