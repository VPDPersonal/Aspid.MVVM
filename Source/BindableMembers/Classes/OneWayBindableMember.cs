#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED                                                                                                                                                                                                                    
#define PROFILER
#endif

using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Sealed <see cref="IBinderAdder"/> that pushes value changes from the ViewModel to subscribed
    /// <see cref="IBinder{T}"/> / <see cref="IAnyBinder"/> instances; additionally exposes a get/set
    /// <see cref="Value"/> and a <see cref="Changed"/> event. Accepts only <see cref="BindMode.OneWay"/>
    /// and <see cref="BindMode.OneTime"/> binders.
    /// </summary>
    /// <typeparam name="T">The reference type of the bound value.</typeparam>
    public sealed class OneWayBindableMember<T> : IBindableMember<T>, IBinderRemover
    {
        /// <summary>
        /// Raised when the value changes.
        /// </summary>
        public event Action<T?>? Changed;

        private T? _value;
        
        /// <summary>
        /// Gets or sets the current value. Setting the value will trigger the <see cref="Changed"/> event.
        /// </summary>
        public T? Value
        {
            get => _value;
            set
            {
#if PROFILER
                using (this.Marker())
#endif 
                {
                    _value = value;
                    Changed?.Invoke(value);
                }
            }
        }
        
        /// <summary>
        /// Gets the binding mode for this member.
        /// </summary>
        public BindMode Mode => BindMode.OneWay;    

        /// <summary>
        /// Initializes a new instance of the <see cref="OneWayBindableMember{T}"/> class with the specified initial value.
        /// </summary>
        /// <param name="value">The initial value of the bindable member.</param>
        public OneWayBindableMember(T? value)
        {
            _value = value;
        }

        /// <summary>
        /// Adds the binder to the event with the current value and subscribes to the value change event.
        /// </summary>
        /// <param name="binder">The binder to bind to the event.</param>
        /// <returns>An <see cref="IBinderRemover"/> to enable removal of the binder later, or <see langword="null"/> for <see cref="BindMode.OneTime"/> binders.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the binding mode is not <see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/>.
        /// </exception>
        IBinderRemover? IBinderAdder.Add(IBinder binder)
        {
#if PROFILER
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

        /// <summary>
        /// Removes the binder from the event subscription.
        /// </summary>
        /// <param name="binder">The binder to remove.</param>
        void IBinderRemover.Remove(IBinder binder)
        {
#if PROFILER
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
        /// Sets the current value and raises the <see cref="Changed"/> event.
        /// </summary>
        /// <param name="value">The new value to set and notify.</param>
        public void Invoke(T? value) =>
            Value = value;
    }
}
