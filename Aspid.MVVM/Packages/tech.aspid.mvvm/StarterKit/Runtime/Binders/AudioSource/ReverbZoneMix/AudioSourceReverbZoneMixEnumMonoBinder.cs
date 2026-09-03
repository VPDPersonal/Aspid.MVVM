using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="AudioSource.reverbZoneMix"/>.
    /// </summary>
    /// <remarks>
    /// The value is clamped to 0..1.1.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – ReverbZoneMix Enum")]
    public sealed class AudioSourceReverbZoneMixEnumMonoBinder : EnumMonoBinder<AudioSource, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.reverbZoneMix = this.SafeClamp(value, 0f, 1.1f);
    }
}
