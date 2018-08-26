using Automation.FrameworkExtension.elementInterfaces;
using Automation.FrameworkExtension.motionDriver;

namespace Automation.FrameworkExtension.elements
{
    public class  CyImpl : ICylinderEx
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Driver { get; set; }
        public IMotionWrapper DriverImpl { get; }
        public bool Enable { get; set; }
        public int Delay { get; set; }
        public int DiOrg { get; set; }
        public int DiWork { get; set; }
        public int DoOrg { get; set; }
        public int DoWork { get; set; }
    }
}