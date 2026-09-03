// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder{TProperty}">MonoBinder&lt;int&gt;</see> that binds an <see langword="int"/> property,
    /// accepting every numeric type via <see cref="IIntBinder"/> and reporting to every numeric type via <see cref="INumberReverseBinder"/>.
    /// </summary>
    public abstract class IntMonoBinder : MonoBinder<int>,
        IIntBinder,
        INumberReverseBinder
    {
        private NumberReverseChannel _channel;

        /// <inheritdoc/>
        ref NumberReverseChannel INumberReverseBinder.Channel => ref _channel;

        /// <summary>
        /// Sends the current value on every numeric channel.
        /// </summary>
        /// <remarks>
        /// Also calls the base implementation: <see cref="IReverseBinder{T}"/> for the property's own type resolves to
        /// the class-level <c>ValueChanged</c>, not to the channel.
        /// </remarks>
        protected override void SendInitialValueToSource()
        {
            base.SendInitialValueToSource();
            _channel.Raise(GetConvertedBackValue(Property));
        }

        /// <summary>
        /// Sends <paramref name="value"/> to the ViewModel on every numeric channel, after <c>GetConvertedBackValue</c>.
        /// </summary>
        /// <param name="value">The value to send.</param>
        /// <remarks>
        /// <c>RaiseValueChanged</c> alone reaches only a ViewModel member of the property's own type.
        /// </remarks>
        protected void RaiseNumberValueChanged(int value)
        {
            RaiseValueChanged(value);
            _channel.Raise(GetConvertedBackValue(value));
        }
    }
}
