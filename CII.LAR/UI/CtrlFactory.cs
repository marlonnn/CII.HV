using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.UI
{
    public class CtrlFactory
    {
        private static CtrlFactory ctrlFactory;

        private SettingControl setingControl;
        private SettingCtrl settingCtrl;
        private SerialPortCtrl serialPortCtrl;
        private StatisticsCtrl statisticsCtrl;
        private LaserAppearanceCtrl laserAppearanceCtrl;
        private RulerAppearanceCtrl rulerAppearanceCtrl;
        private ScaleAppearanceCtrl scaleAppearanceCtrl;
        private LaserCtrl laserCtrl;
        private LaserAlignment laserAlignment;
        private VideoChooseCtrl videoChooseCtrl;
        private LaserHoleSize laserHoleSize;
        private DebugCtrl debugCtrl;
        private ObjectLenseCtrl lenseCtrl;
        private ShortcutCtrl shortcutCtrl;

        public static void InitializeCtrlFactory(RichPictureBox richPictureBox)
        {
            ctrlFactory = new CtrlFactory(richPictureBox);
        }

        public CtrlFactory(RichPictureBox richPictureBox)
        {
            setingControl = new SettingControl();
            settingCtrl = new SettingCtrl(richPictureBox);
            serialPortCtrl = new SerialPortCtrl();
            statisticsCtrl = new StatisticsCtrl();
            laserAppearanceCtrl = new LaserAppearanceCtrl(richPictureBox);
            rulerAppearanceCtrl = new RulerAppearanceCtrl(richPictureBox);
            laserCtrl = new LaserCtrl();
            this.laserAlignment = new LaserAlignment();
            videoChooseCtrl = new VideoChooseCtrl();
            laserHoleSize = new LaserHoleSize();
            debugCtrl = new DebugCtrl();
            lenseCtrl = new ObjectLenseCtrl(richPictureBox);
            shortcutCtrl = new ShortcutCtrl(Program.EntryForm.hotKeyManager);
            scaleAppearanceCtrl = new ScaleAppearanceCtrl(richPictureBox);
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
                case CtrlType.SettingCtrl:
                    ctrl = this.setingControl as T;
                    //ctrl = this.settingCtrl as T;
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
                case CtrlType.ScaleAppearanceCtrl:
                    ctrl = scaleAppearanceCtrl as T;
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
                case CtrlType.LaserHoleSize:
                    ctrl = this.laserHoleSize as T;
                    break;
                case CtrlType.DebugCtrl:
                    ctrl = this.debugCtrl as T;
                    break;
                case CtrlType.LenseCtrl:
                    ctrl = this.lenseCtrl as T;
                    break;
                case CtrlType.ShortCut:
                    ctrl = this.shortcutCtrl as T;
                    break;
            }
            return ctrl;
        }
    }
}
