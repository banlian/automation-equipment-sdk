using System;
using System.ComponentModel;

namespace Automation.FrameworkExtension.platforms.points
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class PosXYZ : IPos
    {
        public int Index { get; set; }
        public string Name { get; set; }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public double OffsetX { get; set; }
        public double OffsetY { get; set; }
        public double OffsetZ { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; } = true;


        public PosXYZ()
        {
        }

        public PosXYZ(double[] pos)
        {
            if (pos.Length == 1)
            {
                X = pos[0];
            }
            else if (pos.Length == 2)
            {
                X = pos[0];
                Y = pos[1];
            }
            else if (pos.Length == 3)
            {
                X = pos[0];
                Y = pos[1];
                Z = pos[2];
            }
        }

        public PosXYZ(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static PosXYZ operator +(PosXYZ left, PosXYZ right)
        {
            return new PosXYZ()
            {
                X = left.X + right.X,
                Y = left.Y + right.Y,
                Z = left.Z + right.Z,
            };
        }
        public static PosXYZ operator -(PosXYZ left, PosXYZ right)
        {
            return new PosXYZ()
            {
                X = left.X - right.X,
                Y = left.Y - right.Y,
                Z = left.Z - right.Z,
            };
        }

        public static PosXYZ operator -(PosXYZ right)
        {
            return new PosXYZ()
            {
                X = -right.X,
                Y = -right.Y,
                Z = -right.Z,
            };
        }

        public override string ToString()
        {
            return $"{Index},{Name},{X:F3},{Y:F3},{Z:F3},{OffsetX:F3},{OffsetY:F3},{OffsetZ:F3},{Status},{Description}";
        }

        public virtual double[] Data()
        {
            return new[] { X, Y, Z, OffsetX, OffsetY, OffsetZ };
        }

        public double Distance(IPos pos)
        {
            var xyz = pos as PosXYZ;
            if (xyz != null)
            {
                var p = (this - xyz);
                return Math.Sqrt(p.X * p.X + p.Y * p.Y + p.Z * p.Z);
            }

            throw new Exception("Pos Type Error");
        }
    }
}