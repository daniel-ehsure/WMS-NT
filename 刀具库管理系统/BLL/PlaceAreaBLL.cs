using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Model;
using System.Collections;
using System.Data;

namespace BLL
{
    /// <summary>
    /// 库房BLL
    /// </summary>
    public class PlaceAreaBLL
    {
        PlaceAreaDAL dal = new PlaceAreaDAL();

        public DataTable GetList(string name)
        {
            return dal.GetList(name);
        }

        public string GetNextCode()
        {
            return dal.GetNextCode(2);
        }

        public bool IsExit(string name)
        {
            return dal.IsExit(name);
        }

        public bool IsExitNotSelf(string id, string name)
        {
            return dal.IsExitNotSelf(id, name);
        }

        public bool Save(T_JB_PLACEAREA mo)
        {
            return dal.Save(mo);
        }

        public T_JB_PLACEAREA GetById(string id)
        {
            return dal.GetById(id);
        }

        public bool Update(T_JB_PLACEAREA mo)
        {
            return dal.Update(mo);
        }

        /// <summary>
        /// 是否试用中
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool IsUse(string id)
        {
            return dal.IsUse(id);
        }

        public bool delete(List<String> lists)
        {
            return dal.delete(lists);
        }
    }
}
