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
        protected sealed override AudioClip? Property
        {
            get => Target.clip;
            set => Target.clip = value;
        }

        /// <inheritdoc />
        public AudioSourceClipBinder(AudioSource target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}