using System;
using System.Linq;
using Automation.FrameworkExtension.elementsInterfaces;
using Automation.FrameworkExtension.motionDriver;
using Automation.FrameworkExtension.stateMachine;

namespace Automation.FrameworkExtension.elements
{

    public enum AxisType
    {
        Limit2_Org,
        Limit2,
        Org,

    }



    public class AxisEx : IAxisEx, IElement
    {
        public AxisEx()
        {
        }


        public int Id { get; set; }
        public string Name { get; set; } = nameof(AxisEx);
        public AxisType Type { get; set; }
        public string Description { get; set; } = nameof(AxisEx);
        public bool Enable { get; set; }
        public string DriverName { get; set; } = nameof(DriverName);
        public MotionCardWrapper Driver { get; set; }

        public double AxisLead { get; set; }
        public int AxisPlsPerRoll { get; set; }
        public double AxisSpeed { get; set; }
        public double AxisAcc { get; set; }
        public int AxisChannel { get; set; }



        public int HomeOrder { get; set; }
        public int HomeMode { get; set; }
        public int HomeDir { get; set; }
        public double HomeCurve { get; set; }
        public double HomeAcc { get; set; }
        public double HomeVm { get; set; }
        public string Error { get; set; }


        public void Build(StateMachine machine)
        {
            Driver = machine.MotionExs.Values.First(m => m.Name == DriverName);
        }

        public double ToMm(int pls)
        {
            return (double)pls / AxisPlsPerRoll * AxisLead;
        }

        public int ToPls(double mm)
        {
            return Convert.ToInt32(mm / AxisLead * AxisPlsPerRoll);
        }

        public override string ToString()
        {
            return $"{Name} {Type} {Description} {DriverName} {AxisChannel} {Enable} {AxisLead:F2} {AxisPlsPerRoll} {AxisSpeed:F2} {AxisAcc:F2} {HomeOrder} {HomeDir} {HomeMode} {HomeVm:F2}";
        }

        public string Export()
        {
            return $"{Name} {Type} {Description} {DriverName} {AxisChannel} {Enable} {AxisLead:F2} {AxisPlsPerRoll} {AxisSpeed:F2} {AxisAcc:F2} {HomeOrder} {HomeDir} {HomeMode} {HomeVm:F2}";
        }

        public void Import(string line, StateMachine machine)
        {
            var data = line.Split(' ');

            int i = 0;
            int id = int.Parse(data[i++]);
            Id = id;
            Name = data[i++];
            Type = (AxisType)Enum.Parse(typeof(AxisType), data[i++]);
            Description = data[i++];
            DriverName = data[i++];
            AxisChannel = int.Parse(data[i++]);
            Enable = bool.Parse(data[i++]);

            AxisLead = double.Parse(data[i++]);
            AxisPlsPerRoll = int.Parse(data[i++]);
            AxisSpeed = double.Parse(data[i++]);
            AxisAcc = double.Parse(data[i++]);

            HomeOrder = int.Parse(data[i++]);
            HomeDir = int.Parse(data[i++]);
            HomeMode = int.Parse(data[i++]);
            HomeVm = double.Parse(data[i++]);

            Driver = machine.MotionExs.Values.FirstOrDefault(m => m.Name == DriverName);
            if (Driver == null)
            {
                throw new FormatException($"Parsing Error: Not Found Motion Driver Instance {line} {Driver}");
            }


            if (machine.AxisExs.ContainsKey(id))
            {
                return;
            }
            machine.AxisExs.Add(id, this);
        }

    }



}