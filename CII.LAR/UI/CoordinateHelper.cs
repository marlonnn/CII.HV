using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.UI
{
    public class CoordinateHelper
    {
        private ZWPictureBox pictureBox;
        public CoordinateHelper(ZWPictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
        }

        /// <summary>
        /// 屏幕上点转换为PictureBox上的点
        /// </summary>
        /// <param name="screenPoint">屏幕上点</param>
        /// <returns></returns>
        private Point PointToPictureBox(Point screenPoint)
        {
            return this.pictureBox.PointToClient(screenPoint);
        }

        /// <summary>
        /// PictureBox上的点转换为屏幕上的点
        /// </summary>
        /// <param name="pictureBoxPoint">PictureBox上的点</param>
        /// <returns></returns>
        private Point PictureBoxPointToScreen(Point pictureBoxPoint)
        {
            return this.pictureBox.PointToScreen(pictureBoxPoint);
        }

        /// <summary>
        /// 屏幕上的点的起点坐标转换为屏幕左下角
        /// </summary>
        /// <param name="screenPoint"></param>
        /// <param name="screenHeight"></param>
        /// <returns></returns>
        private Point ChangeToOriginalPoint(Point screenPoint, int screenHeight)
        {
            return new Point(screenPoint.X, screenHeight - screenPoint.Y);
        }
    }
}
