using UnityEngine;
using UnityEngine.Video;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="VideoPlayer.playbackSpeed"/>.
    /// </summary>
    /// <remarks>
    /// The value is clamped to [0, 10], the range Unity documents.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(VideoPlayer), serializePropertyNames: "m_PlaybackSpeed")]
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
