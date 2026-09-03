using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="AudioSource.pitch"/>.
    /// </summary>
    /// <remarks>
    /// The value is clamped to -3..3.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), serializePropertyNames: "m_Pitch", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Pitch Enum")]
    public sealed class AudioSourcePitchEnumMonoBinder : EnumMonoBinder<AudioSource, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.pitch = this.SafeClamp(value, -3f, 3f);
    }
}
