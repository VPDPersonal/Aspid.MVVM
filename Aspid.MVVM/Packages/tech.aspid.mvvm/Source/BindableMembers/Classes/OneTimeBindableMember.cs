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
    /// <typeparam name="T">The bound value type.</typeparam>
    public sealed class OneTimeBindableMember<T> : IReadOnlyValueBindableMember<T>
    {
        /// <summary>
        /// Gets or sets the value pushed to the next binder; setting it does not update binders already bound.
        /// </summary>
        public T? Value { get; set; }
        
        /// <summary>
        /// Gets the binding mode of the member; always <see cref="BindMode.OneTime"/>.
        /// </summary>
        public BindMode Mode => BindMode.OneTime;

        /// <summary>
        /// Creates a member holding <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to push to the binder.</param>
        public OneTimeBindableMember(T? value)
        {
            Value = value;
        }

        /// <inheritdoc/>
        /// <exception cref="InvalidOperationException">
        /// The mode of <paramref name="binder"/> is neither <see cref="BindMode.OneWay"/> nor <see cref="BindMode.OneTime"/>.
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
                binder.Mode.ThrowExceptionIfNotOne();

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
