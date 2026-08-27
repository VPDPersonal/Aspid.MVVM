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
        /// <see cref="AudioSource.timeSamples"/> accepts any value unclamped, including one past the end of the
        /// clip. With no clip assigned, the write is skipped.
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
        /// Unlike <see cref="AudioSource.timeSamples"/>, <see cref="AudioSource.time"/> logs an error and resets
        /// to the start for an out-of-range value instead of clamping it.
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
