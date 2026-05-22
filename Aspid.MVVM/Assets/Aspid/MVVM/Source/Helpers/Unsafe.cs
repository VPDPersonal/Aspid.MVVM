using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides unsafe type reinterpretation utilities.
    /// Uses <c>Unity.Collections.LowLevel.Unsafe.UnsafeUtility</c> in Unity builds and
    /// <c>System.Runtime.CompilerServices.Unsafe</c> otherwise.
    /// </summary>
    public static class Unsafe
    {
        /// <summary>
        /// Reinterprets a reference to a value of type <typeparamref name="TFrom"/> as a value of type <typeparamref name="TTo"/>.
        /// </summary>
        /// <typeparam name="TFrom">The source type.</typeparam>
        /// <typeparam name="TTo">The target type.</typeparam>
        /// <param name="source">A reference to the source value.</param>
        /// <returns>The value reinterpreted as <typeparamref name="TTo"/>.</returns>
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
}
