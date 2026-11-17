using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides utility methods for Inspector header names.
    /// </summary>
    public static class HeaderNameHelper
    {
        /// <summary>
        /// Removes a trailing suffix while preserving the duplicate index.
        /// </summary>
        /// <param name="name">The header name, optionally ending in <c> (N)</c>.</param>
        /// <param name="suffix">The suffix to remove, including its separator.</param>
        /// <returns>The shortened name; the original name when the suffix is absent.</returns>
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
