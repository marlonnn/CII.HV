using CII.Library.CIINet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR
{
    public class SimpleProtocolData : IOvertime, IByteStream
    {
        private byte[] data;
        private int timeout = 1000;
        private int trytimes = 2;

        public SimpleProtocolData(byte[] data)
        {
            this.data = data;
        }

        #region IByteStream 成员

        public byte[] GetBytes()
        {
            return data;
        }

        #endregion

        #region IOvertime 成员

        public int TimeOut
        {
            get
            {
                return timeout;
            }
            set
            {
                timeout = value;
            }
        }

        public int TryTimes
        {
            get
            {
                return trytimes;
            }
            set
            {
                trytimes = value;
            }
        }

        #endregion
    }
}
