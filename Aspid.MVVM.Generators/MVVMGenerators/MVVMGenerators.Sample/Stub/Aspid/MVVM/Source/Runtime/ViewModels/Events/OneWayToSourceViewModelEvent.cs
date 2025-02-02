using System;
using System.Runtime.CompilerServices;

namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a one-way to source binding event that allows the View to notify the ViewModel of changes.
    /// </summary>
    /// <typeparam name="T">The type of the value managed by the event.</typeparam>
    public sealed class OneWayToSourceViewModelEvent<T> : IRemoveBinderFromViewModel
    {
        private readonly Action<T?> _setValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="OneWayToSourceViewModelEvent{T}"/> class.
        /// </summary>
        /// <param name="setValue">The action to set the value in the ViewModel.</param>
        public OneWayToSourceViewModelEvent(Action<T?> setValue)
        {
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue), "SetValue action cannot be null.");
        }

        /// <summary>
        /// Adds a binder to the event for one-way to source binding.
        /// </summary>
        /// <param name="binder">The binder that will manage the binding logic.</param>
        /// <returns>An interface for removing the binder from the event.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the binder is not of type <see cref="IReverseBinder{T}"/> or if the binding mode is not <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public IRemoveBinderFromViewModel AddBinder(IBinder binder)
        {
            GetReverseBinder(binder).ValueChanged += _setValue;
            return this;
        }

        /// <summary>
        /// Removes a binder from the event.
        /// </summary>
        /// <param name="binder">The binder to be removed.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the binder is not of type <see cref="IReverseBinder{T}"/> or if the binding mode is not <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        void IRemoveBinderFromViewModel.RemoveBinder(IBinder binder) =>
            GetReverseBinder(binder).ValueChanged -= _setValue;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IReverseBinder<T> GetReverseBinder(IBinder binder)
        {
            if (binder.Mode is not BindMode.OneWayToSource || binder is not IReverseBinder<T> specificReverseBinder) 
                throw new InvalidOperationException($"Binder must be of type {typeof(IReverseBinder<T>)} and have a binding mode of {BindMode.OneWayToSource}.");

            return specificReverseBinder;
        }
    }
}