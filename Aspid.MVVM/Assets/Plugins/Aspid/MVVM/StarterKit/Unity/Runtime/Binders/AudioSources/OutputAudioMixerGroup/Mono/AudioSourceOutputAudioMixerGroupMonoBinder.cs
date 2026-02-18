using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – OutputAudioMixerGroup")]
    public class AudioSourceOutputAudioMixerGroupMonoBinder : ComponentMonoBinder<AudioSource, AudioMixerGroup>
    {
        protected sealed override AudioMixerGroup Property
        {
            get => CachedComponent.outputAudioMixerGroup;
            set => CachedComponent.outputAudioMixerGroup = value;
        }
    }
}