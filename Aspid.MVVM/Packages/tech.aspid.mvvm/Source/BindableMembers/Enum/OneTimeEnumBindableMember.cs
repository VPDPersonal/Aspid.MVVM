#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
#define ASPID_MVVM_PROFILER
#endif

using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Concrete <see cref="OneTimeStructBindableMember{T, TBoxed}"/> that fixes <c>TBoxed</c> to <see cref="Enum"/>.
    /// </summary>
    /// <typeparam name="T">The bound enum type.</typeparam>
    public sealed class OneTimeEnumBindableMember<T> : OneTimeStructBindableMember<T, Enum>
        where T : struct, Enum
    {
        private static readonly OneTimeEnumBindableMember<T> _instance = new();
    
        private OneTimeEnumBindableMember() { }
    
        /// <summary>
        /// Returns the shared instance with <see cref="OneTimeStructBindableMember{T, TBoxed}.Value"/> set to <paramref name="value"/>.
        /// </summary>
        /// <remarks>
        /// Every call returns the same instance and overwrites its value, so bind it before the next call.
        /// </remarks>
        /// <param name="value">The value to push to the binder.</param>
        /// <returns>The shared instance for <typeparamref name="T"/>.</returns>
        public static OneTimeEnumBindableMember<T> Get(T value)
        {
#if ASPID_MVVM_PROFILER
            using (GetMarker.Auto())
#endif
            {
                _instance.Value = value;
                return _instance;
            }
        }
    }
}
