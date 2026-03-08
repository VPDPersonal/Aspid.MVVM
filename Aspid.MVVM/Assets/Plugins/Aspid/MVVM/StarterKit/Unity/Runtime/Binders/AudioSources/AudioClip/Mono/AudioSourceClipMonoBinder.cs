using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="AudioSource.clip"/> property on an <see cref="AudioSource"/>
    /// when the bound ViewModel value changes.
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established the current value
    /// is sent back to the ViewModel.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Clip")]
    public class AudioSourceClipMonoBinder : ComponentMonoBinder<AudioSource, AudioClip>
    {
        protected sealed override AudioClip Property
        {
            get => CachedComponent.clip;
            set => CachedComponent.clip = value;
        }
    }
}