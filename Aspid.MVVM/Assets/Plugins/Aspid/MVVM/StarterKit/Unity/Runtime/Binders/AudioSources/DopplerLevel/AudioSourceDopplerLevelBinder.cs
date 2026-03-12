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

        /// <inheritdoc />
        public AudioSourceDopplerLevelBinder(AudioSource target, IConverter<float, float>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}