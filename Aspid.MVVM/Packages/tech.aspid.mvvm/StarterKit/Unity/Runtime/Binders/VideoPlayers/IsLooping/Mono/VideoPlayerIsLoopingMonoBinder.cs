using UnityEngine;
using UnityEngine.Video;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{VideoPlayer}"/> that binds <see cref="VideoPlayer.isLooping"/>.
    /// </summary>
    /// <remarks>
    /// Whether the clip repeats. A background loop and a cutscene that must end are the same component with this
    /// one flag different.
    /// </remarks>
    [AddBinderContextMenu(typeof(VideoPlayer))]
    [AddComponentMenu("Aspid/MVVM/Binders/Video/VideoPlayer Binder – Is Looping")]
    public class VideoPlayerIsLoopingMonoBinder : ComponentBoolMonoBinder<VideoPlayer>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.isLooping;
            set => CachedComponent.isLooping = value;
        }
    }
}
