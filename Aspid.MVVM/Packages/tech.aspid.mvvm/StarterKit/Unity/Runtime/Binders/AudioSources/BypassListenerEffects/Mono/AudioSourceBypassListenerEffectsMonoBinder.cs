using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{AudioSource}"/> that binds the <see cref="AudioSource.bypassListenerEffects"/> property.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – BypassListenerEffects")]
    public class AudioSourceBypassListenerEffectsMonoBinder : ComponentBoolMonoBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.bypassListenerEffects;
            set => CachedComponent.bypassListenerEffects = value;
        }
    }
}