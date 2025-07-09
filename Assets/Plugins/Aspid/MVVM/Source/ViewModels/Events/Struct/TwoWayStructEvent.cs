using System;

namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a two-way bindable member event that supports multiple binding modes and bidirectional updates.
    /// </summary>
    /// <typeparam name="T">The type of the value being handled in the bindable member event.</typeparam>
    public sealed class TwoWayStructEvent<T> : TwoWayStructEvent<T, ValueType>
        where T : struct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TwoWayStructEvent{T}"/> class with the specified value and a setter action.
        /// </summary>
        /// <param name="value">The initial value of the bindable member event.</param>
        /// <param name="setValue">
        /// The action used to set the value when the event is triggered.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="setValue"/> is <c>null</c>.</exception>
        public TwoWayStructEvent(T value, Action<T> setValue) 
            : base(value, setValue) { }
    }

    /// <summary>
    /// Represents a two-way bindable member event that supports multiple binding modes and bidirectional updates.
    /// </summary>
    /// <typeparam name="T">The type of the value being handled in the bindable member event.</typeparam>
    /// <typeparam name="TBoxed">Boxed type</typeparam>
    public abstract class TwoWayStructEvent<T, TBoxed> : TwoWayStructEvent, IBindableMemberEvent
        where T : struct, TBoxed
        where TBoxed : class
    {
        /// <summary>
        /// Event triggered when the value changes.
        /// </summary>
        public event Action<T>? Changed;
        
        private event Action<TBoxed>? BoxedChanged;

        private T _value;
        private readonly Action<T> _setValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="TwoWayStructEvent{T, TBoxed}"/> class with the specified value and a setter action.
        /// </summary>
        /// <param name="value">The initial value of the bindable member event.</param>
        /// <param name="setValue">
        /// The action used to set the value when the event is triggered.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="setValue"/> is <c>null</c>.</exception>
        protected TwoWayStructEvent(T value, Action<T> setValue)
        {
            _value = value;
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
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
        public IBindableMemberEventRemover? Add(IBinder binder)
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (AddMarker.Auto())
#endif
            {
                var mode = binder.Mode;
                if (mode is BindMode.None)
                    throw new InvalidOperationException("Mode can't be None.");

                switch (mode)
                {
                    case BindMode.OneWay: OneWay(); break;
                
                    case BindMode.TwoWay:
                        OneWay();
                        OneWayToSource();
                        break;
                
                    case BindMode.OneTime: switch (binder)
                        {
                            case IBinder<T> specificBinder: specificBinder.SetValue(_value); break;
                            case IBinder<TBoxed> structBinder: structBinder.SetValue(_value); break;
                            case IAnyBinder anyBinder: anyBinder.SetValue(_value); break;
                            default: throw BinderInvalidCastException.Struct<T, TBoxed>(binder);
                        }
                        return null;
                
                    case BindMode.OneWayToSource: OneWayToSource(); break;
                }

                return this;
            }

            void OneWay()
            {
                switch (binder)
                {
                    case IBinder<T> specificBinder: 
                        specificBinder.SetValue(_value); 
                        Changed += specificBinder.SetValue;
                        break;
                    
                    case IBinder<TBoxed> specificBinder: 
                        specificBinder.SetValue(_value); 
                        BoxedChanged += specificBinder.SetValue;
                        break;
                        
                    case IAnyBinder anyBinder:
                        anyBinder.SetValue(_value); 
                        Changed += anyBinder.SetValue;
                        break;
                        
                    default: throw BinderInvalidCastException.Struct<T, TBoxed>(binder);
                }
            }

            void OneWayToSource()
            {
                switch (binder)
                {
                    case IReverseBinder<T> reverseBinder: reverseBinder.ValueChanged += _setValue; break;
                    case IReverseBinder<TBoxed> structReverseBinder: structReverseBinder.ValueChanged += SetBoxedValue; break;
                    default: throw ReverseBinderInvalidCastException<T>.Struct<TBoxed>(binder);
                }
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Removes the binder's subscription from the event based on its binding mode.
        /// </summary>
        /// <param name="binder">The binder instance to remove.</param>
        public void Remove(IBinder binder)
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (RemoveMarker.Auto())
#endif
            {
                switch (binder.Mode)
                {
                    case BindMode.OneTime: return;
                    case BindMode.OneWay: OneWay(); return;
                
                    case BindMode.TwoWay:
                        OneWay();
                        OneWayToSource();
                        return;
                
                    case BindMode.OneWayToSource: OneWayToSource(); return;
                }
            
                return;
            }

            void OneWay()
            {
                switch (binder)
                {
                    case IBinder<T> specificBinder: Changed -= specificBinder.SetValue; break;
                    case IBinder<TBoxed> structBinder: BoxedChanged -= structBinder.SetValue; break;
                    case IAnyBinder anyBinder: Changed -= anyBinder.SetValue; break;
                    default: throw BinderInvalidCastException.Struct<T, TBoxed>(binder);
                }
            }
            
            void OneWayToSource()
            {
                switch (binder)
                {
                    case IReverseBinder<T> reverseBinder: reverseBinder.ValueChanged -= _setValue; break;
                    case IReverseBinder<TBoxed> structReverseBinder: structReverseBinder.ValueChanged -= SetBoxedValue; break;
                    default: throw ReverseBinderInvalidCastException<T>.Struct<TBoxed>(binder);
                }
            }
        }

        /// <summary>
        /// Triggers the Changed event with the specified value and updates the current value.
        /// </summary>
        /// <param name="value">The new value to set and notify.</param>
        public void Invoke(T value)
        {
            _value = value;
            Changed?.Invoke(value);
            BoxedChanged?.Invoke(value);
        }
        
        private void SetBoxedValue(TBoxed? value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));
            
            _setValue.Invoke((T)value);
        }
    }
    
    public abstract class TwoWayStructEvent
    {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
        protected static readonly Unity.Profiling.ProfilerMarker AddMarker = new("TwoWayStructEvent.Add");
        protected static readonly Unity.Profiling.ProfilerMarker RemoveMarker = new("TwoWayStructEvent.Remove");
#endif
    }
}