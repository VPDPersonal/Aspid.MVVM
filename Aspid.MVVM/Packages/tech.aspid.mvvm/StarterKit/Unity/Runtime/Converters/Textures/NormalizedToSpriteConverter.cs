#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Picks one of a list of sprites by a 0..1 amount.
    /// </summary>
    /// <remarks>
    /// Stepped bars, discrete health hearts, stamina pips — a continuous value rendered as one of a
    /// fixed set of frames.
    /// </remarks>
    [Serializable]
    public sealed class NormalizedToSpriteConverter : IConverter<float, Sprite?>
    {
        [Tooltip("The frames, from empty to full.")]
        [SerializeField] private Sprite[] _frames = Array.Empty<Sprite>();

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalizedToSpriteConverter"/> class with no frames.
        /// </summary>
        public NormalizedToSpriteConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalizedToSpriteConverter"/> class.
        /// </summary>
        /// <param name="frames">The frames, from empty to full.</param>
        public NormalizedToSpriteConverter(Sprite[]? frames)
        {
            _frames = frames ?? Array.Empty<Sprite>();
        }

        /// <summary>
        /// Picks the frame for the specified amount.
        /// </summary>
        /// <param name="value">The 0..1 amount.</param>
        /// <returns>The frame at that amount, or <see langword="null"/> when there are none.</returns>
        public Sprite? Convert(float value)
        {
            if (_frames is not { Length: > 0 }) return null;

            var index = Mathf.Clamp(Mathf.FloorToInt(Mathf.Clamp01(value) * _frames.Length), 0, _frames.Length - 1);
            return _frames[index];
        }
    }
}
