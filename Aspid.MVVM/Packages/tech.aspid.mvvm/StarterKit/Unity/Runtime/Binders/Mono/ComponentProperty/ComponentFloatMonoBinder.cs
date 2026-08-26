using System;
using UnityEngine;
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{T1, T2, T3}">ComponentMonoBinder&lt;TComponent, float, IConverter&lt;float, float&gt;&gt;</see> that binds a <see langword="float"/> property,
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
        /// <remarks>
        /// Forwards to the inherited <see cref="ComponentMonoBinder{TComponent, TProperty}.ValueChanged"/>.
        /// The base class already implements <see cref="IReverseBinder{T}"/> for <see langword="float"/>, and a class
        /// member always wins over a default interface implementation, so <see cref="IReverseBinder{T}.ValueChanged"/>
        /// resolves to the inherited event rather than to the bridge declared in <see cref="INumberReverseBinder"/>.
        /// Aliasing keeps both surfaces backed by a single subscriber list.
        /// </remarks>
        public event Action<float> FloatValueChanged
        {
            add => ValueChanged += value;
            remove => ValueChanged -= value;
        }

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