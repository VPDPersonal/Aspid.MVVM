#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
#define ASPID_MVVM_PROFILER
#endif

using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Concrete <see cref="OneWayStructBindableMember{T, TBoxed}"/> that fixes <c>TBoxed</c> to <see cref="ValueType"/>.
    /// </summary>
    /// <typeparam name="T">The bound struct type.</typeparam>
    public sealed class OneWayStructBindableMember<T> : OneWayStructBindableMember<T, ValueType>
        where T : struct
    {
        /// <inheritdoc/>
        public OneWayStructBindableMember(T value)
            : base(value) { }
    }

    /// <summary>
    /// <see cref="IBindableMember{T}"/> that pushes every value change to its binders, boxing it for an
    /// <see cref="IBinder{T}"/> of <typeparamref name="TBoxed"/>.
    /// </summary>
    /// <remarks>
    /// A <see cref="BindMode.TwoWay"/> binder receives values without reporting changes back to the ViewModel.
    /// </remarks>
    /// <typeparam name="T">The bound struct type.</typeparam>
    /// <typeparam name="TBoxed">The boxed form of <typeparamref name="T"/>, such as <see cref="ValueType"/> or <see cref="Enum"/>.</typeparam>
    public abstract class OneWayStructBindableMember<T, TBoxed> : IBindableMember<T>, IBinderRemover
        where T : struct, TBoxed
        where TBoxed : class
    {
        private T _value;
        
        /// <inheritdoc/>
        public event Action<T>? Changed;

        private event Action<TBoxed>? BoxedChanged;

        /// <summary>
        /// Creates a member holding <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The initial value.</param>
        protected OneWayStructBindableMember(T value)
        {
            _value = value;
        }

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
        /// Gets the binding mode of the member; always <see cref="BindMode.OneWay"/>.
        /// </summary>
        public BindMode Mode => BindMode.OneWay;

        /// <inheritdoc/>
        /// <exception cref="InvalidOperationException">
        /// The mode of <paramref name="binder"/> is not <see cref="BindMode.OneWay"/>, <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneTime"/>.
        /// </exception>
        /// <exception cref="BinderInvalidCastException">
        /// <paramref name="binder"/> implements none of <see cref="IBinder{T}"/>, <see cref="IBinder{T}"/> of
        /// <typeparamref name="TBoxed"/> and <see cref="IAnyBinder"/>.
        /// </exception>
        IBinderRemover? IBinderAdder.Add(IBinder binder)
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                var mode = binder.Mode;
                mode.EnsureForward();

                switch (binder)
                {
                    case IBinder<T> specificBinder:
                        specificBinder.SetValue(_value);
                        if (mode is BindMode.OneTime) return null;

                        Changed += specificBinder.SetValue;
                        return this;

                    case IBinder<TBoxed> structBinder:
                        structBinder.SetValue(_value);
                        if (mode is BindMode.OneTime) return null;

                        BoxedChanged += structBinder.SetValue;
                        return this;

                    case IAnyBinder anyBinder:
                        anyBinder.SetValue(_value);
                        if (mode is BindMode.OneTime) return null;

                        Changed += anyBinder.SetValue;
                        return this;

                    default: throw BinderInvalidCastException.Struct<T, TBoxed>(binder);
                }
            }
        }

        /// <inheritdoc/>
        /// <exception cref="BinderInvalidCastException">
        /// <paramref name="binder"/> implements none of <see cref="IBinder{T}"/>, <see cref="IBinder{T}"/> of
        /// <typeparamref name="TBoxed"/> and <see cref="IAnyBinder"/>.
        /// </exception>
        void IBinderRemover.Remove(IBinder binder)
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                switch (binder)
                {
                    case IBinder<T> specificBinder:
                        Changed -= specificBinder.SetValue;
                        break;

                    case IBinder<TBoxed> structBinder:
                        BoxedChanged -= structBinder.SetValue;
                        break;

                    case IAnyBinder anyBinder:
                        Changed -= anyBinder.SetValue;
                        break;

                    default: throw BinderInvalidCastException.Struct<T, TBoxed>(binder);
                }
            }
        }

        /// <summary>
        /// Sets <see cref="Value"/> to <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The new value.</param>
        public void Invoke(T value) =>
            Value = value;
    }
}
