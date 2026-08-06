using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Rewrites the stored type of a managed reference directly in an asset's YAML text. This is the only way
    /// to re-point a <c>[SerializeReference]</c> whose type can no longer be loaded (renamed / moved / deleted),
    /// because Unity drops missing references to <see langword="null"/> through the serialization API and never
    /// exposes them for reassignment. Parser-free: the document and the target <c>RefIds</c> entry are located by
    /// line scanning, and only the inline <c>{ … }</c> on the entry's <c>type:</c> line is replaced.
    /// </summary>
    /// <remarks>
    /// Split across partial files by responsibility:
    /// <see cref="FindMissingReferences"/> / <see cref="FindUnsetRequiredFields"/> (scan, <c>.Scan.cs</c>),
    /// the YAML-mutating repairs (<c>.Rewrite.cs</c>) and the read-only id/type/field probes (<c>.Read.cs</c>).
    /// This file holds the document / <c>RefIds</c> / indentation primitives those parts share.
    /// </remarks>
    internal static partial class SerializeReferenceYamlEditor
    {
        // The null managed-reference id Unity stores for an unassigned [SerializeReference] field
        // (UnityEngine.Serialization.ManagedReferenceUtility.RefIdNull).
        private const long NullRid = -2;

        // The object document header ("--- !u!114 &11400000") and the RefIds-block lookup are single-sourced in
        // SerializeReferenceYaml so this repair flow and the graph scanner read Unity's RefIds shape identically.
        private static Regex DocumentHeader => SerializeReferenceYaml.DocumentHeader;

        // Returns the [start, end) line range of the document whose anchor equals fileId. Falls back to the single
        // document of a one-object asset (the common ScriptableObject case) when the anchor cannot be matched.
        private static (int start, int end) FindDocumentRange(string[] lines, long fileId)
        {
            var start = -1;
            var end = lines.Length;
            var headerCount = 0;
            var firstHeader = -1;

            for (var i = 0; i < lines.Length; i++)
            {
                var match = DocumentHeader.Match(lines[i]);
                if (!match.Success) continue;

                headerCount++;
                if (firstHeader < 0) firstHeader = i;

                if (start >= 0)
                {
                    end = i;
                    break;
                }

                if (long.TryParse(match.Groups["id"].Value, out var anchor) && anchor == fileId)
                    start = i;
            }

            if (start >= 0) return (start, end);
            return headerCount == 1 ? (firstHeader, lines.Length) : (-1, -1);
        }

        private static int FindRefIdsStart(string[] lines, int start, int end) =>
            SerializeReferenceYaml.FindRefIdsStart(lines, start, end);

        private static int FindEntryEnd(string[] lines, int headerIndex, int end, int entryIndent) =>
            SerializeReferenceYaml.FindEntryEnd(lines, headerIndex, end, entryIndent);

        private static int IndentOf(string line) =>
            SerializeReferenceYaml.IndentOf(line);

        private static bool TryParseInlineType(string body, out ManagedTypeName type) =>
            SerializeReferenceYaml.TryParseInlineType(body, out type);

        // Writes lines back preserving the source's newline style and trailing-newline state. Unity writes its YAML
        // with LF on every platform; File.WriteAllLines would re-emit Environment.NewLine (CRLF on Windows) and churn
        // the whole file for a one-line edit.
        private static void WritePreservingNewlines(string assetPath, IReadOnlyList<string> lines)
        {
            var original = File.ReadAllText(assetPath);
            var newline = DominantNewline(original);

            var builder = new StringBuilder(original.Length);
            for (var i = 0; i < lines.Count; i++)
            {
                builder.Append(lines[i]);
                if (i < lines.Count - 1) builder.Append(newline);
            }

            // Re-add the trailing terminator only if the source had one (Unity assets always do).
            if (original.Length > 0 && original[^1] == '\n') builder.Append(newline);

            File.WriteAllText(assetPath, builder.ToString());
        }

        // The newline style that dominates the source by line count — a majority pick keeps a one-line edit on a
        // mixed-ending file from flipping every other line's terminator. LF (Unity's invariant terminator) wins ties;
        // a lone CR maps to LF since this writer only emits "\r\n" or "\n".
        private static string DominantNewline(string text)
        {
            var crlf = 0;
            var loneLf = 0;

            for (var i = 0; i < text.Length; i++)
            {
                if (text[i] != '\n') continue;
                if (i > 0 && text[i - 1] == '\r') crlf++;
                else loneLf++;
            }

            return crlf > loneLf ? "\r\n" : "\n";
        }

        // Whether every line in [start, end) is indented with spaces only — the precondition the block-removing writes
        // verify before touching the file, so an asset with unexpected (tab / mixed) indentation is left untouched.
        private static bool BlockIndentIsTrusted(string[] lines, int start, int end)
        {
            for (var i = Math.Max(start, 0); i < end && i < lines.Length; i++)
            {
                if (!IndentIsSpaceOnly(lines[i])) return false;
            }

            return true;
        }

        // A line whose leading indentation is spaces only — Unity's invariant for serialized YAML. Tab / mixed
        // indentation is where IndentOf and the "- rid:" \s* regexes can measure nesting differently, so callers
        // about to delete a bounded block bail rather than risk a mis-bounded, non-undoable write.
        private static bool IndentIsSpaceOnly(string line)
        {
            // A blank / whitespace-only line carries no indentation to measure — FindEntryEnd spans blank lines inside
            // an entry, so a stray tab in such a line must not abort an otherwise valid (space-indented) block removal.
            if (string.IsNullOrWhiteSpace(line)) return true;

            // Only the leading run counts as indentation; a non-space whitespace character there makes the block
            // untrusted. Whitespace after the first content character is not indentation.
            foreach (var character in line)
            {
                if (character == ' ') continue;
                return !char.IsWhiteSpace(character);
            }

            return true;
        }

        // A Unity-serialized YAML asset carries the "%TAG !u!" directive before its first document. Guards the
        // destructive writes so a hand-authored or foreign YAML file can never be surgically rewritten.
        private static bool LooksLikeUnityYaml(string[] lines)
        {
            for (var i = 0; i < lines.Length; i++)
            {
                var line = (i == 0 ? StripByteOrderMark(lines[i]) : lines[i]).TrimStart();
                if (line.Length == 0) continue;
                if (line.StartsWith("%TAG !u!", StringComparison.Ordinal)) return true;
                if (line.StartsWith("---", StringComparison.Ordinal)) return false; // first document reached without the %TAG marker
            }

            return false;
        }

        // File.ReadAllLines strips a UTF-8 BOM in practice, but guard the first line defensively so the %TAG sniff is
        // never thrown off by a leading byte-order mark.
        private static string StripByteOrderMark(string line) =>
            line.Length > 0 && line[0] == '\uFEFF' ? line[1..] : line;
    }
}
