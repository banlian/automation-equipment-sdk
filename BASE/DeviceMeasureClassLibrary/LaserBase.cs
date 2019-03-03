using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.FrameworkExtension.stateMachine;

namespace DeviceMeasureClassLibrary
{
    public class LaserBase : ILaser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Vendor { get; set; }
        public string Version { get; set; }
        public string ConfigFilePath { get; set; }

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

        public bool Initialize()
        {
            return true;
        }

        public bool Terminate()
        {
            return true;
        }

        public double[] Measure()
        {
            return new[] { (double)new Random().Next(100) };
        }
    }
}
