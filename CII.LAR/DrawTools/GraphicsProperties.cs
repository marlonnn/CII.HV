using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.DrawTools
{
    /// <summary>
    /// Graphics property
    /// Author:Zhong Wen 2017/07/25
    /// </summary>
    public class GraphicsProperties
    {
        public delegate void GraphicsPropertiesChangedDelegate(DrawObject drawObject, GraphicsProperties graphicsProperties);
        public GraphicsPropertiesChangedDelegate GraphicsPropertiesChangedHandler;

        private string graphicsName;
        public string GraphicsName
        {
            get
            {
                return graphicsName;
            }
            set
            {
                graphicsName = value;
            }
        }

        private Color[] colorSets;
        public Color[] ColorSets
        {
            get
            {
                return colorSets;
            }
            set
            {
                colorSets = value;
            }
        }

        private Color color;
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                if (value != color)
                {
                    color = value;
                    GraphicsPropertiesChangedHandler?.Invoke(DrawObject, this);
                }
            }
        }
        private int penWidth;
        public int PenWidth
        {
            get
            {
                return penWidth;
            }
            set
            {
                if (value != penWidth)
                {
                    penWidth = value;
                    GraphicsPropertiesChangedHandler?.Invoke(DrawObject, this);
                }
            }
        }

        /// <summary>
        /// set color transparency
        /// </summary>
        private int alpha;

        public int Alpha
        {
            get
            {
                return this.alpha;
            }
            set
            {
                if (value != this.alpha)
                {
                    this.alpha = value;
                    color = Color.FromArgb(value, this.color);
                    GraphicsPropertiesChangedHandler?.Invoke(DrawObject, this);
                }
            }
        }

        /// <summary>
        /// use for laser circle target size
        /// </summary>
        private int targetSize = 1;

        public int TargetSize
        {
            get { return this.targetSize; }
            set
            {
                if (value != this.targetSize)
                {
                    this.targetSize = value;
                    GraphicsPropertiesChangedHandler?.Invoke(DrawObject, this);
                }
            }
        }

        /// <summary>
        /// use for laser circle exclusion target size
        /// </summary>
        private int exclusionSize = 20;

        public int ExclusionSize
        {
            get { return this.exclusionSize; }
            set
            {
                if (value != this.exclusionSize)
                {
                    this.exclusionSize = value;
                    GraphicsPropertiesChangedHandler?.Invoke(DrawObject, this);
                }
            }
        }

        private float pulseSize = 1;
        public float PulseSize
        {
            get { return this.pulseSize; }
            set
            {
                if (value != this.pulseSize)
                {
                    this.pulseSize = value;
                    Console.WriteLine("graphic pulse size: " + value);
                    GraphicsPropertiesChangedHandler?.Invoke(DrawObject, this);
                }
            }
        }

        private DrawObject drawObject;

        public DrawObject DrawObject
        {
            get
            {
                return drawObject;
            }
            set
            {
                this.drawObject = value;
            }
        }
        public GraphicsProperties(string name)
        {
            color = Color.Red;
            penWidth = 1;
            graphicsName = name;
            InitializeColorSets();
        }

        private void InitializeColorSets()
        {
            ColorSets = new Color[10] {
                Color.White, Color.Red, Color.Orange, Color.Yellow, Color.Green,
                Color.Blue, Color.Violet,Color.YellowGreen, Color.DarkGreen, Color.BlueViolet};
        }

        public void ChangeColor(int value)
        {
            if (value > 0 && value < 11)
            {
                this.Color = ColorSets[value - 1];
            }
        }
    }
}
