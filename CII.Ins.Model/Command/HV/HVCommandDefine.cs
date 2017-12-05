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

        /// <summary>
        /// 控制选项配置
        /// </summary>
        public const string ControlConfig = "ControlConfig";

        /// <summary>
        /// 外设配置
        /// </summary>
        public const string ExternalConfig = "ExternalConfig";

        /// <summary>
        /// 外设寿命配置
        /// </summary>
        public const string LifeConfig = "LifeConfig";

        /// <summary>
        /// 单板测试
        /// </summary>
        public const string SingleBoardTest = "SingleBoardTest";


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

        /// <summary>
        /// 控制项选择
        /// </summary>
        public const int ControlConfig_Read_Select = 0x25500;

        /// <summary>
        /// 控制项选择
        /// </summary>
        public const int ControlConfig_ReadWrite_Select = 0x2FF00;

        /// <summary>
        /// 控制模式1
        /// </summary>
        public const int ControlConfig_ReadWrite_ControlMode1 = 0x2FF01;

        /// <summary>
        /// 控制方向1
        /// </summary>
        public const int ControlConfig_ReadWrite_Direction1 = 0x2FF02;

        /// <summary>
        /// 总步数1
        /// </summary>
        public const int ControlConfig_ReadWrite_TotalSteps1 = 0x2FF03;

        /// <summary>
        /// 控制模式2
        /// </summary>
        public const int ControlConfig_ReadWrite_ControlMode2 = 0x2FF04;

        /// <summary>
        /// 控制方向2
        /// </summary>
        public const int ControlConfig_ReadWrite_Direction2 = 0x2FF05;

        /// <summary>
        /// 总步数2
        /// </summary>
        public const int ControlConfig_ReadWrite_TotalSteps2 = 0x2FF06;

        /// <summary>
        /// 电机1配置
        /// </summary>
        public const int ExternalConfig_ReadWrite_Motor1Config = 0x3FF00;

        /// <summary>
        /// 电机2配置
        /// </summary>
        public const int ExternalConfig_ReadWrite_Motor2Config = 0x3FF01;

        /// <summary>
        /// 电机1开始使用年分
        /// </summary>
        public const int LifeConfig_ReadWrite_Motor1Year = 0x4FF00;

        /// <summary>
        /// 电机1开始使用月份
        /// </summary>
        public const int LifeConfig_ReadWrite_Motor1Month = 0x4FF01;

        /// <summary>
        /// 电机1开始使用日
        /// </summary>
        public const int LifeConfig_ReadWrite_Motor1Day = 0x4FF02;

        /// <summary>
        /// 电机1的寿命
        /// </summary>
        public const int LifeConfig_ReadWrite_Motor1Life = 0x4FF03;

        /// <summary>
        /// 电机1使用时间
        /// </summary>
        public const int LifeConfig_ReadWrite_Motor1UseTime = 0x4FF04;

        /// <summary>
        /// 电机2开始使用年份
        /// </summary>
        public const int LifeConfig_ReadWrite_Motor2Year = 0x4FF05;

        /// <summary>
        /// 电机2开始使用月份
        /// </summary>
        public const int LifeConfig_ReadWrite_Motor2Month = 0x4FF06;

        /// <summary>
        /// 电机2使用天数
        /// </summary>
        public const int LifeConfig_ReadWrite_Motor2Day = 0x4FF07;

        /// <summary>
        /// 电机2的寿命
        /// </summary>
        public const int LifeConfig_ReadWrite_Motor2Life = 0x4FF08;

        /// <summary>
        /// 电机2使用时间
        /// </summary>
        public const int LifeConfig_ReadWrite_Motor2UseTime = 0x4FF09;

        /// <summary>
        /// 传感器开始使用年份
        /// </summary>
        public const int LifeConfig_ReadWrite_SensorYear = 0x4FF0A;

        /// <summary>
        /// 传感器开始使用月份
        /// </summary>
        public const int LifeConfig_ReadWrite_SensorMonth = 0x4FF0B;

        /// <summary>
        /// 传感器开始使用日
        /// </summary>
        public const int LifeConfig_ReadWrite_SensorDay = 0x4FF0C;

        /// <summary>
        /// 传感器的寿命
        /// </summary>
        public const int LifeConfig_ReadWrite_SensorLife = 0x4FF0D;

        /// <summary>
        /// 传感器使用天数
        /// </summary>
        public const int LifeConfig_ReadWrite_SensorUseTime = 0x4FF0E;

        /// <summary>
        /// 控制项选择
        /// </summary>
        public const int SingleBoardTest_Write_Selection = 0x56600;

        /// <summary>
        /// 输出开关
        /// </summary>
        public const int SingleBoardTest_Write_OutputSwitch = 0x56601;

        /// <summary>
        /// 输出占空比
        /// </summary>
        public const int SingleBoardTest_Write_OutputDutyCycle = 0x56602;

        /// <summary>
        /// 看门狗开关
        /// </summary>
        public const int SingleBoardTest_Write_WatchDogSwitch = 0x56603;

        /// <summary>
        /// 看门狗占空比
        /// </summary>
        public const int SingleBoardTest_Write_WatchDogDutyCycle = 0x56604;

        /// <summary>
        /// MCU复位次数开关
        /// </summary>
        public const int SingleBoardTest_Write_MCUSwitch = 0x56605;

        /// <summary>
        /// MCU占空比
        /// </summary>
        public const int SingleBoardTest_Write_MCUDutyCycle = 0x56606;

        /// <summary>
        /// 状态标志
        /// </summary>
        public const int SingleBoardTest_ReadResponse_StateFlag = 0x5AA00;

        /// <summary>
        /// 5字节单板号
        /// </summary>
        public const int SingleBoardTest_ReadResponse_BoardNumber = 0x5AA01;

        /// <summary>
        /// 32字节版本号
        /// </summary>
        public const int SingleBoardTest_ReadResponse_VersionNumber = 0x5AA02;

        /// <summary>
        /// 16字节序列号
        /// </summary>
        public const int SingleBoardTest_ReadResponse_SerialNumber = 0x5AA03;

        /// <summary>
        /// FRAM测试结果
        /// </summary>
        public const int SingleBoardTest_ReadResponse_FRAMTestResult = 0x5AA04;

        /// <summary>
        /// 内部电压1
        /// </summary>
        public const int SingleBoardTest_ReadResponse_InternalVoltage1 = 0x5AA05;

        /// <summary>
        /// 内部电压2
        /// </summary>
        public const int SingleBoardTest_ReadResponse_InternalVoltage2 = 0x5AA06;

        /// <summary>
        /// CPU温度
        /// </summary>
        public const int SingleBoardTest_ReadResponse_CPUTemperature = 0x5AA07;

        /// <summary>
        /// 外部温度1/外部电压1
        /// </summary>
        public const int SingleBoardTest_ReadResponse_TV1 = 0x5AA08;

        /// <summary>
        /// 外部温度2/外部电压2
        /// </summary>
        public const int SingleBoardTest_ReadResponse_TV2 = 0x5AA09;

        /// <summary>
        /// 外部温度3/外部电压3
        /// </summary>
        public const int SingleBoardTest_ReadResponse_TV3 = 0x5AA0A;

        /// <summary>
        /// 外部温度4/外部电压4
        /// </summary>
        public const int SingleBoardTest_ReadResponse_TV4 = 0x5AA0B;

        /// <summary>
        /// 外部温度5/外部电压5
        /// </summary>
        public const int SingleBoardTest_ReadResponse_TV5 = 0x5AA0C;

        /// <summary>
        /// 外部温度6/外部电压6
        /// </summary>
        public const int SingleBoardTest_ReadResponse_TV6 = 0x5AA0D;

        /// <summary>
        /// 外部温度7/外部电压7
        /// </summary>
        public const int SingleBoardTest_ReadResponse_TV7 = 0x5AA0E;

        /// <summary>
        /// 输入1占空比
        /// </summary>
        public const int SingleBoardTest_ReadResponse_DutyCycle1 = 0x5AA0F;

        /// <summary>
        /// 输入1周期
        /// </summary>
        public const int SingleBoardTest_ReadResponse_Cycle1 = 0x5AA10;

        /// <summary>
        /// 输入2占空比
        /// </summary>
        public const int SingleBoardTest_ReadResponse_DutyCycle2 = 0x5AA11;

        /// <summary>
        /// 输入2周期
        /// </summary>
        public const int SingleBoardTest_ReadResponse_Cycle2 = 0x5AA12;

        /// <summary>
        /// 输入3占空比
        /// </summary>
        public const int SingleBoardTest_ReadResponse_DutyCycle3 = 0x5AA13;

        /// <summary>
        /// 输入3周期
        /// </summary>
        public const int SingleBoardTest_ReadResponse_Cycle3 = 0x5AA14;

        /// <summary>
        /// 输入4占空比
        /// </summary>
        public const int SingleBoardTest_ReadResponse_DutyCycle4 = 0x5AA15;

        /// <summary>
        /// 输入4周期
        /// </summary>
        public const int SingleBoardTest_ReadResponse_Cycle4 = 0x5AA16;

        /// <summary>
        /// MCU复位次数
        /// </summary>
        public const int SingleBoardTest_ReadResponse_MCUResetTimes = 0x5AA17;


    }
}