using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Kraggs.Graphics.Math3D
{
    /// <summary>
    /// A floating-point 3d vector.
    /// </summary>
    [DebuggerDisplay("[ {x}, {y}, {z} ]")]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct Vec3f
    {
        /// <summary>
        /// The x component.
        /// </summary>
        public float x;
        /// <summary>
        /// The y component.
        /// </summary>
        public float y;
        /// <summary>
        /// The z component.
        /// </summary>
        public float z;

        #region Properties

        /// <summary>
        /// A Vec3f with all components set to zero.
        /// </summary>
        public static readonly Vec3f Zero = new Vec3f() { x = 0.0f, y = 0.0f, z = 0.0f };
        /// <summary>
        /// A Vec3f with X component 1 and all others zero.
        /// </summary>
        public static readonly Vec3f UnitX = new Vec3f() { x = 1.0f, y = 0.0f, z = 0.0f };
        /// <summary>
        /// A Vec3f with Y component 1 and all others zero.
        /// </summary>
        public static readonly Vec3f UnitY = new Vec3f() { x = 0.0f, y = 1.0f, z = 0.0f };
        /// <summary>
        /// A Vec3f with Z component 1 and all others zero.
        /// </summary>
        public static readonly Vec3f UnitZ = new Vec3f() { x = 0.0f, y = 0.0f, z = 1.0f };

        /// <summary>
        /// Returns the unsqrt length of the vector.
        /// </summary>
        [DebuggerNonUserCode()]
        public float LengthSquared
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
                return MathFunctions.Sqrt(x * x + y * y + z * z);
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
            var f = 1.0f / MathFunctions.Sqrt(x * x + y * y + z * z);

            this.x = x * f;
            this.y = y * f;
            this.z = z * f;
        }

        /// <summary>
        /// Returns a normalized version of this vector.
        /// </summary>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vec3f GetNormal()
        {
            var f = 1.0f / MathFunctions.Sqrt(x * x + y * y + z * z);

            return new Vec3f() { x = x * f, y = y * f, z = z * f };
        }
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vec3f GetFastNormal()
        {
            var f = MathFunctions.FastInverseSqrt(x * x + y * y + z * z);

            return new Vec3f() { x = x * f, y = y * f, z = z * f };
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
        public static Vec3f Normalize(Vec3f vec)
        {
            var f = 1.0f / MathFunctions.Sqrt(vec.x * vec.x + vec.y * vec.y + vec.z * vec.z);

            return new Vec3f()
            {
                x = vec.x * f,
                y = vec.y * f,
                z = vec.z * f
            };
        }


        /// <summary>
        /// Returns the distance between 2 vectors.
        /// </summary>
        /// <param name="left">left side</param>
        /// <param name="right">right side.</param>
        /// <returns>distance between 2 points.</returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(Vec3f left, Vec3f right)
        {
            var t = new Vec3f() { x = left.x - right.x, y = left.y - right.y, z = left.z - right.z };
            return t.Length;
        }

        /// <summary>
        /// Returns the dot product of two pre-normlized vectors.
        /// </summary>
        /// <param name="left">a normalized vector.</param>
        /// <param name="right">a normlized vector.</param>
        /// <returns>dot product.</returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(Vec3f left, Vec3f right)
        {
            return left.x * right.x + left.y * right.y + left.z * right.z;
        }
        /// <summary>
        /// Returns the dot product of two pre-normlized vectors.
        /// </summary>
        /// <param name="left">a normalized vector.</param>
        /// <param name="right">a normalized vector.</param>
        /// <param name="result">dot product</param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Dot(ref Vec3f left, ref Vec3f right, out float result)
        {
            result = left.x * right.x + left.y * right.y + left.z * right.z;
        }

        /// <summary>
        /// Returns the cross product of two pre-normalized vectors.
        /// </summary>
        /// <param name="left">a normalized vector.</param>
        /// <param name="right">a normalized vector.</param>
        /// <returns>cross product</returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f Cross(Vec3f left, Vec3f right)
        {
            return new Vec3f()
            {
                x = left.y * right.z - left.z * right.y,
                y = left.z * right.x - left.x * right.z,
                z = left.x * right.y - left.y * right.x
            };            
        }

        /// <summary>
        /// Returns the cross product of two pre-normalized vectors.
        /// </summary>
        /// <param name="left">a normalized vector.</param>
        /// <param name="right">a normalied vector</param>
        /// <param name="result">the cross product.</param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Cross(ref Vec3f left, ref Vec3f right, out Vec3f result)
        {
            result = new Vec3f()
            {
                x = left.y * right.z - left.z * right.y,
                y = left.z * right.x - left.x * right.z,
                z = left.x * right.y - left.y * right.x
            };
        }

        /// <summary>
        /// If Dot(Nref, I) < 0.0, return N otherwise return -N
        /// </summary>
        /// <param name="N"></param>
        /// <param name="I"></param>
        /// <param name="Nref"></param>
        /// <returns></returns>
        public static Vec3f FaceForward(Vec3f N, Vec3f I, Vec3f Nref)
        {
            //TODO: Use operatores instead of function madness.
            //return Dot(Nref, I) < 0 ? N : -N;

            return Dot(Nref, I) < 0.0f ? N : Negate(N);
        }

        /// <summary>
        /// For the incident vector I and surface orientation N,
        /// return the reflection direction: result = I - 2.0 * Dot(N, I) * N.
        /// </summary>
        /// <param name="I"></param>
        /// <param name="N"></param>
        /// <returns></returns>
        public static Vec3f Reflect(Vec3f I, Vec3f N)
        {
            //TODO: Use operatores instead of function madness.
            // return I - N * Dot(N, I) * 2.0f

            return Subtract(I, Multiply(N, Dot(N, I) * 2.0f));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="I"></param>
        /// <param name="N"></param>
        /// <returns></returns>
        public static Vec3f Refract(Vec3f I, Vec3f N, float eta)
        {
            //TODO: Use operatores instead of function madness.
            

            float dotValue = Dot(N, I);
            float k = 1.0f - eta * eta * (1.0f - dotValue * dotValue);

            if (k < 0.0f)
                return Vec3f.Zero;
            else
                return Multiply(Subtract(Multiply(I, eta), (eta * dotValue + MathFunctions.Sqrt(k))), N);
                            
                //return Multiply(Subtract(Multiply(I, eta),sdf), N);
                //return eta * I - (eta * dotValue + MathFunctions.Sqrt(k)) * N;

            // return eta * I - (eta * dotValue + MathFunctions.Sqrt(k)) * N;
        }


        /// <summary>
        /// Normalized parameters and returns the dot product of left and right.
        /// Its faster than dot(left.GetNormal(), right.GetNormal()).
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float NormalizeDot(Vec3f left, Vec3f right)
        {
            return
                Dot(left, right) *
                MathFunctions.InverseSqrt(Dot(left, left)) *
                Dot(right, right);
        }

        /// <summary>
        /// Normalized parameters and returns the dot product of left and right.
        /// Its faster than dot(left.GetFastNormal(), right.GetFastNormal()).
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static float FastNormalizeDot(Vec3f left, Vec3f right)
        {
            return
                Dot(left, right) *
                MathFunctions.FastInverseSqrt(Dot(left, left)) *
                Dot(right, right);
        }

        /// <summary>
        /// Normalized parameters and returns the dot product of left and right.
        /// Its faster than dot(left.GetFastNormal(), right.GetFastNormal()).
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="result"></param>
        public static void FastNormalizeDot(ref Vec3f left, ref Vec3f right, out float result)
        {
            float a, b, c;
            Dot(ref left, ref right, out a);
            Dot(ref left, ref left, out b);
            Dot(ref right, ref right, out c);

            result = a * MathFunctions.FastInverseSqrt(b) * c;
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
        public static Vec3f Add(Vec3f vec, float scalar)
        {
            return new Vec3f() { x = vec.x + scalar, y = vec.y + scalar, z = vec.z + scalar };
        }
        /// <summary>
        /// Adds a vector and a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add(ref Vec3f vec, float scalar, out Vec3f result)
        {
            result = new Vec3f() { x = vec.x + scalar, y = vec.y + scalar, z = vec.z + scalar };
        }

        /// <summary>
        /// Subtracts from a vector a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f Subtract(Vec3f vec, float scalar)
        {
            return new Vec3f() { x = vec.x - scalar, y = vec.y - scalar, z = vec.z - scalar };
        }
        /// <summary>
        /// Subtracts from a vector a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subtract(ref Vec3f vec, float scalar, out Vec3f result)
        {
            result = new Vec3f() { x = vec.x - scalar, y = vec.y - scalar, z = vec.z - scalar };
        }

        /// <summary>
        /// Multiplies a vector and a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f Multiply(Vec3f vec, float scalar)
        {
            return new Vec3f() { x = vec.x * scalar, y = vec.y * scalar, z = vec.z * scalar };
        }
        /// <summary>
        /// Multiplies a vector and a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Vec3f vec, float scalar, out Vec3f result)
        {
            result = new Vec3f() { x = vec.x * scalar, y = vec.y * scalar, z = vec.z * scalar };
        }

        /// <summary>
        /// Divides a vector with a scalar component wise.
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f Divide(Vec3f vec, float scalar)
        {
            //return new Vec3f() { x = vec.x + scalar, y = vec.y + scalar };
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
        public static void Divide(ref Vec3f vec, float scalar, out Vec3f result)
        {
            Multiply(ref vec, 1.0f / scalar, out result);
            //result = new Vec3f() { x = vec.x + scalar, y = vec.y + scalar };
        }

        /// <summary>
        /// Adds to vectors to each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f Add(Vec3f left, Vec3f right)
        {
            return new Vec3f() { x = left.x + right.x, y = left.y + right.y, z = left.z + right.z };
            //Vec3f result;
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
        public static void Add(ref Vec3f left, ref Vec3f right, out Vec3f result)
        {
            result = new Vec3f() { x = left.x + right.x, y = left.y + right.y, z = left.z + right.z };
        }

        /// <summary>
        /// Subtracts 2 vectors from each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f Subtract(Vec3f left, Vec3f right)
        {
            return new Vec3f() { x = left.x - right.x, y = left.y - right.y, z = left.z - right.z };
        }
        /// <summary>
        /// Subtracts 2 vectors from each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subtract(ref Vec3f left, ref Vec3f right, out Vec3f result)
        {
            result = new Vec3f() { x = left.x - right.x, y = left.y - right.y, z = left.z - right.z };
        }

        /// <summary>
        /// Multiplies 2 vectors with each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f Multiply(Vec3f left, Vec3f right)
        {
            return new Vec3f() { x = left.x * right.x, y = left.y * right.y, z = left.z * right.z };
        }
        /// <summary>
        /// Multiplies 2 vectors with each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Vec3f left, ref Vec3f right, out Vec3f result)
        {
            result = new Vec3f() { x = left.x * right.x, y = left.y * right.y, z = left.z * right.z };
        }

        /// <summary>
        /// Divides 2 vectors with each other component wise.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f Divide(Vec3f left, Vec3f right)
        {
            return new Vec3f() { x = left.x / right.x, y = left.y / right.y, z = left.z / right.z };
        }
        /// <summary>
        /// Divides 2 vectors with each other component wise.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Divide(ref Vec3f left, ref Vec3f right, out Vec3f result)
        {
            result = new Vec3f() { x = left.x / right.x, y = left.y / right.y, z = left.z / right.z };
        }

        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f Negate(Vec3f vec)
        {
            return new Vec3f() { x = -vec.x, y = -vec.y, z = -vec.z };
        }

        #endregion

    }
}
