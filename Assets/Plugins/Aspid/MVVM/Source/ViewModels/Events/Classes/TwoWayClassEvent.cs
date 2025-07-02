using System;

namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a two-way bindable member event that supports multiple binding modes and bidirectional updates.
    /// </summary>
    /// <typeparam name="T">The type of the value being handled in the bindable member event.</typeparam>
    public sealed class TwoWayClassEvent<T> : IBindableMemberEvent
    {
        /// <summary>
        /// Event triggered when the value changes.
        /// </summary>
        public event Action<T?>? Changed;

        private T? _value;
        private readonly Action<T?> _setValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="TwoWayClassEvent{T}"/> class with the specified value and a setter action.
        /// </summary>
        /// <param name="value">The initial value of the bindable member event.</param>
        /// <param name="setValue">
        /// The action used to set the value when the event is triggered.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="setValue"/> is <c>null</c>.</exception>
        public TwoWayClassEvent(T? value, Action<T?> setValue)
        {
            _value = value;
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
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
        public IBindableMemberEventRemover? Add(IBinder binder)
        {
            var mode = binder.Mode;
            if (mode is BindMode.None)
                throw new InvalidOperationException("Mode can't be None.");

            switch (mode)
            {
                case BindMode.OneWay: OneWay(); break;
                
                case BindMode.TwoWay:
                    OneWay();
                    OneWayToSource();
                    break;
                
                case BindMode.OneTime: switch (binder)
                    {
                        case IBinder<T> specificBinder: specificBinder.SetValue(_value); break;
                        case IAnyBinder anyBinder: anyBinder.SetValue(_value); break;
                        default: throw BinderInvalidCastException.Class<T>(binder);
                    }

                    return null;
                
                case BindMode.OneWayToSource: OneWayToSource(); break;
            }

            return this;

            void OneWay()
            {
                switch (binder)
                {
                    case IBinder<T> specificBinder: 
                        specificBinder.SetValue(_value); 
                        Changed += specificBinder.SetValue;
                        break;
                        
                    case IAnyBinder anyBinder:
                        anyBinder.SetValue(_value); 
                        Changed += anyBinder.SetValue;
                        break;
                        
                    default: throw BinderInvalidCastException.Class<T>(binder);
                }
            }

            void OneWayToSource()
            {
                if (binder is not IReverseBinder<T> reverseBinder)
                    throw ReverseBinderInvalidCastException<T>.Class(binder);

                reverseBinder.ValueChanged += _setValue;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Removes the binder's subscription from the event based on its binding mode.
        /// </summary>
        /// <param name="binder">The binder instance to remove.</param>
        public void Remove(IBinder binder)
        {
            switch (binder.Mode)
            {
                case BindMode.OneTime: return;
                case BindMode.OneWay: OneWay(); return;
                
                case BindMode.TwoWay:
                    OneWay();
                    OneWayToSource();
                    return;
                
                case BindMode.OneWayToSource: OneWayToSource(); return;
            }
            
            return;

            void OneWay() => Changed -= binder switch
            {
                IBinder<T> specificBinder => specificBinder.SetValue,
                IAnyBinder anyBinder => anyBinder.SetValue,
                _ => throw BinderInvalidCastException.Class<T>(binder)
            };
            
            void OneWayToSource()
            {
                if (binder is not IReverseBinder<T> reverseBinder)
                    throw ReverseBinderInvalidCastException<T>.Class(binder);

                reverseBinder.ValueChanged -= _setValue;
            }
        }

        /// <summary>
        /// Triggers the Changed event with the specified value and updates the current value.
        /// </summary>
        /// <param name="value">The new value to set and notify.</param>
        public void Invoke(T? value)
        {
            _value = value;
            Changed?.Invoke(value);
        }
    }
}