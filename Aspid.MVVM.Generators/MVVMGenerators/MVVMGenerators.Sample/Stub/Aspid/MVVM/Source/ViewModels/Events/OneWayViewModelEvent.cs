using System;

namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a one-way binding event that allows the ViewModel to notify the View of changes.
    /// </summary>
    /// <typeparam name="T">The type of the value managed by the event.</typeparam>
    public sealed class OneWayViewModelEvent<T> : IViewModelEvent, IDisposable
    {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
        private static readonly Unity.Profiling.ProfilerMarker AddBinderMarker = new("OneWayViewModelEvent.AddBinder");
#endif
        
        /// <summary>
        /// Event that is triggered when the value changes.
        /// </summary>
        public event Action<T?>? Changed;

        /// <summary>
        /// Adds a binder to the event for one-way or one-time binding.
        /// </summary>
        /// <param name="binder">The binder that will manage the binding logic.</param>
        /// <returns>
        /// An interface for removing the binder from the event, or <c>null</c> if the binding mode is <see cref="BindMode.OneTime"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Thrown if the binding mode is not <see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the binder is not of type <see cref="IBinder{T}"/>.
        /// </exception>
        public IViewModelEventRemover? AddBinder(IBinder binder)
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (AddBinderMarker.Auto())
#endif
            {
                return AddBinder(binder.Cast<T>());
            }
        }
        
        /// <summary>
        /// Adds a binder to the event for one-way or one-time binding.
        /// </summary>
        /// <param name="binder">The binder that will manage the binding logic.</param>
        /// <returns>
        /// An interface for removing the binder from the event, or <c>null</c> if the binding mode is <see cref="BindMode.OneTime"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Thrown if the binding mode is not <see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the binder is not of type <see cref="IBinder{T}"/>.
        /// </exception>
        public IViewModelEventRemover? AddBinder(IBinder<T?> binder)
        {
            var mode = binder.Mode;
            
            if (mode is not BindMode.OneWay) 
                throw new Exception("Only OneWay binding modes are supported in OneWayViewModelEvent.");
            
            Changed += binder.SetValue;
            return this;
        }

        /// <summary>
        /// Removes a binder from the event.
        /// </summary>
        /// <param name="binder">The binder to be removed.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the binder is not of type <see cref="IBinder{T}"/>.
        /// </exception>
        public void RemoveBinder(IBinder binder) =>
            Changed -= binder.Cast<T>().SetValue;
        
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