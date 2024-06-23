using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Ostomachion.Transformations.R3;

/// <summary>
/// Represents a projective (line-preserving) transformation in 3D Euclidean space using homogeneous coordinates.
/// </summary>
/// <remarks>
/// Can be represented by a 4x4 real matrix and is essentially a reinterpretation of <see cref="Matrix4x4"/>.
/// </remarks>
/// <param name="X">The first row of the transformation matrix.</param>
/// <param name="Y">The second row of the transformation matrix.</param>
/// <param name="Z">The third row of the transformation matrix.</param>
/// <param name="W">The fourth row of the transformation matrix.</param>
public readonly record struct ProjectiveTransform4R3(HomogeneousVector4R3 X, HomogeneousVector4R3 Y, HomogeneousVector4R3 Z, HomogeneousVector4R3 W)
    : IProjectiveTransform<ProjectiveTransform4R3, HomogeneousVector4R3, HomogeneousVector4R3, float>
{
    [UnscopedRef]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal readonly ref readonly Matrix4x4 AsMatrix4x4() => ref Unsafe.As<ProjectiveTransform4R3, Matrix4x4>(ref Unsafe.AsRef(in this));

    public static ProjectiveTransform4R3 operator *(in ProjectiveTransform4R3 left, in ProjectiveTransform4R3 right) =>
        (left.AsMatrix4x4() * right.AsMatrix4x4()).AsProjectiveTransformR3();

    public static HomogeneousVector4R3 operator *(HomogeneousVector4R3 left, in ProjectiveTransform4R3 right) =>
        Vector4.Transform(left.AsVector4(), right.AsMatrix4x4()).AsHomogeneousVector4R3();
}
