using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using System.Data;
using Model;

namespace BLL
{
  public  class EmployeeBLL
    {
      EmployeeDAL dal = new EmployeeDAL();

       /// <summary>
      /// 获得全部员工信息
      /// </summary>
      /// <returns></returns>
      public DataTable getEmployeeList(string name, string gw, string unit, string cid)
      {
          return dal.getEmployeeList(name, gw, unit, cid);
      }

       /// <summary>
      /// 删除员工
      /// </summary>
      /// <param name="c_id">员工编码</param>
      /// <returns></returns>
      public bool delete(List<String> lists)
      {
          return dal.delete(lists);
      }

        /// <summary>
      /// 获得员工的详细信息
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      public T_JB_EMPLOYEE getEmployeeById(string id)
      {
          return dal.getEmployeeById(id);
      }

       /// <summary>
      /// 保存员工信息
      /// </summary>
      /// <param name="user">员工信息</param>
      /// <returns></returns>
      public bool save(T_JB_EMPLOYEE materiel,string userid)
      {
          return dal.save(materiel,userid);
      }

        /// <summary>
      /// 保存员工信息
      /// </summary>
      /// <param name="user">员工信息</param>
      /// <returns></returns>
      public bool update(T_JB_EMPLOYEE materiel)
      {
          return dal.update(materiel);
      }

      /// <summary>
      /// 获得岗位列表
      /// </summary>
      /// <returns></returns>
      public DataTable GetGWList()
      {
          return dal.GetGWList();
      }
    }
}
