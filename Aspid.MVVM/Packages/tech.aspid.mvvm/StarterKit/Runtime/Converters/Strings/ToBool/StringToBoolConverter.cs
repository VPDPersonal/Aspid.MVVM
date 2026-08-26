using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a boolean out of text.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String/To Bool",
        Name = "Parse",
        Tooltip = "Reads a boolean out of text")]
    public sealed class StringToBoolConverter : ITwoWayConverter<string?, bool>
    {
        [Tooltip("The spellings read as true, matched without regard to case. The first one is " +
            "written back for true.")]
        [SerializeField] private string[] _trueTokens = { "true", "1", "yes", "on" };

        [Tooltip("The spellings read as false, matched without regard to case. Empty: unmatched text " +
            "takes the fallback quietly. The first one is written back for false.")]
        [SerializeField] private string[] _falseTokens = Array.Empty<string>();

        [Tooltip("Returned when the text matches nothing.")]
        [SerializeField] private bool _fallback;

        /// <remarks>Default: with the usual spellings.</remarks>
        public StringToBoolConverter() { }

        /// <param name="trueTokens">
        /// The spellings read as <see langword="true"/>; an empty or <see langword="null"/> list keeps
        /// the usual ones. The first one is written back for <see langword="true"/>.
        /// </param>
        /// <param name="falseTokens">
        /// The spellings read as <see langword="false"/>; an empty or <see langword="null"/> list makes
        /// unmatched text take the fallback without reporting it. The first one is written back for
        /// <see langword="false"/>.
        /// </param>
        /// <param name="fallback">
        /// Returned when the text matches nothing. When omitted, <see langword="false"/>.
        /// </param>
        public StringToBoolConverter(
            string[]? trueTokens,
            string[]? falseTokens = null,
            bool fallback = false)
        {
            _fallback = fallback;
            if (trueTokens is { Length: > 0 }) _trueTokens = trueTokens;
            if (falseTokens is { Length: > 0 }) _falseTokens = falseTokens;
        }

        /// <summary>
        /// Reads a boolean out of the specified text.
        /// </summary>
        /// <param name="value">The text to read.</param>
        /// <returns>
        /// Whether the text matches an accepted spelling, or the fallback when it matches none.
        /// </returns>
        public bool Convert(string? value)
        {
            // Blank text is an unfilled field rather than a spelling of either answer.
            if (string.IsNullOrWhiteSpace(value)) return _fallback;

            // Clearing the list in the Inspector is a scene bug, not a way to say "always false".
            if (_trueTokens is not { Length: > 0 })
            {
                return this.UseFallback(
                    _fallback,
                    "the list of spellings read as true is empty, so no text can read as true");
            }

            var trimmed = value.Trim();

            foreach (var t in _trueTokens)
            {
                if (string.Equals(trimmed, t, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            if (_falseTokens is { Length: > 0 })
            {
                foreach (var t in _falseTokens)
                {
                    if (string.Equals(trimmed, t, StringComparison.OrdinalIgnoreCase))
                        return false;
                }

                return this.UseFallback(_fallback, value.Expected("a boolean spelling"));
            }

            // With no false-spellings, unmatched text is the fallback by construction, not by failure.
            return _fallback;
        }

        /// <summary>
        /// Writes the specified boolean as text.
        /// </summary>
        /// <param name="value">The boolean to write.</param>
        /// <returns>
        /// The first spelling authored for it, or the plain word when none is authored.
        /// </returns>
        public string ConvertBack(bool value)
        {
            if (value)
            {
                // The cleared list Convert reports: there is no spelling to write for true either.
                return _trueTokens is { Length: > 0 }
                    ? _trueTokens[0]
                    : this.UseFallback("true", "the list of spellings read as true is empty");
            }

            if (_falseTokens is { Length: > 0 }) return _falseTokens[0];

            // "false" only reads back as false while the fallback is false.
            if (_fallback)
            {
                this.LogError(
                    problem: "no spelling is read as false and the fallback is true",
                    consequence: "Writing \"false\", which reads back as true.");
            }

            return "false";
        }
    }
}
