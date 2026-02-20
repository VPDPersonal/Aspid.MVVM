#nullable enable
using System;
using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class AudioSourceOutputAudioMixerGroupSwitcherBinder : SwitcherBinder<AudioSource, AudioMixerGroup?>
    {
        public AudioSourceOutputAudioMixerGroupSwitcherBinder(
            AudioSource target,
            AudioMixerGroup? trueValue, 
            AudioMixerGroup? falseValue,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, mode) { }

        protected override void SetValue(AudioMixerGroup? value) =>
            Target.outputAudioMixerGroup = value;
    }
}