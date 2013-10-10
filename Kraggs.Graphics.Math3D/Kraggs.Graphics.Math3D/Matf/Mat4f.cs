using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Kraggs.Graphics.Math3D
{
    //[DebuggerDisplay("TODO")]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct Mat4f : IEquatable<Mat4f>, IGLMatrix, IGenericStream
    {
        /// <summary>
        /// The First Column.
        /// </summary>
        public Vec4f c0;
        /// <summary>
        /// The Second Column.
        /// </summary>
        public Vec4f c1;
        /// <summary>
        /// The Third Column.
        /// </summary>
        public Vec4f c2;
        /// <summary>
        /// The Fourth Column.
        /// </summary>
        public Vec4f c3;

        #region Properties

        public static readonly Mat4f Zero = new Mat4f() { c0 = Vec4f.Zero, c1 = Vec4f.Zero, c2 = Vec4f.Zero, c3 = Vec4f.Zero };
        public static readonly Mat4f Identity = new Mat4f() { c0 = Vec4f.UnitX, c1 = Vec4f.UnitY, c2 = Vec4f.UnitZ, c3 = Vec4f.UnitW };

        /// <summary>
        /// Returns the row0 of this matrix.
        /// It also gives a nice debug view of this matrix.
        /// </summary>
        [DebuggerNonUserCode()]
        public Vec4f Row0
        {
            get
            {
                return new Vec4f() { x = c0.x, y = c1.x, z = c2.x, w = c3.x };
            }
        }

        /// <summary>
        /// Returns the row1 of this matrix.
        /// It also gives a nice debug view of this matrix.
        /// </summary>
        [DebuggerNonUserCode()]
        public Vec4f Row1
        {
            get
            {
                return new Vec4f() { x = c0.y, y = c1.y, z = c2.y, w = c3.y };
            }
        }
        /// <summary>
        /// Returns the row2 of this matrix.
        /// It also gives a nice debug view of this matrix.
        /// </summary>
        [DebuggerNonUserCode()]
        public Vec4f Row2
        {
            get
            {
                return new Vec4f() { x = c0.z, y = c1.z, z = c2.z, w = c3.z };
            }
        }
        /// <summary>
        /// Returns the row3 of this matrix.
        /// It also gives a nice debug view of this matrix.
        /// </summary>
        [DebuggerNonUserCode()]
        public Vec4f Row3
        {
            get
            {
                return new Vec4f() { x = c0.w, y = c1.w, z = c2.w, w = c3.w };
            }
        }

        /// <summary>
        /// Returns the translation in this matrix.
        /// </summary>
        [DebuggerNonUserCode()]        
        public Vec3f Translation
        {
            get
            {
                return new Vec3f() { x = c3.x, y = c3.y, z = c3.z };
                //return (Vec3f)c3;
            }
        }

        /// <summary>
        /// Returns the scale in this matrix as a vec3f
        /// TODO: Should this be Vec4f instead?
        /// </summary>
        [DebuggerNonUserCode()]
        public Vec3f Scale
        {
            get
            {
                return new Vec3f() { x = c0.x, y = c1.y, z = c2.z};
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a matrix comparable to Identity * Scalar.
        /// </summary>
        /// <param name="scalar"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]        
        public Mat4f(float scalar)
        {
            c0 = Vec4f.Zero;
            c1 = Vec4f.Zero;
            c2 = Vec4f.Zero;
            c3 = Vec4f.Zero;
            c0.x = scalar;
            c1.y = scalar;
            c2.z = scalar;
            c3.w = scalar;
        }

        /// <summary>
        /// Returns a new matrix from individuall floating components.
        /// </summary>
        /// <param name="x0">x component of column 0</param>
        /// <param name="y0">y component of column 0</param>
        /// <param name="z0">z component of column 0</param>
        /// <param name="w0">w component of column 0</param>
        /// <param name="x1">x component of column 1</param>
        /// <param name="y1">y component of column 1</param>
        /// <param name="z1">z component of column 1</param>
        /// <param name="w1">w component of column 1</param>
        /// <param name="x2">x component of column 2</param>
        /// <param name="y2">y component of column 2</param>
        /// <param name="z2">z component of column 2</param>
        /// <param name="w2">w component of column 2</param>
        /// <param name="x3">x component of column 3</param>
        /// <param name="y3">y component of column 3</param>
        /// <param name="z3">z component of column 3</param>
        /// <param name="w3">w component of column 3</param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Mat4f(
            float x0, float y0, float z0, float w0,
            float x1, float y1, float z1, float w1,
            float x2, float y2, float z2, float w2,
            float x3, float y3, float z3, float w3)
        {
            c0 = new Vec4f() { x = x0, y = y0, z = z0, w = w0 };
            c1 = new Vec4f() { x = x1, y = y1, z = z1, w = w1 };
            c2 = new Vec4f() { x = x2, y = y2, z = z2, w = w2 };
            c3 = new Vec4f() { x = x3, y = y3, z = z3, w = w3 };
        }
        /// <summary>
        /// Creates a matrix from Column vectors.
        /// </summary>
        /// <param name="c0"></param>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <param name="c3"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Mat4f(Vec4f c0, Vec4f c1, Vec4f c2, Vec4f c3)
        {
            this.c0 = c0;
            this.c1 = c1;
            this.c2 = c2;
            this.c3 = c3;
        }

        #endregion

        #region Instance Functions

        [DebuggerNonUserCode()]        
        public float Determinant
        {
            get
            {
                return GetDeterminant();
            }
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private float GetDeterminant()
        {
            float SubFactor00 = c2.z * c3.w - c3.z * c2.w;
            float SubFactor01 = c2.y * c3.w - c3.y * c2.w;
            float SubFactor02 = c2.y * c3.z - c3.y * c2.z;
            float SubFactor03 = c2.x * c3.w - c3.x * c2.w;
            float SubFactor04 = c2.x * c3.z - c3.x * c2.z;
            float SubFactor05 = c2.x * c3.y - c3.x * c2.y;

            Vec4f DetCof = new Vec4f()
            {
                x = +(c1.y * SubFactor00 - c1.z * SubFactor01 + c1.w * SubFactor02),
                y = -(c1.x * SubFactor00 - c1.z * SubFactor03 + c1.w * SubFactor04),
                z = +(c1.x * SubFactor01 - c1.y * SubFactor03 + c1.w * SubFactor05),
                w = -(c1.x * SubFactor02 - c1.y * SubFactor04 + c1.z * SubFactor05)
            };

            return c0.x * DetCof.x
                + c0.y * DetCof.y
                + c0.z * DetCof.z
                + c0.w * DetCof.w;
        }

        public unsafe float[] ToArray()
        {
            var result = new float[16];

            fixed (float* ptr = &this.c0.x)
                Marshal.Copy((IntPtr)ptr, result, 0, result.Length);

            return result;
        }

        /// <summary>
        /// Transposes this matrix.
        /// </summary>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Transpose()
        {
            // first column
            float t = c0.y;
            c0.y = c1.x;
            c1.x = t;

            t = c0.z;
            c0.z = c2.x;
            c2.x = t;

            t = c0.w;
            c0.w = c3.x;
            c3.x = t;

            // second column
            t = c1.z;
            c1.z = c2.y;
            c2.y = t;

            t = c1.w;
            c1.w = c3.y;
            c3.y = t;

            // third column
            t = c2.w;
            c2.w = c3.z;
            c3.z = t;

            //System.Threading.Interlocked.Exchange(
        }

        #endregion

        #region Static Matrix Functions

        /// <summary>
        /// Transposes a matrix.
        /// </summary>
        /// <param name="m">matrix to get transpose version of</param>
        /// <returns>The transpose version of m.</returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f Transpose(Mat4f m)
        {
            return new Mat4f(
                m.c0.x, m.c1.x, m.c2.x, m.c3.x,
                m.c0.y, m.c1.y, m.c2.y, m.c3.y,
                m.c0.z, m.c1.z, m.c2.z, m.c3.z,
                m.c0.w, m.c1.w, m.c2.w, m.c3.w
                );
        }

        /// <summary>
        /// Transposes a Matrix
        /// </summary>
        /// <param name="m">Matrix to get transpose version of.</param>
        /// <param name="result">The transpose version of m.</param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Transpose(ref Mat4f m, out Mat4f result)
        {
            result.c0.x = m.c0.x;
            result.c0.y = m.c1.x;
            result.c0.z = m.c2.x;
            result.c0.w = m.c3.x;

            result.c1.x = m.c0.y;
            result.c1.y = m.c1.y;
            result.c1.z = m.c2.y;
            result.c1.w = m.c3.y;

            result.c2.x = m.c0.z;
            result.c2.y = m.c1.z;
            result.c2.z = m.c2.z;
            result.c2.w = m.c3.z;

            result.c3.x = m.c0.w;
            result.c3.y = m.c1.w;
            result.c3.z = m.c2.w;
            result.c3.w = m.c3.w;
        }

        /// <summary>
        /// Calculates the OuterProduct of column c and Row r.
        /// </summary>
        /// <param name="c">Column</param>
        /// <param name="r">Row</param>
        /// <returns>the resulting outerproduct matrix.</returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f OuterProduct(Vec4f c, Vec4f r)
        {
            Mat4f m; // = Mat3f.Zero;
            m.c0 = c * r.x;
            m.c1 = c * r.y;
            m.c2 = c * r.z;
            m.c3 = c * r.w;
            return m;
        }

        /// <summary>
        /// Calculates the inverse of a matrix.
        /// </summary>
        /// <param name="m">Matrix to get inverse of.</param>
        /// <returns>the inverse matrix of m.</returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f Inverse(Mat4f m)
        {
            float Coef00 = m.c2.z * m.c3.w - m.c3.z * m.c2.w;
            float Coef02 = m.c1.z * m.c3.w - m.c3.z * m.c1.w;
            float Coef03 = m.c1.z * m.c2.w - m.c2.z * m.c1.w;

            float Coef04 = m.c2.y * m.c3.w - m.c3.y * m.c2.w;
            float Coef06 = m.c1.y * m.c3.w - m.c3.y * m.c1.w;
            float Coef07 = m.c1.y * m.c2.w - m.c2.y * m.c1.w;

            float Coef08 = m.c2.y * m.c3.z - m.c3.y * m.c2.z;
            float Coef10 = m.c1.y * m.c3.z - m.c3.y * m.c1.z;
            float Coef11 = m.c1.y * m.c2.z - m.c2.y * m.c1.z;

            float Coef12 = m.c2.x * m.c3.w - m.c3.x * m.c2.w;
            float Coef14 = m.c1.x * m.c3.w - m.c3.x * m.c1.w;
            float Coef15 = m.c1.x * m.c2.w - m.c2.x * m.c1.w;

            float Coef16 = m.c2.x * m.c3.z - m.c3.x * m.c2.z;
            float Coef18 = m.c1.x * m.c3.z - m.c3.x * m.c1.z;
            float Coef19 = m.c1.x * m.c2.z - m.c2.x * m.c1.z;

            float Coef20 = m.c2.x * m.c3.y - m.c3.x * m.c2.y;
            float Coef22 = m.c1.x * m.c3.y - m.c3.x * m.c1.y;
            float Coef23 = m.c1.x * m.c2.y - m.c2.x * m.c1.y;

            Vec4f SignA = new Vec4f(+1.0f, -1.0f, +1.0f, -1.0f);
            Vec4f SignB = new Vec4f(-1.0f, +1.0f, -1.0f, +1.0f);

            Vec4f Fac0 = new Vec4f(Coef00, Coef00, Coef02, Coef03);
            Vec4f Fac1 = new Vec4f(Coef04, Coef04, Coef06, Coef07);
            Vec4f Fac2 = new Vec4f(Coef08, Coef08, Coef10, Coef11);
            Vec4f Fac3 = new Vec4f(Coef12, Coef12, Coef14, Coef15);
            Vec4f Fac4 = new Vec4f(Coef16, Coef16, Coef18, Coef19);
            Vec4f Fac5 = new Vec4f(Coef20, Coef20, Coef22, Coef23);

            Vec4f Vec0 = new Vec4f(m.c1.x, m.c0.x, m.c0.x, m.c0.x);
            Vec4f Vec1 = new Vec4f(m.c1.y, m.c0.y, m.c0.y, m.c0.y);
            Vec4f Vec2 = new Vec4f(m.c1.z, m.c0.z, m.c0.z, m.c0.z);
            Vec4f Vec3 = new Vec4f(m.c1.w, m.c0.w, m.c0.w, m.c0.w);

            Vec4f Inv0 = SignA * (Vec1 * Fac0 - Vec2 * Fac1 + Vec3 * Fac2);
            Vec4f Inv1 = SignB * (Vec0 * Fac0 - Vec2 * Fac3 + Vec3 * Fac4);
            Vec4f Inv2 = SignA * (Vec0 * Fac1 - Vec1 * Fac3 + Vec3 * Fac5);
            Vec4f Inv3 = SignB * (Vec0 * Fac2 - Vec1 * Fac4 + Vec2 * Fac5);

            Mat4f inverse = new Mat4f(Inv0, Inv1, Inv2, Inv3);

            Vec4f Row0 = new Vec4f(inverse.c0.x, inverse.c1.x, inverse.c2.x, inverse.c3.x);

            float determinant = Vec4f.Dot(m.c0, Row0);
            
            return inverse / determinant;

        }

        /// <summary>
        /// Creates a affice Inverse of provided matrix.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f AffineInverse(Mat4f m)
        {
            Mat4f result = m;
            result.c3 = Vec4f.UnitW;
            result.Transpose();
            Vec4f translation = result * new Vec4f(-(Vec3f)m.c3, m.c3.w);
            result.c3 = translation;
            return result;
        }

        public static Mat4f InverseTranspose(Mat4f m)
        {
            float SubFactor00 = m.c2.z * m.c3.w - m.c3.z * m.c2.w;
            float SubFactor01 = m.c2.y * m.c3.w - m.c3.y * m.c2.w;
            float SubFactor02 = m.c2.y * m.c3.z - m.c3.y * m.c2.z;
            float SubFactor03 = m.c2.x * m.c3.w - m.c3.x * m.c2.w;
            float SubFactor04 = m.c2.x * m.c3.z - m.c3.x * m.c2.z;
            float SubFactor05 = m.c2.x * m.c3.y - m.c3.x * m.c2.y;
            float SubFactor06 = m.c1.z * m.c3.w - m.c3.z * m.c1.w;
            float SubFactor07 = m.c1.y * m.c3.w - m.c3.y * m.c1.w;
            float SubFactor08 = m.c1.y * m.c3.z - m.c3.y * m.c1.z;
            float SubFactor09 = m.c1.x * m.c3.w - m.c3.x * m.c1.w;
            float SubFactor10 = m.c1.x * m.c3.z - m.c3.x * m.c1.z;
            float SubFactor11 = m.c1.y * m.c3.w - m.c3.y * m.c1.w;
            float SubFactor12 = m.c1.x * m.c3.y - m.c3.x * m.c1.y;
            float SubFactor13 = m.c1.y * m.c2.w - m.c2.z * m.c1.w;
            float SubFactor14 = m.c1.y * m.c2.w - m.c2.y * m.c1.w;
            float SubFactor15 = m.c1.y * m.c2.z - m.c2.y * m.c1.z;
            float SubFactor16 = m.c1.x * m.c2.w - m.c2.x * m.c1.w;
            float SubFactor17 = m.c1.x * m.c2.z - m.c2.x * m.c1.z;
            float SubFactor18 = m.c1.x * m.c2.y - m.c2.x * m.c1.y;

            Mat4f inverse;

            inverse.c0.x = +(m.c1.y * SubFactor00 - m.c1.z * SubFactor01 + m.c1.w * SubFactor02);
            inverse.c0.y = -(m.c1.x * SubFactor00 - m.c1.z * SubFactor03 + m.c1.w * SubFactor04);
            inverse.c0.z = +(m.c1.x * SubFactor01 - m.c1.y * SubFactor03 + m.c1.w * SubFactor05);
            inverse.c0.w = -(m.c1.x * SubFactor02 - m.c1.y * SubFactor04 + m.c1.z * SubFactor05);

            inverse.c1.x = -(m.c0.y * SubFactor00 - m.c0.z * SubFactor01 + m.c0.w * SubFactor02);
            inverse.c1.y = +(m.c0.x * SubFactor00 - m.c0.z * SubFactor03 + m.c0.w * SubFactor04);
            inverse.c1.z = -(m.c0.x * SubFactor01 - m.c0.y * SubFactor03 + m.c0.w * SubFactor05);
            inverse.c1.w = +(m.c0.x * SubFactor02 - m.c0.y * SubFactor04 + m.c0.z * SubFactor05);

            inverse.c2.x = +(m.c0.y * SubFactor06 - m.c0.z * SubFactor07 + m.c0.w * SubFactor08);
            inverse.c2.y = -(m.c0.x * SubFactor06 - m.c0.z * SubFactor09 + m.c0.w * SubFactor10);
            inverse.c2.z = +(m.c0.x * SubFactor11 - m.c0.y * SubFactor09 + m.c0.w * SubFactor12);
            inverse.c2.w = -(m.c0.x * SubFactor08 - m.c0.y * SubFactor10 + m.c0.z * SubFactor12);

            inverse.c3.x = -(m.c0.y * SubFactor13 - m.c0.z * SubFactor14 + m.c0.w * SubFactor15);
            inverse.c3.y = +(m.c0.x * SubFactor13 - m.c0.z * SubFactor16 + m.c0.w * SubFactor17);
            inverse.c3.z = -(m.c0.x * SubFactor14 - m.c0.y * SubFactor16 + m.c0.w * SubFactor18);
            inverse.c3.w = +(m.c0.x * SubFactor15 - m.c0.y * SubFactor17 + m.c0.z * SubFactor18);

            float determinant =
                +m.c0.x * inverse.c0.x
                + m.c0.y * inverse.c0.y
                + m.c0.z * inverse.c0.z
                + m.c0.w * inverse.c0.w;

            return inverse / determinant;

        }


        #endregion

        #region Static Transform Functions

        /// <summary>
        /// Creates a translation 4x4 matrix from a vector of 3 components.
        /// GLM verified at Milestone6 at 8.sept.2013
        /// </summary>
        /// <param name="translation"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]        
        public static Mat4f CreateTranslation(Vec3f translation)
        {
            Mat4f result = Mat4f.Identity;
            result.c3 = new Vec4f(translation, 1.0f);
            return result;
            //result.c3 =
            //    Vec4f.UnitX * translation.x +
            //    Vec4f.UnitY * translation.y +
            //    Vec4f.UnitZ * translation.z +
            //    Vec4f.UnitW;

            //return result;
        }

        //public static Mat4f CreateTranslation(Vec3f translation, Mat4f StartingPoint )
        //{

        //}

        /// <summary>
        /// Creates a rotation 4x4 matrix from an axis vector and an angle expressed in degrees.
        /// GLM verified at Milestone6 at 8.sept.2013
        /// </summary>
        /// <param name="angleInDegrees">Rotation angle in degrees.</param>
        /// <param name="axis">Rotation Axis, recommended to be normalized.</param>
        /// <returns></returns>
        public static Mat4f CreateRotation(float angleInDegrees, Vec3f axis)
        {
            float a = MathF.ToRadians(angleInDegrees);
            float c = (float)Math.Cos(a);
            float s = (float)Math.Sin(a);

            axis.Normalize();

            Mat4f result = Mat4f.Zero;

            result.c0.x = c + (1 - c) * axis.x * axis.x;
            result.c0.y = (1 - c) * axis.x * axis.y + s * axis.z;
            result.c0.z = (1 - c) * axis.x * axis.z - s * axis.y;

            result.c1.x = (1 - c) * axis.y * axis.x - s * axis.z;
            result.c1.y = c + (1 - c) * axis.y * axis.y;
            result.c1.z = (1 - c) * axis.y * axis.z + s * axis.x;

            result.c2.x = (1 - c) * axis.z * axis.x + s * axis.y;
            result.c2.y = (1 - c) * axis.z * axis.y - s * axis.x;
            result.c2.z = c + (1 - c) * axis.z * axis.z;

            result.c3.w = 1.0f;
            return result;


            //Vec3f temp = (1.0f - c) * axis;

            //// TODO: Remove referenes to seperate Result mat4f. Compute directly into rotate.

            //Mat4f rotate = Mat4f.Zero;

            //rotate.c0.x = c + temp.x * axis.x;
            //rotate.c0.y = 0 + temp.x * axis.y + s * axis.z;
            //rotate.c0.z = 0 + temp.x * axis.z - s * axis.y;

            //rotate.c1.x = 0 + temp.y * axis.x - s * axis.z;
            //rotate.c1.y = 0 + temp.y * axis.y;
            //rotate.c1.z = 0 + temp.y * axis.z + s * axis.x;

            //rotate.c2.x = 0 + temp.z * axis.x + s * axis.y;
            //rotate.c2.y = 0 + temp.z * axis.y - s * axis.x;
            //rotate.c2.z = c + temp.z * axis.z;

            //Mat4f Result = Mat4f.Zero;

            //Result.c0 = Vec4f.UnitX * rotate.c0.x + Vec4f.UnitY * rotate.c0.y + Vec4f.UnitZ * rotate.c0.z;
            //Result.c1 = Vec4f.UnitX * rotate.c1.x + Vec4f.UnitY * rotate.c1.y + Vec4f.UnitZ * rotate.c1.z;
            //Result.c2 = Vec4f.UnitX * rotate.c2.x + Vec4f.UnitY * rotate.c2.y + Vec4f.UnitZ * rotate.c2.z;
            //Result.c3 = Vec4f.UnitW;
            //return Result;

        }

        /// <summary>
        /// Creates a rotation around x axix matrix.
        /// GLM verified at Milestone6 at 8.sept.2013
        /// </summary>
        /// <param name="angleInDegrees">Degree to rotate.</param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]        
        public static Mat4f CreateRotationX(float angleInDegrees)
        {
            float r = MathF.ToRadians(angleInDegrees);
            float c = (float)Math.Cos(r);
            float s = (float)Math.Sin(r);

            Mat4f result = Mat4f.Identity;
            result.c1.y = c;
            result.c1.z = s;
            result.c2.y = -s;
            result.c2.z = c;

            return result;
        }

        /// <summary>
        /// Creates a rotation matrix around Y axis.
        /// GLM verified at Milestone6 at 8.sept.2013
        /// </summary>
        /// <param name="angleInDegrees">Degree to rotate.</param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f CreateRotationY(float angleInDegrees)
        {
            float r = MathF.ToRadians(angleInDegrees);
            float c = (float)Math.Cos(r);
            float s = (float)Math.Sin(r);

            Mat4f result = Mat4f.Identity;
            result.c0.x = c;
            result.c0.z = -s;
            result.c2.x = s;
            result.c2.z = c;

            return result;
        }

        /// <summary>
        /// Creates a rotation matrix around z axis.
        /// GLM verified at Milestone6 at 8.sept.2013
        /// </summary>
        /// <param name="angleInDegrees">Degrees to rotate.</param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f CreateRotationZ(float angleInDegrees)
        {
            //float r = MathFunctions.Radians(angleInDegrees);
            float r = MathF.ToRadians(angleInDegrees);
            float c = (float)Math.Cos(r);
            float s = (float)Math.Sin(r);

            Mat4f result = Mat4f.Identity;
            result.c0.x = c;
            result.c0.y = s;
            result.c1.x = -s;
            result.c1.y = c;

            return result;
        }

        /// <summary>
        /// Creates a scale 4x4 matrix from 3 scalars/Vec3f.
        /// GLM verified at Milestone6 at 8.sept.2013
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]        
        public static Mat4f CreateScale(Vec3f scale)
        {
            Mat4f result = Mat4f.Zero;
            result.c0.x = scale.x;
            result.c1.y = scale.y;
            result.c2.z = scale.z;
            result.c3.w = 1.0f;

            return result;
        }

        /// <summary>
        /// Creates a matrix for an orthographics parallel viewing volume.
        /// GLM verified at Milestone6 at 8.sept.2013
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]        
        public static Mat4f CreateOrtho(float left, float right, float bottom, float top)
        {
            Mat4f result = Mat4f.Identity;
            result.c0.x = 2.0f / (right - left);
            result.c1.y = 2.0f / (top - bottom);
            result.c2.z = -1.0f;
            result.c3.x = -(right + left) / (right - left);
            result.c3.y = -(top + bottom) / (top - bottom);

            return result;
        }

        /// <summary>
        /// Creates a matrix for projecting two-dimensional coordinates onto the screen.
        /// GLM verified at Milestone6 at 8.sept.2013
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        /// <param name="top"></param>
        /// <param name="zNear"></param>
        /// <param name="zFar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]        
        public static Mat4f CreateOrtho(float left, float right, float bottom, float top, float zNear, float zFar)
        {
            Mat4f result = Mat4f.Identity;
            result.c0.x = 2.0f / (right - left);
            result.c1.y = 2.0f / (top - bottom);
            result.c2.z = -2.0f / (zFar - zNear);
            result.c3.x = -(right + left) / (right - left);
            result.c3.y = -(top + bottom) / (top - bottom);
            result.c3.z = -(zFar + zNear) / (zFar - zNear);
            return result;
        }

        /// <summary>
        /// Creates a frustrum matrix.
        /// GLM verified at Milestone6 at 8.sept.2013
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        /// <param name="top"></param>
        /// <param name="near"></param>
        /// <param name="far"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]        
        public static Mat4f CreateFrustum(float left, float right, float bottom, float top, float near, float far)
        {
            Mat4f result = Mat4f.Zero;
            result.c0.x = (2.0f * near) / (right - left);
            result.c1.y = (2.0f * near) / (top - bottom);
            result.c2.x = (right + left) / (right - left);
            result.c2.y = (top + bottom) / (top - bottom);
            result.c2.z = -(far + near) / (far - near);
            result.c2.w = -1.0f;
            result.c3.z = -(2.0f * far * near) / (far - near);
            return result;
        }

        /// <summary>
        /// Creates a matrix for a symmetrix perspective-view frustrum.
        /// GLM verified at Milestone6 at 8.sept.2013
        /// </summary>
        /// <param name="fovy"></param>
        /// <param name="aspect"></param>
        /// <param name="near"></param>
        /// <param name="far"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]        
        public static Mat4f CreatePerspective(float fovy, float aspect, float near, float far)
        {
            //float range = (float)Math.Tan(MathFunctions.Radians(fovy / 2.0f)) * near;
            float range = MathF.Tan(MathF.ToRadians(fovy / 2.0f)) * near;
            float left = -range * aspect;
            float right = range * aspect;
            float bottom = -range;
            float top = range;

            Mat4f result = Mat4f.Zero;
            result.c0.x = (2.0f * near) / (right - left);
            result.c1.y = (2.0f * near) / (top - bottom);
            result.c2.z = -(far + near) / (far - near);
            result.c2.w = -1.0f;
            result.c3.z = -(2.0f * far * near) / (far - near);

            return result;
        }

        /// <summary>
        /// Creates a perspective projection matrix based on a field of view.
        /// GLM verified at Milestone6 at 8.sept.2013
        /// </summary>
        /// <param name="fov"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="near"></param>
        /// <param name="far"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]        
        public static Mat4f CreatePerspectiveFov(float fov, float width, float height, float near, float far)
        {
            //float rad = MathFunctions.Radians(fov);
            float rad = MathF.ToRadians(fov);
            float h = (float)Math.Cos(0.5f * rad) / (float)Math.Sin(0.5f * rad);
            float w = h * height / width;

            Mat4f result = Mat4f.Zero;
            result.c0.x = w;
            result.c1.y = h;
            result.c2.z = -(far + near) / (far - near);
            result.c2.w = -1.0f;
            result.c3.z = -(2.0f * far * near) / (far - near);

            return result;
        }

        /// <summary>
        /// Creates a matrix for symmetric perspective-view frustrum with far plane at infinite.
        /// GLM verified at Milestone6 at 8.sept.2013
        /// </summary>
        /// <param name="fovy"></param>
        /// <param name="aspect"></param>
        /// <param name="near"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]        
        public static Mat4f CreateInfinitePerspective(float fovy, float aspect, float near)
        {
            //float range = (float)Math.Tan(MathFunctions.Radians(fovy / 2.0f)) * near;
            float range = MathF.Tan(MathF.ToRadians(fovy / 2.0f)) * near;
            float left = -range * aspect;
            float right = range * aspect;
            float bottom = -range;
            float top = range;

            Mat4f result = Mat4f.Zero;
            result.c0.x = (2.0f * near) / (right - left);
            result.c1.y = (2.0f * near) / (top - bottom);
            result.c2.z = -1.0f;
            result.c2.w = -1.0f;
            result.c3.z = -2.0f * near;

            return result;
        }

        /// <summary>
        /// Creates a matrix for a symmetric perspective-view frustrum with far plane at infinite for graphics hardware that doesn't supprt depth clamping.
        /// GLM verified at Milestone6 at 8.sept.2013
        /// </summary>
        /// <param name="fovy"></param>
        /// <param name="aspect"></param>
        /// <param name="near"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]        
        public static Mat4f CreateTweakedInfinitePerspective(float fovy, float aspect, float near)
        {
            //float range = (float)Math.Tan(MathFunctions.Radians(fovy / 2.0f)) * near;
            float range = MathF.Tan(MathF.ToRadians(fovy / 2.0f)) * near;
            float left = -range * aspect;
            float right = range * aspect;
            float bottom = -range;
            float top = range;

            Mat4f result = Mat4f.Zero;
            result.c0.x = (2.0f * near) / (right - left);
            result.c1.y = (2.0f * near) / (top - bottom);
            result.c2.z = 0.0001f - 1.0f;
            result.c2.w = -1.0f;
            result.c3.z = -(0.0001f - 2.0f) * near;
            return result;
        }

        /// <summary>
        /// Map the specified object coordinates (obj,x, obj.y, obj.z) into window coordinates.
        /// GLM verified at Milestone6 at 8.sept.2013
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="model"></param>
        /// <param name="proj"></param>
        /// <param name="viewport"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]        
        public static Vec3f Project(Vec3f obj, Mat4f model, Mat4f proj, Vec4f viewport)
        {
            Vec4f tmp = new Vec4f(obj, 1.0f);
            tmp = model * tmp;
            tmp = proj * tmp;

            tmp /= tmp.w;
            tmp = tmp * 0.5f + 0.5f;
            tmp.x = tmp.x * viewport.z + viewport.x;
            tmp.y = tmp.y * viewport.w + viewport.y;

            return (Vec3f)tmp;
        }

        /// <summary>
        /// Maps the specified windows coordinates (win.x, win.y, winz) into object coordinates.
        /// GLM verified at Milestone6 at 8.sept.2013
        /// </summary>
        /// <param name="win"></param>
        /// <param name="model"></param>
        /// <param name="proj"></param>
        /// <param name="viewport"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]        
        public static Vec3f UnProject(Vec3f win, Mat4f model, Mat4f proj, Vec4f viewport)
        {
            Mat4f inverse = Mat4f.Inverse(proj * model);

            Vec4f tmp = new Vec4f(win, 1.0f);
            tmp.x = (tmp.x - viewport.x) / viewport.z;
            tmp.y = (tmp.y - viewport.y) / viewport.w;
            tmp = tmp * 2.0f - 1.0f;

            Vec4f obj = inverse * tmp;
            obj /= obj.w;

            return (Vec3f)obj;
        }

        /// <summary>
        /// Creates a picking region.
        /// GLM verified at Milestone6 at 8.sept.2013
        /// </summary>
        /// <param name="center"></param>
        /// <param name="delta"></param>
        /// <param name="viewport"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]        
        public static Mat4f CreatePickMatrix(Vec2f center, Vec2f delta, Vec4f viewport)
        {
            Debug.Assert(delta.x > 0.0f && delta.y > 0.0f);

            Mat4f result = Mat4f.Identity;

            if (!(delta.x > 0.0f && delta.y > 0.0f))
                return result; // error.

            Vec3f temp = new Vec3f(
                (viewport.z - 2.0f * (center.x - viewport.x)) / delta.x,
                (viewport.w - 2.0f * (center.y - viewport.y)) / delta.y,
                0.0f);

            // Translate and scale the picked region to the entire window.
            result = result * CreateTranslation(temp);
            result = result * CreateScale(new Vec3f(
                viewport.z / delta.x, viewport.w / delta.y, 1.0f));

            return result;
        }

        /// <summary>
        /// Creates a look at view matrix.
        /// GLM verified at Milestone6 at 8.sept.2013
        /// </summary>
        /// <param name="eye"></param>
        /// <param name="center"></param>
        /// <param name="up"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]        
        public static Mat4f CreateLookAt(Vec3f eye, Vec3f center, Vec3f up)
        {
            Vec3f f = Vec3f.Normalize(center - eye);
            Vec3f u = Vec3f.Normalize(up);
            Vec3f s = Vec3f.Normalize(Vec3f.Cross(f, u));
            u = Vec3f.Cross(s, f);

            Mat4f result = Mat4f.Identity;            
            result.c0.x = s.x;
            result.c1.x = s.y;
            result.c2.x = s.z;
            result.c0.y = u.x;
            result.c1.y = u.y;
            result.c2.y = u.z;
            result.c0.z = -f.x;
            result.c1.z = -f.y;
            result.c2.z = -f.z;
            result.c3.x = -Vec3f.Dot(s, eye);
            result.c3.y = -Vec3f.Dot(u, eye);
            result.c3.z =  Vec3f.Dot(f, eye);

            return result;
        }

        #endregion

        #region Static EulerAngles

        /// <summary>
        /// Creates a 3D 4 * 4 homogeneous rotation matrix from an euler angle X.
        /// </summary>
        /// <param name="AngleX"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]        
        public static Mat4f CreateEulerAngleX(float AngleX)
        {
            float cosX = (float)Math.Cos(AngleX);
            float sinX = (float)Math.Sin(AngleX);

            return new Mat4f(
                1.0f, 0.0f, 0.0f, 0.0f,
                0.0f, cosX, sinX, 0.0f,
                0.0f, -sinX, cosX, 0.0f,
                0.0f, 0.0f, 0.0f, 1.0f
                );
        }

        /// <summary>
        /// Creates a 3D 4 * 4 homogeneous rotation matrix from an euler angle Y.
        /// </summary>
        /// <param name="AngleY"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]        
        public static Mat4f CreateEulerAngleY(float AngleY)
        {
            float cosY = (float)Math.Cos(AngleY);
            float sinY = (float)Math.Sin(AngleY);

            return new Mat4f(
                    cosY, 0.0f, sinY, 0.0f,
                    0.0f, 1.0f, 0.0f, 0.0f,
                    -sinY, 0.0f, cosY, 0.0f,
                    0.0f, 0.0f, 0.0f, 1.0f
                );
        }

        /// <summary>
        /// Creates a 3D 4 * 4 homogeneous rotation matrix from an euler angle Z.
        /// </summary>
        /// <param name="AngleZ"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]        
        public static Mat4f CreateEulerAngleZ(float AngleZ)
        {
            float cosZ = (float)Math.Cos(AngleZ);
            float sinZ = (float)Math.Sin(AngleZ);

            return new Mat4f(
                    cosZ, sinZ, 0.0f, 0.0f,
                    -sinZ, cosZ, 0.0f, 0.0f,
                    0.0f, 0.0f, 1.0f, 0.0f,
                    0.0f, 0.0f, 0.0f, 1.0f
                );
        }

        /// <summary>
        /// Creates a 3D 4 * 4 homogeneous rotation matrix from euler angles (X * Y).
        /// </summary>
        /// <param name="AngleX"></param>
        /// <param name="AngleY"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]        
        public static Mat4f CreateEulerAngleXY(float AngleX, float AngleY)
        {
            float cosX = (float)Math.Cos(AngleX);
            float sinX = (float)Math.Sin(AngleX);
            float cosY = (float)Math.Cos(AngleY);
            float sinY = (float)Math.Sin(AngleY);

            return new Mat4f(
                    cosY, -sinX * sinY, cosX * sinY, 0.0f,
                    0.0f,  cosX,      sinX,          0.0f,
                    -sinY, -sinX * cosY, cosX * cosY, 0.0f,
                    0.0f, 0.0f, 0.0f, 1.0f
                );
        }

        /// <summary>
        /// Creates a 3D 4 * 4 homogeneous rotation matrix from euler angles (Y * X).
        /// </summary>
        /// <param name="AngleY"></param>
        /// <param name="AngleX"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f CreateEulerAngleYX(float AngleY, float AngleX)
        {
            float cosX = (float)Math.Cos(AngleX);
            float sinX = (float)Math.Sin(AngleX);
            float cosY = (float)Math.Cos(AngleY);
            float sinY = (float)Math.Sin(AngleY);

            return new Mat4f(
                    cosY, 0.0f, sinY, 0.0f,
                    -sinX * sinY, cosX, sinX * cosY, 0.0f,
                    -cosX * sinY, -sinX, cosX * cosY, 0.0f,
                    0.0f, 0.0f, 0.0f, 1.0f
                );

        }

        /// <summary>
        /// Creates a 3D 4 * 4 homogeneous rotation matrix from euler angles (X * Z).
        /// </summary>
        /// <param name="AngleX"></param>
        /// <param name="AngleZ"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f CreateEulerAngleXZ(float AngleX, float AngleZ)
        {
            return CreateEulerAngleX(AngleX) * CreateEulerAngleZ(AngleZ);
        }

        /// <summary>
        /// Creates a 3D 4 * 4 homogeneous rotation matrix from euler angles (Z * X).
        /// </summary>
        /// <param name="AngleZ"></param>
        /// <param name="AngleX"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f CreateEulerAngleZX(float AngleZ, float AngleX)
        {
            return CreateEulerAngleZ(AngleZ) * CreateEulerAngleX(AngleX);
        }

        /// <summary>
        /// Creates a 3D 4 * 4 homogeneous rotation matrix from euler angles (Y * Z).
        /// </summary>
        /// <param name="AngleY"></param>
        /// <param name="AngleZ"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f CreateEulerAngleYZ(float AngleY, float AngleZ)
        {
            float cosY = (float)Math.Cos(AngleY);
            float sinY = (float)Math.Sin(AngleY);
            float cosZ = (float)Math.Cos(AngleZ);
            float sinZ = (float)Math.Sin(AngleZ);

            throw new NotImplementedException();

            //return new Mat4f(

            //    );

        }

        /// <summary>
        /// Creates a 3D 4 * 4 homogeneous rotation matrix from euler angles (Z * Y).
        /// </summary>
        /// <param name="AngleZ"></param>
        /// <param name="AngleY"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f CreateEulerAngleZY(float AngleZ, float AngleY)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a 3D 4 * 4 homogeneous rotation matrix from euler angles (Y * X * Z).
        /// CreateYawPitchRoll and CreateEulerAngleYXZ are the same result.
        /// </summary>
        /// <param name="yaw"></param>
        /// <param name="pitch"></param>
        /// <param name="roll"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f CreateEulerAngleYXZ(float yaw, float pitch, float roll)
        {
            float tmp_ch = (float)Math.Cos(yaw);
            float tmp_sh = (float)Math.Sin(yaw);
            float tmp_cp = (float)Math.Cos(pitch);
            float tmp_sp = (float)Math.Sin(pitch);
            float tmp_cb = (float)Math.Cos(roll);
            float tmp_sb = (float)Math.Sin(roll);

            Mat4f result; // = Mat4f.Zero;
            result.c0.x = tmp_ch * tmp_cb + tmp_sh * tmp_sp * tmp_sb;
            result.c0.y = tmp_sb * tmp_cp;
            result.c0.z = -tmp_sh * tmp_cb + tmp_ch * tmp_sp * tmp_sb;
            result.c0.w = 0.0f;
            result.c1.x = -tmp_ch * tmp_sb + tmp_sh * tmp_sp * tmp_cb;
            result.c1.y = tmp_cb * tmp_cp;
            result.c1.z = tmp_sb * tmp_sh + tmp_ch * tmp_sp * tmp_cb;
            result.c1.w = 0.0f;
            result.c2.x = tmp_sh * tmp_cp;
            result.c2.y = -tmp_sp;
            result.c2.z = tmp_ch * tmp_cp;
            result.c2.w = 0.0f;

            result.c3 = Vec4f.UnitW;

            return result;
        }

        /// <summary>
        /// Creates a 3D 4 * 4 homogeneous rotation matrix from euler angles (Y * X * Z).
        /// CreateYawPitchRoll and CreateEulerAngleYXZ are the same result.
        /// </summary>
        /// <param name="yaw"></param>
        /// <param name="pitch"></param>
        /// <param name="roll"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f CreateYawPitchRoll(float yaw, float pitch, float roll)
        {
            float tmp_ch = (float)Math.Cos(yaw);
            float tmp_sh = (float)Math.Sin(yaw);
            float tmp_cp = (float)Math.Cos(pitch);
            float tmp_sp = (float)Math.Sin(pitch);
            float tmp_cb = (float)Math.Cos(roll);
            float tmp_sb = (float)Math.Sin(roll);

            Mat4f result; // = Mat4f.Zero;
            result.c0.x = tmp_ch * tmp_cb + tmp_sh * tmp_sp * tmp_sb;
            result.c0.y = tmp_sb * tmp_cp;
            result.c0.z = -tmp_sh * tmp_cb + tmp_ch * tmp_sp * tmp_sb;
            result.c0.w = 0.0f;
            result.c1.x = -tmp_ch * tmp_sb + tmp_sh * tmp_sp * tmp_cb;
            result.c1.y = tmp_cb * tmp_cp;
            result.c1.z = tmp_sb * tmp_sh + tmp_ch * tmp_sp * tmp_cb;
            result.c1.w = 0.0f;
            result.c2.x = tmp_sh * tmp_cp;
            result.c2.y = -tmp_sp;
            result.c2.z = tmp_ch * tmp_cp;
            result.c2.w = 0.0f;

            result.c3 = Vec4f.UnitW;

            return result;

        }

        /// <summary>
        /// Creates a 3D 4 * 4 homogeneous rotation matrix from euler angles (Y * X * Z).
        /// </summary>
        /// <param name="angles"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f CreateOrientate4(Vec3f angles)
        {
            return CreateYawPitchRoll(angles.z, angles.x, angles.y);
        }

        #endregion

        #region Static Arithmetic Functions

        /// <summary>
        /// Adds two matrices component wise.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f Add(Mat4f left, Mat4f right)
        {
            return new Mat4f()
            {
                c0 = Vec4f.Add(left.c0, right.c0),
                c1 = Vec4f.Add(left.c1, right.c1),
                c2 = Vec4f.Add(left.c2, right.c2),
                c3 = Vec4f.Add(left.c3, right.c3)
            };
        }
        /// <summary>
        /// Adds two matrices componentwise.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add(ref Mat4f left, ref Mat4f right, out Mat4f result)
        {
            Vec4f.Add(ref left.c0, ref right.c0, out result.c0);
            Vec4f.Add(ref left.c1, ref right.c1, out result.c1);
            Vec4f.Add(ref left.c2, ref right.c2, out result.c2);
            Vec4f.Add(ref left.c3, ref right.c3, out result.c3);
        }
        /// <summary>
        /// Subtracts one matrix from another componentwise.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f Subtract(Mat4f left, Mat4f right)
        {
            return new Mat4f()
            {
                c0 = Vec4f.Subtract(left.c0, right.c0),
                c1 = Vec4f.Subtract(left.c1, right.c1),
                c2 = Vec4f.Subtract(left.c2, right.c2),
                c3 = Vec4f.Subtract(left.c3, right.c3)
            };
        }

        /// <summary>
        /// Subtracts one matrix from another componentwise.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subtract(ref Mat4f left, ref Mat4f right, out Mat4f result)
        {
            Vec4f.Subtract(ref left.c0, ref right.c0, out result.c0);
            Vec4f.Subtract(ref left.c1, ref right.c1, out result.c1);
            Vec4f.Subtract(ref left.c2, ref right.c2, out result.c2);
            Vec4f.Subtract(ref left.c3, ref right.c3, out result.c3);
        }
        /// <summary>
        /// Multiplies two matrices with each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f Multiply(Mat4f left, Mat4f right)
        {
            return left * right;
        }

        /// <summary>
        /// Multiplies two matrices with each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Mat4f left, ref Mat4f right, out Mat4f result)
        {
            result = left * right;
            //TODO: Make this more efficient?
        }
        /// <summary>
        /// Divides a matrix from another.
        /// BEWARE: Creates a tmp inverse matrix with is very costly...
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f Divide(Mat4f left, Mat4f right)
        {
            return left / right;
            //return new Mat4f()
            //{
            //    c0 = Vec4f.Divide(left.c0, right.c0),
            //    c1 = Vec4f.Divide(left.c1, right.c1),
            //    c2 = Vec4f.Divide(left.c2, right.c2)
            //};
        }

        /// <summary>
        /// Divides a matrix from another.
        /// BEWARE: Creates a tmp inverse matrix with is very costly...
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Divide(ref Mat4f left, ref Mat4f right, out Mat4f result)
        {
            result = left / right;
            //TODO: Make this more efficient?
        }

        #endregion

        #region Static Operator Functions

        /// <summary>
        /// Adds two matrices to each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f operator +(Mat4f left, Mat4f right)
        {
            return new Mat4f()
            {
                c0 = left.c0 + right.c0,
                c1 = left.c1 + right.c1,
                c2 = left.c2 + right.c2,
                c3 = left.c3 + right.c3
            };
        }
        /// <summary>
        /// Subtracts one matrix from another.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f operator -(Mat4f left, Mat4f right)
        {
            return new Mat4f()
            {
                c0 = left.c0 - right.c0,
                c1 = left.c1 - right.c1,
                c2 = left.c2 - right.c2,
                c3 = left.c3 - right.c3
            };
        }

        /// <summary>
        /// Multiplies two matrices with each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f operator *(Mat4f left, Mat4f right)
        {
            Vec4f SrcA0 = left.c0;
            Vec4f SrcA1 = left.c1;
            Vec4f SrcA2 = left.c2;
            Vec4f SrcA3 = left.c3;

            Vec4f SrcB0 = right.c0;
            Vec4f SrcB1 = right.c1;
            Vec4f SrcB2 = right.c2;
            Vec4f SrcB3 = right.c3;

            Mat4f result;

            result.c0 = SrcA0 * SrcB0.x + SrcA1 * SrcB0.y + SrcA2 * SrcB0.z + SrcA3 * SrcB0.w;
            result.c1 = SrcA0 * SrcB1.x + SrcA1 * SrcB1.y + SrcA2 * SrcB1.z + SrcA3 * SrcB1.w;
            result.c2 = SrcA0 * SrcB2.x + SrcA1 * SrcB2.y + SrcA2 * SrcB2.z + SrcA3 * SrcB2.w;
            result.c3 = SrcA0 * SrcB3.x + SrcA1 * SrcB3.y + SrcA2 * SrcB3.z + SrcA3 * SrcB3.w;

            return result;

        }
        /// <summary>
        /// Divides one matrix with another.
        /// BEWARE: Creates a tmp inverse matrix with is very costly...
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f operator /(Mat4f left, Mat4f right)
        {
            return left * Mat4f.Inverse(right);
        }

        /// <summary>
        /// Adds a scalar to a matrix.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f operator +(Mat4f m, float scalar)
        {
            return new Mat4f()
            {
                c0 = m.c0 + scalar,
                c1 = m.c1 + scalar,
                c2 = m.c2 + scalar,
                c3 = m.c3 + scalar
            };
        }
        /// <summary>
        /// Subtracts a scalar from a matrix.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f operator -(Mat4f m, float scalar)
        {
            return new Mat4f()
            {
                c0 = m.c0 - scalar,
                c1 = m.c1 - scalar,
                c2 = m.c2 - scalar,
                c3 = m.c3 - scalar
            };
        }
        /// <summary>
        /// Multiplies a scalar with a matrix.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f operator *(Mat4f m, float scalar)
        {
            return new Mat4f()
            {
                c0 = m.c0 * scalar,
                c1 = m.c1 * scalar,
                c2 = m.c2 * scalar,
                c3 = m.c3 * scalar
            };
        }
        /// <summary>
        /// Divides a matrix with scalar.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f operator /(Mat4f m, float scalar)
        {
            return new Mat4f()
            {
                c0 = m.c0 / scalar,
                c1 = m.c1 / scalar,
                c2 = m.c2 / scalar,
                c3 = m.c3 / scalar
            };
        }

        /// <summary>
        /// Adds a matrix to a scalar.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f operator +(float scalar, Mat4f m)
        {
            return new Mat4f()
            {
                c0 = m.c0 + scalar,
                c1 = m.c1 + scalar,
                c2 = m.c2 + scalar,
                c3 = m.c3 + scalar
            };
        }
        /// <summary>
        /// Subtracts a matrix from a scalar.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f operator -(float scalar, Mat4f m)
        {
            return new Mat4f()
            {
                c0 = scalar - m.c0,
                c1 = scalar - m.c1,
                c2 = scalar - m.c2,
                c3 = scalar - m.c3
            };
        }
        /// <summary>
        /// Multiplies a matrix with a scalar.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f operator *(float scalar, Mat4f m)
        {
            return new Mat4f()
            {
                c0 = m.c0 * scalar,
                c1 = m.c1 * scalar,
                c2 = m.c2 * scalar,
                c3 = m.c3 * scalar
            };
        }
        /// <summary>
        /// Divides a scalar with a matrix.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f operator /(float scalar, Mat4f m)
        {
            return new Mat4f()
            {
                c0 = scalar / m.c0,
                c1 = scalar / m.c1,
                c2 = scalar / m.c2,
                c3 = scalar / m.c3
            };
        }

        /// <summary>
        /// Multiplies a matrix with a row vector returning column vector.
        /// </summary>
        /// <param name="m">Matrix</param>
        /// <param name="v">Row Vector</param>
        /// <returns>Column vector.</returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4f operator *(Mat4f m, Vec4f v)
        {
            return new Vec4f()
            {
                x = m.c0.x * v.x + m.c1.x * v.y + m.c2.x * v.z + m.c3.x * v.w,                
                y = m.c0.y * v.x + m.c1.y * v.y + m.c2.y * v.z + m.c3.y * v.w,
                z = m.c0.z * v.x + m.c1.z * v.y + m.c2.z * v.z + m.c3.z * v.w,
                w = m.c0.w * v.x + m.c1.w * v.y + m.c2.w * v.z + m.c3.w * v.w
            };
        }
        /// <summary>
        /// Multiplies a column vector with a matrix returing row vector.
        /// </summary>
        /// <param name="v">Column Vector</param>
        /// <param name="m">Matrix</param>
        /// <returns>Row Vector</returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4f operator *(Vec4f v, Mat4f m)
        {
            return new Vec4f()
            {
                x = m.c0.x * v.x + m.c0.y * v.y + m.c0.z * v.z + m.c0.w * v.w,
                y = m.c1.x * v.x + m.c1.y * v.y + m.c1.z * v.z + m.c1.w * v.w,
                z = m.c2.x * v.x + m.c2.y * v.y + m.c2.z * v.z + m.c2.w * v.w,
                w = m.c3.x * v.x + m.c3.y * v.y + m.c3.z * v.z + m.c3.w * v.w
            };
        }
        /// <summary>
        /// Divides a matrix with a row vector returning column vector.
        /// BEWARE: Creates a tmp inverse matrix with is very costly...
        /// </summary>
        /// <param name="m">Matrix</param>
        /// <param name="v">row vector</param>
        /// <returns>column vector.</returns>
        public static Vec4f operator /(Mat4f m, Vec4f v)
        {
            var inverse = Mat4f.Inverse(m);

            return inverse * v;
        }

        /// <summary>
        /// Divides a column vector with a matrix returning a 
        /// BEWARE: Creates a tmp inverse matrix with is very costly...
        /// </summary>
        /// <param name="v">Column Vector</param>
        /// <param name="m">Matrix</param>
        /// <returns>Row Column</returns>
        public static Vec4f operator /(Vec4f v, Mat4f m)
        {
            var inverse = Mat4f.Inverse(m);

            return v * inverse;
        }

        /// <summary>
        /// Negates a matrix component wise. 
        /// (Not sure if thats mathematically sound thou.)
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f operator -(Mat4f m)
        {
            return new Mat4f()
            {
                c0 = -m.c0,
                c1 = -m.c1,
                c2 = -m.c2,
                c3 = -m.c3
            };
        }

        /// <summary>
        /// Compares two matrices for equality
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Mat4f left, Mat4f right)
        {
            return
                left.c0.x == right.c0.x &&
                left.c0.y == right.c0.y &&
                left.c0.z == right.c0.z &&
                left.c0.w == right.c0.w &&
                left.c1.x == right.c1.x &&
                left.c1.y == right.c1.y &&
                left.c1.z == right.c1.z &&
                left.c1.w == right.c1.w &&
                left.c2.x == right.c2.x &&
                left.c2.y == right.c2.y &&
                left.c2.z == right.c2.z &&
                left.c2.w == right.c2.w &&
                left.c3.x == right.c3.x &&
                left.c3.y == right.c3.y &&
                left.c3.z == right.c3.z &&
                left.c3.w == right.c3.w;
        }

        /// <summary>
        /// Compares two matrices for inequality.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Mat4f left, Mat4f right)
        {
            return
                left.c0.x != right.c0.x ||
                left.c0.y != right.c0.y ||
                left.c0.z != right.c0.z ||
                left.c0.w != right.c0.w ||
                left.c1.x != right.c1.x ||
                left.c1.y != right.c1.y ||
                left.c1.z != right.c1.z ||
                left.c1.w != right.c1.w ||
                left.c2.x != right.c2.x ||
                left.c2.y != right.c2.y ||
                left.c2.z != right.c2.z ||
                left.c2.w != right.c2.w ||
                left.c3.x != right.c3.x ||
                left.c3.y != right.c3.y ||
                left.c3.z != right.c3.z ||
                left.c3.w != right.c3.w;
        }

        /// <summary>
        /// Implicit conversion from Mat2f to Mat4f.
        /// c3.w = 1.0f
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Mat4f(Mat2f m)
        {
            return new Mat4f()
            {
                c0 = new Vec4f() { x = m.c0.x, y = m.c0.y, z = 0.0f, w = 0.0f},
                c1 = new Vec4f() { x = m.c1.x, y = m.c1.y, z = 0.0f, w = 0.0f },
                c2 = Vec4f.Zero,
                c3 = Vec4f.UnitW,
            };
        }
        /// <summary>
        /// Implicit conversion from Mat3f to Mat4f.
        /// c3.w = 1.0f
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Mat4f(Mat3f m)
        {
            return new Mat4f()
            {
                c0 = new Vec4f(m.c0),
                c1 = new Vec4f(m.c1),
                c2 = new Vec4f(m.c2),
                c3 = Vec4f.UnitW,
            };
        }

        #endregion

        #region Object Overloads

        /// <summary>
        /// Returns the hashcode of this matrix.
        /// </summary>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return c0.GetHashCode() ^ c1.GetHashCode() ^ c2.GetHashCode() ^ c3.GetHashCode();
        }
        /// <summary>
        /// Returns a string representation of this matrix.
        /// </summary>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return string.Format(
                "{0}\n{1}\n{2}\n{3}", c0, c1, c2, c3);
        }

        /// <summary>
        /// Compares this matrix with another object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            if (obj is Mat4f)
                return Equals((Mat4f)obj);
            else
                return false;
        }

        /// <summary>
        /// Compares this matrix with another matrix.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Mat4f other)
        {
            return
                this.c0.x == other.c0.x &&
                this.c0.y == other.c0.y &&
                this.c0.z == other.c0.z &&
                this.c0.w == other.c0.w &&
                this.c1.x == other.c1.x &&
                this.c1.y == other.c1.y &&
                this.c1.z == other.c1.z &&
                this.c1.w == other.c1.w &&
                this.c2.x == other.c2.x &&
                this.c2.y == other.c2.y &&
                this.c2.z == other.c2.z &&
                this.c2.w == other.c2.w &&
                this.c3.x == other.c3.x &&
                this.c3.y == other.c3.y &&
                this.c3.z == other.c3.z &&
                this.c3.w == other.c3.w;
        }

        #endregion        

        #region Debug Print Instance

        [Conditional("DEBUG")]
        public void DebugPrintArray()
        {
            var size = Marshal.SizeOf(this);
            var elements = size / sizeof(float);

            unsafe
            {
                fixed (float* ptr = &c0.x)
                {
                    for (int i = 0; i < elements; i++)
                        Console.WriteLine("{0:0.000000}", ptr[i]);
                }
            }
        }

        [Conditional("DEBUG")]
        public void DebugPrintRowMajor()
        {
            var size = Marshal.SizeOf(this);
            var elements = size / sizeof(float);

            unsafe
            {
                fixed (float* ptr = &c0.x)
                {
                    for (int i = 0; i < elements; i += 4)
                        Console.WriteLine("| {0:0.0000} | {1:0.0000} | {2:0.0000} | {3:0.0000} | ",
                            ptr[i], ptr[i + 1], ptr[i + 2], ptr[i + 3]);
                    //Console.WriteLine("{0}", ptr[i]);

                }
            }
        }

        [Conditional("DEBUG")]
        public void DebugPrintColumnMajor()
        {
            var size = Marshal.SizeOf(this);
            var elements = size / sizeof(float);

            unsafe
            {
                fixed (float* ptr = &c0.x)
                {
                    for (int i = 0; i < 4; i++)
                        Console.WriteLine("| {0:0.0000} | {1:0.0000} | {2:0.0000} | {3:0.0000} | ",
                            ptr[i], ptr[i + 4], ptr[i + 8], ptr[i + 12]);
                    //Console.WriteLine("{0}", ptr[i]);

                }
            }
        }

        //[Conditional("DEBUG")]
        public string DebugDisplayHelper()
        {
            var size = Marshal.SizeOf(this);
            var elements = size / sizeof(float);

            var sb = new StringBuilder();

            unsafe
            {
                fixed (float* ptr = &c0.x)
                {
                    for (int i = 0; i < 4; i++)
                        sb.Append(string.Format("| {0:0.0000} | {1:0.0000} | {2:0.0000} | {3:0.0000} | ",
                            ptr[i], ptr[i + 4], ptr[i + 8], ptr[i + 12]));
                    //Console.WriteLine("{0}", ptr[i]);
                }
            }

            return sb.ToString();
        }

        #endregion

        #region IGLMatrix Implementation

        /// <summary>
        /// Number of columns in this matrix.
        /// </summary>
        [DebuggerNonUserCode()]
        int IGLMatrix.ColumnCount
        {
            get { return 4; }
        }

        /// <summary>
        /// Number of rows in this matrix.
        /// </summary>
        [DebuggerNonUserCode()]
        int IGLMatrix.RowCount
        {
            get { return 4; }
        }

        /// <summary>
        /// Is this a square matrix aka columncount=rowcount
        /// </summary>
        [DebuggerNonUserCode()]
        bool IGLMatrix.IsSquareMatrix
        {
            get { return true; }
        }

        /// <summary>
        /// Returns the dotnet type of this components.
        /// </summary>
        [DebuggerNonUserCode()]
        Type IGLMath.BaseType
        {
            get { return typeof(Mat4f); }
        }

        /// <summary>
        /// The number of components totaly in this matrix.
        /// </summary>
        [DebuggerNonUserCode()]
        int IGLMath.ComponentCount
        {
            get { return 16; }
        }

        public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Mat4f));

        /// <summary>
        /// Returns the inmemory size in bytes of this matrix.
        /// </summary>
        [DebuggerNonUserCode()]
        int IGLMath.SizeInBytes
        {
            get { return Mat4f.SizeInBytes; }
        }

        /// <summary>
        /// Returns the gl enum for base compoenent.
        /// </summary>
        [DebuggerNonUserCode()]
        int IGLMath.GLBaseType
        {
            get { return GLConstants.GL_BASE_FLOAT; }
        }

        /// <summary>
        /// Returns the OpenGL attribute type enum
        /// </summary>
        [DebuggerNonUserCode()]
        int IGLMath.GLAttributeType
        {
            get { return GLConstants.FLOAT_MAT4; }
        }

        /// <summary>
        /// Returns the opengl uniform type enum.
        /// </summary>
        [DebuggerNonUserCode()]
        int IGLMath.GLUniformType
        {
            get { return GLConstants.FLOAT_MAT4; }
        }

        /// <summary>
        /// Returns wether this is a matrix (true)
        /// </summary>
        [DebuggerNonUserCode()]
        bool IGLMath.IsMatrix
        {
            get { return true; }
        }

        #endregion

        #region IGenericStream Implementation

        //[DebuggerNonUserCode()]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public void WriteStream(System.IO.BinaryWriter writer, object matrix)
        //{
        //    Mat4f m = (Mat4f)matrix;

        //    writer.Write(m.c0.x);
        //    writer.Write(m.c0.y);
        //    writer.Write(m.c0.z);
        //    writer.Write(m.c0.w);
        //    writer.Write(m.c1.x);
        //    writer.Write(m.c1.y);
        //    writer.Write(m.c1.z);
        //    writer.Write(m.c1.w);
        //    writer.Write(m.c2.x);
        //    writer.Write(m.c2.y);
        //    writer.Write(m.c2.z);
        //    writer.Write(m.c2.w);
        //    writer.Write(m.c3.x);
        //    writer.Write(m.c3.y);
        //    writer.Write(m.c3.z);
        //    writer.Write(m.c3.w);
        //}

        /// <summary>
        /// Writes matrix to stream.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="vec"></param>
        [DebuggerNonUserCode()]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)] // not working with unsafe code!
        public unsafe void WriteStream(System.IO.BinaryWriter writer, object matrix)
        {            
            Mat4f m = (Mat4f)matrix;

            var buf = new byte[Mat4f.SizeInBytes];
            float* ptr = &m.c0.x;

            Marshal.Copy((IntPtr)ptr, buf, 0, buf.Length);
            writer.Write(buf, 0, buf.Length);

        }

        /// <summary>
        /// Reads in a new vector from stream.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)] // not working with unsafe code!
        public unsafe object ReadStream(System.IO.BinaryReader reader)
        {
            var buf = new byte[Mat4f.SizeInBytes];

            reader.Read(buf, 0, buf.Length);

            Mat4f result;

            fixed (byte* ptr = &buf[0])
            {
                Mat4f* p = (Mat4f*)ptr;

                result = *p;
            }

            return result;
        }

        #endregion

    }
}
