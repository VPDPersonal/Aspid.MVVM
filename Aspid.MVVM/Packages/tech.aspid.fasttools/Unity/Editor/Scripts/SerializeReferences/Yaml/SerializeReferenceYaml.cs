using System;
using System.Linq;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// The shared, parser-free YAML-scan toolkit for Unity's managed-reference (<c>RefIds</c>) serialization, used by
    /// both the repair flow (<see cref="SerializeReferenceYamlEditor"/>) and the graph window
    /// (<see cref="SerializeReferenceGraphScanner"/>). Single-sourcing these primitives keeps the two readers in
    /// agreement about Unity's RefIds shape — quoting, indentation and the document/inline-type grammar — so a fix to
    /// one cannot silently diverge from the other.
    /// </summary>
    internal static class SerializeReferenceYaml
    {
        /// <summary>
        /// Matches an object document header (e.g. <c>--- !u!114 &amp;11400000</c>): captures the local file id as the
        /// YAML anchor and the class id (<c>!u!114</c>) used as a best-effort fallback label when the live type name is
        /// unavailable.
        /// </summary>
        public static readonly Regex DocumentHeader = new(@"^--- !u!(?<class>\d+) &(?<id>\d+)", RegexOptions.Compiled);

        /// <summary>
        /// Matches the <c>RefIds:</c> key line that opens the managed-reference id list of an object document.
        /// </summary>
        public static readonly Regex RefIdsKey = new(@"^\s*RefIds:\s*$", RegexOptions.Compiled);

        /// <summary>
        /// Matches the inline <c>class: X, ns: Y, asm: Z</c> body of a <c>RefIds</c> type mapping, honouring the
        /// single-quoted class names Unity writes for closed generics (e.g. <c>'Modifier`1[[…]]'</c>).
        /// </summary>
        public static readonly Regex InlineType = new(
            @"class:\s*(?:'(?<class>(?:[^']|'')*)'|(?<class>[^,}]*?))\s*,\s*ns:\s*(?<ns>[^,}]*?)\s*,\s*asm:\s*(?<asm>[^,}]*?)\s*$",
            RegexOptions.Compiled);

        /// <summary>
        /// The project-asset extensions whose Unity-YAML text can host managed references (<c>RefIds</c>). Single source
        /// of truth: SerializeReference's own scanners layer settings-based folder exclusion on top of this set.
        /// </summary>
        public static readonly string[] ScanExtensions = { ".prefab", ".asset", ".unity" };

        /// <summary>
        /// Parses the inline <c>class: X, ns: Y, asm: Z</c> body of a <c>RefIds</c> type mapping into a
        /// <see cref="ManagedTypeName"/>, honouring the single-quoted class names Unity writes for closed generics
        /// (e.g. <c>'Modifier`1[[…]]'</c>). Returns <see langword="false"/> for a malformed or empty type body.
        /// </summary>
        public static bool TryParseInlineType(string body, out ManagedTypeName type)
        {
            type = default;

            var match = InlineType.Match(body);

            if (!match.Success)
                return false;

            var className = match.Groups["class"].Value.Replace("''", "'");
            type = new ManagedTypeName(match.Groups["asm"].Value, match.Groups["ns"].Value, className);

            return !type.IsEmpty;
        }

        /// <summary>
        /// Index of the <c>RefIds:</c> key line within <c>[start, end)</c>, or <c>-1</c> when the document has no
        /// managed references.
        /// </summary>
        public static int FindRefIdsStart(string[] lines, int start, int end)
        {
            for (var i = start; i < end; i++)
            {
                if (RefIdsKey.IsMatch(lines[i]))
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// The exclusive end line of a <c>RefIds</c> entry that begins at <paramref name="headerIndex"/>: the entry runs
        /// until the next list item at its own indent, or until the block dedents out of it (blank lines are spanned).
        /// </summary>
        public static int FindEntryEnd(string[] lines, int headerIndex, int end, int entryIndent)
        {
            for (var j = headerIndex + 1; j < end; j++)
            {
                if (lines[j].Trim().Length == 0)
                    continue;

                var indent = IndentOf(lines[j]);
                if (indent < entryIndent || (indent == entryIndent && lines[j].TrimStart().StartsWith("- ")))
                    return j;
            }

            return end;
        }

        /// <summary>
        /// Leading-indentation width of a line, counting each space or tab as one unit. Unity always indents its YAML
        /// with spaces, but the <c>"- rid:"</c> / <c>"type:"</c> indent regexes capture leading whitespace with
        /// <c>\s*</c> (which counts tabs too). Counting tabs here keeps this measure aligned with those regexes: a
        /// tab-indented line would otherwise read as indent 0 here while a regex sees it as N, and
        /// <see cref="FindEntryEnd"/> would mis-bound the entry. Alignment, not visual tab width, is what matters —
        /// both measures count one unit per character.
        /// </summary>
        public static int IndentOf(string line)
        {
            var count = 0;
            while (count < line.Length && (line[count] == ' ' || line[count] == '\t'))
            {
                count++;
            }

            return count;
        }

        /// <summary>
        /// Returns <see langword="true"/> when <paramref name="path"/> is a project asset (under <c>Assets/</c>) whose
        /// extension can host managed references. This is the engine-level, settings-agnostic candidate test; callers
        /// that must honour the user's excluded-folder settings combine it with their own exclusion check.
        /// </summary>
        public static bool IsCandidateAssetPath(string path)
        {
            if (string.IsNullOrEmpty(path) || !path.StartsWith("Assets/", StringComparison.Ordinal))
                return false;

            return ScanExtensions.Any(extension => path.EndsWith(extension, StringComparison.OrdinalIgnoreCase));
        }
    }
}
