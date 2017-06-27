using System;
using System.Collections.Generic;
using System.Text;

namespace Util
{
    public enum InOutType
    {
        /// <summary>
        /// 物料出库
        /// </summary>
        MATERIEL_OUT = 1,
        /// <summary>
        /// 物料入库
        /// </summary>
        MATERIEL_IN = 2,
        /// <summary>
        /// 刀具出库
        /// </summary>
        KNIFE_OUT = 3,
        /// <summary>
        /// 刀具入库
        /// </summary>
        KNIFE_IN = 4
    }
}
