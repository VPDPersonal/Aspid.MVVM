// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder{TProperty}">MonoBinder&lt;int&gt;</see> that binds an <see langword="int"/> property,
    /// implementing <see cref="IIntBinder"/> to accept all numeric types
    /// and <see cref="INumberReverseBinder"/> to broadcast to all numeric event types.
    /// </summary>
    public abstract class IntMonoBinder : MonoBinder<int>,
        IIntBinder,
        INumberReverseBinder
    {
        private NumberReverseChannel _channel;

        /// <inheritdoc/>
        ref NumberReverseChannel INumberReverseBinder.Channel => ref _channel;

        /// <summary>
        /// Broadcasts the current value on every numeric channel.
        /// </summary>
        /// <remarks>
        /// Also calls the base implementation: <see cref="IReverseBinder{T}"/> bound for the property's own type
        /// resolves to the base <c>ValueChanged</c> event, not this channel — a class member outranks an interface
        /// implementation.
        /// </remarks>
        protected override void SendInitialValueToSource()
        {
            base.SendInitialValueToSource();
            _channel.Raise(GetConvertedBackValue(Property));
        }

        /// <summary>
        /// Sends <paramref name="value"/> to the ViewModel on every numeric channel this binder exposes.
        /// </summary>
        /// <param name="value">The value to send, before conversion.</param>
        /// <remarks>
        /// Raising <see cref="MonoBinder{TProperty}.ValueChanged"/> alone would reach an
        /// <see langword="int"/> field in the ViewModel but leave a <see langword="float"/> one silent, since the
        /// other three channels are bridged by <see cref="INumberReverseBinder"/> rather than inherited.
        /// </remarks>
        protected void RaiseNumberValueChanged(int value)
        {
            RaiseValueChanged(value);
            _channel.Raise(GetConvertedBackValue(value));
        }
    }
}
