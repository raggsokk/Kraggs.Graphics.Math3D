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
    public partial struct Mat3f : IEquatable<Mat3f>, IBinaryStreamMath3D<Mat3f>, IGLTypeMath3D, IGLMatrix, IGenericStream
    {
        /// <summary>
        /// The first Column.
        /// </summary>
        public Vec3f c0;
        /// <summary>
        /// The Second Column.
        /// </summary>
        public Vec3f c1;
        /// <summary>
        /// The Third Column.
        /// </summary>
        public Vec3f c2;

        #region Properties

        public static readonly Mat3f Zero = new Mat3f() { c0 = Vec3f.Zero, c1 = Vec3f.Zero, c2 = Vec3f.Zero };
        public static readonly Mat3f Identity = new Mat3f() { c0 = Vec3f.UnitX, c1 = Vec3f.UnitY, c2 = Vec3f.UnitZ };

        /// <summary>
        /// Returns the row0 of this matrix.
        /// It also gives a nice debug view of this matrix.
        /// </summary>
        [DebuggerNonUserCode()]
        public Vec3f Row0
        {
            get
            {
                return new Vec3f() { x = c0.x, y = c1.x, z = c2.x};
            }
        }

        /// <summary>
        /// Returns the row1 of this matrix.
        /// It also gives a nice debug view of this matrix.
        /// </summary>
        [DebuggerNonUserCode()]
        public Vec3f Row1
        {
            get
            {
                return new Vec3f() { x = c0.y, y = c1.y, z = c2.y};
            }
        }
        /// <summary>
        /// Returns the row2 of this matrix.
        /// It also gives a nice debug view of this matrix.
        /// </summary>
        [DebuggerNonUserCode()]
        public Vec3f Row2
        {
            get
            {
                return new Vec3f() { x = c0.z, y = c1.z, z = c2.z};
            }
        }

        /// <summary>
        /// Returns the scale in this matrix as a vec3f
        /// </summary>
        [DebuggerNonUserCode()]
        public Vec3f Scale
        {
            get
            {
                return new Vec3f() { x = c0.x, y = c1.y, z = c2.z };
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
        public Mat3f(float scalar)
        {
            c0 = Vec3f.Zero;
            c1 = Vec3f.Zero;
            c2 = Vec3f.Zero;
            c0.x = scalar;
            c1.y = scalar;
            c2.z = scalar;            
        }

        /// <summary>
        /// Returns a new matrix from individuall floating components.
        /// </summary>
        /// <param name="x0">x component of column 0</param>
        /// <param name="y0">y component of column 0</param>
        /// <param name="z0">z component of column 0</param>
        /// <param name="x1">x component of column 1</param>
        /// <param name="y1">y component of column 1</param>
        /// <param name="z1">z component of column 1</param>
        /// <param name="x2">x component of column 2</param>
        /// <param name="y2">y component of column 2</param>
        /// <param name="z2">z component of column 2</param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Mat3f(
            float x0, float y0, float z0,
            float x1, float y1, float z1,
            float x2, float y2, float z2)
        {
            c0 = new Vec3f() { x = x0, y = y0, z = z0 };
            c1 = new Vec3f() { x = x1, y = y1, z = z1 };
            c2 = new Vec3f() { x = x2, y = y2, z = z2 };
        }
        /// <summary>
        /// Creates a matrix from Column vectors.
        /// </summary>
        /// <param name="c0"></param>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Mat3f(Vec3f c0, Vec3f c1, Vec3f c2)
        {
            this.c0 = c0;
            this.c1 = c1;
            this.c2 = c2;
        }

        #endregion

        #region Instance Functions

        /// <summary>
        /// Calculates the Determinant.
        /// </summary>
        [DebuggerNonUserCode()]
        public float Determinant
        {
            get
            {
                return
                    +c0.x * (c1.y * c2.z - c2.y * c1.z)
                    - c1.x * (c0.y * c2.z - c2.y * c0.z)
                    + c2.x * (c0.y * c1.z - c1.y * c0.z);
            }
        }

        /// <summary>
        /// Dumps the matrix from memory to array.
        /// </summary>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public unsafe float[] ToArray()
        {
            var result = new float[9];

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
            float t = c0.y;
            c0.y = c1.x;
            c1.x = t;

            t = c0.z;
            c0.z = c2.x;
            c2.x = t;

            t = c1.z;
            c1.z = c2.y;
            c2.y = t;
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
        public static Mat3f Transpose(Mat3f m)
        {
            return new Mat3f(
                m.c0.x, m.c1.x, m.c2.x,
                m.c0.y, m.c1.y, m.c2.y,
                m.c0.z, m.c1.z, m.c2.z
                );
        }

        /// <summary>
        /// Transposes a Matrix
        /// </summary>
        /// <param name="m">Matrix to get transpose version of.</param>
        /// <param name="result">The transpose version of m.</param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Transpose(ref Mat3f m, out Mat3f result)
        {
            result.c0.x = m.c0.x;
            result.c0.y = m.c1.x;
            result.c0.z = m.c2.x;

            result.c1.x = m.c0.y;
            result.c1.y = m.c1.y;
            result.c1.z = m.c2.y;

            result.c2.x = m.c0.z;
            result.c2.y = m.c1.z;
            result.c2.z = m.c2.z;
        }

        /// <summary>
        /// Calculates the OuterProduct of column c and Row r.
        /// </summary>
        /// <param name="c">Column</param>
        /// <param name="r">Row</param>
        /// <returns>the resulting outerproduct matrix.</returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat3f OuterProduct(Vec3f c, Vec3f r)
        {
            Mat3f m; // = Mat3f.Zero;
            m.c0 = c * r.x;
            m.c1 = c * r.y;
            m.c2 = c * r.z;
            return m;
        }

        /// <summary>
        /// Calculates the inverse of a matrix.
        /// </summary>
        /// <param name="m">Matrix to get inverse of.</param>
        /// <returns>the inverse matrix of m.</returns>
        public static Mat3f Inverse(Mat3f m)
        {
            var determinant = m.Determinant;

            Mat3f inverse;

            inverse.c0.x = +(m.c1.y * m.c2.z - m.c2.y * m.c1.z);
            inverse.c1.x = -(m.c1.x * m.c2.z - m.c2.x * m.c1.z);
            inverse.c2.x = +(m.c1.x * m.c2.y - m.c2.x * m.c1.y);
            inverse.c0.y = -(m.c0.y * m.c2.z - m.c2.y * m.c0.z);
            inverse.c1.y = +(m.c0.x * m.c2.z - m.c2.x * m.c0.z);
            inverse.c2.y = -(m.c0.x * m.c2.y - m.c2.x * m.c0.y);
            inverse.c0.z = +(m.c0.y * m.c1.z - m.c1.y * m.c0.z);
            inverse.c1.z = -(m.c0.x * m.c1.z - m.c1.x * m.c0.z);
            inverse.c2.z = +(m.c0.x * m.c1.y - m.c1.x * m.c0.y);

            
            //inverse /= determinant;            

            return inverse / determinant;
        }

        /// <summary>
        /// Creates a affice Inverse of provided matrix.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat3f AffineInverse(Mat3f m)
        {
            Mat3f result = m;
            result.c2 = Vec3f.UnitZ;
            result.Transpose();
            Vec3f translation = result * new Vec3f(-(Vec2f)m.c2, m.c2.z);
            result.c2 = translation;
            return result;
        }

        public static Mat3f InverseTranspose(Mat3f m)
        {
            var determinant = m.Determinant;

            Mat3f inverse;

            inverse.c0.x = +(m.c1.y * m.c2.z - m.c2.y * m.c1.z);
            inverse.c0.y = -(m.c1.x * m.c2.z - m.c2.x * m.c1.z);
            inverse.c0.z = +(m.c1.x * m.c2.y - m.c2.x * m.c1.y);
            inverse.c1.x = -(m.c0.y * m.c2.z - m.c2.y * m.c0.z);
            inverse.c1.y = +(m.c0.x * m.c2.z - m.c2.x * m.c0.z);
            inverse.c1.z = -(m.c0.x * m.c2.y - m.c2.x * m.c0.y);
            inverse.c2.x = +(m.c0.y * m.c1.z - m.c1.y * m.c0.z);
            inverse.c2.y = -(m.c0.x * m.c1.z - m.c1.x * m.c0.z);
            inverse.c2.z = +(m.c0.x * m.c1.y - m.c1.x * m.c0.y);
           
            return inverse / determinant;
        }

        #endregion

        #region Euler Angles

        /// <summary>
        /// Creates a 3D 3 * 3 homogeneous rotation matrix from an euler angle.
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat3f CreateOrientate3(float angle)
        {
            float c = (float)Math.Cos(angle);
            float s = (float)Math.Cos(angle);

            Mat3f result;
            result.c0.x = c;
            result.c0.y = s;
            result.c0.z = 0.0f;
            result.c1.x = -s;
            result.c1.y = c;
            result.c1.z = 0.0f;
            result.c2 = Vec3f.UnitZ;

            return result;

        }

        /// <summary>
        /// Creates a 3D 3 * 3 rotation matrix from euler angles (Y * X * Z).
        /// </summary>
        /// <param name="angles"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat3f CreateOrientate3(Vec3f angles)
        {
            return (Mat3f)(Mat4f.CreateYawPitchRoll(angles.x, angles.y, angles.z));
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
        public static Mat3f Add(Mat3f left, Mat3f right)
        {
            return new Mat3f()
            {
                c0 = Vec3f.Add(left.c0, right.c0),
                c1 = Vec3f.Add(left.c1, right.c1),
                c2 = Vec3f.Add(left.c2, right.c2)
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
        public static void Add(ref Mat3f left, ref Mat3f right, out Mat3f result)
        {
            Vec3f.Add(ref left.c0, ref right.c0, out result.c0);
            Vec3f.Add(ref left.c1, ref right.c1, out result.c1);
            Vec3f.Add(ref left.c2, ref right.c2, out result.c2);
        }
        /// <summary>
        /// Subtracts one matrix from another componentwise.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat3f Subtract(Mat3f left, Mat3f right)
        {
            return new Mat3f()
            {
                c0 = Vec3f.Subtract(left.c0, right.c0),
                c1 = Vec3f.Subtract(left.c1, right.c1),
                c2 = Vec3f.Subtract(left.c2, right.c2)
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
        public static void Subtract(ref Mat3f left, ref Mat3f right, out Mat3f result)
        {
            Vec3f.Subtract(ref left.c0, ref right.c0, out result.c0);
            Vec3f.Subtract(ref left.c1, ref right.c1, out result.c1);
            Vec3f.Subtract(ref left.c2, ref right.c2, out result.c2);
        }
        /// <summary>
        /// Multiplies two matrices with each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat3f Multiply(Mat3f left, Mat3f right)
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
        public static void Multiply(ref Mat3f left, ref Mat3f right, out Mat3f result)
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
        public static Mat3f Divide(Mat3f left, Mat3f right)
        {
            return left / right;
            //return new Mat3f()
            //{
            //    c0 = Vec3f.Divide(left.c0, right.c0),
            //    c1 = Vec3f.Divide(left.c1, right.c1),
            //    c2 = Vec3f.Divide(left.c2, right.c2)
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
        public static void Divide(ref Mat3f left, ref Mat3f right, out Mat3f result)
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
        public static Mat3f operator +(Mat3f left, Mat3f right)
        {
            return new Mat3f()
            {
                c0 = left.c0 + right.c0,
                c1 = left.c1 + right.c1,
                c2 = left.c2 + right.c2
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
        public static Mat3f operator -(Mat3f left, Mat3f right)
        {
            return new Mat3f()
            {
                c0 = left.c0 - right.c0,
                c1 = left.c1 - right.c1,
                c2 = left.c2 - right.c2
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
        public static Mat3f operator *(Mat3f left, Mat3f right)
        {
            float SrcA00 = left.c0.x;
            float SrcA01 = left.c0.y;
            float SrcA02 = left.c0.z;
            float SrcA10 = left.c1.x;
            float SrcA11 = left.c1.y;
            float SrcA12 = left.c1.z;
            float SrcA20 = left.c2.x;
            float SrcA21 = left.c2.y;
            float SrcA22 = left.c2.z;

            float SrcB00 = right.c0.x;
            float SrcB01 = right.c0.y;
            float SrcB02 = right.c0.z;
            float SrcB10 = right.c1.x;
            float SrcB11 = right.c1.y;
            float SrcB12 = right.c1.z;
            float SrcB20 = right.c2.x;
            float SrcB21 = right.c2.y;
            float SrcB22 = right.c2.z;

            Mat3f result;

            result.c0.x = SrcA00 * SrcB00 + SrcA10 * SrcB01 + SrcA20 * SrcB02;
            result.c0.y = SrcA01 * SrcB00 + SrcA11 * SrcB01 + SrcA21 * SrcB02;
            result.c0.z = SrcA02 * SrcB00 + SrcA12 * SrcB01 + SrcA22 * SrcB02;
            result.c1.x = SrcA00 * SrcB10 + SrcA10 * SrcB11 + SrcA20 * SrcB12;
            result.c1.y = SrcA01 * SrcB10 + SrcA11 * SrcB11 + SrcA21 * SrcB12;
            result.c1.z = SrcA02 * SrcB10 + SrcA12 * SrcB11 + SrcA22 * SrcB12;
            result.c2.x = SrcA00 * SrcB20 + SrcA10 * SrcB21 + SrcA20 * SrcB22;
            result.c2.y = SrcA01 * SrcB20 + SrcA11 * SrcB21 + SrcA21 * SrcB22;
            result.c2.z = SrcA02 * SrcB20 + SrcA12 * SrcB21 + SrcA22 * SrcB22;

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
        public static Mat3f operator /(Mat3f left, Mat3f right)
        {
            return left * Mat3f.Inverse(right);
        }

        /// <summary>
        /// Adds a scalar to a matrix.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat3f operator +(Mat3f m, float scalar)
        {
            return new Mat3f()
            {
                c0 = m.c0 + scalar,
                c1 = m.c1 + scalar,
                c2 = m.c2 + scalar
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
        public static Mat3f operator -(Mat3f m, float scalar)
        {
            return new Mat3f()
            {
                c0 = m.c0 - scalar,
                c1 = m.c1 - scalar,
                c2 = m.c2 - scalar
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
        public static Mat3f operator *(Mat3f m, float scalar)
        {
            return new Mat3f()
            {
                c0 = m.c0 * scalar,
                c1 = m.c1 * scalar,
                c2 = m.c2 * scalar
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
        public static Mat3f operator /(Mat3f m, float scalar)
        {
            return new Mat3f()
            {
                c0 = m.c0 / scalar,
                c1 = m.c1 / scalar,
                c2 = m.c2 / scalar
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
        public static Mat3f operator +(float scalar, Mat3f m)
        {
            return new Mat3f()
            {
                c0 = m.c0 + scalar,
                c1 = m.c1 + scalar,
                c2 = m.c2 + scalar
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
        public static Mat3f operator -(float scalar, Mat3f m)
        {
            return new Mat3f()
            {
                c0 = scalar - m.c0,
                c1 = scalar - m.c1,
                c2 = scalar - m.c2
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
        public static Mat3f operator *(float scalar, Mat3f m)
        {
            return new Mat3f()
            {
                c0 = m.c0 * scalar,
                c1 = m.c1 * scalar,
                c2 = m.c2 * scalar
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
        public static Mat3f operator /(float scalar, Mat3f m)
        {
            return new Mat3f()
            {
                c0 = scalar / m.c0,
                c1 = scalar / m.c1,
                c2 = scalar / m.c2
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
        public static Vec3f operator *(Mat3f m, Vec3f v)
        {
            return new Vec3f()
            {
                x = m.c0.x * v.x + m.c1.x * v.y + m.c2.x * v.z,
                y = m.c0.y * v.x + m.c1.y * v.y + m.c2.y * v.z,
                z = m.c0.z * v.x + m.c1.z * v.y + m.c2.z * v.z
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
        public static Vec3f operator *(Vec3f v, Mat3f m)
        {
            return new Vec3f()
            {
                x = m.c0.x * v.x + m.c0.y * v.y + m.c0.z * v.z,
                y = m.c1.x * v.x + m.c1.y * v.y + m.c1.z * v.z,
                z = m.c2.x * v.x + m.c2.y * v.y + m.c2.z * v.z
            };
        }
        /// <summary>
        /// Divides a matrix with a row vector returning column vector.
        /// BEWARE: Creates a tmp inverse matrix with is very costly...
        /// </summary>
        /// <param name="m">Matrix</param>
        /// <param name="v">row vector</param>
        /// <returns>column vector.</returns>
        public static Vec3f operator /(Mat3f m, Vec3f v)
        {
            var inverse = Mat3f.Inverse(m);

            return inverse * v;
        }

        /// <summary>
        /// Divides a column vector with a matrix returning a 
        /// BEWARE: Creates a tmp inverse matrix with is very costly...
        /// </summary>
        /// <param name="v">Column Vector</param>
        /// <param name="m">Matrix</param>
        /// <returns>Row Column</returns>
        public static Vec3f operator /(Vec3f v, Mat3f m)
        {
            var inverse = Mat3f.Inverse(m);

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
        public static Mat3f operator -(Mat3f m)
        {
            return new Mat3f()
            {
                c0 = -m.c0,
                c1 = -m.c1,
                c2 = -m.c2
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
        public static bool operator ==(Mat3f left, Mat3f right)
        {
            return
                left.c0.x == right.c0.x &&
                left.c0.y == right.c0.y &&
                left.c0.z == right.c0.z &&
                left.c1.x == right.c1.x &&
                left.c1.y == right.c1.y &&
                left.c1.z == right.c1.z &&
                left.c2.x == right.c2.x &&
                left.c2.y == right.c2.y &&
                left.c2.z == right.c2.z;
        }

        /// <summary>
        /// Compares two matrices for inequality.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Mat3f left, Mat3f right)
        {
            return
                left.c0.x != right.c0.x ||
                left.c0.y != right.c0.y ||
                left.c0.z != right.c0.z ||
                left.c1.x != right.c1.x ||
                left.c1.y != right.c1.y ||
                left.c1.z != right.c1.z ||
                left.c2.x != right.c2.x ||
                left.c2.y != right.c2.y ||
                left.c2.z != right.c2.z;
        }

        /// <summary>
        /// Explicit conversion from Mat2f to Mat3f with emmpty values taken from identity matrix.
        /// Aka c2.z = 1.0f in this conversion.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Mat3f (Mat2f m)
        {
            return new Mat3f()
            {
                c0 = new Vec3f(m.c0),
                c1 = new Vec3f(m.c1),
                c2 = Vec3f.UnitZ
            };
        }

        /// <summary>
        /// Explicit conversion from Mat4f to Mat3f with possible dataloss.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Mat3f(Mat4f m)
        {
            return new Mat3f(
                m.c0.x, m.c0.y, m.c0.z,
                m.c1.x, m.c1.y, m.c1.z,
                m.c2.x, m.c2.y, m.c2.z
                );
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
            return c0.GetHashCode() ^ c1.GetHashCode() ^ c2.GetHashCode();
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
                "{0}\n{1}\n{2}", c0, c1, c2);
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
            if (obj is Mat3f)
                return Equals((Mat3f)obj);
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
        public bool Equals(Mat3f other)
        {
            return
                this.c0.x == other.c0.x &&
                this.c0.y == other.c0.y &&
                this.c0.z == other.c0.z &&
                this.c1.x == other.c1.x &&
                this.c1.y == other.c1.y &&
                this.c1.z == other.c1.z &&
                this.c2.x == other.c2.x &&
                this.c2.y == other.c2.y &&
                this.c2.z == other.c2.z;
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
                    for (int i = 0; i < elements; i += 3)
                        Console.WriteLine("| {0:0.0000} | {1:0.0000} | {2:0.0000} | ",
                            ptr[i], ptr[i + 1], ptr[i + 2]);
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
                    for (int i = 0; i < 3; i++)
                        Console.WriteLine("| {0:0.0000} | {1:0.0000} | {2:0.0000} | ",
                            ptr[i], ptr[i + 3], ptr[i + 6]);
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
                    for (int i = 0; i < 3; i++)
                        sb.Append(string.Format("| {0:0.0000} | {1:0.0000} | {2:0.0000} | ",
                            ptr[i], ptr[i + 3], ptr[i + 6]));
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
            get { return 3; }
        }

        /// <summary>
        /// Number of rows in this matrix.
        /// </summary>
        [DebuggerNonUserCode()]
        int IGLMatrix.RowCount
        {
            get { return 3; }
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
            get { return typeof(Mat3f); }
        }

        /// <summary>
        /// The number of components totaly in this matrix.
        /// </summary>
        [DebuggerNonUserCode()]
        int IGLMath.ComponentCount
        {
            get { return 9; }
        }

        public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Mat3f));

        /// <summary>
        /// Returns the inmemory size in bytes of this matrix.
        /// </summary>
        [DebuggerNonUserCode()]
        int IGLMath.SizeInBytes
        {
            get { return Mat3f.SizeInBytes; }
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
            get { return GLConstants.FLOAT_MAT3; }
        }

        /// <summary>
        /// Returns the opengl uniform type enum.
        /// </summary>
        [DebuggerNonUserCode()]
        int IGLMath.GLUniformType
        {
            get { return GLConstants.FLOAT_MAT3; }
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

        #region IGLTypeMath3D

        private static readonly IGLDescriptionMath3D GLTypeDescription = new Mat3fGLDescription();

        /// <summary>
        /// Returns an object with description of this GL Type.
        /// </summary>
        public IGLDescriptionMath3D GetGLTypeDescription
        {
            get
            {
                Debug.Assert(Marshal.SizeOf(typeof(Mat3fGLDescription)) == 0);

                return GetGLTypeDescription;
            }
        }

        /// <summary>
        /// Very private desc struct for this type.
        /// </summary>
        private struct Mat3fGLDescription : IGLDescriptionMath3D
        {
            Type IGLDescriptionMath3D.BaseType
            {
                get { return typeof(float); }
            }

            int IGLDescriptionMath3D.ComponentCount
            {
                get { return 9; }
            }

            int IGLDescriptionMath3D.SizeInBytes
            {
                get { return 36; }
            }

            int IGLDescriptionMath3D.GLBaseType
            {
                get { return GLConstants.GL_BASE_FLOAT; }
            }

            int IGLDescriptionMath3D.GLAttributeType
            {
                get { return GLConstants.FLOAT_MAT3; }
            }

            int IGLDescriptionMath3D.GLUniformType
            {
                get { return GLConstants.FLOAT_MAT3; }
            }

            bool IGLDescriptionMath3D.IsMatrix
            {
                get { return true; }
            }

            bool IGLDescriptionMath3D.IsRowMajor
            {
                get { return false; }
            }

            int IGLDescriptionMath3D.Columns
            {
                get { return 3; }
            }

            int IGLDescriptionMath3D.Rows
            {
                get { return 3; }
            }
        }

        #endregion

        #region IGenericStream Implementation

        /// <summary>
        /// Writes matrix to stream.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="vec"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obsolete("Use functions in IBinaryStreamMath3D instead")]
        void IGenericStream.WriteStream(System.IO.BinaryWriter writer, object matrix)
        {
            Mat3f m = (Mat3f)matrix;

            var arr = m.ToArray();

            for (int i = 0; i < arr.Length; i++)
                writer.Write(arr[i]);

            //writer.Write(m.c0.x);
            //writer.Write(m.c0.y);
            //writer.Write(m.c1.x);
            //writer.Write(m.c1.y);
        }

        /// <summary>
        /// Reads in a new vector from stream.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obsolete("Use functions in IBinaryStreamMath3D instead")]
        object IGenericStream.ReadStream(System.IO.BinaryReader reader)
        {
            return new Mat3f(
                reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(),
                reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(),
                reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        }

        #endregion

        #region IBinaryStreamMath3D Implementation

        /// <summary>
        /// Writes an array of Mat3f elements to a stream.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="elements"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        [DebuggerNonUserCode()]
        public unsafe void WriteStream(System.IO.Stream writer, Mat3f[] elements, int index, int length)
        {
            if (elements == null || elements.Length == 0)
                return;

            var elementCount = Math.Min(elements.Length, index + length) - index;
            //var sizeinbytes = Marshal.SizeOf(typeof(Mat4f));

            var buf = new byte[elementCount * Mat3f.SizeInBytes];

            fixed (float* ptr = &elements[index].c0.x)
            {
                Marshal.Copy((IntPtr)ptr, buf, 0, buf.Length);
            }

            writer.Write(buf, 0, buf.Length);
        }


        /// <summary>
        /// Writes an array of Mat3f's to a binary writer.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="elements"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        [DebuggerNonUserCode()]
        public void WriteStream(System.IO.BinaryWriter writer, Mat3f[] elements, int index, int length)
        {
            if (elements == null && elements.Length == 0)
                return;

            int len = Math.Min(elements.Length, index + length);

            if (len > 8)
            {
                for (int i = index; i < len; i++)
                {
                    writer.Write(elements[i].c0.x);
                    writer.Write(elements[i].c0.y);
                    writer.Write(elements[i].c0.z);
                    writer.Write(elements[i].c1.x);
                    writer.Write(elements[i].c1.y);
                    writer.Write(elements[i].c1.z);
                    writer.Write(elements[i].c2.x);
                    writer.Write(elements[i].c2.y);
                    writer.Write(elements[i].c2.z);
                }
            }
            else
                WriteStream(writer.BaseStream, elements, index, length);
        }

        /// <summary>
        /// Reads in a number of matrices from a stream.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="elements"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public unsafe int ReadStream(System.IO.Stream reader, Mat3f[] elements, int index, int length)
        {
            Debug.Assert(elements != null && elements.Length > 0);

            int count = 0;
            //int sizeinbytes = Marshal.SizeOf(typeof(Mat3f));
            var elementCount = Math.Min(elements.Length, index + length) - index;

            var buf = new byte[elementCount * Mat3f.SizeInBytes];
            count = reader.Read(buf, 0, buf.Length);

            fixed (float* ptr = &elements[index].c0.x)
            {
                Marshal.Copy(buf, 0, (IntPtr)ptr, count * Mat3f.SizeInBytes);
            }

            if (count == buf.Length)
                return elementCount;
            else
                return count % Mat3f.SizeInBytes;
        }


        /// <summary>
        /// Reads an array of Mat3f's from a binary reader.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="elements"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public int ReadStream(System.IO.BinaryReader reader, Mat3f[] elements, int index, int length)
        {
            Debug.Assert(elements != null && elements.Length > 0);

            int count = 0;
            int len = Math.Min(elements.Length, index + length);

            if (len < 8)
            {
                for (int i = index; i < len; i++)
                {
                    //elements[i] = ReadStream(reader);
                    elements[i].c0.x = reader.ReadSingle();
                    elements[i].c0.y = reader.ReadSingle();
                    elements[i].c0.z = reader.ReadSingle();
                    elements[i].c1.x = reader.ReadSingle();
                    elements[i].c1.y = reader.ReadSingle();
                    elements[i].c1.z = reader.ReadSingle();
                    elements[i].c2.x = reader.ReadSingle();
                    elements[i].c2.y = reader.ReadSingle();
                    elements[i].c2.z = reader.ReadSingle();

                    count++;
                }

                return count;
            }
            else
                return ReadStream(reader.BaseStream, elements, index, length);
            
        }

        /// <summary>
        /// Writes a single Mat4f to a binary writer.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="element"></param>
        [DebuggerNonUserCode()]
        public void WriteStream(System.IO.BinaryWriter writer, Mat3f element)
        {
            writer.Write(element.c0.x);
            writer.Write(element.c0.y);
            writer.Write(element.c0.z);
            writer.Write(element.c1.x);
            writer.Write(element.c1.y);
            writer.Write(element.c1.z);
            writer.Write(element.c2.x);
            writer.Write(element.c2.y);
            writer.Write(element.c2.z);
        }


        /// <summary>
        /// Reads a single Mat3f from a binary Reader.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public Mat3f ReadStream(System.IO.BinaryReader reader)
        {
            return new Mat3f(
                reader.ReadSingle(), // x
                reader.ReadSingle(),
                reader.ReadSingle(),
                reader.ReadSingle(), // y
                reader.ReadSingle(),
                reader.ReadSingle(),
                reader.ReadSingle(), // z
                reader.ReadSingle(),
                reader.ReadSingle()
                );
        }

        #endregion
    }
}
