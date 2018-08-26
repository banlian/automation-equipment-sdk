using Lead.Detect.Base;
using Lead.Detect.Element;
using Lead.Detect.FrameworkExtension.motionDriver;
using Lead.Detect.Interfaces.Dev;
using Lead.Detect.Interfaces.Dev.DeviceInterfaces;

namespace Lead.Detect.FrameworkExtension.elementBase
{
    public class DoEx : EleDo, IDoEx
    {
        public DoEx()
        {

        }
        public DoEx(EleDo eledo)
        {
            var props = eledo.GetType().GetProperties();
            foreach (var p in props)
            {
                p.SetValue(this, p.GetValue(eledo));
            }

            DriverCard = new MotionCardWrapper((IMotionCard)DevPrimsManager.Instance.GetPrimByName(Driver));
        }

        public IMotionWrapper DriverCard { get; }
    }
}