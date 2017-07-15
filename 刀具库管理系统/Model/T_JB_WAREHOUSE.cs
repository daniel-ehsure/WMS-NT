using System;
using System.Collections.Generic;
using System.Data;

namespace Model
{
    public class T_JB_WAREHOUSE
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

        private String _c_TYPE = string.Empty;
        /// <summary>
        /// C_TYPE
        /// </summary>
        public String C_TYPE
        {
            get
            {
                return _c_TYPE;
            }
            set
            {
                _c_TYPE = value;
            }
        }

        private String _c_TYPE_NAME = string.Empty;
        /// <summary>
        /// C_TYPE
        /// </summary>
        public String C_TYPE_NAME
        {
            get
            {
                return _c_TYPE_NAME;
            }
            set
            {
                _c_TYPE_NAME = value;
            }
        }

        private String _c_COM = "COM1";
        /// <summary>
        /// C_COM
        /// </summary>
        public String C_COM
        {
            get
            {
                return _c_COM;
            }
            set
            {
                _c_COM = value;
            }
        }

        private String _c_BAUDRATE = "9600";
        /// <summary>
        /// C_BAUDRATE
        /// </summary>
        public String C_BAUDRATE
        {
            get
            {
                return _c_BAUDRATE;
            }
            set
            {
                _c_BAUDRATE = value;
            }
        }

        private String _c_IP_ADDRESS = string.Empty;
        /// <summary>
        /// C_IP_ADDRESS
        /// </summary>
        public String C_IP_ADDRESS
        {
            get
            {
                return _c_IP_ADDRESS;
            }
            set
            {
                _c_IP_ADDRESS = value;
            }
        }

        private String _c_PORT = string.Empty;
        /// <summary>
        /// C_PORT
        /// </summary>
        public String C_PORT
        {
            get
            {
                return _c_PORT;
            }
            set
            {
                _c_PORT = value;
            }
        }

        private String _c_WRITE_PORT = string.Empty;
        /// <summary>
        /// C_WRITE_PORT
        /// </summary>
        public String C_WRITE_PORT
        {
            get
            {
                return _c_WRITE_PORT;
            }
            set
            {
                _c_WRITE_PORT = value;
            }
        }

        private String _c_READ_PORT = string.Empty;
        /// <summary>
        /// C_READ_PORT
        /// </summary>
        public String C_READ_PORT
        {
            get
            {
                return _c_READ_PORT;
            }
            set
            {
                _c_READ_PORT = value;
            }
        }

        private int _i_AUTO = 0;
        /// <summary>
        /// I_AUTO
        /// </summary>
        public int I_AUTO
        {
            get
            {
                return _i_AUTO;
            }
            set
            {
                _i_AUTO = value;
            }
        }

        private int _i_IN_MOBILE = 0;
        /// <summary>
        /// I_IN_MOBILE
        /// </summary>
        public int I_IN_MOBILE
        {
            get
            {
                return _i_IN_MOBILE;
            }
            set
            {
                _i_IN_MOBILE = value;
            }
        }

        private int _i_OUT_MOBILE = 0;
        /// <summary>
        /// I_OUT_MOBILE
        /// </summary>
        public int I_OUT_MOBILE
        {
            get
            {
                return _i_OUT_MOBILE;
            }
            set
            {
                _i_OUT_MOBILE = value;
            }
        }
    }
}
