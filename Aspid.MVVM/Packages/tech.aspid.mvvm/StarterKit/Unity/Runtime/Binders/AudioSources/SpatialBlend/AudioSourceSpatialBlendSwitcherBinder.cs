#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherFloatBinder{AudioSource}"/> that switches the <see cref="AudioSource.spatialBlend"/>
    /// property between two <see cref="float"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 1] before being applied to <see cref="AudioSource.spatialBlend"/>.
    /// </remarks>
    /// <include file="XmlExampleDoc-AudioSource-SpatialBlend-1.1.0.xml" path="doc//member[@name='AudioSourceSpatialBlendSwitcherBinder']/*" />
    [Serializable]
    public sealed class AudioSourceSpatialBlendSwitcherBinder : SwitcherFloatBinder<AudioSource>
    {
        /// <inheritdoc/>
        public AudioSourceSpatialBlendSwitcherBinder(
            AudioSource target,
            float trueValue,
            float falseValue,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }
        
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            Target.spatialBlend = value;

        /// <summary>
        /// Called when converting the selected value before applying it to the <see cref="AudioSource.spatialBlend"/> property.
        /// Clamps the converted value to the valid range of 0 to 1.
        /// </summary>
        protected override float GetConvertedValue(float value) =>
            Mathf.Clamp(base.GetConvertedValue(value), min: 0, max: 1);
    }
}