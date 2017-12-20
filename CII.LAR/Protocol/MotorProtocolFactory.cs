using CII.LAR.Commond;
using CII.LAR.MsgQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CII.LAR.Protocol
{
    public class MotorProtocolFactory
    {
        private MotorTxQueue txQueue;
        public MotorTxQueue TxQueue
        {
            get { return this.txQueue; }
            private set { this.txQueue = value; }
        }

        private MotorTxMsgQueue txMsgQueue;
        public MotorTxMsgQueue TxMsgQueue
        {
            get { return this.txMsgQueue; }
            private set { this.txMsgQueue = value; }
        }

        private MotorRxQueue rxQueue;
        public MotorRxQueue RxQueue
        {
            get { return this.rxQueue; }
            private set { this.rxQueue = value; }
        }

        private MotorRxMsgQueue rxMsgQueue;
        public MotorRxMsgQueue RxMsgQueue
        {
            get { return this.rxMsgQueue; }
            private set { this.rxMsgQueue = value; }
        }

        private MotorProtocol motorProtocol;
        public MotorProtocol MotorProtocol
        {
            get { return this.motorProtocol; }
            private set { this.motorProtocol = value; }
        }

        public static MotorProtocolFactory motorProtocolFactory;
        public static MotorProtocolFactory GetInstance()
        {
            if (motorProtocolFactory == null)
            {
                motorProtocolFactory = new MotorProtocolFactory();
            }
            return motorProtocolFactory;
        }

        private Thread decodeThread;

        private Thread encodeThread;

        private bool runDecodeThread = true;
        public bool RunDecodeThread
        {
            get { return this.runDecodeThread; }
            private set { this.runDecodeThread = value; }
        }

        private bool decode = false;
        public bool Decode
        {
            get { return this.decode; }
            private set { this.decode = value; }
        }

        private bool runEncodeThread = true;
        public bool RunEncodeThread
        {
            get { return this.runEncodeThread; }
            private set { this.runEncodeThread = value; }
        }

        private bool encode = false;
        public bool Encode
        {
            get { return this.encode; }
            private set { this.encode = value; }
        }

        private Dictionary<byte, MotorBaseResponse> decoders;
        public Dictionary<byte, MotorBaseResponse> Decoders
        {
            get { return this.decoders; }
            private set { this.decoders = value; }
        }

        public void SendMessage(MotorBaseRequest baseRequest)
        {
            TxMsgQueue.Push(baseRequest);
        }

        public MotorProtocolFactory()
        {
            InitializeDecoders();
            TxQueue = new MotorTxQueue();
            RxQueue = new MotorRxQueue();
            RxMsgQueue = new MotorRxMsgQueue();
            TxMsgQueue = new MotorTxMsgQueue();
            MotorProtocol = new MotorProtocol();
        }

        private void InitializeDecoders()
        {
            Decoders = new Dictionary<byte, MotorBaseResponse>();
            Decoders[0x40] = new MotorC40Response();
            Decoders[0x50] = new MotorC50Response();
            Decoders[0x60] = new MotorC60Response();
            Decoders[0x64] = new MotorC64Response();
        }

        public void StartDecodeThread()
        {
            Decode = true;
            RunDecodeThread = true;
            decodeThread = new Thread(new ThreadStart(DecodeInternal))
            {
                IsBackground = true,
                Priority = ThreadPriority.Normal,
                Name = "Motor Decode Thread"
            };
            decodeThread.Start();
        }

        public void StopDecodeThread()
        {
            Decode = false;
        }

        public void RestartDecodeThread()
        {
            if (decodeThread != null)
            {
                Decode = true;
                RunDecodeThread = true;
            }
        }
        public void DestroyDecodeThread()
        {
            Decode = false;
            RunDecodeThread = false;
            if (decodeThread != null)
            {
                decodeThread.Abort();
            }
        }

        public void StartEncodeThread()
        {
            encodeThread = new Thread(new ThreadStart(EncodeInternal))
            {
                IsBackground = true,
                Priority = ThreadPriority.Normal,
                Name = "Motor Encode Thread"
            };
            Encode = true;
            encodeThread.Start();
        }

        public void StopEncodeThread()
        {
            encode = false;
        }

        public void RestartEncodeThread()
        {
            if (encodeThread != null)
            {
                Encode = true;
                RunEncodeThread = true;
            }
        }
        public void DestroyEncodeThread()
        {
            Encode = true;
            RunEncodeThread = true;
            if (encodeThread != null)
            {
                encodeThread.Abort();
            }
        }

        public void DecodeInternal()
        {
            while (RunDecodeThread)
            {
                if (Decode)
                {
                    List<Original> list = RxQueue.PopAll();
                    {
                        if (list != null && list.Count > 0)
                        {
                            foreach (var o in list)
                            {
                                OriginalBytes obytes = o as OriginalBytes;
                                if (o != null)
                                {
                                    if (obytes.Data[0] == 0x5D && obytes.Data[0] == 0x5B)
                                    {
                                        MotorProtocol mp = motorProtocol.DePackage(obytes.Data);
                                        byte[] data = mp.CodeRegion;
                                        byte commandCode = data[0];
                                        byte additionCode = data[1];
                                        MotorBaseResponse mr = Decoders[commandCode].Decode(obytes);
                                        if (mr != null)
                                        {
                                            RxMsgQueue.Push(mr);
                                            LogHelper.GetLogger<LaserProtocolFactory>().Error(string.Format("接受到的原始数据为： {0}",
                                                ByteHelper.Byte2ReadalbeXstring(obytes.Data)));
                                        }
                                    }
                                    else
                                    {
                                        LogHelper.GetLogger<LaserProtocolFactory>().Error(string.Format("接受到的原始数据非法： {0}",
                                            ByteHelper.Byte2ReadalbeXstring(obytes.Data)));
                                    }
                                }
                            }
                        }
                    }
                }
                Thread.Sleep(10);
            }
        }

        public void EncodeInternal()
        {
            while (RunEncodeThread)
            {
                if (Encode)
                {
                    List<MotorBaseRequest> list = txMsgQueue.PopAll();
                    if (list != null && list.Count > 0)
                    {
                        foreach (var mr in list)
                        {
                            CIIBasePackage bp = mr.Encode();
                            OriginalBytes ob = new OriginalBytes();
                            ob.Data = motorProtocol.EnPackage(bp);
                            txQueue.Push(ob);
                        }
                    }
                 }
                Thread.Sleep(10);
            }
        }
    }
}
