using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.DrawTools
{
    /// <summary>
    /// Graphics properties manager
    /// Author:Zhong Wen 2017/08/10
    /// </summary>
    public class GraphicsPropertiesManager
    {
        public static GraphicsPropertiesManager graphicsManager;
        public static GraphicsPropertiesManager GraphicsManagerSingleInstance()
        {
            if (graphicsManager == null)
            {
                graphicsManager = new GraphicsPropertiesManager();
            }
            return graphicsManager;
        }

        /// <summary>
        /// all the graphics properties
        /// </summary>
        private List<GraphicsProperties> properties;
        public GraphicsPropertiesManager()
        {
            InitializeGraphicsProperties();
        }

        /// <summary>
        /// initialize graphics properties list
        /// </summary>
        private void InitializeGraphicsProperties()
        {
            properties = new List<GraphicsProperties>();
            properties.Add(new GraphicsProperties("Line"));
            properties.Add(new GraphicsProperties("Rectangle"));
            properties.Add(new GraphicsProperties("Ellipse"));
            properties.Add(new GraphicsProperties("Polygon"));
            properties.Add(new GraphicsProperties("Circle"));
            properties.Add(new GraphicsProperties("Text"));
            properties.Add(new GraphicsProperties("Ruler"));
        }

        /// <summary>
        /// Get GraphicsProperties by draw object name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public GraphicsProperties GetPropertiesByName(string name)
        {
            GraphicsProperties propertie = null;
            switch (name)
            {
                case "Line":
                    propertie = properties[0];
                    break;
                case "Rectangle":
                    propertie = properties[1];
                    break;
                case "Ellipse":
                    propertie = properties[2];
                    break;
                case "Polygon":
                    propertie = properties[3];
                    break;
                case "Circle":
                    propertie = properties[4];
                    break;
                case "Text":
                    propertie = properties[5];
                    break;
                case "Ruler":
                    propertie = properties[6];
                    break;
                default:
                    propertie = properties[0];
                    break;
            }
            return propertie;
        }

    }
}
