using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

using System.IO;

using Kraggs.Graphics.Math3D.Veci;

namespace Kraggs.Graphics.Math3D.StreamExtensions
{
    /// <summary>
    /// Contains specific and generic functions for writing and reading Types found in this library.
    /// TODO: Should Read[TMath] and Write[TMath] be renamed to ReadMath[TMath] and WriteMath[TMath] to prevent pollution of generic function overloads?
    /// </summary>
    public static class BinaryStreamExtensions
    {
        #region Read specific Types

        /// <summary>
        /// Reads a Vec2f from a stream.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public static Vec2f ReadVec2f(this BinaryReader reader)
        {
            return new Vec2f(reader.ReadSingle(), reader.ReadSingle());
        }

        /// <summary>
        /// Reads a Vec3f from a stream.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public static Vec3f ReadVec3f(this BinaryReader reader)
        {
            return new Vec3f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        }

        /// <summary>
        /// Reads a Vec4f from a stream.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public static Vec4f ReadVec4f(this BinaryReader reader)
        {
            return new Vec4f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        }

        /// <summary>
        /// Reads a Mat2f from a stream.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public static Mat2f ReadMat2f(this BinaryReader reader)
        {
            return new Mat2f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        }

        /// <summary>
        /// Reads a Mat3f from a stream.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public static Mat3f ReadMat3f(this BinaryReader reader)
        {
            //return new Mat3f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            var c0 = reader.ReadVec3f();
            var c1 = reader.ReadVec3f();
            var c2 = reader.ReadVec3f();
            return new Mat3f(c0, c1, c2);
        }

        /// <summary>
        /// Reads a Mat4f from a stream.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public static unsafe Mat4f ReadMat4f(this BinaryReader reader)
        {
            //var c0 = reader.ReadVec4f();
            //var c1 = reader.ReadVec4f();
            //var c2 = reader.ReadVec4f();
            //var c3 = reader.ReadVec4f();
            //return new Mat4f(c0, c1, c2, c3);

            var buf = new byte[16 * sizeof(float)];
            var read = reader.Read(buf, 0, buf.Length);

            if (read == buf.Length)
            {
                fixed(byte* ptr = &buf[0])
                    return *(Mat4f*)ptr;
            }
            else
                throw new EndOfStreamException("Stream ended before 16 floats was read to create a mat4f");
        }

        /// <summary>
        /// Reads a vec2i
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public static Vec2i ReadVec2i(this BinaryReader reader)
        {
            return new Vec2i(reader.ReadInt32(), reader.ReadInt32());
        }

        /// <summary>
        /// Reads a vec3i
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public static Vec3i ReadVec3i(this BinaryReader reader)
        {
            return new Vec3i(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
        }

        /// <summary>
        /// Reads a vec4i
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public static Vec4i ReadVec4i(this BinaryReader reader)
        {
            return new Vec4i(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
        }

        /// <summary>
        /// Writes a vec2f
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="vec"></param>
        [DebuggerNonUserCode()]
        public static void Write(this BinaryWriter writer, Vec2f vec)
        {
            writer.Write(vec.x);
            writer.Write(vec.y);
        }

        /// <summary>
        /// Write a vec3f
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="vec"></param>
        [DebuggerNonUserCode()]
        public static void Write(this BinaryWriter writer, Vec3f vec)
        {
            writer.Write(vec.x);
            writer.Write(vec.y);
            writer.Write(vec.z);
        }

        /// <summary>
        /// Write a vec4f
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="vec"></param>
        [DebuggerNonUserCode()]
        public static void Write(this BinaryWriter writer, Vec4f vec)
        {
            writer.Write(vec.x);
            writer.Write(vec.y);
            writer.Write(vec.z);
            writer.Write(vec.w);
        }

        /// <summary>
        /// Write a mat2f to a binarywriter.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="mat"></param>
        [DebuggerNonUserCode()]
        public static void Write(this BinaryWriter writer, Mat2f mat)
        {
            writer.Write(mat.c0);
            writer.Write(mat.c1);
        }
        /// <summary>
        /// Writes a Mat3f.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="mat"></param>
        [DebuggerNonUserCode()]
        public static void Write(this BinaryWriter writer, Mat3f mat)
        {
            writer.Write(mat.c0);
            writer.Write(mat.c1);
            writer.Write(mat.c2);
        }

        [DebuggerNonUserCode()]
        public unsafe static void Write(this BinaryWriter writer, Mat4f mat)
        {
            float* ptr = &mat.c0.x;

            for (uint i = 0; i < 16; ++i)
                writer.Write(ptr[i]);

            //writer.Write(mat.c0);
            //writer.Write(mat.c1);
            //writer.Write(mat.c2);
            //writer.Write(mat.c3);
        }

        /// <summary>
        /// Writes a vec2i
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="vec"></param>
        [DebuggerNonUserCode()]
        public static void Write(this BinaryWriter writer, Vec2i vec)
        {
            writer.Write(vec.x);
            writer.Write(vec.y);
        }

        /// <summary>
        /// Write a vec3i
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="vec"></param>
        [DebuggerNonUserCode()]
        public static void Write(this BinaryWriter writer, Vec3i vec)
        {
            writer.Write(vec.x);
            writer.Write(vec.y);
            writer.Write(vec.z);
        }

        /// <summary>
        /// Write a vec4i
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="vec"></param>
        [DebuggerNonUserCode()]
        public static void Write(this BinaryWriter writer, Vec4i vec)
        {
            writer.Write(vec.x);
            writer.Write(vec.y);
            writer.Write(vec.z);
            writer.Write(vec.w);
        }

        #endregion

        #region IGenericStream Read/write values.

        /// <summary>
        /// Writes a single math type to a stream.
        /// </summary>
        /// <typeparam name="TMath"></typeparam>
        /// <param name="writer"></param>
        /// <param name="vec"></param>
        [Obsolete("Use function Write<TMath> : IBinaryStreamMath3D instead")]
        [DebuggerNonUserCode()]
        public static void WriteVector<TMath>(this BinaryWriter writer, TMath vec) where TMath : IGenericStream, new()
        {
            vec.WriteStream(writer, vec);
        }


        /// <summary>
        /// Writes a vector buffer to a stream.
        /// </summary>
        /// <typeparam name="TMath">Generic Vector type implementing IGenericStream</typeparam>
        /// <param name="writer"></param>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        [Obsolete("Use function Write<TMath> : IBinaryStreamMath3D instead")]
        public static void WriteVector<TMath>(this BinaryWriter writer, TMath[] buffer, int offset, int count) where TMath : IGenericStream
        {
            if (buffer.Length < (offset + count))
                throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");

            if(buffer == null)
                throw new ArgumentException("buffer cant be null");

            //var glmath = (buffer.Length > 0 ? buffer[0] : default(T)) as IGLMath;
            var genstream = (buffer.Length > 0 ? buffer[0] : default(TMath)) as IGenericStream;

            //var glmath = default(T) as IGLMath;
            int min = Math.Min(buffer.Length, offset + count);
            for (int i = offset; i < min; i++)
            {
                genstream.WriteStream(writer, buffer[i]);
            }
        }

        /// <summary>
        /// Reads a single Math Type from a stream.
        /// </summary>
        /// <typeparam name="TMath"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        [Obsolete("Use function Read<TMath> : IBinaryStreamMath3D instead")]
        [DebuggerNonUserCode()]
        public static TMath ReadVector<TMath>(this BinaryReader reader) where TMath : IGenericStream, new()
        {
            var t = default(TMath);
            return (TMath)t.ReadStream(reader);
        }

        /// <summary>
        /// Reads a number of vectors from a stream.
        /// </summary>
        /// <typeparam name="TMath">Generic Vector implementing IGenericStream</typeparam>
        /// <param name="reader"></param>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [Obsolete("Use function Read<TMath> : IBinaryStreamMath3D instead")]
        public static int ReadVector<TMath>(this BinaryReader reader, TMath[] buffer, int offset, int count) where TMath : IGenericStream
        {
            if (buffer == null)
                throw new ArgumentException("buffer cant be null");

            if(buffer.Length < (offset + count))
                throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");

            var genstream = (buffer.Length > 0 ? buffer[0] : default(TMath)) as IGenericStream;

            int veccount = 0;
            int min = Math.Min(buffer.Length, offset + count);

            for (int i = offset; i < min; i++)
            {
                buffer[i] = (TMath)genstream.ReadStream(reader);
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
        [Obsolete("Use function Write<TMath> : IBinaryStreamMath3D instead")]
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
        [Obsolete("Use function Read<TMath> : IBinaryStreamMath3D instead")]
        public static int ReadMatrix<T>(this BinaryReader reader, T[] buffer, int offset, int count) where T : IGenericStream
        {
            return ReadVector<T>(reader, buffer, offset, count);
        }

        #endregion

        #region IBinaryStreamMath3D Generic read/write.

        /// <summary>
        /// Reads a single TMath element from a BinaryReader.
        /// </summary>
        /// <typeparam name="TMath"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public static TMath Read<TMath>(this BinaryReader reader) where TMath : IBinaryStreamMath3D<TMath>
        {
            var t = default(TMath);
            return t.ReadStream(reader);
        }

        /// <summary>
        /// Reads into an array of TMath elements from a BinaryReader.
        /// </summary>
        /// <typeparam name="TMath"></typeparam>
        /// <param name="reader"></param>
        /// <param name="vecs"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [DebuggerNonUserCode()]
        public static int Read<TMath>(this BinaryReader reader, TMath[] vecs, int index, int length) where TMath : IBinaryStreamMath3D<TMath>
        {
            if (vecs == null)
                throw new ArgumentException("vecs cant be null");

            if (vecs.Length < (index + length))
                throw new ArgumentException("index and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");

            return vecs[0].ReadStream(reader, vecs, index, length);
        }

        /// <summary>
        /// Writes a single TMath element to a BinaryWriter.
        /// </summary>
        /// <typeparam name="TMath"></typeparam>
        /// <param name="writer"></param>
        /// <param name="element"></param>
        [DebuggerNonUserCode()]
        public static void Write<TMath>(this BinaryWriter writer, TMath element) where TMath : IBinaryStreamMath3D<TMath>
        {
            var t = default(TMath);
            t.WriteStream(writer, element);
        }

        /// <summary>
        /// Writes an array of TMath elements to a BinaryWriter.
        /// </summary>
        /// <typeparam name="TMath"></typeparam>
        /// <param name="writer"></param>
        /// <param name="vecs"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        [DebuggerNonUserCode()]
        public static void Write<TMath>(this BinaryWriter writer, TMath[] vecs, int index, int length) where TMath : IBinaryStreamMath3D<TMath>
        {
            if (vecs == null)
                throw new ArgumentException("vecs cant be null");

            if (vecs.Length < (index + length))
                throw new ArgumentException("index and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");

            vecs[0].WriteStream(writer, vecs, index, length);
        }

        #endregion
    }
}

