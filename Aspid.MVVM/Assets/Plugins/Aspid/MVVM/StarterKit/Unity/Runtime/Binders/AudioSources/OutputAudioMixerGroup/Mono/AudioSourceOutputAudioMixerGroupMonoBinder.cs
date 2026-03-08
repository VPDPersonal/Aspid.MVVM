using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="AudioSource.outputAudioMixerGroup"/> property on an <see cref="AudioSource"/>
    /// when the bound ViewModel value changes.
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established the current value
    /// is sent back to the ViewModel.
    /// </summary>
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