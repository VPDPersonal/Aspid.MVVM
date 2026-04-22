using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Editor helper for massaging Inspector header script names, e.g. splitting off a
    /// trailing suffix ("Enum", "EnumGroup", "Switcher", ...) while preserving the
    /// Unity-provided " (N)" duplicate index.
    /// </summary>
    public static class HeaderNameHelper
    {
        /// <summary>
        /// Removes <paramref name="suffix"/> from the end of <paramref name="name"/>, keeping
        /// a trailing " (N)" duplicate index intact.
        /// Example: <c>"AudioSource Binder – Pitch Enum (1)"</c> with suffix <c>" Enum"</c> →
        /// <c>"AudioSource Binder – Pitch (1)"</c>.
        /// If the suffix is not present, <paramref name="name"/> is returned unchanged.
        /// </summary>
        /// <param name="name">The full script name, possibly ending with " (N)".</param>
        /// <param name="suffix">The suffix to strip, including any leading separator (e.g. " Enum").</param>
        /// <returns>The name with <paramref name="suffix"/> removed; index preserved.</returns>
        public static string StripTrailingSuffixPreservingIndex(string name, string suffix)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(suffix)) return name;

            var core = name;
            var indexPart = string.Empty;

            var paren = name.LastIndexOf(" (", StringComparison.Ordinal);
            if (paren > 0 && name.EndsWith(")", StringComparison.Ordinal))
            {
                core = name.Substring(0, paren);
                indexPart = name.Substring(paren);
            }

            return core.EndsWith(suffix, StringComparison.Ordinal)
                ? core.Substring(0, core.Length - suffix.Length) + indexPart
                : name;
        }
    }
}
