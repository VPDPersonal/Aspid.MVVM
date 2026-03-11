using System;
using UnityEngine;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Extension methods for applying distance values to an <see cref="AudioSource"/>.
    /// </summary>
    public static class AudioSourceSetters
    {
        /// <summary>
        /// Applies the min/max distance from <paramref name="value"/> to <paramref name="audioSource"/>
        /// according to the specified <paramref name="mode"/>.
        /// </summary>
        /// <param name="audioSource">The <see cref="AudioSource"/> whose distance properties are updated.</param>
        /// <param name="value">
        /// A <see cref="Vector2"/> where <see cref="Vector2.x"/> is treated as the minimum distance
        /// and <see cref="Vector2.y"/> as the maximum distance.
        /// </param>
        /// <param name="mode">Determines which distance component(s) are updated.</param>
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