using UnityEngine;
using UnityEngine.Video;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{VideoPlayer}"/> that binds <see cref="VideoPlayer.playbackSpeed"/>.
    /// </summary>
    /// <remarks>Clamped to 0..10, the range Unity documents; non-finite values are dropped to <c>0</c>.</remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(VideoPlayer))]
    [AddComponentMenu("Aspid/MVVM/Binders/Video/VideoPlayer Binder – Playback Speed")]
    public class VideoPlayerPlaybackSpeedMonoBinder : ComponentFloatMonoBinder<VideoPlayer>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.playbackSpeed;
            set => CachedComponent.playbackSpeed = this.SafeClamp(value, 0f, 10f);
        }
    }
}
