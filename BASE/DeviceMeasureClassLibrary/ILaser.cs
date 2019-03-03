using Automation.FrameworkExtension.deviceDriver;
using Automation.FrameworkExtension.elementsInterfaces;

namespace DeviceMeasureClassLibrary
{
    public interface ILaser : IDevice
    {
        double[] Measure();
    }
}