using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Automation.FrameworkUtilityAlgoLib.Transformation
{
    public class CoordAlignHelper
    {

        #region affine transform


        /// <summary>
        ///  xy transform, return double[2]
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="affine"></param>
        /// <returns></returns>
        public static double[] AffineTransform(double[] pos, double[,] affine)
        {
            var mat = DenseMatrix.OfArray(new double[,]
            {
                {affine[0, 0], affine[0, 1], affine[0, 3]},
                {affine[1, 0], affine[1, 1], affine[1, 3]}
            });

            return mat.Multiply(DenseVector.OfArray(new[] { pos[0], pos[1], 1 })).ToArray();
        }

        /// <summary>
        /// xy inverse transform, return double[2]
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="affine"></param>
        /// <returns></returns>
        public static double[] AffineInverseTransform(double[] pos, double[,] affine)
        {
            var mat = DenseMatrix.OfArray(new double[,]
            {
                {affine[0, 0], affine[0, 1], affine[0, 3]},
                {affine[1, 0], affine[1, 1], affine[1, 3]},
                {0, 0, 1}
            }).Inverse();

            var newMat = DenseMatrix.OfArray(new double[,]
            {
                {mat[0, 0], mat[0, 1], mat[0, 2]},
                {mat[1, 0], mat[1, 1], mat[1, 2]},
            });

            return newMat.Multiply(DenseVector.OfArray(new[] { pos[0], pos[1], 1 })).ToArray();
        }


        public static Tuple<double[,], double> AffineAlign(double[] sx, double[] sy, double[] tx, double[] ty)
        {
            List<Vector<double>> src = new List<Vector<double>>();
            for (int i = 0; i < sx.Length; i++)
            {
                src.Add(DenseVector.OfArray(new[] { sx[i], sy[i], 1 }));
            }

            var a = DenseMatrix.OfRowVectors(src);

            var vec1 = a.TransposeThisAndMultiply(a).Inverse().Multiply(a.Transpose()).Multiply(DenseVector.OfArray(tx));
            var vec2 = a.TransposeThisAndMultiply(a).Inverse().Multiply(a.Transpose()).Multiply(DenseVector.OfArray(ty));
            var affine = DenseMatrix.OfRowVectors(vec1, vec2);


            //calculate errors
            List<double> errors = new List<double>();
            for (int i = 0; i < src.Count; i++)
            {
                errors.Add((affine * src[i] - DenseVector.OfArray(new[] { tx[i], ty[i] })).PointwisePower(2).Norm(1));
            }

            return new Tuple<double[,], double>(new double[,]
                {
                    {affine[0, 0], affine[0, 1], 0, affine[0, 2]},
                    {affine[1, 0], affine[1, 1], 0, affine[1, 2]},
                    {0, 0, 1, 0},
                    {0, 0, 0, 1},
                },
                errors.Max());
        }

        #endregion


        #region rigid align 2

        /// <summary>
        /// 2d transform
        /// </summary>
        /// <param name="src"></param>
        /// <param name="align"></param>
        /// <returns></returns>
        public static double[] Transform(double[] src, double[,] align)
        {
            var scale = align[2, 0];
            var r = DenseMatrix.OfArray(new double[,] { { align[0, 0], align[0, 1] }, { align[1, 0], align[1, 1] } });
            var t = DenseVector.OfArray(new double[] { align[0, 2], align[1, 2] });
            return (scale * r * DenseVector.OfArray(src.Take(2).ToArray()) + t).ToArray();
        }

        /// <summary>
        /// rigid align matrix calculate
        /// </summary>
        /// <param name="sx"></param>
        /// <param name="sy"></param>
        /// <param name="tx"></param>
        /// <param name="ty"></param>
        /// <returns></returns>
        public static Tuple<double[,], double> Align(double[] sx, double[] sy, double[] tx, double[] ty)
        {
            List<Vector<double>> src = new List<Vector<double>>();
            for (int i = 0; i < sx.Length; i++)
            {
                src.Add(DenseVector.OfArray(new[] { sx[i], sy[i] }));
            }

            List<Vector<double>> tar = new List<Vector<double>>();
            for (int i = 0; i < tx.Length; i++)
            {
                tar.Add(DenseVector.OfArray(new[] { tx[i], ty[i] }));
            }

            var ret = Align(src, tar);

            var mat = DenseMatrix.Create(3, 3, 0);

            //r
            mat[0, 0] = ret.Item1[0, 0];
            mat[0, 1] = ret.Item1[0, 1];
            mat[1, 0] = ret.Item1[1, 0];
            mat[1, 1] = ret.Item1[1, 1];

            //vt
            mat[0, 2] = ret.Item3[0];
            mat[1, 2] = ret.Item3[1];

            //scale
            mat[2, 0] = ret.Item2;
            mat[2, 1] = ret.Item2;

            mat[2, 2] = 1;

            return new Tuple<double[,], double>(mat.ToArray(), ret.Item4.Max());
        }

        /// <summary>
        ///  calc transform for single scale parameter
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Tuple<Matrix<double>, double, Vector<double>, List<double>> Align(List<Vector<double>> origin, List<Vector<double>> target)
        {
            var x = DenseMatrix.OfColumnVectors(origin);
            var y = DenseMatrix.OfColumnVectors(target);

            var mx = x.RowSums() / x.ColumnCount;
            var my = y.RowSums() / y.ColumnCount;

            var xc = x - DenseMatrix.OfColumnVectors(Enumerable.Repeat(mx, x.ColumnCount));
            var yc = y - DenseMatrix.OfColumnVectors(Enumerable.Repeat(my, x.ColumnCount));

            var sx = (xc.PointwisePower(2)).RowNorms(1).Sum() / xc.ColumnCount;
            var sy = (yc.PointwisePower(2)).RowNorms(1).Sum() / yc.ColumnCount;

            var Sxy = yc * xc.Transpose() / xc.ColumnCount;

            var svd = Sxy.Svd();
            var U = svd.U;
            var D = svd.W;
            var VT = svd.VT;

            var rankSxy = Sxy.Rank();
            var detSxy = Sxy.Determinant();
            var S = DiagonalMatrix.CreateIdentity(x.RowCount);

            if (rankSxy > x.ColumnCount - 1)
            {
                if (detSxy < 0)
                {
                    S[x.RowCount, x.RowCount] = -1;
                }
                else if (rankSxy == x.RowCount - 1)
                {
                    if (U.Determinant() * VT.Determinant() < 0)
                    {
                        S[x.RowCount, x.RowCount] = -1;
                    }
                }
                else
                {
                    var r = DiagonalMatrix.CreateIdentity(origin.First().Count);
                    var c = 1;
                    var t = DenseVector.OfArray(new Double[origin.First().Count]);

                    return new Tuple<Matrix<double>, double, Vector<double>, List<double>>(r, c, t, new List<double>() { 0d });
                }
            }

            var R = U * S * VT;
            var C = (D * S).Trace() / sx;
            var T = my - C * R * mx;


            //calculate errors
            List<double> errors = new List<double>();
            for (int i = 0; i < origin.Count; i++)
            {
                errors.Add((target[i] - (C * R * origin[i] + T)).PointwisePower(2).Norm(1));
            }


            return new Tuple<Matrix<double>, double, Vector<double>, List<double>>(R, C, T, errors);
        }


        #endregion


        #region rigid align 3

        public static double[] Transform3d(double[] src, double[,] align)
        {
            var scale = align[3, 0];
            var r = DenseMatrix.OfArray(new double[,] { { align[0, 0], align[0, 1], align[0, 2] }, { align[1, 0], align[1, 1], align[1, 2] }, { align[2, 0], align[2, 1], align[2, 2] } });
            var t = DenseVector.OfArray(new double[] { align[0, 3], align[1, 3], align[2, 3] });
            return (scale * r * DenseVector.OfArray(src.Take(3).ToArray()) + t).ToArray();
        }


        ///// <summary>
        ///// up to 3 dimension scale, rotation, translation
        ///// </summary>
        ///// <param name="src"></param>
        ///// <param name="align"></param>
        ///// <returns></returns>
        //public static Vector<double> Transform(Vector<double> src, Tuple<Matrix<double>, double, Vector<double>> align)
        //{
        //    return align.Item2 * align.Item1 * src + align.Item3;
        //}


        public static Tuple<double[,], double> Align3d(double[] sx, double[] sy, double[] sz, double[] tx, double[] ty, double[] tz)
        {
            List<Vector<double>> src = new List<Vector<double>>();
            for (int i = 0; i < sx.Length; i++)
            {
                src.Add(DenseVector.OfArray(new[] { sx[i], sy[i], sz[i] }));
            }

            List<Vector<double>> tar = new List<Vector<double>>();
            for (int i = 0; i < tx.Length; i++)
            {
                tar.Add(DenseVector.OfArray(new[] { tx[i], ty[i], tz[i] }));
            }

            var ret = Align(src, tar);

            var mat = DenseMatrix.Create(4, 4, 0);

            //r
            mat[0, 0] = ret.Item1[0, 0];
            mat[0, 1] = ret.Item1[0, 1];
            mat[0, 2] = ret.Item1[0, 2];
            mat[1, 0] = ret.Item1[1, 0];
            mat[1, 1] = ret.Item1[1, 1];
            mat[1, 2] = ret.Item1[1, 2];
            mat[2, 0] = ret.Item1[2, 0];
            mat[2, 1] = ret.Item1[2, 1];
            mat[2, 2] = ret.Item1[2, 2];

            //vt
            mat[0, 3] = ret.Item3[0];
            mat[1, 3] = ret.Item3[1];
            mat[2, 3] = ret.Item3[2];

            //scale
            mat[3, 0] = ret.Item2;
            mat[3, 1] = ret.Item2;
            mat[3, 2] = ret.Item2;

            mat[3, 3] = 1;

            return new Tuple<double[,], double>(mat.ToArray(), ret.Item4.Max());
        }


        #endregion

      
      
    }
}