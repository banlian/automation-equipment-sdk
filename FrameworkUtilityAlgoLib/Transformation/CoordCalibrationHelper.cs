using System;
using System.Collections.Generic;
using System.Linq;
using Automation.FrameworkExtension.platforms;
using Automation.FrameworkExtension.platforms.motionPlatforms;

namespace Automation.FrameworkUtilityAlgoLib.Transformation
{
    public class CoordCalibrationHelper
    {

        public static Tuple<double[,], double> CalcAffineTransform(List<PosXYZ> pos1, List<PosXYZ> pos2)
        {
            var ret = CoordAlignHelper.AffineAlign(
                pos1.Select(p => p.X).ToArray(),pos1.Select(p => p.Y).ToArray(),
                pos2.Select(p => p.X).ToArray(),pos2.Select(p => p.Y).ToArray()
            );

            return new Tuple<double[,], double>(ret.Item1, ret.Item2);
        }

        public static PosXYZ AffineTransform(PosXYZ pos, TransformParams trans)
        {
            return new PosXYZ(CoordAlignHelper.AffineTransform(pos.Data(), trans.ToDoubles())) { Z = pos.Z };
        }

        public static PosXYZ AffineInverseTransform(TransformParams trans, PosXYZ pos)
        {
            return new PosXYZ(CoordAlignHelper.AffineInverseTransform(pos.Data(), trans.ToDoubles())) { Z = pos.Z };
        }



        public static Tuple<double[,], double> CalcAlignTransform(List<PosXYZ> platform1, List<PosXYZ> platform2)
        {
            var ret = CoordAlignHelper.Align(
                platform1.Select(p => p.X).ToArray(),
                platform1.Select(p => p.Y).ToArray(),
                platform2.Select(p => p.X).ToArray(),
                platform2.Select(p => p.Y).ToArray()
            );

            return new Tuple<double[,], double>(ret.Item1, ret.Item2);
        }

        public static PosXYZ AlignTransform(PosXYZ pos, double[,] trans)
        {
            return new PosXYZ(CoordAlignHelper.Transform(pos.Data(), trans)) { Z = pos.Z };
        }


    }
}