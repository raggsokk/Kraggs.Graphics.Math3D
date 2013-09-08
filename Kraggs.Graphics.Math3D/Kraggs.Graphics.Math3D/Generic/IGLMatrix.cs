using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kraggs.Graphics.Math3D
{
    /// <summary>
    /// An interface describing our matrices for generic opengl functions.
    /// Might find another better name thou.
    /// This properties might even be included in IGLMath since a vector
    /// is also a 3x1 matrix.
    /// </summary>
    public interface IGLMatrix : IGLMath
    {
        /// <summary>
        /// Number of columns in this matrix.
        /// </summary>
        int ColumnCount { get; }
        /// <summary>
        /// Number of rows in this matrix.
        /// </summary>
        int RowCount { get; }

        /// <summary>
        /// Is this a square matrix aka ColumnCount == RowCount?
        /// </summary>
        bool IsSquareMatrix { get; }
    }
}
