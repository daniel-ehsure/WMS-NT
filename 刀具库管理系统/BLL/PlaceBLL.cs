using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Model;
using System.Data;

namespace BLL
{
   public class PlaceBLL
    {
       PlaceDAL dal = new PlaceDAL();
       /// <summary>
        /// 根据类型编号获得子类型信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
       public List<T_JB_Place> GetAllChild(string pid, int grade)
       {
           return dal.getAllChild(pid, grade);
       }

        /// <summary>
        /// 获得全部信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="name"></param>
        /// <param name="meno"></param>
        /// <returns></returns>
       public DataTable GetList(string pid, string name, string meno, int end, int grade)
       {
           return dal.getList(pid, name, meno, end, grade);
       }

       /// <summary>
       /// 根据仓库和名称获得货位（最小控制单元）
       /// </summary>
       /// <param name="tableName"></param>
       /// <returns></returns>
       public DataTable getListByWN(string warehouseId, string name)
       {
           return dal.getListByWN(warehouseId, name);
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
       public string Save(T_JB_Place dm_type)
       {
           return dal.Save(dm_type);
       }

       /// <summary>
       /// 批量保存
       /// </summary>
       /// <param name="tableName"></param>
       /// <param name="name"></param>
       /// <param name="meno"></param>
       /// <returns></returns>
       public bool SaveList(List<List<object>> list, string pid, int grade)
       {
           return dal.SaveList(list, pid, grade);
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
       public T_JB_Place GetById(string id)
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
       /// 更新货位
        /// </summary>
       /// <param name="place">货位</param>
        /// <returns></returns>
       public bool Update(T_JB_Place place)
       {
           return dal.update(place);
       }

       /// <summary>
       /// 更新货位的货区
       /// </summary>
       /// <returns></returns>
       public bool UpdateArea(List<String> places, string placeArea)
       {
           return dal.UpdateArea(places, placeArea);
       }

       /// <summary>
       /// 根据上级获得信息
       /// </summary>
       /// <param name="tableName"></param>
       /// <param name="id"></param>
       /// <returns></returns>
       public DataTable GetByPreId(string preId)
       {
           return dal.GetByPreId(preId);
       }

       /// <summary>
       /// 根据上级、货区、类型获得信息
       /// </summary>
       /// <param name="tableName"></param>
       /// <param name="id"></param>
       /// <returns></returns>
       public DataTable GetPlaceList(string preId, string area, int type)
       {
           return dal.GetPlaceList(preId, area, type);
       }
    }
}
