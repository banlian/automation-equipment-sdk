using Lead.Detect.Element;
using Lead.Detect.FrameworkExtension.motionDriver;

namespace Lead.Detect.FrameworkExtension.elementBase
{
    public interface IAxisEx
    {
        string Name { set; get; }
        EleAxisType Type { set; get; }
        string Description { set; get; }


        string Driver { set; get; }
        IMotionWrapper DriverCard { get; }


        bool Enable { set; get; }
        double AxisLead { set; get; }
        int AxisPlsPerRoll { get; set; }
        double AxisSpeed { set; get; }
        double AxisAcc { get; set; }
        int AxisChannel { set; get; }
        bool AxisChannelEnable { set; get; }


        int PosCheckDI { set; get; }
        bool PosCheckDIEnable { set; get; }
        int NegCheckDI { set; get; }
        bool NegCheckDIEnable { set; get; }
        int OriginCheckDI { set; get; }
        bool OriginCheckDIEnable { set; get; }


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