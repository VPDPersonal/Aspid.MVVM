#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Wraps a string in a rich-text size tag.
    /// </summary>
    [Serializable]
    public sealed class RichTextSizeConverter : IConverterString
    {
        [Tooltip("The size applied to the text.")]
        [SerializeField] private float _size = 100f;

        [Tooltip("Treat the size as a percentage of the label's own size rather than as points.")]
        [SerializeField] private bool _isPercent = true;

        /// <remarks>Default: at full size.</remarks>
        public RichTextSizeConverter() { }

        /// <param name="size">The size applied to the text.</param>
        /// <param name="isPercent">If <see langword="true"/>, treats the size as a percentage.</param>
        public RichTextSizeConverter(float size, bool isPercent = true)
        {
            _size = size;
            _isPercent = isPercent;
        }

        /// <summary>
        /// Wraps the specified string in a size tag.
        /// </summary>
        /// <param name="value">The string to resize.</param>
        /// <returns>The tagged string.</returns>
        public string? Convert(string? value)
        {
            if (string.IsNullOrEmpty(value)) return value;

            var size = _size.ToString(System.Globalization.CultureInfo.InvariantCulture);
            return "<size=" + size + (_isPercent ? "%>" : ">") + value + "</size>";
        }
    }
}
