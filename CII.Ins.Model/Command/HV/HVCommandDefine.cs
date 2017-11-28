//==================================================================================================
// Cell100通信协议类定义
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

namespace CII.Ins.Model.Command.HV
{
    /// <summary>
    /// 通讯编号(ID)
    /// </summary>
    public class CommandId
    {
        /// <summary>
        /// 系统监控
        /// </summary>
        public const string SystemMonitor = "SystemMonitor";

        /// <summary>
        /// 系统参数
        /// </summary>
        public const string SystemParameter = "SystemParameter";


    }


    /// <summary>
    /// 通讯参数编号(ID)
    /// </summary>
    public class ParamId
    {
        /// <summary>
        /// 系统流程状态
        /// </summary>
        public const int SystemMonitor_ReadResponse_FlowStatus = 0xAA00;

        /// <summary>
        /// 电机1状态
        /// </summary>
        public const int SystemMonitor_ReadResponse_Motor1Status = 0xAA01;

        /// <summary>
        /// 电机1控制结果
        /// </summary>
        public const int SystemMonitor_ReadResponse_Motor1Result = 0xAA02;

        /// <summary>
        /// 电机1已完成控制步数
        /// </summary>
        public const int SystemMonitor_ReadResponse_Motor1CompleteSteps = 0xAA03;

        /// <summary>
        /// 电机1累计控制步数
        /// </summary>
        public const int SystemMonitor_ReadResponse_Motor1SumSteps = 0xAA04;

        /// <summary>
        /// 电机2状态
        /// </summary>
        public const int SystemMonitor_ReadResponse_Motor2Status = 0xAA05;

        /// <summary>
        /// 电机2控制结果
        /// </summary>
        public const int SystemMonitor_ReadResponse_Motor2Result = 0xAA06;

        /// <summary>
        /// 电机2已完成控制步数
        /// </summary>
        public const int SystemMonitor_ReadResponse_Motor2CompleteSteps = 0xAA07;

        /// <summary>
        /// 电机2累计控制步数
        /// </summary>
        public const int SystemMonitor_ReadResponse_Motor2SumSteps = 0xAA08;

        /// <summary>
        /// 电压1
        /// </summary>
        public const int SystemMonitor_ReadResponse_AC1 = 0xAA09;

        /// <summary>
        /// 电压2
        /// </summary>
        public const int SystemMonitor_ReadResponse_AC2 = 0xAA0A;

        /// <summary>
        /// CPU温度
        /// </summary>
        public const int SystemMonitor_ReadResponse_CPUTemperature = 0xAA0B;

        /// <summary>
        /// 电机驱动1限流电阻1电压
        /// </summary>
        public const int SystemMonitor_ReadResponse_Motor1AC1 = 0xAA0C;

        /// <summary>
        /// 电机驱动1限流电阻2电压
        /// </summary>
        public const int SystemMonitor_ReadResponse_Motor1AC2 = 0xAA0D;

        /// <summary>
        /// 电机驱动2限流电阻1电压
        /// </summary>
        public const int SystemMonitor_ReadResponse_Motor2AC1 = 0xAA0E;

        /// <summary>
        /// 电机驱动2限流电阻2电压
        /// </summary>
        public const int SystemMonitor_ReadResponse_Motor2AC2 = 0xAA0F;

        /// <summary>
        /// 读取内容选择
        /// </summary>
        public const int SystemParameter_Read_Type = 0x15500;

        /// <summary>
        /// 读取内容选择
        /// </summary>
        public const int SystemParameter_ReadWrite_Type = 0x1FF00;

        /// <summary>
        /// 电机驱动频率
        /// </summary>
        public const int SystemParameter_ReadWrite_MotorDriveFrequency = 0x1FF01;

        /// <summary>
        /// 最大脉冲
        /// </summary>
        public const int SystemParameter_ReadWrite_MaximumPulse = 0x1FF02;


    }
}