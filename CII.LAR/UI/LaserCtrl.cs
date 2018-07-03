using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CII.LAR.DrawTools;
using CII.LAR.Laser;
using CII.LAR.SysClass;
using CII.LAR.Algorithm;
using CII.LAR.Protocol;
using CII.LAR.Commond;

namespace CII.LAR.UI
{
    /// <summary>
    /// Laser setting control
    /// Author: Zhong Wen 2017/08/05
    /// </summary>
    public partial class LaserCtrl : BaseCtrl
    {
        private List<HolePulsePoint> holePulsePoints;

        private GraphicsPropertiesManager graphicsPropertiesManager = Program.SysConfig.GraphicsPropertiesManager;

        private GraphicsProperties graphicsProperties;

        private double pulseValue;

        public double PulseValue
        {
            get { return this.pulseValue; }
            set { this.pulseValue = value; }
        }

        private List<double> savedPulseWidthList;
        public LaserCtrl() : base()
        {
            resources = new ComponentResourceManager(typeof(LaserCtrl));
            holePulsePoints = Program.SysConfig.LaserConfig.HolePulsePoints;
            this.ShowIndex = 5;
            this.CtrlType = CtrlType.LaserCtrl;
            graphicsProperties = graphicsPropertiesManager.GetPropertiesByName("Circle");
            InitializeComponent();
            InitializeSlider();
            this.sliderCtrl.Slider.Value = (int)(Program.SysConfig.LaserConfig.PulseWidth * 10);
            this.sliderCtrl.Slider.MouseUp += Slider_MouseUp;
            serialPortCom = SerialPortCommunication.GetInstance();
            savedPulseWidthList = Program.SysConfig.LaserConfig.SavedPulseWidth;
            InitializeSavedPulseWidthList();
            this.comboBoxEx1.SelectedValueChanged += ComboBoxEx1_SelectedValueChanged;
            this.holesSlider.Visible = false;
            this.lblHoleNumber.Visible = false;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (this.Visible)
            {
                var laserHoleSize = CtrlFactory.GetCtrlFactory().GetCtrlByType<LaserHoleSize>(CtrlType.LaserHoleSize);
                if (laserHoleSize != null)
                {
                    if (laserHoleSize.CurrentPoint != null)
                    {
                        double y = CalXY(laserHoleSize.CurrentPoint.X);
                        this.sliderCtrl.Slider.Value = (int)laserHoleSize.CurrentPoint.X * 10;
                        this.sliderCtrl.PulseHole.Text = string.Format("{0:N}us {1:N}um", laserHoleSize.CurrentPoint.X, y);
                        CheckPulse(PulseValue);

                        if (graphicsProperties != null && Program.SysConfig.LaserConfig != null)
                        {
                            Program.SysConfig.LaserConfig.UpdatePulseWidth((float)y);
                        }
                    }
                }
            }
        }

        public void HolesSliderVisiable(bool visiable)
        {
            this.holesSlider.Visible = visiable;
            this.lblHoleNumber.Visible = visiable;
        }

        private bool valueChangedInvoke = true;
        private void ComboBoxEx1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (valueChangedInvoke)
                RecalculateSliderValue();
        }

        private void RecalculateSliderValue()
        {
            try
            {
                var value = Double.Parse(this.comboBoxEx1.SelectedItem.ToString());
                PulseValue = value;
                this.sliderCtrl.Slider.Value = (int)(PulseValue * 10);
                Program.SysConfig.LaserConfig.PulseWidth = PulseValue;
                //double y = CalSlopeFunction(PulseValue);
                double y = CalXY((float)PulseValue);
                this.sliderCtrl.PulseHole.Text = string.Format("{0:N}us {1:N}um", PulseValue, y);

                CheckPulse(PulseValue);

                if (graphicsProperties != null && Program.SysConfig.LaserConfig != null)
                {
                    Program.SysConfig.LaserConfig.UpdatePulseWidth((float)y);
                }
                this.sliderCtrl.Update = false;
                if (UpdateSliderValueHandler != null)
                {
                    UpdateSliderValueHandler?.Invoke((float)(PulseValue));
                }

                this.sliderCtrl.Update = true;
            }
            catch (Exception ex)
            {
            }
        }

        private SerialPortCommunication serialPortCom;

        private void Slider_MouseUp(object sender, MouseEventArgs e)
        {
            if (serialPortCom != null)
            {
                LaserC72Request c72 = new LaserC72Request(PulseValue);
                var bps = c72.Encode();
                List<byte[]> bytes = new List<byte[]>();
                foreach (var b in bps)
                {
                    var data = LaserProtocolFactory.GetInstance().LaserProtocol.EnPackage(b);
                    bytes.Add(data);
                }
                //var bytes = serialPortCom.Encode(c72);
                serialPortCom.SendData(bytes);
            }
        }

        private void InitializeSlider()
        {
            this.sliderCtrl.SetMinMaxValue((int)(Program.SysConfig.LaserConfig.MinPulseWidthLimit * 10), (int)(Program.SysConfig.LaserConfig.MaxPulseWidthLimit * 10));
            //this.sliderCtrl.SetValue((float)Program.SysConfig.LaserConfig.PulseWidth);
            this.btnFire.BackColor = Color.LightYellow;
            this.btnFire.Text = Res.LaserCtrl.StrFire;
            PulseValue = this.sliderCtrl.Slider.Value;
        }

        private void InitializeSavedPulseWidthList()
        {
            savedPulseWidthList = Program.SysConfig.LaserConfig.SavedPulseWidth;
            if (savedPulseWidthList != null && savedPulseWidthList.Count > 0)
            {
                for (int i =0; i<savedPulseWidthList.Count; i++)
                {
                    this.comboBoxEx1.Items.Add(savedPulseWidthList[i]);
                    if (savedPulseWidthList[i] == Program.SysConfig.LaserConfig.PulseWidth)
                    {
                        this.comboBoxEx1.SelectedIndex = i;
                    }
                }
            }
        }

        public void UpdateHolesInfo(HolesInfo holesInfo)
        {
            this.holesSlider.Maximum = holesInfo.MaxHoleNum;
            this.holesSlider.Minimum = holesInfo.MinHoleNum;
            this.holesSlider.Value = holesInfo.HoleNum;
            this.holesSlider.Text = string.Format("{0}holes", holesInfo.HoleNum);
        }

        bool flashing = false;
        /// <summary>
        /// fire laser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnFire_Click(object sender, EventArgs e)
        {
            if (Program.EntryForm.Laser != null)
            {
                Program.EntryForm.Laser.Flashing = !flashing;

                var fixedLaser = Program.EntryForm.Laser as FixedLaser;
                if (fixedLaser != null)
                {
                    if (serialPortCom != null)
                    {
                        LaserC71Request c71 = new LaserC71Request();
                        var bytes = serialPortCom.Encode(c71);
                        serialPortCom.SendData(bytes);
                    }
                    //Coordinate.GetCoordinate().SendAlignmentMotorPoint();
                }
            }
        }

        private void btnAlignLaser_Click(object sender, EventArgs e)
        {
            DelegateClass.GetDelegate().ClickDelegateHandler?.Invoke(sender, CtrlType.LaserAlignment);
        }

        private void btnHoleSize_Click(object sender, EventArgs e)
        {
            DelegateClass.GetDelegate().ClickDelegateHandler?.Invoke(sender, CtrlType.LaserHoleSize);
        }

        private void btnAppearance_Click(object sender, EventArgs e)
        {
            DelegateClass.GetDelegate().ClickDelegateHandler?.Invoke(sender, CtrlType.LaserAppreance);
        }

        private void SliderValueChangedHandler(object sender, EventArgs e)
        {
            try
            {
                if (this.sliderCtrl.Slider.Value <= 10 && this.sliderCtrl.Slider.Value > 1)
                {
                    var v = this.sliderCtrl.Slider.Value / 10f;
                }

                PulseValue = this.sliderCtrl.Slider.Value / 10f;
                Program.SysConfig.LaserConfig.PulseWidth = PulseValue;
                double y = CalXY((float)PulseValue);
                this.sliderCtrl.PulseHole.Text = string.Format("{0:N}us {1:N}um", PulseValue, y);

                CheckPulse(PulseValue);

                if (graphicsProperties != null && Program.SysConfig.LaserConfig != null)
                {
                    Program.SysConfig.LaserConfig.UpdatePulseWidth((float)y);
                }
                this.sliderCtrl.Update = false;
                if (UpdateSliderValueHandler != null)
                {
                    UpdateSliderValueHandler?.Invoke((float)(PulseValue));
                }

                this.sliderCtrl.Update = true;
            }
            catch (Exception ex)
            {
            }
        }

        private double CalXY(float value)
        {
            double y = 0;
            int count = Program.SysConfig.LaserConfig.HolePulsePoints.Count;
            var x = value;
            if (x == Program.SysConfig.LaserConfig.HolePulsePoints[0].X)
            {
                y = Program.SysConfig.LaserConfig.HolePulsePoints[0].Y;
            }
            else if (x == Program.SysConfig.LaserConfig.HolePulsePoints[count - 1].X)
            {
                y = Program.SysConfig.LaserConfig.HolePulsePoints[count - 1].Y;
            }
            else if (x > Program.SysConfig.LaserConfig.HolePulsePoints[0].X && x < Program.SysConfig.LaserConfig.HolePulsePoints[count - 1].X)
            {
                for (int i = 0; i < Program.SysConfig.LaserConfig.HolePulsePoints.Count; i++)
                {
                    if (x > Program.SysConfig.LaserConfig.HolePulsePoints[i].X && x < Program.SysConfig.LaserConfig.HolePulsePoints[i + 1].X)
                    {
                        double k = (Program.SysConfig.LaserConfig.HolePulsePoints[i + 1].Y - Program.SysConfig.LaserConfig.HolePulsePoints[i].Y) / (Program.SysConfig.LaserConfig.HolePulsePoints[i + 1].X - Program.SysConfig.LaserConfig.HolePulsePoints[i].X);
                        y = k * (value - Program.SysConfig.LaserConfig.HolePulsePoints[i].X) + Program.SysConfig.LaserConfig.HolePulsePoints[i].Y;
                        break;
                    }
                }
            }
            return y;
        }

        public void UpdatePulseWidthSlider(float value)
        {
            this.sliderCtrl.UpdateValue(value);
        }

        private void CheckPulse(double value)
        {
            if (value > Program.SysConfig.LaserConfig.MaxPulseWidth)
            {
                //this.btnFire.BackColor = Color.LightSalmon;
                this.btnFire.Warning = true;
                this.btnFire.Text = Res.LaserCtrl.StrBigPulse;
            }
            else if (value < Program.SysConfig.LaserConfig.MinPulseWidth)
            {
                //this.btnFire.BackColor = Color.LightSalmon;
                this.btnFire.Warning = true;
                this.btnFire.Text = Res.LaserCtrl.StrSmallPulse;
            }
            else
            {
                //this.btnFire.BackColor = Color.LightYellow;
                this.btnFire.Warning = false;
                this.btnFire.Text = Res.LaserCtrl.StrFire;
            }
            this.btnFire.Invalidate();
        }

        private double CalSlopeFunction(double value)
        {
            double y = 0;
            int index = GetValueIndex(value);
            if (index != -1)
            {
                y = CalSlopeFunction(holePulsePoints[index], holePulsePoints[index + 1], value);
            }
            return y;
        }

        private double CalSlopeFunction(HolePulsePoint p1, HolePulsePoint p2, double value)
        {
            double k = 0;
            double deltaX = p2.X - p1.X;
            double deltaY = p2.Y - p1.Y;
            k = deltaY / deltaX;
            //var x = value;
            return  k * (value - p2.X) + p2.Y;
        }

        private int GetValueIndex(double value)
        {
            int index = -1;
            var x = value;
            if (holePulsePoints != null && holePulsePoints.Count > 0)
            {
                for (int i = 0; i < holePulsePoints.Count - 1; i++)
                {
                    var v1 = x - holePulsePoints[i].X;
                    var v2 = holePulsePoints[i + 1].X - x;
                    if (v1 > 0 && v2 >= 0)
                    {
                        index = i;
                        break;
                    }
                }
            }
            return index;
        }

        public override void RefreshUI()
        {
            base.RefreshUI();
            this.Title = global::CII.LAR.Properties.Resources.StrLaserCtrlTitle;
        }

        private void holesSlider_ValueChanged(object sender, EventArgs e)
        {
            Program.EntryForm.UpdateHoleNumber(this.holesSlider.Value);
        }

        /// <summary>
        /// save preset
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SavePulseWidth(Program.SysConfig.LaserConfig.PulseWidth);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DeletePulseWidth(Double.Parse(this.comboBoxEx1.SelectedItem.ToString()));
            }
            catch (Exception ex)
            {

            }
        }

        private void SavePulseWidth (double value)
        {
            if (!CheckExistPulseWidth(value))
            {
                savedPulseWidthList.Add(value);
                RefreshSavedPulseWidthCombox(value);
            }
        }

        private void DeletePulseWidth(double value)
        {
            int deleteIndex = savedPulseWidthList.FindIndex(v => v == value);
            if (CheckExistPulseWidth(value))
            {
                savedPulseWidthList.Remove(value);
                if (savedPulseWidthList.Count == 0)
                {
                    this.comboBoxEx1.Items.Clear();
                    this.comboBoxEx1.Text = "";
                }
                if (deleteIndex == 0)
                {
                    RefreshSavedPulseWidthCombox(savedPulseWidthList[0]);
                }
                else if (deleteIndex - 1 >= 0 && deleteIndex - 1 <= savedPulseWidthList.Count - 1)
                {
                    RefreshSavedPulseWidthCombox(savedPulseWidthList[deleteIndex - 1]);
                }
            }
        }

        private void RefreshSavedPulseWidthCombox(double seletedItem)
        {
            valueChangedInvoke = false;
            this.comboBoxEx1.Items.Clear();
            ReInitializeSavedPulseWidthList(seletedItem);
            valueChangedInvoke = true;
        }

        private void ReInitializeSavedPulseWidthList(double seletedItem)
        {
            savedPulseWidthList = Program.SysConfig.LaserConfig.SavedPulseWidth;
            if (savedPulseWidthList != null && savedPulseWidthList.Count > 0)
            {
                foreach (var pulseWidth in savedPulseWidthList)
                {
                    this.comboBoxEx1.Items.Add(pulseWidth);
                }
            }
            this.comboBoxEx1.SelectedItem = seletedItem;
        }

        private bool CheckExistPulseWidth (double value)
        {
            bool exist = false;
            if (savedPulseWidthList != null)
            {
                foreach (var pulseWidth in savedPulseWidthList)
                {
                    if (value == pulseWidth)
                    {
                        exist = true;
                        break;
                    }
                }
            }
            return exist;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Program.EntryForm.Laser.Flashing = false;
        }
    }
}
