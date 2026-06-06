using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentIntMonoBinder{AudioSource}"/> that binds the <see cref="AudioSource.timeSamples"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current playback position
    /// is sent back to the ViewModel.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – TimeSamples")]
    public class AudioSourceTimeSamplesMonoBinder : ComponentIntMonoBinder<AudioSource>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => CachedComponent.timeSamples;
            set => CachedComponent.timeSamples = value;
        }
    }
}