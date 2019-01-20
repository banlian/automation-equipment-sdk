using System;
using System.Linq;
using Automation.FrameworkExtension.elementsInterfaces;
using Automation.FrameworkExtension.motionDriver;
using Automation.FrameworkExtension.stateMachine;

namespace Automation.FrameworkExtension.elements
{

    public enum DiType
    {
        NormalOpen,
        NormalClose,
    }

    public class DiEx : IDiEx, IElement
    {
        public DiEx()
        {
        }


        public int Id { get; set; } = 0;
        public string Name { get; set; } = nameof(DiEx);
        public DiType Type { get; set; }
        public string Description { get; set; } = nameof(DiEx);
        public bool Enable { get; set; }
        public int Port { get; set; }

        public string DriverName { get; set; } = nameof(DriverName);
        public MotionCardWrapper Driver { get; set; }

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
            Id = id;
            Name = data[i++];
            Type = (DiType)Enum.Parse(typeof(DiType), data[i++]);
            Description = data[i++];
            DriverName = data[i++];
            Port = int.Parse(data[i++]);
            Enable = bool.Parse(data[i++]);
            Driver = machine.MotionExs.Values.FirstOrDefault(m => m.Name == DriverName);
            if (Driver == null)
            {
                throw new FormatException($"Parsing Error: Not Found Motion Driver Instance {line} {DriverName}");
            }

            if (machine.DiExs.ContainsKey(id))
            {
                return;
            }
            machine.DiExs.Add(id, this);
        }

        public static void Import(string type, string line, StateMachine machine)
        {
            DiEx di = new DiEx();
            di.Import(line, machine);
            di = machine.Find<DiEx>(di.Name);

            switch (type)
            {
                case "ESTOP":
                    {
                        machine.DiEstop.Add(machine.DiEstop.Count + 1, di);
                    }
                    break;
                case "START":
                    {
                        machine.DiStart.Add(machine.DiStart.Count + 1, di);
                    }
                    break;
                case "STOP":
                    {
                        machine.DiStop.Add(machine.DiStop.Count + 1, di);
                    }
                    break;
                case "RESET":
                    {
                        machine.DiReset.Add(machine.DiReset.Count + 1, di);
                    }
                    break;
                default:
                    throw new Exception($"DI IMPORT ERROR TYPE {type}");
            }
        }
    }
}