using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="AudioSource.clip"/> property on an <see cref="AudioSource"/>
    /// to a value resolved from an enum bound on the ViewModel.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Clip Enum")]
    public sealed class AudioSourceClipEnumMonoBinder : EnumMonoBinder<AudioSource, AudioClip>
    {
        protected override void SetValue(AudioClip value) =>
            CachedComponent.clip = value;
    }
}