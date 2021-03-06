﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Automation.Base.DeviceMeasureClassLibrary;
using Automation.FrameworkExtension.deviceDriver;
using Automation.FrameworkExtension.stateMachine;

namespace DeviceMeasureClassLibrary
{
    public class LaserBase : DeviceBase, ILaser
    {
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
            var lc = new LaserBaseControl();
            lc.LoadDevice(this);
            return lc;
        }

        public double[] Measure()
        {
            return new[] { (double)new Random().Next(100) };
        }
    }
}
