using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;

//using System.IO;

namespace Kraggs.Graphics.Math3D
{
    /// <summary>
    /// An interface describing the gl attributes of an math element.
    /// NOTE: this should be implemented in an empty struct or class,
    /// not directly on the element.
    /// </summary>
    public interface IGLDescriptionMath3D
    {
        /// <summary>
        /// The .net type of element.
        /// </summary>
        Type BaseType { get; }
        /// <summary>
        /// The number of components in element.
        /// </summary>
        int ComponentCount { get; } // columns * rows.
        /// <summary>
        /// The size in bytes of element.
        /// </summary>
        int SizeInBytes { get; } // sizeof(BaseType) * ComponentCount

        /// <summary>
        /// Returns the opengl base enum type value.
        /// </summary>
        int GLBaseType { get; }
        /// <summary>
        /// Returns the opengl attribute type enum value.
        /// </summary>
        int GLAttributeType { get; }
        /// <summary>
        /// Returns the opengl uniform type enum value.
        /// </summary>
        int GLUniformType { get; }

        // matrix properties.
        /// <summary>
        /// Is this a matrix type.
        /// </summary>
        bool IsMatrix { get; }
        /// <summary>
        /// Is this a row major matrix/vector
        /// </summary>
        bool IsRowMajor { get; }
        /// <summary>
        /// The number of columns in element.
        /// </summary>
        int Columns { get; }
        /// <summary>
        /// The number of rows in element.
        /// </summary>
        int Rows { get; }

    }
}
