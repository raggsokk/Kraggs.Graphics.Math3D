using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Kraggs.Graphics.Math3D
{
    public static partial class NoiseF
    {
        #region GLM Implementation Code

        private static readonly Vec4f VEC4F_0011 = new Vec4f(0.0f, 0.0f, 1.0f, 1.0f);

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float Mod289(float x)
        {
            return x - MathF.Floor(x * 1.0f / 289.0f) * 289.0f;
        }
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vec2f Mod289(Vec2f x)
        {
            return x - Vec2f.Floor(x * 1.0f / 289.0f) * 289.0f;
        }
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vec3f Mod289(Vec3f x)
        {
            return x - Vec3f.Floor(x * 1.0f / 289.0f) * 289.0f;
        }
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vec4f Mod289(Vec4f x)
        {
            return x - Vec4f.Floor(x * 1.0f / 289.0f) * 289.0f;
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
            //throw new NotImplementedException();
            return Mod289(((x * 34.0f) + 1.0f) * x);
        }
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vec3f Permute(Vec3f x)
        {
            //throw new NotImplementedException();
            return Mod289(((x * 34.0f) + 1.0f) * x);
        }
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vec4f Permute(Vec4f x)
        {
            //throw new NotImplementedException();
            return Mod289(((x * 34.0f) + 1.0f) * x);
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

        //[DebuggerNonUserCode()]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //internal static Vec3f Fract(Vec3f v)
        //{
        //    return v - Vec3f.Floor(v);
        //}

        ////LessThan

        //[DebuggerNonUserCode()]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //internal static Vec4f Grad4(float j, Vec4f ip)
        //{
        //    var pXYZ = Vec3f.Floor(Fract(new Vec3f(j) * (Vec3f)ip) * 7.0f) * ip.z - 1.0f;
        //    float pW = 1.5f - Vec3f.Dot(Vec3f.Abs(pXYZ), Vec3f.One);
        //    Vec4f s = new Vec4f(Vec4f.LessThan(new Vec4f(pXYZ, pW), Vec4f.Zero));
        //    pXYZ = pXYZ + (Vec3f)s * 2.0f - 1.0f * s.w;
        //    return new Vec4f(pXYZ, pW);
        //}

        /// <summary>
        /// Classic Perlin Noise.
        /// </summary>
        /// <param name="Position"></param>
        /// <returns></returns>
        public static float Perlin(Vec2f Position)
        {            
            Vec4f tmpXYXY = new Vec4f(Position.x, Position.y, Position.x, Position.y);
            Vec4f Pi = Vec4f.Floor(tmpXYXY) + VEC4F_0011;
            Vec4f Pf = Vec4f.Fract(tmpXYXY) + VEC4F_0011;
            // To avoid truncation effects in permutation
            Pi = Vec4f.Mod(Pi, 289.0f);

            return PerlinCommon2(Pi, Pf);

            //Vec4f ix = new Vec4f(Pi.x, Pi.z, Pi.x, Pi.z);
            //Vec4f iy = new Vec4f(Pi.y, Pi.y, Pi.w, Pi.w);
            //Vec4f fx = new Vec4f(Pf.x, Pf.z, Pf.x, Pf.z);
            //Vec4f fy = new Vec4f(Pf.y, Pf.y, Pf.w, Pf.w);

            //Vec4f i = Permute(Permute(ix) + iy);

            //Vec4f gx = 2.0f * Vec4f.Fract(i / 41.0f) - 1.0f;
            //Vec4f gy = Vec4f.Abs(gx) - 0.5f;
            //Vec4f tx = Vec4f.Floor(gx + 0.5f);
            //gx = gx - tx;

            //Vec2f g00 = new Vec2f(gx.x, gy.x);
            //Vec2f g10 = new Vec2f(gx.y, gy.y);
            //Vec2f g01 = new Vec2f(gx.z, gy.z);
            //Vec2f g11 = new Vec2f(gx.w, gy.w);

            //Vec4f norm = TaylorInvSqrt(new Vec4f(
            //    Vec2f.Dot(g00, g00),
            //    Vec2f.Dot(g01, g01),
            //    Vec2f.Dot(g10, g10),
            //    Vec2f.Dot(g11, g11)));

            //g00 *= norm.x;
            //g01 *= norm.y;
            //g10 *= norm.z;
            //g11 *= norm.w;

            //float n00 = Vec2f.Dot(g00, new Vec2f(fx.x, fy.x));
            //float n10 = Vec2f.Dot(g10, new Vec2f(fx.y, fy.y));
            //float n01 = Vec2f.Dot(g01, new Vec2f(fx.z, fy.z));
            //float n11 = Vec2f.Dot(g11, new Vec2f(fx.w, fy.w));

            //Vec2f fade_xy = Fade(new Vec2f(Pf.x, Pf.y));
            //Vec2f n_x = Vec2f.Mix(new Vec2f(n00, n01), new Vec2f(n10, n11), fade_xy.x);
            //float n_xy = MathF.Mix(n_x.x, n_x.y, fade_xy.y);

            //return 2.3f * n_xy;
        }

        /// <summary>
        /// Periodic Perlin Noise
        /// </summary>
        /// <param name="Position"></param>
        /// <param name="rep"></param>
        /// <returns></returns>
        public static float Perlin(Vec2f Position, Vec2f rep)
        {
            Vec4f tmpXYXY = new Vec4f(Position.x, Position.y, Position.x, Position.y);
            Vec4f Pi = Vec4f.Floor(tmpXYXY) + VEC4F_0011;
            Vec4f Pf = Vec4f.Fract(tmpXYXY) + VEC4F_0011;
            // To create noise with explicit period
            Pi = Vec4f.Mod(Pi, new Vec4f(rep.x, rep.y, rep.x, rep.y));
            // To avoid truncation effects in permutation
            Pi = Vec4f.Mod(Pi, 289.0f);

            return PerlinCommon2(Pi, Pf);
        }
        private static float PerlinCommon2(Vec4f Pi, Vec4f Pf)
        {
            Vec4f ix = new Vec4f(Pi.x, Pi.z, Pi.x, Pi.z);
            Vec4f iy = new Vec4f(Pi.y, Pi.y, Pi.w, Pi.w);
            Vec4f fx = new Vec4f(Pf.x, Pf.z, Pf.x, Pf.z);
            Vec4f fy = new Vec4f(Pf.y, Pf.y, Pf.w, Pf.w);

            Vec4f i = Permute(Permute(ix) + iy);

            Vec4f gx = 2.0f * Vec4f.Fract(i / 41.0f) - 1.0f;
            Vec4f gy = Vec4f.Abs(gx) - 0.5f;
            Vec4f tx = Vec4f.Floor(gx + 0.5f);
            gx = gx - tx;

            Vec2f g00 = new Vec2f(gx.x, gy.x);
            Vec2f g10 = new Vec2f(gx.y, gy.y);
            Vec2f g01 = new Vec2f(gx.z, gy.z);
            Vec2f g11 = new Vec2f(gx.w, gy.w);

            Vec4f norm = TaylorInvSqrt(new Vec4f(
                Vec2f.Dot(g00, g00),
                Vec2f.Dot(g01, g01),
                Vec2f.Dot(g10, g10),
                Vec2f.Dot(g11, g11)));

            g00 *= norm.x;
            g01 *= norm.y;
            g10 *= norm.z;
            g11 *= norm.w;

            float n00 = Vec2f.Dot(g00, new Vec2f(fx.x, fy.x));
            float n10 = Vec2f.Dot(g10, new Vec2f(fx.y, fy.y));
            float n01 = Vec2f.Dot(g01, new Vec2f(fx.z, fy.z));
            float n11 = Vec2f.Dot(g11, new Vec2f(fx.w, fy.w));

            Vec2f fade_xy = Fade(new Vec2f(Pf.x, Pf.y));
            Vec2f n_x = Vec2f.Mix(new Vec2f(n00, n01), new Vec2f(n10, n11), fade_xy.x);
            float n_xy = MathF.Mix(n_x.x, n_x.y, fade_xy.y);

            return 2.3f * n_xy;

        }

        /// <summary>
        /// Classic Perlin Noise
        /// </summary>
        /// <param name="Position"></param>
        /// <returns></returns>
        public static float Perlin(Vec3f Position)
        {
            // Integer part for indexing
            Vec3f Pi0 = Vec3f.Floor(Position);
            // Integer part + 1
            Vec3f Pi1 = Pi0 + 1.0f;
            Pi0 = Mod289(Pi0);
            Pi1 = Mod289(Pi1);
            Vec3f Pf0 = Vec3f.Fract(Position);  // Fractional part for interpolation
            Vec3f Pf1 = Pf0 - 1.0f;             // Fractional part - 1.0

            return PerlinCommon3(Pi0, Pi1, Pf0, Pf1);
            //Vec4f ix = new Vec4f(Pi0.x, Pi1.x, Pi0.x, Pi1.x);
            //Vec4f iy = new Vec4f(Pi0.y, Pi0.y, Pi1.y, Pi1.y);
            //Vec4f iz0 = new Vec4f(Pi0.z);
            //Vec4f iz1 = new Vec4f(Pi1.z);

            //Vec4f ixy = Permute(Permute(ix) + iy);
            //Vec4f ixy0 = Permute(ixy + iz0);
            //Vec4f ixy1 = Permute(ixy + iz1);

            //Vec4f gx0 = ixy * 1.0f / 7.0f;
            //Vec4f gy0 = Vec4f.Fract(Vec4f.Floor(gx0) * 1.0f / 7.0f) - 0.5f;
            //gx0 = Vec4f.Fract(gx0);
            //Vec4f gz0 = new Vec4f(0.5f) - Vec4f.Abs(gx0) - Vec4f.Abs(gy0);
            //Vec4f sz0 = Vec4f.Step(gz0, Vec4f.Zero);
            //gx0 -= sz0 * (Vec4f.Step(Vec4f.Zero, gx0) - 0.5f);
            //gy0 -= sz0 * (Vec4f.Step(Vec4f.Zero, gy0) - 0.5f);

            //Vec4f gx1 = ixy1 * 1.0f / 7.0f;
            //Vec4f gy1 = Vec4f.Fract(Vec4f.Floor(gx1) * 1.0f / 7.0f) - 0.5f;
            //gx1 = Vec4f.Fract(gx1);
            //Vec4f gz1 = new Vec4f(0.5f) - Vec4f.Abs(gx1) - Vec4f.Abs(gy1);
            //Vec4f sz1 = Vec4f.Step(gz1, Vec4f.Zero);
            //gx1 -= sz1 * (Vec4f.Step(Vec4f.Zero, gx1) - 0.5f);
            //gy1 -= sz1 * (Vec4f.Step(Vec4f.Zero, gy1) - 0.5f);

            //Vec3f g000 = new Vec3f(gx0.x, gy0.x, gz0.x);
            //Vec3f g100 = new Vec3f(gx0.y, gy0.y, gz0.y);
            //Vec3f g010 = new Vec3f(gx0.z, gy0.z, gz0.z);
            //Vec3f g110 = new Vec3f(gx0.w, gy0.w, gz0.w);
            //Vec3f g001 = new Vec3f(gx1.x, gy1.x, gz1.x);
            //Vec3f g101 = new Vec3f(gx1.y, gy1.y, gz1.y);
            //Vec3f g011 = new Vec3f(gx1.z, gy1.z, gz1.z);
            //Vec3f g111 = new Vec3f(gx1.w, gy1.w, gz1.w);

            //Vec4f norm0 = TaylorInvSqrt(new Vec4f(
            //    Vec3f.Dot(g000, g000),
            //    Vec3f.Dot(g010, g010),
            //    Vec3f.Dot(g100, g100),
            //    Vec3f.Dot(g110, g110)));
            //g000 *= norm0.x;
            //g010 *= norm0.y;
            //g100 *= norm0.z;
            //g110 *= norm0.w;

            //Vec4f norm1 = TaylorInvSqrt(new Vec4f(
            //    Vec3f.Dot(g001, g001),
            //    Vec3f.Dot(g011, g011),
            //    Vec3f.Dot(g101, g101),
            //    Vec3f.Dot(g111, g111)));
            //g001 *= norm1.x;
            //g011 *= norm1.y;
            //g101 *= norm1.z;
            //g111 *= norm1.w;

            //float n000 = Vec3f.Dot(g000, Pf0);
            //float n100 = Vec3f.Dot(g100, new Vec3f(Pf1.x, Pf0.y, Pf0.z));
            //float n010 = Vec3f.Dot(g010, new Vec3f(Pf0.x, Pf1.y, Pf0.z));
            //float n110 = Vec3f.Dot(g110, new Vec3f(Pf1.x, Pf1.y, Pf0.z));
            //float n001 = Vec3f.Dot(g001, new Vec3f(Pf0.x, Pf0.y, Pf1.z));
            //float n101 = Vec3f.Dot(g101, new Vec3f(Pf1.x, Pf0.y, Pf1.z));
            //float n011 = Vec3f.Dot(g011, new Vec3f(Pf0.x, Pf1.y, Pf1.z));
            //float n111 = Vec3f.Dot(g111, Pf1);

            //Vec3f fade_xyz = Fade(Pf0);
            //Vec4f n_z = Vec4f.Mix(new Vec4f(n000, n100, n010, n110), new Vec4f(n001, n101, n011, n111), fade_xyz.z);
            //Vec2f n_yz = Vec2f.Mix(new Vec2f(n_z.x, n_z.y), new Vec2f(n_z.z, n_z.w), fade_xyz.y);
            //float n_xyz = MathF.Mix(n_yz.x, n_yz.y, fade_xyz.x);

            //return 2.2f * n_xyz;
        }


        /// <summary>
        /// Periodic Perlin Noise
        /// </summary>
        /// <param name="Position"></param>
        /// <param name="rep"></param>
        /// <returns></returns>
        public static float Perlin(Vec3f Position, Vec3f rep)
        {
            // Integer part for indexing, modulo period
            Vec3f Pi0 = Vec3f.Mod(Vec3f.Floor(Position), rep);
            // Integer part + 1, mod period
            Vec3f Pi1 = Vec3f.Mod(Pi0 + 1.0f, rep);
            Pi0 = Mod289(Pi0);
            Pi1 = Mod289(Pi1);
            Vec3f Pf0 = Vec3f.Fract(Position);  // Fractional part for interpolation
            Vec3f Pf1 = Pf0 - 1.0f;             // Fractional part - 1.0

            return PerlinCommon3(Pi0, Pi1, Pf0, Pf1);
        }
        private static float PerlinCommon3(Vec3f Pi0, Vec3f Pi1, Vec3f Pf0, Vec3f Pf1)
        {
            Vec4f ix = new Vec4f(Pi0.x, Pi1.x, Pi0.x, Pi1.x);
            Vec4f iy = new Vec4f(Pi0.y, Pi0.y, Pi1.y, Pi1.y);
            Vec4f iz0 = new Vec4f(Pi0.z);
            Vec4f iz1 = new Vec4f(Pi1.z);

            Vec4f ixy = Permute(Permute(ix) + iy);
            Vec4f ixy0 = Permute(ixy + iz0);
            Vec4f ixy1 = Permute(ixy + iz1);

            Vec4f gx0 = ixy * 1.0f / 7.0f;
            Vec4f gy0 = Vec4f.Fract(Vec4f.Floor(gx0) * 1.0f / 7.0f) - 0.5f;
            gx0 = Vec4f.Fract(gx0);
            Vec4f gz0 = new Vec4f(0.5f) - Vec4f.Abs(gx0) - Vec4f.Abs(gy0);
            Vec4f sz0 = Vec4f.Step(gz0, Vec4f.Zero);
            gx0 -= sz0 * (Vec4f.Step(Vec4f.Zero, gx0) - 0.5f);
            gy0 -= sz0 * (Vec4f.Step(Vec4f.Zero, gy0) - 0.5f);

            Vec4f gx1 = ixy1 * 1.0f / 7.0f;
            Vec4f gy1 = Vec4f.Fract(Vec4f.Floor(gx1) * 1.0f / 7.0f) - 0.5f;
            gx1 = Vec4f.Fract(gx1);
            Vec4f gz1 = new Vec4f(0.5f) - Vec4f.Abs(gx1) - Vec4f.Abs(gy1);
            Vec4f sz1 = Vec4f.Step(gz1, Vec4f.Zero);
            gx1 -= sz1 * (Vec4f.Step(Vec4f.Zero, gx1) - 0.5f);
            gy1 -= sz1 * (Vec4f.Step(Vec4f.Zero, gy1) - 0.5f);

            Vec3f g000 = new Vec3f(gx0.x, gy0.x, gz0.x);
            Vec3f g100 = new Vec3f(gx0.y, gy0.y, gz0.y);
            Vec3f g010 = new Vec3f(gx0.z, gy0.z, gz0.z);
            Vec3f g110 = new Vec3f(gx0.w, gy0.w, gz0.w);
            Vec3f g001 = new Vec3f(gx1.x, gy1.x, gz1.x);
            Vec3f g101 = new Vec3f(gx1.y, gy1.y, gz1.y);
            Vec3f g011 = new Vec3f(gx1.z, gy1.z, gz1.z);
            Vec3f g111 = new Vec3f(gx1.w, gy1.w, gz1.w);

            Vec4f norm0 = TaylorInvSqrt(new Vec4f(
                Vec3f.Dot(g000, g000),
                Vec3f.Dot(g010, g010),
                Vec3f.Dot(g100, g100),
                Vec3f.Dot(g110, g110)));
            g000 *= norm0.x;
            g010 *= norm0.y;
            g100 *= norm0.z;
            g110 *= norm0.w;

            Vec4f norm1 = TaylorInvSqrt(new Vec4f(
                Vec3f.Dot(g001, g001),
                Vec3f.Dot(g011, g011),
                Vec3f.Dot(g101, g101),
                Vec3f.Dot(g111, g111)));
            g001 *= norm1.x;
            g011 *= norm1.y;
            g101 *= norm1.z;
            g111 *= norm1.w;

            float n000 = Vec3f.Dot(g000, Pf0);
            float n100 = Vec3f.Dot(g100, new Vec3f(Pf1.x, Pf0.y, Pf0.z));
            float n010 = Vec3f.Dot(g010, new Vec3f(Pf0.x, Pf1.y, Pf0.z));
            float n110 = Vec3f.Dot(g110, new Vec3f(Pf1.x, Pf1.y, Pf0.z));
            float n001 = Vec3f.Dot(g001, new Vec3f(Pf0.x, Pf0.y, Pf1.z));
            float n101 = Vec3f.Dot(g101, new Vec3f(Pf1.x, Pf0.y, Pf1.z));
            float n011 = Vec3f.Dot(g011, new Vec3f(Pf0.x, Pf1.y, Pf1.z));
            float n111 = Vec3f.Dot(g111, Pf1);

            Vec3f fade_xyz = Fade(Pf0);
            Vec4f n_z = Vec4f.Mix(new Vec4f(n000, n100, n010, n110), new Vec4f(n001, n101, n011, n111), fade_xyz.z);
            Vec2f n_yz = Vec2f.Mix(new Vec2f(n_z.x, n_z.y), new Vec2f(n_z.z, n_z.w), fade_xyz.y);
            float n_xyz = MathF.Mix(n_yz.x, n_yz.y, fade_xyz.x);

            return 2.2f * n_xyz;
            
        }

        /// <summary>
        /// Classic Perlin Noise
        /// </summary>
        /// <param name="Position"></param>
        /// <returns></returns>
        public static float Perlin(Vec4f Position)
        {
            Vec4f Pi0 = Vec4f.Floor(Position);   // Integer part for indexing
            Vec4f Pi1 = Pi0 + 1.0f;              // Integer part + 1
            Pi0 = Vec4f.Mod(Pi0, 289.0f);
            Pi1 = Vec4f.Mod(Pi1, 289.0f);
            Vec4f Pf0 = Vec4f.Fract(Position);
            Vec4f Pf1 = Pf0 - 1.0f;

            return PerlinCommon4(Pi0, Pi1, Pf0, Pf1);

            //Vec4f ix = new Vec4f(Pi0.x, Pi1.x, Pi0.x, Pi1.x);
            //Vec4f iy = new Vec4f(Pi0.y, Pi0.y, Pi1.y, Pi1.y);
            //Vec4f iz0 = new Vec4f(Pi0.z);
            //Vec4f iz1 = new Vec4f(Pi1.z);
            //Vec4f iw0 = new Vec4f(Pi0.w);
            //Vec4f iw1 = new Vec4f(Pi1.w);

            //Vec4f ixy = Permute(Permute(ix) + iy);
            //Vec4f ixy0 = Permute(ixy + iz0);
            //Vec4f ixy1 = Permute(ixy + iz1);
            //Vec4f ixy00 = Permute(ixy0 + iw0);
            //Vec4f ixy01 = Permute(ixy0 + iw1);
            //Vec4f ixy10 = Permute(ixy1 + iw0);
            //Vec4f ixy11 = Permute(ixy1 + iw1);

            //Vec4f gx00 = ixy00 / 7.0f;
            //Vec4f gy00 = Vec4f.Floor(gx00) / 7.0f;
            //Vec4f gz00 = Vec4f.Floor(gy00) / 6.0f;
            //gx00 = Vec4f.Fract(gx00) - 0.5f;
            //gy00 = Vec4f.Fract(gy00) - 0.5f;
            //gz00 = Vec4f.Fract(gz00) - 0.5f;
            //Vec4f gw00 = new Vec4f(0.75f) - Vec4f.Abs(gx00) - Vec4f.Abs(gy00) - Vec4f.Abs(gz00);
            //Vec4f sw00 = Vec4f.Step(gw00, Vec4f.Zero);
            //gx00 -= sw00 * (Vec4f.Step(0.0f, gx00) - 0.5f);
            //gy00 -= sw00 * (Vec4f.Step(0.0f, gy00) - 0.5f);

            //Vec4f gx01 = ixy01 / 7.0f;
            //Vec4f gy01 = Vec4f.Floor(gx01) / 7.0f;
            //Vec4f gz01 = Vec4f.Floor(gy01) / 6.0f;
            //gx01 = Vec4f.Fract(gx01) - 0.5f;
            //gy01 = Vec4f.Fract(gy01) - 0.5f;
            //gz01 = Vec4f.Fract(gz01) - 0.5f;
            //Vec4f gw01 = new Vec4f(0.75f) - Vec4f.Abs(gx01) - Vec4f.Abs(gy01) - Vec4f.Abs(gz01);
            //Vec4f sw01 = Vec4f.Step(gw01, Vec4f.Zero);
            //gx01 -= sw01 * (Vec4f.Step(0.0f, gx01) - 0.5f);
            //gy01 -= sw01 * (Vec4f.Step(0.0f, gy01) - 0.5f);

            //Vec4f gx10 = ixy10 / 7.0f;
            //Vec4f gy10 = Vec4f.Floor(gx10) / 7.0f;
            //Vec4f gz10 = Vec4f.Floor(gy10) / 6.0f;
            //gx10 = Vec4f.Fract(gx10) - 0.5f;
            //gy10 = Vec4f.Fract(gy10) - 0.5f;
            //gz10 = Vec4f.Fract(gz10) - 0.5f;
            //Vec4f gw10 = new Vec4f(0.75f) - Vec4f.Abs(gx10) - Vec4f.Abs(gy10) - Vec4f.Abs(gz10);
            //Vec4f sw10 = Vec4f.Step(gw10, Vec4f.Zero);
            //gx10 -= sw10 * (Vec4f.Step(0.0f, gx10) - 0.5f);
            //gy10 -= sw10 * (Vec4f.Step(0.0f, gy10) - 0.5f);

            //Vec4f gx11 = ixy11 / 7.0f;
            //Vec4f gy11 = Vec4f.Floor(gx11) / 7.0f;
            //Vec4f gz11 = Vec4f.Floor(gy11) / 6.0f;
            //gx11 = Vec4f.Fract(gx11) - 0.5f;
            //gy11 = Vec4f.Fract(gy11) - 0.5f;
            //gz11 = Vec4f.Fract(gz11) - 0.5f;
            //Vec4f gw11 = new Vec4f(0.75f) - Vec4f.Abs(gx11) - Vec4f.Abs(gy11) - Vec4f.Abs(gz11);
            //Vec4f sw11 = Vec4f.Step(gw11, Vec4f.Zero);
            //gx11 -= sw11 * (Vec4f.Step(0.0f, gx11) - 0.5f);
            //gy11 -= sw11 * (Vec4f.Step(0.0f, gy11) - 0.5f);

            //Vec4f g0000 = new Vec4f(gx00.x, gy00.x, gz00.x, gw00.x);
            //Vec4f g1000 = new Vec4f(gx00.y, gy00.y, gz00.y, gw00.y);
            //Vec4f g0100 = new Vec4f(gx00.z, gy00.z, gz00.z, gw00.z);
            //Vec4f g1100 = new Vec4f(gx00.w, gy00.w, gz00.w, gw00.w);
            //Vec4f g0010 = new Vec4f(gx10.x, gy10.x, gz10.x, gw10.x);
            //Vec4f g1010 = new Vec4f(gx10.y, gy10.y, gz10.y, gw10.y);
            //Vec4f g0110 = new Vec4f(gx10.z, gy10.z, gz10.z, gw10.z);
            //Vec4f g1110 = new Vec4f(gx10.w, gy10.w, gz10.w, gw10.w);
            //Vec4f g0001 = new Vec4f(gx01.x, gy01.x, gz01.x, gw01.x);
            //Vec4f g1001 = new Vec4f(gx01.y, gy01.y, gz01.y, gw01.y);
            //Vec4f g0101 = new Vec4f(gx01.z, gy01.z, gz01.z, gw01.z);
            //Vec4f g1101 = new Vec4f(gx01.w, gy01.w, gz01.w, gw01.w);
            //Vec4f g0011 = new Vec4f(gx11.x, gy11.x, gz11.x, gw11.x);
            //Vec4f g1011 = new Vec4f(gx11.y, gy11.y, gz11.y, gw11.y);
            //Vec4f g0111 = new Vec4f(gx11.z, gy11.z, gz11.z, gw11.z);
            //Vec4f g1111 = new Vec4f(gx11.w, gy11.w, gz11.w, gw11.w);

            //Vec4f norm00 = TaylorInvSqrt(new Vec4f(
            //    Vec4f.Dot(g0000, g0000),
            //    Vec4f.Dot(g0100, g0100),
            //    Vec4f.Dot(g1000, g1000),
            //    Vec4f.Dot(g1100, g1100)));
            //g0000 *= norm00.x;
            //g0100 *= norm00.y;
            //g1000 *= norm00.z;
            //g1100 *= norm00.w;

            //Vec4f norm01 = TaylorInvSqrt(new Vec4f(
            //    Vec4f.Dot(g0001, g0001),
            //    Vec4f.Dot(g0101, g0101),
            //    Vec4f.Dot(g1001, g1001),
            //    Vec4f.Dot(g1101, g1101)));
            //g0001 *= norm01.x;
            //g0101 *= norm01.y;
            //g1001 *= norm01.z;
            //g1101 *= norm01.w;

            //Vec4f norm10 = TaylorInvSqrt(new Vec4f(
            //    Vec4f.Dot(g0010, g0010),
            //    Vec4f.Dot(g0110, g0110),
            //    Vec4f.Dot(g1010, g1010),
            //    Vec4f.Dot(g1110, g1110)));
            //g0010 *= norm10.x;
            //g0110 *= norm10.y;
            //g1010 *= norm10.z;
            //g1110 *= norm10.w;

            //Vec4f norm11 = TaylorInvSqrt(new Vec4f(
            //    Vec4f.Dot(g0011, g0011),
            //    Vec4f.Dot(g0111, g0111),
            //    Vec4f.Dot(g1011, g1011),
            //    Vec4f.Dot(g1111, g1111)));
            //g0011 *= norm11.x;
            //g0111 *= norm11.y;
            //g1011 *= norm11.z;
            //g1111 *= norm11.w;

            //float n0000 = Vec4f.Dot(g0000, Pf0);
            //float n1000 = Vec4f.Dot(g1000, new Vec4f(Pf1.x, Pf0.y, Pf0.z, Pf0.w));
            //float n0100 = Vec4f.Dot(g0100, new Vec4f(Pf0.x, Pf1.y, Pf0.z, Pf0.w));
            //float n1100 = Vec4f.Dot(g1100, new Vec4f(Pf1.x, Pf1.y, Pf0.z, Pf0.w));
            //float n0010 = Vec4f.Dot(g0010, new Vec4f(Pf0.x, Pf0.y, Pf1.z, Pf0.w));
            //float n1010 = Vec4f.Dot(g1010, new Vec4f(Pf1.x, Pf0.y, Pf1.z, Pf0.w));
            //float n0110 = Vec4f.Dot(g0110, new Vec4f(Pf0.x, Pf1.y, Pf1.z, Pf0.w));
            //float n1110 = Vec4f.Dot(g1110, new Vec4f(Pf1.x, Pf1.y, Pf1.z, Pf0.w));
            //float n0001 = Vec4f.Dot(g0001, new Vec4f(Pf0.x, Pf0.y, Pf0.z, Pf1.w));
            //float n1001 = Vec4f.Dot(g1001, new Vec4f(Pf1.x, Pf0.y, Pf0.z, Pf1.w));
            //float n0101 = Vec4f.Dot(g0101, new Vec4f(Pf0.x, Pf1.y, Pf0.z, Pf1.w));
            //float n1101 = Vec4f.Dot(g1101, new Vec4f(Pf1.x, Pf1.y, Pf0.z, Pf1.w));
            //float n0011 = Vec4f.Dot(g0011, new Vec4f(Pf0.x, Pf0.y, Pf1.z, Pf1.w));
            //float n1011 = Vec4f.Dot(g1011, new Vec4f(Pf1.x, Pf0.y, Pf1.z, Pf1.w));
            //float n0111 = Vec4f.Dot(g0111, new Vec4f(Pf0.x, Pf1.y, Pf1.z, Pf1.w));
            //float n1111 = Vec4f.Dot(g1111, Pf1);

            //Vec4f fade_xyzw = Fade(Pf0);
            //Vec4f n_0w = Vec4f.Mix(new Vec4f(n0000, n1000, n0100, n1100), new Vec4f(n0001, n1001, n0101, n1101), fade_xyzw.w);
            //Vec4f n_1w = Vec4f.Mix(new Vec4f(n0010, n1010, n0110, n1110), new Vec4f(n0011, n1011, n0111, n1111), fade_xyzw.w);
            //Vec4f n_zw = Vec4f.Mix(n_0w, n_1w, fade_xyzw.z);
            //Vec2f n_yzw = Vec2f.Mix(new Vec2f(n_zw.x, n_zw.y), new Vec2f(n_zw.z, n_zw.w), fade_xyzw.y);
            //float n_xyzw = MathF.Mix(n_yzw.x, n_yzw.y, fade_xyzw.x);

            //return 2.2f * n_xyzw;
        }

        /// <summary>
        /// Periodic Perlin Noise
        /// </summary>
        /// <param name="Position"></param>
        /// <param name="rep"></param>
        /// <returns></returns>
        public static float Perlin(Vec4f Position, Vec4f rep)
        {
            Vec4f Pi0 = Vec4f.Mod(Vec4f.Floor(Position), rep);   // Integer part for indexing, modulo rep.
            Vec4f Pi1 = Vec4f.Mod(Pi0 + 1.0f, rep);              // Integer part + 1, modulo rep.
            Pi0 = Vec4f.Mod(Pi0, 289.0f);
            Pi1 = Vec4f.Mod(Pi1, 289.0f);
            Vec4f Pf0 = Vec4f.Fract(Position);
            Vec4f Pf1 = Pf0 - 1.0f;

            return PerlinCommon4(Pi0, Pi1, Pf0, Pf1);
        }

        private static float PerlinCommon4(Vec4f Pi0, Vec4f Pi1, Vec4f Pf0, Vec4f Pf1)
        {
            Vec4f ix = new Vec4f(Pi0.x, Pi1.x, Pi0.x, Pi1.x);
            Vec4f iy = new Vec4f(Pi0.y, Pi0.y, Pi1.y, Pi1.y);
            Vec4f iz0 = new Vec4f(Pi0.z);
            Vec4f iz1 = new Vec4f(Pi1.z);
            Vec4f iw0 = new Vec4f(Pi0.w);
            Vec4f iw1 = new Vec4f(Pi1.w);

            Vec4f ixy = Permute(Permute(ix) + iy);
            Vec4f ixy0 = Permute(ixy + iz0);
            Vec4f ixy1 = Permute(ixy + iz1);
            Vec4f ixy00 = Permute(ixy0 + iw0);
            Vec4f ixy01 = Permute(ixy0 + iw1);
            Vec4f ixy10 = Permute(ixy1 + iw0);
            Vec4f ixy11 = Permute(ixy1 + iw1);

            Vec4f gx00 = ixy00 / 7.0f;
            Vec4f gy00 = Vec4f.Floor(gx00) / 7.0f;
            Vec4f gz00 = Vec4f.Floor(gy00) / 6.0f;
            gx00 = Vec4f.Fract(gx00) - 0.5f;
            gy00 = Vec4f.Fract(gy00) - 0.5f;
            gz00 = Vec4f.Fract(gz00) - 0.5f;
            Vec4f gw00 = new Vec4f(0.75f) - Vec4f.Abs(gx00) - Vec4f.Abs(gy00) - Vec4f.Abs(gz00);
            Vec4f sw00 = Vec4f.Step(gw00, Vec4f.Zero);
            gx00 -= sw00 * (Vec4f.Step(0.0f, gx00) - 0.5f);
            gy00 -= sw00 * (Vec4f.Step(0.0f, gy00) - 0.5f);

            Vec4f gx01 = ixy01 / 7.0f;
            Vec4f gy01 = Vec4f.Floor(gx01) / 7.0f;
            Vec4f gz01 = Vec4f.Floor(gy01) / 6.0f;
            gx01 = Vec4f.Fract(gx01) - 0.5f;
            gy01 = Vec4f.Fract(gy01) - 0.5f;
            gz01 = Vec4f.Fract(gz01) - 0.5f;
            Vec4f gw01 = new Vec4f(0.75f) - Vec4f.Abs(gx01) - Vec4f.Abs(gy01) - Vec4f.Abs(gz01);
            Vec4f sw01 = Vec4f.Step(gw01, Vec4f.Zero);
            gx01 -= sw01 * (Vec4f.Step(0.0f, gx01) - 0.5f);
            gy01 -= sw01 * (Vec4f.Step(0.0f, gy01) - 0.5f);

            Vec4f gx10 = ixy10 / 7.0f;
            Vec4f gy10 = Vec4f.Floor(gx10) / 7.0f;
            Vec4f gz10 = Vec4f.Floor(gy10) / 6.0f;
            gx10 = Vec4f.Fract(gx10) - 0.5f;
            gy10 = Vec4f.Fract(gy10) - 0.5f;
            gz10 = Vec4f.Fract(gz10) - 0.5f;
            Vec4f gw10 = new Vec4f(0.75f) - Vec4f.Abs(gx10) - Vec4f.Abs(gy10) - Vec4f.Abs(gz10);
            Vec4f sw10 = Vec4f.Step(gw10, Vec4f.Zero);
            gx10 -= sw10 * (Vec4f.Step(0.0f, gx10) - 0.5f);
            gy10 -= sw10 * (Vec4f.Step(0.0f, gy10) - 0.5f);

            Vec4f gx11 = ixy11 / 7.0f;
            Vec4f gy11 = Vec4f.Floor(gx11) / 7.0f;
            Vec4f gz11 = Vec4f.Floor(gy11) / 6.0f;
            gx11 = Vec4f.Fract(gx11) - 0.5f;
            gy11 = Vec4f.Fract(gy11) - 0.5f;
            gz11 = Vec4f.Fract(gz11) - 0.5f;
            Vec4f gw11 = new Vec4f(0.75f) - Vec4f.Abs(gx11) - Vec4f.Abs(gy11) - Vec4f.Abs(gz11);
            Vec4f sw11 = Vec4f.Step(gw11, Vec4f.Zero);
            gx11 -= sw11 * (Vec4f.Step(0.0f, gx11) - 0.5f);
            gy11 -= sw11 * (Vec4f.Step(0.0f, gy11) - 0.5f);

            Vec4f g0000 = new Vec4f(gx00.x, gy00.x, gz00.x, gw00.x);
            Vec4f g1000 = new Vec4f(gx00.y, gy00.y, gz00.y, gw00.y);
            Vec4f g0100 = new Vec4f(gx00.z, gy00.z, gz00.z, gw00.z);
            Vec4f g1100 = new Vec4f(gx00.w, gy00.w, gz00.w, gw00.w);
            Vec4f g0010 = new Vec4f(gx10.x, gy10.x, gz10.x, gw10.x);
            Vec4f g1010 = new Vec4f(gx10.y, gy10.y, gz10.y, gw10.y);
            Vec4f g0110 = new Vec4f(gx10.z, gy10.z, gz10.z, gw10.z);
            Vec4f g1110 = new Vec4f(gx10.w, gy10.w, gz10.w, gw10.w);
            Vec4f g0001 = new Vec4f(gx01.x, gy01.x, gz01.x, gw01.x);
            Vec4f g1001 = new Vec4f(gx01.y, gy01.y, gz01.y, gw01.y);
            Vec4f g0101 = new Vec4f(gx01.z, gy01.z, gz01.z, gw01.z);
            Vec4f g1101 = new Vec4f(gx01.w, gy01.w, gz01.w, gw01.w);
            Vec4f g0011 = new Vec4f(gx11.x, gy11.x, gz11.x, gw11.x);
            Vec4f g1011 = new Vec4f(gx11.y, gy11.y, gz11.y, gw11.y);
            Vec4f g0111 = new Vec4f(gx11.z, gy11.z, gz11.z, gw11.z);
            Vec4f g1111 = new Vec4f(gx11.w, gy11.w, gz11.w, gw11.w);

            Vec4f norm00 = TaylorInvSqrt(new Vec4f(
                Vec4f.Dot(g0000, g0000),
                Vec4f.Dot(g0100, g0100),
                Vec4f.Dot(g1000, g1000),
                Vec4f.Dot(g1100, g1100)));
            g0000 *= norm00.x;
            g0100 *= norm00.y;
            g1000 *= norm00.z;
            g1100 *= norm00.w;

            Vec4f norm01 = TaylorInvSqrt(new Vec4f(
                Vec4f.Dot(g0001, g0001),
                Vec4f.Dot(g0101, g0101),
                Vec4f.Dot(g1001, g1001),
                Vec4f.Dot(g1101, g1101)));
            g0001 *= norm01.x;
            g0101 *= norm01.y;
            g1001 *= norm01.z;
            g1101 *= norm01.w;

            Vec4f norm10 = TaylorInvSqrt(new Vec4f(
                Vec4f.Dot(g0010, g0010),
                Vec4f.Dot(g0110, g0110),
                Vec4f.Dot(g1010, g1010),
                Vec4f.Dot(g1110, g1110)));
            g0010 *= norm10.x;
            g0110 *= norm10.y;
            g1010 *= norm10.z;
            g1110 *= norm10.w;

            Vec4f norm11 = TaylorInvSqrt(new Vec4f(
                Vec4f.Dot(g0011, g0011),
                Vec4f.Dot(g0111, g0111),
                Vec4f.Dot(g1011, g1011),
                Vec4f.Dot(g1111, g1111)));
            g0011 *= norm11.x;
            g0111 *= norm11.y;
            g1011 *= norm11.z;
            g1111 *= norm11.w;

            float n0000 = Vec4f.Dot(g0000, Pf0);
            float n1000 = Vec4f.Dot(g1000, new Vec4f(Pf1.x, Pf0.y, Pf0.z, Pf0.w));
            float n0100 = Vec4f.Dot(g0100, new Vec4f(Pf0.x, Pf1.y, Pf0.z, Pf0.w));
            float n1100 = Vec4f.Dot(g1100, new Vec4f(Pf1.x, Pf1.y, Pf0.z, Pf0.w));
            float n0010 = Vec4f.Dot(g0010, new Vec4f(Pf0.x, Pf0.y, Pf1.z, Pf0.w));
            float n1010 = Vec4f.Dot(g1010, new Vec4f(Pf1.x, Pf0.y, Pf1.z, Pf0.w));
            float n0110 = Vec4f.Dot(g0110, new Vec4f(Pf0.x, Pf1.y, Pf1.z, Pf0.w));
            float n1110 = Vec4f.Dot(g1110, new Vec4f(Pf1.x, Pf1.y, Pf1.z, Pf0.w));
            float n0001 = Vec4f.Dot(g0001, new Vec4f(Pf0.x, Pf0.y, Pf0.z, Pf1.w));
            float n1001 = Vec4f.Dot(g1001, new Vec4f(Pf1.x, Pf0.y, Pf0.z, Pf1.w));
            float n0101 = Vec4f.Dot(g0101, new Vec4f(Pf0.x, Pf1.y, Pf0.z, Pf1.w));
            float n1101 = Vec4f.Dot(g1101, new Vec4f(Pf1.x, Pf1.y, Pf0.z, Pf1.w));
            float n0011 = Vec4f.Dot(g0011, new Vec4f(Pf0.x, Pf0.y, Pf1.z, Pf1.w));
            float n1011 = Vec4f.Dot(g1011, new Vec4f(Pf1.x, Pf0.y, Pf1.z, Pf1.w));
            float n0111 = Vec4f.Dot(g0111, new Vec4f(Pf0.x, Pf1.y, Pf1.z, Pf1.w));
            float n1111 = Vec4f.Dot(g1111, Pf1);

            Vec4f fade_xyzw = Fade(Pf0);
            Vec4f n_0w = Vec4f.Mix(new Vec4f(n0000, n1000, n0100, n1100), new Vec4f(n0001, n1001, n0101, n1101), fade_xyzw.w);
            Vec4f n_1w = Vec4f.Mix(new Vec4f(n0010, n1010, n0110, n1110), new Vec4f(n0011, n1011, n0111, n1111), fade_xyzw.w);
            Vec4f n_zw = Vec4f.Mix(n_0w, n_1w, fade_xyzw.z);
            Vec2f n_yzw = Vec2f.Mix(new Vec2f(n_zw.x, n_zw.y), new Vec2f(n_zw.z, n_zw.w), fade_xyzw.y);
            float n_xyzw = MathF.Mix(n_yzw.x, n_yzw.y, fade_xyzw.x);

            return 2.2f * n_xyzw;

        }




        #endregion
    }
}