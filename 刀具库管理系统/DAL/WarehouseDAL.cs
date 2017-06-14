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
    public class WarehouseDAL : CommonDAL
    {
        private string tableName = "T_JB_WAREHOUSE";

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

        public bool Save(T_JB_WAREHOUSE mo)
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
                    "] ([C_ID],[C_NAME],[C_TYPE] ,[C_COM],[C_BAUDRATE],[C_IP_ADDRESS],[C_PORT],[C_WRITE_PORT],[C_READ_PORT],[I_AUTO],[I_IN_MOBILE],[I_OUT_MOBILE])" +
                    "VALUES (@C_ID,@C_NAME,@C_TYPE,@C_COM,@C_BAUDRATE,@C_IP_ADDRESS,@C_PORT,@C_WRITE_PORT,@C_READ_PORT,@I_AUTO,@I_IN_MOBILE,@I_OUT_MOBILE)";

                Hashtable table = new Hashtable();

                mo.C_ID = (dec_id + 1).ToString().PadLeft(2, '0');

                table.Add("C_ID", mo.C_ID);
                table.Add("C_NAME", mo.C_NAME);
                table.Add("C_TYPE", mo.C_TYPE);
                table.Add("C_COM", mo.C_COM);
                table.Add("C_BAUDRATE", mo.C_BAUDRATE);
                table.Add("C_IP_ADDRESS", mo.C_IP_ADDRESS);
                table.Add("C_PORT", mo.C_PORT);
                table.Add("C_WRITE_PORT", mo.C_WRITE_PORT);
                table.Add("C_READ_PORT", mo.C_READ_PORT);
                table.Add("I_AUTO", mo.I_AUTO);
                table.Add("I_IN_MOBILE", mo.I_IN_MOBILE);
                table.Add("I_OUT_MOBILE", mo.I_OUT_MOBILE);

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

        public bool Update(T_JB_WAREHOUSE mo)
        {
            try
            {
                string sql =
                    "UPDATE  " + "[" + tableName +
                    "] SET [C_NAME] = @C_NAME, " +
                    "[C_TYPE] = @C_TYPE, " +
                    "[C_COM] = @C_COM, " +
                    "[C_BAUDRATE] = @C_BAUDRATE, " +
                    "[C_IP_ADDRESS] = @C_IP_ADDRESS, " +
                    "[C_PORT] = @C_PORT, " +
                    "[C_WRITE_PORT] = @C_WRITE_PORT, " +
                    "[C_READ_PORT] = @C_READ_PORT, " +
                    "[I_AUTO] = @I_AUTO, " +
                    "[I_IN_MOBILE] = @I_IN_MOBILE, " +
                    "[I_OUT_MOBILE] = @I_OUT_MOBILE " +
                    "WHERE [C_ID] = @C_ID ";

                Hashtable table = new Hashtable();

                table.Add("C_ID", mo.C_ID);
                table.Add("C_NAME", mo.C_NAME);
                table.Add("C_TYPE", mo.C_TYPE);
                table.Add("C_COM", mo.C_COM);
                table.Add("C_BAUDRATE", mo.C_BAUDRATE);
                table.Add("C_IP_ADDRESS", mo.C_IP_ADDRESS);
                table.Add("C_PORT", mo.C_PORT);
                table.Add("C_WRITE_PORT", mo.C_WRITE_PORT);
                table.Add("C_READ_PORT", mo.C_READ_PORT);
                table.Add("I_AUTO", mo.I_AUTO);
                table.Add("I_IN_MOBILE", mo.I_IN_MOBILE);
                table.Add("I_OUT_MOBILE", mo.I_OUT_MOBILE);

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
        public T_JB_WAREHOUSE GetById(string id)
        {
            T_JB_WAREHOUSE mo = new T_JB_WAREHOUSE();
            DataRow dr = GetById(tableName, id);

            mo.C_ID = dr["C_ID"].ToString();
            mo.C_NAME = dr["C_NAME"].ToString();
            mo.C_COM = dr["C_COM"].ToString();
            mo.C_BAUDRATE = dr["C_BAUDRATE"].Equals(DBNull.Value) ? string.Empty : dr["C_BAUDRATE"].ToString();
            mo.C_PORT = dr["C_PORT"].Equals(DBNull.Value) ? string.Empty : dr["C_PORT"].ToString();
            mo.C_WRITE_PORT = dr["C_WRITE_PORT"].Equals(DBNull.Value) ? string.Empty : dr["C_WRITE_PORT"].ToString();
            mo.C_READ_PORT = dr["C_READ_PORT"].Equals(DBNull.Value) ? string.Empty : dr["C_READ_PORT"].ToString();
            mo.C_IP_ADDRESS = dr["C_IP_ADDRESS"].Equals(DBNull.Value) ? string.Empty : dr["C_IP_ADDRESS"].ToString();
            mo.C_TYPE = dr["C_TYPE"].Equals(DBNull.Value) ? string.Empty : dr["C_TYPE"].ToString();
            mo.I_AUTO = int.Parse(dr["I_AUTO"].ToString());
            mo.I_IN_MOBILE = int.Parse(dr["I_IN_MOBILE"].ToString());
            mo.I_OUT_MOBILE = int.Parse(dr["I_OUT_MOBILE"].ToString());

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
