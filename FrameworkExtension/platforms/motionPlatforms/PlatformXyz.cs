using System;
using System.Collections.Generic;
using System.Linq;
using Automation.FrameworkExtension.elementsInterfaces;
using Automation.FrameworkExtension.stateMachine;

namespace Automation.FrameworkExtension.platforms.motionPlatforms
{
    public class PlatformXyz : PlatformEx
    {
        public PlatformXyz()
        {
            Axis = new IAxisEx[3];
        }

        public PlatformXyz(string name, IAxisEx[] axis, StationTask task, List<PosXYZ> positions) : base(name, axis, task, positions.Cast<IPlatformPos>().ToList())
        {

        }
        public PlatformXyz(string name, IAxisEx[] axis, StationTask task, List<IPlatformPos> positions) : base(name, axis, task, positions)
        {

        }
        public override Type PosType
        {
            get { return typeof(PosXYZ); }
        }

        public override void TeachPos(string name)
        {
            var curpos = new PosXYZ(CurPos);

            if (Positions.Exists(p => p.Name == name))
            {
                var teachpos = Positions.FirstOrDefault(p => p.Name == name);
                teachpos?.Update(curpos.Data());
            }
            else
            {
                curpos.Name = name;

                Positions.Add(curpos);
            }
        }

    }
}
