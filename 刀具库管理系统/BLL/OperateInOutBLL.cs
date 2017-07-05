using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Model;
using System.Data;
using Util;

namespace BLL
{
    public class OperateInOutBLL
    {
        OperateInOutDAL dal = new OperateInOutDAL();

        /// <summary>
        /// 获得新出入库单号
        /// </summary>     
        /// <returns></returns>
        public string getInOutID(int inOutType)
        {
            return dal.getInOutID(inOutType);
        }



        /// <summary>
        /// 保存出入库信息
        /// </summary>
        /// <param name="user">货位信息</param>
        /// <returns></returns>
        public bool save(int inOutType, string c_id, DateTime rq, string materiel, string place, int count, string czy, string meno)
        {
            return dal.save(inOutType, c_id, rq, materiel, place, count, czy, meno);
        }

        /// <summary>
        /// 获得出入库明细
        /// </summary>
        /// <param name="materile"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="inout"></param>
        /// <returns></returns>
        public DataTable getList(DateTime startDate, DateTime endDate, string planid, int inout, string mid)
        {
            return dal.getList(startDate, endDate, planid, inout, mid);
        }


        /// <summary>
        /// 托盘是否被使用
        /// </summary>
        /// <param name="place"></param>
        /// <returns></returns>
        public bool isTrayInuse(string tray)
        {
            return dal.isTrayInuse(tray);
        }

        /// <summary>
        /// 货位是否被使用
        /// </summary>
        /// <param name="place"></param>
        /// <returns></returns>
        public bool isPlaceInuse(string place)
        {
            return dal.isPlaceInuse(place);
        }
        /// <summary>
        /// 获得物料的详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T_JB_Materiel getMaterielByIdOrName(string id)
        {
            return dal.getMaterielByIdOrName(id);
        }


        /// <summary>
        /// 手工入库
        /// </summary>
        /// <returns></returns>
        public bool HandIn(DataTable dt, string mainMeno, InOutType type)
        {
            return dal.HandIn(dt, mainMeno, type);
        }

        /// <summary>
        /// 手工出库
        /// </summary>
        /// <param name="user">货位信息</param>
        /// <returns></returns>
        public bool handOut(DataTable dt, string mainMeno, InOutType type)
        {
            return dal.handOut(dt, mainMeno, type);
        }

        public DataTable getAllOutPlace(int inout)
        {
            return dal.getAllOutPlace(inout);
        }

        /// <summary>
        /// 手工入库
        /// </summary>
        /// <param name="user">货位信息</param>
        /// <returns></returns>
        public bool handEmptyIn(string place, string tray, int uselie)
        {

            return dal.handEmptyIn(place, tray, uselie);
        }

        /// 联机入库
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="meno"></param>
        /// <param name="station"></param>
        /// <returns></returns>
        public bool emptyInDolist(string tray, int userlie, string place, string inport)
        {
            return dal.emptyInDolist(tray, userlie, place, inport);
        }

        /// <summary>
        /// 手工出库
        /// </summary>
        /// <param name="user">货位信息</param>
        /// <returns></returns>
        public bool handEmptyOut(string place, string tray)
        {
            return dal.handEmptyOut(place, tray);
        }

        /// <summary>
        /// 根据工位编码获得出口地址
        /// </summary>
        /// <param name="stationid"></param>
        /// <returns></returns>
        public string getOutPlacrSorece(string stationid)
        {
            return dal.getOutPlacrSorece(stationid);
        }

        /// 联机入库
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="meno"></param>
        /// <param name="station"></param>
        /// <returns></returns>
        public bool emptyOutDolist(string tray, int userlie, string place, string inport)
        {
            return dal.emptyOutDolist(tray, userlie, place, inport);
        }

        public DataTable getProductOutList(string planid, string mid)
        {
            return dal.getProductOutList(planid, mid);
        }

        public DataTable getProcedureTimes(DateTime startDate, DateTime endDate)
        {
            DataTable ProcedureTimes = new DataTable();


            #region 构建数据表
            DataColumn column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "人员";
            ProcedureTimes.Columns.Add(column);
            DataColumn column1 = new DataColumn();
            column1.DataType = System.Type.GetType("System.String");
            column1.ColumnName = "工序";
            ProcedureTimes.Columns.Add(column1);
            DataColumn column2 = new DataColumn();
            column2.DataType = System.Type.GetType("System.Int32");
            column2.ColumnName = "数量";
            ProcedureTimes.Columns.Add(column2);

            #endregion

            DataTable dt = dal.getInDetail(startDate, endDate);
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string peoname = dt.Rows[i]["c_people_id"].ToString();
                    string cname = dt.Rows[i]["C_MATERIEL"].ToString();
                    int count = Convert.ToInt32(dt.Rows[i]["DEC_COUNT"]);
                    string proid = dt.Rows[i]["c_Procedure"].ToString();
                    int pcount = dal.getValue(cname, proid);
                    string pname = dt.Rows[i]["c_name"].ToString();
                    int tcount = count * pcount;
                    int index = -1;
                    for (int j = 0; j < ProcedureTimes.Rows.Count; j++)
                    {
                        string tpeoname = Convert.ToString(ProcedureTimes.Rows[j][0]);
                        string tpname = Convert.ToString(ProcedureTimes.Rows[j][1]);
                        if (tpeoname.Equals(peoname) && tpname.Equals(pname))
                        {
                            index = j;
                            break;
                        }
                    }
                    if (index == -1)
                    {
                        DataRow dr = ProcedureTimes.NewRow();
                        dr[0] = peoname;
                        dr[1] = pname;
                        dr[2] = tcount;
                        ProcedureTimes.Rows.Add(dr);
                    }
                    else
                    {
                        ProcedureTimes.Rows[index][2] = Convert.ToInt32(ProcedureTimes.Rows[index][2]) + tcount;
                    }



                }
            }
            return ProcedureTimes;
        }

        public DataTable getArea(string planid, DateTime startDate, DateTime endDate, string makes, string thick, bool addMain)
        {
            DataTable area = new DataTable();
            #region 构建数据表
            DataColumn columna1 = new DataColumn();
            columna1.DataType = System.Type.GetType("System.String");
            columna1.ColumnName = "id";
            area.Columns.Add(columna1);
            DataColumn columna2 = new DataColumn();
            columna2.DataType = System.Type.GetType("System.Double");
            columna2.ColumnName = "thick";
            area.Columns.Add(columna2);
            DataColumn columna3 = new DataColumn();
            columna3.DataType = System.Type.GetType("System.Double");
            columna3.ColumnName = "area";
            area.Columns.Add(columna3);
            DataColumn columna4 = new DataColumn();
            columna4.DataType = System.Type.GetType("System.Double");
            columna4.ColumnName = "weight";
            area.Columns.Add(columna4);
            #endregion


            DataTable comps = new DataTable();
            #region 构建数据表
            DataColumn column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "id";
            comps.Columns.Add(column);
            DataColumn column2 = new DataColumn();
            column2.DataType = System.Type.GetType("System.Int32");
            column2.ColumnName = "count";
            comps.Columns.Add(column2);

            #endregion

            #region 获得零件和数量
            DataTable plans = dal.getPlanList(planid, startDate, endDate);
            for (int i = 0; i < plans.Rows.Count; i++)
            {
                string mainid = Convert.ToString(plans.Rows[i]["C_PRODUCT_ID"]);
                int mainCount = Convert.ToInt32(plans.Rows[i]["I_COUNT"]);
                DataTable subs = dal.getSubList(mainid);
                for (int j = 0; j < subs.Rows.Count; j++)
                {
                    string subid = Convert.ToString(subs.Rows[j]["C_COMPONENT_SUB"]);
                    int subCount = Convert.ToInt32(subs.Rows[j]["I_COUNT"]);

                    int index = -1;
                    for (int k = 0; k < comps.Rows.Count; k++)
                    {
                        string tempid = Convert.ToString(comps.Rows[k]["id"]);
                        if (subid.Equals(tempid))
                        {
                            index = k;
                            break;
                        }
                    }

                    if (index == -1)
                    {
                        DataRow dr = comps.NewRow();
                        dr[0] = subid;
                        dr[1] = subCount * mainCount;
                        comps.Rows.Add(dr);
                    }
                    else
                    {
                        int tempCount = Convert.ToInt32(comps.Rows[index][1]);
                        comps.Rows[index][1] = subCount * mainCount + tempCount;
                    }
                }
            }

            if (addMain)
            {
                for (int i = 0; i < plans.Rows.Count; i++)
                {
                    string mainid = Convert.ToString(plans.Rows[i]["C_PRODUCT_ID"]);
                    int mainCount = Convert.ToInt32(plans.Rows[i]["I_COUNT"]);
                    int index = -1;
                    for (int k = 0; k < comps.Rows.Count; k++)
                    {
                        string tempid = Convert.ToString(comps.Rows[k]["id"]);
                        if (mainid.Equals(tempid))
                        {
                            index = k;
                            break;
                        }
                    }

                    if (index == -1)
                    {
                        DataRow dr = comps.NewRow();
                        dr[0] = mainid;
                        dr[1] = mainCount;
                        comps.Rows.Add(dr);
                    }
                    else
                    {
                        int tempCount = Convert.ToInt32(comps.Rows[index][1]);
                        comps.Rows[index][1] = mainCount + tempCount;
                    }
                }
            }
            #endregion

            for (int i = 0; i < comps.Rows.Count; i++)
            {
                string cid = Convert.ToString(comps.Rows[i]["id"]);
                int ccount = Convert.ToInt32(comps.Rows[i]["count"]);
                DataTable temp = dal.getSubMess(cid);
                bool flag = false;
                if (temp.Rows.Count > 0)
                {
                    string tempmakes = Convert.ToString(temp.Rows[0]["c_makings"]);
                    double tempTicks = Convert.ToDouble(temp.Rows[0]["i_thick"]);
                    if (tempTicks != 0)
                    {
                        #region 判断材料和厚度是否符合查询条件
                        if (!(string.IsNullOrEmpty(makes)) && !(string.IsNullOrEmpty(thick)))
                        {
                            if (makes.Equals(tempmakes) && Convert.ToDouble(thick) == tempTicks)
                            {
                                flag = true;
                            }
                        }
                        else if (!(string.IsNullOrEmpty(makes)))
                        {
                            if (makes.Equals(tempmakes))
                            {
                                flag = true;
                            }
                        }
                        else if (!(string.IsNullOrEmpty(thick)))
                        {
                            if (Convert.ToDouble(thick) == tempTicks)
                            {
                                flag = true;
                            }
                        }
                        else
                        {
                            flag = true;
                        }
                        #endregion
                        if (flag)
                        {
                            int exitIndex = -1;
                            for (int j = 0; j < area.Rows.Count; j++)
                            {
                                string exitmakes = Convert.ToString(area.Rows[j]["id"]);
                                double exitTicks = Convert.ToDouble(area.Rows[j]["thick"]);
                                if (tempmakes.Equals(exitmakes) && tempTicks == exitTicks)
                                {
                                    exitIndex = j;
                                    break;
                                }
                            }
                            if (exitIndex != -1)
                            {
                                double temparea = Convert.ToDouble(temp.Rows[0]["d_acreage"]);
                                double tempweight = Convert.ToDouble(temp.Rows[0]["d_weight"]);

                                double exitArea = Convert.ToDouble(area.Rows[exitIndex]["area"]);
                                double exitweight = Convert.ToDouble(area.Rows[exitIndex]["weight"]);

                                area.Rows[exitIndex]["area"] = Math.Round(exitArea + temparea * ccount, 4);
                                area.Rows[exitIndex]["weight"] = Math.Round(exitweight + tempweight * ccount, 4);
                            }
                            else
                            {
                                double temparea = Convert.ToDouble(temp.Rows[0]["d_acreage"]);
                                double tempweight = Convert.ToDouble(temp.Rows[0]["d_weight"]);
                                DataRow dr = area.NewRow();
                                dr[0] = tempmakes;
                                dr[1] = tempTicks;
                                dr[2] = Math.Round(temparea * ccount, 4);
                                dr[3] = Math.Round(tempweight * ccount, 4);
                                area.Rows.Add(dr);
                            }
                        }
                    }
                }
            }

            return area;
        }

        public DataTable getProcedureALLTimes(string planid, DateTime startDate, DateTime endDate, string procedure)
        {

            DataTable ProcedureTimes = new DataTable();


            #region 构建数据表

            DataColumn column1 = new DataColumn();
            column1.DataType = System.Type.GetType("System.String");
            column1.ColumnName = "工序";
            ProcedureTimes.Columns.Add(column1);
            DataColumn column2 = new DataColumn();
            column2.DataType = System.Type.GetType("System.Int32");
            column2.ColumnName = "数量";
            ProcedureTimes.Columns.Add(column2);

            #endregion

            DataTable dt = dal.getInAllDetail(planid, startDate, endDate, procedure);
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string cname = dt.Rows[i]["C_MATERIEL"].ToString();
                    int count = Convert.ToInt32(dt.Rows[i]["DEC_COUNT"]);
                    string proid = dt.Rows[i]["c_Procedure"].ToString();
                    int pcount = dal.getValue(cname, proid);
                    string pname = dt.Rows[i]["c_name"].ToString();
                    int tcount = count * pcount;

                    int index = -1;
                    for (int j = 0; j < ProcedureTimes.Rows.Count; j++)
                    {
                        string temppname = Convert.ToString(ProcedureTimes.Rows[j][0]);
                        if (pname.Equals(temppname))
                        {
                            index = j;
                            break;
                        }
                    }
                    if (index == -1)
                    {
                        DataRow dr = ProcedureTimes.NewRow();

                        dr[0] = pname;
                        dr[1] = tcount;
                        ProcedureTimes.Rows.Add(dr);
                    }
                    else
                    {
                        int tempcount = Convert.ToInt32(ProcedureTimes.Rows[index][1]);
                        ProcedureTimes.Rows[index][1] = tempcount + tcount;
                    }

                }
            }
            return ProcedureTimes;
        }

        public DataTable getArea(string[] ids)
        {
            DataTable area = new DataTable();
            #region 构建数据表
            DataColumn columna1 = new DataColumn();
            columna1.DataType = System.Type.GetType("System.String");
            columna1.ColumnName = "id";
            area.Columns.Add(columna1);
            DataColumn columna2 = new DataColumn();
            columna2.DataType = System.Type.GetType("System.Double");
            columna2.ColumnName = "thick";
            area.Columns.Add(columna2);
            DataColumn columna3 = new DataColumn();
            columna3.DataType = System.Type.GetType("System.Double");
            columna3.ColumnName = "area";
            area.Columns.Add(columna3);
            DataColumn columna4 = new DataColumn();
            columna4.DataType = System.Type.GetType("System.Double");
            columna4.ColumnName = "weight";
            area.Columns.Add(columna4);
            #endregion


            DataTable comps = new DataTable();
            #region 构建数据表
            DataColumn column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "id";
            comps.Columns.Add(column);
            DataColumn column2 = new DataColumn();
            column2.DataType = System.Type.GetType("System.Int32");
            column2.ColumnName = "count";
            comps.Columns.Add(column2);

            #endregion

            #region 获得零件和数量
            foreach (string planid in ids)
            {
                DataTable plans = dal.getPlanList(planid);
                for (int i = 0; i < plans.Rows.Count; i++)
                {
                    string mainid = Convert.ToString(plans.Rows[i]["C_PRODUCT_ID"]);
                    int mainCount = Convert.ToInt32(plans.Rows[i]["I_COUNT"]);
                    DataTable subs = dal.getSubList(mainid);
                    for (int j = 0; j < subs.Rows.Count; j++)
                    {
                        string subid = Convert.ToString(subs.Rows[j]["C_COMPONENT_SUB"]);
                        int subCount = Convert.ToInt32(subs.Rows[j]["I_COUNT"]);

                        int index = -1;
                        for (int k = 0; k < comps.Rows.Count; k++)
                        {
                            string tempid = Convert.ToString(comps.Rows[k]["id"]);
                            if (subid.Equals(tempid))
                            {
                                index = k;
                                break;
                            }
                        }

                        if (index == -1)
                        {
                            DataRow dr = comps.NewRow();
                            dr[0] = subid;
                            dr[1] = subCount * mainCount;
                            comps.Rows.Add(dr);
                        }
                        else
                        {
                            int tempCount = Convert.ToInt32(comps.Rows[index][1]);
                            comps.Rows[index][1] = subCount * mainCount + tempCount;
                        }
                    }
                }

                if (true)
                {
                    for (int i = 0; i < plans.Rows.Count; i++)
                    {
                        string mainid = Convert.ToString(plans.Rows[i]["C_PRODUCT_ID"]);
                        int mainCount = Convert.ToInt32(plans.Rows[i]["I_COUNT"]);
                        int index = -1;
                        for (int k = 0; k < comps.Rows.Count; k++)
                        {
                            string tempid = Convert.ToString(comps.Rows[k]["id"]);
                            if (mainid.Equals(tempid))
                            {
                                index = k;
                                break;
                            }
                        }

                        if (index == -1)
                        {
                            DataRow dr = comps.NewRow();
                            dr[0] = mainid;
                            dr[1] = mainCount;
                            comps.Rows.Add(dr);
                        }
                        else
                        {
                            int tempCount = Convert.ToInt32(comps.Rows[index][1]);
                            comps.Rows[index][1] = mainCount + tempCount;
                        }
                    }
                }
            #endregion

            }

            return comps;
        }

        /// <summary>
        /// 是否有联机任务
        /// </summary>
        /// <returns></returns>
        public bool HasDoList()
        {
            return dal.HasDoList();
            
        }
    }
}
