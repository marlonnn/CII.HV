using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CII.LAR.UI;

namespace CII.LAR.DrawTools
{
    public class DrawCircle : DrawObject
    {
        public override bool HitTest(int nIndex, PointF dataPoint)
        {
            throw new NotImplementedException();
        }

        public override HitTestResult HitTestForSelection(ZWPictureBox pictureBox, Point point)
        {
            throw new NotImplementedException();
        }
    }
}
