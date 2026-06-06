using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Ids.Editors
{
    internal static class IdRegistryValidator
    {
        private const int MaxNameLength = 255;

        private static readonly Regex _identifierPattern =
            new(@"^[A-Za-z_][A-Za-z0-9_\-]*$", RegexOptions.Compiled);

        /// <summary>
        /// Validates a candidate id name. Rules, in order: not whitespace,
        /// starts with a letter/underscore and contains only letters, digits,
        /// underscore or hyphen, length ≤ 255, not flagged by <paramref name="isTaken"/>.
        /// Callers should pass a delegate over a cached lookup instead of
        /// materializing a HashSet on every keystroke.
        /// </summary>
        public static bool IsValidName(string input, Func<string, bool> isTaken, out string error)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                error = "Name cannot be empty.";
                return false;
            }

            if (!_identifierPattern.IsMatch(input))
            {
                error = "Name must start with a letter or underscore and contain only letters, digits, underscore or hyphen.";
                return false;
            }

            if (input.Length > MaxNameLength)
            {
                error = $"Name is too long (max {MaxNameLength} chars).";
                return false;
            }

            if (isTaken != null && isTaken(input))
            {
                error = $"'{input}' already exists.";
                return false;
            }

            error = null;
            return true;
        }

        public static CleanUpSummary Summarize(int count, Func<int, string> getName)
        {
            if (getName is null) throw new ArgumentNullException(nameof(getName));

            var empty = 0;
            var duplicates = 0;
            var seen = new HashSet<string>();
            for (var i = 0; i < count; i++)
            {
                var name = getName(i);
                if (string.IsNullOrEmpty(name)) empty++;
                else if (!seen.Add(name)) duplicates++;
            }

            return new CleanUpSummary(empty, duplicates);
        }
    }
}
