using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Automation.FrameworkExtension.deviceDriver;
using Automation.FrameworkExtension.elementsInterfaces;

namespace Automation.Base.DeviceMeasureClassLibrary
{
    public partial class LaserBaseControl : UserControl, IDeviceControl
    {
        public LaserBaseControl()
        {
            InitializeComponent();
        }

        public void LoadDevice(IDevice device)
        {
            groupBoxDev.Text = device.Name;
        }

        public void UserActivate()
        {
            throw new NotImplementedException();
        }

        public void UserDeactivate()
        {
            throw new NotImplementedException();
        }
    }
}
