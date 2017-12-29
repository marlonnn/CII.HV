using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.UI
{
    public enum CtrlType
    {
        SettingCtrl,
        SerialPort,
        StatisticsCtrl,
        LaserAlignment,
        LaserAppreance,
        LaserCtrl,
        VideoChooseCtrl,
        LaserHoleSize,
        RulerAppearanceCtrl
    }

    public class CtrlFactory
    {
        private static CtrlFactory ctrlFactory;

        private SettingCtrl settingCtrl;
        private SerialPortCtrl serialPortCtrl;
        private StatisticsCtrl statisticsCtrl;
        private LaserAppearanceCtrl laserAppearanceCtrl;
        private RulerAppearanceCtrl rulerAppearanceCtrl;
        private LaserCtrl laserCtrl;
        private LaserAlignment laserAlignment;
        private VideoChooseCtrl videoChooseCtrl;

        public static void InitializeCtrlFactory(VideoControl videoControl)
        {
            ctrlFactory = new CtrlFactory(videoControl);
        }

        public CtrlFactory(VideoControl videoControl)
        {
            settingCtrl = new SettingCtrl(videoControl);
            serialPortCtrl = new SerialPortCtrl();
            statisticsCtrl = new StatisticsCtrl();
            laserAppearanceCtrl = new LaserAppearanceCtrl();
            rulerAppearanceCtrl = new RulerAppearanceCtrl();
            laserCtrl = new LaserCtrl();
            this.laserAlignment = new LaserAlignment();
            videoChooseCtrl = new VideoChooseCtrl();
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
                //case CtrlType.LaserHoleSize:
                //    ctrl = this.laserHoleSize as T;
                //    break;
                //case CtrlType.StatisticsCtrl:
                //    ctrl = this.statisticsCtrl as T;
                //    break;
                case CtrlType.SettingCtrl:
                    ctrl = this.settingCtrl as T;
                    break;
                case CtrlType.SerialPort:
                    ctrl = this.serialPortCtrl as T;
                        break;
                case CtrlType.StatisticsCtrl:
                    ctrl = this.statisticsCtrl as T;
                    break;
                case CtrlType.LaserAppreance:
                    ctrl = laserAppearanceCtrl as T;
                    break;
                case CtrlType.RulerAppearanceCtrl:
                    ctrl = rulerAppearanceCtrl as T;
                    break;
                case CtrlType.LaserCtrl:
                    ctrl = this.laserCtrl as T;
                    break;
                case CtrlType.LaserAlignment:
                    ctrl = this.laserAlignment as T;
                    break;
                case CtrlType.VideoChooseCtrl:
                    ctrl = this.videoChooseCtrl as T;
                    break;
            }
            return ctrl;
        }
    }
}
