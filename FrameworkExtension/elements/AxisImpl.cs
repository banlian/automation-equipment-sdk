using Automation.FrameworkExtension.elementInterfaces;
using Automation.FrameworkExtension.motionDriver;

namespace Automation.FrameworkExtension.elements
{
    public class AxisImpl : IAxisEx
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Driver { get; set; }
        public IMotionWrapper DriverImpl { get; }
        public bool Enable { get; set; }
        public double Lead { get; set; }
        public int PulsePerRoll { get; set; }
        public double Vel { get; set; }
        public double Acc { get; set; }
        public int Channel { get; set; }
        public int HomeMode { get; set; }
        public int HomeDir { get; set; }
        public double HomeCurve { get; set; }
        public double HomeAcc { get; set; }
        public double HomeVm { get; set; }
        public string Error { get; set; }
        public double ToMm(int pls)
        {
            return (double)pls / PulsePerRoll * Lead;
        }

        public int ToPls(double mm)
        {
            return (int) (mm / Lead * PulsePerRoll);
        }
    }
}