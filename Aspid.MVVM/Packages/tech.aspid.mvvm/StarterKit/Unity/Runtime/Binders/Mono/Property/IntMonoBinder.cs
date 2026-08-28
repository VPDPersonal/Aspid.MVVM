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
        /// Also calls the base implementation: a member bound through <see cref="IReverseBinder{T}"/>
        /// for the property's own type reaches the base <c>ValueChanged</c> event rather than the
        /// matching <see cref="INumberReverseBinder"/> channel, because a class member outranks the
        /// implementation the interface carries.
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
        /// A binder that is only ever pushed to has no use for this; it exists for the ones that also listen to
        /// their component and forward what the user did — a dropdown selection, for instance. Raising
        /// <see cref="MonoBinder{TProperty}.ValueChanged"/> alone would reach an
        /// <see langword="int"/> field in the ViewModel but leave a <see langword="float"/> one silent, because
        /// the other three channels are bridged by <see cref="INumberReverseBinder"/> rather than inherited.
        /// </remarks>
        protected void RaiseNumberValueChanged(int value)
        {
            RaiseValueChanged(value);
            _channel.Raise(GetConvertedBackValue(value));
        }
    }
}
