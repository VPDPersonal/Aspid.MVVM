using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="OneWayValue{T}"/> pre-configured with <see cref="BindMode.OneTime"/>,
    /// accepting a ViewModel value exactly once.
    /// </summary>
    /// <typeparam name="T">The type of the bindable value.</typeparam>
    /// <remarks>
    /// The <see cref="BindModeOverrideAttribute"/> on this class restricts the mode selection
    /// in the Unity Inspector to <see cref="BindMode.OneTime"/> only.
    /// An optional <see cref="IConverter{TFrom,TTo}"/> can be supplied to transform the incoming value
    /// before it is stored.
    /// </remarks>
    /// <include file="XmlExampleDoc-Values-1.1.0.xml" path="doc//member[@name='OneTimeValue{1}']/*" />
    [Serializable]
    [BindModeOverride(BindMode.OneTime)]
    public class OneTimeValue<T> : OneWayValue<T>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="OneTimeValue{T}"/> with the default value.
        /// </summary>
        public OneTimeValue()
            : base(BindMode.OneTime) { }

        /// <summary>
        /// Initializes a new instance of <see cref="OneTimeValue{T}"/> with a pre-set value.
        /// </summary>
        /// <param name="value">The initial value.</param>
        public OneTimeValue(T? value)
            : base(value, BindMode.OneTime) { }

        /// <summary>
        /// Initializes a new instance of <see cref="OneTimeValue{T}"/> with a pre-set value and a converter.
        /// </summary>
        /// <param name="value">The initial value passed through the converter before being stored.</param>
        /// <param name="converter">
        /// An optional converter applied to the incoming value before it is stored.
        /// Pass <see langword="null"/> to store the value unchanged.
        /// </param>
        public OneTimeValue(T? value, IConverter<T?, T?>? converter)
            : base(value, converter, BindMode.OneTime) { }
    }
}