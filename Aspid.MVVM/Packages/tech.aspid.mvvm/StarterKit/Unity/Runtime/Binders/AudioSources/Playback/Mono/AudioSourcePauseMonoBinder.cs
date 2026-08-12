using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AudioSourcePlaybackMonoBinder"/> that calls <see cref="AudioSource.Pause"/> when the bound
    /// ViewModel command or action is invoked.
    /// </summary>
    /// <remarks>
    /// Suspends playback, keeping the position so that resuming continues from where it stopped.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Playback")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Pause")]
    public sealed class AudioSourcePauseMonoBinder : AudioSourcePlaybackMonoBinder
    {
        /// <inheritdoc/>
        protected override void Perform(AudioSource audioSource) =>
            audioSource.Pause();
    }
}
