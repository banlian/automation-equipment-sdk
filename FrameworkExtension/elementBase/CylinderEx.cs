using Lead.Detect.Base;
using Lead.Detect.Element;
using Lead.Detect.FrameworkExtension.motionDriver;
using Lead.Detect.Interfaces.Dev;
using Lead.Detect.Interfaces.Dev.DeviceInterfaces;

namespace Lead.Detect.FrameworkExtension.elementBase
{
    public class CylinderEx : EleCylinder, ICylinderEx
    {
        public CylinderEx()
        {
            
        }

        public CylinderEx(EleCylinder cy)
        {
            var props = cy.GetType().GetProperties();
            foreach (var p in props)
            {
                p.SetValue(this, p.GetValue(cy));
            }

            DriverCard = new MotionCardWrapper((IMotionCard)DevPrimsManager.Instance.GetPrimByName(Driver));
        }
        public IMotionWrapper DriverCard { get; }
    }
}