using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.Opertion
{
    public interface Camera
    {
        bool InitCamera(int s32Cam);

        bool ExitCamera();

        bool DisplayLive(IntPtr controlHandler);

        bool FreezeLive();

        bool StopLive();

        bool RecordVedio(string aviFileAbsPath);

        bool StopRecordVedio();

        bool SaveImage(string path, string imageName);
    }
}
