using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="AudioSource.volume"/> on each element.
    /// </summary>
    /// <remarks>
    /// The value is clamped to 0..1.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "m_Volume", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Volume EnumGroup")]
    public sealed class AudioSourceVolumeEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(AudioSource element, float value) =>
            element.volume = this.SafeClamp(value, 0f, 1f);
    }
}
