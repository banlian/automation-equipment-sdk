using Automation.FrameworkExtension.motionDriver;

namespace Automation.FrameworkExtension.elementInterfaces
{
    public interface IAxisEx
    {
        string Name { set; get; }
        string Type { set; get; }
        string Description { set; get; }
        string Driver { set; get; }
        IMotionWrapper DriverImpl { get; }


        bool Enable { set; get; }
        double Lead { set; get; }
        int PulsePerRoll { get; set; }
        double Vel { set; get; }
        double Acc { get; set; }
        int Channel { set; get; }


     
        int HomeMode { set; get; }
        int HomeDir { set; get; }
        double HomeCurve { set; get; }
        double HomeAcc { set; get; }
        double HomeVm { set; get; }

        string Error { get; set; }


        double ToMm(int pls);
        int ToPls(double mm);
        string ToString();
    }
}