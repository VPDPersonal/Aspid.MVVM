using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="AudioSource.clip"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "m_audioClip", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Clip Enum")]
    public sealed class AudioSourceClipEnumMonoBinder : EnumMonoBinder<AudioSource, AudioClip>
    {
        /// <inheritdoc/>
        protected override void SetValue(AudioClip value) =>
            CachedComponent.clip = value;
    }
}
