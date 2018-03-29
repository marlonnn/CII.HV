using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.DrawTools
{
    public class MeasureSystem
    {
        public static event MeasureUnitChangedEventHandler MeasureUnitChanged;
        public delegate void MeasureUnitChangedEventHandler(enUniMis NewUnit);

        private enUniMis myUserUnit;
        public enUniMis UserUnit
        {
            get { return myUserUnit; }
            set
            {
                if ((value == enUniMis.dmm) | (value == enUniMis.inches) | (value == enUniMis.um) | (value == enUniMis.mm) | (value == enUniMis.meters) | (value == enUniMis.cm))
                {
                    if (myUserUnit != value)
                    {
                        myUserUnit = value;
                        MeasureUnitChanged?.Invoke(myUserUnit);
                    }
                }
            }
        }

        public MeasureSystem()
        {
            try
            {
                UserUnit = enUniMis.mm;
            }
            catch (Exception ex)
            {
                LogHelper.GetLogger<MeasureSystem>().Error(ex.Message);
                LogHelper.GetLogger<MeasureSystem>().Error(ex.StackTrace);
            }
        }

        public static double CustomUnitToMicron(double MeasureValue, enUniMis CustomUnit)
        {
            double retVal = 0;
            //Converto in micron ...
            switch (CustomUnit)
            {
                case enUniMis.inches:
                    // 1 inch = 25400 micron ...
                    retVal = 25.4 * MeasureValue;
                    break;
                case enUniMis.um:
                    retVal = MeasureValue / 1000;
                    break;
                case enUniMis.meters:
                    retVal = 1000 * MeasureValue;
                    break;
                case enUniMis.cm:
                    retVal = 10 * MeasureValue;
                    break;
                case enUniMis.mm:
                    retVal = MeasureValue;
                    break;
                case enUniMis.dmm:
                    retVal = MeasureValue / 10;
                    break;
            }
            return retVal;
        }
    }
}
