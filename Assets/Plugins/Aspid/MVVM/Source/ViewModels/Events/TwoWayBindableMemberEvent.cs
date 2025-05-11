using System;
using System.Runtime.CompilerServices;

namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a two-way bindable member event that supports multiple binding modes and bidirectional updates.
    /// </summary>
    /// <typeparam name="T">The type of the value being handled in the bindable member event.</typeparam>
    public sealed class TwoWayBindableMemberEvent<T> : IBindableMemberEvent
    {
        /// <summary>
        /// Event triggered when the value changes.
        /// </summary>
        public event Action<T?>? Changed;

        private T? _value;
        private readonly Action<T?> _setValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="TwoWayBindableMemberEvent{T}"/> class with the specified value and a setter action.
        /// </summary>
        /// <param name="value">The initial value of the bindable member event.</param>
        /// <param name="setValue">
        /// The action used to set the value when the event is triggered.
        /// </param>
        /// <exception cref="NullReferenceException">Thrown if <paramref name="setValue"/> is <c>null</c>.</exception>
        public TwoWayBindableMemberEvent(T? value, Action<T?> setValue)
        {
            _value = value;
            _setValue = setValue ?? throw new NullReferenceException(nameof(setValue));
        }

        /// <inheritdoc />
        /// <summary>
        /// Adds the binder to the event and subscribes it to appropriate modes.
        /// </summary>
        /// <param name="binder">The binder instance to bind to the event.</param>
        /// <returns>Returns itself to allow unsubscribing later.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the binding mode is invalid or not supported.</exception>
        public IBindableMemberEventRemover Add(IBinder binder)
        {
            var mode = binder.Mode;

            switch (mode)
            {
                case BindMode.OneTime:
                    binder.Cast<T>().SetValue(_value);
                    break;
                
                case BindMode.OneWay: 
                    SubscribeBinder(binder);
                    break;
                
                case BindMode.TwoWay:
                    SubscribeBinder(binder);
                    SubscribeReverseBinder(binder);
                    break;
                
                case BindMode.OneWayToSource:
                    SubscribeReverseBinder(binder);
                    break;
                
                case BindMode.None:
                default: ThrowInvalidOperationException(mode);
                    break;
            }

            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Removes the binder's subscription from the event based on its binding mode.
        /// </summary>
        /// <param name="binder">The binder instance to remove.</param>
        public void Remove(IBinder binder)
        {
            var mode = binder.Mode;

            switch (mode)
            {
                case BindMode.OneTime: break;
                
                case BindMode.OneWay: 
                    UnsubscribeBinder(binder);
                    break;
                
                case BindMode.TwoWay:
                    UnsubscribeBinder(binder);
                    UnsubscribeReverseBinder(binder);
                    break;
                
                case BindMode.OneWayToSource:
                    UnsubscribeReverseBinder(binder);
                    break;
                
                case BindMode.None:
                default: ThrowInvalidOperationException(mode);
                    break;
            }
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

        private void SubscribeBinder(IBinder binder)
        {
            var specificBinder = binder.Cast<T>();
            
            specificBinder.SetValue(_value);
            Changed += specificBinder.SetValue;
        }

        private void UnsubscribeBinder(IBinder binder) =>
            Changed -= binder.Cast<T>().SetValue;
        
        private void SubscribeReverseBinder(IBinder binder) =>
            GetReverseBinder(binder).ValueChanged += _setValue;
        
        private void UnsubscribeReverseBinder(IBinder binder) =>
            GetReverseBinder(binder).ValueChanged -= _setValue;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IReverseBinder<T> GetReverseBinder(IBinder binder)
        {
            if (binder is not IReverseBinder<T> specificReverseBinder) 
                throw new InvalidOperationException($"Binder must be of type {typeof(IReverseBinder<T>)}.");

            return specificReverseBinder;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ThrowInvalidOperationException(BindMode mode) =>
            throw new InvalidOperationException();
    }
}