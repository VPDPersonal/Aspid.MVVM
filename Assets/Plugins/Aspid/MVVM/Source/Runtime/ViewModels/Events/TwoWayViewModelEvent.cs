using System;

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
        /// Adds a binder to the event for two-way binding.
        /// </summary>
        /// <param name="binder">The binder that will manage the binding logic.</param>
        /// <param name="value">The initial value to be bound to the event.</param>
        /// <param name="isReverse">A flag indicating whether reverse binding is enabled.</param>
        /// <returns>An interface for removing the binder from the event.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if reverse binding is enabled and <see cref="SetValue"/> is not defined.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the binder is not of a compatible type for binding.
        /// /// </exception>
        public IRemoveBinderFromViewModel AddBinder(IBinder binder, T? value, bool isReverse)
        {
            var isBind = false;
            
            if (binder is IBinder<T> specificBinder)
            {
                isBind = true;
                specificBinder.SetValue(value);
                Changed += specificBinder.SetValue;
            }

            if (isReverse)
            {
                ((IReverseBinder<T>)binder).ValueChanged += SetValue ??
                    throw new ArgumentNullException(nameof(SetValue), "SetValue must be defined for reverse binding.");
            }
            else if (!isBind)
            {
                throw new InvalidOperationException($"Binder must be of type {typeof(IBinder<T>)}.");
            }
            
            return this;
        }
        
        /// <summary>
        /// Removes a binder from the event and stops reverse binding if it was set up.
        /// </summary>
        /// <param name="binder">The binder to be removed.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if reverse binding was set up and <see cref="SetValue"/> is not defined.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the binder is not of a compatible type for unbinding.
        /// </exception>
        void IRemoveBinderFromViewModel.RemoveBinder(IBinder binder)
        {
            var isUnbind = false;

            if (binder is IBinder<T?> specificBinder)
            {
                isUnbind = true;
                Changed -= specificBinder.SetValue;
            }

            if (binder.IsReverseEnabled)
            {
                ((IReverseBinder<T>)binder).ValueChanged -= SetValue ?? 
                    throw new ArgumentNullException(nameof(SetValue), "SetValue must be defined for reverse binding.");
            }
            else if (!isUnbind)
            {
                throw new InvalidOperationException($"Binder must be of type {typeof(IBinder<T>)}.");
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
    }
}