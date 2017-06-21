using System;
using System.Collections.Generic;
using System.Text;
using Util;
using System.Data.Common;
using System.Data;
using Model;
using System.Collections;
using DBCore;

namespace DAL
{
    public class PlaceDAL
    {
        private DBHelper dbHelper = new SQLDBHelper();
        /// <summary>
        /// 根据类型编号获得子类型信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<T_JB_Place> getAllChild(string pid, int grade)
        {
            List<T_JB_Place> list = new List<T_JB_Place>();
            string sql;
            if (pid.Equals("0"))
            {
                sql = "SELECT C_ID, C_NAME from  T_JB_WAREHOUSE order by c_id";
            }
            else if (grade == 0)
            {
                sql = "SELECT C_ID, C_NAME, C_PRE_ID, I_GRADE FROM  T_JB_Place  where c_warehouse = '" + pid + "'  order by c_id";
            }
            else
            {
                sql = "SELECT C_ID, C_NAME, C_PRE_ID, I_GRADE FROM  T_JB_Place  where c_pre_id = '" + pid + "'  order by c_id";
            }

            try
            {
                DataTable ds = dbHelper.GetDataSet(sql);
                for (int i = 0; i < ds.Rows.Count; i++)
                {
                    T_JB_Place dm_type = new T_JB_Place();

                    dm_type.C_id = Convert.IsDBNull(ds.Rows[i]["C_ID"]) ? string.Empty : Convert.ToString(ds.Rows[i]["C_ID"]);
                    dm_type.C_name = Convert.IsDBNull(ds.Rows[i]["C_NAME"]) ? string.Empty : Convert.ToString(ds.Rows[i]["C_NAME"]);
                    dm_type.C_pre_id = pid.Equals("0") ? "0" : Convert.IsDBNull(ds.Rows[i]["C_PRE_ID"]) ? string.Empty : Convert.ToString(ds.Rows[i]["C_PRE_ID"]);

                    dm_type.I_grade = pid.Equals("0") ? 0 : Convert.IsDBNull(ds.Rows[i]["I_GRADE"]) ? 0 : Convert.ToInt32(ds.Rows[i]["I_GRADE"]);
                    list.Add(dm_type);
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
            return list;
        }

        /// <summary>
        /// 获得全部信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="name"></param>
        /// <param name="MEMO"></param>
        /// <returns></returns>
        public DataTable getList(string pid, string name, string memo, int end)
        {
            string sql = " SELECT [C_ID], [C_NAME], [C_PRE_ID], [I_GRADE],[I_END], [I_END] FROM [T_JB_Place] where 1=1 ";
            DataTable dt = new DataTable();
            try
            {
                if (pid != null || name != null || memo != null || end != -1)
                {
                    Hashtable table = new Hashtable();
                    if (pid != null)
                    {
                        sql += " and C_PRE_ID = @C_PRE_ID";
                        table.Add("C_PRE_ID", pid);
                    }
                    if (name != null)
                    {
                        sql += " and C_NAME like @C_NAME";
                        table.Add("C_NAME", "%" + name + "%");
                    }

                    if (end != -1)
                    {
                        sql += " and I_END = @I_END";
                        table.Add("I_END", end);
                    }

                    sql += " order by convert(numeric,c_id) desc";
                    DbParameter[] parms = dbHelper.getParams(table);
                    dt = dbHelper.GetDataSet(sql, parms);
                }
                else
                {
                    sql += " order by convert(numeric,c_id) desc";
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
        /// 是否重名
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="name"></param>
        /// <param name="MEMO"></param>
        /// <returns></returns>
        public bool IsExit(string pid, string name)
        {
            try
            {
                int count = 0;
                string sql = "";
                sql = "select count(*) from T_JB_Place where c_name = @c_name and C_PRE_ID = @pid ";

                Hashtable table = new Hashtable();
                table.Add("c_name", name);
                table.Add("pid", pid);

                DbParameter[] parms = dbHelper.getParams(table);
                object obj = dbHelper.GetScalar(sql, parms);
                count = Convert.IsDBNull(obj) ? 0 : Convert.ToInt32(obj);


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


        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="name"></param>
        /// <param name="MEMO"></param>
        /// <returns></returns>
        public string Save(T_JB_Place dm_type)
        {
            try
            {
                long dec_id = 0;
                string c_id = null;
                int count = 0;
                string sql = "";

                sql = "SELECT max(right(c_id,4)) FROM T_JB_Place where C_PRE_ID = '" + dm_type.C_pre_id + "'";

                object obj = dbHelper.GetScalar(sql);
                dec_id = Convert.IsDBNull(obj) ? 0 : Convert.ToInt64(obj);


                sql = "INSERT INTO T_JB_Place ( C_ID, C_NAME, C_PRE_ID, I_GRADE, I_END, I_END, C_MEMO) " +
                               "values (@C_ID,@C_NAME,@C_PRE_ID,@I_GRADE,@I_END,@I_END,@C_MEMO)";
                Hashtable table = new Hashtable();

                dec_id = dec_id + 1;
                c_id = dm_type.C_pre_id.Equals("0") ? dec_id.ToString().PadLeft(4, '0') : dm_type.C_pre_id + dec_id.ToString().PadLeft(4, '0');

                table.Add("c_id", c_id);
                table.Add("C_NAME", dm_type.C_name);
                table.Add("C_PRE_ID", dm_type.C_pre_id);
                table.Add("I_GRADE", dm_type.I_grade);
                table.Add("I_END", dm_type.I_end);
                table.Add("I_END", dm_type.I_end);
                table.Add("C_MEMO", dm_type.C_memo);

                DbParameter[] parms = dbHelper.getParams(table);

                count = dbHelper.ExecuteCommand(sql, parms);
                if (count > 0)
                {
                    return c_id;
                }
                else
                {
                    return null;
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

        /// <summary>
        /// 批量保存
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="name"></param>
        /// <param name="MEMO"></param>
        /// <returns></returns>
        public bool SaveList(List<List<object>> list, string pid, int grade)
        {
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

                sql = "INSERT INTO T_JB_Place ( C_ID, C_NAME, C_PRE_ID, I_GRADE, I_END, C_WAREHOUSE, I_INUSE, I_LENGTH, I_WIDTH) " +
                               "values (@C_ID,@C_NAME,@C_PRE_ID,@I_GRADE,@I_END,@C_WAREHOUSE, @I_INUSE, @I_LENGTH, @I_WIDTH)";

                com.CommandText = sql;

                int num1 = int.Parse(list[0][0].ToString());

                for (int i = 0; i < num1; i++)
                {
                    int n = int.Parse(list[i][0].ToString());

                    AddPlace(list, 0, i, pid, grade + 1, com);
                }

                tran.Commit();
                return true;
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
        /// 增加货位
        /// </summary>
        /// <param name="list"></param>
        /// <param name="count"></param>
        /// <param name="num"></param>
        /// <param name="upId"></param>
        private void AddPlace(List<List<object>> list, int count, int num, string pid, int grade, DbCommand com)
        {
            string id;

            Hashtable table = new Hashtable();
            if (grade==1)
            {
                table.Add("C_PRE_ID", DBNull.Value);
                table.Add("C_WAREHOUSE", pid);
                id = num.ToString().PadLeft(2, '0');
            }
            else
            {
                table.Add("C_PRE_ID", pid);
                table.Add("C_WAREHOUSE", DBNull.Value);
                id = pid + num.ToString().PadLeft(2, '0');
            }

            table.Add("C_ID", id);
            table.Add("C_NAME", list[count][1].ToString());
            table.Add("I_GRADE", grade);
            table.Add("I_LENGTH", int.Parse(list[count][2].ToString()));
            table.Add("I_WIDTH", int.Parse(list[count][3].ToString()));
            table.Add("I_END", int.Parse(list[count][4].ToString()));
            table.Add("I_INUSE", 1);

            DbParameter[] parms = dbHelper.getParams(table);

            com.Parameters.Clear();
            com.Parameters.AddRange(parms);
            com.ExecuteNonQuery();

            if (count <= list.Count)
            {
                count++;
                for (int i = 0; i < count; i++)
                {
                    AddPlace(list, count, i, id, grade, com);
                }
            }
        }

        /// <summary>
        /// 物料类型是否有子类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool isHaveChild(string id)
        {
            try
            {
                int count = 0;
                string sql = "";
                sql = " select count(*) from T_JB_Place where c_pre_id =@id";

                Hashtable table = new Hashtable();
                table.Add("id", id);

                DbParameter[] parms = dbHelper.getParams(table);
                object obj = dbHelper.GetScalar(sql, parms);
                count = Convert.IsDBNull(obj) ? 0 : Convert.ToInt32(obj);


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
        /// <summary>
        /// 物料类型是否被使用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool isInUse(string id)
        {
            try
            {
                int count = 0;
                string sql = "";
                sql = " select count(*) from T_JB_MATERIEL where C_TYPE = @id ";

                Hashtable table = new Hashtable();
                table.Add("id", id);

                DbParameter[] parms = dbHelper.getParams(table);
                object obj = dbHelper.GetScalar(sql, parms);
                count = Convert.IsDBNull(obj) ? 0 : Convert.ToInt32(obj);


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

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="c_id"></param>
        /// <returns></returns>
        public bool delete(List<String> lists)
        {
            int count = 0;
            try
            {
                string sql = "delete from T_JB_Place where C_ID in ";
                string ccid = "(";
                for (int i = 0; i < lists.Count; i++)
                {
                    ccid += " '" + lists[i] + "'";
                    if (i < lists.Count - 1)
                    {
                        ccid += ",";
                    }
                }
                ccid += " )";
                sql += ccid;
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

        /// <summary>
        /// 根据编码获得信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public T_JB_Place getById(string id)
        {
            T_JB_Place area = null;
            string sql = " SELECT * from  T_JB_Place where C_ID = '" + id + "'";
            try
            {
                DataTable dt = dbHelper.GetDataSet(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    area = new T_JB_Place();
                    area.C_id = dt.Rows[0]["C_ID"].ToString();
                    area.C_name = dt.Rows[0]["C_NAME"].ToString();
                    area.C_pre_id = DBNull.Value.Equals(dt.Rows[0]["C_PRE_ID"]) ? "0" : dt.Rows[0]["C_PRE_ID"].ToString();
                    area.I_grade = DBNull.Value.Equals(dt.Rows[0]["I_GRADE"]) ? 0 : Convert.ToInt32(dt.Rows[0]["I_GRADE"]);
                    area.I_end = DBNull.Value.Equals(dt.Rows[0]["I_END"]) ? 0 : Convert.ToInt32(dt.Rows[0]["I_END"]);
                    area.I_end = DBNull.Value.Equals(dt.Rows[0]["I_END"]) ? 0 : Convert.ToInt32(dt.Rows[0]["I_END"]);
                    area.C_memo = DBNull.Value.Equals(dt.Rows[0]["c_MEMO"]) ? string.Empty : dt.Rows[0]["c_MEMO"].ToString();
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
            return area;
        }

        /// <summary>
        /// 是否重名
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="name"></param>
        /// <param name="MEMO"></param>
        /// <returns></returns>
        public bool IsExit(string pid, string name, string id)
        {
            try
            {
                int count = 0;
                string sql = "";
                sql = "select count(*) from T_JB_Place where c_name = @c_name and C_PRE_ID = @pid and C_ID <> @id";

                Hashtable table = new Hashtable();
                table.Add("c_name", name);
                table.Add("pid", pid);
                table.Add("id", id);

                DbParameter[] parms = dbHelper.getParams(table);
                object obj = dbHelper.GetScalar(sql, parms);
                count = Convert.IsDBNull(obj) ? 0 : Convert.ToInt32(obj);


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

        /// <summary>
        /// 更新类型
        /// </summary>
        /// <param name="dm_type">类型信息</param>
        /// <returns></returns>
        public bool update(T_JB_Place dm_type)
        {
            try
            {
                int count = 0;
                string sql = "UPDATE T_JB_Place SET C_NAME=@C_NAME, I_END=@I_END, C_MEMO=@C_MEMO " +
                             "WHERE C_ID=@C_ID ";
                Hashtable table = new Hashtable();
                table.Add("C_ID", dm_type.C_id);
                table.Add("C_NAME", dm_type.C_name);
                table.Add("I_END", dm_type.I_end);
                table.Add("C_MEMO", dm_type.C_memo);
                DbParameter[] parms = dbHelper.getParams(table);

                count = dbHelper.ExecuteCommand(sql, parms);
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
