namespace Ostomachion.Transformations;

public interface IVector<TSelf, TScalar> :
    IEquatable<TSelf>,
    IEqualityOperators<TSelf, TSelf, bool>,
    IAdditionOperators<TSelf, TSelf, TSelf>,
    IAdditiveIdentity<TSelf, TSelf>,
    ISubtractionOperators<TSelf, TSelf, TSelf>,
    IUnaryNegationOperators<TSelf, TSelf>,
    IUnaryPlusOperators<TSelf, TSelf>,
    IMultiplyOperators<TSelf, TScalar, TSelf>,
    IMultiplicativeIdentity<TSelf, TScalar>,
    IDivisionOperators<TSelf, TScalar, TSelf>
    where TSelf : struct, IVector<TSelf, TScalar>
    where TScalar : struct, INumberBase<TScalar>
{ }

public interface ITransform<TSelf, in TIn, out TOut, TScalar> :
    IEquatable<TSelf>,
    IEqualityOperators<TSelf, TSelf, bool>
    where TSelf : struct, ITransform<TSelf, TIn, TOut, TScalar>
    where TIn : struct, IVector<TIn, TScalar>
    where TOut : struct, IVector<TOut, TScalar>
    where TScalar : struct, INumberBase<TScalar>
{
    /// <summary>Computes the composition of two transforms.</summary>
    /// <param name="left">The first transform.</param>
    /// <param name="right">The second transform.</param>
    /// <returns>The product matrix.</returns>
    static abstract TSelf operator *(in TSelf left, in TSelf right);

    static abstract TOut operator *(TIn left, in TSelf right);
}

/// <summary>
/// A transformation that preserves straight lines.
/// </summary>
/// <typeparam name="TSelf">The type that implements the interface.</typeparam>
public interface IProjectiveTransform<TSelf, in TIn, out TOut, TScalar> :
    ITransform<TSelf, TIn, TOut, TScalar>
    where TSelf : struct, IProjectiveTransform<TSelf, TIn, TOut, TScalar>, IEquatable<TSelf>
    where TIn : struct, IVector<TIn, TScalar>
    where TOut : struct, IVector<TOut, TScalar>
    where TScalar : struct, INumberBase<TScalar>
{ }

/// <summary>
/// A transformation that preserves parallelism and ratios of distances.
/// </summary>
/// <typeparam name="TSelf">The type that implements the interface.</typeparam>
public interface IAffineTransform<TSelf, in TIn, out TOut, TScalar> :
    IProjectiveTransform<TSelf, TIn, TOut, TScalar>
    where TSelf : struct, IAffineTransform<TSelf, TIn, TOut, TScalar>, IEquatable<TSelf>
    where TIn : struct, IVector<TIn, TScalar>
    where TOut : struct, IVector<TOut, TScalar>
    where TScalar : struct, INumberBase<TScalar>
{ }

/// <summary>
/// A transformation that preserves vector addition and scalar multiplication.
/// </summary>
/// <typeparam name="TSelf">The type that implements the interface.</typeparam>
public interface ILinearTransform<TSelf, in TIn, out TOut, TScalar> :
    IAffineTransform<TSelf, TIn, TOut, TScalar>
    where TSelf : struct, ILinearTransform<TSelf, TIn, TOut, TScalar>, IEquatable<TSelf>
    where TIn : struct, IVector<TIn, TScalar>
    where TOut : struct, IVector<TOut, TScalar>
    where TScalar : struct, INumberBase<TScalar>
{ }

/// <summary>
/// A transformation that preserves shape, size, and orientation.
/// </summary>
/// <typeparam name="TSelf">The type that implements the interface.</typeparam>
public interface ITranslationTransform<TSelf, in TIn, out TOut, TScalar> :
    IAffineTransform<TSelf, TIn, TOut, TScalar>
    where TSelf : struct, ITranslationTransform<TSelf, TIn, TOut, TScalar>, IEquatable<TSelf>
    where TIn : struct, IVector<TIn, TScalar>
    where TOut : struct, IVector<TOut, TScalar>
    where TScalar : struct, INumberBase<TScalar>
{ }

/// <summary>
/// The identity transformation.
/// </summary>
/// <typeparam name="TSelf">The type that implements the interface.</typeparam>
public interface IIdentityTransform<TSelf, in TIn, out TOut, TScalar> :
    ILinearTransform<TSelf, TIn, TOut, TScalar>,
    ITranslationTransform<TSelf, TIn, TOut, TScalar>
    where TSelf : struct, IIdentityTransform<TSelf, TIn, TOut, TScalar>, IEquatable<TSelf>
    where TIn : struct, IVector<TIn, TScalar>
    where TOut : struct, IVector<TOut, TScalar>
    where TScalar : struct, INumberBase<TScalar>
{ }
