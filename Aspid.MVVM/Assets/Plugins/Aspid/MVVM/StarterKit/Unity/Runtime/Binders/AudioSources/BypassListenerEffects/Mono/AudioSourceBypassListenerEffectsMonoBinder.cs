using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – BypassListenerEffects")]
    public class AudioSourceBypassListenerEffectsMonoBinder : ComponentBoolMonoBinder<AudioSource>
    {
        protected sealed override bool Property
        {
            get => CachedComponent.bypassListenerEffects;
            set => CachedComponent.bypassListenerEffects = value;
        }
    }
}