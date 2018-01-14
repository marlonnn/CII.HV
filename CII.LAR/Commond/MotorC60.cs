using CII.LAR.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Commond
{
    /// <summary>
    /// 60命令 外设控制选项配置
    /// </summary>
    public class MotorC60Request : MotorBaseRequest
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
        /// 1字节(INT8U)控制模式:
        /// 0x00：不启用(检测/报警/不控制)
        /// 0x01：启用(检测/报警/控制)
        /// </summary>
        private byte controlMode61;
        public byte ControlMode61
        {
            get { return this.controlMode61; }
            set { this.controlMode61 = value; }
        }

        /// <summary>
        /// 控制方向
        /// </summary>
        private byte direction61;
        public byte Direction61
        {
            get { return this.direction61; }
            set { this.direction61 = value; }
        }

        /// <summary>
        /// 总步数
        /// </summary>
        private int totalSteps61;
        public int TotalSteps61
        {
            get { return this.totalSteps61; }
            set { this.totalSteps61 = value; }
        }

        /// <summary>
        /// 1字节(INT8U)控制模式:
        /// 0x00：不启用(检测/报警/不控制)
        /// 0x01：启用(检测/报警/控制)
        /// </summary>
        private byte controlMode62;
        public byte ControlMode62
        {
            get { return this.controlMode62; }
            set { this.controlMode62 = value; }
        }

        /// <summary>
        /// 控制方向
        /// </summary>
        private byte direction62;
        public byte Direction62
        {
            get { return this.direction62; }
            set { this.direction62 = value; }
        }

        /// <summary>
        /// 总步数
        /// </summary>
        private int totalSteps62;
        public int TotalSteps62
        {
            get { return this.totalSteps62; }
            set { this.totalSteps62 = value; }
        }

        /// <summary>
        /// 1字节(INT8U)控制模式:
        /// 0x00：不启用(检测/报警/不控制)
        /// 0x01：启用(检测/报警/控制)
        /// </summary>
        private byte controlModeA1;
        public byte ControlModeA1
        {
            get { return this.controlModeA1; }
            set { this.controlModeA1 = value; }
        }

        /// <summary>
        /// 控制方向
        /// </summary>
        private byte directionA1;
        public byte DirectionA1
        {
            get { return this.directionA1; }
            set { this.directionA1 = value; }
        }

        /// <summary>
        /// 总步数
        /// </summary>
        private int totalStepsA1;
        public int TotalStepsA1
        {
            get { return this.totalStepsA1; }
            set { this.totalStepsA1 = value; }
        }

        /// <summary>
        /// 1字节(INT8U)控制模式:
        /// 0x00：不启用(检测/报警/不控制)
        /// 0x01：启用(检测/报警/控制)
        /// </summary>
        private byte controlModeA2;
        public byte ControlModeA2
        {
            get { return this.controlModeA2; }
            set { this.controlModeA2 = value; }
        }

        /// <summary>
        /// 控制方向
        /// </summary>
        private byte directionA2;
        public byte DirectionA2
        {
            get { return this.directionA2; }
            set { this.directionA2 = value; }
        }

        /// <summary>
        /// 总步数
        /// </summary>
        private int totalStepsA2;
        public int TotalStepsA2
        {
            get { return this.totalStepsA2; }
            set { this.totalStepsA2 = value; }
        }

        public MotorC60Request() : base() { }

        public MotorC60Request(byte commandCode, byte additionalCode) : base()
        {
            this.CodeArea.CommandCode = commandCode;
            this.CodeArea.AdditionCode = additionalCode;
        }

        public override CIIBasePackage Encode()
        {
            if (CodeArea.AdditionCode == 0x55)
            {
                this.CodeArea.DataLength = new byte[] { 0x00, 0x01 };
                this.CodeArea.Data = new byte[] { ControlSelection };
            }
            else if (CodeArea.AdditionCode == 0x66)
            {
                switch (ControlSelection)
                {
                    case 0x00:
                        this.CodeArea.Data = new byte[25];
                        this.CodeArea.Data[0] = ControlSelection;
                        this.CodeArea.Data[1] = ControlMode61;
                        this.CodeArea.Data[2] = Direction61;
                        Array.Copy(ByteHelper.IntToBytes2(TotalSteps61), 0, this.CodeArea.Data, 3, 4);
                        this.CodeArea.Data[7] = ControlMode62;
                        this.CodeArea.Data[8] = Direction62;
                        Array.Copy(ByteHelper.IntToBytes2(TotalSteps62), 0, this.CodeArea.Data, 9, 4);

                        this.CodeArea.Data[13] = ControlModeA1;
                        this.CodeArea.Data[14] = DirectionA1;
                        Array.Copy(ByteHelper.IntToBytes2(TotalStepsA1), 0, this.CodeArea.Data, 15, 4);
                        this.CodeArea.Data[19] = ControlModeA2;
                        this.CodeArea.Data[20] = DirectionA2;
                        Array.Copy(ByteHelper.IntToBytes2(TotalStepsA2), 0, this.CodeArea.Data, 21, 4);

                        break;
                    case 0x60:
                        this.CodeArea.Data = new byte[13];
                        this.CodeArea.Data[0] = ControlSelection;
                        this.CodeArea.Data[1] = ControlMode61;
                        this.CodeArea.Data[2] = Direction61;
                        Array.Copy(ByteHelper.IntToBytes2(TotalSteps61), 0, this.CodeArea.Data, 3, 4);
                        this.CodeArea.Data[7] = ControlMode62;
                        this.CodeArea.Data[8] = Direction62;
                        Array.Copy(ByteHelper.IntToBytes2(TotalSteps62), 0, this.CodeArea.Data, 9, 4);
                        break;
                    case 0x61:
                        this.CodeArea.Data = new byte[7];
                        this.CodeArea.Data[0] = ControlSelection;
                        this.CodeArea.Data[1] = ControlMode61;
                        this.CodeArea.Data[2] = Direction61;
                        Array.Copy(ByteHelper.IntToBytes2(TotalSteps61), 0, this.CodeArea.Data, 3, 4);
                        break;
                    case 0x62:

                        this.CodeArea.Data = new byte[7];
                        this.CodeArea.Data[0] = ControlSelection;
                        this.CodeArea.Data[1] = ControlMode62;
                        this.CodeArea.Data[2] = Direction62;
                        Array.Copy(ByteHelper.IntToBytes2(TotalSteps62), 0, this.CodeArea.Data, 3, 4);
                        break;
                    case 0xA0:
                        this.CodeArea.Data = new byte[13];
                        this.CodeArea.Data[0] = ControlSelection;
                        this.CodeArea.Data[1] = ControlModeA1;
                        this.CodeArea.Data[2] = DirectionA1;
                        Array.Copy(ByteHelper.IntToBytes2(TotalStepsA1), 0, this.CodeArea.Data, 3, 4);
                        this.CodeArea.Data[7] = ControlModeA2;
                        this.CodeArea.Data[8] = DirectionA2;
                        Array.Copy(ByteHelper.IntToBytes2(TotalStepsA2), 0, this.CodeArea.Data, 9, 4);
                        break;
                    case 0xA1:
                        this.CodeArea.Data = new byte[7];
                        this.CodeArea.Data[0] = ControlSelection;
                        this.CodeArea.Data[1] = ControlModeA1;
                        this.CodeArea.Data[2] = DirectionA1;
                        Array.Copy(ByteHelper.IntToBytes2(TotalStepsA1), 0, this.CodeArea.Data, 3, 4);
                        break;
                    case 0xA2:
                        this.CodeArea.Data = new byte[7];
                        this.CodeArea.Data[0] = ControlSelection;
                        this.CodeArea.Data[1] = ControlModeA2;
                        this.CodeArea.Data[2] = DirectionA2;
                        Array.Copy(ByteHelper.IntToBytes2(TotalStepsA2), 0, this.CodeArea.Data, 3, 4);
                        break;

                }

                CopyDataLenght();
            }
            return new CIIBasePackage(this.CodeArea);
        }
    }

    /// <summary>
    /// 60命令 外设控制选项配置 回应
    /// </summary>
    public class MotorC60Response : MotorBaseResponse
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
        /// 1字节(INT8U)控制模式:
        /// 0x00：不启用(检测/报警/不控制)
        /// 0x01：启用(检测/报警/控制)
        /// </summary>
        private byte controlMode61;
        public byte ControlMode61
        {
            get { return this.controlMode61; }
            set { this.controlMode61 = value; }
        }

        /// <summary>
        /// 控制方向
        /// </summary>
        private byte direction61;
        public byte Direction61
        {
            get { return this.direction61; }
            set { this.direction61 = value; }
        }

        /// <summary>
        /// 总步数
        /// </summary>
        private int totalSteps61;
        public int TotalSteps61
        {
            get { return this.totalSteps61; }
            set { this.totalSteps61 = value; }
        }

        /// <summary>
        /// 1字节(INT8U)控制模式:
        /// 0x00：不启用(检测/报警/不控制)
        /// 0x01：启用(检测/报警/控制)
        /// </summary>
        private byte controlMode62;
        public byte ControlMode62
        {
            get { return this.controlMode62; }
            set { this.controlMode62 = value; }
        }

        /// <summary>
        /// 控制方向
        /// </summary>
        private byte direction62;
        public byte Direction62
        {
            get { return this.direction62; }
            set { this.direction62 = value; }
        }

        /// <summary>
        /// 总步数
        /// </summary>
        private int totalSteps62;
        public int TotalSteps62
        {
            get { return this.totalSteps62; }
            set { this.totalSteps62 = value; }
        }

        /// <summary>
        /// 1字节(INT8U)控制模式:
        /// 0x00：不启用(检测/报警/不控制)
        /// 0x01：启用(检测/报警/控制)
        /// </summary>
        private byte controlModeA1;
        public byte ControlModeA1
        {
            get { return this.controlModeA1; }
            set { this.controlModeA1 = value; }
        }

        /// <summary>
        /// 控制方向
        /// </summary>
        private byte directionA1;
        public byte DirectionA1
        {
            get { return this.directionA1; }
            set { this.directionA1 = value; }
        }

        /// <summary>
        /// 总步数
        /// </summary>
        private int totalStepsA1;
        public int TotalStepsA1
        {
            get { return this.totalStepsA1; }
            set { this.totalStepsA1 = value; }
        }

        /// <summary>
        /// 1字节(INT8U)控制模式:
        /// 0x00：不启用(检测/报警/不控制)
        /// 0x01：启用(检测/报警/控制)
        /// </summary>
        private byte controlModeA2;
        public byte ControlModeA2
        {
            get { return this.controlModeA2; }
            set { this.controlModeA2 = value; }
        }

        /// <summary>
        /// 控制方向
        /// </summary>
        private byte directionA2;
        public byte DirectionA2
        {
            get { return this.directionA2; }
            set { this.directionA2 = value; }
        }

        /// <summary>
        /// 总步数
        /// </summary>
        private int totalStepsA2;
        public int TotalStepsA2
        {
            get { return this.totalStepsA2; }
            set { this.totalStepsA2 = value; }
        }

        public MotorC60Response() : base() { }

        public MotorC60Response(byte commandCode, byte additionalCode) : base()
        {
            this.CommandCode = commandCode;
            this.AdditionCode = additionalCode;
        }

        public override MotorBaseResponse Decode(OriginalBytes obytes)
        {
            byte commandCode = obytes.Data[6];
            byte additionalCode = obytes.Data[7];
            MotorC60Response m60r = new MotorC60Response(commandCode, additionalCode);
            if (m60r.AdditionCode == 0xAA)
            {
                //读回应
                m60r.CommandCode = obytes.Data[6];
                m60r.AdditionCode = obytes.Data[7];
                Array.Copy(obytes.Data, 8, m60r.CodeArea.DataLength, 0, 2);
                m60r.CodeArea.Data = new byte[m60r.CodeArea.Length];
                Array.Copy(obytes.Data, 10, m60r.CodeArea.Data, 0, m60r.CodeArea.Length);
                Array.Copy(obytes.Data, 10 + m60r.CodeArea.Length, m60r.CodeArea.CRC16Code, 0, 2);
                m60r.ControlSelection = m60r.CodeArea.Data[0];
                switch (m60r.controlSelection)
                {
                    case 0x00:
                        m60r.ControlMode61 = m60r.CodeArea.Data[1];
                        m60r.Direction61 = m60r.CodeArea.Data[2];
                        m60r.TotalSteps61 = ByteHelper.BytesToInt2(m60r.CodeArea.Data, 3);

                        m60r.ControlMode62 = m60r.CodeArea.Data[7];
                        m60r.Direction62 = m60r.CodeArea.Data[8];
                        m60r.TotalSteps62 = ByteHelper.BytesToInt2(m60r.CodeArea.Data, 9);

                        m60r.ControlModeA1 = m60r.CodeArea.Data[13];
                        m60r.DirectionA1 = m60r.CodeArea.Data[14];
                        m60r.TotalStepsA1 = ByteHelper.BytesToInt2(m60r.CodeArea.Data, 15);

                        m60r.ControlModeA2 = m60r.CodeArea.Data[19];
                        m60r.DirectionA2 = m60r.CodeArea.Data[20];
                        m60r.TotalStepsA2 = ByteHelper.BytesToInt2(m60r.CodeArea.Data, 21);
                        break;
                    case 0x60:
                        m60r.ControlMode61 = m60r.CodeArea.Data[1];
                        m60r.Direction61 = m60r.CodeArea.Data[2];
                        m60r.TotalSteps61 = ByteHelper.BytesToInt2(m60r.CodeArea.Data, 3);

                        m60r.ControlMode62 = m60r.CodeArea.Data[7];
                        m60r.Direction62 = m60r.CodeArea.Data[8];
                        m60r.TotalSteps62 = ByteHelper.BytesToInt2(m60r.CodeArea.Data, 9);
                        break;
                    case 0x61:
                        m60r.ControlMode61 = m60r.CodeArea.Data[1];
                        m60r.Direction61 = m60r.CodeArea.Data[2];
                        m60r.TotalSteps61 = ByteHelper.BytesToInt2(m60r.CodeArea.Data, 3);
                        break;
                    case 0x62:
                        m60r.ControlMode62 = m60r.CodeArea.Data[7];
                        m60r.Direction62 = m60r.CodeArea.Data[8];
                        m60r.TotalSteps62 = ByteHelper.BytesToInt2(m60r.CodeArea.Data, 9);
                        break;
                    case 0xA0:
                        m60r.ControlModeA1 = m60r.CodeArea.Data[13];
                        m60r.DirectionA1 = m60r.CodeArea.Data[14];
                        m60r.TotalStepsA1 = ByteHelper.BytesToInt2(m60r.CodeArea.Data, 15);

                        m60r.ControlModeA2 = m60r.CodeArea.Data[19];
                        m60r.DirectionA2 = m60r.CodeArea.Data[20];
                        m60r.TotalStepsA2 = ByteHelper.BytesToInt2(m60r.CodeArea.Data, 21);
                        break;
                    case 0xA1:
                        m60r.ControlModeA1 = m60r.CodeArea.Data[13];
                        m60r.DirectionA1 = m60r.CodeArea.Data[14];
                        m60r.TotalStepsA1 = ByteHelper.BytesToInt2(m60r.CodeArea.Data, 15);
                        break;
                    case 0xA2:
                        m60r.ControlModeA2 = m60r.CodeArea.Data[19];
                        m60r.DirectionA2 = m60r.CodeArea.Data[20];
                        m60r.TotalStepsA2 = ByteHelper.BytesToInt2(m60r.CodeArea.Data, 21);
                        break;

                }
            }
            else if (m60r.AdditionCode == 0x99)
            {
                //写回应
                if (m60r.CodeArea.Data != null)
                {
                    m60r.ResponseCode = m60r.CodeArea.Data[0];
                }
            }
            m60r.BasePackage = new CIIBasePackage(m60r.CodeArea, false);
            return m60r;
        }
    }
}
