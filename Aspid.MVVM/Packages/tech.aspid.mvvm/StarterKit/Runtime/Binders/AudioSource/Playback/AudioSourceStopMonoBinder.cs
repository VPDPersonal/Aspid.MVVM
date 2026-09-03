using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AudioSourcePlaybackMonoBinder"/> that calls <see cref="AudioSource.Stop"/>.
    /// </summary>
    /// <remarks>
    /// Stops playback and rewinds to the start.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Playback")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Stop")]
    public sealed class AudioSourceStopMonoBinder : AudioSourcePlaybackMonoBinder
    {
        /// <inheritdoc/>
        protected override void Perform(AudioSource audioSource) =>
            audioSource.Stop();
    }
}
