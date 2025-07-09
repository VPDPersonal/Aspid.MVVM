using System;

namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a bindable member event that supports one-way-to-source bindings.
    /// </summary>
    /// <typeparam name="T">The type of the value to be handled in the bindable member event.</typeparam>
    public sealed class OneWayToSourceStructEvent<T> : OneWayToSourceStructEvent<T, ValueType>
        where T : struct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneWayToSourceStructEvent{T}"/> class with the specified value setter action.
        /// </summary>
        /// <param name="setValue">
        /// The action used to set the value when the event is triggered.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="setValue"/> is <c>null</c>.</exception>
        public OneWayToSourceStructEvent(Action<T> setValue) 
            : base(setValue) { }
    }

    /// <summary>
    /// Represents a bindable member event that supports one-way-to-source bindings.
    /// </summary>
    /// <typeparam name="T">The type of the value to be handled in the bindable member event.</typeparam>
    /// <typeparam name="TBoxed">Boxed type</typeparam>
    public abstract class OneWayToSourceStructEvent<T, TBoxed> : OneWayToSourceStructEvent, IBindableMemberEvent
        where T : struct, TBoxed
        where TBoxed : class
    {
        private readonly Action<T> _setValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="OneWayToSourceStructEvent{T, TBoxed}"/> class with the specified value setter action.
        /// </summary>
        /// <param name="setValue">
        /// The action used to set the value when the event is triggered.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="setValue"/> is <c>null</c>.</exception>
        protected OneWayToSourceStructEvent(Action<T> setValue)
        {
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        /// <inheritdoc />
        /// <summary>
        /// Adds a binder to the event if it supports reverse binding for one-way-to-source or two-way modes.
        /// </summary>
        /// <param name="binder">The binder to bind to the event.</param>
        /// <returns>Returns itself to allow unsubscription later.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the <paramref name="binder"/> does not have a valid binding mode or is not of type <see cref="IReverseBinder{T}"/>.
        /// </exception>
        public IBindableMemberEventRemover Add(IBinder binder)
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (AddMarker.Auto())
#endif
            {
                var mode = binder.Mode;
            
                if (mode is not (BindMode.OneWayToSource or BindMode.TwoWay))
                    throw new InvalidOperationException($"Mode must be OneWayToSource. Mode = {{{mode}}}");

                switch (binder)
                {
                    case IReverseBinder<T> reverseBinder: reverseBinder.ValueChanged += _setValue; break;
                    case IReverseBinder<TBoxed> structReverseBinder: structReverseBinder.ValueChanged += SetBoxedValue; break;
                    default: throw ReverseBinderInvalidCastException<T>.Struct<TBoxed>(binder);
                }
            
                return this;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Removes the binder's subscription from the event.
        /// </summary>
        /// <param name="binder">The binder to unsubscribe from the event.</param>
        public void Remove(IBinder binder)
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (RemoveMarker.Auto())
#endif
            {
                switch (binder)
                {
                    case IReverseBinder<T> reverseBinder: reverseBinder.ValueChanged -= _setValue; break;
                    case IReverseBinder<TBoxed> structReverseBinder: structReverseBinder.ValueChanged -= SetBoxedValue; break;
                    default: throw ReverseBinderInvalidCastException<T>.Struct<TBoxed>(binder);
                }
            }
        }

        private void SetBoxedValue(TBoxed? value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));
            
            _setValue.Invoke((T)value);
        }
    }
    
    public abstract class OneWayToSourceStructEvent
    {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
        protected static readonly Unity.Profiling.ProfilerMarker AddMarker = new("OneWayToSourceStructEvent.Add");
        protected static readonly Unity.Profiling.ProfilerMarker RemoveMarker = new("OneWayToSourceStructEvent.Remove");
#endif
    }
}