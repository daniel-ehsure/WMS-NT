using System;
using System.Collections.Generic;
using System.Text;

namespace Util
{
    public class Common
    {
        public static string GetInOutCode(InOutType inOutType)
        {
            string code;

            switch (inOutType)
            {
                case InOutType.MATERIEL_OUT:
                    code = "OM";
                    break;
                case InOutType.MATERIEL_IN:
                    code = "IM";
                    break;
                case InOutType.KNIFE_OUT_USE:
                    code = "OU";
                    break;
                case InOutType.KNIFE_IN:
                    code = "IK";
                    break;
                case InOutType.KNIFE_IN_USE:
                    code = "IU";
                    break;
                default:
                    code = "";
                    break;
            }

            return code;
        }
    }
}
