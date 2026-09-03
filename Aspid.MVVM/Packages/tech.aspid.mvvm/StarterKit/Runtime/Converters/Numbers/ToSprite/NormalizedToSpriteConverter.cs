#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Picks one of a list of sprites by a 0..1 amount.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To Sprite",
        Name = "Normalized To Sprite",
        Tooltip = "Picks one of a list of sprites by a 0..1 amount")]
    public sealed class NormalizedToSpriteConverter : IConverter<float, Sprite?>, IConverter<double, Sprite?>
    {
        [Tooltip("The frames, from empty to full.")]
        [SerializeField] private Sprite[] _frames = Array.Empty<Sprite>();

        private NormalizedToSpriteConverter() { }

        /// <param name="frames">
        /// The frames, from empty to full. With none the converter has nothing to pick from, which is
        /// reported as an error.
        /// </param>
        public NormalizedToSpriteConverter(Sprite[]? frames)
        {
            _frames = frames ?? Array.Empty<Sprite>();
        }

        /// <summary>
        /// Picks the frame for the specified amount.
        /// </summary>
        /// <param name="value">The 0..1 amount.</param>
        /// <returns>
        /// The frame at that amount. With no frames authored the failure is reported as an error and
        /// <see langword="null"/> is returned.
        /// </returns>
        public Sprite? Convert(float value)
        {
            if (_frames.Length == 0)
            {
                this.LogError(
                    problem: "no frames are assigned",
                    consequence: "Returning null.");

                return null;
            }

            // An amount of exactly 1 floors one past the last frame, so the index is clamped too.
            var index = Mathf.Clamp(Mathf.FloorToInt(Mathf.Clamp01(value) * _frames.Length), 0, _frames.Length - 1);
            return _frames[index];
        }

        Sprite? IConverter<double, Sprite?>.Convert(double value) =>
            Convert(NumericSaturation.ToFloat(value));
    }
}
