//==================================================================================================
// Cell100数据类类定义
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

namespace CII.Ins.Model.Data.HV
{
    /// <summary>
    /// 消息枚举类型
    /// </summary>
    public enum MsgType : int
    {
        /// <summary>
        /// UI退出
        /// </summary>
        UIExit = 0,
        /// <summary>
        /// UI最大化
        /// </summary>
        UIMax = 1,
        /// <summary>
        /// UI最小化
        /// </summary>
        UIMin = 2,
        /// <summary>
        /// UI菜单
        /// </summary>
        UIMenu = 3,
        /// <summary>
        /// 关于
        /// </summary>
        UIAbout = 4,
        /// <summary>
        /// 帮助
        /// </summary>
        UIHelp = 5,

        /// <summary>
        /// 启动
        /// </summary>
        Start = 100,
        /// <summary>
        /// 停止
        /// </summary>
        Stop = 101,
    }

    /// <summary>
    /// HV读取数据
    /// </summary>
    public class MonitorData
    {
        public byte FlowStatus;
        /// <summary>
        /// 当前电压
        /// </summary>
        public float Voltage = 0;
        /// <summary>
        /// 当前频率
        /// </summary>
        public float Frequency = 0;
        /// <summary>
        /// 当前电流
        /// </summary>
        public float Electric = 0;
        /// <summary>
        /// 当前温度
        /// </summary>
        public byte Temperature = 0x00;
        /// <summary>
        /// 报警码个数
        /// </summary>
        public byte AlarmCount = 0x00;
        /// <summary>
        /// 报警码
        /// </summary>
        public byte AlarmCode = 0x00;
    }

    /// <summary>
    /// 控制模式, 开关
    /// </summary>
    public enum ControlMode : byte
    {
        /// <summary>
        /// 关
        /// </summary>
        Close = 0x00,
        /// <summary>
        /// 开
        /// </summary>
        Open = 0x01,
    }

}