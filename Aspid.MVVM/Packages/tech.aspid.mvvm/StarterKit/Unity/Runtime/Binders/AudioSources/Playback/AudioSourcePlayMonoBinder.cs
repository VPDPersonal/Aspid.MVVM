using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AudioSourcePlaybackMonoBinder"/> that calls <see cref="AudioSource.Play()"/> when the bound
    /// ViewModel command or action is invoked.
    /// </summary>
    /// <remarks>
    /// Starts the assigned clip from the beginning, or restarts it if it is already playing.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Playback")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Play")]
    public sealed class AudioSourcePlayMonoBinder : AudioSourcePlaybackMonoBinder
    {
        /// <inheritdoc/>
        protected override void Perform(AudioSource audioSource) =>
            audioSource.Play();
    }
}
