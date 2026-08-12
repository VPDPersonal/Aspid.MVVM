using UnityEngine;
using UnityEngine.Video;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{VideoPlayer}"/> that binds <see cref="VideoPlayer.playbackSpeed"/>.
    /// </summary>
    /// <remarks>
    /// How fast the video plays. Clamped to the range Unity documents, 0..10: it accepts anything and then decodes
    /// at a rate the platform cannot sustain, which shows up as stuttering rather than as an error.
    /// </remarks>
    [AddBinderContextMenu(typeof(VideoPlayer))]
    [AddComponentMenu("Aspid/MVVM/Binders/Video/VideoPlayer Binder – Playback Speed")]
    public class VideoPlayerPlaybackSpeedMonoBinder : ComponentFloatMonoBinder<VideoPlayer>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.playbackSpeed;
            set => CachedComponent.playbackSpeed = BinderMath.SafeClamp(value, 0f, 10f);
        }
    }
}
