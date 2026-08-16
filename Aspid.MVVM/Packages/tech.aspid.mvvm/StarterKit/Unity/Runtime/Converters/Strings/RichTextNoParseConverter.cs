#nullable enable
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Stops rich-text markup in a string from being interpreted.
    /// </summary>
    /// <remarks>
    /// TextMeshPro reads markup out of any text it is given, including text a player typed: a name like
    /// <c>&lt;size=400%&gt;</c> resizes every label that shows that player, on every screen.
    /// <c>&lt;noparse&gt;</c> renders the characters instead. Reach for this on anything a player can
    /// type — the rest of the converters here add markup and are for text the game itself authors.
    /// </remarks>
    [Serializable]
    public sealed class RichTextNoParseConverter : IConverterString
    {
        /// <summary>
        /// Wraps the specified string so its markup is shown rather than obeyed.
        /// </summary>
        /// <param name="value">The untrusted string.</param>
        /// <returns>The wrapped string.</returns>
        public string? Convert(string? value) =>
            string.IsNullOrEmpty(value) ? value : "<noparse>" + value + "</noparse>";
    }
}
