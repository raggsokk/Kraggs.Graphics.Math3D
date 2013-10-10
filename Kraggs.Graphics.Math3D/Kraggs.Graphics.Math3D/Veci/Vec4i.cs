using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Kraggs.Graphics.Math3D.Veci
{
    /// <summary>
    /// An integer 4d vector.
    /// </summary>
    [DebuggerDisplay("[ {x}, {y}, {z}, {w} ]")]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct Vec4i : IEquatable<Vec4i>, IBinaryStreamMath3D<Vec4i>, IGLTypeMath3D, IGenericStream
    {
        /// <summary>
        /// The x component.
        /// </summary>
        public int x;
        /// <summary>
        /// The y component.
        /// </summary>
        public int y;
        /// <summary>
        /// The y component.
        /// </summary>
        public int z;
        /// <summary>
        /// The y component.
        /// </summary>
        public int w;

        public static readonly Vec4i Zero = new Vec4i(0);
        public static readonly Vec4i One = new Vec4i(1);

        public static readonly Vec4i UnitX = new Vec4i() { x = 1, y = 0, z = 0, w = 0 };
        public static readonly Vec4i UnitY = new Vec4i() { x = 0, y = 1, z = 0, w = 0 };
        public static readonly Vec4i UnitZ = new Vec4i() { x = 0, y = 0, z = 1, w = 0 };
        public static readonly Vec4i UnitW = new Vec4i() { x = 0, y = 0, z = 0, w = 1 };

        #region Constructors

        /// <summary>
        /// Creates a new vector with all the same component.
        /// </summary>
        /// <param name="val"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vec4i(int val)
        {
            this.x = val;
            this.y = val;
            this.z = val;
            this.w = val;
        }

        /// <summary>
        /// Creates a new vector with specified components.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vec4i(int x, int y, int z, int w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// Creating a vec4i from a vec3i with a specific w component.
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="w"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vec4i(Vec3i vec, int w)
        {
            this.x = vec.x;
            this.y = vec.y;
            this.z = vec.z;
            this.w = w;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the unsqrt length of the vector.
        /// </summary>
        [DebuggerNonUserCode()]
        public int LengthSquared
        {
            get
            {
                return x * x + y * y + z * z + w * w;
            }
        }

        /// <summary>
        /// Returns the correct Sqrt length of the vector.
        /// </summary>
        [DebuggerNonUserCode()]
        public float Length
        {
            get
            {
                return MathF.Sqrt(x * x + y * y + z * z + w * w);
                //return MathFunctions.Sqrt(x * x + y * y);
            }
        }

        #endregion

        #region Static Functions

        /// <summary>
        /// Returns the distance between 2 vectors.
        /// </summary>
        /// <param name="left">left side</param>
        /// <param name="right">right side.</param>
        /// <returns>distance</returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(Vec4i left, Vec4i right)
        {
            var t = new Vec4i() { x = left.x - right.x, y = left.y - right.y, z = left.z - right.z, w = left.w - right.w };
            return t.Length;
        }

        /// <summary>
        /// Clamps the components of a vector to between min and max.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i Clamp(Vec4i v, int min, int max)
        {
            return new Vec4i()
            {
                ////return x < 0.0f ? 0.0f : x > 1.0f ? 1.0f : x;
                x = v.x < min ? min : v.x > max ? max : v.x,
                y = v.y < min ? min : v.y > max ? max : v.y,
                z = v.z < min ? min : v.z > max ? max : v.z,
                w = v.w < min ? min : v.w > max ? max : v.w,
                //x = Math.Max(Math.Min(v.x, max), min),
                //y = Math.Max(Math.Min(v.y, max), min),
            };
        }

        /// <summary>
        /// Clamps the components of a vector to between min and max.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i Clamp(Vec4i v, Vec4i min, Vec4i max)
        {
            return new Vec4i()
            {
                //return x < 0.0f ? 0.0f : x > 1.0f ? 1.0f : x;
                x = v.x < min.x ? min.x : v.x > max.x ? max.x : v.x,
                y = v.y < min.y ? min.y : v.y > max.y ? max.y : v.y,
                z = v.z < min.z ? min.z : v.z > max.z ? max.z : v.z,
                w = v.w < min.w ? min.w : v.w > max.w ? max.w : v.w,
                //x = Math.Max(Math.Min(v.x, max.x), min.x),
                //y = Math.Max(Math.Min(v.y, max.y), min.y),
            };
        }

        /// <summary>
        /// Returns a mix of two vectors with mix factor a.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i Mix(Vec4i x, Vec4i y, int a)
        {
            return x + a * (y - x);
        }

        ///// <summary>
        ///// Returns a mix of two vectors with mix factor a.
        ///// </summary>
        ///// <param name="x"></param>
        ///// <param name="y"></param>
        ///// <param name="a"></param>
        ///// <returns></returns>
        //[DebuggerNonUserCode()]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Vec2f Mix(Vec2i x, Vec2i y, float a)
        //{

        //    //return x + a * (y - x);
        //}

        /// <summary>
        /// Returns the abolute value of a vector.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i Abs(Vec4i v)
        {
            return new Vec4i()
            {
                x = Math.Abs(v.x),
                y = Math.Abs(v.y),
                z = Math.Abs(v.z),
                w = Math.Abs(v.w),
            };
        }

        /// <summary>
        /// Returns a new vector with the largest component from two vectors.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i Max(Vec4i left, Vec4i right)
        {
            return new Vec4i()
            {
                x = left.x > right.x ? left.x : right.x,
                y = left.y > right.y ? left.y : right.y,
                z = left.z > right.z ? left.z : right.z,
                w = left.w > right.w ? left.w : right.w,
                //x = Math.Max(left.x, right.x),
                //y = Math.Max(left.y, right.y)
            };
        }

        /// <summary>
        /// Returns the smallest components of two vectors.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i Min(Vec4i left, Vec4i right)
        {
            return new Vec4i()
            {
                x = left.x < right.x ? left.x : right.x,
                y = left.y < right.y ? left.y : right.y,
                z = left.z < right.z ? left.z : right.z,
                w = left.w < right.w ? left.w : right.w,
                //x = Math.Min(left.x, right.x),
                //y = Math.Min(left.y, right.y)
            };
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i HigherMultiple(Vec4i source, Vec4i multiple)
        {
            var tmp = source % multiple;

            return (tmp.x != 0 || tmp.y != 0 || tmp.z != 0 || tmp.w != 0) ?
                source + multiple - tmp : source;
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i LowerMultiple(Vec4i source, Vec4i multiple)
        {
            var tmp = source % multiple;

            return (tmp.x != 0 || tmp.y != 0 || tmp.z != 0 || tmp.w != 0) ?
                source - tmp : source;
        }

        /// <summary>
        /// Returns the result of all components multiplied.
        /// aka x * y * z * w
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ComponentMultiply(Vec4i vec)
        {
            return 1 * vec.x * vec.y * vec.z * vec.w;
        }

        /// <summary>
        /// Returns the sum of all the components.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ComponentAdd(Vec4i vec)
        {
            return vec.x + vec.y + vec.z + vec.w;
        }

        /// <summary>
        /// Returns the largest component of a vector.
        /// aka which are largest ov x, y, z, w
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ComponentMax(Vec4i vec)
        {            
            return Math.Max(vec.x, Math.Max(vec.y, Math.Max(vec.z, vec.w)));
        }

        /// <summary>
        /// Returns the smallest component of a vector.
        /// aka which is smallest of x, y, z, w
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ComponentMin(Vec4i vec)
        {
            return Math.Min(vec.x, Math.Min(vec.y, Math.Min(vec.z, vec.w)));
        }

        #endregion

        #region Static Arithmetic Functions

        /// <summary>
        /// Adds a vector and a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i Add(Vec4i vec, int scalar)
        {
            return new Vec4i() { x = vec.x + scalar, y = vec.y + scalar, z = vec.z + scalar, w = vec.w + scalar };
        }

        /// <summary>
        /// Adds a vector and a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add(ref Vec4i vec, int scalar, out Vec4i result)
        {
            result = new Vec4i() { x = vec.x + scalar, y = vec.y + scalar, z = vec.z + scalar, w = vec.w + scalar };
        }

        /// <summary>
        /// Subtracts from a vector a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i Subtract(Vec4i vec, int scalar)
        {
            return new Vec4i() { x = vec.x - scalar, y = vec.y - scalar, z = vec.z - scalar, w = vec.w - scalar };
        }

        /// <summary>
        /// Subtracts from a vector a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subtract(ref Vec4i vec, int scalar, out Vec4i result)
        {
            result = new Vec4i() { x = vec.x - scalar, y = vec.y - scalar, z = vec.z - scalar, w = vec.w - scalar };
        }

        /// <summary>
        /// Multiplies a vector and a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i Multiply(Vec4i vec, int scalar)
        {
            return new Vec4i() { x = vec.x * scalar, y = vec.y * scalar, z = vec.z * scalar, w = vec.w * scalar };
        }

        /// <summary>
        /// Multiplies a vector and a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Vec4i vec, int scalar, out Vec4i result)
        {
            result.x = vec.x * scalar;
            result.y = vec.y * scalar;
            result.z = vec.z * scalar;
            result.w = vec.w * scalar;
            //result = new Vec4i() { x = vec.x * scalar, y = vec.y * scalar, z = vec.z * scalar };
        }

        /// <summary>
        /// Divides a vector with a scalar component wise.
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i Divide(Vec4i vec, int scalar)
        {
            return new Vec4i()
            {
                x = vec.x / scalar,
                y = vec.y / scalar,
                z = vec.z / scalar,
                w = vec.w / scalar
            };
            //Divide(ref vec, scalar, out vec);
            //return vec;

            //return Vec2f.Divide((Vec2f)vec, (float)scalar);
        }
        /// <summary>
        /// Divides a vector with a scalar component wise.
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Divide(ref Vec4i vec, int scalar, out Vec4i result)
        {
            result.x = vec.x / scalar;
            result.y = vec.y / scalar;
            result.z = vec.z / scalar;
            result.w = vec.w / scalar;
            //Multiply(ref vec, 1 / scalar, out result);
            //result = new Vec4i() { x = vec.x + scalar, y = vec.y + scalar };

            //Vec2f v2f = (Vec2f)vec;
            //Vec2f.Multiply(ref v2f, (float)scalar, out result);
        }

        /// <summary>
        /// Adds to vectors to each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i Add(Vec4i left, Vec4i right)
        {
            return new Vec4i() { x = left.x + right.x, y = left.y + right.y, z = left.z + right.z, w = left.w + right.w };
            //Vec4i result;
            //Add(ref left, ref right, out result);
            //return result;
        }

        /// <summary>
        /// Adds to vectors to each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add(ref Vec4i left, ref Vec4i right, out Vec4i result)
        {
            result = new Vec4i() { x = left.x + right.x, y = left.y + right.y, z = left.z + right.z, w = left.w + right.w };
        }

        /// <summary>
        /// Subtracts 2 vectors from each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i Subtract(Vec4i left, Vec4i right)
        {
            return new Vec4i() { x = left.x - right.x, y = left.y - right.y, z = left.z - right.z, w = left.w - right.w };
        }
        /// <summary>
        /// Subtracts 2 vectors from each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subtract(ref Vec4i left, ref Vec4i right, out Vec4i result)
        {
            result = new Vec4i() { x = left.x - right.x, y = left.y - right.y, z = left.z - right.z, w = left.w - right.w };
        }

        /// <summary>
        /// Multiplies 2 vectors with each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i Multiply(Vec4i left, Vec4i right)
        {
            return new Vec4i() { x = left.x * right.x, y = left.y * right.y, z = left.z * right.z, w = left.w * right.w };
        }
        /// <summary>
        /// Multiplies 2 vectors with each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Vec4i left, ref Vec4i right, out Vec4i result)
        {
            result = new Vec4i() { x = left.x * right.x, y = left.y * right.y, z = left.z * right.z, w = left.w * right.w };
        }

        /// <summary>
        /// Divides 2 vectors with each other component wise.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i Divide(Vec4i left, Vec4i right)
        {
            return new Vec4i() { x = left.x / right.x, y = left.y / right.y, z = left.z / right.z, w = right.w / right.w };
        }

        /// <summary>
        /// Divides 2 vectors with each other component wise.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Divide(ref Vec4i left, ref Vec4i right, out Vec4i result)
        {
            result = new Vec4i() { x = left.x / right.x, y = left.y / right.y, z = left.z / right.z, w = left.w / right.w };
        }

        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i Negate(Vec4i vec)
        {
            return new Vec4i() { x = -vec.x, y = -vec.y, z = -vec.z, w = -vec.w };
        }

        #endregion

        #region Static Operator Functions

        /// <summary>
        /// Adds two vectors to each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i operator +(Vec4i left, Vec4i right)
        {
            return new Vec4i()
            {
                x = left.x + right.x,
                y = left.y + right.y,
                z = left.z + right.z,
                w = left.w + right.w,
            };
        }

        /// <summary>
        /// Subtracts one vector from another.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i operator -(Vec4i left, Vec4i right)
        {
            return new Vec4i()
            {
                x = left.x - right.x,
                y = left.y - right.y,
                z = left.z - right.z,
                w = left.w - right.w,
            };
        }

        /// <summary>
        /// Multiplies two vectors with each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i operator *(Vec4i left, Vec4i right)
        {
            return new Vec4i()
            {
                x = left.x * right.x,
                y = left.y * right.y,
                z = left.z * right.z,
                w = left.w * right.w,
            };
        }

        /// <summary>
        /// Divides two vectores from each other component wise.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i operator /(Vec4i left, Vec4i right)
        {
            return new Vec4i()
            {
                x = left.x / right.x,
                y = left.y / right.y,
                z = left.z / right.z,
                w = left.w / right.w,
            };
        }
        /// <summary>
        /// Adds a scalar to a vector.
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i operator +(Vec4i vec, int scalar)
        {
            return new Vec4i()
            {
                x = vec.x + scalar,
                y = vec.y + scalar,
                z = vec.z + scalar,
                w = vec.w + scalar,
            };
        }
        /// <summary>
        /// Subtracts a scalar from a vector.
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i operator -(Vec4i vec, int scalar)
        {
            return new Vec4i()
            {
                x = vec.x - scalar,
                y = vec.y - scalar,
                z = vec.z - scalar,
                w = vec.w - scalar,
            };
        }

        /// <summary>
        /// Multiplies a vector and a scalar.
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i operator *(Vec4i vec, int scalar)
        {
            return new Vec4i()
            {
                x = vec.x * scalar,
                y = vec.y * scalar,
                z = vec.z * scalar,
                w = vec.w * scalar,
            };
        }
        /// <summary>
        /// Divides a vector with a scalar.
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i operator /(Vec4i vec, int scalar)
        {
            return new Vec4i()
            {
                x = vec.x / scalar,
                y = vec.y / scalar,
                z = vec.z / scalar,
                w = vec.w / scalar,
            };
            //int f = 1.0f / scalar;

            //return new Vec4i()
            //{
            //    x = vec.x * f,
            //    y = vec.y * f
            //};
        }
        /// <summary>
        /// Adds a scalar to a vector.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i operator +(int scalar, Vec4i vec)
        {
            return new Vec4i()
            {
                x = scalar + vec.x,
                y = scalar + vec.y,
                z = scalar + vec.z,
                w = scalar + vec.w,
            };
        }
        /// <summary>
        /// Subtracts a vector from a scalar.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i operator -(int scalar, Vec4i vec)
        {
            return new Vec4i()
            {
                x = scalar - vec.x,
                y = scalar - vec.y,
                z = scalar - vec.z,
                w = scalar - vec.w,
            };
        }
        /// <summary>
        /// Multiplies a scalar with a vector.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i operator *(int scalar, Vec4i vec)
        {
            return new Vec4i()
            {
                x = scalar * vec.x,
                y = scalar * vec.y,
                z = scalar * vec.z,
                w = scalar * vec.w,
            };
        }
        /// <summary>
        /// Divies a scalar with a vector.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i operator /(int scalar, Vec4i vec)
        {
            return new Vec4i()
            {
                x = scalar / vec.x,
                y = scalar / vec.y,
                z = scalar / vec.z,
                w = scalar / vec.w,
            };
            //int f = 1.0f / scalar;

            //return new Vec4i()
            //{
            //    x = f * vec.x,
            //    y = f * vec.y
            //};
        }

        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i operator -(Vec4i vec)
        {
            return new Vec4i()
            {
                x = -vec.x,
                y = -vec.y,
                z = -vec.z,
                w = -vec.w,
            };
        }

        /// <summary>
        /// Compares two vectors with each other for equality
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true if equal, false otherwise.</returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vec4i left, Vec4i right)
        {
            return
                left.x == right.x &&
                left.y == right.y &&
                left.z == right.z &&
                left.w == right.w;
        }

        /// <summary>
        /// Compares two vectors with each other for inequality
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true if not equal, false otherwise.</returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vec4i left, Vec4i right)
        {
            return
                left.x != right.x ||
                left.y != right.y ||
                left.z != right.z ||
                left.w != right.w;
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i operator %(Vec4i vec, int scalar)
        {
            return new Vec4i()
            {
                x = vec.x % scalar,
                y = vec.y % scalar,
                z = vec.z % scalar,
                w = vec.w % scalar,
            };
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i operator %(int scalar, Vec4i vec)
        {
            return new Vec4i()
            {
                x = scalar % vec.x,
                y = scalar % vec.y,
                z = scalar % vec.z,
                w = scalar % vec.w,
            };
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i operator %(Vec4i left, Vec4i right)
        {
            return new Vec4i()
            {
                x = left.x % right.x,
                y = left.y % right.y,
                z = left.y % right.z,
                w = left.w % right.w,
            };
        }

        /// <summary>
        /// BitShifts the components of an vector to the left.
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="bitshift"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i operator <<(Vec4i vec, int bitshift)
        {
            return new Vec4i()
            {
                x = vec.x << bitshift,
                y = vec.y << bitshift,
                z = vec.z << bitshift,
                w = vec.w << bitshift
            };
        }

        /// <summary>
        /// BitShifts the components of an vector to the right.
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="bitshift"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4i operator >>(Vec4i vec, int bitshift)
        {
            return new Vec4i()
            {
                x = vec.x >> bitshift,
                y = vec.y >> bitshift,
                z = vec.z >> bitshift,
                w = vec.w >> bitshift
            };
        }

        /// <summary>
        /// Returns unsafe int* pointer to x.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public unsafe static explicit operator int*(Vec4i vec)
        {
            return &vec.x;
        }
        /// <summary>
        /// Returns unsafe IntPtr pointer to x.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public unsafe static explicit operator IntPtr(Vec4i vec)
        {
            return (IntPtr)(&vec.x);
        }

        /// <summary>
        /// Cast a vec2i to a Vec4i with z = 0, w = 0
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vec4i(Vec2i vec)
        {
            return new Vec4i(vec.x, vec.y, 0, 0);
        }

        /// <summary>
        /// Casting a vec3i to a Vec4i, with w = 0
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vec4i(Vec3i vec)
        {
            return new Vec4i(vec.x, vec.y, vec.z, 0);
        }

        /// <summary>
        /// Cast a vec3f to a Vec4i, losing floating point accurency.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vec4i(Vec4f vec)
        {
            return new Vec4i((int)vec.x, (int)vec.y, (int)vec.z, (int)vec.w);
        }

        /// <summary>
        /// Cast a Vec4i to a Vec3f
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vec4f(Vec4i vec)
        {
            return new Vec4f(vec.x, vec.y, vec.z, vec.w);
        }

        #endregion

        #region Object Overloads

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode() ^ w.GetHashCode();
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return string.Format("[{0}, {1}, {2}, {3}]", x, y, z, w);
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            if (obj is Vec4i)
                return Equals((Vec4i)obj);
            else
                return false;
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vec4i other)
        {
            return x == other.x && y == other.y && z == other.z && w == other.w;
        }

        #endregion

        #region IGLTypeMath3D

        private static readonly IGLDescriptionMath3D GLTypeDescription = new Vec4iGLDescription();

        /// <summary>
        /// Returns an object with description of this GL Type.
        /// </summary>
        public IGLDescriptionMath3D GetGLTypeDescription
        {
            get
            {
                Debug.Assert(Marshal.SizeOf(typeof(Vec4iGLDescription)) == 0);

                return GetGLTypeDescription;
            }
        }

        /// <summary>
        /// Very private desc struct for this type.
        /// </summary>
        private struct Vec4iGLDescription : IGLDescriptionMath3D
        {
            Type IGLDescriptionMath3D.BaseType
            {
                get { return typeof(int); }
            }

            int IGLDescriptionMath3D.ComponentCount
            {
                get { return 4; }
            }

            int IGLDescriptionMath3D.SizeInBytes
            {
                get { return 16; }
            }

            int IGLDescriptionMath3D.GLBaseType
            {
                get { return GLConstants.GL_BASE_SINT; }
            }

            int IGLDescriptionMath3D.GLAttributeType
            {
                get { return GLConstants.INT_VEC4; }
            }

            int IGLDescriptionMath3D.GLUniformType
            {
                get { return GLConstants.INT_VEC4; }
            }

            bool IGLDescriptionMath3D.IsMatrix
            {
                get { return false; }
            }

            bool IGLDescriptionMath3D.IsRowMajor
            {
                get { return false; }
            }

            int IGLDescriptionMath3D.Columns
            {
                get { return 4; }
            }

            int IGLDescriptionMath3D.Rows
            {
                get { return 1; }
            }
        }

        #endregion

        #region IGenericStream Implementation

        /// <summary>
        /// Writes a vec4i.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="vec"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obsolete("Use functions in IBinaryStreamMath3D instead")]
        void IGenericStream.WriteStream(System.IO.BinaryWriter writer, object vec)
        {
            Vec4i v = (Vec4i)vec;
            writer.Write(v.x);
            writer.Write(v.y);
            writer.Write(v.z);
            writer.Write(v.w);
        }

        /// <summary>
        /// Reads a vec4i.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obsolete("Use functions in IBinaryStreamMath3D instead")]
        object IGenericStream.ReadStream(System.IO.BinaryReader reader)
        {
            return new Vec4i(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
        }

        #endregion

        #region IBinaryStreamMath3D Implementation

        /// <summary>
        /// Writes out an array of vec4i's to a binary writer.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="elements"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        [DebuggerNonUserCode()]
        public void WriteStream(System.IO.BinaryWriter writer, Vec4i[] elements, int index, int length)
        {
            if (elements == null || elements.Length == 0)
                return;

            var len = Math.Min(elements.Length, index + length);

            for (int i = index; i < len; i++)
            {
                writer.Write(elements[i].x);
                writer.Write(elements[i].y);
                writer.Write(elements[i].z);
                writer.Write(elements[i].w);
            }
        }

        /// <summary>
        /// Reads in an array of Vec4i's from a binary reader.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="elements"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public int ReadStream(System.IO.BinaryReader reader, Vec4i[] elements, int index, int length)
        {
            Debug.Assert(elements != null && elements.Length > 0);

            int count = 0;
            var len = Math.Min(elements.Length, index + length);

            for (int i = index; i < len; i++)
            {
                elements[i].x = reader.ReadInt32();
                elements[i].y = reader.ReadInt32();
                elements[i].z = reader.ReadInt32();
                elements[i].w = reader.ReadInt32();
                count++;
            }

            return count;
        }

        /// <summary>
        /// Write a single Vec4f to a binary writer.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="element"></param>
        [DebuggerNonUserCode()]
        public void WriteStream(System.IO.BinaryWriter writer, Vec4i element)
        {
            //Debug.Assert(writer != null);

            writer.Write(element.x);
            writer.Write(element.y);
            writer.Write(element.z);
            writer.Write(element.w);
        }

        /// <summary>
        /// Reads a single Vec4f from a binary reader.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public Vec4i ReadStream(System.IO.BinaryReader reader)
        {
            //Debug.Assert(reader != null);

            return new Vec4i(
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadInt32());
        }

        #endregion
    }
}
