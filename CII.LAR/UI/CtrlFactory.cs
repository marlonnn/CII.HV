using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.UI
{
    public enum CtrlType
    {
        LaserAlignment,
        LaserAppreance,
        LaserCtrl,
        LaserHoleSize,
        StatisticsCtrl,
        RulerAppearanceCtrl,
        SettingCtrl
    }

    public class CtrlFactory
    {
        private static CtrlFactory ctrlFactory;

        private SettingCtrl settingCtrl;

        public static void InitializeCtrlFactory(ZWPictureBox pictureBox)
        {
            ctrlFactory = new CtrlFactory(pictureBox);
        }

        public CtrlFactory(ZWPictureBox pictureBox)
        {
            settingCtrl = new SettingCtrl(pictureBox);
        }

        public static CtrlFactory GetCtrlFactory()
        {
            return ctrlFactory;
        }

        /// <summary>
        /// Get control by control type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ctrlType"></param>
        /// <returns></returns>
        public T GetCtrlByType<T>(CtrlType ctrlType) where T : BaseCtrl
        {
            T ctrl = null;
            switch (ctrlType)
            {
                //case CtrlType.LaserAlignment:
                //    ctrl = this.laserAlignment as T;
                //    break;
                //case CtrlType.LaserAppreance:
                //    ctrl = this.laserAppearance as T;
                //    break;
                //case CtrlType.LaserCtrl:
                //    ctrl = this.laserCtrl as T;
                //    break;
                //case CtrlType.LaserHoleSize:
                //    ctrl = this.laserHoleSize as T;
                //    break;
                //case CtrlType.RulerAppearanceCtrl:
                //    ctrl = this.rulerAppearanceCtrl as T;
                //    break;
                //case CtrlType.StatisticsCtrl:
                //    ctrl = this.statisticsCtrl as T;
                //    break;
                case CtrlType.SettingCtrl:
                    ctrl = this.settingCtrl as T;
                    break;
            }
            return ctrl;
        }
    }
}
