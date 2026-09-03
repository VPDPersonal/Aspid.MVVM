using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="AudioSource.timeSamples"/> on each element.
    /// </summary>
    /// <remarks>
    /// The position is kept inside the current clip; without a clip the write is skipped.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – TimeSamples EnumGroup")]
    public sealed class AudioSourceTimeSamplesEnumGroupMonoBinder : EnumGroupMonoBinder<AudioSource, int>
    {
        /// <inheritdoc/>
        protected override void SetValue(AudioSource element, int value) =>
            element.SetTimeSamples(value);
    }
}
