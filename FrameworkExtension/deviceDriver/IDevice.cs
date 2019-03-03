using Automation.FrameworkExtension.elementsInterfaces;

namespace Automation.FrameworkExtension.deviceDriver
{
    public interface IDevice : IElement
    {
        string Vendor { get; set; }

        string Version { get; set; }

        string Description { get; set; }

        string ConfigFilePath { get; set; }

        bool Initialize();

        bool Terminate();
    }
}
