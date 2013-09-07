using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Kraggs.Graphics.Math3D
{
    /// <summary>
    /// A floating-point 4d vector.
    /// </summary>
    [DebuggerDisplay("[ {x}, {y}, {z}, {w} ]")]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct Vec4f : IEquatable<Vec4f>
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
        /// <summary>
        /// The w component.
        /// </summary>
        public float w;

        #region Properties

        /// <summary>
        /// A Vec4f with all components set to zero.
        /// </summary>
        public static readonly Vec4f Zero = new Vec4f() { x = 0.0f, y = 0.0f, z = 0.0f, w = 0.0f };
        /// <summary>
        /// A Vec4f with X component 1 and all others zero.
        /// </summary>
        public static readonly Vec4f UnitX = new Vec4f() { x = 1.0f, y = 0.0f, z = 0.0f, w = 0.0f };
        /// <summary>
        /// A Vec4f with Y component 1 and all others zero.
        /// </summary>
        public static readonly Vec4f UnitY = new Vec4f() { x = 0.0f, y = 1.0f, z = 0.0f, w = 0.0f };
        /// <summary>
        /// A Vec4f with Z component 1 and all others zero.
        /// </summary>
        public static readonly Vec4f UnitZ = new Vec4f() { x = 0.0f, y = 0.0f, z = 1.0f, w = 0.0f };
        /// <summary>
        /// A Vec4f with W component 1 and all others zero.
        /// </summary>
        public static readonly Vec4f UnitW = new Vec4f() { x = 0.0f, y = 0.0f, z = 0.0f, w = 1.0f };

        /// <summary>
        /// Returns the unsqrt length of the vector.
        /// </summary>
        [DebuggerNonUserCode()]
        public float LengthSquared
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
                return MathFunctions.Sqrt(x * x + y * y + z * z + w * w);
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
            var f = 1.0f / MathFunctions.Sqrt(x * x + y * y + z * z + w * w);

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
        public Vec4f GetNormal()
        {
            var f = 1.0f / MathFunctions.Sqrt(x * x + y * y + z * z + w * w);

            return new Vec4f() { x = x * f, y = y * f, z = z * f, w = w * f };
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
        public static Vec4f Normalize(Vec4f vec)
        {
            var f = 1.0f / MathFunctions.Sqrt(vec.x * vec.x + vec.y * vec.y + vec.z * vec.z + vec.w * vec.w);

            return new Vec4f()
            {
                x = vec.x * f,
                y = vec.y * f,
                z = vec.z * f,
                w = vec.w * f
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
        public static float Distance(Vec4f left, Vec4f right)
        {
            var t = new Vec4f() { x = left.x - right.x, y = left.y - right.y, z = left.z - right.z, w = left.w - right.w };
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
        public static float Dot(Vec4f left, Vec4f right)
        {
            return left.x * right.x + left.y * right.y + left.z * right.z + left.w * right.w;
        }
        /// <summary>
        /// Returns the dot product of two pre-normlized vectors.
        /// </summary>
        /// <param name="left">a normalized vector.</param>
        /// <param name="right">a normalized vector.</param>
        /// <param name="result">dot product</param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Dot(ref Vec4f left, ref Vec4f right, out float result)
        {
            result = left.x * right.x + left.y * right.y + left.z * right.z + left.w * right.w;
        }

        /// <summary>
        /// Returns the cross product of two pre-normalized vectors.
        /// </summary>
        /// <param name="left">a normalized vector.</param>
        /// <param name="right">a normalized vector.</param>
        /// <returns>cross product</returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4f Cross(Vec4f u, Vec4f v, Vec4f w)
        {
            Vec4f result;
            Cross(ref u, ref v, ref w, out result);
            return result;
        }

        /// <summary>
        /// Returns the cross product of two pre-normalized vectors.
        /// </summary>
        /// <param name="left">a normalized vector.</param>
        /// <param name="right">a normalied vector</param>
        /// <param name="result">the cross product.</param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Cross(ref Vec4f u, ref Vec4f v, ref Vec4f w, out Vec4f result)
        {
            float a, b, c, d, e, f;

            // calculate intermediate values.
            a = v.x * w.y - v.y * w.x;
            b = v.x * w.z - v.z * w.x;
            c = v.x * w.w - v.w * w.x;
            d = v.y * w.z - v.z * w.y;
            e = v.y * w.w - v.w * w.y;
            f = v.z * w.w - v.w * w.z;

            // calculate the result vector components.
            //result.x =  u.y * f - u.z * e + u.w * d;
            //result.y = -u.x * f + u.z * c - u.w * b;
            //result.z =  u.x * e - u.y * c + u.w * a;
            //result.w = -u.x * d + u.y * b - u.z * a;

            result = new Vec4f()
            {
                x =  u.y * f - u.z * e + u.w * d,
                y = -u.x * f + u.z * c - u.w * b,
                z =  u.x * e - u.y * c + u.w * a,
                w = -u.x * d + u.y * b - u.z * a
            };
        }

        /// <summary>
        /// This calculates cross product as the vectors was 3d vectors, totaly ignoring w component.
        /// Will probably be totaly wrong if w component is not 0.
        /// </summary>
        /// <param name="left">a vector with w = 0</param>
        /// <param name="right">another vector with w = 0</param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Cross(ref Vec4f left, ref Vec4f right, out Vec4f result)
        {
            result = new Vec4f()
            {
                x = left.y * right.z - left.z * right.y,
                y = left.z * right.x - left.x * right.z,
                z = left.x * right.y - left.y * right.x,
                w = 0,
            };
        }

        /// <summary>
        /// If Dot(Nref, I) < 0.0, return N otherwise return -N
        /// </summary>
        /// <param name="N"></param>
        /// <param name="I"></param>
        /// <param name="Nref"></param>
        /// <returns></returns>
        public static Vec4f FaceForward(Vec4f N, Vec4f I, Vec4f Nref)
        {
            //TODO: Use operatores instead of function madness.
            return Dot(Nref, I) < 0 ? N : -N;

            //return Dot(Nref, I) < 0.0f ? N : Negate(N);
        }

        /// <summary>
        /// For the incident vector I and surface orientation N,
        /// return the reflection direction: result = I - 2.0 * Dot(N, I) * N.
        /// </summary>
        /// <param name="I"></param>
        /// <param name="N"></param>
        /// <returns></returns>
        public static Vec4f Reflect(Vec4f I, Vec4f N)
        {
            //TODO: Use operatores instead of function madness.
            return I - N * Dot(N, I) * 2.0f;

            //return Subtract(I, Multiply(N, Dot(N, I) * 2.0f));
        }

        /// <summary>
        /// For the incident vector I and surface normal N,
        /// and the ratio of indices of refraction eta,
        /// return the refraction vector.
        /// </summary>
        /// <param name="I"></param>
        /// <param name="N"></param>
        /// <returns></returns>
        public static Vec4f Refract(Vec4f I, Vec4f N, float eta)
        {
            //TODO: Use operatores instead of function madness.

            float dotValue = Dot(N, I);
            float k = 1.0f - eta * eta * (1.0f - dotValue * dotValue);

            if (k < 0.0f)
                return Vec4f.Zero;
            else
                return eta * I - (eta * dotValue + MathFunctions.Sqrt(k)) * N;
            //TODO: Use operatores instead of function madness.
            //return Multiply(Subtract(Multiply(I, eta), (eta * dotValue + MathFunctions.Sqrt(k))), N);

            //return Multiply(Subtract(Multiply(I, eta),sdf), N);
            //return eta * I - (eta * dotValue + MathFunctions.Sqrt(k)) * N;

            // return eta * I - (eta * dotValue + MathFunctions.Sqrt(k)) * N;
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
        public static Vec4f Add(Vec4f vec, float scalar)
        {
            return new Vec4f() { x = vec.x + scalar, y = vec.y + scalar, z = vec.z + scalar, w = vec.w + scalar };
        }
        /// <summary>
        /// Adds a vector and a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add(ref Vec4f vec, float scalar, out Vec4f result)
        {
            result = new Vec4f() { x = vec.x + scalar, y = vec.y + scalar, z = vec.z + scalar, w = vec.w + scalar };
        }

        /// <summary>
        /// Subtracts from a vector a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4f Subtract(Vec4f vec, float scalar)
        {
            return new Vec4f() { x = vec.x - scalar, y = vec.y - scalar, z = vec.z - scalar, w = vec.w - scalar };
        }
        /// <summary>
        /// Subtracts from a vector a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subtract(ref Vec4f vec, float scalar, out Vec4f result)
        {
            result = new Vec4f() { x = vec.x - scalar, y = vec.y - scalar, z = vec.z - scalar, w = vec.w - scalar };
        }

        /// <summary>
        /// Multiplies a vector and a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4f Multiply(Vec4f vec, float scalar)
        {
            return new Vec4f() { x = vec.x * scalar, y = vec.y * scalar, z = vec.z * scalar, w = vec.w * scalar };
        }
        /// <summary>
        /// Multiplies a vector and a scalar
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Vec4f vec, float scalar, out Vec4f result)
        {
            result = new Vec4f() { x = vec.x * scalar, y = vec.y * scalar, z = vec.z * scalar, w = vec.w * scalar };
        }

        /// <summary>
        /// Divides a vector with a scalar component wise.
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4f Divide(Vec4f vec, float scalar)
        {
            //return new Vec4f() { x = vec.x + scalar, y = vec.y + scalar };
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
        public static void Divide(ref Vec4f vec, float scalar, out Vec4f result)
        {
            Multiply(ref vec, 1.0f / scalar, out result);
            //result = new Vec4f() { x = vec.x + scalar, y = vec.y + scalar };
        }

        /// <summary>
        /// Adds to vectors to each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4f Add(Vec4f left, Vec4f right)
        {
            return new Vec4f() { x = left.x + right.x, y = left.y + right.y, z = left.z + right.z, w = left.w + right.w };
            //Vec4f result;
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
        public static void Add(ref Vec4f left, ref Vec4f right, out Vec4f result)
        {
            result = new Vec4f() { x = left.x + right.x, y = left.y + right.y, z = left.z + right.z, w = left.w + right.w };
        }

        /// <summary>
        /// Subtracts 2 vectors from each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4f Subtract(Vec4f left, Vec4f right)
        {
            return new Vec4f() { x = left.x - right.x, y = left.y - right.y, z = left.z - right.z, w = left.w - right.w };
        }
        /// <summary>
        /// Subtracts 2 vectors from each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subtract(ref Vec4f left, ref Vec4f right, out Vec4f result)
        {
            result = new Vec4f() { x = left.x - right.x, y = left.y - right.y, z = left.z - right.z, w = left.w - right.w };
        }

        /// <summary>
        /// Multiplies 2 vectors with each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4f Multiply(Vec4f left, Vec4f right)
        {
            return new Vec4f() { x = left.x * right.x, y = left.y * right.y, z = left.z * right.z, w = left.w * right.w };
        }
        /// <summary>
        /// Multiplies 2 vectors with each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Vec4f left, ref Vec4f right, out Vec4f result)
        {
            result = new Vec4f() { x = left.x * right.x, y = left.y * right.y, z = left.z * right.z, w = left.w * right.w };
        }

        /// <summary>
        /// Divides 2 vectors with each other component wise.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4f Divide(Vec4f left, Vec4f right)
        {
            return new Vec4f() { x = left.x / right.x, y = left.y / right.y, z = left.z / right.z, w = left.w / right.w };
        }
        /// <summary>
        /// Divides 2 vectors with each other component wise.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="result"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Divide(ref Vec4f left, ref Vec4f right, out Vec4f result)
        {
            result = new Vec4f() { x = left.x / right.x, y = left.y / right.y, z = left.z / right.z, w = left.w / right.w };
        }

        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4f Negate(Vec4f vec)
        {
            return new Vec4f() { x = -vec.x, y = -vec.y, z = -vec.z, w = -vec.w };
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
        public static Vec4f operator +(Vec4f left, Vec4f right)
        {
            return new Vec4f()
            {
                x = left.x + right.x,
                y = left.y + right.y,
                z = left.z + right.z,
                w = left.w + right.w
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
        public static Vec4f operator -(Vec4f left, Vec4f right)
        {
            return new Vec4f()
            {
                x = left.x - right.x,
                y = left.y - right.y,
                z = left.z - right.z,
                w = left.w - right.w
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
        public static Vec4f operator *(Vec4f left, Vec4f right)
        {
            return new Vec4f()
            {
                x = left.x * right.x,
                y = left.y * right.y,
                z = left.z * right.z,
                w = left.w * right.w
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
        public static Vec4f operator /(Vec4f left, Vec4f right)
        {
            return new Vec4f()
            {
                x = left.x / right.x,
                y = left.y / right.y,
                z = left.z / right.z,
                w = left.w / right.w
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
        public static Vec4f operator +(Vec4f vec, float scalar)
        {
            return new Vec4f()
            {
                x = vec.x + scalar,
                y = vec.y + scalar,
                z = vec.z + scalar,
                w = vec.w + scalar
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
        public static Vec4f operator -(Vec4f vec, float scalar)
        {
            return new Vec4f()
            {
                x = vec.x - scalar,
                y = vec.y - scalar,
                z = vec.z - scalar,
                w = vec.w - scalar
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
        public static Vec4f operator *(Vec4f vec, float scalar)
        {
            return new Vec4f()
            {
                x = vec.x * scalar,
                y = vec.y * scalar,
                z = vec.z * scalar,
                w = vec.w * scalar
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
        public static Vec4f operator /(Vec4f vec, float scalar)
        {
            float f = 1.0f / scalar;

            return new Vec4f()
            {
                x = vec.x * f,
                y = vec.y * f,
                z = vec.z * f,
                w = vec.w * f
            };
        }
        /// <summary>
        /// Adds a scalar to a vector.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4f operator +(float scalar, Vec4f vec)
        {
            return new Vec4f()
            {
                x = scalar + vec.x,
                y = scalar + vec.y,
                z = scalar + vec.z,
                w = scalar + vec.w
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
        public static Vec4f operator -(float scalar, Vec4f vec)
        {
            return new Vec4f()
            {
                x = scalar - vec.x,
                y = scalar - vec.y,
                z = scalar - vec.z,
                w = scalar - vec.w
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
        public static Vec4f operator *(float scalar, Vec4f vec)
        {
            return new Vec4f()
            {
                x = scalar * vec.x,
                y = scalar * vec.y,
                z = scalar * vec.z,
                w = scalar * vec.w
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
        public static Vec4f operator /(float scalar, Vec4f vec)
        {
            float f = 1.0f / scalar;

            return new Vec4f()
            {
                x = f * vec.x,
                y = f * vec.y,
                z = f * vec.z,
                w = f * vec.w
            };
        }

        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4f operator -(Vec4f vec)
        {
            return new Vec4f()
            {
                x = -vec.x,
                y = -vec.y,
                z = -vec.z,
                w = -vec.w
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
        public static bool operator ==(Vec4f left, Vec4f right)
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
        public static bool operator !=(Vec4f left, Vec4f right)
        {
            return
                left.x != right.x ||
                left.y != right.y ||
                left.z != right.z ||
                left.w != right.w;
        }


        /// <summary>
        /// Returns unsafe float* pointer to x.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public unsafe static explicit operator float*(Vec4f vec)
        {
            return &vec.x;
        }
        /// <summary>
        /// Returns unsafe IntPtr pointer to x.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public unsafe static explicit operator IntPtr(Vec4f vec)
        {
            return (IntPtr)(&vec.x);
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
            if (obj is Vec4f)
                return Equals((Vec4f)obj);
            else
                return false;
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vec4f other)
        {
            return
                x == other.x &&
                y == other.y &&
                z == other.z &&
                w == other.w;
        }

        #endregion
    }
}
