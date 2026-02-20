using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Clip Switcher")]
    public sealed class AudioSourceClipSwitcherMonoBinder : SwitcherMonoBinder<AudioSource, AudioClip>
    {
        protected override void SetValue(AudioClip value) =>
            CachedComponent.clip = value;
    }
}