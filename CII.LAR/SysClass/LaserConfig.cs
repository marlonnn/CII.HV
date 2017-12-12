using CII.LAR.DrawTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.SysClass
{
    /// <summary>
    /// Laser config
    /// Author: Zhong Wen 2017/11/10
    /// </summary>
    public class LaserConfig
    {
        private int pulseSizeRatio;
        public int PulseSizeRatio
        {
            get { return this.pulseSizeRatio; }
        }

        public float PulseSize
        {
            get
            {
                return this.pulseSizeRatio * this.GraphicsProperties.PulseSize;
            }
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

        private static LaserConfig laserConfig;

        /// <summary>
        /// GraphicsProperties of this draw object 
        /// </summary>
        private GraphicsProperties graphicsProperties;
        public GraphicsProperties GraphicsProperties
        {
            get
            {
                return graphicsProperties;
            }
            set
            {
                graphicsProperties = value;
            }
        }

        /// <summary>
        /// GraphicsPropertiesManager: include all the draw object graphics properties
        /// </summary>
        private GraphicsPropertiesManager graphicsPropertiesManager = GraphicsPropertiesManager.GraphicsManagerSingleInstance();
        public GraphicsPropertiesManager GraphicsPropertiesManager
        {
            get
            {
                return graphicsPropertiesManager;
            }
            set
            {
                graphicsPropertiesManager = value;
            }
        }

        public LaserConfig()
        {
            this.minPulseWidth = 8;
            this.maxPulseWidth = 40;
            pulseSizeRatio = 2;
            InitializeGraphicsProperties();
            InitializeHolePulsePoints();
        }

        public static LaserConfig GetLaserConfig()
        {
            if (laserConfig == null)
            {
                laserConfig = new LaserConfig();
            }
            return laserConfig;
        }

        public void UpdatePulseWidth(float value)
        {
            if (value != this.GraphicsProperties.PulseSize)
            {
                this.GraphicsProperties.PulseSize = value;
            }
        }

        private void InitializeGraphicsProperties()
        {
            this.GraphicsProperties = GraphicsPropertiesManager.GetPropertiesByName("Circle");
            this.GraphicsProperties.Color = System.Drawing.Color.Yellow;
        }

        private void InitializeHolePulsePoints()
        {
            holePulsePoints = new List<HolePulsePoint>();
            holePulsePoints.Add(new HolePulsePoint(0.005f, 0.1f));
            holePulsePoints.Add(new HolePulsePoint(2.5f, 50f));
        }
    }
}
