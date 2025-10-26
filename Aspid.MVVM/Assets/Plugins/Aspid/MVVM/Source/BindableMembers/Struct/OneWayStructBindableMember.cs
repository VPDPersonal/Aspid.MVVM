using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a one-way bindable member event that supports event notification and handling for values of a specified type.
    /// </summary>
    /// <typeparam name="T">The type of the value being handled in the bindable member event.</typeparam>
    public sealed class OneWayStructBindableMember<T> : OneWayStructBindableMember<T, ValueType>
        where T : struct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneWayStructBindableMember{T}"/> class with the specified initial value.
        /// </summary>
        /// <param name="value">The initial value of the bindable member event.</param>
        public OneWayStructBindableMember(T value) 
            : base(value) { }
    }

    /// <summary>
    /// Represents a one-way bindable member event that supports event notification and handling for values of a specified type.
    /// </summary>
    /// <typeparam name="T">The type of the value being handled in the bindable member event.</typeparam>
    /// <typeparam name="TBoxed">Boxed type</typeparam>
    public abstract class OneWayStructBindableMember<T, TBoxed> : OneWayStructBindableMember, IBindableMember<T>, IBinderRemover
        where T : struct, TBoxed
        where TBoxed : class
    {
        /// <summary>
        /// Event triggered when the value changes.
        /// </summary>
        public event Action<T>? Changed;
        
        private event Action<TBoxed>? BoxedChanged;

        private T _value;
        
        /// <summary>
        /// Gets or sets the current value. Setting the value will trigger the <see cref="Changed"/> event.
        /// </summary>
        public T Value
        {
            get => _value;
            set
            {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
                using (SetValueMarker.Auto())
#endif 
                {
                    _value = value;
                    Changed?.Invoke(value);
                    BoxedChanged?.Invoke(value);
                }
            }
        }
        
        /// <summary>
        /// Gets the binding mode for this member.
        /// </summary>
        public BindMode Mode => BindMode.OneWay;

        /// <summary>
        /// Initializes a new instance of the <see cref="OneWayStructBindableMember{T,TBoxed}"/> class with the specified initial value.
        /// </summary>
        /// <param name="value">The initial value of the bindable member event.</param>
        protected OneWayStructBindableMember(T value)
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
                mode.ThrowExceptionIfNotOne();

                switch (binder)
                {
                    case IBinder<T> specificBinder:
                        specificBinder.SetValue(_value);

                        if (mode is BindMode.OneWay) Changed += specificBinder.SetValue;
                        else return null;
                        
                        return this;
                
                    case IBinder<TBoxed> structBinder:
                        structBinder.SetValue(_value);
                    
                        if (mode is BindMode.OneWay) BoxedChanged += structBinder.SetValue;
                        else return null;
                        
                        return this;
                
                    case IAnyBinder anyBinder:
                        anyBinder.SetValue(_value);

                        if (mode is BindMode.OneWay) Changed += anyBinder.SetValue;
                        else return null;
                        
                        return this;
                
                    default: throw BinderInvalidCastException.Struct<T, TBoxed>(binder);
                }
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
                switch (binder)
                {
                    case IBinder<T> specificBinder: Changed -= specificBinder.SetValue; break;
                    case IBinder<TBoxed> structBinder: BoxedChanged -= structBinder.SetValue; break;
                    case IAnyBinder anyBinder: Changed -= anyBinder.SetValue; break;
                    default: throw BinderInvalidCastException.Struct<T, TBoxed>(binder);
                }
            }
        }
        
        /// <summary>
        /// Triggers the Changed event with the specified value and updates the current value.
        /// </summary>
        /// <param name="value">The new value to set and notify.</param>
        public void Invoke(T value) => Value = value;
    }
    
    public abstract class OneWayStructBindableMember
    {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
        protected static readonly Unity.Profiling.ProfilerMarker AddMarker = new($"OneWayStructBindableMember.Add");
        protected static readonly Unity.Profiling.ProfilerMarker RemoveMarker = new($"OneWayStructBindableMember.Remove");
        protected static readonly Unity.Profiling.ProfilerMarker SetValueMarker = new($"OneWayStructBindableMember.SetValue");
#endif 
    }
}