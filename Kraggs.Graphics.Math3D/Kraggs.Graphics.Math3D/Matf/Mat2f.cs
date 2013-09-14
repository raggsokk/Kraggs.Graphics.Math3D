using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Kraggs.Graphics.Math3D
{
    //[DebuggerDisplay("TODO")]
    [DebuggerDisplay("{DebugDisplayHelper()}")]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct Mat2f : IEquatable<Mat2f>, IGLMatrix, IGenericStream
    {
        /// <summary>
        /// The first column.
        /// </summary>
        public Vec2f c0;
        /// <summary>
        /// The second column.
        /// </summary>
        public Vec2f c1;

        #region Properties

        public static readonly Mat2f Zero = new Mat2f() { c0 = Vec2f.Zero, c1 = Vec2f.Zero };
        public static readonly Mat2f Identity = new Mat2f() { c0 = Vec2f.UnitX, c1 = Vec2f.UnitY };

        /// <summary>
        /// Returns the row0 of this matrix.
        /// It also gives a nice debug view of this matrix.
        /// </summary>
        [DebuggerNonUserCode()]
        public Vec2f Row0
        {
            get
            {
                return new Vec2f() { x = c0.x, y = c1.x};
            }
        }

        /// <summary>
        /// Returns the row1 of this matrix.
        /// It also gives a nice debug view of this matrix.
        /// </summary>
        [DebuggerNonUserCode()]
        public Vec2f Row1
        {
            get
            {
                return new Vec2f() { x = c0.y, y = c1.y};
            }
        }


        #endregion

        #region Constructors

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]        
        public Mat2f(float scalar)
        {
            this.c0.x = scalar;
            this.c0.y = 0.0f;
            this.c1.x = 0.0f;
            this.c1.y = scalar;
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]        
        public Mat2f(float x0, float y0, float x1, float y1)
        {
            c0.x = x0;
            c0.y = y0;
            c1.x = x1;
            c1.y = y1;
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]        
        public Mat2f(Vec2f c0, Vec2f c1)
        {
            this.c0 = c0;
            this.c1 = c1;
        }

        #endregion

        #region Instance Functions

        public float Determinant
        {
            get
            {
                return c0.x * c1.y - c1.x * c0.y;
            }
        }

        public unsafe float[] ToArray()
        {
            var result = new float[4];

            fixed(float* ptr = &this.c0.x)
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
        }

        #endregion

        #region Static Matrix Functions

        /// <summary>
        /// Returns a transpose version of this matrix.
        /// </summary>
        /// <param name="m">matrix to get transpose from.</param>
        /// <returns>transpose matrix.</returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat2f Transpose(Mat2f m)
        {
            return new Mat2f() {
                c0 = new Vec2f() { x = m.c0.x, y = m.c1.x},
                c1 = new Vec2f() { x = m.c0.y, y = m.c1.y}
            };
        }
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat2f OuterProduct(Vec2f c, Vec2f r)
        {
            return new Mat2f(
                c.x * r.x,
                c.y * r.x,
                c.x * r.y,
                c.y * r.y
                );
            //Mat2f m = Mat2f.Zero;
            //m.c0.x = c.x * r.x;
            //m.c0.y = c.y * r.x;
            //m.c1.x = c.x * r.y;
            //m.c1.y = c.y * r.y;

            //return m;
        }
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat2f OuterProduct2(Vec2f c, Vec2f r)
        {
            Mat2f m = Mat2f.Zero;
            m.c0.x = c.x * r.x;
            m.c0.y = c.y * r.x;
            m.c1.x = c.x * r.y;
            m.c1.y = c.y * r.y;

            return m;
        }

        /// <summary>
        /// Result is a transpose version of given matrix.
        /// </summary>
        /// <param name="m">matrix to get transpose from.</param>
        /// <param name="result">transpose matrix.</param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Transpose(ref Mat2f m, out Mat2f result)
        {
            result.c0.x = m.c0.x;
            result.c0.y = m.c1.x;
            result.c1.x = m.c0.y;
            result.c1.y = m.c1.y;
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat2f Inverse(Mat2f m)
        {
            var determinant = m.Determinant;

            Mat2f inverse = new Mat2f
            (
                + m.c1.y / determinant,
                - m.c0.y / determinant,
                - m.c1.x / determinant,
                + m.c0.x / determinant
            );

            return inverse;
        }

        public static Mat2f InverseTranspose(Mat2f m)
        {
            var determinant = m.Determinant;

            Mat2f inverse = new Mat2f(
                +m.c1.y / determinant,
                -m.c0.y / determinant,
                -m.c1.x / determinant,
                +m.c0.x / determinant);

            return inverse;
        }

        #endregion

        #region Euler Angles

        /// <summary>
        /// Creates a 2D 2 * 2 rotation matrix from an euler angle.
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat2f CreateOrientate2(float angle)
        {
            float c = (float)Math.Cos(angle);
            float s = (float)Math.Cos(angle);

            Mat2f result;
            result.c0.x = c;
            result.c0.y = s;
            result.c1.x = -s;
            result.c1.y = c;

            return result;
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
        public static Mat2f Add(Mat2f left, Mat2f right)
        {
            return new Mat2f()
            {
                c0 = Vec2f.Add(left.c0, right.c0),
                c1 = Vec2f.Add(left.c1, right.c1)                
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
        public static void Add(ref Mat2f left, ref Mat2f right, out Mat2f result)
        {
            Vec2f.Add(ref left.c0, ref right.c0, out result.c0);
            Vec2f.Add(ref left.c1, ref right.c1, out result.c1);            
        }
        /// <summary>
        /// Subtracts one matrix from another componentwise.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat2f Subtract(Mat2f left, Mat2f right)
        {
            return new Mat2f()
            {
                c0 = Vec2f.Subtract(left.c0, right.c0),
                c1 = Vec2f.Subtract(left.c1, right.c1)                
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
        public static void Subtract(ref Mat2f left, ref Mat2f right, out Mat2f result)
        {
            Vec2f.Subtract(ref left.c0, ref right.c0, out result.c0);
            Vec2f.Subtract(ref left.c1, ref right.c1, out result.c1);            
        }
        /// <summary>
        /// Multiplies two matrices with each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat2f Multiply(Mat2f left, Mat2f right)
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
        public static void Multiply(ref Mat2f left, ref Mat2f right, out Mat2f result)
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
        public static Mat2f Divide(Mat2f left, Mat2f right)
        {
            return left / right;
            //return new Mat2f()
            //{
            //    c0 = Vec2f.Divide(left.c0, right.c0),
            //    c1 = Vec2f.Divide(left.c1, right.c1),
            //    c2 = Vec2f.Divide(left.c2, right.c2)
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
        public static void Divide(ref Mat2f left, ref Mat2f right, out Mat2f result)
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
        public static Mat2f operator +(Mat2f left, Mat2f right)
        {
            return new Mat2f()
            {
                c0 = left.c0 + right.c0,
                c1 = left.c1 + right.c1
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
        public static Mat2f operator -(Mat2f left, Mat2f right)
        {
            return new Mat2f()
            {
                c0 = left.c0 - right.c0,
                c1 = left.c1 - right.c1
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
        public static Mat2f operator *(Mat2f left, Mat2f right)
        {
            return new Mat2f
            (
                left.c0.x * right.c0.x + left.c1.x * right.c0.y,
                left.c0.y * right.c0.x + left.c1.y * right.c0.y,
                left.c0.x * right.c1.x + left.c1.x * right.c1.y,
                left.c0.y * right.c1.x + left.c1.y * right.c1.y
            );
        }
        /// <summary>
        /// Divides a matrix with another.
        /// BEWARE: Creates a tmp inverse matrix with is very costly...
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Mat2f operator /(Mat2f left, Mat2f right)
        {
            return left * Mat2f.Inverse(right);
        }

        /// <summary>
        /// Adds a scalar to a matrix.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat2f operator +(Mat2f m, float scalar)
        {
            return new Mat2f()
            {
                c0 = m.c0 + scalar,
                c1 = m.c1 + scalar
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
        public static Mat2f operator -(Mat2f m, float scalar)
        {
            return new Mat2f()
            {
                c0 = m.c0 - scalar,
                c1 = m.c1 - scalar
            };
        }
        /// <summary>
        /// Multiplies a matrix with a scalar.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat2f operator *(Mat2f m, float scalar)
        {
            return new Mat2f()
            {
                c0 = m.c0 * scalar,
                c1 = m.c1 * scalar
            };
        }
        /// <summary>
        /// Divides a matrix with a scalar.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat2f operator /(Mat2f m, float scalar)
        {
            return new Mat2f()
            {
                c0 = m.c0 / scalar,
                c1 = m.c1 / scalar
            };
        }

        /// <summary>
        /// Adds a scalar with a matrix.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat2f operator +(float scalar, Mat2f m)
        {
            return new Mat2f()
            {
                c0 = m.c0 + scalar,
                c1 = m.c1 + scalar
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
        public static Mat2f operator -(float scalar, Mat2f m)
        {
            return new Mat2f()
            {
                c0 = scalar - m.c0,
                c1 = scalar - m.c1
            };
        }
        /// <summary>
        /// Multiplies a scalar and a matrix.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat2f operator *(float scalar, Mat2f m)
        {
            return new Mat2f()
            {
                c0 = m.c0 * scalar,
                c1 = m.c1 * scalar
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
        public static Mat2f operator /(float scalar, Mat2f m)
        {
            return new Mat2f()
            {
                c0 = scalar / m.c0,
                c1 = scalar / m.c1
            };
        }

        /// <summary>
        /// Multiplies a matrix with a row vector returning a column vector.
        /// </summary>
        /// <param name="m">matrix</param>
        /// <param name="v">row vector.</param>
        /// <returns>column vector.</returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2f operator *(Mat2f m, Vec2f v)
        {
            return new Vec2f()
            {
                x = m.c0.x * v.x + m.c1.x * v.y,
                y = m.c0.y * v.x + m.c1.y * v.y
            };
        }
        /// <summary>
        /// Multiplies a column vector with a matrix returning a row vector.
        /// </summary>
        /// <param name="v">column vector.</param>
        /// <param name="m">matrix.</param>
        /// <returns>row vector.</returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2f operator *(Vec2f v, Mat2f m)
        {
            return new Vec2f()
            {
                x = v.x * m.c0.x + v.y * m.c0.y,
                y = v.x * m.c1.x + v.y * m.c1.y
            };
        }

        /// <summary>
        /// Divides a matrix with a row vector returning a column vector.
        /// BEWARE: Creates a tmp inverse matrix with is very costly...
        /// </summary>
        /// <param name="m">matrix</param>
        /// <param name="v">row vector</param>
        /// <returns>column vector.</returns>
        public static Vec2f operator /(Mat2f m, Vec2f v)
        {
            var inverse = Mat2f.Inverse(m);

            return inverse * v;
        }

        /// <summary>
        /// Divides a column vector with a matrix returning a row vector.
        /// BEWARE: Creates a tmp inverse matrix with is very costly...
        /// </summary>
        /// <param name="v">column vector</param>
        /// <param name="m">matrix</param>
        /// <returns>row vector.</returns>
        public static Vec2f operator /(Vec2f v, Mat2f m)
        {
            var inverse = Mat2f.Inverse(m);

            return v * inverse;
        }

        /// <summary>
        /// Negates a matrix component wise.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat2f operator -(Mat2f m)
        {
            return new Mat2f()
            {
                c0 = -m.c0,
                c1 = -m.c1
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
        public static bool operator ==(Mat2f left, Mat2f right)
        {
            return
                left.c0.x == right.c0.x &&
                left.c0.y == right.c0.y &&
                left.c1.x == right.c1.x &&
                left.c1.y == right.c1.y;
        }

        /// <summary>
        /// Compares two matrices for inequality.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Mat2f left, Mat2f right)
        {
            return
                left.c0.x != right.c0.x ||
                left.c0.y != right.c0.y ||
                left.c1.x != right.c1.x ||
                left.c1.y != right.c1.y;
        }

        /// <summary>
        /// Explicit conversion of a mat3f to a mat2f with dataloss.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Mat2f(Mat3f m)
        {
            return new Mat2f(m.c0.x, m.c0.y, m.c1.x, m.c1.y);
        }

        /// <summary>
        /// Explicit conversion of a mat4f to a mat2f with major dataloss.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Mat2f(Mat4f m)
        {
            return new Mat2f(m.c0.x, m.c0.y, m.c1.x, m.c1.y);
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
            return c0.GetHashCode() ^ c1.GetHashCode();
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
                "{0}\n{1}", c0, c1);
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
            if (obj is Mat2f)
                return Equals((Mat2f)obj);
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
        public bool Equals(Mat2f other)
        {
            return
                this.c0.x == other.c0.x &&
                this.c0.y == other.c0.y &&
                this.c1.x == other.c1.x &&
                this.c1.y == other.c1.y;
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
                    for (int i = 0; i < elements; i += 2)
                        Console.WriteLine("| {0:0.0000} | {1:0.0000} | ",
                            ptr[i], ptr[i + 1]);
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
                    for (int i = 0; i < 2; i++)
                        Console.WriteLine("| {0:0.0000} | {1:0.0000} | ",
                            ptr[i], ptr[i + 2]);
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
                    for (int i = 0; i < 2; i++)
                        sb.Append(string.Format("| {0:0.0000} | {1:0.0000} | ",
                            ptr[i], ptr[i + 2]));
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
            get { return 2; }
        }

        /// <summary>
        /// Number of rows in this matrix.
        /// </summary>
        [DebuggerNonUserCode()]
        int IGLMatrix.RowCount
        {
            get { return 2; }
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
            get { return typeof(Mat2f); }
        }

        /// <summary>
        /// The number of components totaly in this matrix.
        /// </summary>
        [DebuggerNonUserCode()]
        int IGLMath.ComponentCount
        {
            get { return 4; }
        }

        public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Mat2f));

        /// <summary>
        /// Returns the inmemory size in bytes of this matrix.
        /// </summary>
        [DebuggerNonUserCode()]
        int IGLMath.SizeInBytes
        {
            get { return Mat2f.SizeInBytes; }
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
            get { return GLConstants.FLOAT_MAT2; }
        }

        /// <summary>
        /// Returns the opengl uniform type enum.
        /// </summary>
        [DebuggerNonUserCode()]
        int IGLMath.GLUniformType
        {
            get { return GLConstants.FLOAT_MAT2; }
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

        /// <summary>
        /// Writes matrix to stream.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="vec"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteStream(System.IO.BinaryWriter writer, object matrix)
        {
            Mat2f m = (Mat2f)matrix;
            writer.Write(m.c0.x);
            writer.Write(m.c0.y);
            writer.Write(m.c1.x);
            writer.Write(m.c1.y);
        }

        /// <summary>
        /// Reads in a new vector from stream.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object ReadStream(System.IO.BinaryReader reader)
        {
            return new Mat2f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        }

        #endregion
    }
}
