using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CII.LAR.UI
{
    public partial class VideoPropertyForm : Form
    {
        public VideoPropertyForm()
        {
            InitializeComponent();
            //TestCamera(Program.SysConfig.DeviceMoniker);
        }

        private void TestCamera(string deviceMoniker)
        {
            var videoCaptureDevice = new VideoCaptureDevice(deviceMoniker);
            int value = 1;
            CameraControlFlags flag = CameraControlFlags.Manual;
            videoCaptureDevice.GetCameraProperty(CameraControlProperty.Zoom, out value, out flag);
            int minValueE = 0, maxValueE = 0, stepSizeE = 0, defaultValueE = 0;
            CameraControlFlags controlFlagsE = CameraControlFlags.Auto;
            videoCaptureDevice.GetCameraPropertyRange(CameraControlProperty.Exposure, out minValueE, out maxValueE, 
                out stepSizeE, out defaultValueE, out controlFlagsE);

            int minValueF = 0, maxValueF = 0, stepSizeF = 0, defaultValueF = 0;
            CameraControlFlags controlFlagsF = CameraControlFlags.Manual;
            videoCaptureDevice.GetCameraPropertyRange(CameraControlProperty.Focus, out minValueF, out maxValueF,
                out stepSizeF, out defaultValueF, out controlFlagsF);

            int minValueI = 0, maxValueI = 0, stepSizeI = 0, defaultValueI = 0;
            CameraControlFlags controlFlagsI = CameraControlFlags.Manual;
            videoCaptureDevice.GetCameraPropertyRange(CameraControlProperty.Iris, out minValueI, out maxValueI,
                out stepSizeI, out defaultValueI, out controlFlagsI);

            int minValueP = 0, maxValueP = 0, stepSizeP = 0, defaultValueP = 0;
            CameraControlFlags controlFlagsP = CameraControlFlags.Manual;
            videoCaptureDevice.GetCameraPropertyRange(CameraControlProperty.Pan, out minValueP, out maxValueP,
                out stepSizeP, out defaultValueP, out controlFlagsP);

            int minValueR = 0, maxValueR = 0, stepSizeR = 0, defaultValueR = 0;
            CameraControlFlags controlFlagsR = CameraControlFlags.Manual;
            videoCaptureDevice.GetCameraPropertyRange(CameraControlProperty.Roll, out minValueR, out maxValueR,
                out stepSizeR, out defaultValueR, out controlFlagsR);

            int minValueT = 0, maxValueT = 0, stepSizeT = 0, defaultValueT = 0;
            CameraControlFlags controlFlagsT = CameraControlFlags.Manual;
            videoCaptureDevice.GetCameraPropertyRange(CameraControlProperty.Tilt, out minValueT, out maxValueT,
                out stepSizeT, out defaultValueT, out controlFlagsT);

            int minValueZ = 0, maxValueZ = 0, stepSizeZ = 0, defaultValueZ = 0;
            CameraControlFlags controlFlagsZ = CameraControlFlags.Manual;
            videoCaptureDevice.GetCameraPropertyRange(CameraControlProperty.Zoom, out minValueZ, out maxValueZ,
                out stepSizeZ, out defaultValueZ, out controlFlagsZ);

        }
    }
}
