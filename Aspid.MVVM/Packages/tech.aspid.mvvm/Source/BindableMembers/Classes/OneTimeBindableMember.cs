#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
#define ASPID_MVVM_PROFILER
#endif

using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="IReadOnlyValueBindableMember{T}"/> that pushes its value to a binder once and keeps no subscription.
    /// </summary>
    /// <remarks>
    /// A <see cref="BindMode.TwoWay"/> binder receives the initial value without reporting changes back to the ViewModel.
    /// </remarks>
    /// <typeparam name="T">The bound value type.</typeparam>
    public sealed class OneTimeBindableMember<T> : IReadOnlyValueBindableMember<T>
    {
        /// <summary>
        /// Gets or sets the value sent to each binder when it attaches.
        /// </summary>
        public T? Value { get; set; }

        /// <summary>
        /// Creates a member holding <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to push to the binder.</param>
        public OneTimeBindableMember(T? value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets the fixed mode <see cref="BindMode.OneTime"/>.
        /// </summary>
        public BindMode Mode => BindMode.OneTime;

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
                binder.Mode.EnsureForward();

                switch (binder)
                {
                    case IBinder<T> specificBinder:
                        specificBinder.SetValue(Value);
                        break;

                    case IAnyBinder anyBinder:
                        anyBinder.SetValue(Value);
                        break;

                    default: throw BinderInvalidCastException.Class<T>(binder);
                }

                return null;
            }
        }
    }
}
