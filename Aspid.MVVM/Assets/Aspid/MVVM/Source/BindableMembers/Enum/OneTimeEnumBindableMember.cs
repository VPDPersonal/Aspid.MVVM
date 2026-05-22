#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED                                                                                                                                                                                                                    
#define PROFILER
#endif

using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Concrete <see cref="OneTimeStructBindableMember{T,TBoxed}"/> that fixes <c>TBoxed</c> to <see cref="Enum"/>,
    /// exposed as a per-type singleton via <see cref="Get(T)"/> for one-time enum bindings.
    /// </summary>
    /// <typeparam name="T">The enum type of the bound value.</typeparam>
    public sealed class OneTimeEnumBindableMember<T> : OneTimeStructBindableMember<T, Enum>
        where T : struct, Enum
    {
        private static readonly OneTimeEnumBindableMember<T> _instance = new();
    
        private OneTimeEnumBindableMember() { }
    
        /// <summary>
        /// Creates a reusable instance and assigns the provided enum value for one-time binding.
        /// </summary>
        /// <param name="value">The enum value to provide to the binder.</param>
        /// <returns>A singleton instance of <see cref="OneTimeEnumBindableMember{T}"/> configured with the specified value.</returns>
        public static OneTimeEnumBindableMember<T> Get(T value)
        {
#if PROFILER
            using (GetMarker.Auto())
#endif
            {
                _instance.Value = value;
                return _instance;
            }
        }
    }
}
