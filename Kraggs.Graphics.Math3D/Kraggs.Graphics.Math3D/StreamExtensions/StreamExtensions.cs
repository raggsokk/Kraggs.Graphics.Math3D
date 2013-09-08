using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

using System.IO;

namespace Kraggs.Graphics.Math3D.StreamExtensions
{

    public static class StreamExtensions
    {

        /// <summary>
        /// Writes a vector buffer to a stream.
        /// </summary>
        /// <typeparam name="T">Generic Vector type implementing IGenericStream</typeparam>
        /// <param name="writer"></param>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public static void WriteVector<T>(this BinaryWriter writer, T[] buffer, int offset, int count) where T : IGenericStream
        {
            if (buffer.Length < (offset + count))
                throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");

            if(buffer == null)
                throw new ArgumentException("buffer cant be null");

            //var glmath = (buffer.Length > 0 ? buffer[0] : default(T)) as IGLMath;
            var genstream = (buffer.Length > 0 ? buffer[0] : default(T)) as IGenericStream;

            //var glmath = default(T) as IGLMath;
            int min = Math.Min(buffer.Length, offset + count);
            for (int i = offset; i < min; i++)
            {
                genstream.WriteStream(writer, buffer[i]);
            }
        }

        /// <summary>
        /// Reads a number of vectors from a stream.
        /// </summary>
        /// <typeparam name="T">Generic Vector implementing IGenericStream</typeparam>
        /// <param name="reader"></param>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int ReadVector<T>(this BinaryReader reader, T[] buffer, int offset, int count) where T : IGenericStream
        {
            if (buffer == null)
                throw new ArgumentException("buffer cant be null");

            if(buffer.Length < (offset + count))
                throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");

            var genstream = (buffer.Length > 0 ? buffer[0] : default(T)) as IGenericStream;

            int veccount = 0;
            int min = Math.Min(buffer.Length, offset + count);

            for (int i = offset; i < min; i++)
            {
                buffer[i] = (T)genstream.ReadStream(reader);
                veccount++;
            }

            return veccount;
        }

        /// <summary>
        /// Writes a number of matrices to a stream.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="writer"></param>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteMatrix<T>(this BinaryWriter writer, T[] buffer, int offset, int count) where T : IGenericStream
        {
            WriteVector<T>(writer, buffer, offset, count);
        }

        /// <summary>
        /// Reads a number of matrices from a stream.
        /// </summary>
        /// <typeparam name="T">Matrix implemention IGenericStream</typeparam>
        /// <param name="reader"></param>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ReadMatrix<T>(this BinaryReader reader, T[] buffer, int offset, int count) where T : IGenericStream
        {
            return ReadVector<T>(reader, buffer, offset, count);
        }

    }
}

