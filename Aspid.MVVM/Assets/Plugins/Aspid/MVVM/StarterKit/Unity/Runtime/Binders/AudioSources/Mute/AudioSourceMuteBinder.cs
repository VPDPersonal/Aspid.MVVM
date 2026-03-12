#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{AudioSource}"/> that sets the <see cref="AudioSource.mute"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-AudioSource-Mute-1.1.0.xml" path="doc//member[@name='AudioSourceMuteBinder']/*" />
    [Serializable]
    public class AudioSourceMuteBinder : TargetBoolBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.mute;
            set => Target.mute = value;
        }

        /// <inheritdoc />
        public AudioSourceMuteBinder(AudioSource target, bool isInvert = false, BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}