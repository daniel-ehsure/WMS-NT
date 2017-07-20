using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Model;
using System.Data;
using Util;

namespace BLL
{
   public  class RuningDoListBLL
    {
       RuningDoListDAL dal = new RuningDoListDAL();

       /// <summary>
        /// 获取联机任务列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
       public DataTable getList(int type)
       {
           return dal.getList(type);
       }


       
        /// <summary>
        ///  删除联机任务
        /// </summary>
        /// <param name="toRunDate"></param>
        /// <returns></returns>
       public bool deleteDoList(List<string> list, string dh)
       {
           return dal.deleteDoList(list, dh);
       }

        /// <summary>
        /// 执行联机任务
        /// </summary>
        /// <returns></returns>
       public bool executeDoList(List<string> list, string dh)
       {
           return dal.executeDoList(list, dh);
       }

        /// <summary>
        /// 保存联机队列
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="meno"></param>
        /// <param name="station"></param>
        /// <returns></returns>
       public bool saveDolist(DataTable dt, string meno, int controlType)
       {
           return dal.saveDolist(dt, meno, controlType);
       }

       /// <summary>
       /// 保存联机队列
       /// </summary>
       /// <param name="dt"></param>
       /// <param name="meno"></param>
       /// <param name="station"></param>
       /// <returns></returns>
       public string SaveDolist(DataTable dt, string meno, InOutType type)
       {
           return dal.SaveDolist(dt, meno, type);
       }
    }
}
