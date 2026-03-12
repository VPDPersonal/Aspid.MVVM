#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{AudioSource, float, IConverter{float, float}}"/> that switches the <see cref="AudioSource.reverbZoneMix"/>
    /// property between two <see cref="float"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 1.1] before being applied to <see cref="AudioSource.reverbZoneMix"/>.
    /// </remarks>
    /// <include file="XmlExampleDoc-AudioSource-ReverbZone-1.1.0.xml" path="doc//member[@name='AudioSourceReverbZoneMixSwitcherBinder']/*" />
    [Serializable]
    public sealed class AudioSourceReverbZoneMixSwitcherBinder : SwitcherFloatBinder<AudioSource>
    {
        /// <inheritdoc/>
        public AudioSourceReverbZoneMixSwitcherBinder(
            AudioSource target,
            float trueValue,
            float falseValue,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <summary>
        /// Called when applying the selected value to the <see cref="AudioSource.reverbZoneMix"/> property.
        /// Clamps the value to the valid range of 0 to 1.1.
        /// </summary>
        protected override void SetValue(float value) =>
            Target.reverbZoneMix = Mathf.Clamp(value, min: 0, max: 1.1f);
    }
}