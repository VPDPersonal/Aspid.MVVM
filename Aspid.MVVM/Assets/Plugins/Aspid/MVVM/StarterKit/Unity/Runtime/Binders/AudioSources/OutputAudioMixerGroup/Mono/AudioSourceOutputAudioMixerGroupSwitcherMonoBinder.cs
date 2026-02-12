using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – OutputAudioMixerGroup Switcher")]
    public sealed class AudioSourceOutputAudioMixerGroupSwitcherMonoBinder : SwitcherMonoBinder<AudioSource, AudioMixerGroup>
    {
        protected override void SetValue(AudioMixerGroup value) =>
            CachedComponent.outputAudioMixerGroup = value;
    }
}