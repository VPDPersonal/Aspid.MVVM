#nullable enable
using System;
using UnityEngine;
using UnityEngine.Video;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{VideoPlayer}"/> that binds <see cref="VideoPlayer.playbackSpeed"/>.
    /// </summary>
    /// <remarks>Clamped to 0..10, the range Unity documents; non-finite values are dropped to <c>0</c>.</remarks>
    [Serializable]
    public class VideoPlayerPlaybackSpeedBinder : TargetFloatBinder<VideoPlayer>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.playbackSpeed;
            set => Target.playbackSpeed = BinderMath.SafeClamp(value, 0f, 10f);
        }

        /// <inheritdoc/>
        public VideoPlayerPlaybackSpeedBinder(
            VideoPlayer target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
