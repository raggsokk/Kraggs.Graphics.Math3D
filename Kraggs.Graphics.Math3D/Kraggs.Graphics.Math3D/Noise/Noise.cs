using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Kraggs.Graphics.Math3D
{
    internal static class Noise
    {
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float Mod289(float x)
        {
            return x - MathF.Floor(x * 1.0f / 289.0f) * 289.0f;
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float Permute(float x)
        {
            return Mod289(((x * 34.0f) + 1.0f) * x);
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vec2f Permute(Vec2f x)
        {
            throw new NotImplementedException();
            //return Mod289(((x * 34.0f)) + 1.0f) * x);
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float TaylorInvSqrt(float r)
        {
            return 1.79284291400159f - 0.85373472095314f * r;
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vec2f TaylorInvSqrt(Vec2f r)
        {
            return 1.79284291400159f - 0.85373472095314f * r;
        }
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vec3f TaylorInvSqrt(Vec3f r)
        {
            return 1.79284291400159f - 0.85373472095314f * r;
        }
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vec4f TaylorInvSqrt(Vec4f r)
        {
            //Vec4f result;
            //Vec4f.Multiply(ref r, 0.85373472095314f, out result);
            //return 1.79284291400159f - result;
            return 1.79284291400159f - 0.85373472095314f * r;
        }
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vec2f Fade(Vec2f t)
        {
            return (t * t * t) * (t * (t * 6.0f - 15.0f + 10.0f));
        }
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vec3f Fade(Vec3f t)
        {
            return (t * t * t) * (t * (t * 6.0f - 15.0f + 10.0f));
        }
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vec4f Fade(Vec4f t)
        {
            return (t * t * t) * (t * (t * 6.0f - 15.0f + 10.0f));
        }

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vec3f Fract(Vec3f v)
        {
            return v - Vec3f.Floor(v);
        }

        //LessThan

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vec4f Grad4(float j, Vec4f ip)
        {
            var pXYZ = Vec3f.Floor(Fract(new Vec3f(j) * (Vec3f)ip) * 7.0f) * ip.z - 1.0f;
            float pW = 1.5f - Vec3f.Dot(Vec3f.Abs(pXYZ), Vec3f.One);
            Vec4f s = new Vec4f(Vec4f.LessThan(new Vec4f(pXYZ, pW), Vec4f.Zero));
            pXYZ = pXYZ + (Vec3f)s * 2.0f - 1.0f * s.w;
            return new Vec4f(pXYZ, pW);
        }

    }
}
