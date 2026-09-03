using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentObjectMonoBinder{TComponent, TObject}"/> that binds <see cref="AudioSource.clip"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "m_audioClip")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Clip")]
    public class AudioSourceClipMonoBinder : ComponentObjectMonoBinder<AudioSource, AudioClip>
    {
        /// <inheritdoc/>
        protected sealed override AudioClip Property
        {
            get => CachedComponent.clip;
            set => CachedComponent.clip = value;
        }
    }
}
