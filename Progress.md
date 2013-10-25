
** Kraggs.Graphics.Math3D

Milestone 1
	MathFunctions
		pow(float,float)
		exp(float)
		log(float)
		exp2(float)
		log2(float)	
		sqrt
		inversesqrt
	Vec2f
	Vec3f
	Vec4f
		lengthsquared
		length		
		normalize
		distance

Milestone 2
	Vector (Vec,Vec), (Vec, float)
		Add 
		Subtract
		Multiply
		Divide
		Negate
		Dot
		Cross
		Faceforward
		Reflect
		Refract

Milestone 3
	Vec2f
	Vec3f
	Vec4f
		ToString
		GetHashCode
		operator == !=
		operator (Vec,float) + - * /
		operator (float,Vec) + - * /
		operator (Vec,Vec)   + - * /
		Equals

Milestone 4
	Mat2f
	Mat3f
	Mat4f
		Transpose
		operator (Mat,Mat) *
	Matrix
		Multiply (Mat,Mat)	
		Determinant
		inverse

Milestone 5
	Mat2f
	Mat3f
	Mat4f
		ToString
		GetHashCode
		operator == !=
		Equals		

Milestone 6
	Matrix4f		
		ortho
		lookat
		frustrum
		perspective
		translatexyz
		rotatex
		rotatey
		rotatez
		rotatexyz
		scalexyz

Milestone 7
	MathFunctions
		clamp (float)
		Mix (float)
		Step (float)
		SmoothStep (float)

	Vector
		Abs(Vec)
		Ceiling(Vec)
		Max(Vec,Vec)
		Min(Vec,Vec)
		Floor(Vec)
		Truncate(Vec)
		Clamp(Vec)
		Mix(Vec,Vec)
		Step(Vec)
		SmoothStep(Vec)

Milestone 8
	IGLMath
		BaseType aka dotNetType
		ComponentCount
		SizeInBytes
		GLBaseType aka float, double?
		GLUniformType aka Vec3, Vec4d
		GLAttributeType				
	IGLMatrix : IGLMath
		ColumnCount
		RowCount
		IsSquare?? aka columncount=rowcount
	Stream Extension
		ReadVec<T>
		ReadMat<T>
	SizeInBytes?

Milestone 9
	Quaternions

Milestone 10
	MathFunction Reorg.
		MathF 		(float version of Math)
		FastMath 	(fast version of MathF)
	Lerp 	(Linearly interpolates between two vectors.) (Is this SmoothStep?)
	Slerp 	( torque-minimal path between two vectors.)
	NLerp	(just normalized Lerp)
		
Milestone 11
	Vec2i
	Vec3i
	Vec4i
		ToString
		GetHashCode
		operator == !=
		operator (Vec,int) + - * /
		operator (int,Vec) + - * /
		operator (Vec,Vec)   + - * /
		operator (vec, int) << >>
		operator VecXf (VecXi)
		operator VecXi (VecXf)
		Equals
	Vector (Vec,Vec), (Vec, int)
		Add 
		Subtract
		Multiply
		Divide
		Negate
		HigherMultiple
		LowerMultiple
		ComponentMultiply
		ComponentAdd
		ComponentMax
		ComponentMin
		
Milestone 12
		noop (forgotten).
		
Milestone 13
	reimplement GL Generics to use
		a separate description struct.
		This struct should have no instance
		members but only functions/properties.
	extend StreamExtensions maybe rename to binarystreamextension?
		
	Sign the assembly.
	Release builds should be put in own directory.

Milestone 14
	noise
	random

	
Milestone x++	
	Color4f
		ToString
		GetHashCode
		operator == !=
		operator (Vec,float) + - * /
		operator (float,Vec) + - * /
		operator (Vec,Vec)   + - * /
		Equals
		Lerp (each component separetely)


Release 1
	Milestone 1
	Milestone 2
	Milestone 3
	glm verify milestone 1,2 & 3
Release 2
	Milestone 4
	Milestone 5
	Milestone 6
Release 3
	Milestone 7
	Milestone 8
	Milestone 9
Release 4
	Milestone 10
	Milestone 11
	Milestone 13
Release 5
	Milestone 14
	...