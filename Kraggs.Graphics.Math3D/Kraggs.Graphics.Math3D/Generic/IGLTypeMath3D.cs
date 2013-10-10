using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;

//using System.IO;

namespace Kraggs.Graphics.Math3D
{
    /// <summary>
    /// A simple interface for getting the gl type description of a Math element.
    /// </summary>
    public interface IGLTypeMath3D
    {
        IGLDescriptionMath3D GetGLTypeDescription { get; }
    }
}
