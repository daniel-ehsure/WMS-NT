using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Collections;
/*****************
 * 2008.06.23
 *****************/
namespace DBCore
{
    /// <summary>
    /// 
    /// H2O
    /// 
    /// 
    /// 
    /// 该类是定义不同数据库规范的通用数据库帮助类
    /// </summary>
    public abstract class DBHelper
    {
        public abstract DbConnection getConnection();

        public abstract int ExecuteCommand(string safeSql);

        public abstract int ExecuteCommand(string sql, params DbParameter[] values);

        public abstract object GetScalar(string safeSql);

        public abstract object GetScalar(string sql, params DbParameter[] values);

        //public abstract DbDataReader GetReader(string safeSql);

        //public abstract DbDataReader GetReader(string sql, params DbParameter[] values);

        //public abstract DbDataReader GetReader2(string safeSql);

        //public abstract DbDataReader GetReader2(string sql, params DbParameter[] values);

        public abstract DataTable GetDataSet(string safeSql);

        public abstract DataTable GetDataSet(string sql, params DbParameter[] values);

        public abstract DbParameter[] getParams(Hashtable table);

    }
}
