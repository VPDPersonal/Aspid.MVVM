#nullable enable
using System;
using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class AudioSourceOutputAudioMixerGroupBinder : TargetBinder<AudioSource, AudioMixerGroup>
    {
        protected sealed override AudioMixerGroup? Property
        {
            get => Target.outputAudioMixerGroup;
            set => Target.outputAudioMixerGroup = value;
        }
        
        public AudioSourceOutputAudioMixerGroupBinder(AudioSource target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}