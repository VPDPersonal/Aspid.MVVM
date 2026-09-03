using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="AudioSource.clip"/> on each element.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "m_audioClip", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Clip EnumGroup")]
    public sealed class AudioSourceClipEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, AudioClip>
    {
        /// <inheritdoc/>
        protected override void SetValue(AudioSource element, AudioClip value) =>
            element.clip = value;
    }
}
