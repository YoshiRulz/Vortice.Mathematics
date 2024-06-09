#if !NETCOREAPP3_0_OR_GREATER
namespace System.Runtime.Intrinsics;

internal static class ThrowHelper
{
    private const string ERR_MSG_NOT_SUPPORTED = "Specified type is not supported"; // this is a garbage error message IMO, but it matches the real one --yoshi

    // not [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowForUnsupportedIntrinsicsVector64BaseType<T>()
    {
        if (!Vector64<T>.IsSupported) throw new NotSupportedException(ERR_MSG_NOT_SUPPORTED);
    }

    // not [Intrinsic]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowForUnsupportedIntrinsicsVector128BaseType<T>()
    {
        if (!Vector128<T>.IsSupported) throw new NotSupportedException(ERR_MSG_NOT_SUPPORTED);
    }
}
#endif
