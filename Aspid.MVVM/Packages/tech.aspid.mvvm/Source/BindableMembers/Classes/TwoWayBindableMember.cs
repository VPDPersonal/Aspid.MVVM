#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED                                                                                                                                                                                                                    
#define PROFILER
#endif

using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Sealed <see cref="IBinderAdder"/> that supports every <see cref="BindMode"/> except <see cref="BindMode.None"/>,
    /// dispatching forward updates through <see cref="IBinder{T}"/> / <see cref="IAnyBinder"/> and reverse updates
    /// through <see cref="IReverseBinder{T}"/> / <see cref="IAnyReverseBinder"/>; additionally exposes a get/set
    /// <see cref="Value"/> and a <see cref="Changed"/> event.
    /// </summary>
    /// <typeparam name="T">The reference type of the bound value.</typeparam>
    public sealed class TwoWayBindableMember<T> : IBindableMember<T>, IBinderRemover
    {
        /// <summary>
        /// Raised when the value changes.
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
#if PROFILER
                using (this.Marker())
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
        /// <param name="value">The initial value of the bindable member.</param>
        /// <param name="setValue">
        /// The action used to set the value when the event is triggered.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="setValue"/> is <see langword="null"/>.</exception>
        public TwoWayBindableMember(T? value, Action<T?> setValue)
        {
            _value = value;
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        /// <summary>
        /// Adds the binder to the event with the current value and subscribes to the value change event.
        /// </summary>
        /// <param name="binder">The binder to bind to the event.</param>
        /// <returns>Returns itself to enable removal of the binder later, or <see langword="null"/> for <see cref="BindMode.OneTime"/>.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if <paramref name="binder"/>'s <see cref="IBinder.Mode"/> is <see cref="BindMode.None"/> or an unknown value.
        /// </exception>
        /// <exception cref="BinderInvalidCastException">
        /// Thrown if the forward direction is required (<see cref="BindMode.OneWay"/>, <see cref="BindMode.TwoWay"/>, <see cref="BindMode.OneTime"/>)
        /// and <paramref name="binder"/> is neither <see cref="IBinder{T}"/> nor <see cref="IAnyBinder"/>.
        /// </exception>
        /// <exception cref="ReverseBinderInvalidCastException{T}">
        /// Thrown if the reverse direction is required (<see cref="BindMode.TwoWay"/>, <see cref="BindMode.OneWayToSource"/>)
        /// and <paramref name="binder"/> is neither <see cref="IReverseBinder{T}"/> nor <see cref="IAnyReverseBinder"/>.
        /// </exception>
        IBinderRemover? IBinderAdder.Add(IBinder binder)
        {
#if PROFILER
            using (this.Marker())
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

        /// <summary>
        /// Removes the binder's subscription from the event based on its binding mode.
        /// </summary>
        /// <param name="binder">The binder instance to remove.</param>
        void IBinderRemover.Remove(IBinder binder)
        {
#if PROFILER
            using (this.Marker())
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
        /// Sets the current value and raises the <see cref="Changed"/> event.
        /// </summary>
        /// <param name="value">The new value to set and notify.</param>
        public void Invoke(T? value) =>
            Value = value;
        
        private void OnValueChanged(T? value)
        {
#if PROFILER
            using (this.Marker())
#endif
            {
                _setValue(value);
            }
        }
        
        private void OnObjectValueChanged(object value)
        {
#if PROFILER
            using (this.Marker())
#endif
            {
                if (value is null)
                    OnValueChanged(default);
                else if (value is not T specificValue)
                    throw new ArgumentException("Value must be of type " + typeof(T).FullName);
                else
                    OnValueChanged(specificValue);
            }
        }
    }
}
