using System.Collections.Generic;
using Automation.FrameworkExtension.elementInterfaces;
using Automation.FrameworkExtension.platforms.platformBase;
using Automation.FrameworkExtension.platforms.points;
using Automation.FrameworkExtension.stateMachine;

namespace Automation.FrameworkExtension.platforms.xyzPlatform
{
    public class PlatformXyz : PlatformEx<PosXYZ>
    {
        public IAxisEx AxisX
        {
            get
            {
                return Axis[0];
            }
        }
        public IAxisEx AxisY
        {
            get
            {
                return Axis[1];
            }
        }
        public IAxisEx AxisZ
        {
            get
            {
                return Axis[2];
            }
        }


        public PlatformXyz(string name, IAxisEx[] axis, StationTask task, List<PosXYZ> positions) : base(name, axis, task, positions)
        {

        }

    }
}
