using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kraggs.Graphics.Math3D
{
    internal static class GLConstants
    {
        public const int GL_BASE_BOOL = 0x8B56;
        public const int GL_BASE_UBYTE = 0x1401;
        public const int GL_BASE_SBYTE = 0x1400;
        public const int GL_BASE_USHORT = 0x1403;
        public const int GL_BASE_SSHORT = 0x1402;
        public const int GL_BASE_UINT = 0x1405;
        public const int GL_BASE_SINT = 0x1404;
        //public const int GL_BASE_ULONG = 0x0;
        //public const int GL_BASE_SLONG = 0x0;

        public const int GL_BASE_HALF_FLOAT = 0x140B;
        public const int GL_BASE_FLOAT = 0x1406;
        public const int GL_BASE_DOUBLE = 0x140A;


        public const int BOOL_VEC2 = 0x8B57;
        public const int BOOL_VEC3 = 0x8B58;
        public const int BOOL_VEC4 = 0x8B59;
        public const int INT_VEC2 = 0x8B53;
        public const int INT_VEC3 = 0x8B54;
        public const int INT_VEC4 = 0x8B55;
        public const int UNSIGNED_INT_VEC2 = 0x8DC6;
        public const int UNSIGNED_INT_VEC3 = 0x8DC7;
        public const int UNSIGNED_INT_VEC4 = 0x8DC8;


        public const int FLOAT_VEC2 = 0x8B50;
        public const int FLOAT_VEC3 = 0x8B51;
        public const int FLOAT_VEC4 = 0x8B52;

        public const int DOUBLE_VEC2 = 0x8FFC;
        public const int DOUBLE_VEC3 = 0x8FFD;
        public const int DOUBLE_VEC4 = 0x8FFE;



        // Matrices
        public const int FLOAT_MAT2 = 0x8B5A;
        public const int FLOAT_MAT3 = 0x8B5B;
        public const int FLOAT_MAT4 = 0x8B5C;

        public const int FLOAT_MAT2x3 = 0x8B65;
        public const int FLOAT_MAT2x4 = 0x8B66;
        public const int FLOAT_MAT3x2 = 0x8B67;
        public const int FLOAT_MAT3x4 = 0x8B68;
        public const int FLOAT_MAT4x2 = 0x8B69;
        public const int FLOAT_MAT4x3 = 0x8B6A;

        public const int DOUBLE_MAT2 = 0x8F46;
        public const int DOUBLE_MAT3 = 0x8F47;
        public const int DOUBLE_MAT4 = 0x8F48;

        public const int DOUBLE_MAT2x3 = 0x8F49;
        public const int DOUBLE_MAT2x4 = 0x8F4A;
        public const int DOUBLE_MAT3x2 = 0x8F4B;
        public const int DOUBLE_MAT3x4 = 0x8F4C;
        public const int DOUBLE_MAT4x2 = 0x8F4D;
        public const int DOUBLE_MAT4x3 = 0x8F4E;


    }
}
