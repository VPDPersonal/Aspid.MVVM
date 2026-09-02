using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="AudioSourcePlaybackMonoBinder"/> that calls <see cref="AudioSource.UnPause"/> when the bound
    /// ViewModel command or action is invoked.
    /// </summary>
    /// <remarks>
    /// Resumes a paused source from the position it held. Has no effect on a stopped one.
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
