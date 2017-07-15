using System;
using System.Collections.Generic;
using System.Data;

namespace Model
{
    public class T_JB_WH_TYPE
    {
        private String _c_ID = string.Empty;
        /// <summary>
        /// C_ID
        /// </summary>
        public String C_ID
        {
            get
            {
                return _c_ID;
            }
            set
            {
                _c_ID = value;
            }
        }

        private String _c_NAME = string.Empty;
        /// <summary>
        /// C_NAME
        /// </summary>
        public String C_NAME
        {
            get
            {
                return _c_NAME;
            }
            set
            {
                _c_NAME = value;
            }
        }
    }
}
