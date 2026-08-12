#nullable enable
using System;
using UnityEngine.Video;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetObjectBinder{T1, T2}">TargetObjectBinder&lt;VideoPlayer, VideoClip&gt;</see> that binds
    /// <see cref="VideoPlayer.clip"/>.
    /// </summary>
    /// <remarks>
    /// Which video plays: a cutscene chosen by progress, a tutorial chosen by the step the player is on. Assigning a clip
    /// stops playback and rewinds — Unity's behaviour, not the binder's. A destroyed clip arrives as
    /// <see langword="null"/>.
    /// </remarks>
    [Serializable]
    public class VideoPlayerClipBinder : TargetObjectBinder<VideoPlayer, VideoClip>
    {
        /// <inheritdoc/>
        protected sealed override VideoClip? Property
        {
            get => Target.clip;
            set => Target.clip = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> — the property raises no change event to listen to.</exception>
        public VideoPlayerClipBinder(VideoPlayer target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
