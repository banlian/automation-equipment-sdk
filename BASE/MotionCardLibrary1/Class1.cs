using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.FrameworkExtension.motionDriver;

namespace MotionCardLibrary1
{
    public class MotionCard1 : IMotionCard
    {
        public int DeviceID { get; set; }
        public int GetDo(int index, int i, int port, out int status)
        {
            throw new NotImplementedException();
        }

        public int SetDo(int index, int i, int port, int status)
        {
            throw new NotImplementedException();
        }

        public int GetDi(int index, int i, int port, out int status)
        {
            throw new NotImplementedException();
        }
    }
}
