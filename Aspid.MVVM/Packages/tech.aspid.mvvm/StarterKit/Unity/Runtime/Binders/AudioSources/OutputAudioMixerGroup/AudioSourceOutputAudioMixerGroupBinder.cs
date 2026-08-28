#nullable enable
using System;
using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{AudioSource, AudioMixerGroup}"/> that sets the <see cref="AudioSource.outputAudioMixerGroup"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-AudioSource-OutputAudioMixerGroup-1.1.0.xml" path="doc//member[@name='AudioSourceOutputAudioMixerGroupBinder']/*" />
    [Serializable]
    public class AudioSourceOutputAudioMixerGroupBinder : TargetBinder<AudioSource, AudioMixerGroup>
    {
        /// <inheritdoc/>
        protected sealed override AudioMixerGroup? Property
        {
            get => Target.outputAudioMixerGroup;
            set => Target.outputAudioMixerGroup = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public AudioSourceOutputAudioMixerGroupBinder(AudioSource target, IConverter<AudioMixerGroup, AudioMixerGroup>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}