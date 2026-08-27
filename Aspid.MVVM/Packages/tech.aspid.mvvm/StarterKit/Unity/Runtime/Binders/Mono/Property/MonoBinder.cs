using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that binds a single property using its get/set accessors,
    /// sending the current value back to the ViewModel when bound in <see cref="BindMode.OneWayToSource"/>.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property being bound.</typeparam>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public abstract partial class MonoBinder<TProperty> : MonoBinder, IBinder<TProperty>, IReverseBinder<TProperty>
    {
        /// <inheritdoc/>
        public event Action<TProperty> ValueChanged;

        /// <summary>
        /// Gets or sets the property that this binder reads from and writes to.
        /// </summary>
        protected abstract TProperty Property { get; set; }

        /// <summary>
        /// Sets the bound property to <paramref name="value"/>, passing it through <see cref="GetConvertedValue"/> first.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(TProperty value) =>
            Property = GetConvertedValue(value);

        /// <summary>
        /// Called after binding is established.
        /// Sends the initial property value to the ViewModel when in <see cref="BindMode.OneWayToSource"/> mode.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call the base implementation to preserve
        /// the <see cref="BindMode.OneWayToSource"/> initialization behavior. To change what is sent,
        /// override <see cref="SendInitialValueToSource"/> instead.
        /// </remarks>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                SendInitialValueToSource();
        }

        /// <summary>
        /// Sends the current <see cref="Property"/> value to the ViewModel on binding in
        /// <see cref="BindMode.OneWayToSource"/>. Override to broadcast through additional channels.
        /// </summary>
        /// <remarks>
        /// An override must route the value through <see cref="GetConvertedBackValue"/>: the forward
        /// conversion describes the ViewModel to View direction only.
        /// </remarks>
        protected virtual void SendInitialValueToSource() =>
            RaiseValueChanged();

        /// <summary>
        /// Raises <see cref="ValueChanged"/> with the current <see cref="Property"/> value,
        /// passing it through <see cref="GetConvertedBackValue"/> first.
        /// </summary>
        protected void RaiseValueChanged() =>
            RaiseValueChanged(Property);

        /// <summary>
        /// Raises <see cref="ValueChanged"/> with <paramref name="value"/>,
        /// passing it through <see cref="GetConvertedBackValue"/> first.
        /// </summary>
        /// <param name="value">The value to send to the ViewModel.</param>
        protected void RaiseValueChanged(TProperty value) =>
            ValueChanged?.Invoke(GetConvertedBackValue(value));

        /// <summary>
        /// Converts <paramref name="value"/> before it is applied to the property. Returns the value unchanged by default.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        protected virtual TProperty GetConvertedValue(TProperty value) => value;

        /// <summary>
        /// Converts <paramref name="value"/> before it is sent back to the ViewModel. Returns the value unchanged by default.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        /// <remarks>
        /// Separate from <see cref="GetConvertedValue"/> because the reverse direction is not the
        /// forward one: applying the forward conversion again compounds it, so a ×100 converter
        /// turns 0.75 into 7500 on the way back rather than into 0.75.
        /// </remarks>
        protected virtual TProperty GetConvertedBackValue(TProperty value) => value;
    }
}
