using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – OutputAudioMixerGroup Enum")]
    public sealed class AudioSourceOutputAudioMixerGroupEnumMonoBinder : EnumMonoBinder<AudioSource, AudioMixerGroup>
    {
        protected override void SetValue(AudioMixerGroup value) =>
            CachedComponent.outputAudioMixerGroup = value;
    }
}