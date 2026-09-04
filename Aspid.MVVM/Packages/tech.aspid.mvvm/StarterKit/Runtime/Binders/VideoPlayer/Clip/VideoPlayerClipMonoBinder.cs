using UnityEngine;
using UnityEngine.Video;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentObjectMonoBinder{TComponent, TObject}"/> that binds <see cref="VideoPlayer.clip"/>.
    /// </summary>
    /// <remarks>
    /// Assigning a clip stops playback and rewinds.
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
