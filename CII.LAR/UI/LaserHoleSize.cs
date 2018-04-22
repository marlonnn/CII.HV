using System;
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

        }

        private void InitializeHolePulsePoints()
        {
            CalPiecewiseFunction();
        }

        private void InitializeSlider()
        {
            this.sliderPulse.SetMinMaxValue(5, 1600);
            this.sliderPulse.SetValue((float)Program.SysConfig.LaserConfig.PulseWidth);
            this.sliderPulse.Slider.Value = (int)(Program.SysConfig.LaserConfig.PulseWidth * 1000);
            this.sliderPulse.SliderValueChangedHandler += PulseSliderValueChangedHandler;
        }


        private void InitializeChartSeries()
        {
            this.chart1.ChartAreas[0].AxisX.Maximum = 1.600d;
            this.chart1.ChartAreas[0].AxisX.Minimum = 0.005d;
            this.chart1.ChartAreas[0].AxisX.LabelStyle.Format = "0.00";
            this.chart1.ChartAreas[0].AxisX.Title = "ms";

            this.chart1.ChartAreas[0].AxisY.Maximum = 50d;
            this.chart1.ChartAreas[0].AxisY.Minimum = 0.1d;
            this.chart1.ChartAreas[0].AxisY.Title = "um";
        }

        private void UpdownClickHandler(bool isUp)
        {
            SaveDeleteButtonVisiable(true);
            CurrentPoint = new HolePulsePoint(CurrentPoint.X, isUp ? CurrentPoint.Y + 0.2f : CurrentPoint.Y - 0.2f);
            if (CheckPoint(CurrentPoint))
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
            if (Program.SysConfig.LaserConfig.HolePulsePoints != null && Program.SysConfig.LaserConfig.HolePulsePoints.Count > 0)
            {
                for (int i=0; i< Program.SysConfig.LaserConfig.HolePulsePoints.Count; i++)
                {
                    if (Program.SysConfig.LaserConfig.HolePulsePoints[i].X == point.X)
                    {
                        exist = true;
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
            var x = value / 1000f;
            this.sliderPulse.PulseHole.Text = string.Format("{0:N} ms", x);
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

        private void CalXY(float value)
        {
            for (int i = 0; i < Program.SysConfig.LaserConfig.HolePulsePoints.Count - 1; i++)
            {
                var x = value;
                double k = (Program.SysConfig.LaserConfig.HolePulsePoints[i + 1].Y - Program.SysConfig.LaserConfig.HolePulsePoints[i].Y) / (Program.SysConfig.LaserConfig.HolePulsePoints[i + 1].X - Program.SysConfig.LaserConfig.HolePulsePoints[i].X);
                var y = k * (value - Program.SysConfig.LaserConfig.HolePulsePoints[i].X) + Program.SysConfig.LaserConfig.HolePulsePoints[i].Y;
                CurrentPoint = new HolePulsePoint(x, (float)y);
            }
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
            RemovePoint(CurrentPoint);
            SaveDeleteButtonVisiable(false);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            var dataPoints = this.chart1.Series[0].Points;
            if (dataPoints != null && dataPoints.Count > 0)
            {
                for (int i = 0; i < dataPoints.Count; i++)
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
                            SaveDeleteButtonVisiable(true);
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
    }
}
