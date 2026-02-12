using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Mute EnumGroup")]
    public sealed class AudioSourceMuteEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, bool>
    {
        protected override void SetValue(AudioSource element, bool value) =>
            element.mute = value;
    }
}