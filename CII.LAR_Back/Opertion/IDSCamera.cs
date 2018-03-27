using CII.LAR.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.Opertion
{
    /// <summary>
    /// IDS camera class
    /// Author: Zhong Wen 2017/11/18
    /// </summary>
    public class IDSCamera : Camera
    {
        private uEye.Camera camera;
        public uEye.Camera uEyeCamera
        {
            get { return this.camera; }
            private set { this.camera = value; }
        }

        private VideoControl videoControl;

        private IntPtr displayHandle;

        private CameraSizeControl cameraSizeControl;
        public CameraSizeControl CameraSizeControl
        {
            get { return this.cameraSizeControl; }
            private set { this.cameraSizeControl = value; }
        }

        public IDSCamera(IntPtr displayHandle)
        {
            this.displayHandle = displayHandle;
            this.uEyeCamera = new uEye.Camera();
            CameraSizeControl = new CameraSizeControl(uEyeCamera);
        }

        public IDSCamera(VideoControl videoControl)
        {
            this.videoControl = videoControl;
            this.displayHandle = videoControl.Handle;
            this.uEyeCamera = new uEye.Camera();
            CameraSizeControl = new CameraSizeControl(uEyeCamera);
        }

        /// <summary>
        /// initialize camera
        /// </summary>
        /// <returns></returns>
        public bool InitCamera(int s32Cam)
        {
            // Open Camera
            uEye.Defines.Status status = camera.Init(s32Cam, displayHandle);
            if (status != uEye.Defines.Status.SUCCESS)
            {
                SetError("Camera initializing failed");
                return false;
            }
            // Allocate Memory
            status = camera.Memory.Allocate();
            if (status != uEye.Defines.Status.SUCCESS)
            {
                SetError("Allocate Memory failed");
                return false;
            }
            camera.EventFrame += Camera_EventFrame;
            // cleanup on any camera error
            if (status != uEye.Defines.Status.SUCCESS && camera.IsOpened)
            {
                camera.Exit();
            }
            return true;
        }

        public bool DisplayLive()
        {
            if (camera == null)
            {
                SetError("Please initialize camera first");
                return false;
            }
            // start capture
            var status = camera.Acquisition.Capture();
            if (status != uEye.Defines.Status.SUCCESS)
            {
                SetError("Starting live video failed");
                return false;
            }
            return true;
        }

        /// <summary>
        /// display camera on control
        /// </summary>
        /// <param name="controlHandler"></param>
        /// <returns></returns>
        public bool DisplayLive(IntPtr controlHandler)
        {
            if (camera == null)
            {
                SetError("Please initialize camera first");
                return false;
            }
            this.displayHandle = controlHandler;
            camera.EventFrame += Camera_EventFrame;
            return true;
        }

        private void Camera_EventFrame(object sender, EventArgs e)
        {
            uEye.Camera camera = sender as uEye.Camera;
            if (camera != null && camera.IsOpened)
            {
                Int32 s32MemID;
                camera.Memory.GetActive(out s32MemID);
                camera.Memory.Lock(s32MemID);
                Bitmap bitmap;
                camera.Memory.ToBitmap(s32MemID, out bitmap);

                if (bitmap != null && bitmap.PixelFormat != System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
                {
                    Graphics graphics = Graphics.FromImage(bitmap);
                    DoDrawing(ref graphics, s32MemID);

                    if (videoControl.GraphicsList != null)
                    {
                        videoControl.GraphicsList.Draw(graphics, videoControl);
                    }
                    //if (Program.EntryForm.PictureBox.LaserFunction)
                    //{
                    //    if (Program.EntryForm.Laser != null)
                    //    {
                    //        Program.EntryForm.Laser.OnPaint(graphics);
                    //    }
                    //}
                    graphics.Dispose();
                    bitmap.Dispose();
                }

                camera.Memory.Unlock(s32MemID);
                camera.Display.Render(s32MemID, uEye.Defines.DisplayRenderMode.FitToWindow);
            }
        }

        private void DoDrawing(ref Graphics graphics, Int32 s32MemID)
        {
            // get image size
            System.Drawing.Rectangle rect;
            camera.Size.AOI.Get(out rect);

            graphics.DrawLine(new Pen(Color.Green, 3), rect.Width / 2, 0, rect.Width / 2, rect.Height);
            graphics.DrawLine(new Pen(Color.Red, 3), 0, rect.Height / 2, rect.Width, rect.Height / 2);
        }

        public bool FreezeLive()
        {
            if (IsInitialized())
            {
                if (camera.Acquisition.Freeze() == uEye.Defines.Status.SUCCESS)
                {
                    return true;
                }
                else
                {
                    SetError("Freeze live error");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool StopLive()
        {
            if (IsInitialized())
            {
                if (camera.Acquisition.Stop() == uEye.Defines.Status.SUCCESS)
                {
                    return true;
                }
                else
                {
                    SetError("Stop live error");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool ExitCamera()
        {
            if (camera != null)
            {
                camera.Exit();
            }
            displayHandle = IntPtr.Zero;
            return true;
        }

        public bool RecordVedio(string aviFileAbsPath)
        {
            if (IsInitialized())
            {
                if (camera.Video.Start(aviFileAbsPath) == uEye.Defines.Status.SUCCESS)
                {
                    return true;
                }
                else
                {
                    SetError("Start record vedio error");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool StopRecordVedio()
        {
            if (IsInitialized())
            {
                if (camera.Video.Stop() == uEye.Defines.Status.SUCCESS)
                {
                    return true;
                }
                else
                {
                    SetError("Stop record vedio error");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool IsInitialized()
        {
            if (camera == null || displayHandle == null || displayHandle == IntPtr.Zero)
            {
                SetError("Please initialize camera first");
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool SaveImage(string path, string imageName)
        {
            if (IsInitialized())
            {
                if (!(path.EndsWith("/") || path.EndsWith("\\")))
                {
                    path += "/";
                }
                if (uEye.Defines.Status.Success == camera.Image.Save(path + imageName))
                {
                    return true;
                }
                else
                {
                    SetError("Save image error");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void SetError(string message)
        {
            LogHelper.GetLogger<IDSCamera>().Error(message);
        }

        public bool IsOpened()
        {
            return camera != null && camera.IsOpened;
        }

        public void SetSize(out Rectangle rect)
        {
            this.camera.Size.AOI.Get(out rect);
        }
    }
}
