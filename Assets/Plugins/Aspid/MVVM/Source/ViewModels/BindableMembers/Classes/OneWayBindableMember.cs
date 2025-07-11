using System;

namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a one-way bindable member event that supports event notification and handling for values of a specified type.
    /// </summary>
    /// <typeparam name="T">The type of the value being handled in the bindable member event.</typeparam>
    public sealed class OneWayBindableMember<T> : OneWayBindableMember, IBindableMember<T>, IBinderRemover
    {
        /// <summary>
        /// Event triggered when the value changes.
        /// </summary>
        public event Action<T?>? Changed;

        private T? _value;
        
        /// <summary>
        /// Gets or sets the current value. Setting the value will trigger the <see cref="Changed"/> event.
        /// </summary>
        public T? Value
        {
            get => _value;
            set
            {
                _value = value;
                Changed?.Invoke(value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OneWayBindableMember{T}"/> class with the specified initial value.
        /// </summary>
        /// <param name="value">The initial value of the bindable member event.</param>
        public OneWayBindableMember(T? value)
        {
            _value = value;
        }

        /// <inheritdoc />
        /// <summary>
        /// Adds the binder to the event with the current value and subscribes to the value change event.
        /// </summary>
        /// <param name="binder">The binder to bind to the event.</param>
        /// <returns>Returns itself to enable removal of the binder later.</returns>
        /// <exception cref="Exception">
        /// Thrown if the binding mode is not <see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/>.
        /// </exception>
        IBinderRemover? IBinderAdder.Add(IBinder binder)
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (AddMarker.Auto())
#endif
            {
                var mode = binder.Mode;
            
                if (mode is not (BindMode.OneWay or BindMode.OneTime))
                    throw new InvalidOperationException($"Mode must be OneWay or OneTime. Mode = {{{mode}}}");

                switch (binder)
                {
                    case IBinder<T> specificBinder:
                        specificBinder.SetValue(_value);

                        if (mode is BindMode.OneWay) Changed += specificBinder.SetValue;
                        else return null;
                        break;
                
                    case IAnyBinder anyBinder:
                        anyBinder.SetValue(_value);
                    
                        if (mode is BindMode.OneWay) Changed += anyBinder.SetValue;
                        else return null;
                        break;
                
                    default: throw BinderInvalidCastException.Class<T>(binder);
                }
            
                return this;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Removes the binder from the event subscription.
        /// </summary>
        /// <param name="binder">The binder to remove.</param>
        void IBinderRemover.Remove(IBinder binder)
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (RemoveMarker.Auto())
#endif
            {
                if (binder.Mode is BindMode.OneTime)
                    return;

                Changed -= binder switch
                {
                    IBinder<T> specificBinder => specificBinder.SetValue,
                    IAnyBinder anyBinder => anyBinder.SetValue,
                    _ => throw BinderInvalidCastException.Class<T>(binder)
                };
            }
        }
        
        /// <summary>
        /// Triggers the Changed event with the specified value and updates the current value.
        /// </summary>
        /// <param name="value">The new value to set and notify.</param>
        public void Invoke(T? value) =>
            Value = value;
    }
    
    public abstract class OneWayBindableMember
    {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
        protected static readonly Unity.Profiling.ProfilerMarker AddMarker = new("OneWayClassEvent.Add");
        protected static readonly Unity.Profiling.ProfilerMarker RemoveMarker = new("OneWayClassEvent.Remove");
#endif
    }
}