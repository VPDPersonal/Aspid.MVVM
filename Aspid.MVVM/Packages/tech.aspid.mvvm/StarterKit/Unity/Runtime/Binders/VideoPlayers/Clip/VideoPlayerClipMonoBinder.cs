using UnityEngine;
using UnityEngine.Video;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentObjectMonoBinder{T1, T2}">ComponentObjectMonoBinder&lt;VideoPlayer, VideoClip&gt;</see> that binds
    /// <see cref="VideoPlayer.clip"/>.
    /// </summary>
    /// <remarks>
    /// Assigning a clip stops playback and rewinds — Unity's behavior, not the binder's. A destroyed clip arrives
    /// as <see langword="null"/>.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(VideoPlayer), serializePropertyNames: "m_VideoClip")]
    [AddComponentMenu("Aspid/MVVM/Binders/Video/VideoPlayer Binder – Clip")]
    public class VideoPlayerClipMonoBinder : ComponentObjectMonoBinder<VideoPlayer, VideoClip>
    {
        /// <inheritdoc/>
        protected sealed override VideoClip Property
        {
            get => CachedComponent.clip;
            set => CachedComponent.clip = value;
        }
    }
}
