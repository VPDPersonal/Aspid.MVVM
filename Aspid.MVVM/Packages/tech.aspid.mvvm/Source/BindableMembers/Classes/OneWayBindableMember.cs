#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
#define ASPID_MVVM_PROFILER
#endif

using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="IBindableMember{T}"/> that pushes every value change to its binders.
    /// </summary>
    /// <typeparam name="T">The bound value type.</typeparam>
    public sealed class OneWayBindableMember<T> : IBindableMember<T>, IBinderRemover
    {
        /// <summary>
        /// Occurs when <see cref="Value"/> is set.
        /// </summary>
        public event Action<T?>? Changed;

        private T? _value;
        
        /// <summary>
        /// Gets or sets the current value; setting it raises <see cref="Changed"/> even for an equal value.
        /// </summary>
        public T? Value
        {
            get => _value;
            set
            {
#if ASPID_MVVM_PROFILER
                using (this.Marker())
#endif
                {
                    _value = value;
                    Changed?.Invoke(value);
                }
            }
        }
        
        /// <summary>
        /// Gets the binding mode of the member; always <see cref="BindMode.OneWay"/>.
        /// </summary>
        public BindMode Mode => BindMode.OneWay;    

        /// <summary>
        /// Creates a member holding <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The initial value.</param>
        public OneWayBindableMember(T? value)
        {
            _value = value;
        }

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
                var mode = binder.Mode;
                mode.ThrowExceptionIfNotOne();
                
                switch (binder)
                {
                    case IBinder<T> specificBinder:
                        {
                            specificBinder.SetValue(_value);
                            
                            if (mode is BindMode.OneWay) Changed += specificBinder.SetValue;
                            else return null;
                            
                            return this;
                        }

                    case IAnyBinder anyBinder:
                        {
                            anyBinder.SetValue(_value);
                            
                            if (mode is BindMode.OneWay) Changed += anyBinder.SetValue;
                            else return null;
                            
                            return this;
                        }

                    default: throw BinderInvalidCastException.Class<T>(binder);
                }
            }
        }

        /// <inheritdoc/>
        /// <exception cref="BinderInvalidCastException">
        /// <paramref name="binder"/> implements neither <see cref="IBinder{T}"/> nor <see cref="IAnyBinder"/>.
        /// </exception>
        void IBinderRemover.Remove(IBinder binder)
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                Changed -= binder switch
                {
                    IBinder<T> specificBinder => specificBinder.SetValue,
                    IAnyBinder anyBinder => anyBinder.SetValue,
                    _ => throw BinderInvalidCastException.Class<T>(binder)
                };
            }
        }
        
        /// <summary>
        /// Sets <see cref="Value"/> to <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The new value.</param>
        public void Invoke(T? value) =>
            Value = value;
    }
}
