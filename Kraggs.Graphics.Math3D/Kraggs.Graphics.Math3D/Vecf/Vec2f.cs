using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Kraggs.Graphics.Math3D
{
    /// <summary>
    /// A floating point 2D vector.
    /// </summary>
    [DebuggerDisplay("[ {x}, {y} ]")]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct Vec2f
    {
        /// <summary>
        /// The x component.
        /// </summary>
        public float x;
        /// <summary>
        /// The y component.
        /// </summary>
        public float y;

        #region Properties

        /// <summary>
        /// A Vec2f with all components zero.
        /// </summary>
        public static readonly Vec2f Zero = new Vec2f() { x = 0.0f, y = 0.0f };
        /// <summary>
        /// A Vec2f with X component 1 and all others zero.
        /// </summary>
        public static readonly Vec2f UnitX = new Vec2f() { x = 1.0f, y = 0.0f };
        /// <summary>
        /// A Vec3f with Y component 1 and all others zero.
        /// </summary>
        public static readonly Vec2f UnitY = new Vec2f() { x = 0.0f, y = 1.0f };

        /// <summary>
        /// Returns the unsqrt length of the vector.
        /// </summary>
        [DebuggerNonUserCode()]
        public float LengthSquared
        {
            get
            {
                return x * x + y * y;
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
                return MathFunctions.Sqrt(x * x + y * y);
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
            var f = 1.0f / MathFunctions.Sqrt(x * x + y * y);

            this.x = x * f;
            this.y = y * f;            
        }

        /// <summary>
        /// Returns a normalized version of this vector.
        /// </summary>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vec2f GetNormal()
        {
            var f = 1.0f / MathFunctions.Sqrt(x * x + y * y);

            return new Vec2f() { x = x * f, y = y * f};
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
        public static float Distance(Vec2f left, Vec2f right)
        {
            var t = new Vec2f() { x = left.x - right.x, y = left.y - right.y };
            return t.Length;
        }

        #endregion

    }
}
