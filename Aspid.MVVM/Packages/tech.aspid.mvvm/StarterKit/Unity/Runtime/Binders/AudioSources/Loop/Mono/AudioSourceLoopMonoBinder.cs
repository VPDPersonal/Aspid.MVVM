using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{AudioSource}"/> that binds the <see cref="AudioSource.loop"/> property.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Loop")]
    public class AudioSourceLoopMonoBinder : ComponentBoolMonoBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.loop;
            set => CachedComponent.loop = value;
        }
    }
}