using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Kraggs.Graphics.Math3D
{
    /// <summary>
    /// This class contains several functions which are less accurate but faster than the mathmatically correct functions i MathF.
    /// </summary>
    public static class FastMathF
    {
        #region SquareRoot

        /// <summary>
        /// Faster than the common inversesqrt function but less accurate.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)] //unsafe never inlined 
        public static unsafe float InverseSqrt(float x)
        {
            float tmp = x;
            float xhalf = 0.5f * tmp;
            int i = *(int*)&x;
            i = 0x5f375a86 - (i >> 1);
            tmp = *(float*)&i;
            tmp = tmp * (1.5f - xhalf * tmp * tmp);

            return tmp;
        }

        /// <summary>
        /// Faster than the common sqrt function but less accurate.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sqrt(float x)
        {
            return 1.0f / InverseSqrt(x);
        }

        #endregion

        #region Exponential

        /// <summary>
        /// Faster than the common pow function but less accurate.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Pow(float x, int y)
        {
            var f = 1.0f;
            for (int i = 0; i < y; ++i)
                f *= x;
            return f;
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vec2f Pow(Vec2f x, Veci.Vec2i y)
        {
            return new Vec2f(
                Pow(x.x, y.x),
                Pow(x.y, y.y));
        }

        /// <summary>
        /// Faster than the common exp function but less accurate.
        /// This function provides accurate results only for value between -1 and 1, else avoid it.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Exp(float x)
        {
            // This has a better looking and same performance in release mode than the following code. However, in debug mode it's slower.
            // return 1.0f + x * (1.0f + x * 0.5f * (1.0f + x * 0.3333333333f * (1.0f + x * 0.25 * (1.0f + x * 0.2f))));

            float x2 = x * x;
            float x3 = x2 * x;
            float x4 = x3 * x;
            float x5 = x4 * x;
            return 1.0f + x + (x2 * 0.5f) + (x3 * 0.1666666667f) + (x4 * 0.041666667f) + (x5 * 0.008333333333f);
        }

        /// <summary>
        /// This one might be slower than Math.Log. Needs testing.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log(float x)
        {
            float y1 = (x - 1.0f) / (x + 1.0f);
            float y2 = y1 * y1;
            return 2.0f * y1 * (1.0f + y2 * (0.3333333333f * y2 * (0.2f + y2 * 0.1428571429f)));
        }

        /// <summary>
        /// Returns the logarithm for any base.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="newbase"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log(float x, float newbase)
        {
            Debug.Assert(x != 0.0f);

            return FastMathF.Log(x) / FastMathF.Log(newbase);
        }

        /// <summary>
        /// Faster than the common exp2 function but less accurate.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Exp2(float x)
        {
            return FastMathF.Exp(0.69314718055994530941723212145818f * x);
        }

        /// <summary>
        /// Faster than the common log2 function but less accurate.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log2(float x)
        {
            return FastMathF.Log(x) / 0.69314718055994530941723212145818f;
        }

        #endregion

        #region Trigonometry

        /// <summary>
        /// Faster than the common sin function but less accurate.
        /// Defined between -2pi and 2pi.
        /// </summary>
        /// <param name="Angle"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sin(float Angle)
        {
            return Angle - ((Angle * Angle * Angle) / 6.0f) +
                ((Angle * Angle * Angle * Angle * Angle) / 120.0f) -
                ((Angle * Angle * Angle * Angle * Angle * Angle * Angle) / 120.0f);
        }

        /// <summary>
        /// Faster than the common cos function but less accurate.
        /// Defined between -2pi and 2pi.
        /// </summary>
        /// <param name="Angle"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cos(float Angle)
        {
            return 1.0f - (Angle * Angle * 0.5f) +
                (Angle * Angle * Angle * Angle * 0.041666666666f) -
                (Angle * Angle * Angle * Angle * Angle * Angle * 0.00138888888888f);
        }

        /// <summary>
        /// Faster than the common tan function but less accurate.
        /// Defined between -2pi and 2pi.
        /// </summary>
        /// <param name="Angle"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Tan(float Angle)
        {
            return Angle + (Angle * Angle * Angle * 0.3333333333f) +
                (Angle * Angle * Angle * Angle * Angle * 0.1333333333333f) +
                (Angle * Angle * Angle * Angle * Angle * Angle * Angle * 0.0539682539f);
        }

        /// <summary>
        /// Faster than the common asin function but less accurate.
        /// Defined between -2pi and 2pi.
        /// </summary>
        /// <param name="Angle"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Asin(float Angle)
        {
            return Angle + (Angle * Angle * Angle * 0.166666667f) +
                (Angle * Angle * Angle * Angle * Angle * 0.075f) +
                (Angle * Angle * Angle * Angle * Angle * Angle * Angle * 0.0446428571f) +
                (Angle * Angle * Angle * Angle * Angle * Angle * Angle * Angle * Angle * 0.0303819444f);
            // + (x * x * x * x * x * x * x * x * x * x * x * T(0.022372159));
        }

        /// <summary>
        /// Faster than the common acos function but less accurate.
        /// Defined between -2pi and 2pi.
        /// </summary>
        /// <param name="Angle"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Acos(float Angle)
        {
            return 1.5707963267948966192313216916398f - FastMathF.Asin(Angle); //(PI / 2)
        }

        /// <summary>
        /// Faster than the common atan function but less accurate.
        /// Defined between -2pi and 2pi.
        /// </summary>
        /// <param name="Angle"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Atan(float Angle)
        {
            return Angle - (Angle * Angle * Angle * 0.333333333333f) +
                (Angle * Angle * Angle * Angle * Angle * 0.2f) -
                (Angle * Angle * Angle * Angle * Angle * Angle * Angle * 0.1428571429f) +
                (Angle * Angle * Angle * Angle * Angle * Angle * Angle * Angle * Angle * 0.111111111111f) -
                (Angle * Angle * Angle * Angle * Angle * Angle * Angle * Angle * Angle * Angle * Angle * 0.0909090909f);
        }

        /// <summary>
        /// Faster than the common atan function but less accurate.
        /// Defined between -2pi and 2pi.
        /// </summary>
        /// <param name="Angle"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Atan2(float y, float x)
        {
            var sgn = MathF.Sign(y) * MathF.Sign(x);
            return MathF.Abs(Atan(y / x)) * sgn;
            //throw new NotImplementedException();
        }

        #endregion
    }
}
