using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TwoWayValue{T}"/> pre-configured with <see cref="BindMode.OneWayToSource"/>,
    /// propagating the current value from the View back to the ViewModel on binding.
    /// </summary>
    /// <typeparam name="T">The type of the bindable value.</typeparam>
    /// <remarks>
    /// The <see cref="BindModeOverrideAttribute"/> on this class restricts the mode selection
    /// in the Unity Inspector to <see cref="BindMode.OneWayToSource"/> only.
    /// Setting <see cref="TwoWayValue{T}.Value"/> raises <see cref="IReverseBinder{T}.ValueChanged"/>
    /// so the ViewModel receives the update.
    /// An optional <see cref="IConverter{TFrom,TTo}"/> can be supplied to transform the incoming value
    /// before it is stored.
    /// </remarks>
    /// <include file="XmlExampleDoc-Values-1.1.0.xml" path="doc//member[@name='OneWayToSourceValue{1}']/*" />
    [Serializable]
    [BindModeOverride(BindMode.OneWayToSource)]
    public class OneWayToSourceValue<T> : TwoWayValue<T>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="OneWayToSourceValue{T}"/> with the default value.
        /// </summary>
        public OneWayToSourceValue()
            : base(BindMode.OneWayToSource) { }

        /// <summary>
        /// Initializes a new instance of <see cref="OneWayToSourceValue{T}"/> with a pre-set value.
        /// </summary>
        /// <param name="value">The initial value.</param>
        public OneWayToSourceValue(T? value)
            : base(value, BindMode.OneWayToSource) { }

        /// <summary>
        /// Initializes a new instance of <see cref="OneWayToSourceValue{T}"/> with a pre-set value and a converter.
        /// </summary>
        /// <param name="value">The initial value passed through the converter before being stored.</param>
        /// <param name="converter">
        /// An optional converter applied to the incoming value before it is stored.
        /// Pass <see langword="null"/> to store the value unchanged.
        /// </param>
        public OneWayToSourceValue(T? value, IConverter<T?, T?>? converter)
            : base(value, converter, BindMode.OneWayToSource) { }
    }
}