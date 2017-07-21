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
            string sql = @"select a.Dec_ID,a.C_DH,case I_INOUT when 10  then '零件出库' when 20 then '零件入库' 
                        when 12 then '刀具使用出库' when 21 then '新刀具入库' when 22 then '刀具使用入库'  else '' end as c_inout,
                        a.C_MATERIEL,b.C_NAME as C_MATERIEL_NAME,a.DEC_COUNT,a.C_CZY,a.C_PLACE
                         from T_Runing_Dolist a left join T_JB_MATERIEL b on a.C_MATERIEL = b.c_id  where I_RUN = 0";
            DataTable dt = new DataTable();
            try
            {
                sql += " order by a.Dec_ID ";
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
        public bool deleteDoList(List<string> list, string dh)
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
                    sql = "delete from T_Runing_Dolist where Dec_ID = @Dec_ID and C_DH = @C_DH and I_RUN = 0";
                    com.CommandText = sql;
                    Hashtable table = new Hashtable();
                    table.Add("Dec_ID", did);
                    table.Add("C_DH", dh);

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
        public bool executeDoList(List<string> list, string dh)
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
            DateTime dtNow = DateTime.Now;

            try
            {
                com.Transaction = tran;

                sql = " select * from T_Runing_Dolist where Dec_ID in (" + string.Join(",", list.ToArray()) + ") and C_DH = @C_DH and I_RUN = 0";
                com.CommandText = sql;
                Hashtable table = new Hashtable();

                //List<string> list1 = new List<string>(list.ToArray());

                table.Add("C_DH", dh);

                DbParameter[] parms = dbHelper.getParams(table);
                com.Parameters.Clear();
                com.Parameters.AddRange(parms);

                SqlDataAdapter sda = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                InOutType type = InOutType.KNIFE_IN;

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i == 0)
                        {//主表只加一次

                            type = (InOutType)Convert.ToInt32(dt.Rows[0]["I_INOUT"]);
                            //判断主表数据是否存在
                            sql = "SELECT count(*) FROM T_OPERATE_INOUT_MAIN where [C_ID] = @C_ID";
                            com.CommandText = sql;
                            Hashtable tableDh = new Hashtable();
                            tableDh.Add("C_ID", dh);
                            DbParameter[] parmsDh = dbHelper.getParams(tableDh);
                            com.Parameters.Clear();
                            com.Parameters.AddRange(parmsDh);
                            int mainCount = int.Parse(com.ExecuteScalar().ToString());

                            if (mainCount == 0)
                            {
                                sql = "INSERT INTO [T_OPERATE_INOUT_MAIN]([C_ID], [D_RQ], [C_CRK_LEIBIE],  [C_CZY], [C_MEMO], [D_TIME]) VALUES(@C_ID, @D_RQ, @C_CRK_LEIBIE,  @C_CZY,  @C_MEMO, @D_TIME)";
                                com.CommandText = sql;
                                Hashtable tableMain = new Hashtable();

                                tableMain.Add("C_ID", dh);
                                tableMain.Add("D_RQ", dt.Rows[0][3]);
                                tableMain.Add("C_CZY", dt.Rows[0][7]);
                                tableMain.Add("C_CRK_LEIBIE", (int)type);
                                tableMain.Add("D_TIME", dtNow);
                                tableMain.Add("C_MEMO", dt.Rows[0][10]);

                                DbParameter[] parmsMain = dbHelper.getParams(tableMain);
                                com.Parameters.Clear();
                                com.Parameters.AddRange(parmsMain);
                                com.ExecuteNonQuery();
                            }
                        }

                        //子表
                        sql = "INSERT INTO [T_OPERATE_INOUT_SUB]([C_ID], [C_CRK_LEIBIE],[C_MATERIEL], [C_PLACE], [DEC_COUNT], [I_FLAG], [C_MACHINE]) " +
         "  VALUES(@C_ID,@C_CRK_LEIBIE, @C_MATERIEL, @C_PLACE, @DEC_COUNT, @I_FLAG, @C_MACHINE)";
                        com.CommandText = sql;
                        Hashtable table2 = new Hashtable();
                        table2.Add("C_ID", dh);
                        table2.Add("C_CRK_LEIBIE", (int)type);
                        table2.Add("C_MATERIEL", dt.Rows[i][4]);
                        table2.Add("C_PLACE", dt.Rows[i][5]);
                        table2.Add("DEC_COUNT", dt.Rows[i][6]);
                        table2.Add("I_FLAG", dt.Rows[i][11]);
                        table2.Add("C_MACHINE", dt.Rows[i][12]);

                        DbParameter[] parms2 = dbHelper.getParams(table2);
                        com.Parameters.Clear();
                        com.Parameters.AddRange(parms2);
                        com.ExecuteNonQuery();

                        switch (type)
                        {
                            case InOutType.MATERIEL_OUT:
                            case InOutType.KNIFE_OUT_USE:
                                sql = "UPDATE [T_OPERATE_STOCKS] SET  [DEC_COUNT]=[DEC_COUNT] - @DEC_COUNT where [C_MATERIEL_ID] = @C_MATERIEL_ID and [C_PLACE] = @C_PLACE";
                                com.CommandText = sql;
                                Hashtable table3 = new Hashtable();
                                table3.Add("C_MATERIEL_ID", dt.Rows[i][4]);
                                table3.Add("C_PLACE", dt.Rows[i][5]);
                                table3.Add("DEC_COUNT", dt.Rows[i][6]);

                                DbParameter[] parms3 = dbHelper.getParams(table3);
                                com.Parameters.Clear();
                                com.Parameters.AddRange(parms3);
                                result = com.ExecuteNonQuery();

                                sql = "DELETE FROM  [T_OPERATE_STOCKS] where [DEC_COUNT] <=0";
                                com.CommandText = sql;
                                com.ExecuteNonQuery();
                                break;

                            case InOutType.MATERIEL_IN:
                                //判断货位原来是否有同样零件
                                sql = "SELECT count(*) FROM T_OPERATE_STOCKS where [C_MATERIEL_ID] = @C_MATERIEL_ID and [C_PLACE] = @C_PLACE";
                                com.CommandText = sql;
                                Hashtable table4 = new Hashtable();
                                table4.Add("C_MATERIEL_ID", dt.Rows[i][4]);
                                table4.Add("C_PLACE", dt.Rows[i][5]);
                                DbParameter[] parms4 = dbHelper.getParams(table4);
                                com.Parameters.Clear();
                                com.Parameters.AddRange(parms4);
                                int stockCount = int.Parse(com.ExecuteScalar().ToString());

                                if (stockCount > 0)
                                {//有同样零件
                                    sql = "UPDATE T_OPERATE_STOCKS SET [DEC_COUNT] = [DEC_COUNT] + @DEC_COUNT, [D_END_TIME] = @D_END_TIME, [C_DH] = @C_DH where [C_MATERIEL_ID] = @C_MATERIEL_ID and [C_PLACE] = @C_PLACE ";
                                    com.CommandText = sql;
                                    Hashtable table5 = new Hashtable();
                                    table5.Add("C_MATERIEL_ID", dt.Rows[i][4]);
                                    table5.Add("C_PLACE", dt.Rows[i][5]);
                                    table5.Add("DEC_COUNT", dt.Rows[i][6]);
                                    table5.Add("D_END_TIME", dt.Rows[i][3]);
                                    table5.Add("C_DH", dh);

                                    DbParameter[] parms5 = dbHelper.getParams(table5);
                                    com.Parameters.Clear();
                                    com.Parameters.AddRange(parms5);
                                    com.ExecuteNonQuery();
                                }
                                else
                                {//无同样零件
                                    sql = "INSERT INTO [T_OPERATE_STOCKS]([C_MATERIEL_ID], [C_PLACE], [DEC_COUNT], [D_END_TIME], [C_DH])  VALUES (@C_MATERIEL_ID, @C_PLACE, @DEC_COUNT, @D_END_TIME, @C_DH)";
                                    com.CommandText = sql;
                                    Hashtable table6 = new Hashtable();
                                    table6.Add("C_MATERIEL_ID", dt.Rows[i][4]);
                                    table6.Add("C_PLACE", dt.Rows[i][5]);
                                    table6.Add("DEC_COUNT", dt.Rows[i][6]);
                                    table6.Add("D_END_TIME", dt.Rows[i][3]);
                                    table6.Add("C_DH", dh);

                                    DbParameter[] parms6 = dbHelper.getParams(table6);
                                    com.Parameters.Clear();
                                    com.Parameters.AddRange(parms6);
                                    com.ExecuteNonQuery();
                                }
                                break;

                            case InOutType.KNIFE_IN_USE:
                                sql = "UPDATE [T_OPERATE_INOUT_SUB] SET  [I_FLAG]=2 where [C_PLACE] = @C_PLACE and [I_FLAG]=1";
                                com.CommandText = sql;
                                Hashtable tablePrid = new Hashtable();
                                tablePrid.Add("C_PLACE", dt.Rows[i][5]);

                                DbParameter[] parmsPrid = dbHelper.getParams(tablePrid);
                                com.Parameters.Clear();
                                com.Parameters.AddRange(parmsPrid);
                                result = com.ExecuteNonQuery();

                                sql = "INSERT INTO [T_OPERATE_STOCKS]([C_MATERIEL_ID], [C_PLACE], [DEC_COUNT], [D_END_TIME], [C_DH])  VALUES (@C_MATERIEL_ID, @C_PLACE, @DEC_COUNT, @D_END_TIME, @C_DH)";
                                com.CommandText = sql;
                                Hashtable table7 = new Hashtable();
                                table7.Add("C_MATERIEL_ID", dt.Rows[i][4]);
                                table7.Add("C_PLACE", dt.Rows[i][5]);
                                table7.Add("DEC_COUNT", dt.Rows[i][6]);
                                table7.Add("D_END_TIME", dt.Rows[i][3]);
                                table7.Add("C_DH", dh);

                                DbParameter[] parms7 = dbHelper.getParams(table7);
                                com.Parameters.Clear();
                                com.Parameters.AddRange(parms7);
                                com.ExecuteNonQuery();
                                break;

                            case InOutType.KNIFE_IN:
                                sql = "INSERT INTO [T_OPERATE_STOCKS]([C_MATERIEL_ID], [C_PLACE], [DEC_COUNT], [D_END_TIME], [C_DH])  VALUES (@C_MATERIEL_ID, @C_PLACE, @DEC_COUNT, @D_END_TIME, @C_DH)";
                                com.CommandText = sql;
                                Hashtable tableKI = new Hashtable();
                                tableKI.Add("C_MATERIEL_ID", dt.Rows[i][4]);
                                tableKI.Add("C_PLACE", dt.Rows[i][5]);
                                tableKI.Add("DEC_COUNT", dt.Rows[i][6]);
                                tableKI.Add("D_END_TIME", dt.Rows[i][3]);
                                tableKI.Add("C_DH", dh);

                                DbParameter[] parmsKI = dbHelper.getParams(tableKI);
                                com.Parameters.Clear();
                                com.Parameters.AddRange(parmsKI);
                                com.ExecuteNonQuery();
                                break;

                            default:
                                break;
                        }
                    }

                    sql = "DELETE FROM  [T_Runing_Dolist] where Dec_ID in (" + string.Join(",", list.ToArray()) + ") and C_DH = @C_DH and I_RUN = 0";
                    com.CommandText = sql;

                    DbParameter[] parmsDel = dbHelper.getParams(table);
                    com.Parameters.Clear();
                    com.Parameters.AddRange(parmsDel);

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
                    dec_id = dec_id + 1;
                    c_id = (dec_id).ToString();
                    Hashtable table2 = new Hashtable();
                    table2.Add("Dec_ID", c_id);
                    table2.Add("I_INOUT", controlType);
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

                if (controlType == 1)
                {
                    DataView dv = dt.DefaultView;
                    DataTable dataTableDistinct = dv.ToTable(true, new string[] { "4" });
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
        /// 联机任务
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="meno"></param>
        /// <param name="station"></param>
        /// <returns></returns>
        public string SaveDolist(DataTable dt, string meno, InOutType type)
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

                string dh;
                DateTime dtNow = DateTime.Now;
                long dec_id = 0;
                string c_id = string.Empty;
                sql = "SELECT MAX(c_id) FROM T_OPERATE_INOUT_MAIN where datediff(day,[D_TIME],getdate()) = 0 AND C_CRK_LEIBIE = '" + (int)type + "'";

                com.CommandText = sql;
                object obj = com.ExecuteScalar();
                dec_id = Convert.IsDBNull(obj) ? 0 : Convert.ToInt64(obj.ToString().Substring(10));
                c_id = Common.GetInOutCode(type) + dtNow.ToString("yyyyMMdd") + (dec_id + 1).ToString().PadLeft(6, '0');
                dh = c_id;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sql = @"INSERT INTO [T_Runing_Dolist]([Dec_ID], [C_DH],  [I_INOUT], [D_RQ], [C_MATERIEL], 
                            [C_PLACE], [DEC_COUNT],  [C_CZY], [I_RUN], [D_AddRQ], [C_MEMO], [I_FLAG], [C_MACHINE])
                            VALUES(@Dec_ID, @C_DH, @I_INOUT, @D_RQ, @C_MATERIEL, @C_PLACE, @DEC_COUNT, @C_CZY, @I_RUN, @D_AddRQ, @C_MEMO, @I_FLAG, @C_MACHINE)";
                    com.CommandText = sql;

                    Hashtable table2 = new Hashtable();
                    table2.Add("Dec_ID", i + 1);
                    table2.Add("C_DH", dh);
                    table2.Add("I_INOUT", (int)type);
                    table2.Add("D_RQ", Convert.ToDateTime(dt.Rows[i][5]).ToString("yyyy-MM-dd"));
                    table2.Add("C_MATERIEL", dt.Rows[i][0]);
                    table2.Add("C_PLACE", dt.Rows[i][4]);
                    table2.Add("DEC_COUNT", dt.Rows[i][3]);
                    table2.Add("C_CZY", dt.Rows[i][6]);
                    table2.Add("I_RUN", 0);
                    table2.Add("D_AddRQ", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                    if (meno == null || string.Empty.Equals(meno.Trim()))
                    {
                        table2.Add("C_MEMO", DBNull.Value);
                    }
                    else
                    {
                        table2.Add("C_MEMO", meno);
                    }

                    if (type.Equals(InOutType.KNIFE_OUT_USE))
                    {
                        table2.Add("I_FLAG", 1);
                        table2.Add("C_MACHINE", dt.Rows[i][7]);
                    }
                    else
                    {
                        table2.Add("I_FLAG", 0);
                        table2.Add("C_MACHINE", DBNull.Value);
                    }

                    DbParameter[] parms2 = dbHelper.getParams(table2);
                    com.Parameters.Clear();
                    com.Parameters.AddRange(parms2);
                    result = com.ExecuteNonQuery();
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
    }
}
