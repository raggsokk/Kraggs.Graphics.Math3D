using System;
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
    }
}
