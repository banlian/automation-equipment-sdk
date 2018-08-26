using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.FrameworkExtension.elementInterfaces;
using Automation.FrameworkExtension.motionDriver;

namespace Automation.FrameworkExtension.elements
{
   public class DiImpl : IDiEx
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Driver { get; set; }
        public IMotionWrapper DriverImpl { get; }
        public bool Enable { get; set; }
        public int Port { get; set; }
    }
}
