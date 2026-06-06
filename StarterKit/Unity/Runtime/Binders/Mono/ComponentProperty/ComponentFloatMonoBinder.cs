using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{TComponent, float, IConverter{float, float}}"/> that binds a <see langword="float"/> property,
    /// implementing <see cref="INumberBinder"/> to accept all numeric types
    /// and <see cref="INumberReverseBinder"/> to broadcast to all numeric event types.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="UnityEngine.Component"/> that exposes the target <see langword="float"/> property.</typeparam>
    public abstract partial class ComponentFloatMonoBinder<TComponent> : ComponentMonoBinder<TComponent, float, Converter>,
        INumberBinder,
        INumberReverseBinder
        where TComponent : Component
    {
        /// <inheritdoc/>
        public event Action<int> IntValueChanged;

        /// <inheritdoc/>
        public event Action<long> LongValueChanged;

        /// <inheritdoc/>
        public event Action<float> FloatValueChanged;

        /// <inheritdoc/>
        public event Action<double> DoubleValueChanged;

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(int value) =>
            base.SetValue(value);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(long value) =>
            base.SetValue(value);

        /// <inheritdoc/>
        [BinderLog]
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