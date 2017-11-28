﻿using CII.Ins.Model.Command.HV;
using CII.Ins.Model.Data.HV;
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
                Bitmap bitmap = new Bitmap("test.JPG");
                frm.SetLoadingImage((Image)bitmap);
                frm.SetLoadingName("Welcome");
                frm.ShowDialog();

                //MonitorData data = new MonitorData();
                //SendCommand sendCmd = new SendCommand(CommandId.SystemMonitor, CommandExtendId.Read);
                //RecvCommand recvCmd = (RecvCommand)PortManager.GetInstance().Send("HV", sendCmd);
                //var v = recvCmd.GetBytes();
                //var v1 = recvCmd.GetByte(ParamId.SystemMonitor_ReadResponse_FlowStatus);

                SendCommand sendCmd = new SendCommand(CommandId.SystemParameter, CommandExtendId.Write);
                sendCmd.SetParamValid(ParamId.SystemParameter_Read_Type, true);
                sendCmd.SetValue(ParamId.SystemParameter_ReadWrite_Type, 0xA0);
                sendCmd.SetValue(ParamId.SystemParameter_ReadWrite_MotorDriveFrequency, 100);
                sendCmd.SetValue(ParamId.SystemParameter_ReadWrite_MaximumPulse, 200);
                RecvCommand recvCmd = (RecvCommand)PortManager.GetInstance().Send("HV", sendCmd);
                var v = recvCmd.GetBytes();
                //var v1 = recvCmd.GetByte(ParamId.SystemParameter_Read_Type);
                //var v2 = recvCmd.GetSingle(ParamId.SystemParameter_ReadWrite_MotorDriveFrequency);
                //var v3 = recvCmd.GetSingle(ParamId.SystemParameter_ReadWrite_MaximumPulse);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
