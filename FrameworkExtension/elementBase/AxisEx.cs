using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lead.Detect.Base;
using Lead.Detect.Element;
using Lead.Detect.FrameworkExtension.motionDriver;
using Lead.Detect.Interfaces.Dev.DeviceInterfaces;

namespace Lead.Detect.FrameworkExtension.elementBase
{
    public class AxisEx : EleAxis, IAxisEx
    {
        public AxisEx()
        {

        }

        public AxisEx(EleAxis axis)
        {
            var props = axis.GetType().GetProperties();
            foreach (var p in props)
            {
                p.SetValue(this, p.GetValue(axis));
            }

            DriverCard = new MotionCardWrapper((IMotionCard)DevPrimsManager.Instance.GetPrimByName(Driver));
        }
        public IMotionWrapper DriverCard { get; }
    }
}
