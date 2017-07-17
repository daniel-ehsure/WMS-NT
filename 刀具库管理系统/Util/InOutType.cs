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
        MATERIEL_OUT = 10,
        /// <summary>
        /// 物料入库
        /// </summary>
        MATERIEL_IN = 20,
        /// <summary>
        /// 借刀具出库
        /// </summary>
        KNIFE_OUT_USE = 12,
        /// <summary>
        /// 新刀具入库
        /// </summary>
        KNIFE_IN = 21,
        /// <summary>
        /// 还刀具入库
        /// </summary>
        KNIFE_IN_USE = 22
    }
}
