#nullable enable
using System;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Stops rich-text markup in a string from being interpreted.
    /// </summary>
    /// <remarks>
    /// TextMeshPro reads markup out of any text it is given, including text a player typed;
    /// <c>&lt;noparse&gt;</c> renders the characters instead.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String/Rich Text",
        Name = "No Parse",
        Tooltip = "Stops rich-text markup in a string from being interpreted")]
    public sealed class RichTextNoParseConverter : IConverter<string?, string?>
    {
        /// <summary>
        /// Wraps the specified string so its markup is shown rather than obeyed.
        /// </summary>
        /// <param name="value">The untrusted string.</param>
        /// <returns>The wrapped string; a blank string, spaces included, comes back unwrapped.</returns>
        public string? Convert(string? value) => string.IsNullOrWhiteSpace(value)
                ? value
                : "<noparse>" + value + "</noparse>";
    }
}
