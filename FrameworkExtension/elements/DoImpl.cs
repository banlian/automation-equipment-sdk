using Automation.FrameworkExtension.elementInterfaces;
using Automation.FrameworkExtension.motionDriver;

namespace Automation.FrameworkExtension.elements
{
    public class DoImpl : IDoEx
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Driver { get; set; }
        public IMotionWrapper DriverImpl { get; }
        public int Port { get; set; }
    }
}