using System;
using System.Runtime.CompilerServices;

namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a one-way binding event that allows the ViewModel to notify the View of changes.
    /// </summary>
    /// <typeparam name="T">The type of the value managed by the event.</typeparam>
    public sealed class OneWayViewModelEvent<T> : IRemoveBinderFromViewModel, IDisposable
    {
        /// <summary>
        /// Event that is triggered when the value changes.
        /// </summary>
        public event Action<T?>? Changed;
        
        /// <summary>
        /// Adds a binder to the event for one-way binding.
        /// </summary>
        /// <param name="binder">The binder that will manage the binding logic.</param>
        /// <param name="value">The initial value to be bound to the event.</param>
        /// <returns>An interface for removing the binder from the event.</returns>
        /// <exception cref="Exception">
        /// Thrown if reverse binding is enabled on the binder.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the binder is not of type <see cref="IBinder{T}"/>.
        /// </exception>
        public IRemoveBinderFromViewModel AddBinder(IBinder binder, T? value)
        {
            if (binder.IsReverseEnabled) 
                throw new Exception("Reverse binding is not supported in OneWayViewModelEvent.");

            var specificBinder = GetSpecificBinder(binder);
            specificBinder.SetValue(value);
            Changed += specificBinder.SetValue;
            
            return this;
        }

        /// <summary>
        /// Removes a binder from the event.
        /// </summary>
        /// <param name="binder">The binder to be removed.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the binder is not of type <see cref="IBinder{T}"/>.
        /// </exception>
        void IRemoveBinderFromViewModel.RemoveBinder(IBinder binder) =>
            Changed -= GetSpecificBinder(binder).SetValue;
        
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
        
        /// <summary>
        /// Gets a specific binder of type <see cref="IBinder{T}"/> from the provided binder.
        /// </summary>
        /// <param name="binder">The binder to convert.</param>
        /// <returns>The specific binder of type <see cref="IBinder{T}"/>.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the binder is not of type <see cref="IBinder{T}"/>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IBinder<T> GetSpecificBinder(IBinder binder)
        {
            if (binder is not IBinder<T> specificBinder) 
                throw new InvalidOperationException($"Binder must be of type {typeof(IBinder<T>)}.");

            return specificBinder;
        }
    }
}