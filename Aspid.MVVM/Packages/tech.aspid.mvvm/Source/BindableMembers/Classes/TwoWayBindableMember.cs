#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
#define ASPID_MVVM_PROFILER
#endif

using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="IBindableMember{T}"/> that pushes value changes to its binders and passes values from its reverse binders
    /// to a setter.
    /// </summary>
    /// <remarks>
    /// A value reported by a reverse binder goes only to the setter; <see cref="Value"/> changes once the ViewModel sets it.
    /// </remarks>
    /// <typeparam name="T">The bound value type.</typeparam>
    public sealed class TwoWayBindableMember<T> : IBindableMember<T>, IBinderRemover
    {
        private T? _value;
        private readonly Action<T?> _setValue;

        /// <inheritdoc/>
        public event Action<T?>? Changed;

        /// <summary>
        /// Creates a member holding <paramref name="value"/> that passes reported values to <paramref name="setValue"/>.
        /// </summary>
        /// <param name="value">The initial value.</param>
        /// <param name="setValue">Writes values reported by the View to the ViewModel.</param>
        /// <exception cref="ArgumentNullException"><paramref name="setValue"/> is <see langword="null"/>.</exception>
        public TwoWayBindableMember(
            T? value,
            Action<T?> setValue)
        {
            _value = value;
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        /// <summary>
        /// Gets or sets the value and raises <see cref="Changed"/> on every assignment.
        /// </summary>
        public T? Value
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
                }
            }
        }

        /// <summary>
        /// Gets the fixed mode <see cref="BindMode.TwoWay"/>.
        /// </summary>
        public BindMode Mode => BindMode.TwoWay;

        /// <inheritdoc/>
        /// <exception cref="InvalidOperationException">
        /// The mode of <paramref name="binder"/> is <see cref="BindMode.None"/> or undefined.
        /// </exception>
        /// <exception cref="BinderInvalidCastException">
        /// The mode reaches the View and <paramref name="binder"/> implements neither <see cref="IBinder{T}"/> nor <see cref="IAnyBinder"/>.
        /// </exception>
        /// <exception cref="ReverseBinderInvalidCastException{T}">
        /// The mode reaches the ViewModel and <paramref name="binder"/> implements neither <see cref="IReverseBinder{T}"/> nor <see cref="IAnyReverseBinder"/>.
        /// </exception>
        /// <exception cref="AggregateException">Reverse subscription and rollback both fail, in that order.</exception>
        IBinderRemover? IBinderAdder.Add(IBinder binder)
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                var mode = binder.Mode;

                switch (mode)
                {
                    case BindMode.OneWay:
                        OneWay();
                        break;

                    case BindMode.TwoWay:
                        if (binder is not IBinder<T> and not IAnyBinder)
                            throw BinderInvalidCastException.Class<T>(binder);

                        if (binder is not IReverseBinder<T> and not IAnyReverseBinder)
                            throw ReverseBinderInvalidCastException<T>.Class(binder);

                        OneWay();
                        SubscribeToSource(mode);
                        break;

                    case BindMode.OneTime:
                        switch (binder)
                        {
                            case IBinder<T> specificBinder:
                                specificBinder.SetValue(_value);
                                break;

                            case IAnyBinder anyBinder:
                                anyBinder.SetValue(_value);
                                break;

                            default: throw BinderInvalidCastException.Class<T>(binder);
                        }
                        return null;

                    case BindMode.OneWayToSource:
                        SubscribeToSource(mode);
                        break;

                    case BindMode.None:
                    default: throw new InvalidOperationException("Unsupported binding mode.");
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

            void SubscribeToSource(BindMode mode)
            {
                try
                {
                    switch (binder)
                    {
                        case IReverseBinder<T> reverseBinder:
                            reverseBinder.ValueChanged += OnValueChanged;
                            return;

                        case IAnyReverseBinder anyReverseBinder:
                            anyReverseBinder.ValueChanged += OnObjectValueChanged;
                            return;
                    }
                }
                catch (Exception subscriptionException)
                {
                    try
                    {
                        Remove(binder, mode);
                    }
                    catch (Exception rollbackException)
                    {
                        throw new AggregateException(subscriptionException, rollbackException);
                    }

                    throw;
                }

                throw ReverseBinderInvalidCastException<T>.Class(binder);
            }
        }

        /// <inheritdoc/>
        /// <exception cref="InvalidOperationException">
        /// The mode of <paramref name="binder"/> is <see cref="BindMode.None"/> or undefined.
        /// </exception>
        /// <exception cref="BinderInvalidCastException">
        /// The mode reaches the View and <paramref name="binder"/> implements neither <see cref="IBinder{T}"/>
        /// nor <see cref="IAnyBinder"/>.
        /// </exception>
        /// <exception cref="ReverseBinderInvalidCastException{T}">
        /// The mode reaches the ViewModel and <paramref name="binder"/> implements neither <see cref="IReverseBinder{T}"/>
        /// nor <see cref="IAnyReverseBinder"/>.
        /// </exception>
        void IBinderRemover.Remove(IBinder binder) =>
            Remove(binder, binder.Mode);

        private void Remove(IBinder binder, BindMode mode)
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                switch (mode)
                {
                    case BindMode.OneTime: return;

                    case BindMode.OneWay:
                        OneWay();
                        return;

                    case BindMode.TwoWay:
                        OneWay();
                        OneWayToSource();
                        return;

                    case BindMode.OneWayToSource:
                        OneWayToSource();
                        return;

                    case BindMode.None:
                    default: throw new InvalidOperationException("Unsupported binding mode.");
                }
            }
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
                    case IReverseBinder<T> reverseBinder:
                        reverseBinder.ValueChanged -= OnValueChanged;
                        break;

                    case IAnyReverseBinder anyReverseBinder:
                        anyReverseBinder.ValueChanged -= OnObjectValueChanged;
                        break;

                    default: throw ReverseBinderInvalidCastException<T>.Class(binder);
                }
            }
        }

        /// <summary>
        /// Sets <see cref="Value"/> to <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The new value.</param>
        public void Invoke(T? value) =>
            Value = value;

        private void OnValueChanged(T? value)
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                _setValue(value);
            }
        }

        private void OnObjectValueChanged(object? value)
        {
            if (value is null)
            {
                OnValueChanged(default);
            }
            else if (value is not T specificValue)
            {
                throw new ArgumentException("Value must be of type " + typeof(T).FullName);
            }
            else
            {
                OnValueChanged(specificValue);
            }
        }
    }
}
