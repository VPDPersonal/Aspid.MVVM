// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder{TProperty}">MonoBinder&lt;float&gt;</see> that binds a <see langword="float"/> property,
    /// implementing <see cref="IFloatBinder"/> to accept all numeric types
    /// and <see cref="INumberReverseBinder"/> to broadcast to all numeric event types.
    /// </summary>
    public abstract partial class FloatMonoBinder : MonoBinder<float>,
        IFloatBinder,
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
    }
}
