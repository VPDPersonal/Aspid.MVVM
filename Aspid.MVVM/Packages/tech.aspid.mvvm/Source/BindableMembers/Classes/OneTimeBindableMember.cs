#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
#define ASPID_MVVM_PROFILER
#endif

using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="IReadOnlyValueBindableMember{T}"/> that pushes its value to a binder once and keeps no subscription.
    /// </summary>
    /// <typeparam name="T">The bound value type.</typeparam>
    public sealed class OneTimeBindableMember<T> : IReadOnlyValueBindableMember<T>
    {
#if ASPID_MVVM_PROFILER
        private static readonly Unity.Profiling.ProfilerMarker _getMarker = new(name: $"OneTimeBindableMember<{typeof(T).Name}>.Get");
#endif
        
        private static readonly OneTimeBindableMember<T> _instance = new();
    
        /// <summary>
        /// Gets the value set by the last <see cref="Get(T)"/> call.
        /// </summary>
        public T? Value { get; private set; }
        
        /// <summary>
        /// Gets the binding mode of the member; always <see cref="BindMode.OneTime"/>.
        /// </summary>
        public BindMode Mode => BindMode.OneTime;

        private OneTimeBindableMember() { }

        /// <inheritdoc/>
        /// <exception cref="InvalidOperationException">
        /// The mode of <paramref name="binder"/> is neither <see cref="BindMode.OneWay"/> nor <see cref="BindMode.OneTime"/>.
        /// </exception>
        /// <exception cref="BinderInvalidCastException">
        /// <paramref name="binder"/> implements neither <see cref="IBinder{T}"/> nor <see cref="IAnyBinder"/>.
        /// </exception>
        IBinderRemover? IBinderAdder.Add(IBinder binder)
        {
#if ASPID_MVVM_PROFILER
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
        /// Returns the shared instance with <see cref="Value"/> set to <paramref name="value"/>.
        /// </summary>
        /// <remarks>
        /// Every call returns the same instance and overwrites its value, so bind it before the next call.
        /// </remarks>
        /// <param name="value">The value to push to the binder.</param>
        /// <returns>The shared instance for <typeparamref name="T"/>.</returns>
        public static OneTimeBindableMember<T> Get(T? value)
        {
#if ASPID_MVVM_PROFILER
            using (_getMarker.Auto())
#endif
            {
                _instance.Value = value;
                return _instance;
            }
        }
    }
}
