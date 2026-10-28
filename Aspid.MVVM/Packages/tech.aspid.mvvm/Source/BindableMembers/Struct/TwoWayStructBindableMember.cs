#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
#define ASPID_MVVM_PROFILER
#endif

using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Concrete <see cref="TwoWayStructBindableMember{T, TBoxed}"/> that fixes <c>TBoxed</c> to <see cref="ValueType"/>.
    /// </summary>
    /// <typeparam name="T">The bound struct type.</typeparam>
    public sealed class TwoWayStructBindableMember<T> : TwoWayStructBindableMember<T, ValueType>
        where T : struct
    {
        /// <inheritdoc/>
        public TwoWayStructBindableMember(T value, Action<T> setValue)
            : base(value, setValue) { }
    }

    /// <summary>
    /// <see cref="IBindableMember{T}"/> that pushes value changes to its binders and passes values from its reverse binders
    /// to a setter, also through binders of <typeparamref name="TBoxed"/>.
    /// </summary>
    /// <remarks>
    /// A value reported by a reverse binder goes only to the setter; <see cref="Value"/> changes once the ViewModel sets it.
    /// </remarks>
    /// <typeparam name="T">The bound struct type.</typeparam>
    /// <typeparam name="TBoxed">The boxed form of <typeparamref name="T"/>, such as <see cref="ValueType"/> or <see cref="Enum"/>.</typeparam>
    public abstract class TwoWayStructBindableMember<T, TBoxed> : IBindableMember<T>, IBinderRemover
        where T : struct, TBoxed
        where TBoxed : class
    {
        /// <summary>
        /// Occurs when <see cref="Value"/> is set.
        /// </summary>
        public event Action<T>? Changed;
        
        private event Action<TBoxed>? BoxedChanged;

        private T _value;
        private readonly Action<T> _setValue;
        
        /// <summary>
        /// Gets or sets the current value; setting it raises <see cref="Changed"/> even for an equal value.
        /// </summary>
        public T Value
        {
            get => _value;
            set
            {
#if ASPID_MVVM_PROFILER
                using (this.Marker())
#endif
                {
                    _value = value;
                    Changed?.Invoke(value);
                    BoxedChanged?.Invoke(value);
                }
            }
        }
        
        /// <summary>
        /// Gets the binding mode of the member; always <see cref="BindMode.TwoWay"/>.
        /// </summary>
        public BindMode Mode => BindMode.TwoWay;  

        /// <summary>
        /// Creates a member holding <paramref name="value"/> that passes reported values to <paramref name="setValue"/>.
        /// </summary>
        /// <param name="value">The initial value.</param>
        /// <param name="setValue">The callback that writes a value reported by a reverse binder to the ViewModel.</param>
        /// <exception cref="ArgumentNullException"><paramref name="setValue"/> is <see langword="null"/>.</exception>
        protected TwoWayStructBindableMember(T value, Action<T> setValue)
        {
            _value = value;
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        /// <inheritdoc/>
        /// <exception cref="InvalidOperationException">
        /// The mode of <paramref name="binder"/> is <see cref="BindMode.None"/> or undefined.
        /// </exception>
        /// <exception cref="BinderInvalidCastException">
        /// The mode reaches the View (<see cref="BindMode.OneWay"/>, <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneTime"/>)
        /// and <paramref name="binder"/> implements none of <see cref="IBinder{T}"/>, <see cref="IBinder{T}"/> of
        /// <typeparamref name="TBoxed"/> and <see cref="IAnyBinder"/>.
        /// </exception>
        /// <exception cref="ReverseBinderInvalidCastException{T}">
        /// The mode reaches the ViewModel (<see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>)
        /// and <paramref name="binder"/> implements none of <see cref="IReverseBinder{T}"/>, <see cref="IReverseBinder{T}"/> of
        /// <typeparamref name="TBoxed"/> and <see cref="IAnyReverseBinder"/>.
        /// </exception>
        IBinderRemover? IBinderAdder.Add(IBinder binder)
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                switch (binder.Mode)
                {
                    case BindMode.OneWay: 
                        OneWay();
                        break;
                
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
                    case IReverseBinder<T> reverseBinder: reverseBinder.ValueChanged += OnValueChanged; break;
                    case IReverseBinder<TBoxed> structReverseBinder: structReverseBinder.ValueChanged += OnBoxedValueChanged; break;
                    case IAnyReverseBinder anyReverseBinder: anyReverseBinder.ValueChanged += OnObjectValueChanged; break;
                    default: throw ReverseBinderInvalidCastException<T>.Struct<TBoxed>(binder);
                }
            }
        }

        /// <inheritdoc/>
        /// <exception cref="BinderInvalidCastException">
        /// The mode reaches the View and <paramref name="binder"/> implements none of <see cref="IBinder{T}"/>,
        /// <see cref="IBinder{T}"/> of <typeparamref name="TBoxed"/> and <see cref="IAnyBinder"/>.
        /// </exception>
        /// <exception cref="ReverseBinderInvalidCastException{T}">
        /// The mode reaches the ViewModel and <paramref name="binder"/> implements none of <see cref="IReverseBinder{T}"/>,
        /// <see cref="IReverseBinder{T}"/> of <typeparamref name="TBoxed"/> and <see cref="IAnyReverseBinder"/>.
        /// </exception>
        void IBinderRemover.Remove(IBinder binder)
        {
#if ASPID_MVVM_PROFILER
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
                    case IReverseBinder<T> reverseBinder: reverseBinder.ValueChanged -= OnValueChanged; break;
                    case IReverseBinder<TBoxed> structReverseBinder: structReverseBinder.ValueChanged -= OnBoxedValueChanged; break;
                    case IAnyReverseBinder anyReverseBinder: anyReverseBinder.ValueChanged -= OnObjectValueChanged; break;
                    default: throw ReverseBinderInvalidCastException<T>.Struct<TBoxed>(binder);
                }
            }
        }
        
        /// <summary>
        /// Sets <see cref="Value"/> to <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The new value.</param>
        public void Invoke(T value) => 
            Value = value;
        
        private void OnValueChanged(T value)
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                _setValue(value);
            }
        }

        private void OnBoxedValueChanged(TBoxed? value)
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                if (value is null)
                    throw new ArgumentNullException(nameof(value));

                OnValueChanged((T)value);
            }
        }
        
        private void OnObjectValueChanged(object value)
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                if (value is not T specificValue)
                    throw new ArgumentException("Value must be of type " + typeof(T).FullName);

                OnValueChanged(specificValue);
            }
        }
    }
}
