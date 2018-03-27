using CII.LAR.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Commond
{
    /// <summary>
    /// 系统参数
    /// </summary>
    public class MotorC50Request : MotorBaseRequest
    {
        /// <summary>
        /// 读取类容选择
        /// 读：0x55
        /// 写：0x66
        /// </summary>
        private byte selection;
        public byte Selection
        {
            get { return this.selection; }
            set { this.selection = value; }
        }

        /// <summary>
        /// 电机驱动频率
        /// </summary>
        private int frequency;
        public int Frequency
        {
            get { return this.frequency; }
            set { this.frequency = value; }
        }

        /// <summary>
        /// 最大脉冲数
        /// </summary>
        private int maxPulses;
        public int MaxPulses
        {
            get { return this.maxPulses; }
            set { this.maxPulses = value; }
        }

        public MotorC50Request() : base() { }

        public MotorC50Request(byte commandCode, byte additionalCode) : base()
        {
            this.CodeArea.CommandCode = commandCode;
            this.CodeArea.AdditionCode = additionalCode;
        }

        public override CIIBasePackage Encode()
        {
            if (CodeArea.AdditionCode == 0x55)
            {
                this.CodeArea.DataLength = new byte[] { 0x00, 0x01 };
                this.CodeArea.Data = new byte[] { Selection };
            }
            else if (CodeArea.AdditionCode == 0x66)
            {
                switch (Selection)
                {
                    case 0xA0:
                        this.CodeArea.Data = new byte[9];
                        this.CodeArea.Data[0] = Selection;
                        Array.Copy(ByteHelper.IntToBytes2(Frequency), 0, this.CodeArea.Data, 1, 4);
                        Array.Copy(ByteHelper.IntToBytes2(MaxPulses), 0, this.CodeArea.Data, 5, 4);
                        break;
                    case 0xA1:
                        this.CodeArea.Data = new byte[5];
                        this.CodeArea.Data[0] = Selection;
                        Array.Copy(ByteHelper.IntToBytes2(Frequency), 0, this.CodeArea.Data, 1, 4);
                        break;
                    case 0xA2:
                        this.CodeArea.Data = new byte[5];
                        this.CodeArea.Data[0] = Selection;
                        Array.Copy(ByteHelper.IntToBytes2(MaxPulses), 0, this.CodeArea.Data, 1, 4);
                        break;
                }
                CopyDataLenght();
            }
            return new CIIBasePackage(this.CodeArea);
        }
    }

    public class MotorC50Response : MotorBaseResponse
    {
        /// <summary>
        /// 读取类容选择
        /// 读：0x55
        /// 写：0x66
        /// </summary>
        private byte selection;
        public byte Selection
        {
            get { return this.selection; }
            set { this.selection = value; }
        }

        /// <summary>
        /// 电机驱动频率
        /// </summary>
        private int frequency;
        public int Frequency
        {
            get { return this.frequency; }
            private set { this.frequency = value; }
        }

        /// <summary>
        /// 最大脉冲数
        /// </summary>
        private int maxPulses;
        public int MaxPulses
        {
            get { return this.maxPulses; }
            private set { this.maxPulses = value; }
        }

        public MotorC50Response() : base() { }

        public MotorC50Response(byte commandCode, byte additionalCode) : base()
        {
            this.CommandCode = commandCode;
            this.AdditionCode = additionalCode;
        }

        public override MotorBaseResponse Decode(OriginalBytes obytes)
        {
            byte commandCode = obytes.Data[6];
            byte additionalCode = obytes.Data[7];
            MotorC50Response m50r = new MotorC50Response(commandCode, additionalCode);
            if (m50r.AdditionCode == 0xAA)
            {
                //读回应
                m50r.CommandCode = obytes.Data[6];
                m50r.AdditionCode = obytes.Data[7];
                Array.Copy(obytes.Data, 8, m50r.CodeArea.DataLength, 0, 2);
                m50r.CodeArea.Data = new byte[m50r.CodeArea.Length];
                Array.Copy(obytes.Data, 10, m50r.CodeArea.Data, 0, m50r.CodeArea.Length);
                Array.Copy(obytes.Data, 10 + m50r.CodeArea.Length, m50r.CodeArea.CRC16Code, 0, 2);
                m50r.Selection = m50r.CodeArea.Data[0];
                switch (Selection)
                {
                    case 0xA0:
                        m50r.Frequency = ByteHelper.BytesToInt2(m50r.CodeArea.Data, 1);
                        m50r.MaxPulses = ByteHelper.BytesToInt2(m50r.CodeArea.Data, 5);
                        break;
                    case 0xA1:
                        m50r.Frequency = ByteHelper.BytesToInt2(m50r.CodeArea.Data, 1);
                        break;
                    case 0xA2:
                        m50r.MaxPulses = ByteHelper.BytesToInt2(m50r.CodeArea.Data, 1);
                        break;
                }
            }
            else if (m50r.AdditionCode == 0x99)
            {
                //写回应
                m50r.ResponseCode = m50r.CodeArea.Data[0];
            }
            m50r.BasePackage = new CIIBasePackage(m50r.CodeArea, false);
            return m50r;
        }

    }
}
