#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED                                                                                                                                                                                                                    
#define PROFILER
#endif

using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Concrete <see cref="OneTimeStructBindableMember{T,TBoxed}"/> that fixes <c>TBoxed</c> to <see cref="ValueType"/>,
    /// exposed as a per-type singleton via <see cref="Get(T)"/>.
    /// </summary>
    /// <typeparam name="T">The struct type of the bound value.</typeparam>
    public sealed class OneTimeStructBindableMember<T> : OneTimeStructBindableMember<T, ValueType>
        where T : struct
    {
        private static readonly OneTimeStructBindableMember<T> _instance = new();
        
        private OneTimeStructBindableMember() { }
    
        /// <summary>
        /// Creates a reusable instance and assigns the provided value for one-time binding.
        /// </summary>
        /// <param name="value">The value to be provided to the binder.</param>
        /// <returns>A singleton instance of <see cref="OneTimeStructBindableMember{T}"/> configured with the specified value.</returns>
        public static OneTimeStructBindableMember<T> Get(T value)
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

    /// <summary>
    /// Abstract base <see cref="IBinderAdder"/> for struct-valued one-time bindings that pushes a single
    /// <see cref="Value"/> to the binder and then releases it; supports binders typed against
    /// <typeparamref name="T"/>, <typeparamref name="TBoxed"/>, or <see cref="IAnyBinder"/>.
    /// </summary>
    /// <typeparam name="T">The struct type of the bound value.</typeparam>
    /// <typeparam name="TBoxed">The reference type used as the boxing target for <typeparamref name="T"/> (typically <see cref="ValueType"/> or <see cref="Enum"/>).</typeparam>
    public abstract class OneTimeStructBindableMember<T, TBoxed> : IReadOnlyValueBindableMember<T>
        where T : struct, TBoxed
        where TBoxed : class
    {
#if PROFILER
        protected static readonly Unity.Profiling.ProfilerMarker GetMarker = new(name: $"OneTimeStructBindableMember<{typeof(T).Name}, {typeof(TBoxed).Name}>.Get");
#endif  
        
        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
        public T Value { get; private protected set; }
        
        /// <summary>
        /// Gets the binding mode for this member.
        /// </summary>
        public BindMode Mode => BindMode.OneTime;    
    
        private protected OneTimeStructBindableMember() { }

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
