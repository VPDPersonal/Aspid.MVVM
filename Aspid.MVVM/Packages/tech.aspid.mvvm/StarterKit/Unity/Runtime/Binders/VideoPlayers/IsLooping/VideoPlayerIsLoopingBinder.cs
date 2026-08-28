#nullable enable
using System;
using UnityEngine.Video;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{VideoPlayer, bool}"/> that binds <see cref="VideoPlayer.isLooping"/>.
    /// </summary>
    [Serializable]
    public class VideoPlayerIsLoopingBinder : TargetBinder<VideoPlayer, bool>
    {
        /// <inheritdoc/>
        public VideoPlayerIsLoopingBinder(
            VideoPlayer target,
            IConverter<bool, bool>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }

        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.isLooping;
            set => Target.isLooping = value;
        }
    }
}
