using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="AudioSource.timeSamples"/>.
    /// </summary>
    /// <remarks>
    /// The position is kept inside the current clip; without a clip the write is skipped.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – TimeSamples Enum")]
    public sealed class AudioSourceTimeSamplesEnumMonoBinder : EnumMonoBinder<AudioSource, int>
    {
        /// <inheritdoc/>
        protected override void SetValue(int value) =>
            CachedComponent.SetTimeSamples(value);
    }
}
