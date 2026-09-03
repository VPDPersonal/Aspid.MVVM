using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="AudioSource.spatialBlend"/> on each element.
    /// </summary>
    /// <remarks>
    /// The value is clamped to 0..1.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – SpatialBlend EnumGroup")]
    public sealed class AudioSourceSpatialBlendEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(AudioSource element, float value) =>
            element.spatialBlend = this.SafeClamp(value, 0f, 1f);
    }
}
