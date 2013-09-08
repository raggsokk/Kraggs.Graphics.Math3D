using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Kraggs.Graphics.Math3D
{
    /// <summary>
    /// An interface describing our math elements for generic opengl functions.
    /// Might find another better name thou.
    /// </summary>
    public interface IGLMath
    {
        /// <summary>
        /// Returns the dotnet type of this math elements component.
        /// </summary>
        Type BaseType { get; }
        /// <summary>
        /// The number of components in this math element.
        /// </summary>
        int ComponentCount { get; }
        /// <summary>
        /// Total size in bytes of this math element.
        /// </summary>
        int SizeInBytes { get; }

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

        /// <summary>
        /// Returns true if this math element is a matrix.
        /// If true Cast to IGLMatrix for more matrix info.
        /// </summary>
        bool IsMatrix { get; }
               
    }
}
