using System;
using System.Runtime.CompilerServices;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<int, int>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterInt;
#endif

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
            : base(target, GetConverter(converter), mode) { }

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
        /// Called after binding is established.
        /// In <see cref="BindMode.OneWayToSource"/> mode, broadcasts the current value to all numeric event types:
        /// <see cref="IntValueChanged"/>, <see cref="LongValueChanged"/>, <see cref="FloatValueChanged"/>, and <see cref="DoubleValueChanged"/>.
        /// </summary>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
            {
                var value = GetConvertedValue(Property);

                IntValueChanged?.Invoke(value);
                LongValueChanged?.Invoke(value);
                FloatValueChanged?.Invoke(value);
                DoubleValueChanged?.Invoke(value);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Converter? GetConverter(IConverter<int, int>? converter)
        {
            #if UNITY_2023_1_OR_NEWER
            return converter;
            #else
            return converter?.ToConvertSpecific();
            #endif
        }
    }
}