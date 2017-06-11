using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;


/******************
 * 2008年06月13日
 * *****************/

namespace DBCore
{
    /// <summary>
    /// SQLDBHelper类 是整个应用程序 访问数据库的帮助类的SQL实现
    /// 该类封装了 所有与数据相关的操作
    /// 该类有DAL中的类调用
    /// </summary>
    public class SQLDBHelper : DBHelper
    {
        private string _connectionString = null;

        public string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["constr"].ConnectionString; }
            set { _connectionString = value; }
        }





        #region DBHelper 成员

        public override DbConnection getConnection()
        {
            return new SqlConnection(ConnectionString); ;
        }

        public override int ExecuteCommand(string safeSql)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(safeSql, connection);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            connection.Open();
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }

        public override int ExecuteCommand(string sql, params DbParameter[] values)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.Clear();
            cmd.Parameters.AddRange(values);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            connection.Open();
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }

        public override object GetScalar(string safeSql)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(safeSql, connection);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            connection.Open();
            object result = cmd.ExecuteScalar();
            connection.Close();
            return result;
        }

        public override object GetScalar(string sql, params DbParameter[] values)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.Clear();
            cmd.Parameters.AddRange(values);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            connection.Open();
            object result = cmd.ExecuteScalar();
            connection.Close();
            return result;

        }

        //public override DbDataReader GetReader(string safeSql)
        //{
        //    SqlConnection connection = new SqlConnection(ConnectionString);
        //    SqlCommand cmd = new SqlCommand(safeSql, connection);

        //    SqlDataReader reader = cmd.ExecuteReader();
        //    return reader;
        //}

        //public override DbDataReader GetReader(string sql, params DbParameter[] values)
        //{
        //    SqlConnection connection = new SqlConnection(ConnectionString);
        //    SqlCommand cmd = new SqlCommand(sql, connection);
        //    cmd.Parameters.Clear();
        //    cmd.Parameters.AddRange(values);
        //    SqlDataReader reader = cmd.ExecuteReader();
        //    return reader;
        //}
        //public override DbDataReader GetReader2(string safeSql)
        //{
        //    SqlConnection connection = new SqlConnection(ConnectionString);
        //    SqlCommand cmd = new SqlCommand(safeSql, connection);
        //    SqlDataReader reader = cmd.ExecuteReader();
        //    return reader;
        //}

        //public override DbDataReader GetReader2(string sql, params DbParameter[] values)
        //{
        //    SqlConnection connection = new SqlConnection(ConnectionString);
        //    SqlCommand cmd = new SqlCommand(sql, connection);
        //    cmd.Parameters.Clear();
        //    cmd.Parameters.AddRange(values);
        //    SqlDataReader reader = cmd.ExecuteReader();
        //    return reader;
        //}

        public override DataTable GetDataSet(string safeSql)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(safeSql, connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            connection.Open();
            da.Fill(ds);
            connection.Close();

            return ds.Tables[0];
        }

        public override DataTable GetDataSet(string sql, params DbParameter[] values)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.Clear();
            cmd.Parameters.AddRange(values);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            connection.Open();
            da.Fill(ds);
            connection.Close();

            return ds.Tables[0];
        }

        public override DbParameter[] getParams(Hashtable table)
        {
            string[] temp = new string[table.Count];
            table.Keys.CopyTo(temp, 0);
            SqlParameter[] parameters = new SqlParameter[table.Count];
            for (int i = 0; i < table.Count; i++)
            {
                parameters[i] = new SqlParameter(temp[i].ToString(), table[temp[i]]);
            }
            return parameters;
        }
        #endregion
    }
}
