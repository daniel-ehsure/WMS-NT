﻿using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Model;
using System.Data;

namespace BLL
{
   public class MaterielTypeBLL
    {
       MaterielTypeDAL dal = new MaterielTypeDAL();
       /// <summary>
        /// 根据类型编号获得子类型信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
       public List<T_JB_MaterielType> GetAllChild(string pid)
       {
           return dal.getAllChild(pid);
       }

        /// <summary>
        /// 获得全部信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="name"></param>
        /// <param name="meno"></param>
        /// <returns></returns>
       public DataTable GetList(string pid, string name, string meno, int end)
       {
           return dal.getList(pid, name, meno,end);
       }
       /// <summary>
        /// 是否重名
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="name"></param>
        /// <param name="meno"></param>
        /// <returns></returns>
       public bool IsExit(string pid, string name)
       {
           return dal.IsExit(pid, name);
       }
            /// <summary>
        /// 保存
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="name"></param>
        /// <param name="meno"></param>
        /// <returns></returns>
       public string Save(T_JB_MaterielType dm_type)
       {
           return dal.Save(dm_type);
       }
        /// <summary>
        /// 物料类型是否有子类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       public bool IsHaveChild(string id)
       {
           return dal.isHaveChild(id);
       }
        /// <summary>
       /// 物料类型是否被使用
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public bool IsInUse(string id)
       {
           return dal.isInUse(id);
       }
       /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="c_id"></param>
        /// <returns></returns>
       public bool Delete(List<String> lists)
       {
           return dal.delete(lists);
       }
          /// <summary>
        /// 根据编码获得信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
       public T_JB_MaterielType GetById(string id)
       {
           return dal.getById(id);
       }
       
        /// <summary>
        /// 是否重名
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="name"></param>
        /// <param name="meno"></param>
        /// <returns></returns>
       public bool IsExit(string pid, string name, string id)
       {
           return dal.IsExit(pid, name, id);
       }

        /// <summary>
        /// 更新类型
        /// </summary>
        /// <param name="dm_type">类型信息</param>
        /// <returns></returns>
       public bool Update(T_JB_MaterielType dm_type)
       {
           return dal.update(dm_type);
       }
    }
}
