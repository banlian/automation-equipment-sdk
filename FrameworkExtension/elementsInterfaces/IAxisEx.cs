using Automation.FrameworkExtension.elements;
using Automation.FrameworkExtension.motionDriver;

namespace Automation.FrameworkExtension.elementsInterfaces
{
    public interface IAxisEx : IElement
    {
        AxisType Type { get; set; }
        string Description { get; set; }
        bool Enable { get; set; }

        string DriverName { get; set; }
        MotionCardWrapper Driver { get; }
        double AxisLead { get; set; }
        int AxisPlsPerRoll { get; set; }
        double AxisSpeed { get; set; }
        double AxisAcc { get; set; }
        int AxisChannel { get; set; }



        int HomeOrder { get; set; }
        int HomeMode { get; set; }
        int HomeDir { get; set; }
        double HomeCurve { get; set; }
        double HomeAcc { get; set; }
        double HomeVm { get; set; }


        string Error { get; set; }


        double ToMm(int pls);
        int ToPls(double mm);
        string ToString();
    }
}