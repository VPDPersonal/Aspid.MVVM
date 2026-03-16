#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherFloatBinder{AudioSource}"/> that switches the <see cref="AudioSource.spread"/>
    /// property between two <see cref="float"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 360] before being applied to <see cref="AudioSource.spread"/>.
    /// </remarks>
    /// <include file="XmlExampleDoc-AudioSource-Spread-1.1.0.xml" path="doc//member[@name='AudioSourceSpreadSwitcherBinder']/*" />
    [Serializable]
    public sealed class AudioSourceSpreadSwitcherBinder : SwitcherFloatBinder<AudioSource>
    {
        /// <inheritdoc/>
        public AudioSourceSpreadSwitcherBinder(
            AudioSource target,
            float trueValue,
            float falseValue,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            Target.spread = value;

        /// <summary>
        /// Called when converting the selected value before applying it to the <see cref="AudioSource.spread"/> property.
        /// Clamps the converted value to the valid range of 0 to 360.
        /// </summary>
        protected override float GetConvertedValue(float value) =>
            Mathf.Clamp(base.GetConvertedValue(value), min: 0, max: 360);
    }
}