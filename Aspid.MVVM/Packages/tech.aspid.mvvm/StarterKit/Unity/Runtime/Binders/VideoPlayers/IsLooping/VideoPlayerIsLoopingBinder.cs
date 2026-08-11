#nullable enable
using System;
using UnityEngine;
using UnityEngine.Video;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{VideoPlayer}"/> that binds <see cref="VideoPlayer.isLooping"/>.
    /// </summary>
    /// <remarks>
    /// Whether the clip repeats. A background loop and a cutscene that must end are the same component with this
    /// one flag different.
    /// </remarks>
    [Serializable]
    public class VideoPlayerIsLoopingBinder : TargetBoolBinder<VideoPlayer>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.isLooping;
            set => Target.isLooping = value;
        }

        /// <inheritdoc/>
        public VideoPlayerIsLoopingBinder(
            VideoPlayer target,
            bool isInvert = false,
            BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode) { }
    }
}
