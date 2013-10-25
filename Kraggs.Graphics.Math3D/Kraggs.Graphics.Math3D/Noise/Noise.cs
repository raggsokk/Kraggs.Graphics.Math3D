﻿using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Kraggs.Graphics.Math3D
{
    internal static class Noise
    {
        #region GLM Implementation Code

        //[DebuggerNonUserCode()]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //internal static float Mod289(float x)
        //{
        //    return x - MathF.Floor(x * 1.0f / 289.0f) * 289.0f;
        //}

        //[DebuggerNonUserCode()]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //internal static float Permute(float x)
        //{
        //    return Mod289(((x * 34.0f) + 1.0f) * x);
        //}

        //[DebuggerNonUserCode()]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //internal static Vec2f Permute(Vec2f x)
        //{
        //    throw new NotImplementedException();
        //    //return Mod289(((x * 34.0f)) + 1.0f) * x);
        //}

        //[DebuggerNonUserCode()]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //internal static float TaylorInvSqrt(float r)
        //{
        //    return 1.79284291400159f - 0.85373472095314f * r;
        //}

        //[DebuggerNonUserCode()]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //internal static Vec2f TaylorInvSqrt(Vec2f r)
        //{
        //    return 1.79284291400159f - 0.85373472095314f * r;
        //}
        //[DebuggerNonUserCode()]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //internal static Vec3f TaylorInvSqrt(Vec3f r)
        //{
        //    return 1.79284291400159f - 0.85373472095314f * r;
        //}
        //[DebuggerNonUserCode()]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //internal static Vec4f TaylorInvSqrt(Vec4f r)
        //{
        //    //Vec4f result;
        //    //Vec4f.Multiply(ref r, 0.85373472095314f, out result);
        //    //return 1.79284291400159f - result;
        //    return 1.79284291400159f - 0.85373472095314f * r;
        //}
        //[DebuggerNonUserCode()]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //internal static Vec2f Fade(Vec2f t)
        //{
        //    return (t * t * t) * (t * (t * 6.0f - 15.0f + 10.0f));
        //}
        //[DebuggerNonUserCode()]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //internal static Vec3f Fade(Vec3f t)
        //{
        //    return (t * t * t) * (t * (t * 6.0f - 15.0f + 10.0f));
        //}
        //[DebuggerNonUserCode()]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //internal static Vec4f Fade(Vec4f t)
        //{
        //    return (t * t * t) * (t * (t * 6.0f - 15.0f + 10.0f));
        //}

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

        #endregion

        #region SimplexNoise.java

        /*
         * A speed-improved simplex noise algorithm for 2D, 3D and 4D in Java.
         *
         * Based on example code by Stefan Gustavson (stegu@itn.liu.se).
         * Optimisations by Peter Eastman (peastman@drizzle.stanford.edu).
         * Better rank ordering method by Stefan Gustavson in 2012.
         * Ported to C# by Jarle Hansen (jarle.hansen@gmail.com)
         *
         * This could be speeded up even further, but it's useful as it is.
         *
         * Version 2012-03-09
         * (C# Version 2013-10-25)
         *
         * This code was placed in the public domain by its original author,
         * Stefan Gustavson. You may use it as you see fit, but
         * attribution is appreciated.         
         */
        private static readonly Vec3f[] grad3 = new Vec3f[]
        {            
            new Vec3f(1,1,0), new Vec3f(-1, 1, 0), new Vec3f(1,-1,0), new Vec3f(-1, -1,0),
            new Vec3f(1, 0, 1), new Vec3f(-1, 0, 1), new Vec3f(1, 0, -1), new Vec3f(-1, 0, -1),
            new Vec3f(0, 1, 1), new Vec3f(0, -1, 1), new Vec3f(0, 1, -1), new Vec3f(0, -1, -1),
        };

        private static readonly Vec4f[] grad4 = new Vec4f[]
        {            
            new Vec4f(0,1,1,1), new Vec4f(0,1,1,-1), new Vec4f(0,1,-1,1), new Vec4f(0, 1, -1,-1),
            new Vec4f(1,0,1,1), new Vec4f(1,0,1,-1), new Vec4f(0,-1,-1,1), new Vec4f(0,-1,-1,-1),
            new Vec4f(1,0,1,1), new Vec4f(1,0,1,-1), new Vec4f(1,0,-1,1), new Vec4f(1,0, -1,-1),
            new Vec4f(-1,0,1,1),new Vec4f(-1,0,1,-1),new Vec4f(-1,0,-1,1),new Vec4f(-1,0,-1,-1),
            new Vec4f(1,1,0,1),new Vec4f(1,1,0,-1),new Vec4f(1,-1,0,1),new Vec4f(1,-1,0,-1),
            new Vec4f(-1,1,0,1),new Vec4f(-1,1,0,-1),new Vec4f(-1,-1,0,1),new Vec4f(-1,-1,0,-1),
            new Vec4f(1,1,1,0),new Vec4f(1,1,-1,0),new Vec4f(1,-1,1,0),new Vec4f(1,-1,-1,0),
            new Vec4f(-1,1,1,0),new Vec4f(-1,1,-1,0),new Vec4f(-1,-1,1,0),new Vec4f(-1,-1,-1,0)
        };

        private static byte[] perm = new byte[]{
            151,160,137,91,90,15,
            131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,142,8,99,37,240,21,10,23,
            190, 6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,35,11,32,57,177,33,
            88,237,149,56,87,174,20,125,136,171,168, 68,175,74,165,71,134,139,48,27,166,
            77,146,158,231,83,111,229,122,60,211,133,230,220,105,92,41,55,46,245,40,244,
            102,143,54, 65,25,63,161, 1,216,80,73,209,76,132,187,208, 89,18,169,200,196,
            135,130,116,188,159,86,164,100,109,198,173,186, 3,64,52,217,226,250,124,123,
            5,202,38,147,118,126,255,82,85,212,207,206,59,227,47,16,58,17,182,189,28,42,
            223,183,170,213,119,248,152, 2,44,154,163, 70,221,153,101,155,167, 43,172,9,
            129,22,39,253, 19,98,108,110,79,113,224,232,178,185, 112,104,218,246,97,228,
            251,34,242,193,238,210,144,12,191,179,162,241, 81,51,145,235,249,14,239,107,
            49,192,214, 31,181,199,106,157,184, 84,204,176,115,121,50,45,127, 4,150,254,
            138,236,205,93,222,114,67,29,24,72,243,141,128,195,78,66,215,61,156,180,
            151,160,137,91,90,15,
            131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,142,8,99,37,240,21,10,23,
            190, 6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,35,11,32,57,177,33,
            88,237,149,56,87,174,20,125,136,171,168, 68,175,74,165,71,134,139,48,27,166,
            77,146,158,231,83,111,229,122,60,211,133,230,220,105,92,41,55,46,245,40,244,
            102,143,54, 65,25,63,161, 1,216,80,73,209,76,132,187,208, 89,18,169,200,196,
            135,130,116,188,159,86,164,100,109,198,173,186, 3,64,52,217,226,250,124,123,
            5,202,38,147,118,126,255,82,85,212,207,206,59,227,47,16,58,17,182,189,28,42,
            223,183,170,213,119,248,152, 2,44,154,163, 70,221,153,101,155,167, 43,172,9,
            129,22,39,253, 19,98,108,110,79,113,224,232,178,185, 112,104,218,246,97,228,
            251,34,242,193,238,210,144,12,191,179,162,241, 81,51,145,235,249,14,239,107,
            49,192,214, 31,181,199,106,157,184, 84,204,176,115,121,50,45,127, 4,150,254,
            138,236,205,93,222,114,67,29,24,72,243,141,128,195,78,66,215,61,156,180
        };

        // Simple skewing factors for the 2D case
        private const float F2 = 0.366025403f; // F2 = 0.5*(sqrt(3.0)-1.0)
        private const float G2 = 0.211324865f; // G2 = (3.0-Math.sqrt(3.0))/6.0
        // Simple skewing factors for the 3D case
        private const float F3 = 0.333333333f;
        private const float G3 = 0.166666667f;
        // The skewing and unskewing factors are hairy again for the 4D case
        private const float F4 = 0.309016994f; // F4 = (Math.sqrt(5.0)-1.0)/4.0
        private const float G4 = 0.138196601f;// G4 = (5.0-Math.sqrt(5.0))/20.0

        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int FastFloori(float x)
        {
            int xi = (int)x;
            return x < xi ? xi -1: xi;
        }
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float Dot(ref Vec3f g, float x, float y)
        {
            return g.x * x + g.y * y;
        }
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float Dot(ref Vec3f g, float x, float y, float z)
        {
            return g.x * x + g.y * y + g.z * z;
        }
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float Dot(ref Vec4f g, float x, float y, float z, float w)
        {
            return g.x * x + g.y * y + g.z * z + g.w * w;
        }

        /// <summary>
        /// Returns Simplex Noise in range [-1,1]
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static float Simplex(Vec2f v)
        {
            float n0, n1, n2; // Noise contributions from the three corners
            // Skew the input space to determine which simplex cell we're in
            float s = (v.x + v.y) * F2;  // Hairy factor for 2D
            int i = FastFloori(v.x + s);
            int j = FastFloori(v.y + s);

            float t = (i + j) * G2;
            float X0 = i - t; // Unskew the cell origin back to (x,y) space
            float Y0 = j - t; 
            float x0 = v.x - X0; // The x,y distances from the cell origin
            float y0 = v.y - Y0;
            // For the 2D case, the simplex shape is an equilateral triangle.
            // Determine which simplex we are in.
            int i1, j1; // Offsets for second (middle) corner of simplex in (i,j) coords
            if(x0>y0) { i1 = 1; j1 = 0;}    // lower triangle, XY order: (0,0)->(1,0)->(1,1)
            else {i1 = 0; j1 = 1;}          // upper triangle, YX order: (0,0)->(0,1)->(1,1)

            // A step of (1,0) in (i,j) means a step of (1-c,-c) in (x,y), and
            // a step of (0,1) in (i,j) means a step of (-c,1-c) in (x,y), where
            // c = (3-sqrt(3))/6
            float x1 = x0 - i1 + G2;            // Offsets for middle corner in (x,y) unskewed coords
            float y1 = y0 - j1 + G2;
            float x2 = x0 - 1.0f + 2.0f * G2;   // Offsets for last corner in (x,y) unskewed coords
            float y2 = y0 - 1.0f + 2.0f * G2;

            // Work out the hashed gradient indices of the three simplex corners
            int ii = i & 255;
            int jj = j & 255;

            //TODO: Why are these precomputed when the could easily been inside else blocks below?
            int gi0 = perm[ii + perm[jj]];
            int gi1 = perm[ii + i1 + perm[jj + j1]];
            int gi2 = perm[ii + 1 + perm[jj + 1]];

            // Calculate the contribution from the three corners
            float t0 = 0.5f - x0 * x0 - y0 * y0;
            if(t0<0)
                n0 = 0.0f;
            else
            {
                t0 *= t0;
                n0 = t0 * t0 * Dot(ref grad3[gi0], x0, y0);
            }
            float t1 = 0.5f - x1*x1 - y1*y1;
            if(t1<0)
                n1 = 0.0f;
            else
            {
                t1 *= t1;
                n1 = t1*t1* Dot(ref grad3[gi1], x1, y1);
            }
            float t2 = 0.5f - x2*x2 - y2* y2;
            if(t2<0)
                n2 = 0.0f;
            else
            {
                t2 *= t2;
                n2 = t2 * t2* Dot(ref grad3[gi2], x2, y2);
            }

            // Add contributions from each corner to get the final noise value.
            // The result is scaled to return values in the interval [-1,1].
            return 70.0f * (n0 + n1 + n2);
        }

        public static float Simplex(Vec3f v)
        {
            // Noise contributions from the four corners
            float n0, n1, n2, n3;
            // Skew the input space to determine which simplex cell we're in
            float s = (v.x + v.y + v.z) * F3; // Very nice and simple skew factor for 3D
            int i = FastFloori(v.x + s);
            int j = FastFloori(v.y + s);
            int k = FastFloori(v.z + s);

            float t = (i + j + k) * G3;
            // Unskew the cell origin back to (x,y,z) space
            float X0 = i - t;
            float Y0 = j - t;
            float Z0 = k - t;
            // The x,y,z distances from the cell origin
            float x0 = v.x - X0;
            float y0 = v.y - Y0;
            float z0 = v.z - Z0;
            // For the 3D case, the simplex shape is a slightly irregular tetrahedron.
            // Determine which simplex we are in.
            int i1, j1, k1; // Offsets for second corner of simplex in (i,j,k) coords
            int i2, j2, k2; // Offsets for third corner of simplex in (i,j,k) coords
            if(x0 >= y0)
            {
                if(y0 >= z0)
                { i1 = 1; j1 = 0;k1 = 0; i2 = 1; j2 = 1; k2 = 0; }// X Y Z order 
                else if(x0 >= z0)
                { i1 = 1; j1 = 0;k1 = 0;i2 = 1; j2 = 0;k2 = 1;} // X Z Y order
                else
                { i1 = 0; j1 = 0; k1 = 1; i2 = 1; j2 = 0; k2 = 1;} // Z X Y order                    
            }
            else // x0<y0
            {
                if(y0<z0)
                {i1 = 0; j1 = 0; k1 = 1; i2 = 0; j2 = 1;k2 = 1;} // Z Y X order
                else if(x0 < z0)
                {i1 = 0; j1 = 1; k1 = 0; i2 = 0; j2 = 1; k2 = 1;} // Y Z X order
                else
                {i1 = 0; j1 = 1;k1 = 0;i2 = 1; j2 = 1; k2 = 0;} // Y X Z order
            }
            // A step of (1,0,0) in (i,j,k) means a step of (1-c,-c,-c) in (x,y,z),
            // a step of (0,1,0) in (i,j,k) means a step of (-c,1-c,-c) in (x,y,z), and
            // a step of (0,0,1) in (i,j,k) means a step of (-c,-c,1-c) in (x,y,z), where
            // c = 1/6.
            // Offsets for second corner in (x,y,z) coords
            float x1 = x0 - i1 + G3;
            float y1 = y0 - j1 + G3;
            float z1 = z0 - k1 + G3;
            // Offsets for third corner in (x,y,z) coords
            float x2 = x0 - i2 + 2.0f * G3;
            float y2 = y0 - j2 + 2.0f * G3;
            float z2 = z0 - k2 + 2.0f * G3;
            // Offsets for last corner in (x,y,z) coords
            float x3 = x0 - 1.0f + 3.0f * G3;
            float y3 = y0 - 1.0f + 3.0f * G3;
            float z3 = z0 - 1.0f + 3.0f * G3;
            // Work out the hashed gradient indices of the four simplex corners
            int ii = i & 255;
            int jj = j & 255;
            int kk = k & 255;

            //int gi0 = perm[ii + perm[jj]];
            int gi0 = perm[ii + perm[jj + perm[kk]]];
            int gi1 = perm[ii + i1 + perm[jj + j1 + perm[kk + k1]]];
            int gi2 = perm[ii + i2 + perm[jj + j2 + perm[kk + k2]]];
            int gi3 = perm[ii + 1 + perm[jj + 1 + perm[kk + 1]]];

            // Calculate the contribution from the four corners            
            //NOTE: Should be 0.5, not 0.6, else the noise is not continuous at simplex boundaries. Same for 4D case.
            //NOTE: see page 13 at http://webstaff.itn.liu.se/~stegu/simplexnoise/simplexnoise.pdf
            float t0 = 0.5f - x0 * x0 - y0 * y0 - z0 * z0;
            if(t0<0)
                n0 = 0.0f;
            else
            {
                t0 *= t0;
                n0 = t0*t0* Dot(ref grad3[gi0], x0, y0, z0);
            }

            float t1 = 0.5f - x1 * x1 - y1 * y1 - z1 * z1;
            if(t0<0)
                n1 = 0.0f;
            else
            {
                t1 *= t1;
                n1 = t1*t1* Dot(ref grad3[gi1], x1, y1, z1);
            }

            float t2 = 0.5f - x2 * x2 - y2 * y2 - z2 * z2;
            if(t2<0)
                n2 = 0.0f;
            else
            {
                t2 *= t2;
                n2 = t2*t2* Dot(ref grad3[gi2], x2, y2, z2);
            }

            float t3 = 0.5f - x3 * x3 - y3 * y3 - z3 * z3;
            if(t3<0)
                n3 = 0.0f;
            else
            {
                t3 *= t3;
                n3 = t3*t3* Dot(ref grad3[gi3], x3, y3, z3);
            }

            // Add contributions from each corner to get the final noise value.
            // The result is scaled to stay just inside [-1,1]
            return 32.0f * (n0 + n1 + n2 + n3);
        }

        /// <summary>
        /// 4D simplex noise, better simplex rank ordering method 2012-03-09
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static float Simplex(Vec4f v)
        {
            // Noise contributions from the five corners
            float n0, n1, n2, n3, n4;
            // Skew the (x,y,z,w) space to determine which cell of 24 simplices we're in
            float s = (v.x + v.y + v.z + v.w) * F4; // Factor for 4D skewing
            int i = FastFloori(v.x + s);
            int j = FastFloori(v.y + s);
            int k = FastFloori(v.z + s);
            int l = FastFloori(v.w + s);
            // Factor for 4D unskewing
            float t = (i + j + k + l) * G4;
            // Unskew the cell origin back to (x,y,z,w) space
            float X0 = i - t;
            float Y0 = j - t;
            float Z0 = k - t;
            float W0 = l - t;
            // The x,y,z,w distances from the cell origin
            float x0 = v.x - X0;
            float y0 = v.y - Y0;
            float z0 = v.z - Z0;
            float w0 = v.w - W0;
            // For the 4D case, the simplex is a 4D shape I won't even try to describe.
            // To find out which of the 24 possible simplices we're in, we need to
            // determine the magnitude ordering of x0, y0, z0 and w0.
            // Six pair-wise comparisons are performed between each possible pair
            // of the four coordinates, and the results are used to rank the numbers.
            int rankx = 0;
            int ranky = 0;
            int rankz = 0;
            int rankw = 0;
            if(x0 > y0) rankx++; else ranky++;
            if(x0 > z0) rankx++; else rankz++;
            if(x0 > w0) rankz++; else rankw++;
            if(y0 > z0) ranky++; else rankz++;
            if(y0 > w0) ranky++; else rankw++;
            if(z0 > w0) rankz++; else rankw++;
            int i1, j1, k1, l1; // The integer offsets for the second simplex corner
            int i2, j2, k2, l2; // The integer offsets for the third simplex corner
            int i3, j3, k3, l3; // The integer offsets for the fourth simplex corner
            // simplex[c] is a 4-vector with the numbers 0, 1, 2 and 3 in some order.
            // Many values of c will never occur, since e.g. x>y>z>w makes x<z, y<w and x<w
            // impossible. Only the 24 indices which have non-zero entries make any sense.
            // We use a thresholding to set the coordinates in turn from the largest magnitude.
            // Rank 3 denotes the largest coordinate.
            i1 = rankx >= 3 ? 1: 0;
            j1 = ranky >= 3 ? 1: 0;
            k1 = rankz >= 3 ? 1: 0;
            l1 = rankw >= 3 ? 1: 0;
            // Rank 2 denotes the second largest coordinate.
            i2 = rankx >= 2 ? 1: 0;
            j2 = ranky >= 2 ? 1: 0;
            k2 = rankz >= 2 ? 1: 0;
            l2 = rankw >= 2 ? 1: 0;
            // Rank 1 denotes the second smallest coordinate.
            i3 = rankx >= 1 ? 1: 0;
            j3 = ranky >= 1 ? 1: 0;
            k3 = rankz >= 1 ? 1: 0;
            l3 = rankw >= 1 ? 1: 0;
            // The fifth corner has all coordinate offsets = 1, so no need to compute that.
            // Offsets for second corner in (x,y,z,w) coords
            float x1 = x0 - i1 + G4;
            float y1 = y0 - j1 + G4;
            float z1 = z0 - k1 + G4;
            float w1 = w0 - l1 + G4;
            // Offsets for third corner in (x,y,z,w) coords
            float x2 = x0 - i2 + 2.0f * G4;
            float y2 = y0 - j2 + 2.0f * G4;
            float z2 = z0 - k2 + 2.0f * G4;
            float w2 = w0 - l2 + 2.0f * G4;
            // Offsets for fourth corner in (x,y,z,w) coords
            float x3 = x0 - i3 + 3.0f * G4;
            float y3 = y0 - i3 + 3.0f * G4;
            float z3 = z0 - i3 + 3.0f * G4;
            float w3 = w0 - i3 + 3.0f * G4;
            // Offsets for last corner in (x,y,z,w) coords
            float x4 = x0 - 1.0f + 4.0f * G4;
            float y4 = y0 - 1.0f + 4.0f * G4;
            float z4 = z0 - 1.0f + 4.0f * G4;
            float w4 = w0 - 1.0f + 4.0f * G4;
            // Work out the hashed gradient indices of the five simplex corners
            int ii = i & 255;
            int jj = j & 255;
            int kk = k & 255;
            int ll = l & 255;
            int gi0 = perm[ii + perm[jj + perm[kk + perm[ll]]]];
            int gi1 = perm[ii + i1 + perm[jj + j1 + perm[kk + k1 + perm[ll + l1]]]];
            int gi2 = perm[ii + i2 + perm[jj + j2 + perm[kk + k2 + perm[ll + l2]]]];
            int gi3 = perm[ii + i3 + perm[jj + j3 + perm[kk + k3 + perm[ll + l3]]]];
            int gi4 = perm[ii + 1 + perm[jj + 1 + perm[kk + 1 + perm[ll + 1]]]];
            // Calculate the contribution from the five corners
            float t0 = 0.5f - x0*x0 - y0*y0 - z0*z0 - w0*w0;
            if(t0<0)
                n0 = 0.0f;
            else
            {
                t0 *= t0;
                n0 = t0 * t0 * Dot(ref grad4[gi0], x0, y0, z0, w0);
            }

            float t1 = 0.5f - x1*x1 - y1*y1 - z1*z1 - w1*w1;
            if(t1<0)
                n1 = 0.0f;
            else
            {
                t1 *= t1;
                n1 = t1 * t1 * Dot(ref grad4[gi1], x1, y1, z1, w1);
            }

            float t2 = 0.5f - x2*x2 - y2*y2 - z2*z2 - w2*w2;
            if(t2<0)
                n2 = 0.0f;
            else
            {
                t2 *= t2;
                n2 = t2 * t2 * Dot(ref grad4[gi2], x2, y2, z2, w2);
            }

            float t3 = 0.5f - x3*x3 - y3*y3 - z3*z3 - w3*w3;
            if(t3<0)
                n3 = 0.0f;
            else
            {
                t3 *= t3;
                n3 = t3 * t3 * Dot(ref grad4[gi3], x3, y3, z3, w3);
            }

            float t4 = 0.5f - x4*x4 - y4*y4 - z4*z4 - w4*w4;
            if(t4<0)
                n4 = 0.0f;
            else
            {
                t4 *= t4;
                n4 = t4 * t4 * Dot(ref grad4[gi4], x4, y4, z4, w4);
            }

            // Sum up and scale the result to cover the range [-1,1]
            return 27.0f * (n0 + n1 + n2 + n3 + n4);
        }

        #endregion
    }
}
