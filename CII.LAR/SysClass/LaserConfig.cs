using CII.LAR.DrawTools;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.SysClass
{
    /// <summary>
    /// Laser config
    /// Author: Zhong Wen 2017/11/10
    /// </summary>
    [Serializable]
    public class LaserConfig
    {
        private Matrix<double> finalMatrix;
        public Matrix<double> FinalMatrix
        {
            get { return this.finalMatrix; }
            set { this.finalMatrix = value; }
        }

        private int pulseSizeRatio;
        public int PulseSizeRatio
        {
            get { return this.pulseSizeRatio; }
        }

        public float PulseSize
        {
            get
            {
                return this.pulseSizeRatio * Program.SysConfig.GraphicsPropertiesManager.GetPropertiesByName("Circle").PulseSize;
            }
        }

        private float pulseWidth;

        public float PulseWidth
        {
            get { return this.pulseWidth; }
            set { this.pulseWidth = value; }
        }

        private int minPulseWidth;
        public int MinPulseWidth
        {
            get { return this.minPulseWidth; }
        }

        private int maxPulseWidth;
        public int MaxPulseWidth
        {
            get { return this.maxPulseWidth; }
        }

        private List<HolePulsePoint> holePulsePoints;

        public List<HolePulsePoint> HolePulsePoints
        {
            get { return this.holePulsePoints; }
        }

        public LaserConfig()
        {
            SetDefault();
        }

        private void SetDefault()
        {
            this.minPulseWidth = 8;
            this.maxPulseWidth = 40;
            pulseSizeRatio = 2;
            pulseWidth = 0.5f;
            this.FinalMatrix = Matrix<double>.Build.Dense(3, 3);
            InitializeHolePulsePoints();
        }

        public void UpdatePulseWidth(float value)
        {
            if (value != Program.SysConfig.GraphicsPropertiesManager.GetPropertiesByName("Circle").PulseSize)
            {
                Program.SysConfig.GraphicsPropertiesManager.GetPropertiesByName("Circle").PulseSize = value;
            }
        }

        private void InitializeHolePulsePoints()
        {
            holePulsePoints = new List<HolePulsePoint>();
            holePulsePoints.Add(new HolePulsePoint(0.005f, 0.1f));
            holePulsePoints.Add(new HolePulsePoint(2.5f, 50f));
        }

        // set default value
        [OnDeserializing]
        private void OnDeserializing(StreamingContext sc)
        {
            SetDefault();
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext sc)
        {

        }
    }
}
