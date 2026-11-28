#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
#define ASPID_MVVM_PROFILER
#endif

using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Concrete <see cref="OneWayToSourceStructBindableMember{T, TBoxed}"/> that fixes <c>TBoxed</c> to <see cref="ValueType"/>.
    /// </summary>
    /// <typeparam name="T">The bound struct type.</typeparam>
    public sealed class OneWayToSourceStructBindableMember<T> : OneWayToSourceStructBindableMember<T, ValueType>
        where T : struct
    {
        /// <inheritdoc/>
        public OneWayToSourceStructBindableMember(Action<T> setValue)
            : base(setValue) { }
    }

    /// <summary>
    /// <see cref="IReadOnlyBindableMember{T}"/> that passes values from its reverse binders to a setter, unboxing them from an
    /// <see cref="IReverseBinder{T}"/> of <typeparamref name="TBoxed"/>.
    /// </summary>
    /// <typeparam name="T">The bound struct type.</typeparam>
    /// <typeparam name="TBoxed">The boxed form of <typeparamref name="T"/>, such as <see cref="ValueType"/> or <see cref="Enum"/>.</typeparam>
    public abstract class OneWayToSourceStructBindableMember<T, TBoxed> : IReadOnlyBindableMember<T>, IBinderRemover
        where T : struct, TBoxed
        where TBoxed : class
    {
        private readonly Action<T> _setValue;

        /// <inheritdoc/>
        public event Action<T>? Changed;
        
        /// <summary>
        /// Gets the last value reported by a reverse binder.
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Creates a member that passes reported values to <paramref name="setValue"/>.
        /// </summary>
        /// <param name="setValue">Writes values reported by the View to the ViewModel.</param>
        /// <exception cref="ArgumentNullException"><paramref name="setValue"/> is <see langword="null"/>.</exception>
        protected OneWayToSourceStructBindableMember(Action<T> setValue)
        {
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        /// <summary>
        /// Gets the fixed mode <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        public BindMode Mode => BindMode.OneWayToSource;

        /// <inheritdoc/>
        /// <exception cref="InvalidOperationException">
        /// The binder mode does not send values to the ViewModel.
        /// </exception>
        /// <exception cref="ReverseBinderInvalidCastException{T}">
        /// <paramref name="binder"/> implements none of <see cref="IReverseBinder{T}"/>, <see cref="IReverseBinder{T}"/> of
        /// <typeparamref name="TBoxed"/> and <see cref="IAnyReverseBinder"/>.
        /// </exception>
        IBinderRemover IBinderAdder.Add(IBinder binder)
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                binder.Mode.EnsureReverse();

                try
                {
                    switch (binder)
                    {
                        case IReverseBinder<T> reverseBinder:
                            reverseBinder.ValueChanged += OnValueChanged;
                            break;

                        case IReverseBinder<TBoxed> structReverseBinder:
                            structReverseBinder.ValueChanged += OnBoxedValueChanged;
                            break;

                        case IAnyReverseBinder anyReverseBinder:
                            anyReverseBinder.ValueChanged += OnObjectValueChanged;
                            break;

                        default: throw ReverseBinderInvalidCastException<T>.Struct<TBoxed>(binder);
                    }
                }
                catch
                {
                    try
                    {
                        ((IBinderRemover)this).Remove(binder);
                    }
                    catch
                    {
                        // Do not replace the original exception with a cleanup failure.
                    }

                    throw;
                }

                return this;
            }
        }

        /// <inheritdoc/>
        /// <exception cref="ReverseBinderInvalidCastException{T}">
        /// <paramref name="binder"/> implements none of <see cref="IReverseBinder{T}"/>, <see cref="IReverseBinder{T}"/> of
        /// <typeparamref name="TBoxed"/> and <see cref="IAnyReverseBinder"/>.
        /// </exception>
        void IBinderRemover.Remove(IBinder binder)
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                switch (binder)
                {
                    case IReverseBinder<T> reverseBinder:
                        reverseBinder.ValueChanged -= OnValueChanged;
                        break;

                    case IReverseBinder<TBoxed> structReverseBinder:
                        structReverseBinder.ValueChanged -= OnBoxedValueChanged;
                        break;

                    case IAnyReverseBinder anyReverseBinder:
                        anyReverseBinder.ValueChanged -= OnObjectValueChanged;
                        break;

                    default: throw ReverseBinderInvalidCastException<T>.Struct<TBoxed>(binder);
                }
            }
        }

        private void OnValueChanged(T value)
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                Value = value;
                _setValue(value);
                Changed?.Invoke(value);
            }
        }

        private void OnBoxedValueChanged(TBoxed? value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            OnValueChanged((T)value);
        }

        private void OnObjectValueChanged(object value)
        {
            if (value is not T specificValue)
                throw new ArgumentException("Value must be of type " + typeof(T).FullName);

            OnValueChanged(specificValue);
        }
    }
}
