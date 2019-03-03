namespace Automation.FrameworkExtension.deviceDriver
{
    public interface IDeviceControl
    {
        void LoadDevice(IDevice device);


        void UserActivate();

        void UserDeactivate();
    }
}
