using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="TargetBinder{T1, T2}">TargetBinder&lt;TTarget, float&gt;</see> that binds a <see langword="float"/> property,
    /// implementing <see cref="IFloatBinder"/> to accept all numeric types
    /// and <see cref="INumberReverseBinder"/> to broadcast to all numeric event types.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object that exposes the target <see langword="float"/> property.</typeparam>
    [Serializable]
    public abstract class TargetFloatBinder<TTarget> : TargetBinder<TTarget, float>,
        IFloatBinder,
        INumberReverseBinder
    {
        private NumberReverseChannel _channel;

        /// <inheritdoc/>
        /// <remarks>
        /// For deserialization only: Unity builds a serialized instance without running a constructor's arguments and
        /// assigns the fields itself.
        /// </remarks>
         protected TargetFloatBinder() { }

         protected TargetFloatBinder(TTarget target, IConverter<float, float>? converter, BindMode mode = BindMode.OneWay)
             : base(target, converter, mode) { }

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