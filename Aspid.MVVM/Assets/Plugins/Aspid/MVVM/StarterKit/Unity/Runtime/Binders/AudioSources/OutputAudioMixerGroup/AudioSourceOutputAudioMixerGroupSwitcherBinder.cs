#nullable enable
using System;
using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{AudioSource, AudioMixerGroup}"/> that switches the <see cref="AudioSource.outputAudioMixerGroup"/>
    /// property between two <see cref="AudioMixerGroup"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-AudioSource-OutputAudioMixerGroup-1.1.0.xml" path="doc//member[@name='AudioSourceOutputAudioMixerGroupSwitcherBinder']/*" />
    [Serializable]
    public sealed class AudioSourceOutputAudioMixerGroupSwitcherBinder : SwitcherBinder<AudioSource, AudioMixerGroup?>
    {
        /// <inheritdoc/>
        public AudioSourceOutputAudioMixerGroupSwitcherBinder(
            AudioSource target,
            AudioMixerGroup? trueValue,
            AudioMixerGroup? falseValue,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(AudioMixerGroup? value) =>
            Target.outputAudioMixerGroup = value;
    }
}