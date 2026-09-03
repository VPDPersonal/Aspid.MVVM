using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="AudioSource.mute"/> on each element.
    /// </summary>
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "Mute", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Mute EnumGroup")]
    public sealed class AudioSourceMuteEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(AudioSource element, bool value) =>
            element.mute = value;
    }
}
