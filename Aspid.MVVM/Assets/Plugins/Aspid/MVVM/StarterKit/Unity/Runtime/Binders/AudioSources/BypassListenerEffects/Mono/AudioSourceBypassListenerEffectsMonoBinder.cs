using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="AudioSource.bypassListenerEffects"/> property on an <see cref="AudioSource"/>
    /// when the bound ViewModel value changes.
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established the current value
    /// is sent back to the ViewModel. Supports optional value inversion.
    /// </summary>
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