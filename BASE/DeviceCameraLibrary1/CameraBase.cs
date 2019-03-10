using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Automation.Base.DeviceCameraLibrary1;
using Automation.FrameworkExtension.deviceDriver;
using Automation.FrameworkExtension.elementsInterfaces;
using Automation.FrameworkExtension.stateMachine;

namespace DeviceCameraLibrary1
{
    public class CameraBase : DeviceBase, ICamera
    {
        public Bitmap Snap()
        {
            return new Bitmap(1024, 768);
        }

        public override bool Initialize()
        {
            return true;
        }

        public override bool Terminate()
        {
            return true;
        }

        public override UserControl CreateDeviceControl()
        {
            var control = new CameraBaseControl();
            control.LoadDevice(this);
            return control;
        }
    }
}