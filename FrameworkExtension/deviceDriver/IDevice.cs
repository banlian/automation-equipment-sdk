using System.Windows.Forms;
using Automation.FrameworkExtension.elementsInterfaces;
using Automation.FrameworkExtension.stateMachine;

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

        UserControl CreateDeviceControl();
    }

    public class DeviceBase : IDevice
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Vendor { get; set; }
        public string Version { get; set; }
        public string ConfigFilePath { get; set; }
        public virtual bool Initialize()
        {
            return true;
        }

        public virtual bool Terminate()
        {
            return true;
        }

        public virtual UserControl CreateDeviceControl()
        {
            return null;
        }

        public string Export()
        {
            return $"{Name} {Id} {GetType().Name} {Description} {Vendor} {Version} {ConfigFilePath}";
        }

        public void Import(string line, StateMachine machine)
        {
            var data = line.Split(' ');

            var i = 0;
            var index = int.Parse(data[i++]);
            var name = data[i++];
            var id = int.Parse(data[i++]);
            var type = data[i++];
            var config = data[i++];

            Id = id;
            Name = name;
            Description = type;
            ConfigFilePath = config;

            if (!machine.Devices.ContainsKey(id))
            {
                machine.Devices.Add(id, this);
            }
        }
    }
}
