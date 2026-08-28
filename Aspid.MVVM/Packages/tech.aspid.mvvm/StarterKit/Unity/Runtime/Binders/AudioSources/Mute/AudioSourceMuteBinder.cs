#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{AudioSource, bool}"/> that sets the <see cref="AudioSource.mute"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-AudioSource-Mute-1.1.0.xml" path="doc//member[@name='AudioSourceMuteBinder']/*" />
    [Serializable]
    public class AudioSourceMuteBinder : TargetBinder<AudioSource, bool>
    {
        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public AudioSourceMuteBinder(AudioSource target, IConverter<bool, bool>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.mute;
            set => Target.mute = value;
        }
    }
}