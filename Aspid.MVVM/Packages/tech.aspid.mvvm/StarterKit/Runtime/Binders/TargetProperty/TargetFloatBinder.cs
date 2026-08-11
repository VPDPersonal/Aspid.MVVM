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
    /// Abstract base <see cref="TargetBinder{T1, T2, T3}">TargetBinder&lt;TTarget, float, IConverter&lt;float, float&gt;&gt;</see> that binds a <see langword="float"/> property,
    /// implementing <see cref="INumberBinder"/> to accept all numeric types
    /// and <see cref="INumberReverseBinder"/> to broadcast to all numeric event types.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object that exposes the target <see langword="float"/> property.</typeparam>
    [Serializable]
    public abstract class TargetFloatBinder<TTarget> : TargetBinder<TTarget, float, Converter>,
        INumberBinder,
        INumberReverseBinder
    {
        /// <inheritdoc/>
        public event Action<int>? IntValueChanged;

        /// <inheritdoc/>
        public event Action<long>? LongValueChanged;

        /// <inheritdoc/>
        /// <remarks>
        /// Forwards to the inherited <see cref="TargetBinder{TTarget, TProperty}.ValueChanged"/>.
        /// The base class already implements <see cref="IReverseBinder{T}"/> for <see langword="float"/>, and a class
        /// member always wins over a default interface implementation, so <see cref="IReverseBinder{T}.ValueChanged"/>
        /// resolves to the inherited event rather than to the bridge declared in <see cref="INumberReverseBinder"/>.
        /// Aliasing keeps both surfaces backed by a single subscriber list.
        /// </remarks>
        public event Action<float>? FloatValueChanged
        {
            add => ValueChanged += value;
            remove => ValueChanged -= value;
        }

        /// <inheritdoc/>
        public event Action<double>? DoubleValueChanged;
        
        /// <inheritdoc/>
         protected TargetFloatBinder(TTarget target, IConverter<float, float>? converter, BindMode mode = BindMode.OneWay)
             : base(target, ConverterBridge.Float(converter), mode) { }

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
        /// <remarks>
        /// Calls <c>base.OnBound()</c> to raise the inherited
        /// <see cref="TargetBinder{TTarget, TProperty}.ValueChanged"/>, which backs both
        /// <see cref="FloatValueChanged"/> and <see cref="IReverseBinder{T}.ValueChanged"/> for <see langword="float"/>.
        /// The remaining numeric events are raised here because <see cref="INumberReverseBinder"/> bridges them
        /// to their own <see cref="IReverseBinder{T}"/> instantiations.
        /// </remarks>
        protected override void OnBound()
        {
            base.OnBound();

            if (Mode is not BindMode.OneWayToSource) return;

            var value = GetConvertedValue(Property);

            IntValueChanged?.Invoke((int)value);
            LongValueChanged?.Invoke((long)value);
            DoubleValueChanged?.Invoke(value);
        }
        
    }
}