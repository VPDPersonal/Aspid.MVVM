#nullable enable
using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Converter = Aspid.MVVM.StarterKit.IConverter<string?, string?>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="TargetBinder{TTarget}"/> that binds the <see cref="Object.name"/> property
    /// to a <see langword="string"/> ViewModel property.
    /// </summary>
    /// <remarks>
    /// When <see cref="BindMode.OneWayToSource"/> is active, the current <see cref="Object.name"/>
    /// is propagated to the ViewModel when binding is established.
    /// </remarks>
    /// <include file="XmlExampleDoc-Object-Name-1.1.0.xml" path="doc//member[@name='ObjectNameBinder']/*" />
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public sealed class ObjectNameBinder : TargetBinder<Object>,
        IBinder<string>, 
        IReverseBinder<string>
    {
        /// <inheritdoc/>
        public event Action<string?>? ValueChanged;
        
        [Tooltip("Optional converter applied to the string value in both directions.")]
        [SerializeReference] private Converter? _converter;

        /// <param name="target">The <see cref="GameObject"/> whose <see cref="Object.name"/> will be bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public ObjectNameBinder(GameObject target, BindMode mode)
            : this(target, converter: null, mode) { }

        /// <param name="target">The <see cref="GameObject"/> whose <see cref="Object.name"/> will be bound.</param>
        /// <param name="converter">
        /// An optional converter to transform the value before applying it or propagating it back to the ViewModel.
        /// Pass <see langword="null"/> to use the value unchanged.
        /// </param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>. Defaults to <see cref="BindMode.OneWay"/>.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public ObjectNameBinder(GameObject target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _converter = converter;
        }

        /// <inheritdoc/>
        public void SetValue(string? value) =>
            Target.name = GetConvertedValue(value);
        
        /// <summary>
        /// Called after binding is established.
        /// In <see cref="BindMode.OneWayToSource"/> mode, propagates the current <see cref="Object.name"/> to the ViewModel.
        /// </summary>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(GetConvertedBackValue(Target.name));
        }
        
        private string GetConvertedValue(string value) =>
            _converter?.Convert(value) ?? value ?? string.Empty;

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