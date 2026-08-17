using Aspid.FastTools.Types;
using System;
using UnityEngine;

// The named converter aliases are [Obsolete]. The converters below keep implementing them for
// one release so that a [SerializeReference] field a project declares as one still
// deserializes; the base lists go with the aliases in the next major.
#pragma warning disable CS0618 // Type or member is obsolete

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a boolean out of text.
    /// </summary>
    /// <remarks>
    /// Configuration and backend payloads say "on", "1" and "yes" as often as they say "true", so the
    /// accepted spellings are authored rather than fixed.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "String To Bool Parse", Tooltip = "Reads a boolean out of text")]
    public sealed class StringToBoolParseConverter : IConverterStringToBool
    {
        [Tooltip("The spellings read as true. Matched without regard to case.")]
        [SerializeField] private string[] _trueTokens = { "true", "1", "yes", "on" };

        [Tooltip("The spellings read as false. Leave empty to treat anything unmatched as false; "
            + "fill it in to have text matching neither list reported as a failure.")]
        [SerializeField] private string[] _falseTokens = Array.Empty<string>();

        [Tooltip("What to do with text that does not parse. ReturnInput is not available here — the "
            + "input is text and the output is not — and behaves as ReturnFallback.")]
        [SerializeField] private ConverterFailureMode _onFailure = ConverterFailureMode.ReturnFallback;

        [Tooltip("Returned when the text matches nothing.")]
        [SerializeField] private bool _fallback;

        /// <remarks>Default: with the usual spellings.</remarks>
        public StringToBoolParseConverter() { }

        /// <param name="trueTokens">The spellings read as <see langword="true"/>.</param>
        /// <param name="fallback">Returned when the text matches nothing.</param>
        public StringToBoolParseConverter(string[]? trueTokens, bool fallback = false)
        {
            if (trueTokens is { Length: > 0 }) _trueTokens = trueTokens;
            _fallback = fallback;
        }

        /// <summary>
        /// Reads a boolean out of the specified text.
        /// </summary>
        /// <param name="value">The text to read.</param>
        /// <returns>Whether it matches one of the accepted spellings, or the fallback.</returns>
        public bool Convert(string? value)
        {
            if (string.IsNullOrWhiteSpace(value) || _trueTokens is not { Length: > 0 }) return _fallback;

            var trimmed = value!.Trim();

            for (var i = 0; i < _trueTokens.Length; i++)
                if (string.Equals(trimmed, _trueTokens[i], StringComparison.OrdinalIgnoreCase))
                    return true;

            if (_falseTokens is { Length: > 0 })
            {
                for (var i = 0; i < _falseTokens.Length; i++)
                    if (string.Equals(trimmed, _falseTokens[i], StringComparison.OrdinalIgnoreCase))
                        return false;

                // Neither list matched, so the text is not a spelling of either answer.
                return OnUnparsed(value);
            }

            // With no false-spellings configured, anything unmatched is false by construction rather
            // than by failure — there is nothing to report.
            return _fallback;
        }

        private bool OnUnparsed(string? value)
        {
            if (_onFailure is ConverterFailureMode.Throw)
                throw ConverterFailure.Rejected(nameof(StringToBoolParseConverter), value, "a boolean spelling");

            ConverterFailure.Report(
                ref _loggedFailure, nameof(StringToBoolParseConverter), value, "a boolean spelling", "the fallback");
            return _fallback;
        }

        [NonSerialized] private bool _loggedFailure;
    }
}
