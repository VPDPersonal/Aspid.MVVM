#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{AudioSource}"/> that sets the <see cref="AudioSource.pitch"/> property.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [−3, 3] before being applied to <see cref="AudioSource.pitch"/>.
    /// </remarks>
    /// <include file="XmlExampleDoc-AudioSource-Pitch-1.1.0.xml" path="doc//member[@name='AudioSourcePitchBinder']/*" />
    [Serializable]
    public class AudioSourcePitchBinder : TargetFloatBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.pitch;
            set => Target.pitch = value;
        }

        /// <inheritdoc />
        public AudioSourcePitchBinder(AudioSource target, IConverter<float, float>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <summary>
        /// Called when converting the bound value before applying it to the <see cref="AudioSource.pitch"/> property.
        /// Clamps the converted value to the valid range of −3 to 3.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.GetConvertedValue(value)</c> to preserve
        /// the clamping behavior.
        /// </remarks>
        protected override float GetConvertedValue(float value) =>
            Mathf.Clamp(base.GetConvertedValue(value), min: -3, max: 3);
    }
}