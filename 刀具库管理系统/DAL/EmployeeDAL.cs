using System;
using System.Collections.Generic;
using Util;
using System.Collections;
using System.Data.Common;
using System.Data;
using Model;
using DBCore;

namespace DAL
{
    public class EmployeeDAL
    {
        private DBHelper dbHelper = new SQLDBHelper();

        /// <summary>
        /// 获得全部员工信息
        /// </summary>
        /// <returns></returns>
        public DataTable getEmployeeList(string name, string gw, string unit, string cid)
        {
            string sql = " select a.C_ID,a.C_NAME,a.C_UNIT_ID,b.C_NAME as C_UNIT_NAME,a.C_SEX,a.C_GANGWEI," +
                         " D_BIRTHDAY, C_ADDRESS, C_OFFICE_TEL, C_MOVE_TEL,a.C_MEMO " +
                      " from T_JB_EMPLOYEE a left join T_DM_UNIT b on a.C_UNIT_ID = b.C_ID where 1=1 ";
            DataTable dt = new DataTable();
            try
            {
                Hashtable table = new Hashtable();
                if (cid != null)
                {
                    sql += " and a.C_ID like @C_ID";
                    table.Add("C_ID", "%" + cid + "%");
                }
                if (name != null)
                {
                    sql += " and a.C_NAME like @C_NAME";
                    table.Add("C_NAME", "%" + name + "%");
                }
                if (gw != null)
                {
                    sql += " and a.C_GANGWEI = @C_GANGWEI";
                    table.Add("C_GANGWEI", gw);
                }
                if (unit != null && !unit.Equals("0"))
                {
                    sql += " and a.C_UNIT_ID = @C_UNIT_ID";
                    table.Add("C_UNIT_ID", unit);
                }
                sql += " order by a.C_ID ";
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
        /// 删除员工
        /// </summary>
        /// <param name="c_id">员工编码</param>
        /// <returns></returns>
        public bool delete(List<String> lists)
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
                sql = "delete from T_JB_EMPLOYEE where C_ID in ";
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
                com.CommandText = sql;
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
        /// 获得员工的详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T_JB_EMPLOYEE getEmployeeById(string id)
        {
            T_JB_EMPLOYEE employee = null;
            string sql = " select a.C_ID,a.C_NAME,a.C_UNIT_ID,b.C_NAME as C_UNIT_NAME,a.C_SEX,a.C_GANGWEI," +
                         " D_BIRTHDAY, C_ADDRESS, C_OFFICE_TEL, C_MOVE_TEL,a.C_MEMO " +
                    " from T_JB_EMPLOYEE a left join T_DM_UNIT b on a.C_UNIT_ID = b.C_ID where a.C_ID = '" + id + "'";
            try
            {
                DataTable dt = dbHelper.GetDataSet(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    employee = new T_JB_EMPLOYEE();
                    employee.C_id = dt.Rows[0]["C_ID"].ToString();
                    employee.C_name = dt.Rows[0]["C_NAME"].ToString();
                    employee.C_unitId = dt.Rows[0]["C_UNIT_ID"].ToString();
                    employee.C_sex = dt.Rows[0]["C_SEX"].ToString();
                    employee.C_gangWei = dt.Rows[0]["C_GANGWEI"].ToString();
                    employee.D_birthday = DateTime.Parse(dt.Rows[0]["D_BIRTHDAY"].ToString());
                    employee.C_address = dt.Rows[0]["C_ADDRESS"].ToString();
                    employee.C_office_tel = dt.Rows[0]["C_OFFICE_TEL"].ToString();
                    employee.C_move_tel = dt.Rows[0]["C_MOVE_TEL"].ToString();
                    employee.C_memo = dt.Rows[0]["C_MEMO"].ToString();

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
            return employee;
        }

        /// <summary>
        /// 是否重名
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="name"></param>
        /// <param name="meno"></param>
        /// <returns></returns>
        public bool isExit(string name)
        {
            try
            {
                int count = 0;
                string sql = "";
                sql = "select count(*) from T_JB_EMPLOYEE where C_NAME = @c_name";

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
        /// 保存员工信息
        /// </summary>
        /// <param name="user">员工信息</param>
        /// <returns></returns>
        public bool save(T_JB_EMPLOYEE employee, string userid)
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

                sql = "SELECT max(c_id) FROM  T_JB_EMPLOYEE ";

                com.CommandText = sql;

                object obj = com.ExecuteScalar();
                dec_id = Convert.IsDBNull(obj) ? 0 : Convert.ToInt64(obj);

                sql = "INSERT INTO [T_JB_EMPLOYEE]([C_ID],[C_UNIT_ID], [C_NAME], [C_SEX], " +
                             "   [C_GANGWEI],[D_BIRTHDAY],[C_MEMO]," +
                             " C_ADDRESS, C_OFFICE_TEL, C_MOVE_TEL ) " +
                             "  VALUES(@C_ID,@C_UNIT_ID,@C_NAME,@C_SEX,@C_GANGWEI,@D_BIRTHDAY,@C_MEMO, " +
                             "  @C_ADDRESS,@C_OFFICE_TEL,@C_MOVE_TEL)";
                com.CommandText = sql;
                Hashtable table = new Hashtable();
                dec_id = dec_id + 1;
                c_id = dec_id.ToString().PadLeft(4,'0');
                table.Add("C_ID", c_id);
                table.Add("C_UNIT_ID", employee.C_unitId);
                table.Add("C_NAME", employee.C_name);
                table.Add("C_SEX", employee.C_sex);
                table.Add("C_GANGWEI", employee.C_gangWei);
                table.Add("D_BIRTHDAY", employee.D_birthday);
                table.Add("C_MEMO", employee.C_memo);
                table.Add("C_ADDRESS", employee.C_address);
                table.Add("C_OFFICE_TEL", employee.C_office_tel);
                table.Add("C_MOVE_TEL", employee.C_move_tel);



                DbParameter[] parms = dbHelper.getParams(table);

                com.Parameters.Clear();
                com.Parameters.AddRange(parms);
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
        /// 是否重名
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="name"></param>
        /// <param name="meno"></param>
        /// <returns></returns>
        public bool isExit(string name, string typeid, string id)
        {
            try
            {
                int count = 0;
                string sql = "";
                sql = "select count(*) from T_JB_EMPLOYEE where C_NAME = @c_name and C_UNIT_ID = @typeid and C_ID <> @id";

                Hashtable table = new Hashtable();
                table.Add("c_name", name);
                table.Add("typeid", typeid);
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
        /// 保存员工信息
        /// </summary>
        /// <param name="user">员工信息</param>
        /// <returns></returns>
        public bool update(T_JB_EMPLOYEE employee)
        {
            try
            {
                int count = 0;

                string sql = " UPDATE [T_JB_EMPLOYEE] SET [C_UNIT_ID]=@C_UNIT_ID, [C_NAME]=@C_NAME,[C_SEX]=@C_SEX,  " +
                            " [C_GANGWEI]=@C_GANGWEI, [D_BIRTHDAY]=@D_BIRTHDAY, [C_MEMO]=@C_MEMO,   " +
                            " C_ADDRESS=@C_ADDRESS, C_OFFICE_TEL=@C_OFFICE_TEL, C_MOVE_TEL=@C_MOVE_TEL WHERE [C_ID]=@C_ID ";
                Hashtable table = new Hashtable();

                table.Add("C_ID", employee.C_id);
                table.Add("C_UNIT_ID", employee.C_unitId);
                table.Add("C_NAME", employee.C_name);
                table.Add("C_GANGWEI", employee.C_gangWei);
                table.Add("C_MEMO", employee.C_memo);
                table.Add("C_SEX", employee.C_sex);
                table.Add("D_BIRTHDAY", employee.D_birthday);
                table.Add("C_ADDRESS", employee.C_address);
                table.Add("C_OFFICE_TEL", employee.C_office_tel);
                table.Add("C_MOVE_TEL", employee.C_move_tel);

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
        /// 获得岗位列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetGWList()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "select C_GANGWEI as C_ID, C_GANGWEI as C_NAME from T_JB_EMPLOYEE group by C_GANGWEI";

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
    }
}
