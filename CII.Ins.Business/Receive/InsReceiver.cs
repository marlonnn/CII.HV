//==================================================================================================
// Cell100仪器消息接收器
// 创建人：刘海生
// 创建时间: 2017.04.25
//
// 修改人 修改时间 修改后版本 修改内容
//
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CII.Library.CIINet.Interfaces;

namespace CII.Ins.Business.Receive
{
    /// <summary>
    /// Cell100仪器消息接收器
    /// 创建人：刘海生
    /// 创建时间: 2017.04.25
    /// </summary>
    public class InsReceiver : IPortOwner
    {
        public delegate void ReceiveHandle(object source, IByteStream data);
        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event ReceiveHandle ReceiveEvent;

        #region IPortOwner 成员

        public void InitPortOwner(IPort port, CII.Library.Xml.BaseNode propertys)
        {

        }

        public void OnDisconnecting(IPort port)
        {

        }

        #endregion

        #region IReceivable 成员

        public void Receive(object source, IByteStream data)
        {
            if (ReceiveEvent != null)
            {
                ReceiveEvent(source, data);
            }
        }

        #endregion

        #region GetInstance()
        private static object SyncObj = new object();
        private static InsReceiver Instance = null;
        public static InsReceiver GetInstance()
        {
            lock (SyncObj)
            {
                if (Instance == null)
                {
                    Instance = new InsReceiver();
                }
            }
            return Instance;
        }
        #endregion
    }
}