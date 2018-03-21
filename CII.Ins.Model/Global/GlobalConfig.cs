//==================================================================================================
// 全局变量定义类
// 创建人：刘海生
// 创建时间: 2015.11.13
//
// 修改人 修改时间 修改后版本 修改内容
//
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CII.Ins.Model.GlobalConfig
{
    /// <summary>
    /// 全局变量定义
    /// 创建人：刘海生
    /// 创建时间: 2015.11.13
    /// </summary>
    public class GlobalConfig
    {
        /// <summary>
        /// 0
        /// </summary>
        public static readonly string D = "0";
        /// <summary>
        /// 0.0
        /// </summary>
        public static readonly string D0 = "0.0";
        /// <summary>
        /// 0.00
        /// </summary>
        public static readonly string D00 = "0.00";
        /// <summary>
        /// --
        /// </summary>
        public static readonly string DNone = "--";
        /// <summary>
        /// 日期格式
        /// </summary>
        public static readonly string DD = "yyyy-MM-dd";
        /// <summary>
        /// 全时间格式
        /// </summary>
        public static readonly string DT = "yyyy-MM-dd HH:mm:ss";
        /// <summary>
        /// 仪器版本号
        /// </summary>
        public static readonly string Version = "Version";
        /// <summary>
        /// 报警源
        /// </summary>
        public static readonly string AlarmSource = "HV";
        /// <summary>
        /// 最后一次导趋势文本数据的位置
        /// </summary>
        public static string LastExportDataPath = "LastExportDataPath";
        /// <summary>
        /// 最后一次打开方法文件的位置
        /// </summary>
        public static string LastOpenMethodPath = "LastOpenMethodPath";
        /// <summary>
        /// 最后一次保存方法文件名称
        /// </summary>
        public static string LastSaveMethodPathFileName = "LastSaveMethodPathFileName";
        /// <summary>
        /// txt默认扩展名
        /// </summary>
        public static string ExtDataDefaultTxt = "txt";
        /// <summary>
        /// 服务端端口
        /// </summary>
        public static int ServerPort = 10000;
        /// <summary>
        /// 日平均数据保留天数
        /// </summary>
        public static int DailyAverageMaxDay = 30;
        /// <summary>
        /// 允许最大的通信失败次数
        /// </summary>
        public static int EquipmentConnectFailedMaxCount = 2;

        public static string PortManagerPipeName = "HVPipe";

        public static string PortManagerCOMBusName = "CII.Library.CIINet.Buses.SPCommBus";

        public static string PortManagerCOMBusPort = "port";

        public static string PortManagerCOMBusBaud = "baud";

        public static string PortManagerCOMBusDataBit = "dataBit";

        public static string PortManagerCOMBusStopBit = "stopBit";

        public static string PortManagerProtocolName = "CII.Ins.Model.Protocols.InsCommunicationProtol";

        public static string PortManagerRouterPort = "CII.Library.CIINet.Ports.CIIPorts.CIIRouterPort";

        public static string PortManagerPCAddress = "address";


        //public static string PortManagerCOMBusParity = "";
    }
}