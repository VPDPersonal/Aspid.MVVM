using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<int, int>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterInt;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{TComponent, int, IConverter{int, int}}"/> that binds an <see langword="int"/> property,
    /// implementing <see cref="INumberBinder"/> to accept all numeric types
    /// and <see cref="INumberReverseBinder"/> to broadcast to all numeric event types.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="UnityEngine.Component"/> that exposes the target <see langword="int"/> property.</typeparam>
    public abstract partial class ComponentIntMonoBinder<TComponent> : ComponentMonoBinder<TComponent, int, Converter>,
        INumberBinder,
        INumberReverseBinder
        where TComponent : Component
    {
        /// <inheritdoc/>
        /// <remarks>
        /// Aliases the inherited <see cref="ComponentMonoBinder{TComponent, TProperty}.ValueChanged"/>, which already
        /// backs <see cref="IReverseBinder{T}"/> for <see langword="int"/>, so both events share one subscriber list.
        /// </remarks>
        public event Action<int> IntValueChanged
        {
            add => ValueChanged += value;
            remove => ValueChanged -= value;
        }

        /// <inheritdoc/>
        public event Action<long> LongValueChanged;

        /// <inheritdoc/>
        public event Action<float> FloatValueChanged;

        /// <inheritdoc/>
        public event Action<double> DoubleValueChanged;

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(long value) =>
            base.SetValue((int)value);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(float value) =>
            base.SetValue((int)value);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(double value) =>
            base.SetValue((int)value);
        
        /// <summary>
        /// Called after binding is established.
        /// In <see cref="BindMode.OneWayToSource"/> mode, broadcasts the current value to all numeric event types:
        /// <see cref="IntValueChanged"/>, <see cref="LongValueChanged"/>, <see cref="FloatValueChanged"/>, and <see cref="DoubleValueChanged"/>.
        /// </summary>
        /// <remarks>
        /// Calls <c>base.OnBound()</c> to raise <see cref="IntValueChanged"/> via the inherited
        /// <see cref="ComponentMonoBinder{TComponent, TProperty}.ValueChanged"/>; the remaining numeric events are raised here.
        /// </remarks>
        protected override void OnBound()
        {
            base.OnBound();

            if (Mode is not BindMode.OneWayToSource) return;

            var value = GetConvertedValue(Property);

            LongValueChanged?.Invoke(value);
            FloatValueChanged?.Invoke(value);
            DoubleValueChanged?.Invoke(value);
        }
    }
}