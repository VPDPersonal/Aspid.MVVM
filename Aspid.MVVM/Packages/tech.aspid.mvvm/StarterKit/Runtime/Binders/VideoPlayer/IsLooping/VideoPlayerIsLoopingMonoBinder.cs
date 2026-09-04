using UnityEngine;
using UnityEngine.Video;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="VideoPlayer.isLooping"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(VideoPlayer), serializePropertyNames: "m_Looping")]
    [AddComponentMenu("Aspid/MVVM/Binders/Video/VideoPlayer Binder – Is Looping")]
    public class VideoPlayerIsLoopingMonoBinder : ComponentMonoBinder<VideoPlayer, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.isLooping;
            set => CachedComponent.isLooping = value;
        }
    }
}
