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
    public partial struct Mat4f : IEquatable<Mat4f>
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
        public static implicit operator Mat4f(Mat2f m)
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
        public static implicit operator Mat4f(Mat3f m)
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
    }
}
