#nullable enable
using System;
using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Binder that switches the <see cref="AudioSource.outputAudioMixerGroup"/> between two
    /// <see cref="AudioMixerGroup"/> values based on a bound boolean ViewModel property.
    /// </summary>
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