using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="AudioSource.outputAudioMixerGroup"/> property on a group of <see cref="AudioSource"/>
    /// components, applying the configured selected or default value to each entry based on the bound
    /// enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – OutputAudioMixerGroup EnumGroup")]
    public sealed class AudioSourceOutputAudioMixerGroupEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, AudioMixerGroup>
    {
        protected override void SetValue(AudioSource element, AudioMixerGroup value) =>
            element.outputAudioMixerGroup = value;
    }
}