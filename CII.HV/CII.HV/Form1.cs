using CII.Ins.Business.Command.HV;
using CII.Ins.Model.Command.HV;
using CII.Library.CIINet.Commands;
using CII.Library.CIINet.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.HV
{
    public partial class Form1 : Form
    {
        private HVCommandHelper hvCommandHelper;

        public Form1()
        {
            hvCommandHelper = new HVCommandHelper();
            InitializeComponent();
        }

        private void sendTimer_Tick(object sender, EventArgs e)
        {
            var v  = hvCommandHelper.GetMonitorData();
            Console.WriteLine("motor 1 steps: " + v.Motor1Steps);
            Console.WriteLine("motor 2 steps: " + v.Motor2Steps);
            //SendCommand sendCmd40 = new SendCommand(CommandId.SystemMonitor, CommandExtendId.Read);
            //RecvCommand recvCmd40 = (RecvCommand)PortManager.GetInstance().Send("HV", sendCmd40);
            //var v40 = recvCmd40.GetBytes();
            //var m1Status = recvCmd40.GetByte(ParamId.SystemMonitor_ReadResponse_Motor1Status);
            // var m1Results = recvCmd40.GetByte(ParamId.SystemMonitor_ReadResponse_Motor1Result);
            //var m1CompleteSteps = recvCmd40.GetULong(ParamId.SystemMonitor_ReadResponse_Motor1CompleteSteps);
            //var m1SumSteps = recvCmd40.GetULong(ParamId.SystemMonitor_ReadResponse_Motor1SumSteps);

            //var m2Status = recvCmd40.GetByte(ParamId.SystemMonitor_ReadResponse_Motor2Status);
            //var m2Results = recvCmd40.GetByte(ParamId.SystemMonitor_ReadResponse_Motor2Result);
            //var m2CompleteSteps = recvCmd40.GetULong(ParamId.SystemMonitor_ReadResponse_Motor2CompleteSteps);
            //var m2SumSteps = recvCmd40.GetULong(ParamId.SystemMonitor_ReadResponse_Motor2SumSteps);
            //Console.WriteLine("motor 1 sum steps: " + m1SumSteps);
            //Console.WriteLine("motor 2 sum steps: " + m2SumSteps);
        }

        private void receicedTimer_Tick(object sender, EventArgs e)
        {

        }
    }
}
