using System;
using System.Collections.Generic;
using System.Text;
using Util;
using Model;
using System.Collections;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using DBCore;

namespace DAL
{
    public class RuningDoListDAL
    {
        private DBHelper dbHelper = new SQLDBHelper();

        /// <summary>
        /// 获取联机任务列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable getList(int type)
        {
            string sql = @"select a.Dec_ID,a.C_DH,case I_INOUT when 11  then '生产出库' when 12 then '板材出库' 
                        when 13 then '产品出库' when 14 then '空托盘出库' when 21 then '报工入库' when 22 then '板材入库' 
                        when 23 then '成品入库' when 24 then '空托盘入库'  else '出库' end as c_inout,
                        a.C_MATERIEL,a.C_MATERIEL_NAME,a.DEC_COUNT,a.C_CZY,b.c_name as c_Procedure_name,a.C_PLACE,a.C_Tray
                         from T_Runing_Dolist a left join T_JB_PROCEDURE b on a.c_Procedure = b.c_id   WHERE 1=1";
            DataTable dt = new DataTable();
            try
            {
                if (type == 1)
                {
                    sql += " and a.I_INOUT < 20 ";

                }
                else if (type == 2)
                {
                    sql += " and a.I_INOUT > 20 ";
                }

                sql += " order by C_PLACE ";
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


        /// <summary>
        ///  删除联机任务
        /// </summary>
        /// <param name="toRunDate"></param>
        /// <returns></returns>
        public bool deleteDoList(List<string> list)
        {

            int count = 0;
            DbConnection con = dbHelper.getConnection();
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                Log.write(ex.Message + "\r\n" + ex.StackTrace);
                con.Close();
                throw ex;
            }
            SqlTransaction tran = (SqlTransaction)con.BeginTransaction();
            SqlCommand com = (SqlCommand)con.CreateCommand();
            string sql = string.Empty;
            try
            {
                com.Transaction = tran;
                foreach (string did in list)
                {
                    sql = " select * from T_Runing_Dolist where Dec_ID = " + did;
                    com.CommandText = sql;

                    SqlDataAdapter sda = new SqlDataAdapter(com);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        int inout = Convert.ToInt32(dt.Rows[0]["I_INOUT"]);
                        if (inout > 20)
                        {
                            int i_take = Convert.ToInt32(dt.Rows[0]["I_UseLie"]);
                            if (i_take >= 2)
                            {
                                string placeTemp = dt.Rows[0]["C_PLACE"].ToString();
                                string jia = placeTemp.Substring(0, 2);
                                string ceng = placeTemp.Substring(4, 2);
                                int lie = Convert.ToInt32(placeTemp.Substring(2, 2));

                                string rightPlace = jia + "0" + (lie + 1) + ceng;
                                sql = " update T_JB_PLACE set I_TAKE = 1 where C_ID = '" + placeTemp + "' ";
                                com.CommandText = sql;
                                com.ExecuteNonQuery();
                                sql = " update T_JB_PLACE set I_INUSE = 1 where C_ID = '" + rightPlace + "'";
                                com.CommandText = sql;
                                com.ExecuteNonQuery();
                                sql = "";
                            }
                        }
                    }
                    sql = "delete from T_Runing_Dolist where Dec_ID = @Dec_ID ";
                    com.CommandText = sql;
                    Hashtable table = new Hashtable();
                    table.Add("Dec_ID", did);

                    DbParameter[] parms = dbHelper.getParams(table);
                    com.Parameters.Clear();
                    com.Parameters.AddRange(parms);
                    count = com.ExecuteNonQuery();
                }



                tran.Commit();
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
                tran.Rollback();
                con.Close();
                Log.write(ex.Message + "\r\n" + ex.StackTrace);
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }


        /// <summary>
        /// 执行联机任务
        /// </summary>
        /// <returns></returns>
        public bool executeDoList(List<string> list)
        {
            int result = 0;
            DbConnection conn = dbHelper.getConnection();
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {

                conn.Close();
                throw ex;
            }
            SqlTransaction tran = (SqlTransaction)conn.BeginTransaction();
            SqlCommand com = (SqlCommand)conn.CreateCommand();
            string sql = string.Empty;
            try
            {
                com.Transaction = tran;

                foreach (string id in list)
                {
                    sql = " select * from T_Runing_Dolist where Dec_ID = " + id;
                    com.CommandText = sql;

                    SqlDataAdapter sda = new SqlDataAdapter(com);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        int inout = Convert.ToInt32(dt.Rows[0]["I_INOUT"]);
                        if (inout < 20) //出库
                        {
                            #region 获得出库单号
                            long dec_id = 0;
                            string c_id = string.Empty;
                            sql = "SELECT max(c_id) FROM   T_OPERATE_INOUT_MAIN where C_CRK_LEIBIE =" + inout;

                            com.CommandText = sql;
                            object obj = com.ExecuteScalar();
                            dec_id = Convert.IsDBNull(obj) ? 1000 : Convert.ToInt64(obj);
                            c_id = (dec_id + 1).ToString();
                            #endregion
                            #region 插入主表
                            sql = "INSERT INTO [T_OPERATE_INOUT_MAIN]([C_ID], [D_RQ], [C_CRK_LEIBIE],   [C_OPPOSITE_NO]) VALUES(" +
                                "'" + c_id + "', '" + dt.Rows[0]["D_RQ"] + "', '" + inout + "',    ";

                            object mainMeno = dt.Rows[0]["C_DH"];
                            if (mainMeno == null || string.Empty.Equals(mainMeno) || DBNull.Value.Equals(mainMeno))
                            {
                                sql += "null)";
                            }
                            else
                            {
                                sql += "'" + mainMeno + "')";

                            }
                            com.CommandText = sql;
                            com.ExecuteNonQuery();
                            #endregion
                            #region 插入子表
                            sql = "INSERT INTO [T_OPERATE_INOUT_SUB]([C_ID],[C_CRK_LEIBIE], [C_MATERIEL], [C_PLACE], [C_Tray], [DEC_COUNT],  [C_PLACE_OLD],[I_TYPE],[C_STATION],[c_people_id],[c_Procedure]) " +
                            "  VALUES('" + c_id + "','" + inout + "', ";
                            object C_MATERIEL = dt.Rows[0]["C_MATERIEL"];
                            if (C_MATERIEL == null || string.Empty.Equals(C_MATERIEL) || DBNull.Value.Equals(C_MATERIEL))
                            {
                                sql += "null";
                            }
                            else
                            {
                                sql += "'" + C_MATERIEL + "'";
                            }
                            sql += ", '" + dt.Rows[0]["C_PLACE"] + "', '" + dt.Rows[0]["C_Tray"] + "', ";
                            object DEC_COUNT = dt.Rows[0]["DEC_COUNT"];
                            if (DEC_COUNT == null || string.Empty.Equals(DEC_COUNT) || DBNull.Value.Equals(DEC_COUNT))
                            {
                                sql += "null";
                            }
                            else
                            {
                                sql += DEC_COUNT;

                            }
                            sql += ", '" + dt.Rows[0]["C_PLACE"] + "',1,'" + dt.Rows[0]["C_STATION"] + "',";
                            object c_people_id = dt.Rows[0]["C_CZY"];
                            if (inout == 13)
                            {
                                c_people_id = dt.Rows[0]["c_people_id"];
                            }
                            if (c_people_id == null || string.Empty.Equals(c_people_id) || DBNull.Value.Equals(c_people_id))
                            {
                                sql += "null,";
                            }
                            else
                            {
                                sql += "'" + c_people_id + "',";

                            }
                            object c_Procedure = dt.Rows[0]["c_Procedure"];
                            if (c_Procedure == null || string.Empty.Equals(c_Procedure) || DBNull.Value.Equals(c_Procedure))
                            {
                                sql += "null)";
                            }
                            else
                            {
                                sql += "'" + c_Procedure + "')";
                            }
                            com.CommandText = sql;
                            com.ExecuteNonQuery();
                            #endregion
                            bool isEmpty = false;

                            string materiel = DBNull.Value.Equals(dt.Rows[0]["C_MATERIEL"]) ? string.Empty : Convert.ToString(dt.Rows[0]["C_MATERIEL"]);
                            if (materiel == null || string.Empty.Equals(materiel))
                            {
                                isEmpty = true;
                            }

                            #region 更新库存

                            if (isEmpty == false)
                            {
                                c_people_id = dt.Rows[0]["C_CZY"];
                                sql = "UPDATE [T_OPERATE_STOCKS] SET  [DEC_COUNT]=[DEC_COUNT] - " + dt.Rows[0]["DEC_COUNT"] + " where [C_MATERIEL_ID] = '" +
                                    dt.Rows[0]["C_MATERIEL"] + "' and [C_PLACE] = '" + dt.Rows[0]["C_PLACE"] + "'";

                                if (c_people_id == null || string.Empty.Equals(c_people_id) || DBNull.Value.Equals(c_people_id))
                                {
                                    sql += " and c_people_id is null";
                                }
                                else
                                {
                                    sql += " and c_people_id = '" + c_people_id + "'";
                                }
                                if (c_Procedure == null || string.Empty.Equals(c_Procedure) || DBNull.Value.Equals(c_Procedure))
                                {
                                    sql += " and c_Procedure is null";
                                }
                                else
                                {
                                    if ("0005".Equals(c_Procedure))
                                    {
                                        sql += " and c_Procedure = '9999'";
                                    }
                                    else
                                    {
                                        sql += " and c_Procedure = '" + c_Procedure + "'";
                                    }
                                }
                                if (mainMeno == null || string.Empty.Equals(mainMeno) || DBNull.Value.Equals(mainMeno))
                                {
                                    sql += " and C_DH is null";
                                }
                                else
                                {
                                    if (inout == 12)
                                    {
                                        sql += " and C_DH is null";
                                    }
                                    else
                                    {
                                        sql += " and C_DH = '" + mainMeno + "'";
                                    }
                                }

                                com.CommandText = sql;
                                com.ExecuteNonQuery();


                                sql = "DELETE FROM  [T_OPERATE_STOCKS] where [DEC_COUNT] <=0";
                                com.CommandText = sql;
                                com.ExecuteNonQuery();
                            }
                            else
                            {
                                string place = DBNull.Value.Equals(dt.Rows[0]["C_PLACE"]) ? string.Empty : Convert.ToString(dt.Rows[0]["C_PLACE"]);
                                sql = "DELETE FROM  [T_OPERATE_STOCKS] where [C_PLACE]  = '" + place + "'";
                                com.CommandText = sql;
                                com.ExecuteNonQuery();
                            }

                            #endregion

                            #region 更新货位
                            sql = "select sum([DEC_COUNT]) from T_OPERATE_STOCKS where [C_PLACE] = '" + dt.Rows[0]["C_PLACE"] + "' ";
                            com.CommandText = sql;
                            int stocksCount = 0;
                            object stocksCountTemp = com.ExecuteScalar();
                            if (stocksCountTemp == null || string.Empty.Equals(stocksCountTemp) || DBNull.Value.Equals(stocksCountTemp))
                            {
                                stocksCount = 0;
                            }
                            else
                            {
                                stocksCount = Convert.ToInt32(stocksCountTemp);
                            }

                            if (stocksCount <= 0)
                            {
                                int i_take = Convert.ToInt32(dt.Rows[0]["I_UseLie"]);
                                if (i_take >= 2)
                                {
                                    string placeTemp = dt.Rows[0]["C_PLACE"].ToString();
                                    string jia = placeTemp.Substring(0, 2);
                                    string ceng = placeTemp.Substring(4, 2);
                                    int lie = Convert.ToInt32(placeTemp.Substring(2, 2));

                                    string rightPlace = jia + "0" + (lie + 1) + ceng;
                                    sql = " update T_JB_PLACE set I_TAKE = 1 where C_ID = '" + placeTemp + "' ";
                                    com.CommandText = sql;
                                    com.ExecuteNonQuery();
                                    sql = " update T_JB_PLACE set I_INUSE = 1 where C_ID = '" + rightPlace + "'";
                                    com.CommandText = sql;
                                    com.ExecuteNonQuery();
                                    sql = "";
                                }
                            }
                            #endregion

                            if (inout == 13)
                            {
                                sql = " select I_VALUE from T_JB_COMPONENT_PROCEDURE where C_COMPONENT_ID = '" + dt.Rows[0]["C_MATERIEL"] + "' and C_PROCEDURE_ID ='0005' ";
                                com.CommandText = sql;
                                object objPCount = com.ExecuteScalar();
                                int pCount = Convert.IsDBNull(objPCount) ? 0 : Convert.ToInt32(objPCount);
                                if (pCount == 0)
                                {
                                    pCount = 1;
                                }
                                int piCount = Convert.ToInt32(dt.Rows[0]["DEC_COUNT"]);
                                int toCount = pCount * piCount;
                                sql = " update T_OPERATE_PRODUCE_PLAN_SUB set I_PIECE_FINISH_COUNT = I_PIECE_FINISH_COUNT+" + piCount + ",I_FINISH_COUNT = I_FINISH_COUNT+" + toCount +
                           " where C_PLAN_ID= '" + mainMeno + "' and C_COMPONENT_ID = '" + dt.Rows[0]["C_MATERIEL"] + "' and C_PROCEDURE_ID = '0005' ";
                                com.CommandText = sql;
                                result = com.ExecuteNonQuery();

                            }
                        }
                        else if (inout > 20) //入库
                        {

                            #region 获得出库单号
                            long dec_id = 0;
                            string c_id = string.Empty;
                            sql = "SELECT max(c_id) FROM   T_OPERATE_INOUT_MAIN where C_CRK_LEIBIE =" + inout;

                            com.CommandText = sql;
                            object obj = com.ExecuteScalar();
                            dec_id = Convert.IsDBNull(obj) ? 1000 : Convert.ToInt64(obj);
                            c_id = (dec_id + 1).ToString();
                            #endregion
                            #region 插入主表
                            sql = "INSERT INTO [T_OPERATE_INOUT_MAIN]([C_ID], [D_RQ], [C_CRK_LEIBIE], [C_OPPOSITE_NO]) VALUES('" +
                                c_id + "', '" + dt.Rows[0]["D_RQ"] + "', '" + inout + "',";


                            object mainMeno = dt.Rows[0]["C_DH"];
                            if (mainMeno == null || string.Empty.Equals(mainMeno) || DBNull.Value.Equals(mainMeno))
                            {
                                sql += "null)";
                            }
                            else
                            {
                                sql += "'" + mainMeno + "')";

                            }

                            com.CommandText = sql;
                            com.ExecuteNonQuery();
                            #endregion
                            #region 插入子表
                            sql = "INSERT INTO [T_OPERATE_INOUT_SUB]([C_ID],[C_CRK_LEIBIE], [C_MATERIEL], [C_PLACE], [C_Tray], [DEC_COUNT],  [C_PLACE_OLD],[I_TYPE],[C_STATION],[c_people_id],[c_Procedure]) " +
                            "  VALUES('" + c_id + "','" + inout + "',";
                            object C_MATERIEL = dt.Rows[0]["C_MATERIEL"];
                            if (C_MATERIEL == null || string.Empty.Equals(C_MATERIEL) || DBNull.Value.Equals(C_MATERIEL))
                            {
                                sql += "null";
                            }
                            else
                            {
                                sql += "'" + C_MATERIEL + "'";
                            }
                            sql += ", '" + dt.Rows[0]["C_PLACE"] + "', '" + dt.Rows[0]["C_Tray"] +
                             "', ";
                            object DEC_COUNT = dt.Rows[0]["DEC_COUNT"];
                            if (DEC_COUNT == null || string.Empty.Equals(DEC_COUNT) || DBNull.Value.Equals(DEC_COUNT))
                            {
                                sql += "null";
                            }
                            else
                            {
                                sql += DEC_COUNT;

                            }
                            sql += ", '" + dt.Rows[0]["C_PLACE"] + "',1,'" + dt.Rows[0]["C_STATION"] + "',";
                            object c_people_id = dt.Rows[0]["C_CZY"];
                            if (c_people_id == null || string.Empty.Equals(c_people_id) || DBNull.Value.Equals(c_people_id))
                            {
                                sql += "null,";
                            }
                            else
                            {
                                sql += "'" + c_people_id + "',";

                            }
                            object c_Procedure = dt.Rows[0]["c_Procedure"];
                            if (c_Procedure == null || string.Empty.Equals(c_Procedure) || DBNull.Value.Equals(c_Procedure))
                            {
                                sql += "null)";
                            }
                            else
                            {
                                sql += "'" + c_Procedure + "')";
                            }

                            com.CommandText = sql;
                            com.ExecuteNonQuery();
                            #endregion
                            #region 更新库存
                            sql = " select count(*) from T_OPERATE_STOCKS where [C_MATERIEL_ID] = '" + dt.Rows[0]["C_MATERIEL"] + "' and [C_PLACE] ='" + dt.Rows[0]["C_PLACE"] + "'  ";

                            if (c_people_id == null || string.Empty.Equals(c_people_id) || DBNull.Value.Equals(c_people_id))
                            {
                                sql += " and c_people_id is null";
                            }
                            else
                            {
                                sql += " and c_people_id = '" + c_people_id + "'";
                            }
                            if (c_Procedure == null || string.Empty.Equals(c_Procedure) || DBNull.Value.Equals(c_Procedure))
                            {
                                sql += " and c_Procedure is null";
                            }
                            else
                            {
                                sql += " and c_Procedure = '" + c_Procedure + "'";

                            }
                            if (mainMeno == null || string.Empty.Equals(mainMeno) || DBNull.Value.Equals(mainMeno))
                            {
                                sql += " and C_DH is null";
                            }
                            else
                            {
                                sql += " and C_DH = '" + mainMeno + "'";

                            }

                            com.CommandText = sql;
                            object tempCount = com.ExecuteScalar();
                            int stockCount = 0;
                            if (tempCount != null && !(DBNull.Value.Equals(tempCount)))
                            {
                                stockCount = Convert.ToInt32(tempCount);
                            }
                            if (stockCount > 0)
                            {

                                sql = "UPDATE [T_OPERATE_STOCKS] SET  [DEC_COUNT]=[DEC_COUNT] + " + dt.Rows[0]["DEC_COUNT"] +
                                    ",[D_END_TIME] =getdate() where [C_MATERIEL_ID] = '" + dt.Rows[0]["C_MATERIEL"] + "' and [C_PLACE] = '" + dt.Rows[0]["C_PLACE"] + "'";



                                if (c_people_id == null || string.Empty.Equals(c_people_id) || DBNull.Value.Equals(c_people_id))
                                {
                                    sql += " and c_people_id is null";
                                }
                                else
                                {
                                    sql += " and c_people_id = '" + c_people_id + "'";

                                }
                                if (c_Procedure == null || string.Empty.Equals(c_Procedure) || DBNull.Value.Equals(c_Procedure))
                                {
                                    sql += " and c_Procedure is null";
                                }
                                else
                                {
                                    sql += " and c_Procedure = '" + c_Procedure + "'";

                                }
                                if (mainMeno == null || string.Empty.Equals(mainMeno) || DBNull.Value.Equals(mainMeno))
                                {
                                    sql += " and C_DH is null";
                                }
                                else
                                {
                                    sql += " and C_DH ='" + mainMeno + "'";

                                }
                                com.CommandText = sql;

                                com.ExecuteNonQuery();
                            }
                            else
                            {
                                sql = "INSERT INTO [T_OPERATE_STOCKS]([C_MATERIEL_ID], [C_PLACE], [DEC_COUNT], [D_END_TIME], [C_Tray],[I_uselie],[c_people_id],[c_Procedure],[C_DH])  VALUES (";

                                if (C_MATERIEL == null || string.Empty.Equals(C_MATERIEL) || DBNull.Value.Equals(C_MATERIEL))
                                {
                                    sql += "null";
                                }
                                else
                                {
                                    sql += "'" + C_MATERIEL + "'";

                                }
                                sql += ", '" + dt.Rows[0]["C_PLACE"] + "', ";

                                if (DEC_COUNT == null || string.Empty.Equals(DEC_COUNT) || DBNull.Value.Equals(DEC_COUNT))
                                {
                                    sql += "null";
                                }
                                else
                                {
                                    sql += DEC_COUNT;

                                }
                                sql += ", getdate(), '" + dt.Rows[0]["C_Tray"] + "'," + dt.Rows[0]["I_UseLie"] + ",";
                                if (c_people_id == null || string.Empty.Equals(c_people_id) || DBNull.Value.Equals(c_people_id))
                                {
                                    sql += "null,";
                                }
                                else
                                {
                                    sql += "'" + c_people_id + "',";
                                }
                                if (c_Procedure == null || string.Empty.Equals(c_Procedure) || DBNull.Value.Equals(c_Procedure))
                                {
                                    sql += "null,";
                                }
                                else
                                {
                                    sql += "'" + c_Procedure + "',";
                                }
                                if (mainMeno == null || string.Empty.Equals(mainMeno) || DBNull.Value.Equals(mainMeno))
                                {
                                    sql += "null)";
                                }
                                else
                                {
                                    sql += "'" + mainMeno + "')";

                                }
                                com.CommandText = sql;
                                result = com.ExecuteNonQuery();
                            }

                            #endregion
                            if (inout == 21)
                            {
                                sql = " select I_VALUE from T_JB_COMPONENT_PROCEDURE where C_COMPONENT_ID = '" + dt.Rows[0]["C_MATERIEL"] + "' and C_PROCEDURE_ID ='" + c_Procedure + "' ";
                                com.CommandText = sql;
                                object objPCount = com.ExecuteScalar();
                                int pCount = Convert.IsDBNull(objPCount) ? 0 : Convert.ToInt32(objPCount);
                                if (pCount == 0 && "9999".Equals(c_Procedure))
                                {
                                    pCount = 1;
                                }
                                int piCount = Convert.ToInt32(dt.Rows[0]["DEC_COUNT"]);
                                int toCount = pCount * piCount;
                                sql = " update T_OPERATE_PRODUCE_PLAN_SUB set I_PIECE_FINISH_COUNT = I_PIECE_FINISH_COUNT+" + piCount + ",I_FINISH_COUNT = I_FINISH_COUNT+" + toCount +
                           " where C_PLAN_ID= '" + mainMeno + "' and C_COMPONENT_ID = '" + dt.Rows[0]["C_MATERIEL"] + "' and C_PROCEDURE_ID = '" + c_Procedure + "' ";
                                com.CommandText = sql;
                                result = com.ExecuteNonQuery();


                            }



                        }

                        sql = " delete T_Runing_Dolist where dec_id =  " + dt.Rows[0]["dec_id"];
                        com.CommandText = sql;
                        result = com.ExecuteNonQuery();
                    }
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

                throw ex;
            }
            finally
            {
                conn.Close();
            }

        }

        /// <summary>
        /// 联机出库
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="meno"></param>
        /// <param name="station"></param>
        /// <returns></returns>
        public bool saveDolist(DataTable dt, string meno, int controlType)
        {//todo:controlType 出入库标志和联机出库的一些规则
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

                sql = "SELECT max(Dec_ID) FROM   T_Runing_Dolist ";

                com.CommandText = sql;
                object obj = com.ExecuteScalar();
                dec_id = Convert.IsDBNull(obj) ? 0 : Convert.ToInt64(obj);
               
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sql = @"INSERT INTO [T_Runing_Dolist]([Dec_ID],  [I_INOUT], [D_RQ], [C_MATERIEL], [C_MATERIEL_NAME], [C_TYPE_NAME], 
                            [C_PLACE],  [DEC_COUNT],  [C_CZY], [I_RUN], [D_AddRQ], [C_MEMO])
                            VALUES(@Dec_ID, @I_INOUT, @D_RQ, @C_MATERIEL, @C_MATERIEL_NAME, @C_TYPE_NAME, @C_PLACE, 
                            @DEC_COUNT, @C_CZY, @I_RUN, @D_AddRQ, @C_MEMO)";
                    com.CommandText = sql;
                    dec_id  = dec_id+1;
                    c_id = (dec_id).ToString();
                    Hashtable table2 = new Hashtable();
                    table2.Add("Dec_ID", c_id);
                    table2.Add("I_INOUT",controlType );
                    table2.Add("D_RQ", Convert.ToDateTime( dt.Rows[i][5]).ToString("yyyy-MM-dd"));
                    table2.Add("C_MATERIEL", dt.Rows[i][0]);
                    table2.Add("C_MATERIEL_NAME", dt.Rows[i][1]);
                    table2.Add("C_TYPE_NAME", dt.Rows[i][7]);
                    table2.Add("C_PLACE", dt.Rows[i][4]);
                    table2.Add("DEC_COUNT", dt.Rows[i][3]);
                    table2.Add("C_CZY", dt.Rows[i][6]);
                    table2.Add("I_RUN", 1);                   
                    table2.Add("D_AddRQ", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                    if (meno == null || string.Empty.Equals(meno.Trim()))
                    {
                        table2.Add("C_MEMO", DBNull.Value);
                    }
                    else
                    {
                        table2.Add("C_MEMO", meno);
                    }
                    table2.Add("I_BACK", 0);
                    DbParameter[] parms2 = dbHelper.getParams(table2);
                    com.Parameters.Clear();
                    com.Parameters.AddRange(parms2);
                    result =  com.ExecuteNonQuery();


                }

                if (controlType == 1)
                {
                    DataView dv = dt.DefaultView;
                    DataTable dataTableDistinct = dv.ToTable(true, new string[] {"4"});
                    for (int i = 0; i < dataTableDistinct.Rows.Count; i++)
                    {
                        string usePlace = dataTableDistinct.Rows[i][0].ToString();
                        sql = @"select count(*) from (
                                select a.DEC_COUNT -isnull( d.usecount,0) as canuse,a.C_PLACE
                                from T_OPERATE_STOCKS a  
                                left join  (select  isnull( DEC_COUNT,0) as usecount ,C_MATERIEL,C_PLACE from T_Runing_Dolist where I_INOUT = 1) d  
                                on  d.C_MATERIEL = a.C_MATERIEL_ID and a.C_PLACE = d.C_PLACE) g
                                where g.canuse >0 ";
                        sql += " and g.C_PLACE = '" + usePlace + "'";
                        com.CommandText = sql;
                        object objUse = com.ExecuteScalar();
                        int isBack = 0;
                        if (objUse != null && !(DBNull.Value.Equals(objUse)))
                        {
                            isBack = Convert.ToInt32(objUse);
                        }
                        if (isBack > 0)
                        {
                            sql = "update T_Runing_Dolist set I_BACK = 1 where I_INOUT = 1 and C_PLACE = '" + usePlace + "'";
                            com.CommandText = sql;
                            com.ExecuteNonQuery();
                        }
                    }

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
        /// 联机入库
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="meno"></param>
        /// <param name="station"></param>
        /// <returns></returns>
        public bool SaveDolist(DataTable dt, string meno, InOutType type)
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

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sql = @"INSERT INTO [T_Runing_Dolist]([Dec_ID],  [I_INOUT], [D_RQ], [C_MATERIEL], [C_MATERIEL_NAME], [C_TYPE_NAME], 
                            [C_PLACE], [DEC_COUNT],  [C_CZY], [I_RUN], [D_AddRQ], [C_MEMO], [I_BACK])
                            VALUES(@Dec_ID, @I_INOUT, @D_RQ, @C_MATERIEL, @C_MATERIEL_NAME, @C_TYPE_NAME, @C_PLACE, @DEC_COUNT, @C_CZY, @I_RUN, @D_AddRQ, @C_MEMO, @I_BACK)";
                    com.CommandText = sql;

                    Hashtable table2 = new Hashtable();
                    table2.Add("Dec_ID", i+1);
                    table2.Add("I_INOUT", (int)type);
                    table2.Add("D_RQ", Convert.ToDateTime(dt.Rows[i][5]).ToString("yyyy-MM-dd"));
                    table2.Add("C_MATERIEL", dt.Rows[i][0]);
                    table2.Add("C_MATERIEL_NAME", dt.Rows[i][1]);
                    table2.Add("C_TYPE_NAME", dt.Rows[i][7]);
                    table2.Add("C_PLACE", dt.Rows[i][4]);
                    table2.Add("DEC_COUNT", dt.Rows[i][3]);
                    table2.Add("C_CZY", dt.Rows[i][6]);
                    table2.Add("I_RUN", 1);
                    table2.Add("D_AddRQ", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                    if (meno == null || string.Empty.Equals(meno.Trim()))
                    {
                        table2.Add("C_MEMO", DBNull.Value);
                    }
                    else
                    {
                        table2.Add("C_MEMO", meno);
                    }
                    table2.Add("I_BACK", 0);
                    DbParameter[] parms2 = dbHelper.getParams(table2);
                    com.Parameters.Clear();
                    com.Parameters.AddRange(parms2);
                    result = com.ExecuteNonQuery();
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
    }
}
