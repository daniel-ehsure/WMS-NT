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
  public class CommonDAL
    {
        private DBHelper dbHelper = new SQLDBHelper();

        /// <summary>
        /// 是否重名
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="name"></param>
        /// <param name="meno"></param>
        /// <returns></returns>
        public bool isExit(string tableName, string name)
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
        /// 获得全部信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="name"></param>
        /// <param name="meno"></param>
        /// <returns></returns>
        public DataTable getList(string tableName)
        {
            string sql = "select * from " + tableName;
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

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="c_id"></param>
        /// <returns></returns>
        public bool delete(string tableName, List<String> ids)
        {
            int count = 0;
            try
            {
                string sql = "delete from " + tableName + " where C_ID in ";
                string ccid = "(";
                for (int i = 0; i < ids.Count; i++)
                {
                    ccid += " '" + ids[i] + "'";
                    if (i < ids.Count - 1)
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
        /// 是否重名
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="name"></param>
        /// <param name="meno"></param>
        /// <returns></returns>
        public bool isExit(string tableName, string name, string id)
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
        /// 获得全部信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="name"></param>
        /// <param name="meno"></param>
        /// <returns></returns>
        public DataTable getListForEmpty(string tableName, string name, string meno)
        {
            string sql = "select * from " + tableName + " where 1=1";
            DataTable dt = new DataTable();
            try
            {
                if (name != null || meno != null)
                {
                    Hashtable table = new Hashtable();

                    if (name != null)
                    {
                        sql += " and C_NAME like @C_NAME";
                        table.Add("C_NAME", "%" + name + "%");
                    }

                    if (meno != null)
                    {
                        sql += " and c_meno like @c_meno";
                        table.Add("c_meno", "%" + meno + "%");
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
    }
}
