using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that switches the <see cref="AudioSource.clip"/> property on an <see cref="AudioSource"/>
    /// between two values based on a bound boolean ViewModel property.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Clip Switcher")]
    public sealed class AudioSourceClipSwitcherMonoBinder : SwitcherMonoBinder<AudioSource, AudioClip>
    {
        protected override void SetValue(AudioClip value) =>
            CachedComponent.clip = value;
    }
}