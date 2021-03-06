﻿using System;
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
    public class PlaceDAL : CommonDAL
    {
        private DBHelper dbHelper = new SQLDBHelper();
        private string tableName = "T_JB_Place";

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
                sql = "SELECT C_ID, C_NAME, C_NAME as C_NAME_TREE from  T_JB_WAREHOUSE order by c_id";
            }
            else if (grade == 0)
            {
                sql = "SELECT C_ID, C_NAME, C_NAME_TREE, C_PRE_ID, I_GRADE, I_END FROM  T_JB_Place  where c_warehouse = '" + pid + "'  order by c_id";
            }
            else
            {
                sql = "SELECT C_ID, C_NAME, C_NAME_TREE, C_PRE_ID, I_GRADE, I_END FROM  T_JB_Place  where c_pre_id = '" + pid + "'  order by c_id";
            }

            try
            {
                DataTable ds = dbHelper.GetDataSet(sql);
                for (int i = 0; i < ds.Rows.Count; i++)
                {
                    T_JB_Place dm_type = new T_JB_Place();

                    dm_type.C_id = Convert.IsDBNull(ds.Rows[i]["C_ID"]) ? string.Empty : Convert.ToString(ds.Rows[i]["C_ID"]);
                    dm_type.C_name = Convert.IsDBNull(ds.Rows[i]["C_NAME"]) ? string.Empty : Convert.ToString(ds.Rows[i]["C_NAME"]);
                    dm_type.C_name_tree = Convert.IsDBNull(ds.Rows[i]["C_NAME_TREE"]) ? string.Empty : Convert.ToString(ds.Rows[i]["C_NAME_TREE"]);
                    dm_type.C_pre_id = pid.Equals("0") ? "0" : Convert.IsDBNull(ds.Rows[i]["C_PRE_ID"]) ? string.Empty : Convert.ToString(ds.Rows[i]["C_PRE_ID"]);
                    dm_type.I_end = pid.Equals("0") ? 0 : Convert.IsDBNull(ds.Rows[i]["I_END"]) ? 0 : Convert.ToInt32(ds.Rows[i]["I_END"]);
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
        public DataTable getList(string pid, string id, string memo, int end, int use, int grade)
        {
            string sql;
            DataTable dt = new DataTable();

            try
            {
                if (grade < 0)
                {
                    sql = "SELECT C_ID, C_NAME, '' as C_AREA, 1 AS I_INUSE, 0 AS I_END, 0 AS I_GRADE from  T_JB_WAREHOUSE order by c_id";
                    dt = dbHelper.GetDataSet(sql);
                }
                else
                {
                    sql = " SELECT a.[C_ID], a.[C_NAME], b.[C_NAME],a.[I_INUSE], a.[I_END], a.[I_GRADE] FROM [T_JB_Place] a left join [T_JB_PlaceArea] b on a.C_AREA = b.C_ID where 1=1 ";


                    if (pid != null || id != null || memo != null || end != -1 || use != -1)
                    {
                        Hashtable table = new Hashtable();
                        if (pid != null)
                        {
                            sql += " and a.C_PRE_ID = @C_PRE_ID";
                            table.Add("C_PRE_ID", pid);
                        }
                        if (id != null)
                        {
                            sql += " and a.C_ID like @C_ID";
                            table.Add("C_ID", id + "%");
                        }

                        if (end != -1)
                        {
                            sql += " and a.I_END = @I_END";
                            table.Add("I_END", end);
                        }

                        if (use != -1)
                        {
                            sql += " and a.I_INUSE = @I_INUSE";
                            table.Add("I_INUSE", use);
                        }

                        sql += " order by convert(numeric,a.c_id) asc";
                        DbParameter[] parms = dbHelper.getParams(table);
                        dt = dbHelper.GetDataSet(sql, parms);
                    }
                    else
                    {
                        sql += " order by convert(numeric,a.c_id) asc";
                        dt = dbHelper.GetDataSet(sql);
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
            return dt;

        }

        /// <summary>
        /// 根据id和名称获得货位（最小控制单元）
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable getListByIN(string id, string name, string placeArea, InOutType type)
        {
            string sql;

            if (type.Equals(InOutType.MATERIEL_IN))
            {
                sql = " SELECT [C_ID], [C_NAME], case when b.num > 0 then '是' else '否' end FROM [T_JB_Place] a " +
                                " left join (select C_PLACE, count(*) num from [T_OPERATE_STOCKS] group by C_PLACE) b on a.C_ID = b.C_PLACE where I_END=1 and I_INUSE = 1 " +
                                " and C_ID not in (select C_PLACE from T_RUNING_DOLIST) ";
            }
            else
            {
                sql = " SELECT [C_ID], [C_NAME], case when b.num > 0 then '是' else '否' end FROM [T_JB_Place] a " +
                " left join (select C_PLACE, count(*) num from [T_OPERATE_STOCKS] group by C_PLACE) b on a.C_ID = b.C_PLACE where I_END=1 and I_INUSE = 1 " +
                " and C_ID not in (select C_PLACE from T_RUNING_DOLIST) " +
                " and C_ID not in (select C_PLACE from T_OPERATE_INOUT_SUB where I_FLAG = 1) " +
                " and C_ID not in (select C_PLACE from T_OPERATE_STOCKS) ";
            }

            DataTable dt = new DataTable();
            try
            {
                if (id != null || name != null || placeArea != null)
                {
                    Hashtable table = new Hashtable();
                    if (id != null)
                    {
                        sql += " and C_ID like @id";
                        table.Add("id", id + "%");
                    }

                    if (name != null)
                    {
                        sql += " and C_NAME like @C_NAME";
                        table.Add("C_NAME", "%" + name + "%");
                    }

                    if (placeArea != null)
                    {
                        sql += " and C_AREA = @C_AREA";
                        table.Add("C_AREA", placeArea);
                    }

                    sql += " order by convert(numeric,c_id) asc";
                    DbParameter[] parms = dbHelper.getParams(table);
                    dt = dbHelper.GetDataSet(sql, parms);
                }
                else
                {
                    sql += " order by convert(numeric,c_id) asc";
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

                sql = "SELECT max(right(c_id,2)) FROM T_JB_Place where C_PRE_ID = '" + dm_type.C_pre_id + "'";

                object obj = dbHelper.GetScalar(sql);
                dec_id = Convert.IsDBNull(obj) ? 0 : Convert.ToInt64(obj);

                if (dm_type.I_grade == 1)
                {
                    sql = "SELECT C_NAME FROM T_JB_WAREHOUSE where C_ID = '" + dm_type.C_pre_id + "'";
                }
                else
                {
                    sql = "SELECT C_NAME FROM T_JB_Place where C_ID = '" + dm_type.C_pre_id + "'";
                }

                object obj1 = dbHelper.GetScalar(sql);
                string preName = obj1.ToString();

                sql = "INSERT INTO T_JB_Place ( C_ID, C_NAME, C_NAME_TREE, C_PRE_ID, I_GRADE, I_END, C_WAREHOUSE, I_INUSE, I_LENGTH, I_WIDTH, C_MEMO) " +
                               "values (@C_ID,@C_NAME,@C_NAME_TREE,@C_PRE_ID,@I_GRADE,@I_END,@C_WAREHOUSE, @I_INUSE, @I_LENGTH, @I_WIDTH, @C_MEMO)";

                Hashtable table = new Hashtable();

                dec_id = dec_id + 1;
                c_id = dm_type.C_pre_id.Equals("0") ? dec_id.ToString().PadLeft(2, '0') : dm_type.C_pre_id + dec_id.ToString().PadLeft(2, '0');

                if (dm_type.I_grade == 1)
                {
                    table.Add("C_WAREHOUSE", dm_type.C_pre_id);
                }
                else
                {
                    table.Add("C_WAREHOUSE", DBNull.Value);
                }

                table.Add("c_id", c_id);
                table.Add("C_NAME", preName + dm_type.C_name);
                table.Add("C_NAME_TREE", dm_type.C_name);
                table.Add("C_PRE_ID", dm_type.C_pre_id);
                table.Add("I_GRADE", dm_type.I_grade);
                table.Add("I_END", dm_type.I_end);
                table.Add("I_INUSE", dm_type.I_inuse);
                table.Add("C_MEMO", dm_type.C_memo);
                table.Add("I_LENGTH", dm_type.I_length);
                table.Add("I_WIDTH", dm_type.I_width);

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

                if (grade == 0)
                {
                    sql = "SELECT C_NAME FROM T_JB_WAREHOUSE where C_ID = '" + pid + "'";
                }
                else
                {
                    sql = "SELECT C_NAME FROM T_JB_Place where C_ID = '" + pid + "'";
                }

                object obj1 = dbHelper.GetScalar(sql);
                string preName = obj1.ToString();

                sql = "INSERT INTO T_JB_Place ( C_ID, C_NAME,C_NAME_TREE, C_PRE_ID, I_GRADE, I_END, C_WAREHOUSE, I_INUSE, I_LENGTH, I_WIDTH) " +
                               "values (@C_ID,@C_NAME,@C_NAME_TREE,@C_PRE_ID,@I_GRADE,@I_END,@C_WAREHOUSE, @I_INUSE, @I_LENGTH, @I_WIDTH)";

                com.CommandText = sql;

                int num1 = int.Parse(list[0][0].ToString());

                for (int i = 0; i < num1; i++)
                {
                    AddPlace(list, 0, i, pid, grade + 1, com, preName);
                }

                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                conn.Close();
                Log.write(ex.Message + "\r\n" + ex.StackTrace);
                //throw ex;
                return false;
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
        private void AddPlace(List<List<object>> list, int count, int num, string pid, int grade, DbCommand com, string preName)
        {
            string id;

            Hashtable table = new Hashtable();
            if (grade == 1)
            {
                table.Add("C_PRE_ID", pid);
                table.Add("C_WAREHOUSE", pid);
                id = pid + (num + 1).ToString().PadLeft(2, '0');
            }
            else
            {
                table.Add("C_PRE_ID", pid);
                table.Add("C_WAREHOUSE", DBNull.Value);
                id = pid + (num + 1).ToString().PadLeft(2, '0');
            }

            string nameTree = (num + 1).ToString() + list[count][1].ToString();
            table.Add("C_ID", id);
            table.Add("C_NAME", preName + nameTree);
            table.Add("C_NAME_TREE", nameTree);
            table.Add("I_GRADE", grade);
            int len = 0;
            int.TryParse(list[count][2].ToString(), out len);
            table.Add("I_LENGTH", len);
            int wid = 0;
            int.TryParse(list[count][3].ToString(), out wid);
            table.Add("I_WIDTH", wid);
            table.Add("I_END", int.Parse(list[count][4].ToString()));
            table.Add("I_INUSE", 1);

            DbParameter[] parms = dbHelper.getParams(table);

            com.Parameters.Clear();
            com.Parameters.AddRange(parms);
            com.ExecuteNonQuery();

            if (count + 1 < list.Count)
            {
                count++;
                for (int i = 0; i < int.Parse(list[count][0].ToString()); i++)
                {
                    AddPlace(list, count, i, id, grade + 1, com, preName + nameTree);
                }
            }
        }

        /// <summary>
        /// 货位是否有下级
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
        /// 货位是否被使用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool isInUse(string id)
        {
            try
            {
                string sql = "";
                sql = " select count(*) from T_OPERATE_STOCKS where C_PLACE = @id ";

                Hashtable table = new Hashtable();
                table.Add("id", id);

                DbParameter[] parms = dbHelper.getParams(table);
                object obj = dbHelper.GetScalar(sql, parms);
                int count1 = Convert.IsDBNull(obj) ? 0 : Convert.ToInt32(obj);

                sql = " select count(*) from T_Runing_Dolist where C_PLACE = @id ";

                Hashtable table2 = new Hashtable();
                table2.Add("id", id);

                DbParameter[] parms2 = dbHelper.getParams(table2);
                object obj2 = dbHelper.GetScalar(sql, parms2);
                int count2 = Convert.IsDBNull(obj2) ? 0 : Convert.ToInt32(obj2);

                sql = " select count(*) from T_OPERATE_INOUT_SUB where C_PLACE = @id ";

                Hashtable table4 = new Hashtable();
                table4.Add("id", id);

                DbParameter[] parms4 = dbHelper.getParams(table4);
                object obj4 = dbHelper.GetScalar(sql, parms4);
                int count4 = Convert.IsDBNull(obj2) ? 0 : Convert.ToInt32(obj4);

                if (count1 > 0 || count2 > 0 || count4 > 0)
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
            T_JB_Place place = null;
            string sql = " SELECT *, (select count(*) from T_JB_Place where C_PRE_ID = '" + id + "') I_CHILDREN from  T_JB_Place where C_ID = '" + id + "'";
            try
            {
                DataTable dt = dbHelper.GetDataSet(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    place = new T_JB_Place();
                    place.C_id = dt.Rows[0]["C_ID"].ToString();
                    place.C_name = dt.Rows[0]["C_NAME"].ToString();
                    place.C_pre_id = DBNull.Value.Equals(dt.Rows[0]["C_PRE_ID"]) ? "0" : dt.Rows[0]["C_PRE_ID"].ToString();
                    place.I_length = DBNull.Value.Equals(dt.Rows[0]["I_LENGTH"]) ? 0 : Convert.ToInt32(dt.Rows[0]["I_LENGTH"]);
                    place.I_width = DBNull.Value.Equals(dt.Rows[0]["I_WIDTH"]) ? 0 : Convert.ToInt32(dt.Rows[0]["I_WIDTH"]);
                    place.I_inuse = DBNull.Value.Equals(dt.Rows[0]["I_INUSE"]) ? 0 : Convert.ToInt32(dt.Rows[0]["I_INUSE"]);
                    place.I_end = DBNull.Value.Equals(dt.Rows[0]["I_END"]) ? 0 : Convert.ToInt32(dt.Rows[0]["I_END"]);
                    place.I_children = DBNull.Value.Equals(dt.Rows[0]["I_CHILDREN"]) ? 0 : Convert.ToInt32(dt.Rows[0]["I_CHILDREN"]);
                    place.C_memo = DBNull.Value.Equals(dt.Rows[0]["C_MEMO"]) ? string.Empty : dt.Rows[0]["C_MEMO"].ToString();
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
            return place;
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
        /// 更新货位
        /// </summary>
        /// <param name="dm_type">货位</param>
        /// <returns></returns>
        public bool update(T_JB_Place place)
        {
            try
            {
                int count = 0;
                string sql = "UPDATE T_JB_Place SET C_NAME=@C_NAME, I_END=@I_END, I_INUSE=@I_INUSE, I_LENGTH=@I_LENGTH, I_WIDTH=@I_WIDTH, C_MEMO=@C_MEMO " +
                             "WHERE C_ID=@C_ID ";
                Hashtable table = new Hashtable();
                table.Add("C_ID", place.C_id);
                table.Add("C_NAME", place.C_name);
                table.Add("I_END", place.I_end);
                table.Add("I_INUSE", place.I_inuse);
                table.Add("I_LENGTH", place.I_length);
                table.Add("I_WIDTH", place.I_width);
                table.Add("C_MEMO", place.C_memo);
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


        /// <summary>
        /// 更新货位的货区
        /// </summary>
        /// <param name="dm_type">类型信息</param>
        /// <returns></returns>
        public bool UpdateArea(List<String> places, string placeArea)
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

                sql = "UPDATE T_JB_Place SET C_AREA=@C_AREA " +
                             "WHERE C_ID=@C_ID ";

                com.CommandText = sql;

                for (int i = 0; i < places.Count; i++)
                {
                    Hashtable table = new Hashtable();
                    table.Add("C_ID", places[i]);
                    table.Add("C_AREA", placeArea);
                    DbParameter[] parms = dbHelper.getParams(table);
                    com.Parameters.Clear();
                    com.Parameters.AddRange(parms);
                    com.ExecuteNonQuery();
                }

                tran.Commit();
                return true;
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
        /// 根据上级获得信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetByPreId(string preId)
        {
            DataTable dt = new DataTable();

            string sql = " SELECT C_ID as id, C_NAME as name from  T_JB_Place where C_PRE_ID = '" + preId + "'";
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

        /// <summary>
        /// 根据上级、货区、类型获得信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetPlaceList(string preId, string area, int type)
        {
            string sql = "SELECT a.[C_ID], a.[C_NAME], [I_INUSE], case  [I_INUSE] when 1 then '可用' else '不可用' end as C_INUSE, " +
                         "  [I_LENGTH], [I_WIDTH], b.c_name as c_type FROM [T_JB_PLACE] a left join t_jb_placeArea b on a.C_AREA = b.c_id  where I_END=1";
            DataTable dt = new DataTable();
            try
            {
                Hashtable table = new Hashtable();
                if (preId != null)
                {
                    sql += " and a.C_PRE_ID like '" + preId + "%'";
                }
                if (area != null)
                {
                    sql += " and a.c_area =@area ";

                    table.Add("area", area);
                }
                if (type == 2)
                {
                    sql += " and (a.C_AREA = '' or a.C_AREA is null)";
                }
                else if (type == 3)
                {
                    sql += " and (a.C_AREA != '' or a.C_AREA is not null)";
                }

                DbParameter[] parms = dbHelper.getParams(table);
                sql += " order by a.C_ID";
                dt = dbHelper.GetDataSet(sql, parms);
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

        public string GetNextCode(string pid, int length)
        {
            return GetNextCode(tableName, length, " and C_PRE_ID = '" + pid + "'");
        }

        public DataTable GetEmptyPlace(string area)
        {
            string sql;

            sql = " SELECT [C_ID], [C_NAME] FROM [T_JB_Place] " +
                "  where I_END=1 and I_INUSE = 1 " +
                " and C_ID not in (select C_PLACE from T_RUNING_DOLIST) " +
                " and C_ID not in (select C_PLACE from T_OPERATE_STOCKS) ";

            DataTable dt = new DataTable();
            try
            {
                Hashtable table = new Hashtable();
                if (area != null)
                {
                    sql += " and C_AREA = @C_AREA";
                    table.Add("C_AREA", area);
                }

                sql += " order by convert(numeric,c_id) asc";
                DbParameter[] parms = dbHelper.getParams(table);
                dt = dbHelper.GetDataSet(sql, parms);
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
