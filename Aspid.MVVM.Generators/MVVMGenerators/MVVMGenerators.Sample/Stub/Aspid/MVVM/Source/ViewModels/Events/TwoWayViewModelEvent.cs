using System;
using System.Runtime.CompilerServices;

namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a two-way binding event that allows the ViewModel and View to notify each other of changes.
    /// </summary>
    /// <typeparam name="T">The type of the value managed by the event.</typeparam>
    public sealed class TwoWayViewModelEvent<T> : IViewModelEvent, IDisposable
    {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
        private static readonly Unity.Profiling.ProfilerMarker AddBinderMarker = new("TwoWayViewModelEvent.AddBinder");
#endif
        /// <summary>
        /// Event that is triggered when the value changes.
        /// </summary>
        public event Action<T?>? Changed;

        private readonly Action<T?> _setValue;

        public TwoWayViewModelEvent(Action<T?> setValue)
        {
            _setValue = setValue ?? throw new NullReferenceException(nameof(setValue));
        }

        /// <summary>
        /// Adds a binder to the event for binding based on the specified <see cref="BindMode"/>.
        /// </summary>
        /// <param name="binder">The binder that will manage the binding logic.</param>
        /// <returns>
        /// An interface for removing the binder from the event, or <c>null</c> if the binding mode is <see cref="BindMode.OneTime"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if reverse binding is enabled (in <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>)
        /// and <see cref="_setValue"/> is not defined.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the binder is not of a compatible type for the specified binding mode.
        /// </exception>
        public IViewModelEventRemover? AddBinder(IBinder binder)
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (AddBinderMarker.Auto())
#endif
            {
                var mode = binder.Mode;
                if (mode is BindMode.OneTime) return null;

                var isBind = false;
                if (mode is not BindMode.OneWayToSource)
                {
                    isBind = true;
                    Changed += binder.Cast<T>().SetValue;
                }

                if (mode is BindMode.TwoWay or BindMode.OneWayToSource)
                {
                    GetReverseBinder(binder).ValueChanged += _setValue;
                }
                else if (!isBind)
                {
                    ThrowInvalidOperationException(mode);
                }

                return this;
            }
        }
        
        /// <summary>
        /// Removes a binder from the event and stops reverse binding if it was set up.
        /// </summary>
        /// <param name="binder">The binder to be removed.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if reverse binding was set up (in <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>)
        /// and <see cref="_setValue"/> is not defined.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the binder is not of a compatible type for the specified binding mode.
        /// </exception>
        public void RemoveBinder(IBinder binder)
        {
            var mode = binder.Mode;
            if (mode is BindMode.OneTime) return;
            
            var isUnbind = false;

            if (mode is not BindMode.OneWayToSource)
            {
                isUnbind = true;
                Changed -= binder.Cast<T>().SetValue;
            }

            if (mode is BindMode.TwoWay or BindMode.OneWayToSource)
            {
                GetReverseBinder(binder).ValueChanged -= _setValue;
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
        private static void ThrowInvalidOperationException(BindMode mode) =>
            throw new InvalidOperationException($"Binder must be of type {typeof(IBinder<T>)} for the specified binding mode {mode}.");
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IReverseBinder<T> GetReverseBinder(IBinder binder)
        {
            if (binder.Mode is BindMode.TwoWay or BindMode.OneWayToSource || binder is not IReverseBinder<T> specificReverseBinder) 
                throw new InvalidOperationException($"Binder must be of type {typeof(IReverseBinder<T>)} and have a binding mode of {BindMode.TwoWay} or {BindMode.OneWayToSource}.");

            return specificReverseBinder;
        }
    }
}