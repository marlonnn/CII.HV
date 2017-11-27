//==================================================================================================
// Cell通信操作类接口定义，本类内函数定义以流水式顺序罗列
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
using CII.Ins.Model.Data.HV;
using CII.Ins.Business.Instrument;

namespace CII.Ins.Business.Command.Interface
{
    /// <summary>
    /// Cell100通信操作类接口定义，本类内函数定义以流水式顺序罗列
    /// 创建人：刘海生
    /// 创建时间: 2015.11.13
    /// </summary>
    public interface ICommandHelper
    {
        /// <summary>
        /// 0x44、0x45读取监控数据
        /// </summary>
        /// <returns></returns>
        MonitorData GetMonitorData();

        /// <summary>
        /// 0x41设置高压频率、电压
        /// </summary>
        void SetHvFrequency(uint frequency, uint voltage);
    }
}