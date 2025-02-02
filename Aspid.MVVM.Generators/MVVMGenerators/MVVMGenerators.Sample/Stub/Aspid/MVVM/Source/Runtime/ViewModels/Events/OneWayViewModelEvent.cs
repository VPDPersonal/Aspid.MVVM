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
        /// Adds a binder to the event for one-way or one-time binding.
        /// </summary>
        /// <param name="binder">The binder that will manage the binding logic.</param>
        /// <param name="value">The initial value to be bound to the event.</param>
        /// <returns>
        /// An interface for removing the binder from the event, or <c>null</c> if the binding mode is <see cref="BindMode.OneTime"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Thrown if the binding mode is not <see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the binder is not of type <see cref="IBinder{T}"/>.
        /// </exception>
        public IRemoveBinderFromViewModel? AddBinder(IBinder binder, T? value)
        {
            var mode = binder.Mode;
            
            if (mode is not BindMode.OneWay && mode is not BindMode.OneTime) 
                throw new Exception("Only OneWay and OneTime binding modes are supported in OneWayViewModelEvent.");

            var specificBinder = GetSpecificBinder(binder);
            specificBinder.SetValue(value);
            
            if (mode is BindMode.OneTime) return null;
            
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IBinder<T> GetSpecificBinder(IBinder binder)
        {
            if (binder is not IBinder<T> specificBinder) 
                throw new InvalidOperationException($"Binder must be of type {typeof(IBinder<T>)}.");

            return specificBinder;
        }
    }
}