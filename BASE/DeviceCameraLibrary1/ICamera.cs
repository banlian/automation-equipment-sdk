using System.Drawing;
using Automation.FrameworkExtension.deviceDriver;

namespace DeviceCameraLibrary1
{
    public interface ICamera : IDevice
    {
        Bitmap Snap();
    }
}