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
    /// <remarks>
    /// How fast the video plays. Clamped to the range Unity documents, 0..10: it accepts anything and then decodes
    /// at a rate the platform cannot sustain, which shows up as stuttering rather than as an error.
    /// </remarks>
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
