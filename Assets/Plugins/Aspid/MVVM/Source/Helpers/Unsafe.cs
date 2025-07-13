namespace Aspid.MVVM;

public static class Unsafe
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TTo As<TFrom, TTo>(ref TFrom source)
    {
#if UNITY_2022_1_OR_NEWER
        return Unity.Collections.LowLevel.Unsafe.UnsafeUtility.As<TFrom, TTo>(ref source);
#else
        return global::System.Runtime.CompilerServices.Unsafe.As<TFrom, TTo>(ref source);
#endif
    }
}