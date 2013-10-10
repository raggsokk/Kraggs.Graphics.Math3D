using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Kraggs.Graphics.Math3D.Veci
{
    /// <summary>
    /// An integer 3d vector.
    /// </summary>
    [DebuggerDisplay("[ {x}, {y}, {z} ]")]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct Vec3i : IEquatable<Vec3i>
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

        public static readonly Vec3i Zero = new Vec3i(0);
        public static readonly Vec3i One = new Vec3i(1);

        public static readonly Vec3i UnitX = new Vec3i() { x = 1, y = 0, z = 0 };
        public static readonly Vec3i UnitY = new Vec3i() { x = 0, y = 1, z = 0 };
        public static readonly Vec3i UnitZ = new Vec3i() { x = 0, y = 0, z = 1 };

        #region Constructors

        /// <summary>
        /// Creates a new vector with all the same component.
        /// </summary>
        /// <param name="val"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vec3i(int val)
        {
            this.x = val;
            this.y = val;
            this.z = val;
        }

        /// <summary>
        /// Creates a new vector with specified components.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vec3i(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Creates a new vec3i based on a vec2i with specific z component.
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="z"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vec3i(Vec2i vec, int z)
        {
            this.x = vec.x;
            this.y = vec.y;
            this.z = z;
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
                return x * x + y * y + z * z;
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
                return MathF.Sqrt(x * x + y * y + z * z);
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
        public static float Distance(Vec3i left, Vec3i right)
        {
            var t = new Vec3i() { x = left.x - right.x, y = left.y - right.y, z = left.z - right.z };
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
        public static Vec3i Clamp(Vec3i v, int min, int max)
        {
            return new Vec3i()
            {
                ////return x < 0.0f ? 0.0f : x > 1.0f ? 1.0f : x;
                x = v.x < min ? min : v.x > max ? max : v.x,
                y = v.y < min ? min : v.y > max ? max : v.y,
                z = v.z < min ? min : v.z > max ? max : v.z
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
        public static Vec3i Clamp(Vec3i v, Vec3i min, Vec3i max)
        {
            return new Vec3i()
            {
                //return x < 0.0f ? 0.0f : x > 1.0f ? 1.0f : x;
                x = v.x < min.x ? min.x : v.x > max.x ? max.x : v.x,
                y = v.y < min.y ? min.y : v.y > max.y ? max.y : v.y,
                z = v.z < min.z ? min.z : v.z > max.z ? max.z : v.z
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
        public static Vec3i Mix(Vec3i x, Vec3i y, int a)
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
        public static Vec3i Abs(Vec3i v)
        {
            return new Vec3i()
            {
                x = Math.Abs(v.x),
                y = Math.Abs(v.y),
                z = Math.Abs(v.z)
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
        public static Vec3i Max(Vec3i left, Vec3i right)
        {
            return new Vec3i()
            {
                x = left.x > right.x ? left.x : right.x,
                y = left.y > right.y ? left.y : right.y,
                z = left.z > right.z ? left.z : right.z
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
        public static Vec3i Min(Vec3i left, Vec3i right)
        {
            return new Vec3i()
            {
                x = left.x < right.x ? left.x : right.x,
                y = left.y < right.y ? left.y : right.y,
                z = left.z < right.z ? left.z : right.z
                //x = Math.Min(left.x, right.x),
                //y = Math.Min(left.y, right.y)
            };
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3i HigherMultiple(Vec3i source, Vec3i multiple)
        {
            var tmp = source % multiple;

            return (tmp.x != 0 || tmp.y != 0 || tmp.z != 0) ?
                source + multiple - tmp : source;
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3i LowerMultiple(Vec3i source, Vec3i multiple)
        {
            var tmp = source % multiple;

            return (tmp.x != 0 || tmp.y != 0 || tmp.z != 0) ?
                source - tmp : source;
        }

        /// <summary>
        /// Returns the result of all components multiplied.
        /// aka x * y * z
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ComponentMultiply(Vec3i vec)
        {
            return 1 * vec.x * vec.y * vec.z;
        }

        /// <summary>
        /// Returns the sum of all the components.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ComponentAdd(Vec3i vec)
        {
            return vec.x + vec.y + vec.z;
        }

        /// <summary>
        /// Returns the largest component of a vector.
        /// aka which are largest ov x, y, z
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ComponentMax(Vec3i vec)
        {
            return Math.Max(vec.x, Math.Max(vec.y, vec.z));
        }

        /// <summary>
        /// Returns the smallest component of a vector.
        /// aka which is smallest of x, y, z
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ComponentMin(Vec3i vec)
        {
            return Math.Min(vec.x, Math.Min(vec.y, vec.z));
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
        public static Vec3i Add(Vec3i vec, int scalar)
        {
            return new Vec3i() { x = vec.x + scalar, y = vec.y + scalar, z = vec.z + scalar };
        }

        /// <summary>
        /// Adds a vector and a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add(ref Vec3i vec, int scalar, out Vec3i result)
        {
            result = new Vec3i() { x = vec.x + scalar, y = vec.y + scalar, z = vec.z + scalar };
        }

        /// <summary>
        /// Subtracts from a vector a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3i Subtract(Vec3i vec, int scalar)
        {
            return new Vec3i() { x = vec.x - scalar, y = vec.y - scalar, z = vec.z - scalar };
        }

        /// <summary>
        /// Subtracts from a vector a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subtract(ref Vec3i vec, int scalar, out Vec3i result)
        {
            result = new Vec3i() { x = vec.x - scalar, y = vec.y - scalar, z = vec.z - scalar };
        }

        /// <summary>
        /// Multiplies a vector and a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3i Multiply(Vec3i vec, int scalar)
        {
            return new Vec3i() { x = vec.x * scalar, y = vec.y * scalar, z = vec.z * scalar };
        }

        /// <summary>
        /// Multiplies a vector and a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Vec3i vec, int scalar, out Vec3i result)
        {
            result.x = vec.x * scalar;
            result.y = vec.y * scalar;
            result.z = vec.z * scalar;
            //result = new Vec3i() { x = vec.x * scalar, y = vec.y * scalar, z = vec.z * scalar };
        }

        /// <summary>
        /// Divides a vector with a scalar component wise.
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3i Divide(Vec3i vec, int scalar)
        {
            return new Vec3i()
            {
                x = vec.x / scalar,
                y = vec.y / scalar,
                z = vec.z / scalar
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
        public static void Divide(ref Vec3i vec, int scalar, out Vec3i result)
        {
            result.x = vec.x / scalar;
            result.y = vec.y / scalar;
            result.z = vec.z / scalar;
            //Multiply(ref vec, 1 / scalar, out result);
            //result = new Vec3i() { x = vec.x + scalar, y = vec.y + scalar };

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
        public static Vec3i Add(Vec3i left, Vec3i right)
        {
            return new Vec3i() { x = left.x + right.x, y = left.y + right.y, z = left.z + right.z };
            //Vec3i result;
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
        public static void Add(ref Vec3i left, ref Vec3i right, out Vec3i result)
        {
            result = new Vec3i() { x = left.x + right.x, y = left.y + right.y, z = left.z + right.z };
        }

        /// <summary>
        /// Subtracts 2 vectors from each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3i Subtract(Vec3i left, Vec3i right)
        {
            return new Vec3i() { x = left.x - right.x, y = left.y - right.y, z = left.z - right.z };
        }
        /// <summary>
        /// Subtracts 2 vectors from each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subtract(ref Vec3i left, ref Vec3i right, out Vec3i result)
        {
            result = new Vec3i() { x = left.x - right.x, y = left.y - right.y, z = left.z - right.z };
        }

        /// <summary>
        /// Multiplies 2 vectors with each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3i Multiply(Vec3i left, Vec3i right)
        {
            return new Vec3i() { x = left.x * right.x, y = left.y * right.y, z = left.z * right.z };
        }
        /// <summary>
        /// Multiplies 2 vectors with each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Vec3i left, ref Vec3i right, out Vec3i result)
        {
            result = new Vec3i() { x = left.x * right.x, y = left.y * right.y, z = left.z * right.z };
        }

        /// <summary>
        /// Divides 2 vectors with each other component wise.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3i Divide(Vec3i left, Vec3i right)
        {
            return new Vec3i() { x = left.x / right.x, y = left.y / right.y, z = left.z / right.z };
        }

        /// <summary>
        /// Divides 2 vectors with each other component wise.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Divide(ref Vec3i left, ref Vec3i right, out Vec3i result)
        {
            result = new Vec3i() { x = left.x / right.x, y = left.y / right.y, z = left.z / right.z };
        }

        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3i Negate(Vec3i vec)
        {
            return new Vec3i() { x = -vec.x, y = -vec.y, z = -vec.z };
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
        public static Vec3i operator +(Vec3i left, Vec3i right)
        {
            return new Vec3i()
            {
                x = left.x + right.x,
                y = left.y + right.y,
                z = left.z + right.z,
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
        public static Vec3i operator -(Vec3i left, Vec3i right)
        {
            return new Vec3i()
            {
                x = left.x - right.x,
                y = left.y - right.y,
                z = left.z - right.z,
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
        public static Vec3i operator *(Vec3i left, Vec3i right)
        {
            return new Vec3i()
            {
                x = left.x * right.x,
                y = left.y * right.y,
                z = left.z * right.z,
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
        public static Vec3i operator /(Vec3i left, Vec3i right)
        {
            return new Vec3i()
            {
                x = left.x / right.x,
                y = left.y / right.y,
                z = left.z / right.z,
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
        public static Vec3i operator +(Vec3i vec, int scalar)
        {
            return new Vec3i()
            {
                x = vec.x + scalar,
                y = vec.y + scalar,
                z = vec.z + scalar,
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
        public static Vec3i operator -(Vec3i vec, int scalar)
        {
            return new Vec3i()
            {
                x = vec.x - scalar,
                y = vec.y - scalar,
                z = vec.z - scalar,
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
        public static Vec3i operator *(Vec3i vec, int scalar)
        {
            return new Vec3i()
            {
                x = vec.x * scalar,
                y = vec.y * scalar,
                z = vec.z * scalar,
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
        public static Vec3i operator /(Vec3i vec, int scalar)
        {
            return new Vec3i()
            {
                x = vec.x / scalar,
                y = vec.y / scalar,
                z = vec.z / scalar,
            };
            //int f = 1.0f / scalar;

            //return new Vec3i()
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
        public static Vec3i operator +(int scalar, Vec3i vec)
        {
            return new Vec3i()
            {
                x = scalar + vec.x,
                y = scalar + vec.y,
                z = scalar + vec.z
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
        public static Vec3i operator -(int scalar, Vec3i vec)
        {
            return new Vec3i()
            {
                x = scalar - vec.x,
                y = scalar - vec.y,
                z = scalar - vec.z
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
        public static Vec3i operator *(int scalar, Vec3i vec)
        {
            return new Vec3i()
            {
                x = scalar * vec.x,
                y = scalar * vec.y,
                z = scalar * vec.z
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
        public static Vec3i operator /(int scalar, Vec3i vec)
        {
            return new Vec3i()
            {
                x = scalar / vec.x,
                y = scalar / vec.y,
                z = scalar / vec.z
            };
            //int f = 1.0f / scalar;

            //return new Vec3i()
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
        public static Vec3i operator -(Vec3i vec)
        {
            return new Vec3i()
            {
                x = -vec.x,
                y = -vec.y,
                z = -vec.z   
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
        public static bool operator ==(Vec3i left, Vec3i right)
        {
            return
                left.x == right.x &&
                left.y == right.y &&
                left.z == right.z;
        }

        /// <summary>
        /// Compares two vectors with each other for inequality
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true if not equal, false otherwise.</returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vec3i left, Vec3i right)
        {
            return
                left.x != right.x ||
                left.y != right.y ||
                left.z != right.z;
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3i operator %(Vec3i vec, int scalar)
        {
            return new Vec3i()
            {
                x = vec.x % scalar,
                y = vec.y % scalar,
                z = vec.z % scalar
            };
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3i operator %(int scalar, Vec3i vec)
        {
            return new Vec3i()
            {
                x = scalar % vec.x,
                y = scalar % vec.y,
                z = scalar % vec.z
            };
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3i operator %(Vec3i left, Vec3i right)
        {
            return new Vec3i()
            {
                x = left.x % right.x,
                y = left.y % right.y,
                z = left.y % right.z
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
        public static Vec3i operator <<(Vec3i vec, int bitshift)
        {
            return new Vec3i()
            {
                x = vec.x << bitshift,
                y = vec.y << bitshift,
                z = vec.z << bitshift
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
        public static Vec3i operator >>(Vec3i vec, int bitshift)
        {
            return new Vec3i()
            {
                x = vec.x >> bitshift,
                y = vec.y >> bitshift,
                z = vec.z >> bitshift
            };
        }

        /// <summary>
        /// Returns unsafe int* pointer to x.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public unsafe static explicit operator int*(Vec3i vec)
        {
            return &vec.x;
        }
        /// <summary>
        /// Returns unsafe IntPtr pointer to x.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public unsafe static explicit operator IntPtr(Vec3i vec)
        {
            return (IntPtr)(&vec.x);
        }

        /// <summary>
        /// Cast a vec2i to a Vec3i with z = 0
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vec3i(Vec2i vec)
        {
            return new Vec3i(vec.x, vec.y, 0);
        }

        /// <summary>
        /// Casting a vec4i to a vec3i, loosing w component.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vec3i(Vec4i vec)
        {
            return new Vec3i(vec.x, vec.y, vec.z);
        }

        /// <summary>
        /// Cast a vec3f to a Vec3i, losing floating point accurency.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vec3i(Vec3f vec)
        {
            return new Vec3i((int)vec.x, (int)vec.y, (int)vec.z);
        }

        /// <summary>
        /// Cast a vec3i to a Vec3f
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vec3f(Vec3i vec)
        {
            return new Vec3f(vec.x, vec.y, vec.z);
        }

        #endregion

        #region Object Overloads

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return string.Format("[{0}, {1}, {2}]", x, y, z);
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            if (obj is Vec3i)
                return Equals((Vec3i)obj);
            else
                return false;
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vec3i other)
        {
            return x == other.x && y == other.y && z == other.z;
        }

        #endregion

    }
}
