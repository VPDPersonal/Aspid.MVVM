using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a bindable member event that provides a single value in a one-time binding operation.
    /// </summary>
    /// <typeparam name="T">The type of the value to be bound.</typeparam>
    public sealed class OneTimeBindableMember<T> : IReadOnlyValueBindableMember<T>
    {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
        private static readonly Unity.Profiling.ProfilerMarker _addMarker = new(name: $"OneTimeBindableMember<{typeof(T).Name}>.Add");
        private static readonly Unity.Profiling.ProfilerMarker _getMarker = new(name: $"OneTimeBindableMember<{typeof(T).Name}>.Get");
#endif
        
        private static readonly OneTimeBindableMember<T> _instance = new();
    
        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
        public T? Value { get; private set; }
        
        /// <summary>
        /// Gets the binding mode for this member.
        /// </summary>
        public BindMode Mode => BindMode.OneTime;

        private OneTimeBindableMember() { }

        /// <inheritdoc/>
        /// <summary>
        /// Adds a one-time binding to the specified binder with the associated value.
        /// </summary>
        /// <param name="binder">The binder to be used for the binding.</param>
        /// <returns>
        /// Always returns <c>null</c> because removal of this binding is not supported.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the binding mode is either <see cref="BindMode.OneWayToSource"/> or <see cref="BindMode.OneTime"/> <see cref="BindMode.None"/>.
        /// </exception>
        IBinderRemover? IBinderAdder.Add(IBinder binder)
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (_addMarker.Auto())
#endif
            {
                binder.Mode.ThrowExceptionIfNotOne();

                switch (binder)
                {
                    case IBinder<T> specificBinder:
                        specificBinder.SetValue(Value);
                        break;

                    case IAnyBinder anyBinder:
                        anyBinder.SetValue(Value);
                        break;

                    default: throw BinderInvalidCastException.Class<T>(binder);
                }
            
                return null;
            }
        }

        /// <summary>
        /// Creates a reusable instance and assigns the provided value for one-time binding.
        /// </summary>
        /// <param name="value">The value to be provided to the binder.</param>
        /// <returns>A singleton instance of <see cref="OneTimeBindableMember{T}"/> configured with the specified value.</returns>
        public static OneTimeBindableMember<T> Get(T value)
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (_getMarker.Auto())
#endif
            {
                _instance.Value = value;
                return _instance;
            }
        }
    }
}