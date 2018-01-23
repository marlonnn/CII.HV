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

namespace CII.Ins.Model.Data.LAR
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

    public class ResponseCode
    {
        /// <summary>
        /// 写回应码
        /// </summary>
        private byte code;
        public byte Code
        {
            get { return this.code; }
            set { this.code = value; }
        }

        public string GetResponseCode()
        {
            string codeString = "";
            switch (code)
            {
                case 0x00:
                    codeString = "系统正常工作中";
                    break;
                case 0x11:
                    codeString = "系统设定中";
                    break;
                case 0x22:
                    codeString = "系统标定中";
                    break;
                case 0x33:
                    codeString = "系统初始化中";
                    break;
                case 0x44:
                    codeString = "系统自检中";
                    break;
                case 0x55:
                    codeString = "系统故障中";
                    break;
                case 0x66:
                    codeString = "系统调零中";
                    break;
                case 0x77:
                    codeString = "系统预标定中";
                    break;
                case 0x88:
                    codeString = "写命令成功";
                    break;
                case 0x99:
                    codeString = "网络忙(写命令失败)";
                    break;
                case 0xAA:
                    codeString = "写入数据非法或者超限";
                    break;
            }
            return codeString;
        }
    }

    /// <summary>
    /// HV读取数据
    /// </summary>
    public class MonitorData
    {
        /// <summary>
        /// 电机开关 启用和不启用
        /// </summary>
        private byte motor1Switch;
        public byte Motor1Switch
        {
            get { return this.motor1Switch; }
            set { this.motor1Switch = value; }
        }

        /// <summary>
        /// 电机控制状态
        /// </summary>
        private byte motor1Status;
        public byte Motor1Status
        {
            get { return this.motor1Status; }
            set { this.motor1Status = value; }
        }

        /// <summary>
        /// 已完成控制步数
        /// </summary>
        private int motor1completeSteps;
        public int Motor1completeSteps
        {
            get { return this.motor1completeSteps; }
            set { this.motor1completeSteps = value; }
        }

        /// <summary>
        /// 累计控制步数
        /// </summary>
        private int motor1Steps;
        public int Motor1Steps
        {
            get { return this.motor1Steps; }
            set { this.motor1Steps = value; }
        }

        /// <summary>
        /// 电机开关 启用和不启用
        /// </summary>
        private byte motor2Switch;
        public byte Motor2Switch
        {
            get { return this.motor2Switch; }
            set { this.motor2Switch = value; }
        }

        /// <summary>
        /// 电机控制状态
        /// </summary>
        private byte motor2Status;
        public byte Motor2Status
        {
            get { return this.motor2Status; }
            set { this.motor2Status = value; }
        }

        /// <summary>
        /// 已完成控制步数
        /// </summary>
        private int motor2completeSteps;
        public int Motor2completeSteps
        {
            get { return this.motor2completeSteps; }
            set { this.motor2completeSteps = value; }
        }

        /// <summary>
        /// 累计控制步数
        /// </summary>
        private int motor2Steps;
        public int Motor2Steps
        {
            get { return this.motor2Steps; }
            set { this.motor2Steps = value; }
        }

        /// <summary>
        /// 电压1
        /// </summary>
        private float voltage1;
        public float Voltage1
        {
            get { return this.voltage1; }
            set { this.voltage1 = value; }
        }

        /// <summary>
        /// 电压2
        /// </summary>
        private float voltage2;
        public float Voltage2
        {
            get { return this.voltage2; }
            set { this.voltage2 = value; }
        }

        /// <summary>
        /// CPU温度
        /// </summary>
        private float temperature;
        public float Temperature
        {
            get { return this.temperature; }
            set { this.temperature = value; }
        }

        /// <summary>
        /// 电机驱动1限流电阻1电压
        /// </summary>
        private float m1v1;
        public float M1V1
        {
            get { return this.m1v1; }
            set { this.m1v1 = value; }
        }

        /// <summary>
        /// 电机驱动1限流电阻2电压
        /// </summary>
        private float m1v2;
        public float M1V2
        {
            get { return this.m1v2; }
            set { this.m1v2 = value; }
        }

        /// <summary>
        /// 电机驱动2限流电阻1电压
        /// </summary>
        private float m2v1;
        public float M2V1
        {
            get { return this.m2v1; }
            set { this.m2v1 = value; }
        }

        /// <summary>
        /// 电机驱动2限流电阻2电压
        /// </summary>
        private float m2v2;
        public float M2V2
        {
            get { return this.m2v2; }
            set { this.m2v2 = value; }
        }

        /// <summary>
        /// 51字节保留
        /// </summary>
        private byte[] remainedBytes = new byte[51];
        public byte[] RemainedBytes
        {
            get { return this.remainedBytes; }
            set { this.remainedBytes = value; }
        }

        /// <summary>
        /// 50字节报警码
        /// </summary>
        private byte[] errorAlarmCodes = new byte[50];
        public byte[] ErrorAlarmCodes
        {
            get { return this.errorAlarmCodes; }
            set { this.errorAlarmCodes = value; }
        }
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