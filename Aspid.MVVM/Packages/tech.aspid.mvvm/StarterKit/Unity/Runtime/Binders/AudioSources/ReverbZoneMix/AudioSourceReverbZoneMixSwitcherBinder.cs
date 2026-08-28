#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{TTarget,T}">SwitcherBinder&lt;AudioSource, float&gt;</see> that switches the <see cref="AudioSource.reverbZoneMix"/>
    /// property between two <see cref="float"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 1.1] before being applied to <see cref="AudioSource.reverbZoneMix"/>.
    /// </remarks>
    /// <include file="XmlExampleDoc-AudioSource-ReverbZone-1.1.0.xml" path="doc//member[@name='AudioSourceReverbZoneMixSwitcherBinder']/*" />
    [Serializable]
    public sealed class AudioSourceReverbZoneMixSwitcherBinder : SwitcherBinder<AudioSource, float>
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
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(float value) =>
            Target.reverbZoneMix = this.SafeClamp(value, 0, 1.1f, Target);
    }
}