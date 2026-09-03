using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentIntMonoBinder{TComponent}"/> that binds <see cref="AudioSource.timeSamples"/>.
    /// </summary>
    /// <remarks>
    /// The position is kept inside the current clip; without a clip the write is skipped.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – TimeSamples")]
    public class AudioSourceTimeSamplesMonoBinder : ComponentIntMonoBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => CachedComponent.timeSamples;
            set => CachedComponent.SetTimeSamples(value);
        }
    }
}
