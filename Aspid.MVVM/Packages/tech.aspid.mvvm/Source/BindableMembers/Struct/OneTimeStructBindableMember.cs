#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
#define ASPID_MVVM_PROFILER
#endif

using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Concrete <see cref="OneTimeStructBindableMember{T, TBoxed}"/> that fixes <c>TBoxed</c> to <see cref="ValueType"/>.
    /// </summary>
    /// <typeparam name="T">The bound struct type.</typeparam>
    public sealed class OneTimeStructBindableMember<T> : OneTimeStructBindableMember<T, ValueType>
        where T : struct
    {
        private static readonly OneTimeStructBindableMember<T> _instance = new();
        
        private OneTimeStructBindableMember() { }
    
        /// <summary>
        /// Returns the shared instance with <see cref="OneTimeStructBindableMember{T, TBoxed}.Value"/> set to <paramref name="value"/>.
        /// </summary>
        /// <remarks>
        /// Every call returns the same instance and overwrites its value, so bind it before the next call.
        /// </remarks>
        /// <param name="value">The value to push to the binder.</param>
        /// <returns>The shared instance for <typeparamref name="T"/>.</returns>
        public static OneTimeStructBindableMember<T> Get(T value)
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

    /// <summary>
    /// <see cref="IReadOnlyValueBindableMember{T}"/> that pushes its value to a binder once and keeps no subscription,
    /// boxing it for an <see cref="IBinder{T}"/> of <typeparamref name="TBoxed"/>.
    /// </summary>
    /// <typeparam name="T">The bound struct type.</typeparam>
    /// <typeparam name="TBoxed">The boxed form of <typeparamref name="T"/>, such as <see cref="ValueType"/> or <see cref="Enum"/>.</typeparam>
    public abstract class OneTimeStructBindableMember<T, TBoxed> : IReadOnlyValueBindableMember<T>
        where T : struct, TBoxed
        where TBoxed : class
    {
#if ASPID_MVVM_PROFILER
        protected static readonly Unity.Profiling.ProfilerMarker GetMarker = new(name: $"OneTimeStructBindableMember<{typeof(T).Name}, {typeof(TBoxed).Name}>.Get");
#endif
        
        /// <summary>
        /// Gets the value pushed to the next binder.
        /// </summary>
        public T Value { get; private protected set; }
        
        /// <summary>
        /// Gets the binding mode of the member; always <see cref="BindMode.OneTime"/>.
        /// </summary>
        public BindMode Mode => BindMode.OneTime;    
    
        private protected OneTimeStructBindableMember() { }

        /// <inheritdoc/>
        /// <exception cref="InvalidOperationException">
        /// The mode of <paramref name="binder"/> is neither <see cref="BindMode.OneWay"/> nor <see cref="BindMode.OneTime"/>.
        /// </exception>
        /// <exception cref="BinderInvalidCastException">
        /// <paramref name="binder"/> implements none of <see cref="IBinder{T}"/>, <see cref="IBinder{T}"/> of
        /// <typeparamref name="TBoxed"/> and <see cref="IAnyBinder"/>.
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
                    case IBinder<T> specificBinder: specificBinder.SetValue(Value); break;
                    case IBinder<TBoxed> structBinder: structBinder.SetValue(Value); break;
                    case IAnyBinder anyBinder: anyBinder.SetValue(Value); break;
                    default: throw BinderInvalidCastException.Struct<T, TBoxed>(binder);
                }
            
                return null;
            }
        }
    }
}
