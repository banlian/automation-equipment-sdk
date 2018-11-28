using System;
using System.Linq;
using Automation.FrameworkExtension.elementsInterfaces;
using Automation.FrameworkExtension.motionDriver;
using Automation.FrameworkExtension.stateMachine;

namespace Automation.FrameworkExtension.elements
{

    public enum CylinderType
    {
        DI2_DO2,
        DI2_DO1,
    }


    public class CylinderEx : ICylinderEx, IElement
    {
        public CylinderEx()
        {

        }

        public int Id { get; set; }
        public string Name { get; set; } = nameof(CylinderEx);
        public CylinderType Type { get; set; }
        public string Description { get; set; } = nameof(CylinderEx);
        public bool Enable { get; set; }

        public string DriverName1 { get; set; } = nameof(DriverName1);
        public int DiOrg { get; set; }
        public int DiWork { get; set; }
        public string DriverName2 { get; set; } = nameof(DriverName2);
        public int DoOrg { get; set; }
        public int DoWork { get; set; }

      
     
        public MotionCardWrapper Driver1 { get; set; }
        public MotionCardWrapper Driver2 { get; set; }



        public void Build(StateMachine machine)
        {
            Driver1 = machine.MotionExs.Values.First(m => m.Name == DriverName1);
        }

        private IDiEx[] _diexs;

        public IDiEx[] GetDiExs()
        {
            if (_diexs == null)
            {
                _diexs = new[]
                {
                    new DiEx()
                    {
                        Name = Name + "0",
                        Description = Description + "0",
                        DriverName = DriverName1,
                        Driver = Driver1,
                        Enable = Enable,
                        Port = DiOrg,
                        Type = DiType.NormalOpen
                    },
                    new DiEx()
                    {
                        Name = Name + "1",
                        Description = Description + "1",
                        DriverName = DriverName1,
                        Driver = Driver1,
                        Enable = Enable,
                        Port = DiWork,
                        Type =  DiType.NormalOpen
                    },
                };
            }

            return _diexs;
        }


        private IDoEx[] _doexs;

        public IDoEx[] GetDoExs()
        {
            if (_doexs == null)
            {
                _doexs = new[]
                {
                    new DoEx()
                    {
                        Name = Name + "0",
                        Description = Description + "0",
                        DriverName = DriverName1,
                        Driver = Driver2,
                        Enable = Enable,
                        Port = DoOrg,
                        Type = DoType.Normal
                    },
                    new DoEx()
                    {
                        Name = Name + "1",
                        Description = Description + "1",
                        DriverName = DriverName1,
                        Driver = Driver2,
                        Enable = Enable,
                        Port = DoWork,
                        Type = DoType.Normal
                    },
                };
            }

            return _doexs;
        }


        public override string ToString()
        {
            return $"{Name} {Type} {Description} {DriverName1} {DiOrg} {DiWork} {DoOrg} {DoWork} {Enable}";
        }



        public string Export()
        {
            if (DriverName1 == DriverName2)
            {
                return $"{Name} {Type} {Description} {DriverName1} {DiOrg} {DiWork} {DoOrg} {DoWork} {Enable}";
            }
            else
            {
                return $"{Name} {Type} {Description} {DriverName1} {DiOrg} {DiWork} {DriverName2} {DoOrg} {DoWork} {Enable}";
            }
        }

        public void Import(string line, StateMachine machine)
        {
            var data = line.Split(' ');

            int i = 0;

            int id = int.Parse(data[i++]);
            Name = data[i++];
            Type = (CylinderType)Enum.Parse(typeof(CylinderType), data[i++]);
            Description = data[i++];

            if (data.Length == 10)
            {
                DriverName1 = data[i++];
                DriverName2 = DriverName1;
                DiOrg = int.Parse(data[i++]);
                DiWork = int.Parse(data[i++]);
                DoOrg = int.Parse(data[i++]);
                DoWork = int.Parse(data[i++]);
                Enable = bool.Parse(data[i++]);
            }
            else if (data.Length == 11)
            {
                DriverName1 = data[i++];
                DiOrg = int.Parse(data[i++]);
                DiWork = int.Parse(data[i++]);
                DriverName2 = data[i++];
                DoOrg = int.Parse(data[i++]);
                DoWork = int.Parse(data[i++]);
                Enable = bool.Parse(data[i++]);
            }
            else
            {
                throw new Exception($"Cylinder {Name} Format Error");
            }



            Driver1 = machine.MotionExs.Values.FirstOrDefault(m => m.Name == DriverName1);
            if (Driver1 == null)
            {
                throw new FormatException($"Parsing Error: Not Found Motion Driver Instance {line} {DriverName1}");
            }


            Driver2 = machine.MotionExs.Values.FirstOrDefault(m => m.Name == DriverName2);
            if (Driver2 == null)
            {
                throw new FormatException($"Parsing Error: Not Found Motion Driver Instance {line} {DriverName1}");
            }


            if (machine.CylinderExs.ContainsKey(id))
            {
                return;
            }
            machine.CylinderExs.Add(id, this);
        }
    }
}