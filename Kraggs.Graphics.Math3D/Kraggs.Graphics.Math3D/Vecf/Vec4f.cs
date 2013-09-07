using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Kraggs.Graphics.Math3D
{
    /// <summary>
    /// A floating-point 4d vector.
    /// </summary>
    [DebuggerDisplay("[ {x}, {y}, {z}, {w} ]")]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct Vec4f
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

        #region Properties

        /// <summary>
        /// A Vec4f with all components set to zero.
        /// </summary>
        public static readonly Vec4f Zero = new Vec4f() { x = 0.0f, y = 0.0f, z = 0.0f, w = 0.0f };
        /// <summary>
        /// A Vec4f with X component 1 and all others zero.
        /// </summary>
        public static readonly Vec4f UnitX = new Vec4f() { x = 1.0f, y = 0.0f, z = 0.0f, w = 0.0f };
        /// <summary>
        /// A Vec4f with Y component 1 and all others zero.
        /// </summary>
        public static readonly Vec4f UnitY = new Vec4f() { x = 0.0f, y = 1.0f, z = 0.0f, w = 0.0f };
        /// <summary>
        /// A Vec4f with Z component 1 and all others zero.
        /// </summary>
        public static readonly Vec4f UnitZ = new Vec4f() { x = 0.0f, y = 0.0f, z = 1.0f, w = 0.0f };
        /// <summary>
        /// A Vec4f with W component 1 and all others zero.
        /// </summary>
        public static readonly Vec4f UnitW = new Vec4f() { x = 0.0f, y = 0.0f, z = 0.0f, w = 1.0f };

        /// <summary>
        /// Returns the unsqrt length of the vector.
        /// </summary>
        [DebuggerNonUserCode()]
        public float LengthSquared
        {
            get
            {
                return x * x + y * y + z * z + w * w;
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
                return MathFunctions.Sqrt(x * x + y * y + z * z + w * w);
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
            var f = 1.0f / MathFunctions.Sqrt(x * x + y * y + z * z + w * w);

            this.x = x * f;
            this.y = y * f;
            this.z = z * f;
        }

        /// <summary>
        /// Returns a normalized version of this vector.
        /// </summary>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vec4f GetNormal()
        {
            var f = 1.0f / MathFunctions.Sqrt(x * x + y * y + z * z + w * w);

            return new Vec4f() { x = x * f, y = y * f, z = z * f, w = w * f };
        }

        #endregion

        #region Static Functions

        /// <summary>
        /// Returns the distance between 2 vectors.
        /// </summary>
        /// <param name="left">left side</param>
        /// <param name="right">right side.</param>
        /// <returns>distance</returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(Vec4f left, Vec4f right)
        {
            var t = new Vec4f() { x = left.x - right.x, y = left.y - right.y, z = left.z - right.z, w = left.w - right.w };
            return t.Length;
        }

        #endregion

    }
}
