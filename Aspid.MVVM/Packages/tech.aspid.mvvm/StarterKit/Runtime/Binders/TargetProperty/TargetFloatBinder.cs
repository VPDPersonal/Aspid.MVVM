using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="TargetBinder{T1, T2}">TargetBinder&lt;TTarget, float&gt;</see> that binds a <see langword="float"/> property,
    /// accepting every numeric type via <see cref="IFloatBinder"/> and reporting to every numeric type via <see cref="INumberReverseBinder"/>.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object that exposes the bound property.</typeparam>
    [Serializable]
    public abstract class TargetFloatBinder<TTarget> : TargetBinder<TTarget, float>,
        IFloatBinder,
        INumberReverseBinder
    {
        private NumberReverseChannel _channel;

        /// <remarks>
        /// For deserialization only: Unity assigns the fields itself.
        /// </remarks>
        protected TargetFloatBinder() { }

        /// <inheritdoc/>
        protected TargetFloatBinder(TTarget target, IConverter<float, float>? converter, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }

        /// <inheritdoc/>
        ref NumberReverseChannel INumberReverseBinder.Channel => ref _channel;

        /// <summary>
        /// Sends the current value on every numeric channel.
        /// </summary>
        /// <remarks>
        /// Also calls the base implementation: <see cref="IReverseBinder{T}"/> for the property's own type resolves to
        /// the class-level <see cref="TargetBinder{TTarget, TProperty}.ValueChanged"/>, not to the channel.
        /// </remarks>
        protected override void SendInitialValueToSource()
        {
            base.SendInitialValueToSource();
            _channel.Raise(GetConvertedBackValue(Property));
        }
    }
}
