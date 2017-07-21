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
    public class MachineDAL : CommonDAL
    {
        private string tableName = "T_JB_MACHINE";

        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataTable GetList()
        {
            string sql = "select C_ID from " + tableName;

            DataTable dt = new DataTable();
            try
            {
                sql += " order by c_id";
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

        public bool Save(string id)
        {
            try
            {
                string sql = "select count(*) from " + tableName + " where C_ID = '" + id + "'";


                object obj2 = dbHelper.GetScalar(sql);
                int count = Convert.IsDBNull(obj2) ? 0 : Convert.ToInt32(obj2);

                if (count > 0)
                {
                    return true;
                }
                else
                {
                    sql =
                    "INSERT INTO " + "[" + tableName +
                    "] ([C_ID])" +
                    "VALUES (@C_ID)";

                    Hashtable table = new Hashtable();

                    table.Add("C_ID", id);

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
