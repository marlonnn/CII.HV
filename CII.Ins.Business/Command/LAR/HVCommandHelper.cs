//==================================================================================================
// Cell100通信操作类定义，本类内函数定义以流水式顺序罗列
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
using CII.Library.CIINet.Commands;
using CII.Ins.Model.Command.LAR;
using CII.Library.CIINet.Manager;
using CII.Ins.Model.Data.LAR;
using CII.Ins.Model.GlobalConfig;
using CII.Ins.Business.Command.Interface;
using CII.Library.CIINet.Converter;
using CII.Ins.Business.Instrument;

namespace CII.Ins.Business.Command.LAR
{
    /// <summary>
    /// Cell100通信操作类定义，本类内函数定义以流水式顺序罗列
    /// 创建人：刘海生
    /// 创建时间: 2015.11.13
    /// </summary>
    public class HVCommandHelper : ICommandHelper
    {
        #region -- CommandHelper配置属性及方法 --
        /// <summary>
        /// 操作成功
        /// </summary>
        //private static readonly byte R_OK = 0x88;
        /// <summary>
        /// 操作失败
        /// </summary>
        private static readonly byte R_FAILE = 0x99;
        /// <summary>
        /// 参数非法
        /// </summary>
        private static readonly byte R_BADPARAM = 0xaa;

        /// <summary>
        /// 仪器名称，在CommandManager.xml协议配置文件中确定的
        /// </summary>
        private static readonly string InsName = "LAR";
        #endregion

        #region -- 检查返回值 --
        /// <summary>
        /// 检查PortManager的Send返回值RecvCommand
        /// </summary>
        /// <param name="recvCmd"></param>
        private static void CheckRecvCommand(RecvCommand recvCmd)
        {
            if (recvCmd == null)
            {
                //PC或MCU问题；
                //通信操作返回值为空：同步通讯不会为空，检查通信日志的MCU读/写回应记录来确定是PC上位机问题还是MCU下位机问题；
                throw new Exception("ErrorCode(0xFF)");
            }
            if (((CII.Library.CIINet.Commands.Command)(recvCmd)).GetParamData() == null || ((CII.Library.CIINet.Commands.Command)(recvCmd)).GetParamData().Length <= 0)
            {
                //MCU问题；
                //MCU读/写回应未按CII-NET协议约定格式返回操作结果（0x88成功或0x99失败等），请反馈给MCU同事解决；
                throw new Exception("ErrorCode(0xFE)");
            }
            if ((((CII.Library.CIINet.Commands.Command)(recvCmd)).GetParamData())[0] == R_FAILE)
            {
                //MCU问题；
                //MCU返回读/写失败，请反馈给MCU同事解决；
                throw new Exception("ErrorCode(0x99)");
            }
            if ((((CII.Library.CIINet.Commands.Command)(recvCmd)).GetParamData())[0] == R_BADPARAM)
            {
                //PC问题；
                //因上位机输入参数非法或越限而导致MCU拒绝该操作，请PC上位机软件检查输入数据的合法性；
                throw new Exception("ErrorCode(0xAA)");
            }
        }
        #endregion

        /// <summary>
        /// 0x41设置高压频率、电压
        /// </summary>
        public void SetHvFrequency(uint frequency, uint voltage)
        {
            //SendCommand sendCmd = new SendCommand(CommandId.SystemMonitor, CommandExtendId.Write);
            //RecvCommand recvCmd = (RecvCommand)PortManager.GetInstance().Send(InsName, sendCmd);
            //CheckRecvCommand(recvCmd);

        }

        /// <summary>
        /// 0x40读取监控数据
        /// </summary>
        /// <returns></returns>
        public MonitorData GetMonitorData()
        {
            MonitorData data = new MonitorData();

            SendCommand sendCmd40 = new SendCommand(CommandId.SystemMonitor, CommandExtendId.Read);
            RecvCommand recvCmd40 = (RecvCommand)PortManager.GetInstance().Send("LAR", sendCmd40);
            CheckRecvCommand(recvCmd40);
            data.Motor1Switch = recvCmd40.GetByte(ParamId.SystemMonitor_ReadResponse_Motor1Status);
            data.Motor1Status = recvCmd40.GetByte(ParamId.SystemMonitor_ReadResponse_Motor1Result);
            data.Motor1completeSteps = (int)recvCmd40.GetULong(ParamId.SystemMonitor_ReadResponse_Motor1CompleteSteps);
            data.Motor1Steps = (int)recvCmd40.GetULong(ParamId.SystemMonitor_ReadResponse_Motor1SumSteps);

            data.Motor2Switch = recvCmd40.GetByte(ParamId.SystemMonitor_ReadResponse_Motor2Status);
            data.Motor2Status = recvCmd40.GetByte(ParamId.SystemMonitor_ReadResponse_Motor2Result);
            data.Motor2completeSteps = (int)recvCmd40.GetULong(ParamId.SystemMonitor_ReadResponse_Motor2CompleteSteps);
            data.Motor2Steps = (int)recvCmd40.GetULong(ParamId.SystemMonitor_ReadResponse_Motor2SumSteps);

            return data;
        }

    }
}