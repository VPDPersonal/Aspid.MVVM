using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Mute Enum")]
    public sealed class AudioSourceMuteEnumMonoBinder : EnumMonoBinder<AudioSource, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.mute = value;
    }
}