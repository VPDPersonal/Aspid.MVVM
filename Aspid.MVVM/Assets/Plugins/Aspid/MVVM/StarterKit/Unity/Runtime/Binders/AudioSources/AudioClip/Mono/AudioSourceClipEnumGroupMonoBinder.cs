using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Clip EnumGroup")]
    public sealed class AudioSourceClipEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, AudioClip>
    {
        protected override void SetValue(AudioSource element, AudioClip value) =>
            element.clip = value;
    }
}