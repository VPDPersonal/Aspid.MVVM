#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{AudioSource}"/> that sets the <see cref="AudioSource.volume"/> property.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 1] before being applied to <see cref="AudioSource.volume"/>.
    /// </remarks>
    /// <include file="XmlExampleDoc-AudioSource-Volume-1.1.0.xml" path="doc//member[@name='AudioSourceVolumeBinder']/*" />
    [Serializable]
    public class AudioSourceVolumeBinder : TargetFloatBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.volume;
            set => Target.volume = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public AudioSourceVolumeBinder(AudioSource target, IConverter<float, float>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <summary>
        /// Called when converting the bound value before applying it to the <see cref="AudioSource.volume"/> property.
        /// Clamps the converted value to the valid range of 0 to 1.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.GetConvertedValue(value)</c> to preserve
        /// the clamping behavior.
        /// </remarks>
        protected override float GetConvertedValue(float value) =>
            BinderMath.SafeClamp(base.GetConvertedValue(value), 0, 1);
    }
}