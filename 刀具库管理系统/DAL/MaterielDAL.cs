using System;
using System.Collections.Generic;
using System.Text;
using Util;
using System.Collections;
using System.Data.Common;
using System.Data;
using Model;
using DBCore;

namespace DAL
{
  public  class MaterielDAL
    {
      private DBHelper dbHelper = new SQLDBHelper();


      /// <summary>
      /// 获得全部物料信息
      /// </summary>
      /// <returns></returns>
      public DataTable getMaterielListForQuerry(string id, string name, string area, string type, int finish, string standerd, string userid)
      {
          string sql = " select a.C_ID,a.C_NAME,a.C_TYPE,b.C_NAME as C_TYPENAME,a.C_STANDARD,a.C_AREA,c.C_NAME as C_AREANAME," +
                      " I_FINISH, [C_PICCODE], [I_LAYOUTCOUNT], [C_SURFACE], [C_SCIENCE], [DEC_AREA], [DEC_WEIGHT], " +
                      " case I_BUY when 1 then '是' else '否' end as I_BUY, [DEC_production]," +
                   " case I_FINISH when 1 then '是' else '否' end as C_FINISH,I_LENGTH,I_WIDTH,I_THICK,a.C_MEMO " +
                   " from T_JB_MATERIEL a left join T_JB_MATERIELTYPE b on a.C_TYPE = b.C_ID left join t_jb_placeArea c on a.C_AREA = c.C_ID where a.C_ID in ( " +
                   " select C_MATERIEL from T_JB_MATERIEL_USER where C_JIAOSE = @JIAOSE  ) ";
          DataTable dt = new DataTable();
          try
          {
              Hashtable table = new Hashtable();
              table.Add("JIAOSE", userid);
              if (id != null)
              {
                  sql += " and a.C_ID like @C_ID";
                  table.Add("C_ID", "%" + id + "%");
              }
              if (name != null)
              {
                  sql += " and a.C_NAME like @C_NAME";
                  table.Add("C_NAME", "%" + name + "%");
              }
              if (area != null)
              {
                  sql += " and a.C_AREA = @C_AREA";
                  table.Add("C_AREA", area);
              }
              if (type != null)
              {
                  sql += " and a.C_TYPE = @C_TYPE";
                  table.Add("C_TYPE", type);
              }
              if (finish != -1)
              {
                  sql += " and a.I_FINISH = @I_FINISH";
                  table.Add("I_FINISH", finish);
              }
              if (standerd != null)
              {
                  sql += " and a.C_STANDARD like @C_STANDARD";
                  table.Add("C_STANDARD", "%" + standerd + "%");
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
      /// 获得全部物料信息
      /// </summary>
      /// <returns></returns>
      public DataTable getMaterielList(string name, string area, string type, int finish, string standerd, string userid,string tuhao,string cid)
      {
          string sql = " select a.C_ID,a.C_NAME,a.C_TYPE,b.C_NAME as C_TYPENAME,a.C_STANDARD,a.C_AREA,c.C_NAME as C_AREANAME," +
                       " a.C_MEMO " +
                    " from T_JB_MATERIEL a left join T_JB_MATERIELTYPE b on a.C_TYPE = b.C_ID left join t_jb_placeArea c on a.C_AREA = c.C_ID where 1 = 1";
          DataTable dt = new DataTable();
          try
          {
              Hashtable table = new Hashtable();
              if (tuhao != null)
              {
                  sql += " and a.C_PICCODE like @tuhao";
                  table.Add("C_PICCODE", "%" + tuhao + "%");
              }
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
              if (area != null)
              {
                  sql += " and a.C_AREA = @C_AREA";
                  table.Add("C_AREA", area);
              }
              if (type != null)
              {
                  sql += " and a.C_TYPE = @C_TYPE";
                  table.Add("C_TYPE", type);
              }
              if (finish != -1)
              {
                  sql += " and a.I_FINISH = @I_FINISH";
                  table.Add("I_FINISH", finish);
              }
              if (standerd != null)
              {
                  sql += " and a.C_STANDARD like @C_STANDARD";
                  table.Add("C_STANDARD", "%" + standerd + "%");
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
      /// 获得全部物料信息
      /// </summary>
      /// <returns></returns>
      public DataTable getMaterielList(string name, string area,string type, int finish, string standerd,string userid)
      {
          string sql = " select a.C_ID,a.C_NAME,a.C_TYPE,b.C_NAME as C_TYPENAME,a.C_STANDARD,a.C_AREA,c.C_NAME as C_AREANAME,I_FINISH, " +
                  " case I_FINISH when 1 then '是' else '否' end as C_FINISH,I_LENGTH,I_WIDTH,I_THICK,a.C_MEMO " +
                  " from T_JB_MATERIEL a left join T_JB_MATERIELTYPE b on a.C_TYPE = b.C_ID left join t_jb_placeArea c on a.C_AREA = c.C_ID where 1=1 ";
          DataTable dt = new DataTable();
          try
          {
              Hashtable table = new Hashtable();
              if (name != null)
              {
                  sql += " and a.C_NAME like @C_NAME";
                  table.Add("C_NAME", "%" + name + "%");
              }
              if (area != null)
              {
                  sql += " and a.C_AREA = @C_AREA";
                  table.Add("C_AREA", area);
              }
              if (type != null)
              {
                  sql += " and a.C_TYPE = @C_TYPE";
                  table.Add("C_TYPE", type);
              }
              if (finish != -1)
              {
                  sql += " and a.I_FINISH = @I_FINISH";
                  table.Add("I_FINISH", finish);
              }
              if (standerd != null)
              {
                  sql += " and a.C_STANDARD like @C_STANDARD";
                  table.Add("C_STANDARD", "%" + standerd + "%");
              }
              sql += " order by a.C_ID ";
              if (table.Count >0)
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

      public DataTable getMaterielList(string id,string name, string area, string type, int finish, string standerd, string userid)
      {
          string sql = " select a.C_ID,a.C_NAME,a.C_TYPE,b.C_NAME as C_TYPENAME,a.C_STANDARD,a.C_AREA,c.C_NAME as C_AREANAME,I_FINISH, " +
                    " case I_FINISH when 1 then '是' else '否' end as C_FINISH,I_LENGTH,I_WIDTH,I_THICK,a.C_MEMO " +
                    " from T_JB_MATERIEL a left join T_JB_MATERIELTYPE b on a.C_TYPE = b.C_ID left join t_jb_placeArea c on a.C_AREA = c.C_ID where a.C_ID in ( " +
                    " select C_MATERIEL from T_JB_MATERIEL_USER where C_JIAOSE = @JIAOSE  ) ";
          DataTable dt = new DataTable();
          try
          {
              Hashtable table = new Hashtable();
              table.Add("JIAOSE", userid);
              if (id != null)
              {
                  sql += " and a.C_ID like @C_ID";
                  table.Add("C_ID", "%" + id + "%");
              }
              if (name != null)
              {
                  sql += " and a.C_NAME like @C_NAME";
                  table.Add("C_NAME", "%" + name + "%");
              }
              if (area != null)
              {
                  sql += " and a.C_AREA = @C_AREA";
                  table.Add("C_AREA", area);
              }
              if (type != null)
              {
                  sql += " and a.C_TYPE = @C_TYPE";
                  table.Add("C_TYPE", type);
              }
              if (finish != -1)
              {
                  sql += " and a.I_FINISH = @I_FINISH";
                  table.Add("I_FINISH", finish);
              }
              if (standerd != null)
              {
                  sql += " and a.C_STANDARD like @C_STANDARD";
                  table.Add("C_STANDARD", "%" + standerd + "%");
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
      /// 物料是否被使用
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      public bool isInUse(string id,string userid)
      {
          try
          {

              string sql = "";
              sql = " select count(*) from T_OPERATE_STOCKS where C_MATERIEL_ID = @id ";

              Hashtable table = new Hashtable();
              table.Add("id", id);

              DbParameter[] parms = dbHelper.getParams(table);
              object obj = dbHelper.GetScalar(sql, parms);
              int count1 = Convert.IsDBNull(obj) ? 0 : Convert.ToInt32(obj);

              sql = " select count(*) from T_Runing_Dolist where C_MATERIEL = @id ";

              Hashtable table2 = new Hashtable();
              table2.Add("id", id);

              DbParameter[] parms2 = dbHelper.getParams(table2);
              object obj2 = dbHelper.GetScalar(sql, parms2);
              int count2 = Convert.IsDBNull(obj2) ? 0 : Convert.ToInt32(obj2);

              sql = " select count(*) from T_OPERATE_INOUT_SUB where C_MATERIEL = @id ";

              Hashtable table4 = new Hashtable();
              table4.Add("id", id);

              DbParameter[] parms4 = dbHelper.getParams(table4);
              object obj4 = dbHelper.GetScalar(sql, parms4);
              int count4 = Convert.IsDBNull(obj2) ? 0 : Convert.ToInt32(obj4);

              //物料权限
              //sql = " select count(*) from T_JB_MATERIEL_USER where C_MATERIEL = @id and C_JIAOSE <> @userid ";

              //Hashtable table5 = new Hashtable();
              //table5.Add("id", id);
              //table5.Add("userid", userid);

              //DbParameter[] parms5 = dbHelper.getParams(table5);
              //object obj5 = dbHelper.GetScalar(sql, parms5);
              //int count5 = Convert.IsDBNull(obj2) ? 0 : Convert.ToInt32(obj5);

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
      /// 删除物料
      /// </summary>
      /// <param name="c_id">物料编码</param>
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
               sql = "delete from T_JB_MATERIEL where C_ID in ";
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

              //物料权限
              //sql = "delete from T_JB_MATERIEL_USER where C_MATERIEL in " + ccid;
              //com.CommandText = sql;
              //result = com.ExecuteNonQuery();
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
      /// 获得物料的详细信息
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      public T_JB_Materiel getMaterielById(string id)
      {
          T_JB_Materiel materiel = null;
            string sql = " select a.C_ID,a.C_NAME,a.C_TYPE,b.C_NAME as C_TYPENAME,a.C_STANDARD,a.C_AREA,c.C_NAME as C_AREANAME,I_FINISH, "+
                    " case I_FINISH when 1 then '是' else '否' end as C_FINISH,I_LENGTH,I_WIDTH,I_THICK,a.C_MEMO, "+
                    " a.C_PICCODE, a.I_LAYOUTCOUNT, a.C_SURFACE, a.C_SCIENCE, a.DEC_AREA, a.DEC_WEIGHT, a.I_BUY,DEC_production, "+
                    " a.DEC_ANGLE, a.DEC_DIMENSION1, a.DEC_DIMENSION2,a.DEC_DIMENSION3,a.C_REGRINDING_LENGTH " +
                    " from T_JB_MATERIEL a left join T_JB_MATERIELTYPE b on a.C_TYPE = b.C_ID left join t_jb_placeArea c on a.C_AREA = c.C_ID  where a.C_ID = '" + id + "'";
          try
          {
              DataTable dt = dbHelper.GetDataSet(sql);
              if (dt != null && dt.Rows.Count > 0)
              {
                  materiel = new T_JB_Materiel();
                  materiel.C_id = dt.Rows[0]["C_ID"].ToString();
                  materiel.C_name = dt.Rows[0]["C_NAME"].ToString();
                  materiel.C_type = dt.Rows[0]["C_TYPE"].ToString();
                  materiel.C_typeName = dt.Rows[0]["C_TYPENAME"].ToString();
                  object temp = dt.Rows[0]["C_STANDARD"];
                  if (temp == null || DBNull.Value.Equals(temp))
                  {
                      materiel.C_standerd = string.Empty;
                  }
                  else
                  {
                      materiel.C_standerd = dt.Rows[0]["C_STANDARD"].ToString();
                  }
                  temp = dt.Rows[0]["C_AREA"];
                  if (temp == null || DBNull.Value.Equals(temp))
                  {
                      materiel.C_area = string.Empty;
                  }
                  else
                  {
                      materiel.C_area = dt.Rows[0]["C_AREA"].ToString();
                  }
                  materiel.C_areaName = dt.Rows[0]["C_AREANAME"].ToString();
                  materiel.I_finish = Convert.ToInt32(dt.Rows[0]["I_FINISH"]);

                  materiel.I_thick = Convert.ToDecimal(dt.Rows[0]["I_THICK"]);
               //   materiel.I_single = Convert.ToInt32(dt.Rows[0]["I_SINGLE"]); 
                  materiel.I_length = Convert.ToDecimal(dt.Rows[0]["I_LENGTH"]);
                  materiel.I_width = Convert.ToDecimal(dt.Rows[0]["I_WIDTH"]);
                  temp = dt.Rows[0]["C_MEMO"];
                  if (temp == null || DBNull.Value.Equals(temp))
                  {
                      materiel.C_memo = string.Empty;
                  }
                  else
                  {
                      materiel.C_memo = dt.Rows[0]["C_MEMO"].ToString();
                  }
                  materiel.C_piccode = dt.Rows[0]["C_PICCODE"] == null || dt.Rows[0]["C_PICCODE"].Equals(DBNull.Value) ? string.Empty : dt.Rows[0]["C_PICCODE"].ToString();
                  materiel.I_layOutCount = Convert.ToInt32(dt.Rows[0]["I_LAYOUTCOUNT"]);
                  materiel.C_surface = dt.Rows[0]["C_SURFACE"] == null || dt.Rows[0]["C_SURFACE"].Equals(DBNull.Value) ? string.Empty : dt.Rows[0]["C_SURFACE"].ToString();
                  materiel.C_Science = dt.Rows[0]["C_SCIENCE"] == null || dt.Rows[0]["C_SCIENCE"].Equals(DBNull.Value) ? string.Empty : dt.Rows[0]["C_SCIENCE"].ToString();
                  materiel.Dec_area = Convert.ToDecimal(dt.Rows[0]["DEC_AREA"]);
                  materiel.Dec_weight = Convert.ToDecimal(dt.Rows[0]["DEC_WEIGHT"]);
                  materiel.I_buy = Convert.ToInt32(dt.Rows[0]["I_BUY"]);
                  materiel.Dec_production = Convert.ToDecimal(dt.Rows[0]["DEC_production"]);

                  materiel.Dec_angle = Convert.ToDecimal(dt.Rows[0]["DEC_ANGLE"]);
                  materiel.Dec_dimension1 = Convert.ToDecimal(dt.Rows[0]["DEC_DIMENSION1"]);
                  materiel.Dec_dimension2 = Convert.ToDecimal(dt.Rows[0]["DEC_DIMENSION2"]);
                  materiel.Dec_dimension3 = Convert.ToDecimal(dt.Rows[0]["DEC_DIMENSION3"]);
                  materiel.C_regrinding_length = dt.Rows[0]["C_REGRINDING_LENGTH"] == null || dt.Rows[0]["C_REGRINDING_LENGTH"].Equals(DBNull.Value) ? string.Empty : dt.Rows[0]["C_REGRINDING_LENGTH"].ToString();
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
          return materiel;
      }

      /// <summary>
      /// 是否重名
      /// </summary>
      /// <param name="tableName"></param>
      /// <param name="name"></param>
      /// <param name="meno"></param>
      /// <returns></returns>
      public bool isExit(string name,string typeid)
      {
          try
          {
              int count = 0;
              string sql = "";
              sql = "select count(*) from T_JB_MATERIEL where C_NAME = @c_name and C_TYPE = @typeid";

              Hashtable table = new Hashtable();
              table.Add("c_name", name);
              table.Add("typeid", typeid);

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
      /// 保存物料信息
      /// </summary>
      /// <param name="user">物料信息</param>
      /// <returns></returns>
      public bool save(T_JB_Materiel materiel, string userid)
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

              sql = "INSERT INTO [T_JB_MATERIEL]([C_ID],[C_TYPE], [C_NAME], [I_SINGLE], [DEC_PRICE], [C_STANDARD], " +
                           "   [I_LENGTH], [I_WIDTH] ,[I_THICK],[C_AREA],[I_FINISH],[C_MEMO],"+
                           " [C_PICCODE], [I_LAYOUTCOUNT], [C_SURFACE], [C_SCIENCE], [DEC_AREA], [DEC_WEIGHT], [I_BUY], [DEC_production], " +
                           " [DEC_ANGLE], [DEC_DIMENSION1], [DEC_DIMENSION2], [DEC_DIMENSION3], [C_REGRINDING_LENGTH]) " +
                           "  VALUES(@C_ID,@C_TYPE,@C_NAME,@I_SINGLE,0,@C_STANDARD, @I_LENGTH,@I_WIDTH ,@I_THICK,@C_AREA,@I_FINISH,@C_MEMO, "+
                           " @C_PICCODE, @I_LAYOUTCOUNT, @C_SURFACE, @C_SCIENCE, @DEC_AREA, @DEC_WEIGHT, @I_BUY, @DEC_production,"+
                            " @DEC_ANGLE, @DEC_DIMENSION1, @DEC_DIMENSION2, @DEC_DIMENSION3, @C_REGRINDING_LENGTH)";
              com.CommandText = sql;
              Hashtable table = new Hashtable();
              table.Add("C_ID", materiel.C_id);
              table.Add("C_TYPE", materiel.C_type);
              table.Add("C_NAME", materiel.C_name);
              table.Add("I_SINGLE", materiel.I_single);
              if (materiel.C_standerd == null || string.Empty.Equals(materiel.C_standerd.Trim()))
              {
                  table.Add("C_STANDARD", DBNull.Value);
              }
              else
              {
                  table.Add("C_STANDARD", materiel.C_standerd);
              }
              table.Add("I_LENGTH", materiel.I_length);
              table.Add("I_WIDTH", materiel.I_width);
              table.Add("I_THICK", materiel.I_thick);
              table.Add("C_AREA", materiel.C_area);
              table.Add("I_FINISH", materiel.I_finish);
              table.Add("C_MEMO", materiel.C_memo);

              table.Add("C_PICCODE", materiel.C_piccode == null ? string.Empty : materiel.C_piccode);
              table.Add("I_LAYOUTCOUNT", materiel.I_layOutCount);
              table.Add("C_SURFACE", materiel.C_surface == null ? string.Empty : materiel.C_surface);
              table.Add("C_SCIENCE", materiel.C_Science == null ? string.Empty : materiel.C_Science);
              table.Add("DEC_AREA", materiel.Dec_area);
              table.Add("DEC_WEIGHT", materiel.Dec_weight);
              table.Add("I_BUY", materiel.I_buy);
              table.Add("DEC_production", materiel.Dec_production);

              table.Add("DEC_ANGLE", materiel.Dec_angle);
              table.Add("DEC_DIMENSION1", materiel.Dec_dimension1);
              table.Add("DEC_DIMENSION2", materiel.Dec_dimension2);
              table.Add("DEC_DIMENSION3", materiel.Dec_dimension3);
              table.Add("C_REGRINDING_LENGTH", materiel.C_regrinding_length == null ? string.Empty : materiel.C_regrinding_length);

              DbParameter[] parms = dbHelper.getParams(table);

              com.Parameters.Clear();
              com.Parameters.AddRange(parms);
              result = com.ExecuteNonQuery();

              //sql = " INSERT INTO [T_JB_MATERIEL_USER]([C_MATERIEL], [C_JIAOSE], [I_YESNO]) VALUES(@C_MATERIEL, @JIAOSE, @YESNO) ";
              //com.CommandText = sql;
              //Hashtable table2 = new Hashtable();
              //table2.Add("C_MATERIEL", materiel.C_id);
              //table2.Add("JIAOSE", userid);
              //table2.Add("YESNO", 1);

              //DbParameter[] parms2 = dbHelper.getParams(table2);

              //com.Parameters.Clear();
              //com.Parameters.AddRange(parms2);
              //result = com.ExecuteNonQuery();

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
      public bool isExit(string name, string typeid,string id)
      {
          try
          {
              int count = 0;
              string sql = "";
              sql = "select count(*) from T_JB_MATERIEL where C_NAME = @c_name and C_TYPE = @typeid and C_ID <> @id";

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
      /// 保存物料信息
      /// </summary>
      /// <param name="user">物料信息</param>
      /// <returns></returns>
      public bool update(T_JB_Materiel materiel)
      {
          try
          {             
              int count = 0;             

            string  sql = " UPDATE [T_JB_MATERIEL]SET [C_TYPE]=@C_TYPE, [C_NAME]=@C_NAME, [I_SINGLE]=@I_SINGLE, [C_STANDARD]=@C_STANDARD, [I_LENGTH]=@I_LENGTH,  "+
                        " [I_WIDTH]=@I_WIDTH, [I_THICK]=@I_THICK, [C_AREA]=@C_AREA, [I_FINISH]=@I_FINISH, [C_MEMO]=@C_MEMO,   "+
                        " [C_PICCODE]=@C_PICCODE, [I_LAYOUTCOUNT]=@I_LAYOUTCOUNT, [C_SURFACE]=@C_SURFACE, [C_SCIENCE]=@C_SCIENCE,  "+
                        "  [DEC_AREA]=@DEC_AREA, [DEC_WEIGHT]=@DEC_WEIGHT, [I_BUY]=@I_BUY, [DEC_production]=@DEC_production,  " +
                        "  [DEC_ANGLE]=@DEC_ANGLE, [DEC_DIMENSION1]=@DEC_DIMENSION1, [DEC_DIMENSION2]=@DEC_DIMENSION2, [DEC_DIMENSION3]=@DEC_DIMENSION3, [C_REGRINDING_LENGTH]=@C_REGRINDING_LENGTH WHERE [C_ID]=@C_ID ";
              Hashtable table = new Hashtable();

              table.Add("C_ID", materiel.C_id);
              table.Add("C_TYPE", materiel.C_type);
              table.Add("C_NAME", materiel.C_name);
              table.Add("I_SINGLE", materiel.I_single);
              if (materiel.C_standerd == null || string.Empty.Equals(materiel.C_standerd.Trim()))
              {
                  table.Add("C_STANDARD", DBNull.Value);
              }
              else
              {
                  table.Add("C_STANDARD", materiel.C_standerd);
              }
              table.Add("I_LENGTH", materiel.I_length);
              table.Add("I_WIDTH", materiel.I_width);
              table.Add("I_THICK", materiel.I_thick);
              table.Add("C_AREA", materiel.C_area);
              table.Add("I_FINISH", materiel.I_finish);
              table.Add("C_MEMO", materiel.C_memo);

              table.Add("C_PICCODE", materiel.C_piccode == null ? string.Empty : materiel.C_piccode);
              table.Add("I_LAYOUTCOUNT", materiel.I_layOutCount);
              table.Add("C_SURFACE", materiel.C_surface == null ? string.Empty : materiel.C_surface);
              table.Add("C_SCIENCE", materiel.C_Science == null ? string.Empty : materiel.C_Science);
              table.Add("DEC_AREA", materiel.Dec_area);
              table.Add("DEC_WEIGHT", materiel.Dec_weight);
              table.Add("I_BUY", materiel.I_buy);
              table.Add("DEC_production", materiel.Dec_production);

              table.Add("DEC_ANGLE", materiel.Dec_angle);
              table.Add("DEC_DIMENSION1", materiel.Dec_dimension1);
              table.Add("DEC_DIMENSION2", materiel.Dec_dimension2);
              table.Add("DEC_DIMENSION3", materiel.Dec_dimension3);
              table.Add("C_REGRINDING_LENGTH", materiel.C_regrinding_length == null ? string.Empty : materiel.C_regrinding_length);

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
      /// 获得所有要操作的数据
      /// </summary>
      /// <param name="jiaose"></param>
      /// <returns></returns>
      public DataTable getAllInformations(string jiaose)
      {
          DataTable dt = new DataTable();
          try
          {
              string sql = " select '"+jiaose+"' as jiaose,(select count(*) from T_JB_MATERIEL_USER where C_MATERIEL = a.c_id and C_JIAOSE = '"+jiaose+"' ) as yesno, "+
                  "  a.C_ID,a.C_NAME,a.C_TYPE,b.C_NAME as C_TYPENAME,a.C_STANDARD,a.C_AREA,c.C_NAME as C_AREANAME,a.C_MEMO "+
                  "       from T_JB_MATERIEL a left join T_JB_MATERIELTYPE b on a.C_TYPE = b.C_ID left join t_jb_placeArea c on a.C_AREA = c.C_ID";
             
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
      /// 设置物料数据权限
      /// </summary>
      /// <param name="isyes"></param>
      /// <param name="isno"></param>
      /// <param name="userid"></param>
      /// <returns></returns>
      public bool setJurisdiction(List<string> isyes, List<string> isno, string userid)
      {
          int result = 0;
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

              sql = "delete from T_JB_MATERIEL_USER where C_MATERIEL=@C_MATERIEL and C_JIAOSE=@JIAOSE";
              foreach (string id in isno)
              {
                  com.CommandText = sql;
                  Hashtable table = new Hashtable();
                  table.Add("C_MATERIEL", id);
                  table.Add("JIAOSE", userid);

                  DbParameter[] parms = dbHelper.getParams(table);

                  com.Parameters.Clear();
                  com.Parameters.AddRange(parms);
                  count = com.ExecuteNonQuery();
              }

             
             
              foreach (string id in isyes)
              {
                  string isSql = " select Count(*) from T_JB_MATERIEL_USER where C_MATERIEL='"+id+"' and C_JIAOSE='"+userid+"' ";
                  com.CommandText = isSql;
                  com.Parameters.Clear();
                  object obj = com.ExecuteScalar();
                  int isexit = Convert.IsDBNull(obj) ? 0 : Convert.ToInt32(obj);
                  if (isexit > 0)
                  {
                      sql = " update T_JB_MATERIEL_USER set I_YESNO = 1 where C_MATERIEL=@C_MATERIEL and C_JIAOSE=@JIAOSE";
                  }
                  else
                  {
                      sql = "INSERT INTO [T_JB_MATERIEL_USER]([C_MATERIEL],[C_JIAOSE], [I_YESNO] )  VALUES(@C_MATERIEL,@JIAOSE,1)";
                  }

                  com.CommandText = sql;
                  Hashtable table = new Hashtable();
                  table.Add("C_MATERIEL", id);
                  table.Add("JIAOSE", userid);

                  DbParameter[] parms = dbHelper.getParams(table);

                  com.Parameters.Clear();
                  com.Parameters.AddRange(parms);
                  result = com.ExecuteNonQuery();
              }
            
              tran.Commit();
              if (result > 0 || count >0)
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
