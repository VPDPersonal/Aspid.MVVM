using System;
using UnityEngine;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public static class AudioSourceSetters
    {
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

