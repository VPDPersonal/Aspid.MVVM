using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Clip Enum")]
    public sealed class AudioSourceClipEnumMonoBinder : EnumMonoBinder<AudioSource, AudioClip>
    {
        protected override void SetValue(AudioClip value) =>
            CachedComponent.clip = value;
    }
}