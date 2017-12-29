using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.UI
{
    public enum PanDirection
    {
        None,
        Left,
        Right,
        Up,
        Down
    }

    public enum DrawToolType
    {
        None,
        Pointer,
        Line,
        Rectangle,
        Ellipse,
        Polygon,
        PolyLine,
        Circle,
        MultipleCircle,
        Move,
        NumberOfDrawTools
    }
}
