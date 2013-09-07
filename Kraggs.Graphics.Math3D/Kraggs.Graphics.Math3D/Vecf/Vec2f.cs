using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Kraggs.Graphics.Math3D
{
    /// <summary>
    /// A floating point 2D vector.
    /// </summary>
    [DebuggerDisplay("[ {x}, {y} ]")]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct Vec2f
    {
        /// <summary>
        /// The x component.
        /// </summary>
        public float x;
        /// <summary>
        /// The y component.
        /// </summary>
        public float y;

        #region Properties

        /// <summary>
        /// A Vec2f with all components zero.
        /// </summary>
        public static readonly Vec2f Zero = new Vec2f() { x = 0.0f, y = 0.0f };
        /// <summary>
        /// A Vec2f with X component 1 and all others zero.
        /// </summary>
        public static readonly Vec2f UnitX = new Vec2f() { x = 1.0f, y = 0.0f };
        /// <summary>
        /// A Vec3f with Y component 1 and all others zero.
        /// </summary>
        public static readonly Vec2f UnitY = new Vec2f() { x = 0.0f, y = 1.0f };

        /// <summary>
        /// Returns the unsqrt length of the vector.
        /// </summary>
        [DebuggerNonUserCode()]
        public float LengthSquared
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
                return MathFunctions.Sqrt(x * x + y * y);
            }
        }

        #endregion

        #region Instance Functions

        /// <summary>
        /// Normalizes this vector
        /// </summary>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Normalize()
        {
            var f = 1.0f / MathFunctions.Sqrt(x * x + y * y);

            this.x = x * f;
            this.y = y * f;            
        }

        /// <summary>
        /// Returns a normalized version of this vector.
        /// </summary>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vec2f GetNormal()
        {
            var f = 1.0f / MathFunctions.Sqrt(x * x + y * y);

            return new Vec2f() { x = x * f, y = y * f};
        }

        #endregion

        #region Static Functions

        /// <summary>
        /// Returns a Normlized version of the vector.
        /// </summary>
        /// <param name="vec">Vector to get normal from.</param>
        /// <returns>Normalized vector.</returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2f Normalize(Vec2f vec)
        {
            var f = 1.0f / MathFunctions.Sqrt(vec.x * vec.x + vec.y * vec.y);

            return new Vec2f()
            {
                x = vec.x * f,
                y = vec.y * f
            };
        }

        /// <summary>
        /// Returns the distance between 2 vectors.
        /// </summary>
        /// <param name="left">left side</param>
        /// <param name="right">right side.</param>
        /// <returns>distance</returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(Vec2f left, Vec2f right)
        {
            var t = new Vec2f() { x = left.x - right.x, y = left.y - right.y };
            return t.Length;
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(Vec2f left, Vec2f right)
        {
            return left.x * right.x + left.y * right.y;
        }

        //[DebuggerNonUserCode()]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Vec2f Cross(Vec2f left, Vec2f right)
        //{
        //    //return new Vec2f
        //    throw new NotImplementedException();
        //}

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
        public static Vec2f Add(Vec2f vec, float scalar)
        {
            return new Vec2f() { x = vec.x + scalar, y = vec.y + scalar };
        }
        /// <summary>
        /// Adds a vector and a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add(ref Vec2f vec, float scalar, out Vec2f result)
        {
            result = new Vec2f() { x = vec.x + scalar, y = vec.y + scalar };
        }

        /// <summary>
        /// Subtracts from a vector a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2f Subtract(Vec2f vec, float scalar)
        {
            return new Vec2f() { x = vec.x - scalar, y = vec.y - scalar };
        }
        /// <summary>
        /// Subtracts from a vector a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subtract(ref Vec2f vec, float scalar, out Vec2f result)
        {
            result = new Vec2f() { x = vec.x - scalar, y = vec.y - scalar };
        }

        /// <summary>
        /// Multiplies a vector and a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2f Multiply(Vec2f vec, float scalar)
        {
            return new Vec2f() { x = vec.x * scalar, y = vec.y * scalar };
        }
        /// <summary>
        /// Multiplies a vector and a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Vec2f vec, float scalar, out Vec2f result)
        {
            result = new Vec2f() { x = vec.x * scalar, y = vec.y * scalar };
        }

        /// <summary>
        /// Divides a vector with a scalar component wise.
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2f Divide(Vec2f vec, float scalar)
        {
            //return new Vec2f() { x = vec.x + scalar, y = vec.y + scalar };
            Divide(ref vec, scalar, out vec);
            return vec;
        }
        /// <summary>
        /// Divides a vector with a scalar component wise.
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Divide(ref Vec2f vec, float scalar, out Vec2f result)
        {
            Multiply(ref vec, 1.0f / scalar, out result);
            //result = new Vec2f() { x = vec.x + scalar, y = vec.y + scalar };
        }

        /// <summary>
        /// Adds to vectors to each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2f Add(Vec2f left, Vec2f right)
        {
            return new Vec2f() { x = left.x + right.x, y = left.y + right.y };
            //Vec2f result;
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
        public static void Add(ref Vec2f left, ref Vec2f right, out Vec2f result)
        {
            result = new Vec2f() { x = left.x + right.x, y = left.y + right.y };
        }

        /// <summary>
        /// Subtracts 2 vectors from each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2f Subtract(Vec2f left, Vec2f right)
        {
            return new Vec2f() { x = left.x - right.x, y = left.y - right.y };
        }
        /// <summary>
        /// Subtracts 2 vectors from each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subtract(ref Vec2f left, ref Vec2f right, out Vec2f result)
        {
            result = new Vec2f() { x = left.x - right.x, y = left.y - right.y };
        }

        /// <summary>
        /// Multiplies 2 vectors with each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2f Multiply(Vec2f left, Vec2f right)
        {
            return new Vec2f() { x = left.x * right.x, y = left.y * right.y };
        }
        /// <summary>
        /// Multiplies 2 vectors with each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Vec2f left, ref Vec2f right, out Vec2f result)
        {
            result = new Vec2f() { x = left.x * right.x, y = left.y * right.y };
        }

        /// <summary>
        /// Divides 2 vectors with each other component wise.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2f Divide(Vec2f left, Vec2f right)
        {
            return new Vec2f() { x = left.x / right.x, y = left.y / right.y };
        }
        /// <summary>
        /// Divides 2 vectors with each other component wise.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Divide(ref Vec2f left, ref Vec2f right, out Vec2f result)
        {
            result = new Vec2f() { x = left.x / right.x, y = left.y / right.y };
        }

        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2f Negate(Vec2f vec)
        {
            return new Vec2f() { x = -vec.x, y = -vec.y};
        }

        #endregion

    }
}
