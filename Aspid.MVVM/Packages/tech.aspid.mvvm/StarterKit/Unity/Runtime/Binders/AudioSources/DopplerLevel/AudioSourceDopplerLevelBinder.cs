#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{AudioSource}"/> that sets the <see cref="AudioSource.dopplerLevel"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-AudioSource-DopplerLevel-1.1.0.xml" path="doc//member[@name='AudioSourceDopplerLevelBinder']/*" />
    [Serializable]
    public class AudioSourceDopplerLevelBinder : TargetFloatBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.dopplerLevel;
            set => Target.dopplerLevel = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public AudioSourceDopplerLevelBinder(AudioSource target, IConverter<float, float>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <summary>
        /// Called when converting the bound value before applying it to the <see cref="AudioSource.dopplerLevel"/> property.
        /// Replaces a non-finite converted value with <c>0</c>.
        /// </summary>
        /// <remarks>
        /// Unity clamps this property to its 0..5 range inside the setter, but lets <c>NaN</c> and infinities through,
        /// which silently corrupts the doppler effect for the whole source. When overriding this method, always call
        /// <c>base.GetConvertedValue(value)</c> to keep that guard.
        /// </remarks>
        protected override float GetConvertedValue(float value) =>
            BinderMath.SafeClamp(base.GetConvertedValue(value), 0, 5);
    }
}