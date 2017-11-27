//==================================================================================================
// 仪器通用通信协议栈定义
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
using CII.Library.CIINet.Protocols;
using CII.Library.CIINet.Interfaces;
using CII.Library.CIINet.Ports.CIIPorts;
using CII.Library.CIINet.Ports.SyncPorts;
using CII.Library.CIINet.Commands;

namespace CII.Ins.Model.Protocols
{
    /// <summary>
    /// 仪器通用通信协议栈定义
    /// 创建人：刘海生
    /// 创建时间: 2015.11.13
    /// </summary>
    public class InsCommunicationProtol : Protocol
    {
        protected override Parser ConstructParser()
        {
            return new InsParser();
        }

        public override string FriendlyName
        {
            get { return "InsCommunicationProtol"; }
        }
    }

    /// <summary>
    /// 仪器通用通信协议解析器定义
    /// 创建人：刘海生
    /// 创建时间: 2015.11.13
    /// </summary
    public class InsParser : Parser
    {
        /// <summary>
        /// 构造协议栈
        /// InsertPort,SyncPort,CIIRouterPort,CommandPort
        /// </summary>
        /// <returns>协议栈</returns>
        protected override IPort[] ConstructPorts()
        {
            //将收发的数据进行分层处理，这里定义的4层，总称为协议栈
            //分层port，也叫分层协议
            IPort[] ports = new IPort[4];
            //第1层，插入port（插入协议）：封装或拆装5D5B……5D5D
            ports[0] = new InsertPort();
            //第2层，同步port（同步协议）：异步协议中等待达到同步效果
            ports[1] = new SyncPort();
            //第3层，路由port（路由协议）：源地址/目标地址的多级转发处理（如有）
            ports[2] = new CIIRouterPort();
            //第4层，协议命令port（协议命令协议）（处理CommandManager.xml中定义的协议）；
            ports[3] = new CommandPort();
            return ports;
        }
    }
}