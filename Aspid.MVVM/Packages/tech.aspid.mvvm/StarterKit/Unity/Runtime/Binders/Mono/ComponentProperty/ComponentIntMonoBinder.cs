using System;
using UnityEngine;
using Converter = Aspid.MVVM.StarterKit.IConverter<int, int>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{T1, T2, T3}">ComponentMonoBinder&lt;TComponent, int, IConverter&lt;int, int&gt;&gt;</see> that binds an <see langword="int"/> property,
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
        public event Action<int> IntValueChanged;

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

            var converted = GetConvertedBackValue(value);

            IntValueChanged?.Invoke(converted);
            LongValueChanged?.Invoke(converted);
            FloatValueChanged?.Invoke(converted);
            DoubleValueChanged?.Invoke(converted);
        }
    }
}