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
  public class LayoutDAL
    {
      private DBHelper dbHelper = new SQLDBHelper();
        /// <summary>
        /// 根据用户角色获得可以显示的所有功能组名称
        /// </summary>
        /// <returns>组名称列表</returns>
        public List<string> getAllGroupName()
        {
            List<string> list = new List<string>();
            string sql = "select GROUPNAME from OUTLOOK_TABLE_JB_USER where jiaose = @role and yesno = '1' group by GROUPNAME,OB_NAME order by OB_NAME";
            try
            {
                Hashtable table = new Hashtable();
                table.Add("@role", Global.longid);
                DbParameter[] parms = dbHelper.getParams(table);

                DataTable ds = dbHelper.GetDataSet(sql, parms);
                for (int i = 0; i < ds.Rows.Count; i++)
                {
                    string temp = ds.Rows[i]["GROUPNAME"].ToString();
                    list.Add(temp);
                }
                dbHelper.getConnection().Close();
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
        /// 通过功能组名称根据用户角色获得 组内所以可以显示的项目的名称
        /// </summary>
        /// <param name="groupname">功能组名称</param>
        /// <returns>项目名称列表</returns>
        public List<Outlook_Table_Jb_User> getItemNameByGroupName(string groupname)
        {
            List<Outlook_Table_Jb_User> list = new List<Outlook_Table_Jb_User>();
            string sql = "select ITEMNAME,PICNAME from OUTLOOK_TABLE_JB_USER  where GROUPNAME = @groupname and jiaose = @role and yesno = '1'order by TRIG_ID";
            try
            {
                Hashtable table = new Hashtable();
                table.Add("@groupname", groupname);
                table.Add("@role", Global.longid);
                DbParameter[] parms = dbHelper.getParams(table);

                DataTable ds = dbHelper.GetDataSet(sql, parms);
                for (int i = 0; i < ds.Rows.Count; i++)
                {
                    Outlook_Table_Jb_User outlook = new Outlook_Table_Jb_User();
                    outlook.Itemname = ds.Rows[i]["ITEMNAME"].ToString();
                    outlook.Picname = ds.Rows[i]["PICNAME"].ToString();
                    list.Add(outlook);
                }
               
                dbHelper.getConnection().Close();
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
        /// 初始化数据字典，该字典用来维护点击功能打开哪个窗体
        /// </summary>
        /// <returns>项目名称和窗体名称的映射</returns>
        public Hashtable initDictionary()
        {
            Hashtable table = new Hashtable();
            string sql = "select ITEMNAME,OBJECTNAME from OUTLOOK_TABLE_JB_USER where jiaose = '" + Global.longid+ "' and yesno = '1'";
            try
            {
                DataTable ds = dbHelper.GetDataSet(sql);
                for (int i = 0; i < ds.Rows.Count; i++)
                {
               
                    table.Add(ds.Rows[i]["ITEMNAME"], ds.Rows[i]["OBJECTNAME"]);
                }
                
                dbHelper.getConnection().Close();
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
            return table;
        }

        /// <summary>
        /// 是否为合法用户
        /// </summary>
        /// <param name="user">用户信息的数据封装对象</param>
        /// <returns>是否合法</returns>
        public bool login(T_Jb_Login_Pass user)
        {
            bool flag = false;
            string sql = "select * from T_JB_LOGIN_PASS where C_LOGID=@useruid and C_PASSWORD=@userpass ";
            try
            {
                Hashtable table = new Hashtable();
                table.Add("useruid", user.C_loginID);
                table.Add("userpass", user.C_password);
              
                DbParameter[] parms = dbHelper.getParams(table);

                DataTable ds = dbHelper.GetDataSet(sql, parms);
                if (ds.Rows.Count>0)
                {
                    flag = true;
                    user.C_loginID = ds.Rows[0][0].ToString();


                    user.C_password = ds.Rows[0][3].ToString();
                    user.C_chinesename = ds.Rows[0][4].ToString();
                    user.C_jiaose = ds.Rows[0][5].ToString();
                    
                    user.I_state = Convert.ToInt32(ds.Rows[0][7]);
                }
                dbHelper.getConnection().Close();
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
            return flag;
        }

        /// <summary>
        /// 获得用户的旧密码
        /// </summary>
        /// <param name="name">用户名</param>
        /// <returns></returns>
        public string getPasswordByName(string name)
        {
            string password = null;
            string sql = "select c_password from t_jb_login_pass where c_logid = '" + name + "'";
            try
            {
                object temp = dbHelper.GetScalar(sql);
                if (temp != null && !(DBNull.Value.Equals(temp)))
                {
                    password = dbHelper.GetScalar(sql).ToString();
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
            return password;
        }

        /// <summary>
        /// 更改用户密码
        /// </summary>
        /// <param name="newPassword"></param>
        /// <param name="c_id"></param>
        /// <returns></returns>
        public bool updateUser(T_Jb_Login_Pass user)
        {
            try
            {
                int count = 0;
                string sql = "update T_JB_LOGIN_PASS set c_password = @newPassword ,C_CHINESENAME = @name,C_JIAOSE = @C_JIAOSE  where C_LOGID = @c_id ";
                Hashtable table = new Hashtable();
                table.Add("newPassword", user.C_password);
                table.Add("name", user.C_chinesename);
                table.Add("c_id", user.C_loginID);
                table.Add("C_JIAOSE", user.C_jiaose);
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
        /// 保存用户信息
        /// </summary>
        /// <param name="user">系统用户</param>
        /// <returns></returns>
        public bool save(T_Jb_Login_Pass user)
        {
            try
            {
                int count = 0;
                string sql = "INSERT INTO  T_JB_LOGIN_PASS  ( C_LOGID ,    C_PASSWORD ,    C_CHINESENAME ,  C_JIAOSE ,   I_STATE ) " +
                            " VALUES(@C_LOGID,@C_PASSWORD,@C_CHINESENAME,@C_JIAOSE,@I_STATE )";
                Hashtable table = new Hashtable();
                table.Add("C_LOGID", user.C_loginID);
                
                table.Add("C_PASSWORD", user.C_password);
                table.Add("C_CHINESENAME", user.C_chinesename);
                table.Add("C_JIAOSE", user.C_jiaose);              
                table.Add("I_STATE", user.I_state);

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
      /// 获得全部用户信息
      /// </summary>
      /// <returns></returns>
        public DataTable getUserList(string id,string name)
        {
            string sql = "select C_LOGID,C_CHINESENAME,C_JIAOSE from T_JB_LOGIN_PASS where 1=1";
            DataTable dt = new DataTable();
            try
            {
                if (id != null || name != null)
                {
                    Hashtable table = new Hashtable();
                    if (id != null)
                    {
                        sql += " and C_LOGID like @useruid";
                      
                        table.Add("useruid", "%"+id+"%");
                    }
                    if (name != null)
                    {
                        sql += " and C_CHINESENAME like @username";
                        table.Add("username", "%" + name+"%");
                    }
                    DbParameter[] parms = dbHelper.getParams(table);
                    dt = dbHelper.GetDataSet(sql,parms);
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
        /// 删除用户
        /// </summary>
        /// <param name="c_id">用户名</param>
        /// <returns></returns>
        public bool delte(List<String> lists)
        {
            int count = 0;           
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

                sql = " delete from OUTLOOK_TABLE_JB_USER where JIAOSE in ";
                sql += ccid;
                dbHelper.ExecuteCommand(sql);
                sql = " delete from T_JB_MATERIEL_USER where JIAOSE in ";
                sql += ccid;
                dbHelper.ExecuteCommand(sql);
                sql = "delete from T_JB_LOGIN_PASS where C_LOGID in ";

                sql += ccid;
                count = dbHelper.ExecuteCommand(sql);
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
      /// 获得用户的详细信息
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
        public T_Jb_Login_Pass getUserById(string id)
        {
            T_Jb_Login_Pass user = null;
            string sql = "select C_LOGID,C_PASSWORD,C_CHINESENAME,C_JIAOSE,I_STATE from T_JB_LOGIN_PASS where c_logid = '" + id + "'";
            try
            {
                DataTable dt  = dbHelper.GetDataSet(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    user = new T_Jb_Login_Pass();
                    user.C_loginID = dt.Rows[0]["C_LOGID"].ToString();
                    user.C_password = dt.Rows[0]["C_PASSWORD"].ToString();
                    user.C_chinesename = dt.Rows[0]["C_CHINESENAME"].ToString();
                    user.C_jiaose = dt.Rows[0]["C_JIAOSE"].ToString();
                    user.I_state = Convert.ToInt32( dt.Rows[0]["I_STATE"]);
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
            return user;
        }

        /// <summary>
        /// 获得全部工位信息
        /// </summary>
        /// <returns></returns>
        public DataTable getStationList(string id, string name)
        {
            string sql = "SELECT [C_ID], [C_name], [c_meno]  FROM [T_JB_STATION] where 1=1 ";
            DataTable dt = new DataTable();
            try
            {
                if (id != null || name != null)
                {
                    Hashtable table = new Hashtable();
                    if (id != null)
                    {
                        sql += " and C_ID like @id";

                        table.Add("id", "%" + id + "%");
                    }
                    if (name != null)
                    {
                        sql += " and C_name like @name";
                        table.Add("name", "%" + name + "%");
                    }
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

        #region 用户权限需要

        /// <summary>
        /// 获得全部用户信息
        /// </summary>
        /// <returns></returns>
        public DataTable getUserList()
        {
            string sql = "select C_LOGID from T_JB_LOGIN_PASS where 1=1";
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
        /// 获得所有要操作的数据
        /// </summary>
        /// <param name="jiaose"></param>
        /// <returns></returns>
        public DataTable getAllInformations(string jiaose)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "select count(*) from outlook_table_jb_user where jiaose = '" + jiaose + "'";
                object obj = dbHelper.GetScalar(sql);
                int count = 0;
                if (obj != null)
                {
                    count = Convert.ToInt32(obj);
                }
                if (count > 0)
                {
                    sql = "select '" + jiaose + "' as jiaose,yesno ,itemname, groupname , picname ,TRIG_ID,OBJECTNAME,OB_NAME from outlook_table_jb_user where jiaose = '" + jiaose + "' order by OB_NAME,TRIG_ID ";
                }
                else
                {
                    sql = "select '" + jiaose + "' as jiaose,0 as yesno ,itemname, groupname , picname ,TRIG_ID,OBJECTNAME,OB_NAME from outlook_table_jb_user where jiaose = 'sa' order by OB_NAME,TRIG_ID ";
                }
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
        /// 保存权限信息
        /// </summary>
        /// <param name="jiaose"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        public bool save(string jiaose, List<Outlook_Table_Jb_User> users)
        {
            int count = 0;
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
            DbParameter[] parms = null;
            try
            {
                com.Transaction = tran;
                sql = "select count(*) from outlook_table_jb_user where jiaose = '" + jiaose + "'";
                com.CommandText = sql;
                object obj = com.ExecuteScalar();

                if (obj != null)
                {
                    count = Convert.ToInt32(obj);
                }
                Hashtable table = new Hashtable();
                if (count > 0)
                {
                    sql = "UPDATE OUTLOOK_TABLE_JB_USER SET YESNO = @YESNO WHERE ITEMNAME=@ITEMNAME and JIAOSE=@JIAOSE";
                    foreach (Outlook_Table_Jb_User user in users)
                    {
                        com.CommandText = sql;

                        table.Clear();
                        table.Add("YESNO", user.Yesno);
                        table.Add("ITEMNAME", user.Itemname);
                        table.Add("JIAOSE", user.Jiase);
                        parms = dbHelper.getParams(table);
                        com.Parameters.Clear();
                        com.Parameters.AddRange(parms);
                        com.ExecuteNonQuery();
                    }
                }
                else
                {
                    sql = "INSERT INTO OUTLOOK_TABLE_JB_USER ( ITEMNAME ,  GROUPNAME ,  PICNAME ,  TRIG_ID ,  OBJECTNAME ,  OB_NAME ,  JIAOSE ,  YESNO ) " +
                          " VALUES(@ITEMNAME,@GROUPNAME,@PICNAME,@TRIG_ID,@OBJECTNAME,@OB_NAME,@JIAOSE,@YESNO) ";
                    foreach (Outlook_Table_Jb_User user in users)
                    {
                        com.CommandText = sql;

                        table.Clear();
                        table.Add("ITEMNAME", user.Itemname);
                        table.Add("GROUPNAME", user.Groupname);
                        table.Add("PICNAME", user.Picname);
                        table.Add("TRIG_ID", user.Trig_id);
                        table.Add("OBJECTNAME", user.Objecname);
                        table.Add("OB_NAME", user.Ob_name);
                        table.Add("YESNO", user.Yesno);
                        table.Add("JIAOSE", user.Jiase);
                        parms = dbHelper.getParams(table);
                        com.Parameters.Clear();
                        com.Parameters.AddRange(parms);
                        com.ExecuteNonQuery();
                    }
                }

                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                conn.Close();
                Log.write(ex.Message + "\r\n" + ex.StackTrace);
                throw ex;
            }

            conn.Close();
            return true;
        }
        #endregion


        /// <summary>
        /// 数据库初始化
        /// </summary>
        /// <param name="c_id">用户名</param>
        /// <returns></returns>
        public bool resetDataBase()
        {
            int count = 0;
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



                sql = " delete from OUTLOOK_TABLE_JB_USER where JIAOSE <> 'sa' ";
                dbHelper.ExecuteCommand(sql);
                sql = " update OUTLOOK_TABLE_JB_USER set YESNO =1 ";
                dbHelper.ExecuteCommand(sql);
                sql = " delete from T_JB_LAYOUT ";
                dbHelper.ExecuteCommand(sql);
                sql = " delete from T_JB_LOGIN_PASS where C_LOGID <> 'sa' ";
                dbHelper.ExecuteCommand(sql);
                sql = " update T_JB_LOGIN_PASS set C_PASSWORD = '1' ";
                dbHelper.ExecuteCommand(sql);
                sql = " delete from T_JB_MATERIEL ";
                dbHelper.ExecuteCommand(sql);
                sql = " delete from T_JB_MATERIEL_USER ";
                dbHelper.ExecuteCommand(sql);
                sql = " delete from t_jb_placeArea ";
                dbHelper.ExecuteCommand(sql);
                sql = " update dbo.T_JB_PLACE set C_AREA = null ";
                dbHelper.ExecuteCommand(sql);
                sql = " delete from T_JB_STATION ";
                dbHelper.ExecuteCommand(sql);
                sql = " delete from T_JB_TYPE ";
                dbHelper.ExecuteCommand(sql);
                sql = " delete from T_OPERATE_INOUT_MAIN ";
                dbHelper.ExecuteCommand(sql);
                sql = " delete from T_OPERATE_INOUT_SUB ";
                dbHelper.ExecuteCommand(sql);
                sql = " delete from T_Operate_OutPlan ";
                dbHelper.ExecuteCommand(sql);
                sql = " delete from T_OPERATE_STOCKS ";
                dbHelper.ExecuteCommand(sql);
                sql = " delete from T_Runing_Dolist ";
                dbHelper.ExecuteCommand(sql);
                sql = " delete from T_Runing_Dolist_temp ";
                dbHelper.ExecuteCommand(sql);
                sql = " delete from T_Runing_Station ";               

                count = dbHelper.ExecuteCommand(sql);
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
        /// 获得全部用户信息
        /// </summary>
        /// <returns></returns>
        public DataTable getAllRols(string rol)
        {
            string sql = " select distinct C_JIAOSE from T_JB_LOGIN_PASS ";
            if (!"超级管理员".Equals(rol))
            {
                sql += " where  C_JIAOSE <> '超级管理员'";
            }

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
        /// 获得全部用户信息
        /// </summary>
        /// <returns></returns>
        public DataTable getLogByDate(string date)
        {
            string sql = " select pid 序号, username 用户, addtime 时间, mess 信息 from T_SYS_LOG where addtime between '" + date + " 00:00:00' and '" + date + " 23:59:59'";

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
    }
}
