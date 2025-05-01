using System;
using System.Runtime.CompilerServices;

namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a two-way binding event that allows the ViewModel and View to notify each other of changes.
    /// </summary>
    /// <typeparam name="T">The type of the value managed by the event.</typeparam>
    public sealed class TwoWayViewModelEvent<T> : IRemoveBinderFromViewModel, IDisposable
    {
        /// <summary>
        /// Event that is triggered when the value changes.
        /// </summary>
        public event Action<T?>? Changed;
        
        /// <summary>
        /// Optional action to set the value of the bound property, used for reverse binding.
        /// </summary>
        public Action<T?>? SetValue { get; set; }
        
        /// <summary>
        /// Adds a binder to the event for binding based on the specified <see cref="BindMode"/>.
        /// </summary>
        /// <param name="binder">The binder that will manage the binding logic.</param>
        /// <param name="value">The initial value to be bound to the event.</param>
        /// <param name="mode">The binding mode that determines the direction of data flow.</param>
        /// <returns>
        /// An interface for removing the binder from the event, or <c>null</c> if the binding mode is <see cref="BindMode.OneTime"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if reverse binding is enabled (in <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>)
        /// and <see cref="SetValue"/> is not defined.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the binder is not of a compatible type for the specified binding mode.
        /// </exception>
        public IRemoveBinderFromViewModel? AddBinder(IBinder binder, T? value, BindMode mode)
        {
            var isBind = false;

            if (mode is not BindMode.OneWayToSource
                && binder is IBinder<T> specificBinder)
            {
                isBind = true;
                specificBinder.SetValue(value);
                
                if (mode is BindMode.OneTime) return null;
                Changed += specificBinder.SetValue;
            }

            if (mode is BindMode.TwoWay or BindMode.OneWayToSource)
            {
                ThrowArgumentNullExceptionIfSetValueNull();
                ((IReverseBinder<T>)binder).ValueChanged += SetValue;
            }
            else if (!isBind)
            {
                ThrowInvalidOperationException(mode);
            }
            
            return this;
        }
        
        /// <summary>
        /// Removes a binder from the event and stops reverse binding if it was set up.
        /// </summary>
        /// <param name="binder">The binder to be removed.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if reverse binding was set up (in <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>)
        /// and <see cref="SetValue"/> is not defined.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the binder is not of a compatible type for the specified binding mode.
        /// </exception>
        void IRemoveBinderFromViewModel.RemoveBinder(IBinder binder)
        {
            var isUnbind = false;
            var mode = binder.Mode;

            if (mode is not BindMode.OneWayToSource 
                && binder is IBinder<T?> specificBinder)
            {
                isUnbind = true;
                Changed -= specificBinder.SetValue;
            }

            if (mode is BindMode.TwoWay or BindMode.OneWayToSource)
            {
                ThrowArgumentNullExceptionIfSetValueNull();
                ((IReverseBinder<T>)binder).ValueChanged -= SetValue;
            }
            else if (!isUnbind)
            {
                ThrowInvalidOperationException(mode);
            }
        }

        /// <summary>
        /// Invokes the <see cref="Changed"/> event with the specified value.
        /// </summary>
        /// <param name="value">The value to trigger the event with.</param>
        public void Invoke(T value) => 
            Changed?.Invoke(value);

        /// <summary>
        /// Disposes the event by clearing all subscribers.
        /// </summary>
        public void Dispose() => 
            Changed = null;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ThrowArgumentNullExceptionIfSetValueNull()
        {
            if (SetValue is not null) return;
            throw new ArgumentNullException(nameof(SetValue), "SetValue must be defined for reverse binding.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ThrowInvalidOperationException(BindMode mode) =>
            throw new InvalidOperationException($"Binder must be of type {typeof(IBinder<T>)} for the specified binding mode {mode}.");
    }
}