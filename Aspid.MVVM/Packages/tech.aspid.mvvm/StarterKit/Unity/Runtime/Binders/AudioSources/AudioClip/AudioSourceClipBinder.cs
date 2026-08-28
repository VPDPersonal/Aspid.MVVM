#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{AudioSource, AudioClip}"/> that sets the <see cref="AudioSource.clip"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-AudioSource-AudioClip-1.1.0.xml" path="doc//member[@name='AudioSourceClipBinder']/*" />
    [Serializable]
    public class AudioSourceClipBinder : TargetBinder<AudioSource, AudioClip>
    {
        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public AudioSourceClipBinder(AudioSource target, IConverter<AudioClip, AudioClip>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <inheritdoc/>
        protected sealed override AudioClip? Property
        {
            get => Target.clip;
            set => Target.clip = value;
        }
    }
}