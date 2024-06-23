using System.Runtime.CompilerServices;
using Ostomachion.Transformations.R3;

namespace Ostomachion.Transformations;
internal static class SystemNumericsExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static ref readonly ProjectiveTransform4R3 AsProjectiveTransformR3(in this Matrix4x4 m) => ref Unsafe.As<Matrix4x4, ProjectiveTransform4R3>(ref Unsafe.AsRef(in m));


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static ref readonly HomogeneousVector4R3 AsHomogeneousVector4R3(in this Vector4 m) => ref Unsafe.As<Vector4, HomogeneousVector4R3>(ref Unsafe.AsRef(in m));
}
