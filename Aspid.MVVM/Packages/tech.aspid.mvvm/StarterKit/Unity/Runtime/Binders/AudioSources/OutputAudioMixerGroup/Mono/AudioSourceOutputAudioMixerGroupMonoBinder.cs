using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{AudioSource, AudioMixerGroup}"/> that binds the <see cref="AudioSource.outputAudioMixerGroup"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current outputAudioMixerGroup value
    /// is sent back to the ViewModel.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – OutputAudioMixerGroup")]
    public class AudioSourceOutputAudioMixerGroupMonoBinder : ComponentMonoBinder<AudioSource, AudioMixerGroup>
    {
        /// <inheritdoc/>
        protected sealed override AudioMixerGroup Property
        {
            get => CachedComponent.outputAudioMixerGroup;
            set => CachedComponent.outputAudioMixerGroup = value;
        }
    }
}