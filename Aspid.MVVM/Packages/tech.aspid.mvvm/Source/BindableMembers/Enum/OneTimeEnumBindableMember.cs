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
        /// <summary>
        /// Creates a member holding <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The enum value to push to the binder.</param>
        public OneTimeEnumBindableMember(T value) : base(value) { }
    
        /// <summary>
        /// Creates a member holding <paramref name="value"/>.
        /// </summary>
        /// <remarks>
        /// Every call allocates a new instance. To bind without allocations, keep one instance and update its
        /// <see cref="OneTimeStructBindableMember{T, TBoxed}.Value"/>, as the generated ViewModel code does.
        /// </remarks>
        /// <param name="value">The enum value to push to the binder.</param>
        /// <returns>A new instance holding <paramref name="value"/>.</returns>
        public static OneTimeEnumBindableMember<T> Get(T value)
        {
#if ASPID_MVVM_PROFILER
            using (GetMarker.Auto())
#endif
            {
                return new OneTimeEnumBindableMember<T>(value);
            }
        }
    }
}
