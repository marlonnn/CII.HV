using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Opertion
{
    public class CameraSizeControl
    {
        public event EventHandler<EventArgs> AOIChanged;

        protected uEye.Camera camera;

        private int width;
        public int Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        private int height;
        public int Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        public CameraSizeControl(uEye.Camera camera)
        {
            this.camera = camera;
        }

        public void SetAoiWidth(int s32Value)
        {
            uEye.Defines.Status statusRet;
            Rectangle rect;
            uEye.Types.Range<Int32> rangeWidth, rangeHeight;
            camera.Size.AOI.GetPosRange(out rangeWidth, out rangeHeight);

            while ((s32Value % rangeWidth.Increment) != 0)
            {
                --s32Value;
            }

            statusRet = camera.Size.AOI.Get(out rect);
            rect.Width = s32Value;

            statusRet = camera.Size.AOI.Set(rect);

            // memory reallocation
            Int32[] memList;
            statusRet = camera.Memory.GetList(out memList);
            statusRet = camera.Memory.Free(memList);
            statusRet = camera.Memory.Allocate();
        }

        public void SetAoiHeight(int s32Value)
        {
            uEye.Defines.Status statusRet;
            System.Drawing.Rectangle rect;

            uEye.Types.Range<Int32> rangeWidth, rangeHeight;
            statusRet = camera.Size.AOI.GetPosRange(out rangeWidth, out rangeHeight);

            while ((s32Value % rangeHeight.Increment) != 0)
            {
                --s32Value;
            }

            statusRet = camera.Size.AOI.Get(out rect);
            rect.Height = s32Value;

            statusRet = camera.Size.AOI.Set(rect);

            // memory reallocation
            Int32[] memList;
            statusRet = camera.Memory.GetList(out memList);
            statusRet = camera.Memory.Free(memList);
            statusRet = camera.Memory.Allocate();
        }

        public void SetAoiBounds(int sWidth, int sHeight, int left, int top)
        {
            SetAoiWidthHeight(sWidth, sHeight);
            SetAoiLeftTop(left, top);
            // inform our main form
            if (AOIChanged != null)
            {
                AOIChanged(this, EventArgs.Empty);
            }
        }

        public void SetAoiWidthHeight(int sWidth, int sHeight)
        {
            uEye.Defines.Status statusRet;
            Rectangle rect;
            uEye.Types.Range<Int32> rangeWidth, rangeHeight;
            camera.Size.AOI.GetPosRange(out rangeWidth, out rangeHeight);

            while ((sWidth % rangeWidth.Increment) != 0)
            {
                --sWidth;
            }
            while ((sHeight % rangeHeight.Increment) != 0)
            {
                --sHeight;
            }

            statusRet = camera.Size.AOI.Get(out rect);
            rect.Width = sWidth;
            rect.Height = sHeight;
            statusRet = camera.Size.AOI.Set(rect);

            // memory reallocation
            Int32[] memList;
            statusRet = camera.Memory.GetList(out memList);
            statusRet = camera.Memory.Free(memList);
            statusRet = camera.Memory.Allocate();
        }

        private void SetAoiLeftTop(int left, int top)
        {
            uEye.Defines.Status statusRet;
            System.Drawing.Rectangle rect;

            uEye.Types.Range<Int32> rangePosX, rangePosY;
            statusRet = camera.Size.AOI.GetPosRange(out rangePosX, out rangePosY);

            while ((left % rangePosX.Increment) != 0)
            {
                --left;
            }
            while ((top % rangePosY.Increment) != 0)
            {
                --top;
            }
            statusRet = camera.Size.AOI.Get(out rect);
            rect.X = left;
            rect.Y = top;
            statusRet = camera.Size.AOI.Set(rect);

            // update aoi width
            uEye.Types.Range<Int32> rangeWidth, rangeHeight;
            statusRet = camera.Size.AOI.GetSizeRange(out rangeWidth, out rangeHeight);
        }


        private void SetAoiLeft(Int32 s32Value)
        {
            uEye.Defines.Status statusRet;
            System.Drawing.Rectangle rect;

            uEye.Types.Range<Int32> rangePosX, rangePosY;
            statusRet = camera.Size.AOI.GetPosRange(out rangePosX, out rangePosY);

            while ((s32Value % rangePosX.Increment) != 0)
            {
                --s32Value;
            }

            statusRet = camera.Size.AOI.Get(out rect);
            rect.X = s32Value;

            statusRet = camera.Size.AOI.Set(rect);

            // update aoi width
            uEye.Types.Range<Int32> rangeWidth, rangeHeight;
            statusRet = camera.Size.AOI.GetSizeRange(out rangeWidth, out rangeHeight);
        }

        private void SetAoiTop(Int32 s32Value)
        {
            uEye.Defines.Status statusRet;
            System.Drawing.Rectangle rect;

            uEye.Types.Range<Int32> rangePosX, rangePosY;
            statusRet = camera.Size.AOI.GetPosRange(out rangePosX, out rangePosY);

            while ((s32Value % rangePosY.Increment) != 0)
            {
                --s32Value;
            }

            statusRet = camera.Size.AOI.Get(out rect);
            rect.Y = s32Value;

            statusRet = camera.Size.AOI.Set(rect);

            // update aoi height
            uEye.Types.Range<Int32> rangeWidth, rangeHeight;
            statusRet = camera.Size.AOI.GetSizeRange(out rangeWidth, out rangeHeight);
        }
    }
}
