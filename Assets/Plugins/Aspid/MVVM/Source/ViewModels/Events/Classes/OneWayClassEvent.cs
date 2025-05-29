using System;

namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a one-way bindable member event that supports event notification and handling for values of a specified type.
    /// </summary>
    /// <typeparam name="T">The type of the value being handled in the bindable member event.</typeparam>
    public sealed class OneWayClassEvent<T> : IBindableMemberEvent
    {
        /// <summary>
        /// Event triggered when the value changes.
        /// </summary>
        public event Action<T?>? Changed;

        private T? _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="OneWayClassEvent{T}"/> class with the specified initial value.
        /// </summary>
        /// <param name="value">The initial value of the bindable member event.</param>
        public OneWayClassEvent(T? value)
        {
            _value = value;
        }

        /// <inheritdoc />
        /// <summary>
        /// Adds the binder to the event with the current value and subscribes to the value change event.
        /// </summary>
        /// <param name="binder">The binder to bind to the event.</param>
        /// <returns>Returns itself to enable removal of the binder later.</returns>
        /// <exception cref="Exception">
        /// Thrown if the binding mode is not <see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/>.
        /// </exception>
        public IBindableMemberEventRemover Add(IBinder binder)
        {
            var mode = binder.Mode;
            
            if (mode is not (BindMode.OneWay or BindMode.OneTime))
                throw new InvalidOperationException($"Mode must be OneWay or OneTime. Mode = {{{mode}}}");

            switch (binder)
            {
                case IBinder<T> specificBinder:
                    specificBinder.SetValue(_value);
                    
                    if (mode is BindMode.OneWay)
                        Changed += specificBinder.SetValue;
                    break;
                
                case IAnyBinder anyBinder:
                    anyBinder.SetValue(_value);
                    
                    if (mode is BindMode.OneWay)
                        Changed += anyBinder.SetValue;
                    break;
                
                default: throw BinderInvalidCastException<T>.Class();
            }
            
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Removes the binder from the event subscription.
        /// </summary>
        /// <param name="binder">The binder to remove.</param>
        public void Remove(IBinder binder)
        {
            if (binder.Mode is BindMode.OneTime)
                return;

            Changed -= binder switch
            {
                IBinder<T> specificBinder => specificBinder.SetValue,
                IAnyBinder anyBinder => anyBinder.SetValue,
                _ => throw BinderInvalidCastException<T>.Class()
            };
        }
        
        /// <summary>
        /// Triggers the Changed event with the specified value and updates the current value.
        /// </summary>
        /// <param name="value">The new value to set and notify.</param>
        public void Invoke(T value)
        {
            _value = value;
            Changed?.Invoke(value);
        }
    }
}