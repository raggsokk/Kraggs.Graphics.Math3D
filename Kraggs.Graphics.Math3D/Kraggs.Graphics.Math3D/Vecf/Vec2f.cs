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
    public partial struct Vec2f : IEquatable<Vec2f>, IGLMath, IGenericStream
    {
        /// <summary>
        /// The x component.
        /// </summary>
        public float x;
        /// <summary>
        /// The y component.
        /// </summary>
        public float y;

        #region Constructors

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vec2f(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        //public static Vec2f[] FromBytes(byte[] buffer, int offset, int count)
        //{
        //    throw new NotImplementedException();
        //    int len = 0;
        //    int vcount = 0;

        //    var vecs = new Vec2f[vcount];

        //    for (int i = offset; i < len; i += Vec2f.SizeInBytes)
        //    {
        //        //vecs[count].x = BitConverter.ToSingle(
        //    }
        //}        

        #endregion

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
                return MathF.Sqrt(x * x + y * y);
                //return MathFunctions.Sqrt(x * x + y * y);
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
            //var f = 1.0f / MathFunctions.Sqrt(x * x + y * y);
            var f = 1.0f / MathF.Sqrt(x * x + y * y);

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
            //var f = 1.0f / MathFunctions.Sqrt(x * x + y * y);
            var f = 1.0f / MathF.Sqrt(x * x + y * y);
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
            var f = 1.0f / MathF.Sqrt(vec.x * vec.x + vec.y * vec.y);

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

        /// <summary>
        /// If Dot(Nref, I) < 0.0, return N otherwise return -N
        /// </summary>
        /// <param name="N"></param>
        /// <param name="I"></param>
        /// <param name="Nref"></param>
        /// <returns></returns>
        public static Vec2f FaceForward(Vec2f N, Vec2f I, Vec2f Nref)
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
        public static Vec2f Reflect(Vec2f I, Vec2f N)
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
        public static Vec2f Refract(Vec2f I, Vec2f N, float eta)
        {
            //TODO: Use operatores instead of function madness.

            float dotValue = Dot(N, I);
            float k = 1.0f - eta * eta * (1.0f - dotValue * dotValue);

            if (k < 0.0f)
                return Vec2f.Zero;
            else
                return eta * I - (eta * dotValue + MathF.Sqrt(k)) * N;
                //return eta * I - (eta * dotValue + MathFunctions.Sqrt(k)) * N;
            //TODO: Use operatores instead of function madness.
            //return Multiply(Subtract(Multiply(I, eta), (eta * dotValue + MathFunctions.Sqrt(k))), N);

            //return Multiply(Subtract(Multiply(I, eta),sdf), N);
            //return eta * I - (eta * dotValue + MathFunctions.Sqrt(k)) * N;

            // return eta * I - (eta * dotValue + MathFunctions.Sqrt(k)) * N;
        }

        //[DebuggerNonUserCode()]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Vec2f Cross(Vec2f left, Vec2f right)
        //{
        //    //return new Vec2f
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Clamps the components of a vector to between min and max.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2f Clamp(Vec2f x, float min, float max)
        {
            return new Vec2f()
            {
                x = MathF.Clamp(x.x, min, max),
                y = MathF.Clamp(x.y, min, max)
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
        public static Vec2f Clamp(Vec2f x, Vec2f min, Vec2f max)
        {
            return new Vec2f()
            {
                x = MathF.Clamp(x.x, min.x, max.x),
                y = MathF.Clamp(x.y, min.y, max.y)
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
        public static Vec2f Mix(Vec2f x, Vec2f y, float a)
        {
            return x + a * (y - x);
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
        public static Vec2f Mix(Vec2f x, Vec2f y, Vec2f a)
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
        public static Vec2f Lerp(Vec2f x, Vec2f y, float a)
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
        public static Vec2f Nlerp(Vec2f x, Vec2f y, float a)
        {
            return Vec2f.Normalize(x + a * (y - x));
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
        public static Vec2f Slerp(Vec2f x, Vec2f y, float a)
        {
            // Dot product - the cosine of the angle between 2 vectors.
            var dot = Vec2f.Dot(x, y);

            // Clamp it to be in the range of Acos()
            dot = MathF.Clamp(dot, -1.0f, 1.0f);

            float theta = MathF.Acos(dot) * a;
            var relativeVec = Vec2f.Normalize(y - x * dot); // Orthonormal basis

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
        public static Vec2f Step(float edge, Vec2f x)
        {
            return new Vec2f()
            {
                x = x.x < edge ? 0.0f : 1.0f,
                y = x.y < edge ? 0.0f : 1.0f
            };
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
        public static Vec2f Step(Vec2f edge, Vec2f x)
        {
            return new Vec2f()
            {
                x = x.x < edge.x ? 0.0f : 1.0f,
                y = x.y < edge.y ? 0.0f : 1.0f
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
        public static Vec2f SmoothStep(float edge0, float edge1, Vec2f x)
        {
            return new Vec2f()
            {
                x = MathF.SmoothStep(edge0, edge1, x.x),
                y = MathF.SmoothStep(edge0, edge1, x.y)
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
        public static Vec2f SmoothStep(Vec2f edge0, Vec2f edge1, Vec2f x)
        {
            return new Vec2f()
            {
                x = MathF.SmoothStep(edge0.x, edge1.x, x.x),
                y = MathF.SmoothStep(edge0.y, edge1.y, x.y)
            };
        }

        /// <summary>
        /// Returns the abolute value of a vector.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2f Abs(Vec2f v)
        {            
            return new Vec2f()
            {
                x = Math.Abs(v.x),
                y = Math.Abs(v.y)
            };
        }

        /// <summary>
        /// Returns the smalles integral value that is greater than or equal to the specified number.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2f Ceiling(Vec2f v)
        {            
            return new Vec2f()
            {
                x = (float)Math.Ceiling(v.x),
                y = (float)Math.Ceiling(v.y)
            };
        }

        /// <summary>
        /// Returns the largest value less than or equal to the specified number
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2f Floor(Vec2f v)
        {
            return new Vec2f()
            {
                x = (float)Math.Floor(v.x),
                y = (float)Math.Floor(v.y)
            };
        }

        /// <summary>
        /// Calculates the integral component parts of a specified vector.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2f Truncate(Vec2f v)
        {
            return new Vec2f()
            {
                x = (float)Math.Truncate(v.x),
                y = (float)Math.Truncate(v.y)
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
        public static Vec2f Max(Vec2f left, Vec2f right)
        {            
            return new Vec2f()
            {
                x = Math.Max(left.x, right.x),
                y = Math.Max(left.y, right.y)
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
        public static Vec2f Min(Vec2f left, Vec2f right)
        {
            return new Vec2f()
            {
                x = Math.Min(left.x, right.x),
                y = Math.Min(left.y, right.y)
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

        #region Static Operator Functions

        /// <summary>
        /// Adds two vectors to each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2f operator +(Vec2f left, Vec2f right)
        {
            return new Vec2f() {
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
        public static Vec2f operator -(Vec2f left, Vec2f right)
        {
            return new Vec2f()
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
        public static Vec2f operator *(Vec2f left, Vec2f right)
        {
            return new Vec2f()
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
        public static Vec2f operator /(Vec2f left, Vec2f right)
        {
            return new Vec2f()
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
        public static Vec2f operator +(Vec2f vec, float scalar)
        {
            return new Vec2f()
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
        public static Vec2f operator -(Vec2f vec, float scalar)
        {
            return new Vec2f()
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
        public static Vec2f operator *(Vec2f vec, float scalar)
        {
            return new Vec2f()
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
        public static Vec2f operator /(Vec2f vec, float scalar)
        {
            float f = 1.0f / scalar;

            return new Vec2f()
            {
                x = vec.x * f,
                y = vec.y * f
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
        public static Vec2f operator +(float scalar, Vec2f vec)
        {
            return new Vec2f()
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
        public static Vec2f operator -(float scalar, Vec2f vec)
        {
            return new Vec2f()
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
        public static Vec2f operator *(float scalar, Vec2f vec)
        {
            return new Vec2f()
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
        public static Vec2f operator /(float scalar, Vec2f vec)
        {
            float f = 1.0f / scalar;

            return new Vec2f()
            {
                x = f * vec.x,
                y = f * vec.y
            };
        }

        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2f operator -(Vec2f vec)
        {
            return new Vec2f()
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
        public static bool operator ==(Vec2f left, Vec2f right)
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
        public static bool operator !=(Vec2f left, Vec2f right)
        {
            return
                left.x != right.x ||
                left.y != right.y;
        }

        /// <summary>
        /// Returns unsafe float* pointer to x.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public unsafe static explicit operator float*(Vec2f vec)
        {
            return &vec.x;
        }
        /// <summary>
        /// Returns unsafe IntPtr pointer to x.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public unsafe static explicit operator IntPtr(Vec2f vec)
        {
            return (IntPtr)(&vec.x);
        }

        /// <summary>
        /// Cast a vec3f to a vec2f, loosing z component.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vec2f(Vec3f vec)
        {
            return new Vec2f(vec.x, vec.y);
        }

        /// <summary>
        /// Casting a vec4f to a vec3f, loosing z and w components.
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vec2f(Vec4f vec)
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
            if (obj is Vec2f)
                return Equals((Vec2f)obj);
            else
                return false;
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vec2f other)
        {
            return x == other.x && y == other.y;
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
            get { return 2; }
        }

        public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Vec2f));

        /// <summary>
        /// Returns the inmemory size in bytes of this vector.
        /// </summary>
        [DebuggerNonUserCode()]
        int IGLMath.SizeInBytes
        {
            get { return Vec2f.SizeInBytes; }
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
            get { return GLConstants.FLOAT_VEC2; }
        }

        /// <summary>
        /// Returns the OpenGL uniform type enum
        /// </summary>
        [DebuggerNonUserCode()]
        int IGLMath.GLUniformType
        {
            get { return GLConstants.FLOAT_VEC2; }
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
            Vec2f v = (Vec2f)vec;
            writer.Write(v.x);
            writer.Write(v.y);
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
            return new Vec2f()
            {
                x = reader.ReadSingle(),
                y = reader.ReadSingle()
            };
        }

        #endregion
    }
}
