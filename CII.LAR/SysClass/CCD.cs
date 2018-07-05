using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.SysClass
{
    [Serializable]
    public class CCD
    {
        private CCDType ccdType;
        public CCDType CType
        {
            get { return this.ccdType; }
            set { this.ccdType = value; }
        }

        public int Length
        {
            get { return this.GetCCDLength(); }
        }

        public CCD()
        {
            SetDefault();
        }

        private void SetDefault()
        {
            this.ccdType = CCDType.OneForth;
        }

        /// <summary>
        /// CCD行业标准
        /// CCD对角线的长度：指的是CCD的芯片尺寸，常有的是1/3英寸、1/2英寸、2/3英寸的，相对应的长度分别为6mm；8mm；11mm
        /// </summary>
        /// <returns></returns>
        public int GetCCDLength()
        {
            int length = 0;
            switch (ccdType)
            {
                case CCDType.OneForth:
                    length = 4;
                    break;
                case CCDType.OneThird:
                    length = 6;
                    break;
                case CCDType.OneSecond:
                    length = 8;
                    break;
                case CCDType.TwoThird:
                    length = 11;
                    break;
            }
            return length;
        }
    }
}
