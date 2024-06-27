#if !NETCOREAPP3_0_OR_GREATER
namespace System.Runtime.Intrinsics;

internal static class Scalar<T>
{
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type
#pragma warning disable CS8605 // Unboxing a possibly null value
    public static T One
    {
        // not [Intrinsic]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            // yeah I have no idea what's going on with these casts --yoshi

            if (typeof(T) == typeof(byte)) return (T)(object)(byte)1U;
            if (typeof(T) == typeof(sbyte)) return (T)(object)(sbyte)1;

            if (typeof(T) == typeof(ushort)) return (T)(object)(ushort)1U;
            if (typeof(T) == typeof(short)) return (T)(object)(short)1;

            if (typeof(T) == typeof(uint)) return (T)(object)1U;
            if (typeof(T) == typeof(int)) return (T)(object)1;
            if (typeof(T) == typeof(float)) return (T)(object)1.0f;

            if (typeof(T) == typeof(nuint)) return (T)(object)(nuint)1U;
            if (typeof(T) == typeof(nint)) return (T)(object)(nint)1;

            if (typeof(T) == typeof(ulong)) return (T)(object)1UL;
            if (typeof(T) == typeof(long)) return (T)(object)1L;
            if (typeof(T) == typeof(double)) return (T)(object)1.0;

            ThrowHelper.ThrowForUnsupportedIntrinsicsVector64BaseType<T>();
            return default!;
        }
    }

    // not [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool GreaterThan(T left, T right)
    {
        // yeah I have no idea what's going on with these casts --yoshi

        if (typeof(T) == typeof(byte)) return (byte)(object)left > (byte)(object)right;
        if (typeof(T) == typeof(sbyte)) return (sbyte)(object)left > (sbyte)(object)right;

        if (typeof(T) == typeof(ushort)) return (ushort)(object)left > (ushort)(object)right;
        if (typeof(T) == typeof(short)) return (short)(object)left > (short)(object)right;

        if (typeof(T) == typeof(uint)) return (uint)(object)left > (uint)(object)right;
        if (typeof(T) == typeof(int)) return (int)(object)left > (int)(object)right;
        if (typeof(T) == typeof(float)) return (float)(object)left > (float)(object)right;

        if (typeof(T) == typeof(nuint)) return (nuint)(object)left > (nuint)(object)right;
        if (typeof(T) == typeof(nint)) return (nint)(object)left > (nint)(object)right;

        if (typeof(T) == typeof(ulong)) return (ulong)(object)left > (ulong)(object)right;
        if (typeof(T) == typeof(long)) return (long)(object)left > (long)(object)right;
        if (typeof(T) == typeof(double)) return (double)(object)left > (double)(object)right;

        ThrowHelper.ThrowForUnsupportedIntrinsicsVector64BaseType<T>();
        return default!;
    }

    // not [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool LessThan(T left, T right)
    {
        // yeah I have no idea what's going on with these casts --yoshi

        if (typeof(T) == typeof(byte)) return (byte)(object)left < (byte)(object)right;
        if (typeof(T) == typeof(sbyte)) return (sbyte)(object)left < (sbyte)(object)right;

        if (typeof(T) == typeof(ushort)) return (ushort)(object)left < (ushort)(object)right;
        if (typeof(T) == typeof(short)) return (short)(object)left < (short)(object)right;

        if (typeof(T) == typeof(uint)) return (uint)(object)left < (uint)(object)right;
        if (typeof(T) == typeof(int)) return (int)(object)left < (int)(object)right;
        if (typeof(T) == typeof(float)) return (float)(object)left < (float)(object)right;

        if (typeof(T) == typeof(nuint)) return (nuint)(object)left < (nuint)(object)right;
        if (typeof(T) == typeof(nint)) return (nint)(object)left < (nint)(object)right;

        if (typeof(T) == typeof(ulong)) return (ulong)(object)left < (ulong)(object)right;
        if (typeof(T) == typeof(long)) return (long)(object)left < (long)(object)right;
        if (typeof(T) == typeof(double)) return (double)(object)left < (double)(object)right;

        ThrowHelper.ThrowForUnsupportedIntrinsicsVector64BaseType<T>();
        return default!;
    }

    // not [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Multiply(T left, T right)
    {
        // yeah I have no idea what's going on with these casts --yoshi

        if (typeof(T) == typeof(byte)) return (T)(object)(byte)((byte)(object)left * (byte)(object)right);
        if (typeof(T) == typeof(sbyte)) return (T)(object)(sbyte)((sbyte)(object)left * (sbyte)(object)right);

        if (typeof(T) == typeof(ushort)) return (T)(object)(ushort)((ushort)(object)left * (ushort)(object)right);
        if (typeof(T) == typeof(short)) return (T)(object)(short)((short)(object)left * (short)(object)right);

        if (typeof(T) == typeof(uint)) return (T)(object)((uint)(object)left * (uint)(object)right);
        if (typeof(T) == typeof(int)) return (T)(object)((int)(object)left * (int)(object)right);
        if (typeof(T) == typeof(float)) return (T)(object)(float)((float)(object)left * (float)(object)right);

        if (typeof(T) == typeof(nuint)) return (T)(object)((nuint)(object)left * (nuint)(object)right);
        if (typeof(T) == typeof(nint)) return (T)(object)((nint)(object)left * (nint)(object)right);

        if (typeof(T) == typeof(ulong)) return (T)(object)((ulong)(object)left * (ulong)(object)right);
        if (typeof(T) == typeof(long)) return (T)(object)((long)(object)left * (long)(object)right);
        if (typeof(T) == typeof(double)) return (T)(object)(double)((double)(object)left * (double)(object)right);

        ThrowHelper.ThrowForUnsupportedIntrinsicsVector64BaseType<T>();
        return default!;
    }

    // not [Intrinsic]
    // also not AggressiveInlining
    public static bool ObjectEquals(T left, T right)
    {
        // yeah I have no idea what's going on with these casts --yoshi

        if (typeof(T) == typeof(byte)) return ((byte)(object)left).Equals((byte)(object)right);
        if (typeof(T) == typeof(sbyte)) return ((sbyte)(object)left).Equals((sbyte)(object)right);

        if (typeof(T) == typeof(ushort)) return ((ushort)(object)left).Equals((ushort)(object)right);
        if (typeof(T) == typeof(short)) return ((short)(object)left).Equals((short)(object)right);

        if (typeof(T) == typeof(uint)) return ((uint)(object)left).Equals((uint)(object)right);
        if (typeof(T) == typeof(int)) return ((int)(object)left).Equals((int)(object)right);
        if (typeof(T) == typeof(float)) return ((float)(object)left).Equals((float)(object)right);

        if (typeof(T) == typeof(nuint)) return ((nuint)(object)left).Equals((nuint)(object)right);
        if (typeof(T) == typeof(nint)) return ((nint)(object)left).Equals((nint)(object)right);

        if (typeof(T) == typeof(ulong)) return ((ulong)(object)left).Equals((ulong)(object)right);
        if (typeof(T) == typeof(long)) return ((long)(object)left).Equals((long)(object)right);
        if (typeof(T) == typeof(double)) return ((double)(object)left).Equals((double)(object)right);

        ThrowHelper.ThrowForUnsupportedIntrinsicsVector64BaseType<T>();
        return default!;
    }
#pragma warning restore CS8600
#pragma warning restore CS8605
}
#endif
