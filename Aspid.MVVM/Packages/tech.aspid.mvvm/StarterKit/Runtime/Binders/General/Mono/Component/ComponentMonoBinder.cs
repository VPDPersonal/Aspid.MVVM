using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{TComponent}"/> that binds a single component property through its accessors,
    /// applying an optional converter in both directions. In <see cref="BindMode.OneWayToSource"/>, the current property value is sent to the ViewModel on binding.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the bound property.</typeparam>
    /// <typeparam name="TProperty">The type of the bound property.</typeparam>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public abstract partial class ComponentMonoBinder<TComponent, TProperty> : ComponentMonoBinder<TComponent>,
        IBinder<TProperty>,
        IReverseBinder<TProperty>
        where TComponent : Component
    {
        [Tooltip("Optional converter applied to the value; empty leaves it as-is. Reverses only via ITwoWayConverter.")]
        [SerializeReference] private IConverter<TProperty, TProperty> _converter;

        /// <summary>
        /// Gets or sets the bound property.
        /// </summary>
        protected abstract TProperty Property { get; set; }

        /// <inheritdoc/>
        public event Action<TProperty> ValueChanged;

        /// <summary>
        /// Writes <paramref name="value"/> to <see cref="Property"/> after <see cref="GetConvertedValue"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(TProperty value) =>
            Property = GetConvertedValue(value);

        /// <summary>
        /// Sends the initial property value to the ViewModel in <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        /// <remarks>
        /// When overriding, always call <c>base.OnBound()</c>. To change what is sent, override <see cref="SendInitialValueToSource"/> instead.
        /// </remarks>
        protected override void OnBound()
        {
            WarnAboutOneWayConverter();

            if (Mode is BindMode.OneWayToSource)
                SendInitialValueToSource();

            return;

            void WarnAboutOneWayConverter()
            {
                if (Mode is not (BindMode.OneWayToSource or BindMode.TwoWay)) return;
                if (_converter is null or ITwoWayConverter<TProperty, TProperty>) return;

                this.LogWarning(
                    problem: $"it is bound as {Mode} with {_converter.GetType().GetTypeName()}, which converts one way only",
                    consequence: "Values sent back to the ViewModel are not converted.");
            }
        }

        /// <summary>
        /// Called on binding in <see cref="BindMode.OneWayToSource"/> to send the current <see cref="Property"/> to the ViewModel.
        /// Override to broadcast through additional channels.
        /// </summary>
        /// <remarks>
        /// An override must route the value through <see cref="GetConvertedBackValue"/>.
        /// </remarks>
        protected virtual void SendInitialValueToSource() =>
            RaiseValueChanged();

        /// <summary>
        /// Raises <see cref="ValueChanged"/> with the current <see cref="Property"/>, after <see cref="GetConvertedBackValue"/>.
        /// </summary>
        protected void RaiseValueChanged() =>
            RaiseValueChanged(Property);

        /// <summary>
        /// Raises <see cref="ValueChanged"/> with <paramref name="value"/>, after <see cref="GetConvertedBackValue"/>.
        /// </summary>
        /// <param name="value">The value to send to the ViewModel.</param>
        protected void RaiseValueChanged(TProperty value) =>
            ValueChanged?.Invoke(GetConvertedBackValue(value));

        /// <summary>
        /// Converts <paramref name="value"/> with the serialized converter, or returns it unchanged when none is set.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        protected virtual TProperty GetConvertedValue(TProperty value) => _converter is not null
            ? _converter.Convert(value)
            : value;

        /// <summary>
        /// Converts <paramref name="value"/> for the ViewModel; unchanged unless the converter implements <see cref="ITwoWayConverter{TFrom, TTo}"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        protected virtual TProperty GetConvertedBackValue(TProperty value) => _converter is ITwoWayConverter<TProperty, TProperty> twoWay
            ? twoWay.ConvertBack(value)
            : value;
    }
}
