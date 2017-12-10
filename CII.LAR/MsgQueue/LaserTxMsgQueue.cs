using CII.LAR.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.MsgQueue
{
    public class LaserTxMsgQueue : ConcurrentQueue<LaserBaseRequest>
    {
    }
}
