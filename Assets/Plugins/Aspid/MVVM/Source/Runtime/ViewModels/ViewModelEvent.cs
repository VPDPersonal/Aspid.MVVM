using System;

namespace Aspid.MVVM.ViewModels
{
    // TODO None sealed
    /// <summary>
    /// Represents a ViewModel event that supports binding and reverse binding for a specific type.
    /// </summary>
    /// <typeparam name="T">The type of the value managed by the event.</typeparam>
    public sealed class ViewModelEvent<T> : IRemoveBinderFromViewModel
    {
        /// <summary>
        /// Event that is triggered when the value changes.
        /// </summary>
        public event Action<T>? Changed;
        
        /// <summary>
        /// Optional action to set the value of the bound property, used for reverse binding.
        /// </summary>
        public Action<T>? SetValue { get; set; }

        /// <summary>
        /// Adds a binder to the event and optionally sets up reverse binding.
        /// </summary>
        /// <param name="binder">The binder that will manage the binding logic.</param>
        /// <param name="value">The initial value to be bound to the event.</param>
        /// <param name="isReverse">
        /// A flag indicating whether reverse binding is enabled. If true, the binder will also listen for changes to update the property.
        /// </param>
        /// <returns>An interface for removing the binder from the event.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if reverse binding is enabled and <see cref="SetValue"/> is not defined.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the binder is not of a compatible type for binding.
        /// </exception>
        public IRemoveBinderFromViewModel AddBinder(IBinder binder, T? value, bool isReverse)
        {
            var isBind = false;
            
            if (binder is IBinder<T> specificBinder)
            {
                isBind = true;
                specificBinder.SetValue(value);
                Changed += specificBinder.SetValue;
            }

            if (isReverse && binder is IReverseBinder<T> specificReverseBinder)
            {
                if (SetValue is null) throw new ArgumentNullException();
                
                specificReverseBinder.ValueChanged += SetValue;
                return this;
            }

            if (!isBind)
            {
                throw new InvalidOperationException();
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
        public void RemoveBinder(IBinder binder)
        {
            var isUnbind = false;

            if (binder is IBinder<T> specificBinder)
            {
                isUnbind = true;
                Changed -= specificBinder.SetValue;
            }

            if (binder.IsReverseEnabled && binder is IReverseBinder<T> specificReverseBinder)
            {
                if (SetValue is null) throw new ArgumentNullException();

                specificReverseBinder.ValueChanged -= SetValue;
                return;
            }
            
            if (!isUnbind)
            {
                throw new InvalidOperationException();
            }
        }
        
        /// <summary>
        /// Invokes the <see cref="Changed"/> event with the specified value.
        /// </summary>
        /// <param name="value">The value to trigger the event with.</param>
        public void Invoke(T value) => Changed?.Invoke(value);
    }
}