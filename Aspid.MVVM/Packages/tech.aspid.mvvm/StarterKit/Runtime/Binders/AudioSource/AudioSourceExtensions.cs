using System;
using UnityEngine;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Extension methods that write validated values to an <see cref="AudioSource"/>.
    /// </summary>
    public static class AudioSourceExtensions
    {
        /// <summary>
        /// Sets <see cref="AudioSource.timeSamples"/>, keeping the position inside the current clip.
        /// </summary>
        /// <remarks>
        /// Without a clip to write is skipped.
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
        /// Unity logs an error and rewinds for an out-of-range <see cref="AudioSource.time"/> instead of clamping it.
        /// Without a clip to write is skipped.
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

            audioSource.time = BinderMath.SafeClamp(typeof(AudioSourceExtensions), value, 0f, end, audioSource);
        }

        /// <summary>
        /// Writes <see cref="AudioSource.minDistance"/>, <see cref="AudioSource.maxDistance"/> or both from <paramref name="value"/>.
        /// </summary>
        /// <remarks>
        /// Unity validates neither distance: a negative nor inverted pair silences the source. Negative distances are
        /// raised to zero, an inverted pair is swapped, and a non-finite pair is reported and not applied.
        /// </remarks>
        /// <param name="audioSource">The source whose distances are set.</param>
        /// <param name="value">The distances; <see cref="Vector2.x"/> is the minimum, <see cref="Vector2.y"/> the maximum.</param>
        /// <param name="mode">Which distances <paramref name="value"/> writes.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetMinMaxDistance(this AudioSource audioSource, Vector2 value, AudioSourceDistanceMode mode)
        {
            value = mode switch
            {
                AudioSourceDistanceMode.Min => new Vector2(value.x, audioSource.maxDistance),
                AudioSourceDistanceMode.Max => new Vector2(audioSource.minDistance, value.y),
                AudioSourceDistanceMode.Range => value,
                _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
            };

            if (!BinderMath.RequireFinite(typeof(AudioSourceExtensions), value, audioSource)) return;

            value = new Vector2(Mathf.Max(0f, value.x), Mathf.Max(0f, value.y));

            if (value.x > value.y)
            {
                BinderLogger.LogError(
                    typeof(AudioSourceExtensions),
                    problem: $"the distance range ({value.x}, {value.y}) is inverted",
                    consequence: "The endpoints are swapped.",
                    context: audioSource);

                value = new Vector2(value.y, value.x);
            }

            audioSource.minDistance = value.x;
            audioSource.maxDistance = value.y;
        }

        private static int LastSample(AudioClip clip) =>
            Mathf.Max(0, clip.samples - 1);
    }
}
