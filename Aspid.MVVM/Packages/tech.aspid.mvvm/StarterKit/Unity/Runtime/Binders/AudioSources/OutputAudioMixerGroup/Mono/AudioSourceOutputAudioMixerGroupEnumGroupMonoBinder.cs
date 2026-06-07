using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{AudioSource, AudioMixerGroup}"/> that sets the <see cref="AudioSource.outputAudioMixerGroup"/>
    /// property on each element based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – OutputAudioMixerGroup EnumGroup")]
    public sealed class AudioSourceOutputAudioMixerGroupEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, AudioMixerGroup>
    {
        /// <inheritdoc/>
        protected override void SetValue(AudioSource element, AudioMixerGroup value) =>
            element.outputAudioMixerGroup = value;
    }
}