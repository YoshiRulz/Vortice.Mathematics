#if NETCOREAPP3_0_OR_GREATER
[assembly: TypeForwardedTo(typeof(System.Runtime.Intrinsics.Vector128<>))]
#else
namespace System.Runtime.Intrinsics;

[Intrinsic]
[StructLayout(LayoutKind.Sequential, Size = Vector128.Size)]
public readonly struct Vector128<T> : IEquatable<Vector128<T>>
{
    public static int Count
    {
        [Intrinsic]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => 2 * Vector64<T>.Count;
    }

    public static bool IsSupported
    {
        [Intrinsic]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Vector64<T>.IsSupported;
    }

    public static Vector128<T> One
    {
        [Intrinsic]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            var vector = Vector64<T>.One;
            return Vector128.Create(vector, vector);
        }
    }

    public static Vector128<T> Zero
    {
        [Intrinsic]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ThrowHelper.ThrowForUnsupportedIntrinsicsVector128BaseType<T>();
            return default;
        }
    }

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<T> operator *(Vector128<T> left, Vector128<T> right)
        => Vector128.Multiply(left, right); // fixed delegation --yoshi

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<T> operator *(Vector128<T> left, T right)
        => Vector128.Multiply(left, right); // fixed delegation --yoshi

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Vector128<T> left, Vector128<T> right)
        => left.Equals(right); // fixed delegation --yoshi

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Vector128<T> left, Vector128<T> right)
        => !left.Equals(right); // fixed delegation --yoshi

    internal readonly Vector64<T> _lower;

    internal readonly Vector64<T> _upper;

    internal Vector128(Vector64<T> lower, Vector64<T> upper)
    {
        _lower = lower;
        _upper = upper;
    }

    // not [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is Vector128<T> other && Equals(other);

    // not [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Vector128<T> other)
        => _lower.Equals(other._lower) && _upper.Equals(other._upper);

    // not [Intrinsic]
    public override int GetHashCode()
    {
        HashCode hashCode = default;
        for (int i = 0; i < Count; i++) hashCode.Add(this.GetElementUnsafe(i));
        return hashCode.ToHashCode();
    }
}

public static class Vector128
{
    private const string ERR_MSG_OUT_OF_RANGE = "Specified argument was out of the range of valid values."; // this is a garbage error message IMO, but it matches the real one --yoshi

    internal const int Size = 16 * sizeof(byte);

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<TTo> As<TFrom, TTo>(this Vector128<TFrom> vector)
    {
        ThrowHelper.ThrowForUnsupportedIntrinsicsVector128BaseType<TFrom>();
        ThrowHelper.ThrowForUnsupportedIntrinsicsVector128BaseType<TTo>();
        return Unsafe.As<Vector128<TFrom>, Vector128<TTo>>(ref vector);
    }

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<uint> AsUInt32<T>(this Vector128<T> vector)
        => vector.As<T, uint>();

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> AsVector128(this Vector4 value)
        => Unsafe.As<Vector4, Vector128<float>>(ref value);

    // not [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<T> Create<T>(Vector64<T> lower, Vector64<T> upper)
        => new(lower: lower, upper: upper);

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<T> Create<T>(T value)
    {
        var vector = Vector64.Create<T>(value);
        return Create(vector, vector);
    }

    // not [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<T> Create<T>(ReadOnlySpan<T> values)
    {
        if (values.Length < Vector128<T>.Count) throw new ArgumentOutOfRangeException(paramName: nameof(values), message: ERR_MSG_OUT_OF_RANGE);
        return Unsafe.ReadUnaligned<Vector128<T>>(ref Unsafe.As<T, byte>(ref MemoryMarshal.GetReference(values)));
    }

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Create(float e0, float e1, float e2, float e3)
        => Create(lower: Vector64.Create(e0, e1), upper: Vector64.Create(e2, e3));

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<int> Create(int e0, int e1, int e2, int e3)
        => Create(lower: Vector64.Create(e0, e1), upper: Vector64.Create(e2, e3));

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<uint> Create(uint e0, uint e1, uint e2, uint e3)
        => Create(lower: Vector64.Create(e0, e1), upper: Vector64.Create(e2, e3));

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EqualsAll<T>(Vector128<T> left, Vector128<T> right)
        => left == right;

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetElement<T>(this Vector128<T> vector, int index)
    {
        if ((uint)index >= (uint)Vector128<T>.Count) throw new ArgumentOutOfRangeException(paramName: nameof(index), actualValue: index, message: ERR_MSG_OUT_OF_RANGE);
        return vector.GetElementUnsafe(index);
    }

    // not [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static ref T GetElementUnsafe<T>(in this Vector128<T> vector, int index)
    {
        Debug.Assert((index >= 0) && (index < Vector128<T>.Count));
        ref T address = ref Unsafe.As<Vector128<T>, T>(ref Unsafe.AsRef(in vector));
        return ref Unsafe.Add(ref address, index);
    }

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<T> Max<T>(Vector128<T> left, Vector128<T> right)
        => Create(lower: Vector64.Max(left._lower, right._lower), upper: Vector64.Max(left._upper, right._upper));

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<T> Min<T>(Vector128<T> left, Vector128<T> right)
        => Create(lower: Vector64.Min(left._lower, right._lower), upper: Vector64.Min(left._upper, right._upper));

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<T> Multiply<T>(Vector128<T> left, Vector128<T> right)
        => Vector128.Create(lower: left._lower * right._lower, upper: left._upper * right._upper); // fixed delegation --yoshi

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<T> Multiply<T>(Vector128<T> left, T right)
        => Vector128.Create(lower: left._lower * right, upper: left._upper * right); // fixed delegation --yoshi

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ToScalar<T>(this Vector128<T> vector)
    {
        ThrowHelper.ThrowForUnsupportedIntrinsicsVector128BaseType<T>();
        return vector.GetElementUnsafe(0);
    }
}
#endif
