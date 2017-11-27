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
using CII.Ins.Model.Command.HV;
using CII.Library.CIINet.Manager;
using CII.Ins.Model.Data.HV;
using CII.Ins.Model.GlobalConfig;
using CII.Ins.Business.Command.Interface;
using CII.Library.CIINet.Converter;
using CII.Ins.Business.Instrument;

namespace CII.Ins.Business.Command.HV
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
        private static readonly string InsName = "HV";
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
        /// 0x44、0x45读取监控数据
        /// </summary>
        /// <returns></returns>
        public MonitorData GetMonitorData()
        {
            MonitorData data = new MonitorData();

            SendCommand sendCmd = new SendCommand(CommandId.SystemMonitor, CommandExtendId.Read);
            //sendCmd.SetValue(ParamId.SystemMonitor_Read_AC1,0x01);
            RecvCommand recvCmd = (RecvCommand)PortManager.GetInstance().Send(InsName, sendCmd);
            CheckRecvCommand(recvCmd);
            data.FlowStatus = recvCmd.GetByte(ParamId.SystemMonitor_ReadResponse_FlowStatus);

            ////0x45
            //SendCommand sendCmd = new SendCommand(CommandId.HVBoard, CommandExtendId.Read);
            //RecvCommand recvCmd = (RecvCommand)PortManager.GetInstance().Send(InsName, sendCmd);
            //CheckRecvCommand(recvCmd);

            //data.Voltage = recvCmd.GetSingle(ParamId.HVBoard_ReadResponse_Voltage);
            //data.Frequency = recvCmd.GetSingle(ParamId.HVBoard_ReadResponse_Frequency);
            //data.Electric = recvCmd.GetSingle(ParamId.HVBoard_ReadResponse_Electric);
            //data.AlarmCount = recvCmd.GetByte(ParamId.HVBoard_ReadResponse_AlarmCount);
            //data.AlarmCode = recvCmd.GetByte(ParamId.HVBoard_ReadResponse_AlarmCode);

            ////0x44
            //SendCommand sendCmd2 = new SendCommand(CommandId.TSTRTemperature, CommandExtendId.Read);
            //RecvCommand recvCmd2 = (RecvCommand)PortManager.GetInstance().Send(InsName, sendCmd2);
            //CheckRecvCommand(recvCmd2);

            //data.Temperature = recvCmd2.GetByte(ParamId.TSTRTemperature_ReadResponse_Temperature);

            return data;
        }

    }
}