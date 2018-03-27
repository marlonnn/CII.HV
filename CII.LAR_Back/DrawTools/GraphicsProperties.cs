using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.DrawTools
{
    /// <summary>
    /// Graphics property
    /// Author:Zhong Wen 2017/07/25
    /// </summary>
    [Serializable]
    public class GraphicsProperties
    {
        public delegate void GraphicsPropertiesChangedDelegate(DrawObject drawObject, GraphicsProperties graphicsProperties);

        [field: NonSerialized]
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
        private int targetSize;

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
        private int exclusionSize;

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

        private float pulseSize;
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

        [NonSerialized]
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
            graphicsName = name;
            switch(name)
            {
                case "Line":
                    color = Color.Green;
                    break;
                case "Text":
                    color = Color.Black;
                    break;
                case "Rectangle":
                    color = Color.Violet;
                    break;
                case "Ellipse":
                    color = Color.Orange;
                    break;
                case "Polygon":
                    color = Color.Blue;
                    break;
                case "Circle":
                    color = Color.Yellow;
                    break;
                case "Ruler":
                    color = Color.Red;
                    break;
            }
            SetDefault();
        }

        private void InitializeColorSets()
        {
            ColorSets = new Color[10] {
                Color.White, Color.Red, Color.Orange, Color.Yellow, Color.Green,
                Color.Blue, Color.Violet,Color.YellowGreen, Color.DarkGreen, Color.BlueViolet};
        }

        private void SetDefault()
        {
            penWidth = 1;
            pulseSize = 1;
            exclusionSize = 20;
            targetSize = 1;
            alpha = 255;
            InitializeColorSets();
        }

        public void ChangeColor(int value)
        {
            if (value > 0 && value < 11)
            {
                this.Color = ColorSets[value - 1];
            }
        }

        public int ColorIndex()
        {
            int index = 0;
            for (int i=0; i<ColorSets.Length; i++)
            {
                if (this.color == ColorSets[i])
                {
                    index = i;
                    break;
                }
            }
            return index;
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
