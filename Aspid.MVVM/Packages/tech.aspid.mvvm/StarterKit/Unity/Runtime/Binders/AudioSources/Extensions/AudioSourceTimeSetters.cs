using UnityEngine;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Extension methods for writing an <see cref="AudioSource"/> playback position.
    /// </summary>
    public static class AudioSourceTimeSetters
    {
        /// <summary>
        /// Sets <see cref="AudioSource.timeSamples"/>, keeping the position inside the current clip.
        /// </summary>
        /// <remarks>
        /// The property stores whatever it is given, including a negative sample index and one past the end of
        /// the clip. Its usual source is a seek slider or a <c>progress * clip.samples</c> calculation, which is
        /// exactly where an off-by-one or a stale clip length comes from.
        /// <para/>
        /// With no clip assigned the write is skipped: there is no timeline to seek, and Unity ignores the
        /// assignment anyway.
        /// </remarks>
        /// <param name="audioSource">The source whose playback position is set.</param>
        /// <param name="value">The sample index to seek to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetTimeSamples(this AudioSource audioSource, int value)
        {
            var clip = audioSource.clip;
            if (!clip) return;

            audioSource.timeSamples = Mathf.Clamp(value, 0, LastSample(clip));
        }

        /// <summary>
        /// Sets <see cref="AudioSource.time"/>, keeping the position inside the current clip.
        /// </summary>
        /// <remarks>
        /// Unlike <see cref="AudioSource.timeSamples"/>, this property refuses a position outside the clip rather
        /// than storing it — but it refuses it loudly, with an audio-engine error per assignment, and drops the
        /// source back to the start. Bound to a seek slider or a <c>progress * duration</c> calculation whose
        /// duration is stale or zero, that is an error per frame and a playhead that jumps to the beginning
        /// instead of stopping at the end.
        /// </remarks>
        /// <param name="audioSource">The source whose playback position is set.</param>
        /// <param name="value">The position, in seconds, to seek to.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetTime(this AudioSource audioSource, float value)
        {
            var clip = audioSource.clip;
            if (!clip) return;

            var frequency = clip.frequency;
            var end = frequency > 0 ? LastSample(clip) / (float)frequency : 0f;

            audioSource.time = BinderMath.SafeClamp(value, 0f, end);
        }

        /// <summary>
        /// The last sample a seek may land on — one before the end, since the end itself is out of range.
        /// </summary>
        private static int LastSample(AudioClip clip) =>
            Mathf.Max(0, clip.samples - 1);
    }
}
