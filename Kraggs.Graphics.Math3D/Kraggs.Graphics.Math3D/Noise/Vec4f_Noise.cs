using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Kraggs.Graphics.Math3D
{
    partial struct Vec4f
    {
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vec4b LessThan(Vec4f l, Vec4f r)
        {
            return new Vec4b()
            {
                x = l.x < r.x,
                y = l.y < r.y,
                z = l.z < r.z
            };
        }
    }

    internal struct Vec4b
    {
        public bool x;
        public bool y;
        public bool z;
        public bool w;

        internal static explicit operator Vec4f(Vec4b v)
        {

        }
    }
}
