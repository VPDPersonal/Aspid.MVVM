#nullable enable
using System;
using UnityEngine;
using System.Globalization;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Wraps a string in a rich-text size tag.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String/Rich Text",
        Name = "Size",
        Tooltip = "Wraps a string in a rich-text size tag")]
    public sealed class RichTextSizeConverter : IConverter<string?, string?>
    {
        [Tooltip("The size applied to the text.")]
        [SerializeField] [Min(0)] private float _size = 100f;

        [Tooltip("Treat the size as a percentage of the label's own size rather than as points.")]
        [SerializeField] private bool _isPercent = true;

        /// <remarks>Default: at full size.</remarks>
        public RichTextSizeConverter() { }

        /// <param name="size">The size applied to the text.</param>
        /// <param name="isPercent">If <see langword="true"/>, treats the size as a percentage.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="size"/> is not above zero.
        /// </exception>
        public RichTextSizeConverter(
            float size,
            bool isPercent = true)
        {
            if (!(size > 0f))
                throw new ArgumentOutOfRangeException(nameof(size), size, "The size must be above zero.");

            _size = size;
            _isPercent = isPercent;
        }

        /// <summary>
        /// Wraps the specified string in a size tag.
        /// </summary>
        /// <param name="value">The string to resize.</param>
        /// <returns>
        /// The tagged string, or the string untagged when it is blank, spaces included, or the
        /// configured size is not above zero.
        /// </returns>
        public string? Convert(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;

            // Testing the good case rather than the bad one puts NaN in the report along with zero.
            if (!(_size > 0f))
            {
                this.LogError(
                    problem: $"the size is {_size.Describe()}, which no text can be drawn at",
                    consequence: "Leaving the string untagged.");

                return value;
            }

            var size = _size.ToString(CultureInfo.InvariantCulture);
            return "<size=" + size + (_isPercent ? "%>" : ">") + value + "</size>";
        }
    }
}
