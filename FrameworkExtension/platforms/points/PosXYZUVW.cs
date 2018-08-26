namespace Automation.FrameworkExtension.platforms.points
{
    public class PosXYZUVW : PosXYZ
    {
        public double U { get; set; }

        public double OffsetU { get; set; }

        public double V { get; set; }

        public double OffsetV { get; set; }

        public double W { get; set; }

        public double OffsetW { get; set; }

        public PosXYZUVW()
        {
        }

        public PosXYZUVW(double[] pos)
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
            else if (pos.Length == 4)
            {
                X = pos[0];
                Y = pos[1];
                Z = pos[2];
                U = pos[3];
            }
            else if (pos.Length == 6)
            {
                X = pos[0];
                Y = pos[1];
                Z = pos[2];
                U = pos[3];
                V = pos[4];
                W = pos[5];
            }
        }

        public PosXYZUVW(double x, double y, double z, double u = 0, double v = 0, double w = 0)
        {
            X = x;
            Y = y;
            Z = z;
            U = u;
            V = v;
            W = w;
        }


        public static PosXYZUVW operator +(PosXYZUVW left, PosXYZUVW right)
        {
            return new PosXYZUVW()
            {
                X = left.X + right.X,
                Y = left.Y + right.Y,
                Z = left.Z + right.Z,
                U = left.U + right.U,
                V = left.V + right.V,
                W = left.W + right.W,
            };
        }

        public static PosXYZUVW operator -(PosXYZUVW left, PosXYZUVW right)
        {
            return new PosXYZUVW()
            {
                X = left.X - right.X,
                Y = left.Y - right.Y,
                Z = left.Z - right.Z,
                U = left.U - right.U,
                V = left.V - right.V,
                W = left.W - right.W,
            };
        }

        public static PosXYZUVW operator -(PosXYZUVW right)
        {
            return new PosXYZUVW()
            {
                X = -right.X,
                Y = -right.Y,
                Z = -right.Z,
                U = -right.U,
                V = -right.V,
                W = -right.W,
            };
        }


        public override double[] Data()
        {
            return new[] { X, Y, Z, U, V, W, OffsetX, OffsetY, OffsetZ, OffsetU, OffsetV, OffsetW };
        }

        public override string ToString()
        {
            return $"{Index},{Name},{X:F3},{Y:F3},{Z:F3},{U:F3},{V:F3},{W:F3},{OffsetX:F3},{OffsetY:F3},{OffsetZ:F3},{OffsetU:F3},{OffsetV:F3},{OffsetW:F3},{Description}";
        }


    }
}