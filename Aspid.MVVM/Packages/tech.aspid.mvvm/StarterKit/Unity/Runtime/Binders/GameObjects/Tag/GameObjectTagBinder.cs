#nullable enable
using System;
using UnityEngine;
using Converter = Aspid.MVVM.StarterKit.IConverter<string?, string?>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{GameObject}"/> that sets the <see cref="GameObject.tag"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current
    /// <see cref="GameObject.tag"/> value is sent back to the ViewModel.
    /// </remarks>
    /// <include file="XmlExampleDoc-GameObject-Tag-1.1.0.xml" path="doc//member[@name='GameObjectTagBinder']/*" />
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public sealed class GameObjectTagBinder : TargetBinder<GameObject>,
        IBinder<string>, 
        IReverseBinder<string>
    {
        /// <inheritdoc/>
        public event Action<string?>? ValueChanged;
        
        [Tooltip("Optional converter applied to the value before it is used. Leave empty to use the value as-is.")]
        [SerializeReference] private Converter? _converter;
        
        /// <summary>
        /// Initializes a new instance of <see cref="GameObjectTagBinder"/> targeting the specified <see cref="GameObject"/>.
        /// </summary>
        /// <param name="target">The <see cref="GameObject"/> whose <see cref="GameObject.tag"/> property is bound.</param>
        /// <param name="converter">The converter used to transform the bound string value, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public GameObjectTagBinder(GameObject target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _converter = converter;
        }

        /// <summary>
        /// Sets the <see cref="GameObject.tag"/> property to <paramref name="value"/> (optionally converted).
        /// </summary>
        /// <param name="value">The string value received from the ViewModel.</param>
        public void SetValue(string? value) =>
            Target.tag = GetConvertedValue(value);
        
        /// <summary>
        /// Called when binding is established. In <see cref="BindMode.OneWayToSource"/>, sends the value the
        /// target already holds to the ViewModel so the source starts in step with the view.
        /// </summary>
        /// <remarks>
        /// Does nothing in the other modes: they push from the ViewModel, and reporting the target's current
        /// value back would be the ViewModel hearing its own state from the view.
        /// </remarks>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(GetConvertedBackValue(Target.tag));
        }
        
        private string GetConvertedValue(string value) =>
            _converter?.Convert(value) ?? value;

        /// <summary>
        /// Converts a value on its way back to the ViewModel.
        /// </summary>
        /// <param name="value">The value read from the target.</param>
        /// <returns>
        /// The value as the ViewModel expects it: undone by the converter when it offers
        /// <see cref="ITwoWayConverter{TFrom, TTo}"/>, and unchanged when it does not.
        /// </returns>
        /// <remarks>
        /// The forward converter must not be applied here. This binder pushes the target's current
        /// value to the ViewModel, so running it through <c>Convert</c> a second time hands the
        /// ViewModel a value that has been converted twice — visibly wrong for anything that is not
        /// its own inverse.
        /// </remarks>
        private string GetConvertedBackValue(string value) =>
            _converter is ITwoWayConverter<string?, string?> twoWay
                ? twoWay.ConvertBack(value) ?? value
                : value;
    }
}