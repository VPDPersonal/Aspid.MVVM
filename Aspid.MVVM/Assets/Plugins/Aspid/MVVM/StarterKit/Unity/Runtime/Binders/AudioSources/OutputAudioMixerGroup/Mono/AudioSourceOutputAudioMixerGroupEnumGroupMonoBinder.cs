using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – OutputAudioMixerGroup EnumGroup")]
    public sealed class AudioSourceOutputAudioMixerGroupEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, AudioMixerGroup>
    {
        protected override void SetValue(AudioSource element, AudioMixerGroup value) =>
            element.outputAudioMixerGroup = value;
    }
}