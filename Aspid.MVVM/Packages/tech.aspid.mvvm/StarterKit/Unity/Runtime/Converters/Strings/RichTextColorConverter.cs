#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Wraps a string in a rich-text colour tag.
    /// </summary>
    /// <remarks>
    /// The Greeter sample writes this by hand as an example of a custom converter; shipping it means
    /// nobody has to.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "Rich Text Color", Tooltip = "Wraps a string in a rich-text colour tag")]
    public sealed class RichTextColorConverter : IConverterString
    {
        [Tooltip("The colour the text is tagged with.")]
        [SerializeField] private Color _color = Color.white;

        [Tooltip("Include the alpha channel in the tag.")]
        [SerializeField] private bool _includeAlpha;

        /// <remarks>Default: colouring white.</remarks>
        public RichTextColorConverter() { }

        /// <param name="color">The colour the text is tagged with.</param>
        /// <param name="includeAlpha">If <see langword="true"/>, includes the alpha channel.</param>
        public RichTextColorConverter(Color color, bool includeAlpha = false)
        {
            _color = color;
            _includeAlpha = includeAlpha;
        }

        /// <summary>
        /// Wraps the specified string in a colour tag.
        /// </summary>
        /// <param name="value">The string to colour.</param>
        /// <returns>The tagged string.</returns>
        public string? Convert(string? value) =>
            string.IsNullOrEmpty(value) ? value : Wrap(value!, _color, _includeAlpha);

        internal static string Wrap(string value, Color color, bool includeAlpha) =>
            "<color=#" + (includeAlpha ? ColorUtility.ToHtmlStringRGBA(color) : ColorUtility.ToHtmlStringRGB(color))
            + ">" + value + "</color>";
    }
}
