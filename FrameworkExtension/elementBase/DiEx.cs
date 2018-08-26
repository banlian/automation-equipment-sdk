using System.Windows.Forms.VisualStyles;
using Lead.Detect.Base;
using Lead.Detect.Element;
using Lead.Detect.FrameworkExtension.motionDriver;
using Lead.Detect.Interfaces.Dev;
using Lead.Detect.Interfaces.Dev.DeviceInterfaces;

namespace Lead.Detect.FrameworkExtension.elementBase
{
    public class DiEx : EleDi, IDiEx
    {
        public DiEx()
        {

        }
        public DiEx(EleDi eleDi)
        {
            var props = eleDi.GetType().GetProperties();
            foreach (var p in props)
            {
                p.SetValue(this, p.GetValue(eleDi));
            }

            DriverCard = new MotionCardWrapper((IMotionCard) DevPrimsManager.Instance.GetPrimByName(Driver));
        }

        public IMotionWrapper DriverCard { get; }





    }
}
