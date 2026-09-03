using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="AudioSource.spatialBlend"/>.
    /// </summary>
    /// <remarks>
    /// The value is clamped to 0..1.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – SpatialBlend Enum")]
    public sealed class AudioSourceSpatialBlendEnumMonoBinder : EnumMonoBinder<AudioSource, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.spatialBlend = this.SafeClamp(value, 0f, 1f);
    }
}
