using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Kraggs.Graphics.Math3D
{
    public static class MathF
    {
        #region Float Functions

        /// <summary>
        /// Clamps x between min and max.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp(float x, float min, float max)
        {
            return Math.Max(Math.Min(x, max), min);
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp01(float x)
        {
            return x < 0.0f ? 0.0f : x > 1.0f ? 1.0f : x;
            //return x > 0.0f ? x < 1.0 ? 
            //return MathF.Clamp(x, 0.0f, 1.0f);
            //return Math.Max(Math.Min(x, 1.0f), 0.0f);
            //return MathF.Max(MathF.Min(x, 1.0f), 0.0f);
        }

        /// <summary>
        /// returns a mix/lerp of two floats with mix factor in a.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Mix(float x, float y, float a)
        {
            return x + a * (y - x);
        }
        /// <summary>
        /// Returns either x or y dependent on a.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Mix(float x, float y, bool a)
        {
            return a ? y : x;
            //return x + a * (y - x);        
        }

        /// <summary>
        /// returns a lerp/mix of two floats with mix factor in a.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Lerp(float x, float y, float a)
        {
            return x + a * (y - x);
        }

        //[DebuggerNonUserCode()]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static float Slerp(float x, float y, float a)
        //{
        //    return x + a * (y - x);
        //}

        //[DebuggerNonUserCode()]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static float Nerp(float x, float y, float a)
        //{
        //    return x + a * (y - x);
        //}

        /// <summary>
        /// step generates a step function by comparing x to edge.
        /// For element i of the return value, 0.0 is returned if x[i] < edge[i], and 1.0 is returned otherwise.
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Step(float edge, float x)
        {
            return x < edge ? 0.0f : 1.0f;
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
        public static float SmoothStep(float edge0, float edge1, float x)
        {
            float tmp = Clamp((x - edge0) / (edge1 - edge0), 0.0f, 1.0f);
            return tmp * tmp * (3.0f - 2.0f * tmp);
        }

        /// <summary>
        /// Higher Multiple number of Source.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="Multiple"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float HigherMultiple(float source, float Multiple)
        {
            int tmp = (int)source % (int)Multiple;
            return tmp != 0 ? source + Multiple - (float)tmp : source;
        }

        /// <summary>
        /// Lower Multiple number of Source.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="Multiple"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float LowerMultiple(float source, float Multiple)
        {
            int tmp = (int)source % (int)Multiple;
            return tmp != 0 ? source - (float)tmp : source;
        }


        #endregion        

        #region SquareRoot

        /// <summary>
        /// Returns the inverse of square root of x.
        /// aka 1 / sqrt(x)
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe float InverseSqrt(float x)
        {
            return 1.0f / MathF.Sqrt(x);
        }

        /// <summary>
        /// Returns the square root of x;
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sqrt(float x)
        {
            return (float)Math.Sqrt(x);
        }

        #endregion

        #region Exponential

        /// <summary>
        /// Returns x powered to y.
        /// aka Pow(2,2) = 4
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Pow(float x, int y)
        {
            return (float)Math.Pow(x, y);
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Exp(float x)
        {
            return (float)Math.Exp(x);
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log(float x)
        {
            return (float)Math.Log(x);
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log2(float x, float newbase)
        {
            return (float)Math.Log(x, newbase);
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Exp2(float x)
        {
            return (float)Math.Exp(0.69314718055994530941723212145818d * x);
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log2(float x)
        {
            return (float)(Math.Log(x) / 0.69314718055994530941723212145818d);
            //return FastMathF.Log(x) / 0.69314718055994530941723212145818f;
        }

        #endregion

        #region Trigonometry

        /// <summary>
        /// Convert degrees to radians.
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ToRadians(float degrees)
        {
            const float pi = 3.1415926535897932384626433832795f;

            return degrees * (pi / 180.0f);
        }

        /// <summary>
        /// Converts radians to degrees.
        /// </summary>
        /// <param name="radians"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ToDegrees(float radians)
        {
            const float pi = 3.1415926535897932384626433832795f;

            return radians * 180.0f / pi;
        }

        /// <summary>
        /// Returns the sine of an angle.
        /// </summary>
        /// <param name="Angle"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sin(float Angle)
        {
            return (float)Math.Sin(Angle);
        }

        /// <summary>
        /// Returns cosine of an angle.
        /// </summary>
        /// <param name="Angle"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cos(float Angle)
        {
            return (float)Math.Cos(Angle);
        }

        /// <summary>
        /// Returns tangent of an angle.
        /// </summary>
        /// <param name="Angle"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Tan(float Angle)
        {
            return (float)Math.Tan(Angle);
        }

        /// <summary>
        /// Returns angle where sine is number.
        /// </summary>
        /// <param name="Angle"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Asin(float Angle)
        {
            return (float)Math.Asin(Angle);
        }

        /// <summary>
        /// Returns angle where cosine is number
        /// </summary>
        /// <param name="Angle"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Acos(float Angle)
        {
            return (float)Math.Acos(Angle);
        }

        /// <summary>
        /// Returns angle where tangent is number.
        /// </summary>
        /// <param name="Angle"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Atan(float Angle)
        {
            return (float)Math.Atan(Angle);
        }

        /// <summary>
        /// Returns angle where tangent is quotient of two numbers.
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Atan2(float y, float x)
        {
            return (float)Math.Atan2(y, x);
            //return (float)Math.Atan(Angle);
        }

        #endregion

        #region Math float wrappers

        /// <summary>
        /// Returns a value indicating the sign of a number.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sign(float x)
        {
            return (float)Math.Sign(x);
        }        

        /// <summary>
        /// Returns the absolute number of a value, ignoring positive/negative.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Abs(float x)
        {
            return (float)Math.Abs(x);
        }

        /// <summary>
        /// Returns the larger of two values.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Max(float left, float right)
        {
            return (float)Math.Max(left, right);
        }

        /// <summary>
        /// Returns the smaller of two values.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Min(float left, float right)
        {
            return (float)Math.Min(left, right);
        }

        /// <summary>
        /// Smallest integral value that is greater than or equal.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Ceiling(float x)
        {
            return (float)Math.Ceiling(x);
        }
        /// <summary>
        /// Largest integer value that is less than or equal
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Floor(float x)
        {
            return (float)Math.Floor(x);
        }
        /// <summary>
        /// Calculates the integral part of a number
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Truncate(float x)
        {
            return (float)Math.Truncate(x);
        }

        #endregion

        /*
Math			MathF
Abs             abs(float)
Acos			Acos
Asin			Asin
Atan			Atan
Atan2			Atan2?
Ceiling			Ceiling(float)		Smallest integral value that is greater than or equal.
Cos				Cos
Cosh						        Hyperbolic Cosine
Exp				Exp
Floor			Floor(float)	    Largest integer value that is less than or equal
Log				Log
Log(newbase?)
Log10
Max             max(float)
Min             min(float)
Pow				Pow
Round
Sign			Sign(float)			Returns a value indicating the sign of a number.
Sin				Sin
Sinh						        Hyperbolic Sin
Sqrt			Sqrt
Tan				Tan
Tanh						        Hyperbolic Tanh
Truncate		Truncate(float)		Calculates the integral part of a number

 */
    }
}
