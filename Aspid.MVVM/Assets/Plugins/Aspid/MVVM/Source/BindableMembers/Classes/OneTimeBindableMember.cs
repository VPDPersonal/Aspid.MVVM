#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED                                                                                                                                                                                                                    
#define PROFILER
#endif

using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Sealed <see cref="IBinderAdder"/> exposed as a per-type singleton that pushes a single <see cref="Value"/>
    /// to the binder once and then releases it; rejects every <see cref="BindMode"/> other than
    /// <see cref="BindMode.OneWay"/> and <see cref="BindMode.OneTime"/>.
    /// </summary>
    /// <typeparam name="T">The reference type of the bound value.</typeparam>
    public sealed class OneTimeBindableMember<T> : IReadOnlyValueBindableMember<T>
    {
#if PROFILER
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

        /// <summary>
        /// Adds a one-time binding to the specified binder with the associated value.
        /// </summary>
        /// <param name="binder">The binder to be used for the binding.</param>
        /// <returns>
        /// Always returns <see langword="null"/> because removal of this binding is not supported.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the binding mode is <see cref="BindMode.OneWayToSource"/>, <see cref="BindMode.TwoWay"/>, or <see cref="BindMode.None"/>.
        /// </exception>
        IBinderRemover? IBinderAdder.Add(IBinder binder)
        {
#if PROFILER
            using (this.Marker())
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
#if PROFILER
            using (_getMarker.Auto())
#endif
            {
                _instance.Value = value;
                return _instance;
            }
        }
    }
}
