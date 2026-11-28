#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
#define ASPID_MVVM_PROFILER
#endif

using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="IReadOnlyBindableMember{T}"/> that passes values from its reverse binders to a setter.
    /// </summary>
    /// <typeparam name="T">The bound value type.</typeparam>
    public sealed class OneWayToSourceBindableMember<T> : IReadOnlyBindableMember<T>, IBinderRemover
    {
        private readonly Action<T?> _setValue;

        /// <inheritdoc/>
        public event Action<T?>? Changed;
        
        /// <summary>
        /// Gets the last value reported by a reverse binder.
        /// </summary>
        public T? Value { get; private set; }

        /// <summary>
        /// Creates a member that passes reported values to <paramref name="setValue"/>.
        /// </summary>
        /// <param name="setValue">Writes values reported by the View to the ViewModel.</param>
        /// <exception cref="ArgumentNullException"><paramref name="setValue"/> is <see langword="null"/>.</exception>
        public OneWayToSourceBindableMember(Action<T?> setValue)
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
        /// <paramref name="binder"/> implements neither <see cref="IReverseBinder{T}"/> nor <see cref="IAnyReverseBinder"/>.
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

                        case IAnyReverseBinder anyReverseBinder:
                            anyReverseBinder.ValueChanged += OnObjectValueChanged;
                            break;

                        default: throw ReverseBinderInvalidCastException<T>.Class(binder);
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
        /// <paramref name="binder"/> implements neither <see cref="IReverseBinder{T}"/> nor <see cref="IAnyReverseBinder"/>.
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

                    case IAnyReverseBinder anyReverseBinder:
                        anyReverseBinder.ValueChanged -= OnObjectValueChanged;
                        break;

                    default: throw ReverseBinderInvalidCastException<T>.Class(binder);
                }
            }
        }

        private void OnValueChanged(T? value)
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
