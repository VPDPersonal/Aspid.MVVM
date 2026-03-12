#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{AudioSource}"/> that sets the <see cref="AudioSource.reverbZoneMix"/> property.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 1.1] before being applied to <see cref="AudioSource.reverbZoneMix"/>.
    /// </remarks>
    /// <include file="XmlExampleDoc-AudioSource-ReverbZone-1.1.0.xml" path="doc//member[@name='AudioSourceReverbZoneMixBinder']/*" />
    [Serializable]
    public class AudioSourceReverbZoneMixBinder : TargetFloatBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.reverbZoneMix;
            set => Target.reverbZoneMix = value;
        }

        /// <inheritdoc />
        public AudioSourceReverbZoneMixBinder(AudioSource target, IConverter<float, float>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <summary>
        /// Called when converting the bound value before applying it to the <see cref="AudioSource.reverbZoneMix"/> property.
        /// Clamps the converted value to the valid range of 0 to 1.1.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.GetConvertedValue(value)</c> to preserve
        /// the clamping behavior.
        /// </remarks>
        protected override float GetConvertedValue(float value) =>
            Mathf.Clamp(base.GetConvertedValue(value), min: 0, max: 1.1f);
    }
}