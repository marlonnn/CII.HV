using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR
{
    public enum LaserType
    {
        SaturnFixed,
        SaturnActive,
        Alignment
    }

    public enum MachineStatus
    {
        LiveVideo,
        Simulate,
        Unknown
    }

    public class ExpManager
    {
        private MachineStatus machineStatus;
        public MachineStatus MachineStatus
        {
            get { return this.machineStatus; }
            set { this.machineStatus = value; }
        }

        public ExpManager()
        {
            this.machineStatus = MachineStatus.Unknown;
        }

    }
}
