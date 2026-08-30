using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="Binder{TProperty}">Binder&lt;int&gt;</see> that binds an <see langword="int"/> property,
    /// implementing <see cref="IIntBinder"/> to accept all numeric types
    /// and <see cref="INumberReverseBinder"/> to broadcast to all numeric event types.
    /// </summary>
    [Serializable]
    public abstract class IntBinder : Binder<int>,
        IIntBinder,
        INumberReverseBinder
    {
        private NumberReverseChannel _channel;

        /// <inheritdoc/>
        /// <remarks>
        /// For deserialization only: Unity builds a serialized instance without running a constructor's arguments and
        /// assigns the fields itself.
        /// </remarks>
        protected IntBinder() { }

        protected IntBinder(IConverter<int, int>? converter, BindMode mode = BindMode.OneWay)
            : base(converter, mode) { }

        /// <inheritdoc/>
        ref NumberReverseChannel INumberReverseBinder.Channel => ref _channel;

        /// <summary>
        /// Broadcasts the current value on every numeric channel.
        /// </summary>
        /// <remarks>
        /// Also calls the base implementation: <see cref="IReverseBinder{T}"/> bound for the property's own type
        /// resolves to the base <see cref="ValueChanged"/> event, not this channel — a class member outranks an
        /// interface implementation.
        /// </remarks>
        protected override void SendInitialValueToSource()
        {
            base.SendInitialValueToSource();
            _channel.Raise(GetConvertedBackValue(Property));
        }
    }
}
