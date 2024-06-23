using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Ostomachion.Transformations.R3;

public readonly record struct HomogeneousVector4R3(float X, float Y, float Z, float W = 1) :
    IVector<HomogeneousVector4R3, float>
{
    public HomogeneousVector4R3(Vector3 vector) : this(vector.X, vector.Y, vector.Z) { }

    public static HomogeneousVector4R3 Undefined => new(0, 0, 0, 0);

    public static HomogeneousVector4R3 AdditiveIdentity => new(0, 0, 0, 1);

    public static float MultiplicativeIdentity => 1;

    [UnscopedRef]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal readonly ref readonly Vector4 AsVector4() => ref Unsafe.As<HomogeneousVector4R3, Vector4>(ref Unsafe.AsRef(in this));

    public static HomogeneousVector4R3 operator +(HomogeneousVector4R3 value) => value;

    public static HomogeneousVector4R3 operator +(HomogeneousVector4R3 left, HomogeneousVector4R3 right) => new(
        left.X * right.W + right.X * left.W,
        left.Y * right.W + right.Y * left.W,
        left.Z * right.W + right.Z * left.W,
        left.W * right.W);

    public static HomogeneousVector4R3 operator -(HomogeneousVector4R3 value) => new(value.X, value.Y, value.Z, -value.W);

    public static HomogeneousVector4R3 operator -(HomogeneousVector4R3 left, HomogeneousVector4R3 right) => new(
        left.X * right.W - right.X * left.W,
        left.Y * right.W - right.Y * left.W,
        left.Z * right.W - right.Z * left.W,
        left.W * right.W);

    public static HomogeneousVector4R3 operator *(HomogeneousVector4R3 left, float right) => new(left.X * right, left.Y * right, left.Z * right, left.W);

    public static HomogeneousVector4R3 operator /(HomogeneousVector4R3 left, float right) => new(left.X, left.Y, left.Z, left.W * right);

    public HomogeneousVector4R3 Canonicalize() => this switch
    {
        { X: 0, Y: 0, Z: 0, W: 0 } => this,
        { Y: 0, Z: 0, W: 0 } => new(1, 0, 0, 0),
        { Z: 0, W: 0 } => new(X / Y, 1, 0, 0),
        { W: 0 } => new(X / Z, Y / Z, 1, 0),
        _ => new(X / W, Y / W, Z / W, 1)
    };

    public readonly bool Equals([NotNullWhen(true)] HomogeneousVector4R3? other)
    {
        if (other is null)
            return false;

        var l = Canonicalize();
        var r = other.Value.Canonicalize();

        return (l.X, l.Y, l.Z, l.W) == (r.X, r.Y, r.Z, r.W);
    }

    public readonly override int GetHashCode()
    {
        var n = Canonicalize();
        return (n.X, n.Y, n.Z, n.W).GetHashCode();
    }

    public readonly override string ToString() => $"[{X} : {Y} : {Z} : {W}]";
}
