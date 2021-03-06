﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CII.LAR.DrawTools;
using CII.LAR.SysClass;
using System.Windows.Forms.DataVisualization.Charting;
using CII.LAR.Laser;
using CII.LAR.Commond;
using CII.LAR.Protocol;

namespace CII.LAR.UI
{
    public partial class LaserHoleSize : BaseCtrl
    {
        //private List<HolePulsePoint> Program.SysConfig.LaserConfig.HolePulsePoints;
        private GraphicsPropertiesManager graphicsPropertiesManager = Program.SysConfig.GraphicsPropertiesManager;

        private GraphicsProperties graphicsProperties;

        private HolePulsePoint currentPoint;
        public HolePulsePoint CurrentPoint
        {
            get { return this.currentPoint; }
            set
            {
                this.currentPoint = value;
                this.holeSizeCtrl.HoleSize = value.Y;
            }
        }

        private SerialPortManager serialPortCom;

        public LaserHoleSize()
        {
            resources = new ComponentResourceManager(typeof(LaserHoleSize));
            this.ShowIndex = 8;
            this.CtrlType = CtrlType.LaserHoleSize;
            graphicsProperties = graphicsPropertiesManager.GetPropertiesByName("Circle");
            InitializeComponent();
            InitializeSlider();
            InitializeChartSeries();
            InitializeHolePulsePoints();
            this.holeSizeCtrl.UpdownClickHandler += UpdownClickHandler;
            serialPortCom = SerialPortManager.GetInstance();
            this.btnSave.Click += BtnSave_Click;

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            MaterialSkin.ToastNotification.Instance().ShowToast(Properties.Resources.StrSaveSuccess, null);
        }

        private void InitializeHolePulsePoints()
        {
            CalPiecewiseFunction();
        }

        private void InitializeSlider()
        {
            this.sliderPulse.SetMinMaxValue((int)(Program.SysConfig.LaserConfig.MinPulseWidthLimit * 10), (int)(Program.SysConfig.LaserConfig.MaxPulseWidthLimit * 10));
            this.sliderPulse.SetValue((float)Program.SysConfig.LaserConfig.PulseWidth);
            //this.sliderPulse.Slider.Value = (int)(Program.SysConfig.LaserConfig.PulseWidth * 10);
            this.sliderPulse.SliderValueChangedHandler += PulseSliderValueChangedHandler;
            this.sliderPulse.Slider.MouseUp += Slider_MouseUp;
        }

        private void Slider_MouseUp(object sender, MouseEventArgs e)
        {
            if (serialPortCom != null)
            {
                LaserC72Request c72 = new LaserC72Request(this.sliderPulse.Slider.Value / 10f);
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

        public void ReculateSiderValue()
        {
            this.sliderPulse.SetValue((float)Program.SysConfig.LaserConfig.PulseWidth);
            CalXY((float)Program.SysConfig.LaserConfig.PulseWidth);
        }

        private void InitializeChartSeries()
        {
            this.chart1.ChartAreas[0].AxisX.Maximum = Program.SysConfig.LaserConfig.MaxPulseWidthLimit;
            this.chart1.ChartAreas[0].AxisX.Minimum = Program.SysConfig.LaserConfig.MinPulseWidthLimit;
            this.chart1.ChartAreas[0].AxisX.LabelStyle.Format = "0";
            this.chart1.ChartAreas[0].AxisX.Title = "us";

            this.chart1.ChartAreas[0].AxisY.Maximum = Program.SysConfig.LaserConfig.MaxHoleLimit;
            this.chart1.ChartAreas[0].AxisY.Minimum = Program.SysConfig.LaserConfig.MinHoleLimit;
            this.chart1.ChartAreas[0].AxisY.LabelStyle.Format = "##.#";
            this.chart1.ChartAreas[0].AxisY.Title = "um";
        }

        private void UpdownClickHandler(bool isUp)
        {
            bool exist = CheckPoint(CurrentPoint);
            var value = isUp ? CurrentPoint.Y + 0.2f : CurrentPoint.Y - 0.2f;
            //if (value < 0) return;
            if (!CheckPointValidate(new HolePulsePoint(CurrentPoint.X, value), exist)) return;
            SaveDeleteButtonVisiable(true);
            CurrentPoint = new HolePulsePoint(CurrentPoint.X, value);
            if (exist)
            {
                UpdatePoint(CurrentPoint);
            }
            else
            {
                AddPoint(CurrentPoint);
            }
            if (this.graphicsProperties != null)
            {
                Program.SysConfig.LaserConfig.UpdatePulseWidth(CurrentPoint.Y);
            }
        }

        /// <summary>
        /// 验证微调点的有效性
        /// </summary>
        /// <param name="point"></param>
        /// <param name="exist"></param>
        /// <returns></returns>
        private bool CheckPointValidate(HolePulsePoint point, bool exist)
        {
            bool validate = false;
            if (exist)
            {
                if (Program.SysConfig.LaserConfig.HolePulsePoints != null && Program.SysConfig.LaserConfig.HolePulsePoints.Count > 0)
                {
                    int index = Program.SysConfig.LaserConfig.HolePulsePoints.FindIndex(p => p.X == point.X);
                    if(index == Program.SysConfig.LaserConfig.HolePulsePoints.Count - 1)
                    {
                        if (point.Y < Program.SysConfig.LaserConfig.HolePulsePoints[index].Y)
                        {
                            validate = true;
                        }
                    }
                    else if ( index > 0 && index < Program.SysConfig.LaserConfig.HolePulsePoints.Count - 1)
                    {
                        if (point.Y > Program.SysConfig.LaserConfig.HolePulsePoints[index - 1].Y && point.Y < Program.SysConfig.LaserConfig.HolePulsePoints[index + 1].Y)
                        {
                            validate = true;
                        }
                    }
                    else
                    {
                        validate = point.Y > 0.01 && point.Y < 33;
                    }
                }
            }
            else
            {
                if (Program.SysConfig.LaserConfig.HolePulsePoints != null && Program.SysConfig.LaserConfig.HolePulsePoints.Count > 0)
                {
                    for (int i = 0; i < Program.SysConfig.LaserConfig.HolePulsePoints.Count; i++)
                    {
                        if (point.X > Program.SysConfig.LaserConfig.HolePulsePoints[i].X && point.X < Program.SysConfig.LaserConfig.HolePulsePoints[i + 1].X)
                        {
                            validate = point.Y > Program.SysConfig.LaserConfig.HolePulsePoints[i].Y && point.Y < Program.SysConfig.LaserConfig.HolePulsePoints[i + 1].Y;
                        }
                    }
                }
            }
            return validate;
        }

        private void RemovePoint(HolePulsePoint point)
        {
            if (Program.SysConfig.LaserConfig.HolePulsePoints != null)
            {
                for (int i = 0; i < Program.SysConfig.LaserConfig.HolePulsePoints.Count; i++)
                {
                    if (Program.SysConfig.LaserConfig.HolePulsePoints[i].X == point.X)
                    {
                        Program.SysConfig.LaserConfig.HolePulsePoints.Remove(Program.SysConfig.LaserConfig.HolePulsePoints[i]);
                    }
                }
                CalPiecewiseFunction();
            }
        }

        private void AddPoint(HolePulsePoint point)
        {
            if (Program.SysConfig.LaserConfig.HolePulsePoints != null)
            {
                Program.SysConfig.LaserConfig.HolePulsePoints.Add(point);
                CalPiecewiseFunction();
            }
        }

        private void UpdatePoint(HolePulsePoint point)
        {
            if (Program.SysConfig.LaserConfig.HolePulsePoints != null && Program.SysConfig.LaserConfig.HolePulsePoints.Count > 0)
            {
                for (int i = 0; i < Program.SysConfig.LaserConfig.HolePulsePoints.Count; i++)
                {
                    if (Program.SysConfig.LaserConfig.HolePulsePoints[i].X == point.X)
                    {
                        Program.SysConfig.LaserConfig.HolePulsePoints[i].Y = point.Y;
                    }
                }
                CalPiecewiseFunction();
            }
        }

        private bool CheckPoint(HolePulsePoint point)
        {
            bool exist = false;
            if (point != null)
            {
                if (Program.SysConfig.LaserConfig.HolePulsePoints != null && Program.SysConfig.LaserConfig.HolePulsePoints.Count > 0)
                {
                    for (int i = 0; i < Program.SysConfig.LaserConfig.HolePulsePoints.Count; i++)
                    {
                        if (Program.SysConfig.LaserConfig.HolePulsePoints[i].X == point.X)
                        {
                            exist = true;
                        }
                    }
                }
            }
            return exist;
        }

        private bool CheckPoint(float x)
        {
            bool exist = false;
            if (Program.SysConfig.LaserConfig.HolePulsePoints != null && Program.SysConfig.LaserConfig.HolePulsePoints.Count > 0)
            {
                for (int i = 0; i < Program.SysConfig.LaserConfig.HolePulsePoints.Count - 1; i++)
                {
                    if (Program.SysConfig.LaserConfig.HolePulsePoints[i].X == x)
                    {
                        exist = true;
                    }
                }
            }
            return exist;
        }

        private void SortPoints()
        {
            Program.SysConfig.LaserConfig.HolePulsePoints.Sort((p1, p2) => p1.X.CompareTo(p2.X));
        }

        private void CalPiecewiseFunction()
        {
            if (Program.SysConfig.LaserConfig.HolePulsePoints != null && Program.SysConfig.LaserConfig.HolePulsePoints.Count > 0)
            {
                SortPoints();
                this.chart1.Series[0].Points.Clear();
                for (int i=0; i<Program.SysConfig.LaserConfig.HolePulsePoints.Count; i++)
                {
                    if (i + 1 < Program.SysConfig.LaserConfig.HolePulsePoints.Count)
                    {
                        CalSlopeFunction(Program.SysConfig.LaserConfig.HolePulsePoints[i], Program.SysConfig.LaserConfig.HolePulsePoints[i + 1]);
                    }
                }
            }
        }

        private void CalSlopeFunction(HolePulsePoint p1, HolePulsePoint p2)
        {
            this.chart1.Series[0].Points.AddXY(p1.X, p1.Y);
            this.chart1.Series[0].Points.AddXY(p2.X, p2.Y);
        }

        private void PulseSliderValueChangedHandler(object sender, EventArgs e)
        {
            var value = this.sliderPulse.Slider.Value;
            var x = value / 10f;
            this.sliderPulse.PulseHole.Text = string.Format("{0:N} us", x);
            //this.sliderPulse.SetValue(x);
            CalXY(x);
            if (this.graphicsProperties != null)
            {
                Program.SysConfig.LaserConfig.UpdatePulseWidth(CurrentPoint.Y);
            }
            this.sliderPulse.Update = false;
            UpdateSliderValueHandler?.Invoke(x);
            this.sliderPulse.Update = true;
            SaveDeleteButtonVisiable(CheckPoint(x));
        }
        public void UpdatePulseWidthSlider(float value)
        {
            this.sliderPulse.UpdateValue(value);
        }

        /// <summary>
        /// 根据分段函数来计算，怕了吧~
        /// </summary>
        /// <param name="value"></param>
        private void CalXY(float value)
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
            CurrentPoint = new HolePulsePoint(x, (float)y);
        }

        private void SaveDeleteButtonVisiable(bool isVisiable)
        {
            this.btnSave.Visible = isVisiable;
            this.btnDelete.Visible = isVisiable;
        }

        public override void RefreshUI()
        {
            base.RefreshUI();
            this.Title = global::CII.LAR.Properties.Resources.StrLaserHoleSizeCalibration;
        }

        private void btnLaserCtrl_Click(object sender, EventArgs e)
        {
            DelegateClass.GetDelegate().ClickDelegateHandler?.Invoke(sender, CtrlType.LaserCtrl);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = Program.SysConfig.LaserConfig.HolePulsePoints.FindIndex(p => p.X == CurrentPoint.X);
            if (index == 0 || index == Program.SysConfig.LaserConfig.HolePulsePoints.Count - 1)
            {
                return;
            }
            RemovePoint(CurrentPoint);
            SaveDeleteButtonVisiable(false);
            this.sliderPulse.SetValue(CurrentPoint.X);
            this.sliderPulse.PulseHole.Text = string.Format("{0:N} us", CurrentPoint.X);
            //this.sliderPulse.SetValue(x);
            CalXY(CurrentPoint.X);
            if (this.graphicsProperties != null)
            {
                Program.SysConfig.LaserConfig.UpdatePulseWidth(CurrentPoint.Y);
            }
            this.sliderPulse.Update = false;
            UpdateSliderValueHandler?.Invoke(CurrentPoint.X);
            this.sliderPulse.Update = true;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (this.Visible)
            {
                ReculateSiderValue();
                var dataPoints = this.chart1.Series[0].Points;
                if (dataPoints != null && dataPoints.Count > 2)
                {
                    for (int i = 1; i < dataPoints.Count - 1; i++)
                    {
                        if (Math.Abs(valueToFindX - dataPoints[i].XValue) < 0.01)
                        {
                            dataPoints[i].MarkerSize = 10;
                            dataPoints[i].MarkerColor = Color.DarkGreen;
                        }
                    }
                    this.chart1.Invalidate();
                }
                SaveDeleteButtonVisiable(false);
            }
        }

        Point? prevPosition = null;
        //ToolTip tooltip = new ToolTip();

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                var pos = e.Location;
                if (prevPosition.HasValue && pos == prevPosition.Value)
                    return;
                //tooltip.RemoveAll();
                prevPosition = pos;
                var results = chart1.HitTest(pos.X, pos.Y, false,
                                                ChartElementType.DataPoint);
                foreach (var result in results)
                {
                    if (result.ChartElementType == ChartElementType.DataPoint)
                    {
                        var prop = result.Object as DataPoint;
                        if (prop != null)
                        {
                            var pointXPixel = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue);
                            var pointYPixel = result.ChartArea.AxisY.ValueToPixelPosition(prop.YValues[0]);

                            // check if the cursor is really close to the point (2 pixels around the point)
                            if (Math.Abs(pos.X - pointXPixel) < 4&&
                                Math.Abs(pos.Y - pointYPixel) < 4)
                            {
                                //tooltip.Show("X=" + prop.XValue + ", Y=" + prop.YValues[0], this.chart1,
                                //                pos.X, pos.Y - 15);
                                valueToFindX = prop.XValue;
                                valueToFindY = prop.YValues[0];
                                findPoint = true;
                            }
                            else
                            {
                                findPoint = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private double valueToFindX;
        private double valueToFindY;
        private bool findPoint = false;

        private void chart1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && findPoint)
            {
                var dataPoints = this.chart1.Series[0].Points;
                if (dataPoints != null && dataPoints.Count > 0)
                {
                    for (int i =0; i < dataPoints.Count; i++)
                    {
                        if (Math.Abs(valueToFindX - dataPoints[i].XValue) < 0.01)
                        {
                            dataPoints[i].MarkerStyle = MarkerStyle.Circle;
                            dataPoints[i].MarkerColor = Color.Red;
                            dataPoints[i].MarkerSize = 10;
                            CurrentPoint = new HolePulsePoint((float)dataPoints[i].XValue, (float)dataPoints[i].YValues[0]);
                            int index = Program.SysConfig.LaserConfig.HolePulsePoints.FindIndex(p => p.X == CurrentPoint.X);
                            if (index != 0 &&  index != Program.SysConfig.LaserConfig.HolePulsePoints.Count - 1)
                            {
                                SaveDeleteButtonVisiable(true);
                            }
                            this.sliderPulse.Update = false;
                            this.sliderPulse.SetValue(CurrentPoint.X);
                            if (this.graphicsProperties != null)
                            {
                                Program.SysConfig.LaserConfig.UpdatePulseWidth(CurrentPoint.Y);
                            }
                            UpdateSliderValueHandler?.Invoke(CurrentPoint.X);
                            this.sliderPulse.Update = true;
                        }
                        else
                        {
                            dataPoints[i].MarkerSize = 10;
                            dataPoints[i].MarkerColor = Color.DarkGreen;
                        }
                    }
                    this.chart1.Invalidate();
                }
            }
        }

        bool flashing = false;

        private void btnFire_Click(object sender, EventArgs e)
        {

            var fixedLaser = Program.EntryForm.Laser as FixedLaser;
            if (fixedLaser != null)
            {
                if (serialPortCom != null)
                {
                    Program.EntryForm.Laser.Flashing = !flashing;

                    LaserC71Request c71 = new LaserC71Request();
                    var bytes = serialPortCom.Encode(c71);
                    serialPortCom.SendData(bytes);
                }
                //Coordinate.GetCoordinate().SendAlignmentMotorPoint();
            }

            var activeLaser = Program.EntryForm.Laser as ActiveLaser;
            if (activeLaser != null)
            {
                if (serialPortCom != null)
                {
                    activeLaser.Circle++;
                    activeLaser.CircleFire = true;
                    Program.EntryForm.Laser.Flashing = !flashing;
                }
            }
        }
    }
}
