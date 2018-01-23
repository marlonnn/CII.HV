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
    public class Method : IdNameNode
    {
        /// <summary>
        /// 当前直接指定仪器型号：HV
        /// </summary>
        private ICommandHelper ICommandHelper = new HVCommandHelper();

        /// <summary>
        /// 读取监控信息
        /// </summary>
        /// <returns></returns>
        public MonitorData GetMonitorData()
        {
            return ICommandHelper.GetMonitorData();
        }

        /// <summary>
        /// 设置控制参数
        /// </summary>
        public void SetMethod()
        {
            ICommandHelper.SetHvFrequency(Frequency, Voltage);
        }

        public uint frequency = 0;
        /// <summary>
        /// 频率
        /// </summary>
        public uint Frequency
        {
            get { return frequency; }
            set { frequency = value; }
        }

        public uint voltage = 0;
        /// <summary>
        /// 电压
        /// </summary>
        public uint Voltage
        {
            get { return voltage; }
            set { voltage = value; }
        }

    }
}