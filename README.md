** Kraggs.Graphics.Math3D
======================================================================

A 3D Math library for use with OpenGL.
That means the Matrix function is left-handed and 
matrix storage is column major.

All the structures can be used with Marshall and with pointers
since the structure themeself are not marked unsafe. Some of the
functions are unsafe thou.

All the matrix functions will be compared to the GLM library 
for validating library correctness.

This is probably not the fastest 3d math library for .net but it 
strives to be consistent with GML which are an industry standard
in the OpenGL c++ world. As time progress hopefully new optimizations
and improvements will be discovered and implemented.

