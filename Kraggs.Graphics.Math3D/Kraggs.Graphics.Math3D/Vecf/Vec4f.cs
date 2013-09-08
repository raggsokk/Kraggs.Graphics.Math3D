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
    public partial struct Vec4f : IEquatable<Vec4f>, IGLMath, IGenericStream
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

        #region Constructors

        /// <summary>
        /// Constructs a vector from individualy components.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vec4f(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// Constructs a Vec4f from a Vec3f with optionally w component.
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="w"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vec4f(Vec3f vec, float w = 0.0f)
        {
            this.x = vec.x;
            this.y = vec.y;
            this.z = vec.z;
            this.w = w;
        }

        #endregion


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

        /// <summary>
        /// Clamps the components of a vector to between min and max.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4f Clamp(Vec4f x, float min, float max)
        {
            return new Vec4f()
            {
                x = MathFunctions.Clamp(x.x, min, max),
                y = MathFunctions.Clamp(x.y, min, max),
                z = MathFunctions.Clamp(x.z, min, max),
                w = MathFunctions.Clamp(x.w, min, max)
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
        public static Vec4f Clamp(Vec4f x, Vec4f min, Vec4f max)
        {
            return new Vec4f()
            {
                x = MathFunctions.Clamp(x.x, min.x, max.x),
                y = MathFunctions.Clamp(x.y, min.y, max.y),
                z = MathFunctions.Clamp(x.z, min.z, max.z),
                w = MathFunctions.Clamp(x.w, min.w, max.w)
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
        public static Vec4f Mix(Vec4f x, Vec4f y, float a)
        {
            return x + a * (y - x);
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
        public static Vec4f Mix(Vec4f x, Vec4f y, Vec4f a)
        {
            return x + a * (y - x);
        }

        /// <summary>
        /// step generates a step function by comparing x to edge.
        /// For element i of the return value, 0.0 is returned if x[i] gt edge[i], and 1.0 is returned otherwise.
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4f Step(float edge, Vec4f x)
        {
            return new Vec4f()
            {
                x = x.x < edge ? 0.0f : 1.0f,
                y = x.y < edge ? 0.0f : 1.0f,
                z = x.z < edge ? 0.0f : 1.0f,
                w = x.w < edge ? 0.0f : 1.0f
            };
        }

        /// <summary>
        /// step generates a step function by comparing x to edge.
        /// For element i of the return value, 0.0 is returned if x[i] gt edge[i], and 1.0 is returned otherwise.
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4f Step(Vec4f edge, Vec4f x)
        {
            return new Vec4f()
            {
                x = x.x < edge.x ? 0.0f : 1.0f,
                y = x.y < edge.y ? 0.0f : 1.0f,
                z = x.z < edge.z ? 0.0f : 1.0f,
                w = x.w < edge.w ? 0.0f : 1.0f
            };
        }

        /// <summary>
        /// perform Hermite interpolation between two values
        /// </summary>
        /// <param name="edge0"></param>
        /// <param name="edge1"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4f SmoothStep(float edge0, float edge1, Vec4f x)
        {
            return new Vec4f()
            {
                x = MathFunctions.SmoothStep(edge0, edge1, x.x),
                y = MathFunctions.SmoothStep(edge0, edge1, x.y),
                z = MathFunctions.SmoothStep(edge0, edge1, x.z),
                w = MathFunctions.SmoothStep(edge0, edge1, x.w)
            };
        }

        /// <summary>
        /// perform Hermite interpolation between two values
        /// </summary>
        /// <param name="edge0"></param>
        /// <param name="edge1"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4f SmoothStep(Vec4f edge0, Vec4f edge1, Vec4f x)
        {
            return new Vec4f()
            {
                x = MathFunctions.SmoothStep(edge0.x, edge1.x, x.x),
                y = MathFunctions.SmoothStep(edge0.y, edge1.y, x.y),
                z = MathFunctions.SmoothStep(edge0.z, edge1.z, x.z),
                w = MathFunctions.SmoothStep(edge0.w, edge1.w, x.w)
            };
            
        }

        /// <summary>
        /// Returns the abolute value of a vector.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4f Abs(Vec4f v)
        {
            return new Vec4f()
            {
                x = Math.Abs(v.x),
                y = Math.Abs(v.y),
                z = Math.Abs(v.z),
                w = Math.Abs(v.w)
            };
        }

        /// <summary>
        /// Returns the smalles integral value that is greater than or equal to the specified number.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4f Ceiling(Vec4f v)
        {
            return new Vec4f()
            {
                x = (float)Math.Ceiling(v.x),
                y = (float)Math.Ceiling(v.y),
                z = (float)Math.Ceiling(v.z),
                w = (float)Math.Ceiling(v.w)
            };
        }

        /// <summary>
        /// Returns the largest value less than or equal to the specified number
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4f Floor(Vec4f v)
        {
            return new Vec4f()
            {
                x = (float)Math.Floor(v.x),
                y = (float)Math.Floor(v.y),
                z = (float)Math.Floor(v.z),
                w = (float)Math.Floor(v.w)
            };
        }

        /// <summary>
        /// Calculates the integral component parts of a specified vector.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4f Truncate(Vec4f v)
        {
            return new Vec4f()
            {
                x = (float)Math.Truncate(v.x),
                y = (float)Math.Truncate(v.y),
                z = (float)Math.Truncate(v.z),
                w = (float)Math.Truncate(v.w)
            };
        }

        /// <summary>
        /// Returns the larger components of two vectors.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4f Max(Vec4f left, Vec4f right)
        {
            return new Vec4f()
            {
                x = Math.Max(left.x, right.x),
                y = Math.Max(left.y, right.y),
                z = Math.Max(left.z, right.z),
                w = Math.Max(left.w, right.w)
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
        public static Vec4f Min(Vec4f left, Vec4f right)
        {
            return new Vec4f()
            {
                x = Math.Min(left.x, right.x),
                y = Math.Min(left.y, right.y),
                z = Math.Min(left.z, right.z),
                w = Math.Min(left.w, right.w)
            };
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

        /// <summary>
        /// Computes the hash code of this vector.
        /// </summary>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode() ^ w.GetHashCode();
        }

        /// <summary>
        /// Returns a string representation of this vector.
        /// </summary>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return string.Format("[{0}, {1}, {2}, {3}]", x, y, z, w);
        }

        /// <summary>
        /// Compares this vector to another object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            if (obj is Vec4f)
                return Equals((Vec4f)obj);
            else
                return false;
        }

        /// <summary>
        /// Compares two vectors with each other.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
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

        #region IGLMath

        /// <summary>
        /// Returns the dotnet type of this components. 
        /// </summary>
        [DebuggerNonUserCode()]
        Type IGLMath.BaseType
        {
            get { return typeof(float); }
        }

        /// <summary>
        /// The number of components totaly in this vector.
        /// </summary>
        [DebuggerNonUserCode()]
        int IGLMath.ComponentCount
        {
            get { return 4; }
        }

        public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Vec4f));

        /// <summary>
        /// Returns the inmemory size in bytes of this vector. 
        /// </summary>
        [DebuggerNonUserCode()]
        int IGLMath.SizeInBytes
        {
            get { return Vec4f.SizeInBytes; }
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
            get { return GLConstants.FLOAT_VEC4; }
        }

        /// <summary>
        /// Returns the OpenGL uniform type enum
        /// </summary>
        [DebuggerNonUserCode()]
        int IGLMath.GLUniformType
        {
            get { return GLConstants.FLOAT_VEC4; }
        }

        /// <summary>
        /// Is this a matrix (false)
        /// </summary>
        [DebuggerNonUserCode()]
        bool IGLMath.IsMatrix
        {
            get { return false; }
        }

        #endregion

        #region IGenericStream Implementation

        /// <summary>
        /// Writes vec to stream.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="vec"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteStream(System.IO.BinaryWriter writer, object vec)
        {
            Vec4f v = (Vec4f)vec;
            writer.Write(v.x);
            writer.Write(v.y);
            writer.Write(v.z);
            writer.Write(v.w);
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
            return new Vec4f()
            {
                x = reader.ReadSingle(),
                y = reader.ReadSingle(),
                z = reader.ReadSingle(),
                w = reader.ReadSingle()
            };
        }

        #endregion

    }
}
