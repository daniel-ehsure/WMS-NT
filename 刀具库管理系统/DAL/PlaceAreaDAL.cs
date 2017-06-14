using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.Common;
using DBCore;
using Model;
using System.Data;
using Util;

namespace DAL
{
    public class PlaceAreaDAL : CommonDAL
    {
        private string tableName = "T_JB_PLACEAREA";

        public string GetNextCode(int length)
        {
            return GetNextCode(tableName, length, string.Empty);
        }

        /// <summary>
        /// 是否重名
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="name"></param>
        /// <param name="meno"></param>
        /// <returns></returns>
        public bool IsExit(string name)
        {
            try
            {
                int count = 0;
                string sql = "";
                sql = "select count(*) from " + tableName + " where c_name = @c_name";

                Hashtable table = new Hashtable();
                table.Add("c_name", name);

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
        /// 获得列表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="name"></param>
        /// <param name="meno"></param>
        /// <returns></returns>
        public DataTable GetList(string name)
        {
            string sql = "select * from " + tableName + " where 1=1 ";

            DataTable dt = new DataTable();
            try
            {
                if (name != null)
                {
                    Hashtable table = new Hashtable();

                    if (name != null)
                    {
                        sql += " and C_NAME like @C_NAME";
                        table.Add("C_NAME", "%" + name + "%");
                    }

                    sql += " order by convert(numeric,c_id)";
                    DbParameter[] parms = dbHelper.getParams(table);
                    dt = dbHelper.GetDataSet(sql, parms);
                }
                else
                {
                    sql += " order by convert(numeric,c_id)";
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

        public bool Save(T_JB_PLACEAREA mo)
        {
            try
            {
                long dec_id = 0;
                string c_id = string.Empty;
                int count = 0;
                string sql = "SELECT max(c_id) FROM " + tableName;

                object obj = dbHelper.GetScalar(sql);
                dec_id = Convert.IsDBNull(obj) ? 0 : Convert.ToInt64(obj);

                sql =
                    "INSERT INTO " + "[" + tableName +
                    "] ([C_ID],[C_NAME],[C_MEMO])" +
                    "VALUES (@C_ID,@C_NAME,@C_MEMO)";

                Hashtable table = new Hashtable();

                mo.C_ID = (dec_id + 1).ToString().PadLeft(4, '0');

                table.Add("C_ID", mo.C_ID);
                table.Add("C_NAME", mo.C_NAME);
                table.Add("C_MEMO", mo.C_MEMO);

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

        public bool Update(T_JB_PLACEAREA mo)
        {
            try
            {
                string sql =
                    "UPDATE  " + "[" + tableName +
                    "] SET [C_NAME] = @C_NAME, " +
                    "[C_MEMO] = @C_MEMO " +
                    "WHERE [C_ID] = @C_ID ";

                Hashtable table = new Hashtable();

                table.Add("C_ID", mo.C_ID);
                table.Add("C_NAME", mo.C_NAME);
                table.Add("C_MEMO", mo.C_MEMO);


                DbParameter[] parms = dbHelper.getParams(table);

                int count = dbHelper.ExecuteCommand(sql, parms);

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
        /// 根据id获得实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T_JB_PLACEAREA GetById(string id)
        {
            T_JB_PLACEAREA mo = new T_JB_PLACEAREA();
            DataRow dr = GetById(tableName, id);

            mo.C_ID = dr["C_ID"].ToString();
            mo.C_NAME = dr["C_NAME"].ToString();
            mo.C_MEMO = dr["C_MEMO"].ToString();

            return mo;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="c_id"></param>
        /// <returns></returns>
        public bool delete(List<String> ids)
        {
            return delete(tableName, ids);
        }

        /// <summary>
        /// 是否重名，不包括自己
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="name"></param>
        /// <param name="meno"></param>
        /// <returns></returns>
        public bool IsExitNotSelf(string name, string id)
        {
            try
            {
                int count = 0;
                string sql = "";
                sql = "select count(*) from " + tableName + " where c_name = @c_name and c_id <> @c_id";

                Hashtable table = new Hashtable();
                table.Add("c_id", id);
                table.Add("c_name", name);

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
        /// 是否使用中
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool IsUse(string id)
        {
            //todo:完善方法
            return false;
        }
    }
}
