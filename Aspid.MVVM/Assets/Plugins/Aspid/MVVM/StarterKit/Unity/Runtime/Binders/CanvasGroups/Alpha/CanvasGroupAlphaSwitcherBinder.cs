#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{CanvasGroup, float, IConverter{float, float}}"/> that switches the <see cref="CanvasGroup.alpha"/>
    /// property between two <see cref="float"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 1] before being applied to <see cref="CanvasGroup.alpha"/>.
    /// </remarks>
    /// <include file="XmlExampleDoc-CanvasGroup-Alpha-1.1.0.xml" path="doc//member[@name='CanvasGroupAlphaSwitcherBinder']/*" />
    [Serializable]
    public sealed class CanvasGroupAlphaSwitcherBinder : SwitcherFloatBinder<CanvasGroup>
    {
        /// <inheritdoc/>
        public CanvasGroupAlphaSwitcherBinder(
            CanvasGroup target,
            float trueValue,
            float falseValue,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            Target.alpha = Mathf.Clamp01(value);
    }
}