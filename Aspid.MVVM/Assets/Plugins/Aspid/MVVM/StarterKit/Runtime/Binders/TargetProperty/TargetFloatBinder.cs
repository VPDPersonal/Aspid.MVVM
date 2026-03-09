using System;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="TargetBinder{TTarget,TProperty,TConverter}"/> that binds a <see langword="float"/> property,
    /// implementing <see cref="INumberBinder"/> to accept all numeric types
    /// and <see cref="INumberReverseBinder"/> to broadcast to all numeric event types.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object that exposes the target <see langword="float"/> property.</typeparam>
    [Serializable]
    public abstract class TargetFloatBinder<TTarget> : TargetBinder<TTarget, float, Converter>,
        INumberBinder,
        INumberReverseBinder
    {
        /// <inheritdoc cref="INumberReverseBinder.IntValueChanged"/>
        public event Action<int>? IntValueChanged;

        /// <inheritdoc cref="INumberReverseBinder.LongValueChanged"/>
        public event Action<long>? LongValueChanged;

        /// <inheritdoc cref="INumberReverseBinder.FloatValueChanged"/>
        public event Action<float>? FloatValueChanged;

        /// <inheritdoc cref="INumberReverseBinder.DoubleValueChanged"/>
        public event Action<double>? DoubleValueChanged;

        /// <summary>
        /// Initializes a new instance of <see cref="TargetFloatBinder{TTarget}"/>.
        /// </summary>
        /// <param name="target">The target object whose float property is managed by this binder.</param>
        /// <param name="converter">An optional float-to-float converter applied before the value is stored.</param>
        /// <param name="mode">The binding mode to use.</param>
        protected TargetFloatBinder(TTarget target, Converter? converter, BindMode mode = BindMode.OneWay)
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
        /// Called after binding is established.
        /// In <see cref="BindMode.OneWayToSource"/> mode, broadcasts the current value to all numeric event types:
        /// <see cref="IntValueChanged"/>, <see cref="LongValueChanged"/>, <see cref="FloatValueChanged"/>, and <see cref="DoubleValueChanged"/>.
        /// </summary>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
            {
                var value = GetConvertedValue(Property);

                IntValueChanged?.Invoke((int)value);
                LongValueChanged?.Invoke((long)value);
                FloatValueChanged?.Invoke(value);
                DoubleValueChanged?.Invoke(value);
            }
        }
    }
}
