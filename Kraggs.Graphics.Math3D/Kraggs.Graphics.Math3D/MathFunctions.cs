﻿using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Kraggs.Graphics.Math3D
{
    [DebuggerNonUserCode()]
    public static class MathFunctions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Pow(float x, float y)
        {
            return (float)Math.Pow(x, y);
        }

        /// <summary>
        /// Returns the natural exponentiation of x, i.e, e ^ x
        /// </summary>
        /// <param name="x">x can be in range [inf-, inf+].</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Exp(float x)
        {            
            return (float)Math.Exp(x);
        }

        /// <summary>
        /// Returns the natural logarithm of x, i.e.,
        /// returns the value y withc satisfies the equation x = e ^ y
        /// </summary>
        /// <param name="x">result are undefined if x <= 0</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log(float x)
        {            
            return (float)Math.Log(x);
        }

        /// <summary>
        /// Returns 2 raised to the x power.
        /// </summary>
        /// <param name="x">can be in range [inf-, inf+].</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Exp2(float x)
        {
            //return (float)Math.Pow(2.0d, x);
            return (float)Math.Exp(0.69314718055994530941723212145818f * x);
        }

        /// <summary>
        /// Returns the base 2 log of x, returns the value y,
        /// with satisfies the equation x = 2 ^ y.
        /// </summary>
        /// <param name="x">x needs to be in range [0, inf+] for return to be valid.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log2(float x)
        {
            Debug.Assert(x >= 0.0f);
            //return (float)Math.Log(a, newbase);            

            //return (float)Math.Log(x, 2.0d);
            return (float)(Math.Log(x) / 0.69314718055994530941723212145818f);
        }

        /// <summary>
        /// Returns the positive square root of f.
        /// </summary>
        /// <param name="f">sqrt function is defined for input values of f in the range [0, inf+].</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sqrt(float f)
        {
            return (float)Math.Sqrt(f);
        }

        /// <summary>
        /// Returns the reciprocal of the positive square root of f.
        /// </summary>
        /// <param name="f">InverseSqrt function is defined for input values of f in the range [0, inf+].</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float InverseSqrt(float f)
        {
            return (float)1.0f / Sqrt(f);
        }

        #region Fast Functions

        public static unsafe float FastInverseSqrt(float x)
        {
            float tmp = x;
            float xhalf = 0.5f * tmp;
            int i = *(int*)&x;
            i = 0x5f375a86 - (i >> 1);
            tmp = *(float*)&i;
            tmp = tmp * (1.5f - xhalf * tmp * tmp);

            return tmp;
        }

        public static float FastSqrt(float x)
        {
            return 1.0f / FastInverseSqrt(x);
        }

        #endregion

        #region Trigonometric

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Radians(float degrees)
        {
            const float pi = 3.1415926535897932384626433832795f;

            return degrees * (pi / 180.0f);
        }
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Degrees(float radians)
        {
            const float pi = 3.1415926535897932384626433832795f;

            return radians * 180.0f / pi;
            
        }

        #endregion

        #region Other Math Functions // nice name?

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

        /// <summary>
        /// returns a mix of two floats with mix factor in a.
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

        #endregion
    }
}
