using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Kraggs.Graphics.Math3D.Veci
{
    /// <summary>
    /// An integer 2d vector.
    /// </summary>
    [DebuggerDisplay("[ {x}, {y} ]")]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct Vec2i : IEquatable<Vec2i>
    {
        /// <summary>
        /// The x component.
        /// </summary>
        public int x;
        /// <summary>
        /// The y component.
        /// </summary>
        public int y;

        public static readonly Vec2i Zero = new Vec2i(0);
        public static readonly Vec2i One = new Vec2i(1);

        public static readonly Vec2i UnitX = new Vec2i() { x = 1, y = 0 };
        public static readonly Vec2i UnitY = new Vec2i() { x = 0, y = 1 };

        #region Constructors

        /// <summary>
        /// Creates a new vector with all the same component.
        /// </summary>
        /// <param name="val"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vec2i(int val)
        {
            this.x = val;
            this.y = val;
        }

        /// <summary>
        /// Creates a new vector with specified components.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vec2i(int x, int y)
        {
            this.x = x;
            this.y = y;
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
                return x * x + y * y;
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
                return MathF.Sqrt(x * x + y * y);                
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
        public static float Distance(Vec2i left, Vec2i right)
        {
            var t = new Vec2i() { x = left.x - right.x, y = left.y - right.y };
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
        public static Vec2i Clamp(Vec2i v, int min, int max)
        {
            return new Vec2i()
            {
                ////return x < 0.0f ? 0.0f : x > 1.0f ? 1.0f : x;
                x = v.x < min ? min : v.x > max ? max : v.x,
                y = v.y < min ? min : v.y > max ? max : v.y
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
        public static Vec2i Clamp(Vec2i v, Vec2i min, Vec2i max)
        {
            return new Vec2i()
            {
                //return x < 0.0f ? 0.0f : x > 1.0f ? 1.0f : x;
                x = v.x < min.x ? min.x : v.x > max.x ? max.x : v.x,
                y = v.y < min.y ? min.y : v.y > max.y ? max.y : v.y
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
        public static Vec2i Mix(Vec2i x, Vec2i y, int a)
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
        public static Vec2i Abs(Vec2i v)
        {
            return new Vec2i()
            {
                x = Math.Abs(v.x),
                y = Math.Abs(v.y)
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
        public static Vec2i Max(Vec2i left, Vec2i right)
        {
            return new Vec2i()
            {
                x = left.x > right.x ? left.x : right.x,
                y = left.y > right.y ? left.y : right.y,
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
        public static Vec2i Min(Vec2i left, Vec2i right)
        {
            return new Vec2i()
            {
                x = left.x < right.x ? left.x : right.x,
                y = left.y < right.y ? left.y : right.y,
                //x = Math.Min(left.x, right.x),
                //y = Math.Min(left.y, right.y)
            };
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2i HigherMultiple(Vec2i source, Vec2i multiple)
        {
            var tmp = source % multiple;

            return (tmp.x != 0 || tmp.y != 0) ?
                source + multiple - tmp : source;
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2i LowerMultiple(Vec2i source, Vec2i multiple)
        {
            var tmp = source % multiple;

            return (tmp.x != 0 || tmp.y != 0) ?
                source - tmp : source;
        }

        /// <summary>
        /// Returns the result of all components multiplied.
        /// aka x * y
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ComponentMultiply(Vec2i vec)
        {
            return 1 * vec.x * vec.y;
        }

        /// <summary>
        /// Returns the sum of all the components.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ComponentAdd(Vec2i vec)
        {
            return vec.x + vec.y;
        }

        /// <summary>
        /// Returns the largest component of a vector.
        /// aka which are largest ov x, y
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ComponentMax(Vec2i vec)
        {
            return Math.Max(vec.x, vec.y);
        }

        /// <summary>
        /// Returns the smallest component of a vector.
        /// aka which is smallest of x, y
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ComponentMin(Vec2i vec)
        {
            return Math.Min(vec.x, vec.y);
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
        public static Vec2i Add(Vec2i vec, int scalar)
        {
            return new Vec2i() { x = vec.x + scalar, y = vec.y + scalar };
        }

        /// <summary>
        /// Adds a vector and a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add(ref Vec2i vec, int scalar, out Vec2i result)
        {
            result = new Vec2i() { x = vec.x + scalar, y = vec.y + scalar };
        }

        /// <summary>
        /// Subtracts from a vector a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2i Subtract(Vec2i vec, int scalar)
        {
            return new Vec2i() { x = vec.x - scalar, y = vec.y - scalar };
        }

        /// <summary>
        /// Subtracts from a vector a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subtract(ref Vec2i vec, int scalar, out Vec2i result)
        {
            result = new Vec2i() { x = vec.x - scalar, y = vec.y - scalar };
        }

        /// <summary>
        /// Multiplies a vector and a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2i Multiply(Vec2i vec, int scalar)
        {
            return new Vec2i() { x = vec.x * scalar, y = vec.y * scalar };
        }

        /// <summary>
        /// Multiplies a vector and a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Vec2i vec, int scalar, out Vec2i result)
        {
            result = new Vec2i() { x = vec.x * scalar, y = vec.y * scalar };
        }

        /// <summary>
        /// Divides a vector with a scalar component wise.
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2i Divide(Vec2i vec, int scalar)
        {
            return new Vec2i(vec.x / scalar, vec.y / scalar);
            //return new Vec2i()
            //{
            //    x = vec.x / scalar,
            //    y = vec.y / scalar
            //};
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
        public static void Divide(ref Vec2i vec, int scalar, out Vec2i result)
        {
            result.x = vec.x / scalar;
            result.y = vec.y / scalar;
            //Multiply(ref vec, 1 / scalar, out result);
            //result = new Vec2i() { x = vec.x + scalar, y = vec.y + scalar };

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
        public static Vec2i Add(Vec2i left, Vec2i right)
        {
            return new Vec2i() { x = left.x + right.x, y = left.y + right.y };
            //Vec2i result;
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
        public static void Add(ref Vec2i left, ref Vec2i right, out Vec2i result)
        {
            result = new Vec2i() { x = left.x + right.x, y = left.y + right.y };
        }

        /// <summary>
        /// Subtracts 2 vectors from each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2i Subtract(Vec2i left, Vec2i right)
        {
            return new Vec2i() { x = left.x - right.x, y = left.y - right.y };
        }
        /// <summary>
        /// Subtracts 2 vectors from each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subtract(ref Vec2i left, ref Vec2i right, out Vec2i result)
        {
            result = new Vec2i() { x = left.x - right.x, y = left.y - right.y };
        }

        /// <summary>
        /// Multiplies 2 vectors with each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2i Multiply(Vec2i left, Vec2i right)
        {
            return new Vec2i() { x = left.x * right.x, y = left.y * right.y };
        }
        /// <summary>
        /// Multiplies 2 vectors with each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Vec2i left, ref Vec2i right, out Vec2i result)
        {
            result = new Vec2i() { x = left.x * right.x, y = left.y * right.y };
        }

        /// <summary>
        /// Divides 2 vectors with each other component wise.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2i Divide(Vec2i left, Vec2i right)
        {
            return new Vec2i() { x = left.x / right.x, y = left.y / right.y };
        }

        /// <summary>
        /// Divides 2 vectors with each other component wise.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Divide(ref Vec2i left, ref Vec2i right, out Vec2i result)
        {
            result = new Vec2i() { x = left.x / right.x, y = left.y / right.y };
        }

        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2i Negate(Vec2i vec)
        {
            return new Vec2i() { x = -vec.x, y = -vec.y };
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
        public static Vec2i operator +(Vec2i left, Vec2i right)
        {
            return new Vec2i()
            {
                x = left.x + right.x,
                y = left.y + right.y
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
        public static Vec2i operator -(Vec2i left, Vec2i right)
        {
            return new Vec2i()
            {
                x = left.x - right.x,
                y = left.y - right.y
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
        public static Vec2i operator *(Vec2i left, Vec2i right)
        {
            return new Vec2i()
            {
                x = left.x * right.x,
                y = left.y * right.y
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
        public static Vec2i operator /(Vec2i left, Vec2i right)
        {
            return new Vec2i()
            {
                x = left.x / right.x,
                y = left.y / right.y
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
        public static Vec2i operator +(Vec2i vec, int scalar)
        {
            return new Vec2i()
            {
                x = vec.x + scalar,
                y = vec.y + scalar
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
        public static Vec2i operator -(Vec2i vec, int scalar)
        {
            return new Vec2i()
            {
                x = vec.x - scalar,
                y = vec.y - scalar
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
        public static Vec2i operator *(Vec2i vec, int scalar)
        {
            return new Vec2i()
            {
                x = vec.x * scalar,
                y = vec.y * scalar
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
        public static Vec2i operator /(Vec2i vec, int scalar)
        {
            return new Vec2i()
            {
                x = vec.x / scalar,
                y = vec.y / scalar
            };
            //int f = 1.0f / scalar;

            //return new Vec2i()
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
        public static Vec2i operator +(int scalar, Vec2i vec)
        {
            return new Vec2i()
            {
                x = scalar + vec.x,
                y = scalar + vec.y
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
        public static Vec2i operator -(int scalar, Vec2i vec)
        {
            return new Vec2i()
            {
                x = scalar - vec.x,
                y = scalar - vec.y
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
        public static Vec2i operator *(int scalar, Vec2i vec)
        {
            return new Vec2i()
            {
                x = scalar * vec.x,
                y = scalar * vec.y
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
        public static Vec2i operator /(int scalar, Vec2i vec)
        {
            return new Vec2i()
            {
                x = scalar / vec.x,
                y = scalar / vec.y
            };
            //int f = 1.0f / scalar;

            //return new Vec2i()
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
        public static Vec2i operator -(Vec2i vec)
        {
            return new Vec2i()
            {
                x = -vec.x,
                y = -vec.y
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
        public static bool operator ==(Vec2i left, Vec2i right)
        {
            return
                left.x == right.x &&
                left.y == right.y;
        }

        /// <summary>
        /// Compares two vectors with each other for inequality
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true if not equal, false otherwise.</returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vec2i left, Vec2i right)
        {
            return
                left.x != right.x ||
                left.y != right.y;
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2i operator %(Vec2i vec, int scalar)
        {
            return new Vec2i()
            {
                x = vec.x % scalar,
                y = vec.y % scalar
            };
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2i operator %(int scalar, Vec2i vec)
        {
            return new Vec2i()
            {
                x = scalar % vec.x,
                y = scalar % vec.y
            };
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2i operator %(Vec2i left, Vec2i right)
        {
            return new Vec2i()
            {
                x = left.x % right.x,
                y = left.y % right.y
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
        public static Vec2i operator <<(Vec2i vec, int bitshift)
        {
            return new Vec2i()
            {
                x = vec.x << bitshift,
                y = vec.y << bitshift
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
        public static Vec2i operator >>(Vec2i vec, int bitshift)
        {
            return new Vec2i()
            {
                x = vec.x >> bitshift,
                y = vec.y >> bitshift
            };
        }

        /// <summary>
        /// Returns unsafe int* pointer to x.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public unsafe static explicit operator int*(Vec2i vec)
        {
            return &vec.x;
        }
        /// <summary>
        /// Returns unsafe IntPtr pointer to x.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public unsafe static explicit operator IntPtr(Vec2i vec)
        {
            return (IntPtr)(&vec.x);
        }

        /// <summary>
        /// Cast a vec3f to a Vec2i, loosing z component.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vec2i(Vec3i vec)
        {
            return new Vec2i(vec.x, vec.y);
        }

        /// <summary>
        /// Casting a vec4f to a vec3f, loosing z and w components.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vec2i(Vec4i vec)
        {
            return new Vec2i(vec.x, vec.y);
        }

        /// <summary>
        /// Cast a vec3f to a Vec2i, loosing z component.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vec2i(Vec2f vec)
        {
            return new Vec2i((int)vec.x, (int)vec.y);
        }

        /// <summary>
        /// Cast a vec3f to a Vec2i, loosing z component.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vec2f(Vec2i vec)
        {
            return new Vec2f(vec.x, vec.y);
        }

        #endregion

        #region Object Overloads

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode();
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return string.Format("[{0}, {1}]", x, y);
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            if (obj is Vec2i)
                return Equals((Vec2i)obj);
            else
                return false;
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vec2i other)
        {
            return x == other.x && y == other.y;
        }

        #endregion
    }
}
