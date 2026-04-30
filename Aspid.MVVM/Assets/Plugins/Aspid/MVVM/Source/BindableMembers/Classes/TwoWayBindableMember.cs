using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a two-way bindable member event that supports multiple binding modes and bidirectional updates.
    /// </summary>
    /// <typeparam name="T">The type of the value being handled in the bindable member event.</typeparam>
    public sealed class TwoWayBindableMember<T> : IBindableMember<T>, IBinderRemover
    {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
        private static readonly Unity.Profiling.ProfilerMarker _addMarker = new(name: $"TwoWayBindableMember<{typeof(T).Name}>.Add");
        private static readonly Unity.Profiling.ProfilerMarker _removeMarker = new(name: $"TwoWayBindableMember<{typeof(T).Name}>.Remove");
        private static readonly Unity.Profiling.ProfilerMarker _setValueMarker = new(name: $"TwoWayBindableMember<{typeof(T).Name}>.SetValue");
        private static readonly Unity.Profiling.ProfilerMarker _onValueChangedMarker = new(name: $"TwoWayBindableMember<{typeof(T).Name}>.OnValueChanged");
        private static readonly Unity.Profiling.ProfilerMarker _onObjectValueChangedMarker = new(name: $"TwoWayBindableMember<{typeof(T).Name}>.OnObjectValueChanged");
#endif
        
        /// <summary>
        /// Event triggered when the value changes.
        /// </summary>
        public event Action<T?>? Changed;

        private T? _value;
        private readonly Action<T?> _setValue;
        
        /// <summary>
        /// Gets or sets the current value. Setting the value will trigger the <see cref="Changed"/> event.
        /// </summary>
        public T? Value
        {
            get => _value;
            set
            {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
                using (_setValueMarker.Auto())
#endif
                {
                    _value = value;
                    Changed?.Invoke(value);
                }
            }
        }
        
        /// <summary>
        /// Gets the binding mode for this member.
        /// </summary>
        public BindMode Mode => BindMode.TwoWay;  

        /// <summary>
        /// Initializes a new instance of the <see cref="TwoWayBindableMember{T}"/> class with the specified value and a setter action.
        /// </summary>
        /// <param name="value">The initial value of the bindable member event.</param>
        /// <param name="setValue">
        /// The action used to set the value when the event is triggered.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="setValue"/> is <c>null</c>.</exception>
        public TwoWayBindableMember(T? value, Action<T?> setValue)
        {
            _value = value;
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        /// <inheritdoc/>
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
            using (_addMarker.Auto())
#endif
            {
                switch (binder.Mode)
                {
                    case BindMode.OneWay: OneWay(); break;
                
                    case BindMode.TwoWay:
                        OneWay();
                        OneWayToSource();
                        break;
                
                    case BindMode.OneTime: switch (binder)
                        {
                            case IBinder<T> specificBinder: specificBinder.SetValue(_value); break;
                            case IAnyBinder anyBinder: anyBinder.SetValue(_value); break;
                            default: throw BinderInvalidCastException.Class<T>(binder);
                        }

                        return null;
                
                    case BindMode.OneWayToSource: OneWayToSource(); break;

                    case BindMode.None:
                    default:
                    throw new InvalidOperationException("Mode can't be None.");
                }
            }
            
            return this;

            void OneWay()
            {
                switch (binder)
                {
                    case IBinder<T> specificBinder: 
                        specificBinder.SetValue(_value); 
                        Changed += specificBinder.SetValue;
                        break;
                        
                    case IAnyBinder anyBinder:
                        anyBinder.SetValue(_value); 
                        Changed += anyBinder.SetValue;
                        break;
                        
                    default: throw BinderInvalidCastException.Class<T>(binder);
                }
            }

            void OneWayToSource()
            {
                switch (binder)
                {
                    case IReverseBinder<T> reverseBinder: reverseBinder.ValueChanged += OnValueChanged; break;
                    case IAnyReverseBinder anyReverseBinder: anyReverseBinder.ValueChanged += OnObjectValueChanged; break;
                    default: throw ReverseBinderInvalidCastException<T>.Class(binder);
                }
            }
        }

        /// <inheritdoc/>
        /// <summary>
        /// Removes the binder's subscription from the event based on its binding mode.
        /// </summary>
        /// <param name="binder">The binder instance to remove.</param>
        void IBinderRemover.Remove(IBinder binder)
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (_removeMarker.Auto())
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
            }
            
            return;

            void OneWay() => Changed -= binder switch
            {
                IBinder<T> specificBinder => specificBinder.SetValue,
                IAnyBinder anyBinder => anyBinder.SetValue,
                _ => throw BinderInvalidCastException.Class<T>(binder)
            };
            
            void OneWayToSource()
            {
                switch (binder)
                {
                    case IReverseBinder<T> reverseBinder: reverseBinder.ValueChanged -= OnValueChanged; break;
                    case IAnyReverseBinder anyReverseBinder: anyReverseBinder.ValueChanged -= OnObjectValueChanged; break;
                    default: throw ReverseBinderInvalidCastException<T>.Class(binder);
                }
            }
        }
        
        /// <summary>
        /// Triggers the Changed event with the specified value and updates the current value.
        /// </summary>
        /// <param name="value">The new value to set and notify.</param>
        public void Invoke(T? value) => Value = value;
        
        private void OnValueChanged(T? value)
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (_onValueChangedMarker.Auto())
#endif
            {
                _setValue(value);
            }
        }
        
        private void OnObjectValueChanged(object value)
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (_onObjectValueChangedMarker.Auto())
#endif
            {
                if (value is not T specificValue)
                    throw new ArgumentException("Value must be of type " + typeof(T).FullName);

                OnValueChanged(specificValue);
            }
        }
    }
}