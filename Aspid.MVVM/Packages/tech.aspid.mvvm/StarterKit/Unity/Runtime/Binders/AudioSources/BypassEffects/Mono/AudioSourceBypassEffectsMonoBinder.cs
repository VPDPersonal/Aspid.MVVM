using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{AudioSource}"/> that binds the <see cref="AudioSource.bypassEffects"/> property.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – BypassEffects")]
    public class AudioSourceBypassEffectsMonoBinder : ComponentBoolMonoBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.bypassEffects;
            set => CachedComponent.bypassEffects = value;
        }
    }
}