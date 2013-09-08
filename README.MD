** Kraggs.Graphics.Math3D
======================================================================

A 3D Math library for use with OpenGL.
That means the Matrix function is left-handed and 
matrix storage is column major.

All the structures only contain unsafe functions and no unsafe
structures for easy marshalling with opengl functions/buffers.

All the matrix functions will be compared to the GLM library 
for validating library correctness.

This is probably not the fastes 3d math library for .net but its 
strives to be consistent with GML which are an industry standard
in the OpenGL c++ world. As time progress hopefull new optimizations
will be discovered by programmers better than me.