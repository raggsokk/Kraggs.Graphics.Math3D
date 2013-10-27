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
    public partial struct Vec3f : IEquatable<Vec3f>, IBinaryStreamMath3D<Vec3f>, IGLTypeMath3D, IGLMath, IGenericStream
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

        #region Constructors

        /// <summary>
        /// Creates a vector with all components set to value.
        /// </summary>
        /// <param name="value"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vec3f(float value)
        {
            this.x = value;
            this.y = value;
            this.z = value;
        }

        /// <summary>
        /// Constructs a vector from individualy components.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vec3f(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Contructs a Vec3f from a Vec2f and a z component.
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="z"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vec3f(Vec2f vec, float z = 0.0f)
        {
            this.x = vec.x;
            this.y = vec.y;
            this.z = z;
        }

        #endregion

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
        /// A Vec3f with all components set to 1.
        /// </summary>
        public static readonly Vec3f One = new Vec3f() { x = 1.0f, y = 1.0f, z = 1.0f };

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
                return MathF.Sqrt(x * x + y * y + z * z);
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
            //var f = 1.0f / MathFunctions.Sqrt(x * x + y * y + z * z);
            var f = 1.0f / MathF.Sqrt(x * x + y * y + z * z);

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
            var f = 1.0f / MathF.Sqrt(x * x + y * y + z * z);

            return new Vec3f() { x = x * f, y = y * f, z = z * f };
        }
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vec3f GetFastNormal()
        {
            //var f = MathFunctions.FastInverseSqrt(x * x + y * y + z * z);
            var f = FastMathF.InverseSqrt(x * x + y * y + z * z);
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
            var f = 1.0f / MathF.Sqrt(vec.x * vec.x + vec.y * vec.y + vec.z * vec.z);

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
        public static Vec3f Reflect(Vec3f I, Vec3f N)
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
        public static Vec3f Refract(Vec3f I, Vec3f N, float eta)
        {
            //TODO: Use operatores instead of function madness.

            float dotValue = Dot(N, I);
            float k = 1.0f - eta * eta * (1.0f - dotValue * dotValue);

            if (k < 0.0f)
                return Vec3f.Zero;
            else
                return eta * I - (eta * dotValue + MathF.Sqrt(k)) * N;
            //TODO: Use operatores instead of function madness.
                //return Multiply(Subtract(Multiply(I, eta), (eta * dotValue + MathFunctions.Sqrt(k))), N);
                            
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
                MathF.InverseSqrt(Dot(left, left)) *
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
                FastMathF.InverseSqrt(Dot(left, left)) *
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

            result = a * FastMathF.InverseSqrt(b) * c;
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
        public static Vec3f Clamp(Vec3f x, float min, float max)
        {
            return new Vec3f()
            {
                x = MathF.Clamp(x.x, min, max),
                y = MathF.Clamp(x.y, min, max),
                z = MathF.Clamp(x.z, min, max)
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
        public static Vec3f Clamp(Vec3f x, Vec3f min, Vec3f max)
        {
            return new Vec3f()
            {
                x = MathF.Clamp(x.x, min.x, max.x),
                y = MathF.Clamp(x.y, min.y, max.y),
                z = MathF.Clamp(x.z, min.z, max.z)
            };
        }

        /// <summary>
        /// Returns a mix/lerp of two vectors with mix factor a.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f Mix(Vec3f x, Vec3f y, float a)
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
        public static Vec3f Mix(Vec3f x, Vec3f y, Vec3f a)
        {
            return x + a * (y - x);
        }

        /// <summary>
        /// Returns a lerp/mix of two vectors with mix factor a.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f Lerp(Vec3f x, Vec3f y, float a)
        {
            return x + a * (y - x);
        }

        /// <summary>
        /// Normalized Lerp.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f Nlerp(Vec3f x, Vec3f y, float a)
        {
            return Vec3f.Normalize(x + a * (y - x));
        }

        /// <summary>
        /// Spherical Lerp.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f Slerp(Vec3f x, Vec3f y, float a)
        {
            // Dot product - the cosine of the angle between 2 vectors.
            var dot = Vec3f.Dot(x, y);

            // Clamp it to be in the range of Acos()
            dot = MathF.Clamp(dot, -1.0f, 1.0f);

            float theta = MathF.Acos(dot) * a;
            var relativeVec = Vec3f.Normalize(y - x * dot); // Orthonormal basis

            return ((x * MathF.Cos(theta)) + (relativeVec * MathF.Sin(theta)));
        }

        /// <summary>
        /// step generates a step function by comparing x to edge.
        /// For element i of the return value, 0.0 is returned if x[i] < edge[i], and 1.0 is returned otherwise.
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f Step(float edge, Vec3f x)
        {
            return new Vec3f()
            {
                x = x.x < edge ? 0.0f : 1.0f,
                y = x.y < edge ? 0.0f : 1.0f,
                z = x.z < edge ? 0.0f : 1.0f
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
        public static Vec3f Step(Vec3f edge, Vec3f x)
        {
            return new Vec3f()
            {
                x = x.x < edge.x ? 0.0f : 1.0f,
                y = x.y < edge.y ? 0.0f : 1.0f,
                z = x.z < edge.z ? 0.0f : 1.0f
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
        public static Vec3f SmoothStep(float edge0, float edge1, Vec3f x)
        {
            return new Vec3f()
            {
                x = MathF.SmoothStep(edge0, edge1, x.x),
                y = MathF.SmoothStep(edge0, edge1, x.y),
                z = MathF.SmoothStep(edge0, edge1, x.z)
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
        public static Vec3f SmoothStep(Vec3f edge0, Vec3f edge1, Vec3f x)
        {
            return new Vec3f()
            {
                x = MathF.SmoothStep(edge0.x, edge1.x, x.x),
                y = MathF.SmoothStep(edge0.y, edge1.y, x.y),
                z = MathF.SmoothStep(edge0.z, edge1.z, x.z)
            };
        }

        /// <summary>
        /// Returns the abolute value of a vector.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f Abs(Vec3f v)
        {
            return new Vec3f()
            {
                x = Math.Abs(v.x),
                y = Math.Abs(v.y),
                z = Math.Abs(v.z)
            };
        }

        /// <summary>
        /// Returns the smalles integral value that is greater than or equal to the specified number.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f Ceiling(Vec3f v)
        {
            return new Vec3f()
            {
                x = (float)Math.Ceiling(v.x),
                y = (float)Math.Ceiling(v.y),
                z = (float)Math.Ceiling(v.z)
            };
        }

        /// <summary>
        /// Returns the largest value less than or equal to the specified number
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f Floor(Vec3f v)
        {
            return new Vec3f()
            {
                x = (float)Math.Floor(v.x),
                y = (float)Math.Floor(v.y),
                z = (float)Math.Floor(v.z)
            };
        }

        /// <summary>
        /// Calculates the integral component parts of a specified vector.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f Truncate(Vec3f v)
        {
            return new Vec3f()
            {
                x = (float)Math.Truncate(v.x),
                y = (float)Math.Truncate(v.y),
                z = (float)Math.Truncate(v.z)
            };
        }
        /// <summary>
        /// Return x - floor(x).
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f Fract(Vec3f v)
        {
            return new Vec3f(
                MathF.Fract(v.x),
                MathF.Fract(v.y),
                MathF.Fract(v.z)
                );
        }

        /// <summary>
        /// Modulus. Returns x - y * floor(x / y)
        /// for each component in x using the floating point value y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f Mod(Vec3f x, float i)
        {
            return new Vec3f(
                MathF.Mod(x.x, i),
                MathF.Mod(x.y, i),
                MathF.Mod(x.z, i)
                );
        }

        /// <summary>
        /// Modulus. Returns x - y * floor(x / y)
        /// for each component in x using the floating point value y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f Mod(Vec3f x, Vec3f i)
        {
            return new Vec3f(
                MathF.Mod(x.x, i.x),
                MathF.Mod(x.y, i.y),
                MathF.Mod(x.z, i.z)                
                );
        }


        /// <summary>
        /// Returns the larger components of two vectors.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f Max(Vec3f left, Vec3f right)
        {
            return new Vec3f()
            {
                x = Math.Max(left.x, right.x),
                y = Math.Max(left.y, right.y),
                z = Math.Max(left.z, right.z)
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
        public static Vec3f Min(Vec3f left, Vec3f right)
        {
            return new Vec3f()
            {
                x = Math.Min(left.x, right.x),
                y = Math.Min(left.y, right.y),
                z = Math.Min(left.z, right.z)
            };
        }

        /// <summary>
        /// Returns the result of all components multiplied.
        /// aka x * y * z
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ComponentMultiply(Vec3f vec)
        {
            return 1.0f * vec.x * vec.y * vec.z;
        }

        /// <summary>
        /// Returns the sum of all the components.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ComponentAdd(Vec3f vec)
        {
            return vec.x + vec.y + vec.z;
        }

        /// <summary>
        /// Returns the largest component of a vector.
        /// aka which are largest ov x, y, z
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ComponentMax(Vec3f vec)
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
        public static float ComponentMin(Vec3f vec)
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

        #region Static Operator Functions

        /// <summary>
        /// Adds two vectors to each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f operator +(Vec3f left, Vec3f right)
        {
            return new Vec3f()
            {
                x = left.x + right.x,
                y = left.y + right.y,
                z = left.z + right.z
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
        public static Vec3f operator -(Vec3f left, Vec3f right)
        {
            return new Vec3f()
            {
                x = left.x - right.x,
                y = left.y - right.y,
                z = left.z - right.z
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
        public static Vec3f operator *(Vec3f left, Vec3f right)
        {
            return new Vec3f()
            {
                x = left.x * right.x,
                y = left.y * right.y,
                z = left.z * right.z
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
        public static Vec3f operator /(Vec3f left, Vec3f right)
        {
            return new Vec3f()
            {
                x = left.x / right.x,
                y = left.y / right.y,
                z = left.z / right.z
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
        public static Vec3f operator +(Vec3f vec, float scalar)
        {
            return new Vec3f()
            {
                x = vec.x + scalar,
                y = vec.y + scalar,
                z = vec.z + scalar
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
        public static Vec3f operator -(Vec3f vec, float scalar)
        {
            return new Vec3f()
            {
                x = vec.x - scalar,
                y = vec.y - scalar,
                z = vec.z - scalar
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
        public static Vec3f operator *(Vec3f vec, float scalar)
        {
            return new Vec3f()
            {
                x = vec.x * scalar,
                y = vec.y * scalar,
                z = vec.z * scalar
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
        public static Vec3f operator /(Vec3f vec, float scalar)
        {
            float f = 1.0f / scalar;

            return new Vec3f()
            {
                x = vec.x * f,
                y = vec.y * f,
                z = vec.z * f
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
        public static Vec3f operator +(float scalar, Vec3f vec)
        {
            return new Vec3f()
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
        public static Vec3f operator -(float scalar, Vec3f vec)
        {
            return new Vec3f()
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
        public static Vec3f operator *(float scalar, Vec3f vec)
        {
            return new Vec3f()
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
        public static Vec3f operator /(float scalar, Vec3f vec)
        {
            float f = 1.0f / scalar;

            return new Vec3f()
            {
                x = f * vec.x,
                y = f * vec.y,
                z = f * vec.z
            };
        }

        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f operator -(Vec3f vec)
        {
            return new Vec3f()
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
        public static bool operator ==(Vec3f left, Vec3f right)
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
        public static bool operator !=(Vec3f left, Vec3f right)
        {
            return
                left.x != right.x ||
                left.y != right.y ||
                left.z != right.z;
        }


        /// <summary>
        /// Returns unsafe float* pointer to x.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public unsafe static explicit operator float*(Vec3f vec)
        {
            return &vec.x;
        }
        /// <summary>
        /// Returns unsafe IntPtr pointer to x.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public unsafe static explicit operator IntPtr(Vec3f vec)
        {
            return (IntPtr)(&vec.x);
        }

        /// <summary>
        /// Creates a vec3f from a vec2f.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vec3f(Vec2f vec)
        {
            return new Vec3f(vec.x, vec.y, 0.0f);
        }
        
        /// <summary>
        /// Creates a vec3f from a vec4f, ignoring w component completely.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vec3f(Vec4f vec)
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
            if (obj is Vec3f)
                return Equals((Vec3f)obj);
            else
                return false;
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vec3f other)
        {
            return 
                x == other.x && 
                y == other.y &&
                z == other.z;
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
            get { return 3; }
        }

        public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Vec3f));

        /// <summary>
        /// Returns the inmemory size in bytes of this vector.
        /// </summary>
        [DebuggerNonUserCode()]
        int IGLMath.SizeInBytes
        {
            get { return Vec3f.SizeInBytes; }
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
            get { return GLConstants.FLOAT_VEC3; }
        }

        /// <summary>
        /// Returns the OpenGL uniform type enum
        /// </summary>
        [DebuggerNonUserCode()]
        int IGLMath.GLUniformType
        {
            get { return GLConstants.FLOAT_VEC3; }
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

        #region IGLTypeMath3D

        private static readonly IGLDescriptionMath3D GLTypeDescription = new Vec3fGLDescription();

        public IGLDescriptionMath3D GetGLTypeDescription
        {
            get
            {
                Debug.Assert(Marshal.SizeOf(typeof(Vec3fGLDescription)) == 0);

                return GetGLTypeDescription;
            }
        }

        private struct Vec3fGLDescription : IGLDescriptionMath3D
        {
            Type IGLDescriptionMath3D.BaseType
            {
                get { return typeof(float); }
            }

            int IGLDescriptionMath3D.ComponentCount
            {
                get { return 3; }
            }

            int IGLDescriptionMath3D.SizeInBytes
            {
                get { return 12; }
                //get { return ComponentCount * sizeof(BaseType); }
            }

            int IGLDescriptionMath3D.GLBaseType
            {
                get { return GLConstants.GL_BASE_FLOAT; }
            }

            int IGLDescriptionMath3D.GLAttributeType
            {
                get { return GLConstants.FLOAT_VEC3; }
            }

            int IGLDescriptionMath3D.GLUniformType
            {
                get { return GLConstants.FLOAT_VEC3; }
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
                get { return 3; }
            }

            int IGLDescriptionMath3D.Rows
            {
                get { return 1; }
            }
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
        [Obsolete("Use functions in IBinaryStreamMath3D instead")]
        void IGenericStream.WriteStream(System.IO.BinaryWriter writer, object vec)
        {
            Vec3f v = (Vec3f)vec;
            writer.Write(v.x);
            writer.Write(v.y);
            writer.Write(v.z);
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
            return new Vec3f()
            {
                x = reader.ReadSingle(),
                y = reader.ReadSingle(),
                z = reader.ReadSingle()
            };
        }

        #endregion

        #region IBinaryStreamMath3D Implementation

        /// <summary>
        /// Writes out an array of vec3f's to a binary writer.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="elements"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        public void WriteStream(System.IO.BinaryWriter writer, Vec3f[] elements, int index, int length)
        {
            if (elements == null || elements.Length == 0)
                return;

            var len = Math.Min(elements.Length, index + length);

            for (int i = index; i < len; i++)
            {
                writer.Write(elements[i].x);
                writer.Write(elements[i].y);
                writer.Write(elements[i].z);
            }
        }

        /// <summary>
        /// Reads in an array of Vec3f's from a binary reader.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="elements"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public int ReadStream(System.IO.BinaryReader reader, Vec3f[] elements, int index, int length)
        {
            int count = 0;
            var len = Math.Min(elements.Length, index + length);

            for (int i = index; i < len; i++)
            {
                elements[i].x = reader.ReadSingle();
                elements[i].y = reader.ReadSingle();
                elements[i].z = reader.ReadSingle();
                count++;
            }

            return count;
        }

        /// <summary>
        /// Write a single Vec3f to a binary writer.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="element"></param>
        [DebuggerNonUserCode()]
        public void WriteStream(System.IO.BinaryWriter writer, Vec3f element)
        {
            writer.Write(element.x);
            writer.Write(element.y);
            writer.Write(element.z);
        }
        /// <summary>
        /// Reads a single Vec3f from a binary reader.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public Vec3f ReadStream(System.IO.BinaryReader reader)
        {
            return new Vec3f(
                reader.ReadSingle(),
                reader.ReadSingle(),
                reader.ReadSingle());
        }

        #endregion
    }
}
