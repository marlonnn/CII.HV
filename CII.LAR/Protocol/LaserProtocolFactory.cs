using CII.LAR.Commond;
using CII.LAR.MsgQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Protocol
{
    public class LaserProtocolFactory
    {
        private TxQueue txQueue;
        public TxQueue TxQueue
        {
            get { return this.txQueue; }
            private set { this.txQueue = value; }
        }

        private TxMsgQueue txMsgQueue;
        public TxMsgQueue TxMsgQueue
        {
            get { return this.txMsgQueue; }
            private set { this.txMsgQueue = value; }
        }

        private RxQueue rxQueue;
        public RxQueue RxQueue
        {
            get { return this.rxQueue; }
            private set { this.rxQueue = value; }
        }

        private RxMsgQueue rxMsgQueue;
        public RxMsgQueue RxMsgQueue
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

        private Dictionary<byte, BaseResponse> decoders;
        public Dictionary<byte, BaseResponse> Decoders
        {
            get { return this.decoders; }
            private set { this.decoders = value; }
        }
        public LaserProtocolFactory ()
        {
            TxQueue = new TxQueue();
            RxQueue = new RxQueue();
            RxMsgQueue = new RxMsgQueue();
            TxMsgQueue = new TxMsgQueue();
            LaserProtocol = new LaserProtocol();
        }

        private void InitializeDecoders()
        {
            Decoders = new Dictionary<byte, BaseResponse>();
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

        public void DecodeInternal()
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
                        BasePackage bp = new BasePackage();
                        bp.Type = data[0];
                        bp.AppData = new byte[data.Length - 1];
                        Array.Copy(data, 0, bp.AppData, 0, data.Length - 1);
                        if (Decoders.ContainsKey(bp.Type))
                        {
                            List<BaseResponse> responseList = Decoders[bp.Type].Decode(bp, obytes);
                            if (responseList != null && responseList.Count > 0)
                            {
                                RxMsgQueue.Push(responseList);
                            }
                        }
                        else
                        {
                            LogHelper.GetLogger<LaserProtocolFactory>().Error(string.Format("没有解码器可以解码：{0}",
                                    ByteHelper.Byte2ReadalbeXstring(obytes.Data)));
                        }
                    }
                }
            }
        }

        public void EncodeInternal()
        {
            List<BaseRequest> list = txMsgQueue.PopAll();
            if (list != null && list.Count > 0)
            {
                foreach (var br in list)
                {
                    BasePackage bp = br.Encode();
                    byte[] data = bp.AppData;
                    OriginalBytes ob = new OriginalBytes();
                    ob.Data = laserProtocol.EnPackage(data);
                    txQueue.Push(ob);
                }
            }
        }
    }
}
