using System;
using System.Linq;
using Automation.FrameworkExtension.elementsInterfaces;
using Automation.FrameworkExtension.motionDriver;
using Automation.FrameworkExtension.stateMachine;

namespace Automation.FrameworkExtension.elements
{

    public enum VioType
    {
        Normal,
    }


    public class VioEx :  IVioEx, IElement
    {
        public VioEx()
        {
        }


        public int Id { get; set; }
        public string Name { get; set; } = nameof(VioEx);
        public VioType Type { get; set; }
        public string Description { get; set; } = nameof(VioEx);
        public bool Enable { get; set; }
        public string DriverName { get; set; } = nameof(DriverName);
        public int Port { get; set; }
        public MotionCardWrapper Driver { get; protected set; }

        public void Build(StateMachine machine)
        {
            Driver = machine.MotionExs.Values.First(m => m.Name == DriverName);
        }

        public override string ToString()
        {
            return $"{Name} {Type} {Description} {DriverName} {Port} {Enable}";
        }

        public string Export()
        {
            return $"{Name} {Type} {Description} {DriverName} {Port} {Enable}";
        }

        public void Import(string line, StateMachine machine)
        {
            var data = line.Split(' ');

            int i = 0;
            int id = int.Parse(data[i++]);


            Name = data[i++];
            Type = (VioType)Enum.Parse(typeof(VioType), data[i++]);
            Description = data[i++];
            DriverName = data[i++];
            Port = int.Parse(data[i++]);
            Enable = bool.Parse(data[i++]);


            Driver = machine.MotionExs.Values.FirstOrDefault(m => m.Name == DriverName);
            if (Driver == null)
            {
                throw new FormatException($"Parsing Error: Not Found Motion Driver Instance {line} {DriverName}");
            }


            if (machine.VioExs.ContainsKey(id))
            {
                return;
            }
            machine.VioExs.Add(id, this);
        }
    }
}