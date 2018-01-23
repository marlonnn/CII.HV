using CII.Ins.Model.Command.LAR;
using CII.Ins.Model.Data.LAR;
using CII.Library.CIINet.Commands;
using CII.Library.CIINet.Manager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.HV
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //Application.Run(new Form1());
                ////CII.Ins.Business.Entry.HV.Entry.
                //Form frm = CII.Library.UI.PC.Config.UIManager.GetInstance().MainForm;
                //Application.Run(frm);
                CII.Library.LoadingForm frm = new Library.LoadingForm();
                Bitmap bitmap = new Bitmap("test.png");
                frm.SetLoadingImage((Image)bitmap);
                frm.SetLoadingName("Welcome");
                frm.ShowDialog();

                //MonitorData data = new MonitorData();
                //SendCommand sendCmd = new SendCommand(CommandId.SystemMonitor, CommandExtendId.Read);
                //RecvCommand recvCmd = (RecvCommand)PortManager.GetInstance().Send("HV", sendCmd);
                //var v = recvCmd.GetBytes();
                //var v1 = recvCmd.GetByte(ParamId.SystemMonitor_ReadResponse_FlowStatus);

                //SendCommand sendCmd = new SendCommand(CommandId.SystemParameter, CommandExtendId.Write);
                //sendCmd.SetParamValid(ParamId.SystemParameter_Read_Type, true);
                //sendCmd.SetValue(ParamId.SystemParameter_ReadWrite_Type, 0xA0);
                //sendCmd.SetValue(ParamId.SystemParameter_ReadWrite_MotorDriveFrequency, 100);
                //sendCmd.SetValue(ParamId.SystemParameter_ReadWrite_MaximumPulse, 200);
                //RecvCommand recvCmd = (RecvCommand)PortManager.GetInstance().Send("HV", sendCmd);
                //var v = recvCmd.GetBytes();
                ////var v1 = recvCmd.GetByte(ParamId.SystemParameter_Read_Type);
                ////var v2 = recvCmd.GetSingle(ParamId.SystemParameter_ReadWrite_MotorDriveFrequency);
                ////var v3 = recvCmd.GetSingle(ParamId.SystemParameter_ReadWrite_MaximumPulse);

                SendCommand sendCmd = new SendCommand(CommandId.ControlConfig, CommandExtendId.Read);
                //sendCmd.SetParamValid(ParamId.ControlConfig_Read_Select, true);
                //sendCmd.SetParamValid(ParamId.ControlConfig_ReadWrite_ControlMode1, true);
                //sendCmd.SetParamValid(ParamId.ControlConfig_ReadWrite_Direction1, true);
                //sendCmd.SetParamValid(ParamId.ControlConfig_ReadWrite_TotalSteps1, true);
                //sendCmd.SetParamValid(ParamId.ControlConfig_ReadWrite_ControlMode2, true);
                //sendCmd.SetParamValid(ParamId.ControlConfig_ReadWrite_Direction2, true);
                //sendCmd.SetParamValid(ParamId.ControlConfig_ReadWrite_TotalSteps2, true);

                sendCmd.SetValue(ParamId.ControlConfig_Read_Select, 0x00);
                //sendCmd.SetValue(ParamId.ControlConfig_ReadWrite_ControlMode1, 0x00);
                //sendCmd.SetValue(ParamId.ControlConfig_ReadWrite_Direction1, 0x01);
                //sendCmd.SetValue(ParamId.ControlConfig_ReadWrite_TotalSteps1, 50);
                //sendCmd.SetValue(ParamId.ControlConfig_ReadWrite_ControlMode2, 0x00);
                //sendCmd.SetValue(ParamId.ControlConfig_ReadWrite_Direction2, 0x01);
                //sendCmd.SetValue(ParamId.ControlConfig_ReadWrite_TotalSteps2, 50);
                RecvCommand recvCmd = (RecvCommand)PortManager.GetInstance().Send("HV", sendCmd);
                var v = recvCmd.GetBytes();

                SendCommand sendCmd40 = new SendCommand(CommandId.SystemMonitor, CommandExtendId.Read);
                RecvCommand recvCmd40 = (RecvCommand)PortManager.GetInstance().Send("HV", sendCmd40);
                var v40 = recvCmd40.GetBytes();
                var m1Status = recvCmd40.GetByte(ParamId.SystemMonitor_ReadResponse_Motor1Status);
                var m1Results = recvCmd40.GetByte(ParamId.SystemMonitor_ReadResponse_Motor1Result);
                var m1CompleteSteps = recvCmd40.GetULong(ParamId.SystemMonitor_ReadResponse_Motor1CompleteSteps);
                var m1SumSteps = recvCmd40.GetULong(ParamId.SystemMonitor_ReadResponse_Motor1SumSteps);

                var m2Status = recvCmd40.GetByte(ParamId.SystemMonitor_ReadResponse_Motor2Status);
                var m2Results = recvCmd40.GetByte(ParamId.SystemMonitor_ReadResponse_Motor2Result);
                var m2CompleteSteps = recvCmd40.GetULong(ParamId.SystemMonitor_ReadResponse_Motor2CompleteSteps);
                var m2SumSteps = recvCmd40.GetULong(ParamId.SystemMonitor_ReadResponse_Motor2SumSteps);

                SendCommand sendCmd60 = new SendCommand(CommandId.ControlConfig, CommandExtendId.Write);
                sendCmd60.SetValue(ParamId.ControlConfig_ReadWrite_Select, 0x60);

                sendCmd60.SetValue(ParamId.ControlConfig_ReadWrite_ControlMode1, 0x01);
                sendCmd60.SetValue(ParamId.ControlConfig_ReadWrite_Direction1, 0x01);
                sendCmd60.SetValue(ParamId.ControlConfig_ReadWrite_TotalSteps1, 100);

                sendCmd60.SetValue(ParamId.ControlConfig_ReadWrite_ControlMode2, 0x01);
                sendCmd60.SetValue(ParamId.ControlConfig_ReadWrite_Direction2, 0x01);
                sendCmd60.SetValue(ParamId.ControlConfig_ReadWrite_TotalSteps2, 100);

                RecvCommand recvCmd60 = (RecvCommand)PortManager.GetInstance().Send("HV", sendCmd60);
                var v60 = recvCmd60.GetBytes();
                //var vv = recvCmd60.GetByte();
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
            }
        }
    }
}
