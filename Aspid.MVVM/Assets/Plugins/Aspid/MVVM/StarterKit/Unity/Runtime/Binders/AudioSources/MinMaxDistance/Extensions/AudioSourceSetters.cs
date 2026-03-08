using System;
using UnityEngine;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Extension methods for <see cref="AudioSource"/> that provide convenient distance setters.
    /// </summary>
    public static class AudioSourceSetters
    {
        /// <summary>
        /// Sets the min/max distance of <paramref name="audioSource"/> according to the specified <paramref name="mode"/>.
        /// </summary>
        /// <param name="audioSource">The <see cref="AudioSource"/> whose distances are updated.</param>
        /// <param name="value">A <see cref="Vector2"/> where x is the minimum distance and y is the maximum distance.</param>
        /// <param name="mode">Controls which distance component(s) are updated.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetMinMaxDistance(this AudioSource audioSource, Vector2 value, AudioSourceDistanceMode mode)
        {
            value = mode switch
            {
                AudioSourceDistanceMode.Min => new Vector2(value.x, audioSource.maxDistance),
                AudioSourceDistanceMode.Max => new Vector2(audioSource.minDistance, value.y),
                AudioSourceDistanceMode.Range => new Vector2(value.x, value.y),
                _ => throw new ArgumentOutOfRangeException()
            };
            
            audioSource.minDistance = value.x;
            audioSource.maxDistance = value.y;
        }
    }
}