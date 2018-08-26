namespace Automation.FrameworkExtension.motionDriver
{

    /// <summary>
    /// motion card must be implemented as this interface
    /// </summary>
    public interface IMotionCard
    {
        int DeviceID { get; set; }
        int GetDo(int index, int i, int port, out int status);
        int SetDo(int index, int i, int port, int status);
        int GetDi(int index, int i, int port, out int status);
    }
}