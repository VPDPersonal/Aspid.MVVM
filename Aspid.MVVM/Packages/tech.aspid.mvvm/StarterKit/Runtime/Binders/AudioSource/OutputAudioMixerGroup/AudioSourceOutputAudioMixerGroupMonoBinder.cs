using UnityEngine;
using UnityEngine.Audio;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentObjectMonoBinder{TComponent, TObject}"/> that binds <see cref="AudioSource.outputAudioMixerGroup"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "OutputAudioMixerGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – OutputAudioMixerGroup")]
    public class AudioSourceOutputAudioMixerGroupMonoBinder : ComponentObjectMonoBinder<AudioSource, AudioMixerGroup>
    {
        /// <inheritdoc/>
        protected sealed override AudioMixerGroup Property
        {
            get => CachedComponent.outputAudioMixerGroup;
            set => CachedComponent.outputAudioMixerGroup = value;
        }
    }
}
