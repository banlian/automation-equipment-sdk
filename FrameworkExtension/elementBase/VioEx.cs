using Lead.Detect.Base;
using Lead.Detect.Element;
using Lead.Detect.FrameworkExtension.motionDriver;
using Lead.Detect.Interfaces.Dev;
using Lead.Detect.Interfaces.Dev.DeviceInterfaces;

namespace Lead.Detect.FrameworkExtension.elementBase
{
    public class VioEx : EleVio, IVioEx
    {
        public VioEx()
        {
            
        }

        public VioEx(EleVio vio)
        {
            var props = vio.GetType().GetProperties();
            foreach (var p in props)
            {
                p.SetValue(this, p.GetValue(vio));
            }

            DriverCard = new MotionCardWrapper((IMotionCard)DevPrimsManager.Instance.GetPrimByName(Driver));
        }

        public IMotionWrapper DriverCard { get; }
    }
}