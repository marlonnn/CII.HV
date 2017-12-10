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
    public class LaserProtocolFactory
    {
        private LaserTxQueue txQueue;
        public LaserTxQueue TxQueue
        {
            get { return this.txQueue; }
            private set { this.txQueue = value; }
        }

        private LaserTxMsgQueue txMsgQueue;
        public LaserTxMsgQueue TxMsgQueue
        {
            get { return this.txMsgQueue; }
            private set { this.txMsgQueue = value; }
        }

        private LaserRxQueue rxQueue;
        public LaserRxQueue RxQueue
        {
            get { return this.rxQueue; }
            private set { this.rxQueue = value; }
        }

        private LaserRxMsgQueue rxMsgQueue;
        public LaserRxMsgQueue RxMsgQueue
        {
            get { return this.rxMsgQueue; }
            private set { this.rxMsgQueue = value; }
        }

        private LaserProtocol laserProtocol;
        public LaserProtocol LaserProtocol
        {
            get { return this.laserProtocol; }
            private set { this.laserProtocol = value; }
        }

        private Dictionary<byte, LaserBaseResponse> decoders;
        public Dictionary<byte, LaserBaseResponse> Decoders
        {
            get { return this.decoders; }
            private set { this.decoders = value; }
        }

        private LaserBaseResponse decoder;
        public LaserBaseResponse Decoder
        {
            get { return this.decoder; }
            set { this.decoder = value; }
        }

        /// <summary>
        /// 发送消息之前必须先设置解码器
        /// </summary>
        /// <param name="type"></param>
        private void SetDecoder(byte type)
        {
            if (Decoders.ContainsKey(type))
            {
                Decoder = Decoders[type];
            }
        }

        private byte GetMsgType()
        {
            byte type = 0x00;
            if (Decoders.ContainsValue(Decoder))
            {
                type = Decoders.FirstOrDefault(d =>d.Value.Type == Decoder.Type).Key;
            }
            return type;
        }

        public LaserProtocolFactory ()
        {
            InitializeDecoders();
            TxQueue = new LaserTxQueue();
            RxQueue = new LaserRxQueue();
            RxMsgQueue = new LaserRxMsgQueue();
            TxMsgQueue = new LaserTxMsgQueue();
            LaserProtocol = new LaserProtocol();
        }

        public void SendMessage(LaserBaseRequest baseRequest)
        {
            SetDecoder(baseRequest.Type);
            TxMsgQueue.Push(baseRequest);
        }

        private void InitializeDecoders()
        {
            Decoders = new Dictionary<byte, LaserBaseResponse>();
            Decoders[0x00] = new LaserC00Response();
            Decoders[0x01] = new LaserC01Response();
            Decoders[0x03] = new LaserC03Response();
            Decoders[0x04] = new LaserC04Response();
            Decoders[0x05] = new LaserC05Response();
            Decoders[0x06] = new LaserC06Response();
            Decoders[0x07] = new LaserC07Response();
            Decoders[0x08] = new LaserC08Response();
            Decoders[0x09] = new LaserC09Response();
            Decoders[0x0B] = new LaserC0BResponse();
            Decoders[0x0C] = new LaserC0CResponse();
            Decoders[0x71] = new LaserC71Response();
            Decoders[0x72] = new LaserC72Response();
            Decoders[0x73] = new LaserC73Response();
            Decoders[0x74] = new LaserC74Response();
            Decoders[0x75] = new LaserC75Response();
            //initialize default decoder
            Decoder = Decoders[0x00];
        }

        public static LaserProtocolFactory laserProtocolFactory;
        public static LaserProtocolFactory GetInstance()
        {
            if (laserProtocolFactory == null)
            {
                laserProtocolFactory = new LaserProtocolFactory();
            }
            return laserProtocolFactory;
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

        public void StartDecodeThread()
        {
            Decode = true;
            RunDecodeThread = true;
            decodeThread = new Thread(new ThreadStart(DecodeInternal))
            {
                IsBackground = true,
                Priority = ThreadPriority.Normal,
                Name = "DecodeThread"
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
                Name = "DecodeThread"
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
                    if (list != null && list.Count > 0)
                    {
                        foreach (var o in list)
                        {
                            OriginalBytes obytes = o as OriginalBytes;
                            if (o != null)
                            {
                                LaserProtocol lp = laserProtocol.DePackage(obytes.Data);
                                byte[] data = lp.Body;
                                byte markHead = data[0];
                                byte type = GetMsgType();
                                byte[] appData = new byte[data.Length - 2];
                                Array.Copy(data, 1, appData, 0, data.Length - 2);
                                LaserBasePackage bp = new LaserBasePackage(markHead, type, appData);
                                List<LaserBaseResponse> responseList = Decoder.Decode(bp, obytes);
                                if (responseList != null && responseList.Count > 0)
                                {
                                    RxMsgQueue.Push(responseList);
                                }
                                LogHelper.GetLogger<LaserProtocolFactory>().Error(string.Format("接受到的原始数据为： {0}",
                                        ByteHelper.Byte2ReadalbeXstring(obytes.Data)));
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
                    List<LaserBaseRequest> list = txMsgQueue.PopAll();
                    if (list != null && list.Count > 0)
                    {
                        foreach (var br in list)
                        {
                            List<LaserBasePackage> bps = br.Encode();
                            foreach (LaserBasePackage bp in bps)
                            {
                                OriginalBytes ob = new OriginalBytes();
                                ob.Data = laserProtocol.EnPackage(bp);
                                txQueue.Push(ob);
                            }
                        }
                    }
                }
                Thread.Sleep(10);
            }
        }
    }
}
