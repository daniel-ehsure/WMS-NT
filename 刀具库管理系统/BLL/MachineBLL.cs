using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DAL;

namespace BLL
{
    public class MachineBLL
    {
        MachineDAL dal = new MachineDAL();

        public DataTable GetList()
        {
            return dal.GetList();
        }

        public bool Save(string id)
        {
            return dal.Save(id);
        }
    }
}
