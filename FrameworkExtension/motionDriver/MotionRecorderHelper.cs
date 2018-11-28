using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Automation.FrameworkExtension.platforms.motionPlatforms;

namespace Automation.FrameworkExtension.motionDriver
{
    public class MotionRecorderHelper
    {

        private readonly Stopwatch _sw = new Stopwatch();
        private readonly List<PosXYZ> _movements = new List<PosXYZ>();

        public void InitRecord()
        {
            _movements.Clear();
            _sw.Stop();
            _sw.Reset();
        }

        public void RecordMoveStart(PosXYZ p)
        {
            _sw.Restart();
            _movements.Add(p);
        }

        public void RecordMoveFinish(PosXYZ p)
        {
            _sw.Stop();
            var last = _movements.Last();
            last.X = Math.Abs(p.X - last.X);
            last.Y = Math.Abs(p.Y - last.Y);
            last.Z = Math.Abs(p.Z - last.Z);
            last.OffsetX = _sw.ElapsedMilliseconds;
        }

        public string DisplatMoveDetails()
        {
            var sb = new StringBuilder();
            sb.AppendLine("MoveDetails:\n-----------------------------------------------");
            for (int i = 0; i < _movements.Count; i++)
            {
                sb.AppendLine($" {i} {_movements[i].ToString()}");
            }
            sb.AppendLine("MoveDetails:\n-----------------------------------------------");
            sb.AppendLine($"X:{_movements.Select(m => m.X).Sum():F2} Y:{_movements.Select(m => m.Y).Sum():F2} Z:{_movements.Select(m => m.Z).Sum():F2} TIME:{_movements.Select(m => m.OffsetX).Sum():F2}");
            sb.AppendLine("MoveDetails:\n-----------------------------------------------");

            return sb.ToString();
        }
    }
}
