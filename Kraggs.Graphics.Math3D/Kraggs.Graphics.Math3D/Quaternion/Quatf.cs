using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Kraggs.Graphics.Math3D
{
    [DebuggerDisplay("[ {w}, {x}, {y}, {z} ]")]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct Quatf : IEquatable<Quatf>, IGLMath, IGenericStream
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

        ///// <summary>
        ///// Default constructor for Quaternion.
        ///// </summary>
        //[DebuggerNonUserCode()]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public Quatf()
        //{
        //    this.x = 0.0f;
        //    this.y = 0.0f;
        //    this.z = 0.0f;
        //    this.w = 1.0f;
        //}

        /// <summary>
        /// Constructs a quaternion.
        /// </summary>
        /// <param name="w"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Quatf(float w, float x, float y, float z)
        {
            this.w = w;
            this.x = x;
            this.y = y;
            this.z = z;            
        }

        /// <summary>
        /// Creates a quaternion from scalar w and axix vector v.
        /// </summary>
        /// <param name="w"></param>
        /// <param name="v"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Quatf(float w, Vec3f v)
        {
            this.w = w;
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
        }

        /// <summary>
        /// Builds a quaternion from euler angles (pitch, yaw, roll), in radians.
        /// </summary>
        /// <param name="eulerAnglesInRadian"></param>
        /// <returns></returns>
        public Quatf FromEulerAngles(Vec3f eulerAnglesInRadian)
        {
            var c = MathFunctions.Cos(eulerAnglesInRadian * 0.5f);
            var s = MathFunctions.Sin(eulerAnglesInRadian * 0.5f);

            return new Quatf()
            {
                w = c.x * c.y * c.z + s.x * s.y * s.z,
                x = s.x * c.y * c.z - c.x * s.y * s.z,
                y = c.x * s.y * c.z + s.x * c.y * s.z,
                z = c.x * c.y * s.z - s.x * s.y * c.z
            };

            //var tc = (float)Math.Cos(eulerAnglesInRadian * 0.5f);
            //var ts = (float)Math.Cos(
        }

        /// <summary>
        /// FromMatrix vs CreateQuaternion??
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Quatf FromMatrix(Mat3f m)
        {
            return CreateQuaternion(m);
        }

        /// <summary>
        /// FromMatrix vs CreateQuaternion??
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Quatf FromMatrix(Mat4f m)
        {
            return CreateQuaternion(m);
        }

        #endregion

        #region Properties

        public static readonly Quatf Zero = new Quatf() { x = 0.0f, y = 0.0f, z = 0.0f, w = 0.0f };
        public static readonly Quatf UnitW = new Quatf() { x = 0.0f, y = 0.0f, z = 0.0f, w = 1.0f };

        ///// <summary>
        ///// Returns the unsqrt length of the vector.
        ///// </summary>
        //[DebuggerNonUserCode()]
        //public float LengthSquared
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //        return x * x + y * y + z * z + w * w;
        //    }
        //}
        /// <summary>
        /// Returns the correct Sqrt length of the vector.
        /// </summary>
        [DebuggerNonUserCode()]
        public float Length
        {
            get
            {
                return (float)Math.Sqrt(Dot(this, this));
                //throw new NotImplementedException();
                //return MathFunctions.Sqrt(x * x + y * y + z * z + w * w);
            }
        }

        /// <summary>
        /// Returns the angle stored in this quaternion.
        /// </summary>
        [DebuggerNonUserCode()]
        public float Angle
        {
            get
            {
                return Quatf.GetAngle(this);
            }
        }

        /// <summary>
        /// Returns the axis this quaternion rotates around.
        /// </summary>
        [DebuggerNonUserCode()]
        public Vec3f Axis
        {
            get
            {
                return Quatf.GetAxis(this);
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
            this = Quatf.Normalize(this);
            //throw new NotImplementedException();
            //var f = 1.0f / MathFunctions.Sqrt(x * x + y * y + z * z + w * w);

            //this.x = x * f;
            //this.y = y * f;
            //this.z = z * f;
            //this.w = w * f;
        }

        /// <summary>
        /// Returns a normalized version of this vector.
        /// </summary>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Quatf GetNormal()
        {
            return Quatf.Normalize(this);
            //var f = 1.0f / MathFunctions.Sqrt(x * x + y * y + z * z + w * w);

            //return new Vec4f() { x = x * f, y = y * f, z = z * f, w = w * f };
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
        public static Quatf Normalize(Quatf q)
        {
            float len = q.Length;

            if (len <= 0.0f) // PROBLEM!
                return Quatf.UnitW;

            float oneOverLen = 1.0f / len;
            return new Quatf()
            {
                w = q.w * oneOverLen,
                x = q.x * oneOverLen,
                y = q.y * oneOverLen,
                z = q.z * oneOverLen
            };            
        }

        /// <summary>
        /// Returns dot product of left and right, i.e.,
        /// left.x * right.x + left.y * right.y + ....
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(Quatf left, Quatf right)
        {
            return left.x * right.x + left.y * right.y + left.z * right.z + left.w * right.w;
        }

        /// <summary>
        /// Returns the cross product of two quaternions.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quatf Cross(Quatf left, Quatf right)
        {
            return new Quatf()
            {
                w = left.w * right.w - left.x * right.x - left.y * right.y - left.z * right.z,
                x = left.w * right.x + left.x * right.w + left.y * right.z - left.z * right.y,
                y = left.w * right.y + left.y * right.w + left.z * right.x - left.x * right.z,
                z = left.w * right.z + left.z * right.w + left.x * right.y - left.y * right.x,
            };
        }

        /// <summary>
        /// Calculates cross product of a vector and a quaternion.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f Cross(Vec3f v, Quatf q)
        {
            return Inverse(q) * v;
        }

        /// <summary>
        /// Calculates cross product of quaterion and a vector.
        /// </summary>
        /// <param name="q"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f Cross(Quatf q, Vec3f v)
        {            
            return q * v;
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quatf Squad(Quatf q1, Quatf q2, Quatf s1, Quatf s2, float h)
        {
            return Mix(Mix(q1, q2, h), Mix(s1, s2, h), 2.0f * h * (1.0f - h));
        }

        /// <summary>
        /// Rotates a vector around quaternion.
        /// </summary>
        /// <param name="q"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f Rotate(Quatf q, Vec3f v)
        {
            return q * v;
        }

        /// <summary>
        /// Rotates a vector around quaternion.
        /// </summary>
        /// <param name="q"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4f Rotate(Quatf q, Vec4f v)
        {
            return q * v;
        }


        /// <summary>
        /// Returns the angle component of this quaternion.
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetAngle(Quatf q)
        {
            return (float)MathFunctions.Degrees((float)Math.Acos(q.w) * 2.0f);
        }

        /// <summary>
        /// Returns the axis component of this quaternion.
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f GetAxis(Quatf q)
        {
            float tmp1 = 1.0f - q.w * q.w;
            if (tmp1 <= 0.0f)
                return Vec3f.UnitZ;
            float tmp2 = 1.0f / (float)Math.Sqrt(tmp1);
            return new Vec3f()
            {
                x = q.x * tmp2,
                y = q.y * tmp2,
                z = q.z * tmp2
            };                        
        }

        /// <summary>
        /// Creates a quaternion from angle and axis.
        /// </summary>
        /// <param name="angle">angle in degrees</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quatf AngleAxis(float angle, float x, float y, float z)
        {
            return AngleAxis(angle, new Vec3f() { x = x, y = y, z = z });
        }
        /// <summary>
        /// Creates a quaternion from angle and axis.
        /// </summary>
        /// <param name="angle">angle in degrees.</param>
        /// <param name="axis"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quatf AngleAxis(float angle, Vec3f axis)
        {
            Quatf result = UnitW;

            float a = MathFunctions.Radians(angle);
            float s = (float)Math.Sin(a * 0.5f);

            result.w = (float)Math.Cos(a * 0.5f);
            result.x = axis.x * s;
            result.y = axis.y * s;
            result.z = axis.z * s;

            return result;

        }

        /// <summary>
        /// Gets the roll component of this quaternion.
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Roll(Quatf q)
        {
            return MathFunctions.Degrees((float)Math.Atan2(2.0f * (q.x * q.y + q.w * q.z),
                q.w * q.w + q.x * q.x - q.y * q.y - q.z * q.z));
        }

        /// <summary>
        /// Gets the Pitch component of this quaternion.
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Pitch(Quatf q)
        {
            return MathFunctions.Degrees((float)Math.Atan2(2.0f * (q.y * q.z + q.w * q.x),
                q.w * q.w - q.x * q.x - q.y * q.y + q.z * q.z));
        }
        /// <summary>
        /// Gets the Yaw component of this quaternion.
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Yaw(Quatf q)
        {
            return MathFunctions.Degrees((float)Math.Asin(-2.0f * (q.x * q.z - q.w * q.y)));
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quatf ShortMix(Quatf left, Quatf right, float a)
        {
            if (a <= 0.0f) return left;
            if (a >= 1.0f) return right;

            float fCos = Dot(right, left);
            Quatf right2 = right;

            if (fCos < 0.0f)
            {
                right2 = -right;
                fCos = -fCos;
            }

            float k0, k1;
            if (fCos > 0.9999f)
            {
                k0 = 1.0f - a;
                k1 = 0.0f + a;
            }
            else
            {
                float fSin = (float)Math.Sqrt(1.0f - fCos * fCos);
                Debug.WriteLine("atan = atan2????");
                float fAngle = (float)Math.Atan2(fSin, fCos);
                float fOneOverSin = 1.0f / fSin;
                k0 = (float)Math.Sin((1.0f - a) * fAngle) * fOneOverSin;
                k1 = (float)Math.Sin((0.0f + a) * fAngle) * fOneOverSin;
            }

            return new Quatf()
            {
                w = k0 * left.w + k1 * right2.w,
                x = k0 * left.x + k1 * right2.x,
                y = k0 * left.y + k1 * right2.y,
                z = k0 * left.z + k1 * right2.z
            };
        }

        /// <summary>
        /// FastMix of two quaternions.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quatf FastMix(Quatf left, Quatf right, float a)
        {
            return Quatf.Normalize(left * (1.0f - a) + (right * a));
        }


        /// <summary>
        /// Returns a SLERP interpolated quaternion of left and right according a.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quatf Mix(Quatf left, Quatf right, float a)
        {
            float angle = (float)Math.Acos(Dot(left, right));
            return ((float)Math.Sin((1.0f - a) * angle) * left + (float)Math.Sin(a * angle) * right) / (float)Math.Sin(angle);
        }
        
        //public static Quatf Mix(Quatf left, Quatf right, float a )
        //{
        //    if (a <= 0.0f)
        //        return left;
        //    if (a >= 1.0f)
        //        return right;

        //    float fCos = Dot(left, right);
        //    Quatf right2 = right;

        //    if (fCos < 0.0f)
        //    {
        //        right2 = -right;
        //        fCos = -fCos;
        //    }

        //    float k0, k1;
        //    if (fCos > 0.9999f)
        //    {
        //        k0 = 1.0f - a;
        //        k1 = 0.0f + a;
        //    }
        //    else
        //    {
        //        float fSin = (float)Math.Sqrt(1.0f - fCos * fCos);
        //        float fAngle = (float)Math.Atan2(fSin, fCos);
        //        float fOneOverSin = 1.0f / fSin;
        //        k0 = (float)Math.Sin((1.0f - a) * fAngle) * fOneOverSin;
        //        k1 = (float)Math.Sin((0.0f + a) * fAngle) * fOneOverSin;
        //    }

        //    return new Quatf()
        //    {
        //        w = k0 * left.x + k1 * right2.w,
        //        x = k0 * left.x + k1 * right2.x,
        //        y = k0 * left.y + k1 * right2.y,
        //        z = k0 * left.z + k1 * right2.z
        //    };
        //}

        /// <summary>
        /// Returns the q conjugate
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quatf Conjugate(Quatf q)
        {
            return new Quatf()
            {
                w = q.w,
                x = -q.x,
                y = -q.y,
                z = -q.z
            };
        }

        /// <summary>
        /// Returns the q inverse.
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quatf Inverse(Quatf q)
        {
            return Conjugate(q) / Dot(q, q);
        }

        /// <summary>
        /// Rotates a quaternion from a vector of 3 components axis and an angle expressed in degrees.
        /// </summary>
        /// <param name="q"></param>
        /// <param name="angleInDegrees"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quatf Rotate(Quatf q, float angleInDegrees, Vec3f axis)
        {
            var tmp = axis;

            // Axis of rotation must be normalized.
            float len = tmp.Length;
            if((float)Math.Abs(len - 1.0f) > 0.001f)
            {
                float oneOverLen = 1.0f / len;
                tmp.x *= oneOverLen;
                tmp.y *= oneOverLen;
                tmp.z *= oneOverLen;
            }

            float AngleRad = MathFunctions.Radians(angleInDegrees);
            float fSin = (float)Math.Sin(AngleRad * 0.5f);

            return q * new Quatf()
            {
                w = (float)Math.Cos(AngleRad * 0.5f),
                x = tmp.x * fSin,
                y = tmp.y * fSin,
                z = tmp.z * fSin
            };
        }

        /// <summary>
        /// Returns euler angles, yitch as x, yaw as y, roll as z.
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f EulerAngles(Quatf q)
        {
            return new Vec3f()
            {
                x = Pitch(q),
                y = Yaw(q),
                z = Roll(q)
            };
        }

        /// <summary>
        /// Converts a quaternion to a 3 x 3 matrix.
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat3f CreateMat3f(Quatf q)
        {
            Mat3f result = Mat3f.Identity;

            result.c0.x = 1 - 2 * q.y * q.y - 2 * q.z * q.z;
            result.c0.y = 2 * q.x * q.y + 2 * q.w * q.z;
            result.c0.z = 2 * q.x * q.z - 2 * q.w * q.y;

            result.c1.x = 2 * q.x * q.y - 2 * q.w * q.z;
            result.c1.y = 1 - 2 * q.x * q.x - 2 * q.z * q.z;
            result.c1.z = 2 * q.y * q.z + 2 * q.w * q.x;

            result.c2.x = 2 * q.x * q.z + 2 * q.w * q.y;
            result.c2.y = 2 * q.y * q.z - 2 * q.w * q.x;
            result.c2.z = 1 - 2 * q.x * q.x - 2 * q.y * q.y;

            return result;
        }

        /// <summary>
        /// Converts a quaternion to a 4 x 4 matrix.
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Mat4f CreateMat4f(Quatf q)
        {
            return (Mat4f)CreateMat3f(q);
        }

        /// <summary>
        /// Converts a 3 * 3 matrix to a quaternion.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quatf CreateQuaternion(Mat3f m)
        {
            float fourXSquareMinus1 = m.c0.x - m.c1.y - m.c2.z;
            float fourYSquareMinus1 = m.c1.y - m.c0.x - m.c2.z;
            float fourZSquareMinus1 = m.c2.z - m.c0.x - m.c1.y;
            float fourWSquareMinus1 = m.c0.x - m.c1.y - m.c2.z;

            int biggestIndex = 0;
            float fourBiggestSquareMinus1 = fourWSquareMinus1;
            if (fourXSquareMinus1 > fourBiggestSquareMinus1)
            {
                fourBiggestSquareMinus1 = fourXSquareMinus1;
                biggestIndex = 1;
            }
            if (fourYSquareMinus1 > fourBiggestSquareMinus1)
            {
                fourBiggestSquareMinus1 = fourYSquareMinus1;
                biggestIndex = 2;
            }
            if (fourZSquareMinus1 > fourBiggestSquareMinus1)
            {
                fourBiggestSquareMinus1 = fourZSquareMinus1;
                biggestIndex = 3;
            }

            float biggestVal = (float)Math.Sqrt(fourBiggestSquareMinus1 + 1.0f) * 0.5f;
            float mult = 0.25f / biggestVal;

            Quatf result = Quatf.UnitW;
            switch (biggestIndex)
            {
                case 0:
                    result.w = biggestVal;
                    result.x = (m.c1.z - m.c2.y) * mult;
                    result.y = (m.c2.x - m.c0.z) * mult;
                    result.z = (m.c0.y - m.c1.x) * mult;
                    break;
                case 1:
                    result.w = (m.c1.z - m.c2.y) * mult;
                    result.x = biggestVal;
                    result.y = (m.c0.y + m.c1.x) * mult;
                    result.z = (m.c2.x + m.c0.z) * mult;
                    break;
                case 2:
                    result.w = (m.c2.x - m.c0.z) * mult;
                    result.x = (m.c0.y + m.c1.x) * mult;
                    result.y = biggestVal;
                    result.z = (m.c1.z + m.c2.y) * mult;
                    break;
                case 3:
                    result.w = (m.c0.y - m.c1.x) * mult;
                    result.x = (m.c2.x + m.c0.z) * mult;
                    result.y = (m.c1.z + m.c2.y) * mult;
                    result.z = biggestVal;
                    break;
            }

            return result;
        }

        /// <summary>
        /// Converts a 4 x 4 matrix to a quaternion
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quatf CreateQuaternion(Mat4f m)
        {
            return CreateQuaternion((Mat3f)m);
        }

        #endregion

        #region Static Operator Functions

        /// <summary>
        /// Adds two quaterions to each other.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quatf operator +(Quatf left, Quatf right)
        {
            return new Quatf()
            {
                w = left.w + right.w,
                x = left.x + right.x,
                y = left.y + right.y,
                z = left.z + right.z
            };
        }

        /// <summary>
        /// Multiplies two qauterions.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quatf operator *(Quatf left, Quatf right)
        {
            return new Quatf()
            {
                w = left.w * right.w - left.x * right.x - left.y * right.y - left.z * right.z,
                x = left.w * right.x + left.x * right.w + left.y * right.z - left.z * right.y,
                y = left.w * right.y + left.y * right.w + left.z * right.x - left.x * right.z,
                z = left.w * right.z + left.z * right.w + left.x * right.y - left.y * right.x
            };
        }

        /// <summary>
        /// Multiplies a quaterion and a vec3f.
        /// </summary>
        /// <param name="q"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f operator *(Quatf q, Vec3f v)
        {
            var QuatVector = new Vec3f(q.x, q.y, q.z);
            var uv = Vec3f.Cross(QuatVector, v);
            var uuv = Vec3f.Cross(QuatVector, uv);
            uv *= (2.0f * q.w);
            uuv *= 2.0f;

            return v + uv + uuv;
        }
        /// <summary>
        /// Multiplies a vec3f and a quaternion.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec3f operator *(Vec3f v, Quatf q)
        {
            return Inverse(q) * v;
        }

        /// <summary>
        /// Multiplies a quaternion and a Vec4f
        /// </summary>
        /// <param name="q"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4f operator *(Quatf q, Vec4f v)
        {
            return new Vec4f(q * (Vec3f)v, v.w);
        }

        /// <summary>
        /// Multiplies a Vec4f and a quaternion
        /// </summary>
        /// <param name="v"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec4f operator *(Vec4f v, Quatf q)
        {
            return Inverse(q) * v;
        }
        /// <summary>
        /// Multiplies a quaternion and a scalar.
        /// </summary>
        /// <param name="q"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quatf operator *(Quatf q, float scalar)
        {
            return new Quatf()
            {
                w = q.w * scalar,
                x = q.x * scalar,
                y = q.y * scalar,
                z = q.z * scalar
            };
        }
        /// <summary>
        /// Multiplies a scalar and a quaterion.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quatf operator *(float scalar, Quatf q)
        {
            return q * scalar;
        }
        /// <summary>
        /// Divides a quaterion with a scalar.
        /// </summary>
        /// <param name="q"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quatf operator /(Quatf q, float scalar)
        {
            return new Quatf()
            {
                w = q.w / scalar,
                x = q.x / scalar,
                y = q.y / scalar,
                z = q.z / scalar
            };
        }

        /// <summary>
        /// Negates a quaternion.
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quatf operator -(Quatf q)
        {
            return new Quatf()
            {
                w = -q.w,
                x = -q.x,
                y = -q.y,
                z = -q.z
            };
        }

        /// <summary>
        /// Compares two quaterion for equality
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Quatf left, Quatf right)
        {
            return
                left.x == right.x &&
                left.y == right.y &&
                left.z == right.z &&
                left.w == right.w;
        }
        /// <summary>
        /// Compares two quaterion for inequality
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Quatf left, Quatf right)
        {
            return
                left.x != right.x ||
                left.y != right.y ||
                left.z != right.z ||
                left.w != right.w;
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
            return w.GetHashCode() ^ x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
        }

        /// <summary>
        /// Returns a string representation of this vector.
        /// </summary>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return string.Format("[{0}, {1}, {2}, {3}]", w, x, y, z);
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
            if (obj is Quatf)
                return Equals((Quatf)obj);
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
        public bool Equals(Quatf other)
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

        public static readonly int SizeInBytes = Marshal.SizeOf(typeof(Quatf));

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
            Quatf v = (Quatf)vec;
            writer.Write(v.w);
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
        public object ReadStream(System.IO.BinaryReader reader)
        {
            return new Quatf()
            {
                w = reader.ReadSingle(),
                x = reader.ReadSingle(),
                y = reader.ReadSingle(),
                z = reader.ReadSingle()                
            };
        }

        #endregion
    }
}
