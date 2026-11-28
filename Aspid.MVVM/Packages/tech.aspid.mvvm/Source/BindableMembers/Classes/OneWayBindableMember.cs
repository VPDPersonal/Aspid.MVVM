#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
#define ASPID_MVVM_PROFILER
#endif

using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="IBindableMember{T}"/> that pushes every value change to its binders.
    /// </summary>
    /// <remarks>
    /// A <see cref="BindMode.TwoWay"/> binder receives values without reporting changes back to the ViewModel.
    /// </remarks>
    /// <typeparam name="T">The bound value type.</typeparam>
    public sealed class OneWayBindableMember<T> : IBindableMember<T>, IBinderRemover
    {
        private T? _value;

        /// <inheritdoc/>
        public event Action<T?>? Changed;

        /// <summary>
        /// Creates a member holding <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The initial value.</param>
        public OneWayBindableMember(T? value)
        {
            _value = value;
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
        /// Gets the fixed mode <see cref="BindMode.OneWay"/>.
        /// </summary>
        public BindMode Mode => BindMode.OneWay;

        /// <inheritdoc/>
        /// <exception cref="InvalidOperationException">
        /// The binder mode does not send values to the View.
        /// </exception>
        /// <exception cref="BinderInvalidCastException">
        /// <paramref name="binder"/> implements neither <see cref="IBinder{T}"/> nor <see cref="IAnyBinder"/>.
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
                        {
                            specificBinder.SetValue(_value);
                            if (mode is BindMode.OneTime) return null;

                            Changed += specificBinder.SetValue;
                            return this;
                        }

                    case IAnyBinder anyBinder:
                        {
                            anyBinder.SetValue(_value);
                            if (mode is BindMode.OneTime) return null;

                            Changed += anyBinder.SetValue;
                            return this;
                        }

                    default: throw BinderInvalidCastException.Class<T>(binder);
                }
            }
        }

        /// <inheritdoc/>
        /// <exception cref="BinderInvalidCastException">
        /// <paramref name="binder"/> implements neither <see cref="IBinder{T}"/> nor <see cref="IAnyBinder"/>.
        /// </exception>
        void IBinderRemover.Remove(IBinder binder)
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                Changed -= binder switch
                {
                    IBinder<T> specificBinder => specificBinder.SetValue,
                    IAnyBinder anyBinder => anyBinder.SetValue,
                    _ => throw BinderInvalidCastException.Class<T>(binder)
                };
            }
        }

        /// <summary>
        /// Sets <see cref="Value"/> to <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The new value.</param>
        public void Invoke(T? value) =>
            Value = value;
    }
}
