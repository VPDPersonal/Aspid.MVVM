using System;
using System.Runtime.CompilerServices;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="TargetBinder{TTarget, float, IConverter{flaot, float}}"/> that binds a <see langword="float"/> property,
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
        public event Action<float>? FloatValueChanged;

        /// <inheritdoc/>
        public event Action<double>? DoubleValueChanged;
        
        /// <inheritdoc/>
         protected TargetFloatBinder(TTarget target, IConverter<float, float>? converter, BindMode mode = BindMode.OneWay)
             : base(target, GetConverter(converter), mode) { }

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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Converter? GetConverter(IConverter<float, float>? converter)
        {
            #if UNITY_2023_1_OR_NEWER
            return converter;
            #else
            return converter?.ToConvertSpecific();
            #endif
        }
    }
}