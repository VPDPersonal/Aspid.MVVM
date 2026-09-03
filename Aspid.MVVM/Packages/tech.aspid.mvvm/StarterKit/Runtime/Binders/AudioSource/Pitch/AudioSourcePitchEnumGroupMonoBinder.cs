using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="AudioSource.pitch"/> on each element.
    /// </summary>
    /// <remarks>
    /// The value is clamped to -3..3.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "m_Pitch", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Pitch EnumGroup")]
    public sealed class AudioSourcePitchEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(AudioSource element, float value) =>
            element.pitch = this.SafeClamp(value, -3f, 3f);
    }
}
