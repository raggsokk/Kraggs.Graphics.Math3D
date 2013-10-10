using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using System.IO;

namespace Kraggs.Graphics.Math3D
{
    /// <summary>
    /// Generic Stream Functions for reading / writing an array of elements.
    /// This new interface compared to IGenericStream delegates the reading loop 
    /// to the indiviual implementer and therefore opens up for faster impls.
    /// </summary>
    public interface IBinaryStreamMath3D<TMath3D> //where TMath : new()
    {
        /// <summary>
        /// Writes a number of elements to a stream.
        /// </summary>
        /// <param name="writer">writer to write to.</param>
        /// <param name="elements">Array of elements to write.</param>
        /// <param name="index">start index to write at.</param>
        /// <param name="length">number of elements to write.</param>
        /// <returns></returns>
        void WriteStream(BinaryWriter writer, TMath3D[] elements, int index, int length); //where TMath : IGenericStream2, new();

        /// <summary>
        /// Reads a number of elements to a stream.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="elements"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        int ReadStream(BinaryReader reader, TMath3D[] elements, int index, int length); //where TMath : IGenericStream2, new();

        /// <summary>
        /// Write a single tmath element.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="element"></param>
        void WriteStream(BinaryWriter writer, TMath3D element);

        /// <summary>
        /// Read a single tmath element.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        TMath3D ReadStream(BinaryReader reader);

        ///// <summary>
        ///// Writes a number of TMath elements directly to a stream.
        ///// </summary>
        ///// <param name="writer"></param>
        ///// <param name="elements"></param>
        ///// <param name="index"></param>
        ///// <param name="length"></param>
        //void WriteStream(Stream writer, TMath3D[] elements, int index, int length); 

        ///// <summary>
        ///// Reads a number of TMath elements directly from a stream.
        ///// </summary>
        ///// <param name="reader"></param>
        ///// <param name="elements"></param>
        ///// <param name="index"></param>
        ///// <param name="length"></param>
        ///// <returns></returns>
        //int ReadStream(Stream reader, TMath3D[] elements, int index, int length);

        ///// <summary>
        ///// Write a single tmath element.
        ///// </summary>
        ///// <param name="writer"></param>
        ///// <param name="element"></param>
        //void WriteStream(Stream writer, TMath3D element);

        ///// <summary>
        ///// Read a single tmath element.
        ///// </summary>
        ///// <param name="reader"></param>
        ///// <returns></returns>
        //TMath3D ReadStream(Stream reader);
    }
}
