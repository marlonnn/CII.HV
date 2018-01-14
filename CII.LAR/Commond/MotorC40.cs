using CII.LAR.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Commond
{
    /// <summary>
    /// 40 系统监控相关命令
    /// </summary>
    public class MotorC40Request : MotorBaseRequest
    {
        public MotorC40Request() : base() { }

        public MotorC40Request(byte commandCode, byte additionalCode) : base()
        {
            this.CodeArea.CommandCode = commandCode;
            this.CodeArea.AdditionCode = additionalCode;
        }

        public override CIIBasePackage Encode()
        {
            this.CodeArea.DataLength = new byte[] { 0x00, 0x00 };
            return new CIIBasePackage(this.CodeArea);
        }
    }

    public class MotorC40Response : MotorBaseResponse
    {
        /// <summary>
        /// 控制项选择
        /// 读：0x55
        /// 写：0x66
        /// </summary>
        private byte controlSelection;
        public byte ControlSelection
        {
            get { return this.controlSelection; }
            set { this.controlSelection = value; }
        }

        /// <summary>
        /// 系统流程状态
        /// </summary>
        private byte systemFlowStatus;
        public byte SystemFlowStatus
        {
            get { return this.systemFlowStatus; }
            set { this.systemFlowStatus = value; }
        }

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
        public MotorC40Response() : base() { }

        public MotorC40Response(byte commandCode, byte additionalCode) : base()
        {
            this.CommandCode = commandCode;
            this.AdditionCode = additionalCode;
        }

        public override MotorBaseResponse Decode(OriginalBytes obytes)
        {
            if (obytes.Data.Length != 164) return null;
            byte commandCode = obytes.Data[6];
            byte additionalCode = obytes.Data[7];
            MotorC40Response m40r = new MotorC40Response(commandCode, additionalCode);
            if (m40r.AdditionCode == 0xAA)
            {
                //读回应
                m40r.CommandCode = obytes.Data[6];
                m40r.AdditionCode = obytes.Data[7];
                Array.Copy(obytes.Data, 8, m40r.CodeArea.DataLength, 0, 2);
                m40r.CodeArea.Data = new byte[m40r.CodeArea.Length];
                Array.Copy(obytes.Data, 10, m40r.CodeArea.Data, 0, m40r.CodeArea.Length);
                Array.Copy(obytes.Data, 10 + m40r.CodeArea.Length, m40r.CodeArea.CRC16Code, 0, 2);

                m40r.SystemFlowStatus = m40r.CodeArea.Data[0];
                m40r.Motor1Switch = m40r.CodeArea.Data[1];
                m40r.Motor1Status = m40r.CodeArea.Data[2];
                m40r.Motor1completeSteps = ByteHelper.BytesToInt2(m40r.CodeArea.Data, 3);
                m40r.Motor1Steps = ByteHelper.BytesToInt2(m40r.CodeArea.Data, 7);

                m40r.Motor2Switch = m40r.CodeArea.Data[11];
                m40r.Motor2Status = m40r.CodeArea.Data[12];
                m40r.Motor2completeSteps = ByteHelper.BytesToInt2(m40r.CodeArea.Data, 13);
                m40r.Motor2Steps = ByteHelper.BytesToInt2(m40r.CodeArea.Data, 17);

                m40r.Voltage1 = ByteHelper.BytesToInt2(m40r.CodeArea.Data, 21);
                m40r.Voltage2 = ByteHelper.BytesToInt2(m40r.CodeArea.Data, 25);
                m40r.Temperature = ByteHelper.BytesToInt2(m40r.CodeArea.Data, 29);
                m40r.M1V1 = ByteHelper.BytesToInt2(m40r.CodeArea.Data, 33);
                m40r.M1V2 = ByteHelper.BytesToInt2(m40r.CodeArea.Data, 37);
                m40r.M2V1 = ByteHelper.BytesToInt2(m40r.CodeArea.Data, 41);
                m40r.M2V2 = ByteHelper.BytesToInt2(m40r.CodeArea.Data, 45);

                Array.Copy(m40r.CodeArea.Data, 100, m40r.ErrorAlarmCodes, 0, 50);
            }

            m40r.BasePackage = new CIIBasePackage(m40r.CodeArea, false);
            return m40r;
        }
    }
}
