#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{CanvasGroup}"/> that sets the <see cref="CanvasGroup.alpha"/> property.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 1] before being applied to <see cref="CanvasGroup.alpha"/>.
    /// </remarks>
    /// <include file="XmlExampleDoc-CanvasGroup-Alpha-1.1.0.xml" path="doc//member[@name='CanvasGroupAlphaBinder']/*" />
    [Serializable]
    public class CanvasGroupAlphaBinder : TargetFloatBinder<CanvasGroup>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.alpha;
            set => Target.alpha = value;
        }

        /// <inheritdoc/>
        public CanvasGroupAlphaBinder(CanvasGroup target, IConverter<float, float>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <summary>
        /// Called when converting the bound value before applying it to the <see cref="CanvasGroup.alpha"/> property.
        /// Clamps the converted value to the valid range of 0 to 1.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.GetConvertedValue(value)</c> to preserve
        /// the clamping behavior.
        /// </remarks>
        protected override float GetConvertedValue(float value) =>
            Mathf.Clamp01(base.GetConvertedValue(value));
    }
}