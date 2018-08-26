using Lead.Detect.Element;
using Lead.Detect.FrameworkExtension.motionDriver;

namespace Lead.Detect.FrameworkExtension.elementBase
{
    public interface IDoEx
    {
        string Name { set; get; }
        EleDoType Type { set; get; }
        string Description { set; get; }


        string Driver { set; get; }
        IMotionWrapper DriverCard { get; }


        int Port { set; get; }
        string ToString();
    }
}