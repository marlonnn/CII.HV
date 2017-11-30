using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Opertion
{
    /// <summary>
    /// Camera manager: IDSCamera、CameraSizeControl
    /// Author: Zhong Wen 2017/11/24
    /// </summary>
    public class CameraManager
    {
        private IDSCamera idsCamera;
        public IDSCamera IDSCamera
        {
            get { return this.idsCamera; }
            private set { this.idsCamera = value; }
        }

        private CameraSizeControl cameraSizeControl;
        public CameraSizeControl CameraSizeControl
        {
            get { return this.cameraSizeControl; }
            set { this.cameraSizeControl = value; }
        }

        public static CameraManager cameraManager;

        public CameraManager()
        {
            IDSCamera = new IDSCamera();
            cameraSizeControl = new CameraSizeControl(IDSCamera.uEyeCamera);
        }

        public static CameraManager GetCameraManager()
        {
            if (cameraManager == null)
            {
                cameraManager = new CameraManager();
            }
            return cameraManager;
        }

    }
}
