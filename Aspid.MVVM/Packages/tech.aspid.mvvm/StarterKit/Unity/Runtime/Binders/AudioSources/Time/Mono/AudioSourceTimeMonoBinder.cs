using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{AudioSource}"/> that binds the <see cref="AudioSource.time"/> property.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Time")]
    public class AudioSourceTimeMonoBinder : ComponentFloatMonoBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.time;
            set => CachedComponent.SetTime(value);
        }
    }
}