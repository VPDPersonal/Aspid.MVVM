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

        /// <inheritdoc />
        public AudioSourceOutputAudioMixerGroupBinder(AudioSource target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}