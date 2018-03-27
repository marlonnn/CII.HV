using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Laser
{
    public class HolesInfo
    {
        public delegate void HolesInfoChange(HolesInfo holesInfo);
        public HolesInfoChange HolesInfoChangeHandler;
        private int minHoleNum = 0;
        public int MinHoleNum
        {
            get { return this.minHoleNum; }
            set
            {
                this.minHoleNum = value;
            }
        }

        private int maxHoleNum = 0;
        public int MaxHoleNum
        {
            get { return this.maxHoleNum; }
            set
            {
                this.maxHoleNum = value;
            }
        }

        private int holeNum = 0;
        public int HoleNum
        {
            get { return this.holeNum; }
            set
            {
                this.holeNum = value;
                HolesInfoChangeHandler?.Invoke(this);
            }
        }

        public HolesInfo()
        {

        }

        public void UpdateHoleNum(int holes)
        {
            this.holeNum = holes;
        }
    }
}
