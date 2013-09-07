using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Kraggs.Graphics.Math3D
{
    /// <summary>
    /// A floating-point 3d vector.
    /// </summary>
    [DebuggerDisplay("[ {x}, {y}, {z} ]")]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct Vec3f
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

        #region Properties

        /// <summary>
        /// A Vec3f with all components set to zero.
        /// </summary>
        public static readonly Vec3f Zero = new Vec3f() { x = 0.0f, y = 0.0f, z = 0.0f };
        /// <summary>
        /// A Vec3f with X component 1 and all others zero.
        /// </summary>
        public static readonly Vec3f UnitX = new Vec3f() { x = 1.0f, y = 0.0f, z = 0.0f };
        /// <summary>
        /// A Vec3f with Y component 1 and all others zero.
        /// </summary>
        public static readonly Vec3f UnitY = new Vec3f() { x = 0.0f, y = 1.0f, z = 0.0f };
        /// <summary>
        /// A Vec3f with Z component 1 and all others zero.
        /// </summary>
        public static readonly Vec3f UnitZ = new Vec3f() { x = 0.0f, y = 0.0f, z = 1.0f };

        /// <summary>
        /// Returns the unsqrt length of the vector.
        /// </summary>
        [DebuggerNonUserCode()]
        public float LengthSquared
        {
            get
            {
                return x * x + y * y + z * z;
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
                return MathFunctions.Sqrt(x * x + y * y + z * z);
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
            var f = 1.0f / MathFunctions.Sqrt(x * x + y * y + z * z);

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
        public Vec3f GetNormal()
        {
            var f = 1.0f / MathFunctions.Sqrt(x * x + y * y + z * z);

            return new Vec3f() { x = x * f, y = y * f, z = z * f };
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
        public static float Distance(Vec3f left, Vec3f right)
        {
            var t = new Vec3f() { x = left.x - right.x, y = left.y - right.y, z = left.z - right.z };
            return t.Length;
        }

        #endregion

    }
}
