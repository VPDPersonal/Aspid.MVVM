using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="TargetBinderWithConverter{T1, T2}">TargetBinderWithConverter&lt;TTarget, float&gt;</see> that binds a <see langword="float"/> property,
    /// implementing <see cref="INumberBinder"/> to accept all numeric types
    /// and <see cref="INumberReverseBinder"/> to broadcast to all numeric event types.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object that exposes the target <see langword="float"/> property.</typeparam>
    [Serializable]
    public abstract class TargetFloatBinder<TTarget> : TargetBinderWithConverter<TTarget, float>,
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
         protected TargetFloatBinder(TTarget target, IConverter<float, float>? converter, BindMode mode = BindMode.OneWay)
             : base(target, converter, mode) { }

        /// <summary>
        /// Sets the target float property from an <see cref="int"/> value.
        /// </summary>
        /// <param name="value">The integer value to apply, widened to <see cref="float"/>.</param>
        public void SetValue(int value) =>
            base.SetValue(value);

        /// <summary>
        /// Sets the target float property from a <see cref="long"/> value.
        /// </summary>
        /// <param name="value">The long value to apply, widened to <see cref="float"/>.</param>
        public void SetValue(long value) =>
            base.SetValue(value);

        /// <summary>
        /// Sets the target float property from a <see cref="double"/> value.
        /// </summary>
        /// <param name="value">The double value to apply, narrowed to <see cref="float"/>.</param>
        public void SetValue(double value) =>
            base.SetValue((float)value);

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

            IntValueChanged?.Invoke((int)value);
            LongValueChanged?.Invoke((long)value);
            FloatValueChanged?.Invoke(value);
            DoubleValueChanged?.Invoke(value);
        }

    }
}