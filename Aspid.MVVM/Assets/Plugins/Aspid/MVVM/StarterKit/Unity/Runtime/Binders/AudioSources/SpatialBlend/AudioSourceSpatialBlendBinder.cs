#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{AudioSource}"/> that sets the <see cref="AudioSource.spatialBlend"/> property.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 1] before being applied to <see cref="AudioSource.spatialBlend"/>.
    /// </remarks>
    /// <include file="XmlExampleDoc-AudioSource-SpatialBlend-1.1.0.xml" path="doc//member[@name='AudioSourceSpatialBlendBinder']/*" />
    [Serializable]
    public class AudioSourceSpatialBlendBinder : TargetFloatBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.spatialBlend;
            set => Target.spatialBlend = value;
        }
        
        /// <inheritdoc/>
        public AudioSourceSpatialBlendBinder(AudioSource target, IConverter<float, float>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
        
        /// <summary>
        /// Called when converting the bound value before applying it to the <see cref="AudioSource.spatialBlend"/> property.
        /// Clamps the converted value to the valid range of 0 to 1.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.GetConvertedValue(value)</c> to preserve
        /// the clamping behavior.
        /// </remarks>
        protected override float GetConvertedValue(float value) =>
            Mathf.Clamp(base.GetConvertedValue(value), min: 0, max: 1);
    }
}