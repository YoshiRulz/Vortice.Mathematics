#if NETCOREAPP3_0_OR_GREATER
[assembly: TypeForwardedTo(typeof(System.Runtime.Intrinsics.Vector64<>))]
#else
namespace System.Runtime.Intrinsics;

[Intrinsic]
[StructLayout(LayoutKind.Sequential, Size = Vector64.Size)]
public readonly struct Vector64<T> : IEquatable<Vector64<T>>
{
    public unsafe static int Count
    {
        [Intrinsic]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#pragma warning disable CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type ('T')
        get => Vector64.Size / sizeof(T);
#pragma warning restore CS8500
    }

    public static bool IsSupported
    {
        [Intrinsic]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => typeof(T) == typeof(byte) || typeof(T) == typeof(sbyte)
            || typeof(T) == typeof(ushort) || typeof(T) == typeof(short)
            || typeof(T) == typeof(uint) || typeof(T) == typeof(int) || typeof(T) == typeof(float)
            || typeof(T) == typeof(nuint) || typeof(T) == typeof(nint)
            || typeof(T) == typeof(ulong) || typeof(T) == typeof(long) || typeof(T) == typeof(double);
    }

    public static Vector64<T> One
    {
        [Intrinsic]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            var scalar = Scalar<T>.One;
            return Vector64.Create(scalar);
        }
    }

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<T> operator *(Vector64<T> left, Vector64<T> right)
        => Vector64.Multiply(left, right); // fixed delegation --yoshi

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<T> operator *(Vector64<T> left, T right)
        => Vector64.Multiply(left, right); // fixed delegation --yoshi

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Vector64<T> left, Vector64<T> right)
        => left.Equals(right); // fixed delegation --yoshi

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Vector64<T> left, Vector64<T> right)
        => !left.Equals(right); // fixed delegation --yoshi

    internal readonly ulong _00;

    // not [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals([NotNullWhen(true)] object? obj) => (obj is Vector64<T> other) && Equals(other);

    // not [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Vector64<T> other)
    {
        for (int index = 0; index < Count; index++)
        {
            if (!Scalar<T>.ObjectEquals(this.GetElementUnsafe(index), other.GetElementUnsafe(index))) return false;
        }
        return true;
    }

    // not [Intrinsic]
    public override int GetHashCode()
    {
        HashCode hashCode = default;
        for (int i = 0; i < Count; i++) hashCode.Add(this.GetElementUnsafe(i));
        return hashCode.ToHashCode();
    }
}

public static class Vector64
{
    internal const int Size = 8 * sizeof(byte);

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe Vector64<T> Create<T>(T value)
    {
        ThrowHelper.ThrowForUnsupportedIntrinsicsVector64BaseType<T>();
        Unsafe.SkipInit(out Vector64<T> result);
        for (int index = 0; index < Vector64<T>.Count; index++) result.SetElementUnsafe(index, value);
        return result;
    }

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe Vector64<float> Create(float e0, float e1)
    {
        float* pResult = stackalloc float[2] { e0, e1 };
        return Unsafe.AsRef<Vector64<float>>(pResult);
    }

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe Vector64<int> Create(int e0, int e1)
    {
        int* pResult = stackalloc int[2] { e0, e1 };
        return Unsafe.AsRef<Vector64<int>>(pResult);
    }

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe Vector64<uint> Create(uint e0, uint e1)
    {
        uint* pResult = stackalloc uint[2] { e0, e1 };
        return Unsafe.AsRef<Vector64<uint>>(pResult);
    }

    // not [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static ref T GetElementUnsafe<T>(in this Vector64<T> vector, int index)
    {
        Debug.Assert((index >= 0) && (index < Vector64<T>.Count));
        ref T address = ref Unsafe.As<Vector64<T>, T>(ref Unsafe.AsRef(in vector));
        return ref Unsafe.Add(ref address, index);
    }

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<T> Max<T>(Vector64<T> left, Vector64<T> right)
    {
        Unsafe.SkipInit(out Vector64<T> result);
        // improved w/ ref --yoshi
        scoped ref T elemLeft = ref Unsafe.NullRef<T>();
        scoped ref T elemRight = ref Unsafe.NullRef<T>();
        for (int index = 0; index < Vector64<T>.Count; index++)
        {
            elemLeft = ref left.GetElementUnsafe(index);
            elemRight = ref right.GetElementUnsafe(index);
            result.SetElementUnsafe(index, Scalar<T>.GreaterThan(elemLeft, elemRight) ? ref elemLeft : ref elemRight);
        }
        return result;
    }

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<T> Min<T>(Vector64<T> left, Vector64<T> right)
    {
        Unsafe.SkipInit(out Vector64<T> result);
        // improved w/ ref --yoshi
        scoped ref T elemLeft = ref Unsafe.NullRef<T>();
        scoped ref T elemRight = ref Unsafe.NullRef<T>();
        for (int index = 0; index < Vector64<T>.Count; index++)
        {
            elemLeft = ref left.GetElementUnsafe(index);
            elemRight = ref right.GetElementUnsafe(index);
            result.SetElementUnsafe(index, Scalar<T>.LessThan(elemLeft, elemRight) ? ref elemLeft : ref elemRight);
        }
        return result;
    }

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<T> Multiply<T>(Vector64<T> left, Vector64<T> right)
    {
        // fixed delegation --yoshi
        Unsafe.SkipInit(out Vector64<T> result);
        for (int index = 0; index < Vector64<T>.Count; index++)
        {
            result.SetElementUnsafe(index, Scalar<T>.Multiply(left.GetElementUnsafe(index), right.GetElementUnsafe(index)));
        }
        return result;
    }

    [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<T> Multiply<T>(Vector64<T> left, T right)
    {
        // fixed delegation --yoshi
        Unsafe.SkipInit(out Vector64<T> result);
        for (int index = 0; index < Vector64<T>.Count; index++)
        {
            result.SetElementUnsafe(index, Scalar<T>.Multiply(left.GetElementUnsafe(index), right));
        }
        return result;
    }

    // not [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void SetElementUnsafe<T>(in this Vector64<T> vector, int index, in T value)
    {
        Debug.Assert((index >= 0) && (index < Vector64<T>.Count));
        ref T address = ref Unsafe.As<Vector64<T>, T>(ref Unsafe.AsRef(in vector));
        Unsafe.Add(ref address, index) = value;
    }
}
#endif
