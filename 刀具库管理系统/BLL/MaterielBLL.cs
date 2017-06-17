using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using System.Data;
using Model;

namespace BLL
{
  public  class MaterielBLL
    {
      MaterielDAL dal = new MaterielDAL();

      public DataTable getMaterielListForQuerry(string id, string name, string area, string type, int finish, string standerd, string userid)
      {
          return dal.getMaterielListForQuerry(id, name, area, type, finish, standerd, userid);
      }
       /// <summary>
      /// 获得全部物料信息
      /// </summary>
      /// <returns></returns>
      public DataTable getMaterielList(string name, string area, string type, int finish, string standerd, string userid, string tuhao, string cid)
      {
          return dal.getMaterielList(name, area, type, finish, standerd, userid,tuhao,cid);
      }
       /// <summary>
      /// 获得全部物料信息
      /// </summary>
      /// <returns></returns>
      public DataTable getMaterielList(string name, string area, string type, int finish, string standerd, string userid)
      {
          return dal.getMaterielList(name, area, type, finish, standerd,userid);
      }

      public DataTable getMaterielList(string id, string name, string area, string type, int finish, string standerd, string userid)
      {
          return dal.getMaterielList(id, name, area, type, finish, standerd, userid);
      }
        /// <summary>
      /// 物料是否被使用
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      public bool isInUse(string id,string userid)
      {
          return dal.isInUse(id,userid);
      }

       /// <summary>
      /// 删除物料
      /// </summary>
      /// <param name="c_id">物料编码</param>
      /// <returns></returns>
      public bool delete(List<String> lists)
      {
          return dal.delete(lists);
      }

        /// <summary>
      /// 获得物料的详细信息
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      public T_JB_Materiel getMaterielById(string id)
      {
          return dal.getMaterielById(id);
      }
       /// <summary>
      /// 是否重名
      /// </summary>
      /// <param name="tableName"></param>
      /// <param name="name"></param>
      /// <param name="meno"></param>
      /// <returns></returns>
      public bool isExit(string name, string typeid)
      {
          return dal.isExit(name, typeid);
      }
       /// <summary>
      /// 保存物料信息
      /// </summary>
      /// <param name="user">物料信息</param>
      /// <returns></returns>
      public bool save(T_JB_Materiel materiel,string userid)
      {
          return dal.save(materiel,userid);
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
          return dal.isExit(name, typeid, id);
      }

        /// <summary>
      /// 保存物料信息
      /// </summary>
      /// <param name="user">物料信息</param>
      /// <returns></returns>
      public bool update(T_JB_Materiel materiel)
      {
          return dal.update(materiel);
      }

       /// <summary>
      /// 获得所有要操作的数据
      /// </summary>
      /// <param name="jiaose"></param>
      /// <returns></returns>
      public DataTable getAllInformations(string jiaose)
      {
          return dal.getAllInformations(jiaose);
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
          return dal.setJurisdiction(isyes, isno, userid);
      }
    }
}
