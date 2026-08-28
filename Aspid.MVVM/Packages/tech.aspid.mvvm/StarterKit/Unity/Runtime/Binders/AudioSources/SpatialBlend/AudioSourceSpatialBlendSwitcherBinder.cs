#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{TTarget,T}">SwitcherBinder&lt;AudioSource, float&gt;</see> that switches the <see cref="AudioSource.spatialBlend"/>
    /// property between two <see cref="float"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 1] before being applied to <see cref="AudioSource.spatialBlend"/>.
    /// </remarks>
    /// <include file="XmlExampleDoc-AudioSource-SpatialBlend-1.1.0.xml" path="doc//member[@name='AudioSourceSpatialBlendSwitcherBinder']/*" />
    [Serializable]
    public sealed class AudioSourceSpatialBlendSwitcherBinder : SwitcherBinder<AudioSource, float>
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
        /// <param name="value">The value to convert.</param>
        protected override float GetConvertedValue(float value) =>
            this.SafeClamp(base.GetConvertedValue(value), 0, 1, Target);
    }
}