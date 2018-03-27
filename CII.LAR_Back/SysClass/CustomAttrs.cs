using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.SysClass
{
    class CustomAttrs : Attribute
    {
        #region constructor
        public CustomAttrs()
        {

        }

        public CustomAttrs(string key, object value)
        {
            switch (key)
            {
                case "IfEntirelyModify":
                    _ifEntirelyModify = Convert.ToBoolean(value);
                    break;
                default:
                    break;
            }
        }

        #endregion 

        #region private members
        private bool _ifEntirelyModify = false;

        #endregion

        #region public properties
        public bool IfEntirelyModify
        {
            get
            {
                return _ifEntirelyModify;
            }
            //set via constructor
        }
        #endregion
    }
}
