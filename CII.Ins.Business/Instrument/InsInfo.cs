//==================================================================================================
// 仪器基本属性类定义
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
using CII.Library.Xml;
using CII.Ins.Business.Command.Interface;
using CII.Ins.Model.Data.LAR;
using CII.Ins.Business.Command.LAR;

namespace CII.Ins.Business.Instrument
{
    /// <summary>
    /// 仪器基本属性类定义
    /// 创建人：刘海生
    /// 创建时间: 2017.04.25
    /// </summary>
    [Serializable]
    public class InsInfo : IdNameNode
    {
        #region -- 仪器属性 保存到文件 --
        public string insName = string.Empty;
        /// <summary>
        /// 仪器名称
        /// </summary>
        public string InsName
        {
            get { return insName; }
            set { insName = value; }
        }

        public string insIP = string.Empty;
        /// <summary>
        /// 仪器ip地址
        /// </summary>
        public string InsIP
        {
            get { return insIP; }
            set { insIP = value; }
        }

        public uint insPort = 15000;
        /// <summary>
        /// 仪器端口
        /// </summary>
        public uint InsPort
        {
            get { return insPort; }
            set { insPort = value; }
        }

        public string insID = string.Empty;
        /// <summary>
        /// 仪器序列号
        /// </summary>
        public string InsID
        {
            get { return insID; }
            set { insID = value; }
        }
        #endregion
    }
}