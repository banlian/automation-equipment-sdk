using Automation.FrameworkExtension.elements;
using Automation.FrameworkExtension.motionDriver;

namespace Automation.FrameworkExtension.elementsInterfaces
{
    public interface IDoEx : IElement
    {
        DoType Type { set; get; }
        string Description { set; get; }
        bool Enable { get; set; }
        int Port { set; get; }



        string DriverName { set; get; }
        MotionCardWrapper Driver { get; }
    }
}