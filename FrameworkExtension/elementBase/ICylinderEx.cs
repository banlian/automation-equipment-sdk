using Lead.Detect.Element;
using Lead.Detect.FrameworkExtension.motionDriver;

namespace Lead.Detect.FrameworkExtension.elementBase
{
    public interface ICylinderEx
    {
        string Name { set; get; }
        EleDoType Type { set; get; }
        string Description { set; get; }

        string Driver { set; get; }
        IMotionWrapper DriverCard { get; }


        bool Enable { set; get; }
        int Delay { get; set; }
        int DiOrg { set; get; }
        int DiWork { set; get; }
        int DoOrg { set; get; }
        int DoWork { set; get; }
        string ToString();
    }
}