namespace Automation.FrameworkExtension.deviceDriver
{
    public interface IDeviceControl<T> where T : IDevice
    {
        void LoadDevice(T device);

        void UserActivate();

        void UserDeactivate();
    }
}
