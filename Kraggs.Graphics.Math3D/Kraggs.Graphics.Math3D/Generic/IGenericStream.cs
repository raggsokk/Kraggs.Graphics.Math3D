using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Kraggs.Graphics.Math3D
{
    /// <summary>
    /// Generic Stream Functions for reading / writing a element.
    /// </summary>
    //[Obsolete("Use functions in IBinaryStreamMath3D instead")]
    public interface IGenericStream
    {
        /// <summary>
        /// Writes vec to binary writer.
        /// </summary>
        /// <param name="writer"></param>
        void WriteStream(System.IO.BinaryWriter writer, object vec);

        /// <summary>
        /// Reads vec from binary reader.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        object ReadStream(System.IO.BinaryReader reader);

    }
}
