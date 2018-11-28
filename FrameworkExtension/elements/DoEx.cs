using System;
using System.Linq;
using Automation.FrameworkExtension.elementsInterfaces;
using Automation.FrameworkExtension.motionDriver;
using Automation.FrameworkExtension.stateMachine;

namespace Automation.FrameworkExtension.elements
{

    public enum DoType
    {
        Normal,

    }

    public class DoEx : IDoEx, IElement
    {
        public DoEx()
        {
        }


        public int Id { get; set; }
        public string Name { get; set; } = nameof(DoEx);
        public DoType Type { get; set; }
        public string Description { get; set; } = nameof(DoEx);
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

            Name = data[i++];
            Type = (DoType)Enum.Parse(typeof(DoType), data[i++]);
            Description = data[i++];
            DriverName = data[i++];
            Port = int.Parse(data[i++]);
            Enable = bool.Parse(data[i++]);
            Driver = machine.MotionExs.Values.FirstOrDefault(m => m.Name == DriverName);
            if (Driver == null)
            {
                throw new FormatException($"Parsing Error: Not Found Motion Driver Instance {line} {DriverName}");
            }

            if (machine.DoExs.ContainsKey(id))
            {
                return;
            }
            machine.DoExs.Add(id, this);
        }

        public static void Import(string type, string line, StateMachine machine)
        {
            DoEx doex = new DoEx();
            doex.Import(line, machine);
            doex = machine.Find<DoEx>(doex.Name);

            switch (type)
            {
                case "LIGHTGREEN":
                    {
                        machine.DoLightGreen.Add(machine.DoLightGreen.Count + 1, doex);
                    }
                    break;
                case "LIGHTYELLOW":
                    {
                        machine.DoLightYellow.Add(machine.DoLightYellow.Count + 1, doex);
                    }
                    break;
                case "LIGHTRED":
                    {
                        machine.DoLightRed.Add(machine.DoLightRed.Count + 1, doex);
                    }
                    break;
                case "BUZZER":
                    {
                        machine.DoBuzzer.Add(machine.DoBuzzer.Count + 1, doex);
                    }
                    break;
                default:
                    throw new Exception($"DO IMPORT ERROR TYPE {type}");
            }
        }
    }
}