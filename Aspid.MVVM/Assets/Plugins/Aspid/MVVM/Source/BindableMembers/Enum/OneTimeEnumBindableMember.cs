using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a bindable member event that provides a single value in a one-time binding operation.
    /// </summary>
    /// <typeparam name="T">The type of the value to be bound.</typeparam>
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
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (GetMarker.Auto())
#endif
            {
                _instance.Value = value;
                return _instance;
            }
        }
    }
}