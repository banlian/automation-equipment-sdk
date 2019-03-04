using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Automation.FrameworkExtension.deviceDriver;
using Automation.FrameworkExtension.elementsInterfaces;
using Automation.FrameworkExtension.stateMachine;

namespace DeviceCameraLibrary1
{
    public class ICamera : IDevice
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Export()
        {
            throw new NotImplementedException();
        }

        public void Import(string line, StateMachine machine)
        {
            throw new NotImplementedException();
        }

        public string Vendor { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public string ConfigFilePath { get; set; }
        public bool Initialize()
        {
            throw new NotImplementedException();
        }

        public bool Terminate()
        {
            throw new NotImplementedException();
        }

        public UserControl CreateDeviceControl()
        {
            return null;
        }
    }
}
