using CII.LAR.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Commond
{
    public class MotorC64Request : MotorBaseRequest
    {
        /// <summary>
        /// 电机1配置
        /// </summary>
        private byte motor1Config;
        public byte Motor1Config
        {
            get { return this.motor1Config; }
            set { this.motor1Config = value; }
        }

        /// <summary>
        /// 电机2配置
        /// </summary>
        private byte motor2Config;
        public byte Motor2Config
        {
            get { return this.motor2Config; }
            set { this.motor2Config = value; }
        }

        public MotorC64Request() : base() { }

        public MotorC64Request(byte commandCode, byte additionalCode) : base()
        {
            this.CodeArea.CommandCode = commandCode;
            this.CodeArea.AdditionCode = additionalCode;
        }

        public override CIIBasePackage Encode()
        {
            if (CodeArea.AdditionCode == 0x55)
            {
                this.CodeArea.DataLength = new byte[] { 0x00, 0x00 };
            }
            else if (CodeArea.AdditionCode == 0x66)
            {
                this.CodeArea.Data = new byte[2];
                this.CodeArea.Data[0] = Motor1Config;
                this.CodeArea.Data[1] = motor2Config;
                CopyDataLenght();
            }
            return new CIIBasePackage(this.CodeArea);
        }
    }

    public class MotorC64Response : MotorBaseResponse
    {
        /// <summary>
        /// 电机1配置
        /// </summary>
        private byte motor1Config;
        public byte Motor1Config
        {
            get { return this.motor1Config; }
            set { this.motor1Config = value; }
        }

        /// <summary>
        /// 电机2配置
        /// </summary>
        private byte motor2Config;
        public byte Motor2Config
        {
            get { return this.motor2Config; }
            set { this.motor2Config = value; }
        }

        public MotorC64Response() : base() { }

        public MotorC64Response(byte commandCode, byte additionalCode) : base()
        {
            this.CommandCode = commandCode;
            this.AdditionCode = additionalCode;
        }
        public override MotorBaseResponse Decode(OriginalBytes obytes)
        {
            byte commandCode = obytes.Data[6];
            byte additionalCode = obytes.Data[7];
            MotorC64Response m64r = new MotorC64Response(commandCode, additionalCode);
            if (m64r.AdditionCode == 0xAA)
            {
                m64r.CommandCode = obytes.Data[6];
                m64r.AdditionCode = obytes.Data[7];
                Array.Copy(obytes.Data, 8, m64r.CodeArea.DataLength, 0, 2);
                m64r.CodeArea.Data = new byte[m64r.CodeArea.Length];
                Array.Copy(obytes.Data, 10, m64r.CodeArea.Data, 0, m64r.CodeArea.Length);
                Array.Copy(obytes.Data, 10 + m64r.CodeArea.Length, m64r.CodeArea.CRC16Code, 0, 2);
                m64r.Motor1Config = m64r.CodeArea.Data[0];
                m64r.Motor2Config = m64r.CodeArea.Data[1];
            }
            else if (m64r.AdditionCode == 0x99)
            {
                //写回应
                m64r.ResponseCode = m64r.CodeArea.Data[0];
            }
            m64r.BasePackage = new CIIBasePackage(m64r.CodeArea, false);
            return m64r;
        }
    }
}
