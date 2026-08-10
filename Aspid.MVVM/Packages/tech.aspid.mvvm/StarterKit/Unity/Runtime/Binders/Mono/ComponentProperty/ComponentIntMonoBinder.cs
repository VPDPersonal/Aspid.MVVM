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
        /// Forwards to the inherited <see cref="ComponentMonoBinder{TComponent, TProperty}.ValueChanged"/>.
        /// The base class already implements <see cref="IReverseBinder{T}"/> for <see langword="int"/>, and a class
        /// member always wins over a default interface implementation, so <see cref="IReverseBinder{T}.ValueChanged"/>
        /// resolves to the inherited event rather than to the bridge declared in <see cref="INumberReverseBinder"/>.
        /// Aliasing keeps both surfaces backed by a single subscriber list.
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
        /// Calls <c>base.OnBound()</c> to raise the inherited
        /// <see cref="ComponentMonoBinder{TComponent, TProperty}.ValueChanged"/>, which backs both
        /// <see cref="IntValueChanged"/> and <see cref="IReverseBinder{T}.ValueChanged"/> for <see langword="int"/>.
        /// The remaining numeric events are raised here because <see cref="INumberReverseBinder"/> bridges them
        /// to their own <see cref="IReverseBinder{T}"/> instantiations.
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

        /// <summary>
        /// Sends <paramref name="value"/> to the ViewModel on every numeric channel this binder exposes.
        /// </summary>
        /// <param name="value">The value to send, before conversion.</param>
        /// <remarks>
        /// A binder that is only ever pushed to has no use for this; it exists for the ones that also listen to
        /// their component and forward what the user did — a dropdown selection, for instance. Raising
        /// <see cref="ComponentMonoBinder{TComponent, TProperty}.ValueChanged"/> alone would reach an
        /// <see langword="int"/> field in the ViewModel but leave a <see langword="float"/> one silent, because
        /// the other three channels are bridged by <see cref="INumberReverseBinder"/> rather than inherited.
        /// </remarks>
        protected void RaiseNumberValueChanged(int value)
        {
            RaiseValueChanged(value);

            var converted = GetConvertedValue(value);

            LongValueChanged?.Invoke(converted);
            FloatValueChanged?.Invoke(converted);
            DoubleValueChanged?.Invoke(converted);
        }
    }
}