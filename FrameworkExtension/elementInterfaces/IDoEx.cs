using Automation.FrameworkExtension.motionDriver;

namespace Automation.FrameworkExtension.elementInterfaces
{
    public interface IDoEx
    {
        string Name { set; get; }
        string Type { set; get; }
        string Description { set; get; }


        string Driver { set; get; }
        IMotionWrapper DriverImpl { get; }


        int Port { set; get; }
        string ToString();
    }
}