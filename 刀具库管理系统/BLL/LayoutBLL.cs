using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Model;
using System.Collections;
using System.Data;

namespace BLL
{
    public class LayoutBLL
    {
        LayoutDAL dal = new LayoutDAL();
        /// <summary>
        /// 根据用户角色获得可以显示的所有功能组名称
        /// </summary>
        /// <returns>组名称列表</returns>
        public List<string> getAllGroupName()
        {

            try
            {
                return dal.getAllGroupName();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 通过功能组名称根据用户角色获得 组内所以可以显示的项目的名称
        /// </summary>
        /// <param name="groupname">功能组名称</param>
        /// <returns>项目名称列表</returns>
        public List<Outlook_Table_Jb_User> getItemNameByGroupName(string groupname)
        {

            try
            {
                return dal.getItemNameByGroupName(groupname);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 初始化数据字典，该字典用来维护点击功能打开哪个窗体
        /// </summary>
        /// <returns>项目名称和窗体名称的映射</returns>
        public Hashtable initDictionary()
        {

            try
            {
                return dal.initDictionary();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        /// <summary>
        /// 是否为合法用户
        /// </summary>
        /// <param name="user">用户信息的数据封装对象</param>
        /// <returns>是否合法</returns>
        public bool login(T_Jb_Login_Pass user)
        {
            try
            {
                return dal.login(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

         /// <summary>
        /// 获得用户的旧密码
        /// </summary>
        /// <param name="name">用户名</param>
        /// <returns></returns>
        public string getPasswordByName(string name)
        {
            try
            {
                return dal.getPasswordByName(name);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                return dal.updateUser(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

         /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="user">系统用户</param>
        /// <returns></returns>
        public bool save(T_Jb_Login_Pass user)
        {
            return dal.save(user);
        }
        // <summary>
      /// 获得全部用户信息
      /// </summary>
      /// <returns></returns>
        public DataTable getUserList(string id, string name)
        {
            return dal.getUserList(id,name);
        }

          /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="c_id">用户名</param>
        /// <returns></returns>
        public bool delte(List<String> lists)
        {
            return dal.delte(lists);
        }

         /// <summary>
      /// 获得用户的详细信息
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
        public T_Jb_Login_Pass getUserById(string id)
        {
            return dal.getUserById(id);
        }

          /// <summary>
        /// 获得全部工位信息
        /// </summary>
        /// <returns></returns>
        public DataTable getStationList(string id, string name)
        {
            return dal.getStationList(id, name);
        }


          /// <summary>
        /// 获得全部用户信息
        /// </summary>
        /// <returns></returns>
        public DataTable getUserList()
        {
            return dal.getUserList();
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
        /// 保存权限信息
        /// </summary>
        /// <param name="jiaose"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        public bool save(string jiaose, List<Outlook_Table_Jb_User> users)
        {
            return dal.save(jiaose, users);
        }
         /// <summary>
        /// 数据库初始化
        /// </summary>
        /// <param name="c_id">用户名</param>
        /// <returns></returns>
        public bool resetDataBase()
        {
            return dal.resetDataBase();
        }

        public DataTable getAllRols(string rol)
        {
            return dal.getAllRols(rol);
        }

        public DataTable getLogByDate(string date)
        {
            return dal.getLogByDate(date);
        }
    }
}
