using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
   public class T_JB_COMPONENT_PROCEDURE
    {
        private string _C_COMPONENT_ID;
        private string _C_PROCEDURE_ID;
        private int _I_VALUE;
        private int _I_sequence;

        public int I_sequence
        {
            get { return _I_sequence; }
            set { _I_sequence = value; }
        }

        public string C_COMPONENT_ID
        {
            get
            {
                return this._C_COMPONENT_ID;
            }
            set
            {
                this._C_COMPONENT_ID = value;
            }
        }

        public string C_PROCEDURE_ID
        {
            get
            {
                return this._C_PROCEDURE_ID;
            }
            set
            {
                this._C_PROCEDURE_ID = value;
            }
        }

        public int I_VALUE
        {
            get
            {
                return this._I_VALUE;
            }
            set
            {
                this._I_VALUE = value;
            }
        }
    }
}
