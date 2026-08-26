using System;
using Converter = Aspid.MVVM.StarterKit.IConverter<int, int>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="TargetBinder{TTarget, int, IConverter{int, int}}"/> that binds an <see langword="int"/> property,
    /// implementing <see cref="INumberBinder"/> to accept all numeric types
    /// and <see cref="INumberReverseBinder"/> to broadcast to all numeric event types.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object that exposes the target <see langword="int"/> property.</typeparam>
    [Serializable]
    public abstract class TargetIntBinder<TTarget> : TargetBinder<TTarget, int, Converter>,
        INumberBinder,
        INumberReverseBinder
    {
        /// <inheritdoc/>
        public event Action<int>? IntValueChanged;

        /// <inheritdoc/>
        public event Action<long>? LongValueChanged;

        /// <inheritdoc/>
        public event Action<float>? FloatValueChanged;

        /// <inheritdoc/>
        public event Action<double>? DoubleValueChanged;

        /// <inheritdoc/>
        protected TargetIntBinder(TTarget target, IConverter<int, int>? converter, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }

        /// <summary>
        /// Sets the target int property from a <see cref="long"/> value, truncating to <see cref="int"/>.
        /// </summary>
        /// <param name="value">The long value to apply.</param>
        public void SetValue(long value) =>
            base.SetValue((int)value);

        /// <summary>
        /// Sets the target int property from a <see cref="float"/> value, truncating to <see cref="int"/>.
        /// </summary>
        /// <param name="value">The float value to apply.</param>
        public void SetValue(float value) =>
            base.SetValue((int)value);

        /// <summary>
        /// Sets the target int property from a <see cref="double"/> value, truncating to <see cref="int"/>.
        /// </summary>
        /// <param name="value">The double value to apply.</param>
        public void SetValue(double value) =>
            base.SetValue((int)value);

        /// <summary>
        /// Broadcasts the current value to all numeric event types:
        /// <see cref="IntValueChanged"/>, <see cref="LongValueChanged"/>, <see cref="FloatValueChanged"/>, and <see cref="DoubleValueChanged"/>.
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

            var value = GetConvertedBackValue(Property);

            IntValueChanged?.Invoke(value);
            LongValueChanged?.Invoke(value);
            FloatValueChanged?.Invoke(value);
            DoubleValueChanged?.Invoke(value);
        }

    }
}