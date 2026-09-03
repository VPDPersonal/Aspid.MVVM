using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AudioSourcePlaybackMonoBinder"/> that calls <see cref="AudioSource.UnPause"/>.
    /// </summary>
    /// <remarks>
    /// Resumes a paused source from its position; has no effect on a stopped one.
    /// </remarks>
    [AddBinderContextMenu(typeof(AudioSource), SubPath = "Playback")]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – UnPause")]
    public sealed class AudioSourceUnPauseMonoBinder : AudioSourcePlaybackMonoBinder
    {
        /// <inheritdoc/>
        protected override void Perform(AudioSource audioSource) =>
            audioSource.UnPause();
    }
}
