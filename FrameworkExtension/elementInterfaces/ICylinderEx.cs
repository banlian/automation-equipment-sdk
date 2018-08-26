using Automation.FrameworkExtension.motionDriver;

namespace Automation.FrameworkExtension.elementInterfaces
{
    public interface ICylinderEx
    {
        string Name { set; get; }
        string Type { set; get; }
        string Description { set; get; }

        string Driver { set; get; }
        IMotionWrapper DriverImpl { get; }


        bool Enable { set; get; }
        int Delay { get; set; }
        int DiOrg { set; get; }
        int DiWork { set; get; }
        int DoOrg { set; get; }
        int DoWork { set; get; }
        string ToString();
    }
}