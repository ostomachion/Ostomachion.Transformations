using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Ostomachion.Transformations.R3;

/// <summary>
/// Represents a projective (line-preserving) transformation in 3D Euclidean space using homogeneous coordinates.
/// </summary>
/// <remarks>
/// Can be represented by a 4x4 real matrix and is essentially a reinterpretation of <see cref="Matrix4x4"/>.
/// </remarks>
/// <param name="X">The first column of the transformation matrix.</param>
/// <param name="Y">The second column of the transformation matrix.</param>
/// <param name="Z">The third column of the transformation matrix.</param>
/// <param name="W">The forth column of the transformation matrix.</param>
public readonly record struct ProjectiveColumnTransform4R3(in HomogeneousVector4R3 X, in HomogeneousVector4R3 Y, in HomogeneousVector4R3 Z, in HomogeneousVector4R3 W)
    : IProjectiveTransform<ProjectiveColumnTransform4R3, HomogeneousVector4R3, HomogeneousVector4R3, float>
{
    [UnscopedRef]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal readonly ref readonly Matrix4x4 AsMatrix4x4() => ref Unsafe.As<ProjectiveColumnTransform4R3, Matrix4x4>(ref Unsafe.AsRef(in this));

    public static ProjectiveColumnTransform4R3 operator *(in ProjectiveColumnTransform4R3 left, in ProjectiveColumnTransform4R3 right) =>
        (left.AsMatrix4x4() * right.AsMatrix4x4()).AsProjectiveTransformR3();

    public static HomogeneousVector4R3 operator *(in HomogeneousVector4R3 left, in ProjectiveColumnTransform4R3 right) =>
        Vector4.Transform(left.AsVector4(), right.AsMatrix4x4()).AsHomogeneousVector4R3();

    public static HomogeneousVector4R3 operator *(HomogeneousVector4R3 left, in ProjectiveColumnTransform4R3 right) =>
        Vector4.Transform(left.AsVector4(), right.AsMatrix4x4()).AsHomogeneousVector4R3();
}
