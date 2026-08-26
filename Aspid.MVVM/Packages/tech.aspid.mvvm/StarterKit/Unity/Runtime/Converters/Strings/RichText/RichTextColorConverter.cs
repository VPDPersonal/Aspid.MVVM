#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Wraps a string in a rich-text color tag.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String/Rich Text",
        Name = "Color",
        Tooltip = "Wraps a string in a rich-text color tag")]
    public sealed class RichTextColorConverter : IConverter<string?, string?>
    {
        [Tooltip("The color the text is tagged with.")]
        [SerializeField] private Color _color = Color.white;

        [Tooltip("Include the alpha channel in the tag.")]
        [SerializeField] private bool _includeAlpha;

        /// <remarks>Default: coloring white.</remarks>
        public RichTextColorConverter() { }

        /// <param name="color">The color the text is tagged with.</param>
        /// <param name="includeAlpha">If <see langword="true"/>, includes the alpha channel.</param>
        public RichTextColorConverter(Color color, bool includeAlpha = false)
        {
            _color = color;
            _includeAlpha = includeAlpha;
        }

        /// <summary>
        /// Wraps the specified string in a color tag.
        /// </summary>
        /// <param name="value">The string to color.</param>
        /// <returns>The tagged string; a blank string, spaces included, is left untagged.</returns>
        public string? Convert(string? value) => string.IsNullOrWhiteSpace(value) 
            ? value 
            : Wrap(value, _color, _includeAlpha);

        /// <summary>
        /// Wraps a string in a color tag.
        /// </summary>
        /// <param name="value">The string to color.</param>
        /// <param name="color">The color the text is tagged with.</param>
        /// <param name="includeAlpha">When <see langword="true"/>, includes the alpha channel.</param>
        /// <returns>The tagged string.</returns>
        // Shared with ThresholdRichTextColorConverter, which tags a number it formatted itself.
        internal static string Wrap(string value, Color color, bool includeAlpha)
        {
            var colorText = includeAlpha
                ? ColorUtility.ToHtmlStringRGBA(color)
                : ColorUtility.ToHtmlStringRGB(color);
            
            return $"<color=#{colorText}>{value}</color>";
        }
    }
}
