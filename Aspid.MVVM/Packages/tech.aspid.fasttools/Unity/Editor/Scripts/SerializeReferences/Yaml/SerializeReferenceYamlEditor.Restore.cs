using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    internal static partial class SerializeReferenceYamlEditor
    {
        /// <summary>
        /// Captures the full <c>RefIds</c> entry block (its <c>- rid: N</c> header, <c>type:</c> mapping and <c>data:</c>
        /// payload) of the managed reference pointed at by <paramref name="elementPath"/> — an element of a top-level
        /// <c>[SerializeReference]</c> array (path shape <c>_field.Array.data[N]</c>). The captured lines are the exact
        /// text needed to re-materialise the reference later via <see cref="TryRestoreArrayElementReference"/>, verbatim
        /// indentation and all. Used by the missing-list guard to snapshot a missing element <i>before</i> a list resize
        /// destroys it (Unity collapses a named missing <c>rid</c> into the anonymous <c>-2</c> sentinel, dropping its
        /// type and orphaned payload). Returns <see langword="false"/> for a non-array path, a null/empty element, or an
        /// entry the file does not carry.
        /// </summary>
        public static bool TryReadArrayElementEntryBlock(string assetPath, long fileId, string elementPath,
            out long rid, out List<string> entryLines)
        {
            rid = 0;
            entryLines = null;

            try
            {
                if (!TryParseTopLevelArrayElement(elementPath, out _, out _)) return false;
                if (!TryReadReferenceId(assetPath, fileId, elementPath, out rid)) return false;
                if (rid < 0) return false; // a null (-1) / missing-sentinel (-2) element carries no entry to snapshot

                var lines = SerializeReferenceYamlProbeCache.ReadAllLines(assetPath);
                var (start, end) = FindDocumentRange(lines, fileId);
                if (start < 0) return false;

                var refIdsStart = FindRefIdsStart(lines, start, end);
                if (refIdsStart < 0) return false;

                var headerPattern = new Regex($@"^(?<indent>\s*)-\s+rid:\s*{rid}\s*$");
                for (var i = refIdsStart + 1; i < end; i++)
                {
                    var match = headerPattern.Match(lines[i]);
                    if (!match.Success) continue;

                    var entryIndent = match.Groups["indent"].Length;
                    var entryEnd = FindEntryEnd(lines, i, end, entryIndent);

                    var captured = new List<string>(entryEnd - i);
                    for (var k = i; k < entryEnd; k++) captured.Add(lines[k]);

                    // A lone header with no type mapping is not a real entry — nothing worth restoring later.
                    if (captured.Count < 2) return false;

                    entryLines = captured;
                    return true;
                }

                return false;
            }
            catch (Exception exception)
            {
                Debug.LogError($"[Aspid FastTools] Failed to read RefIds entry for '{elementPath}' in '{assetPath}': {exception}");
                return false;
            }
        }

        /// <summary>
        /// Re-materialises a managed reference that a list resize collapsed to <c>&lt;None&gt;</c>: re-points the array
        /// element at <paramref name="elementPath"/> (a top-level <c>_field.Array.data[N]</c> path) to a fresh
        /// <c>rid</c> and re-inserts <paramref name="entryLines"/> — the entry captured earlier by
        /// <see cref="TryReadArrayElementEntryBlock"/> — into the <c>RefIds</c> block under that fresh id. The fresh id
        /// (one past the document's current maximum) avoids colliding with any surviving reference. Only acts when the
        /// element currently holds a null/sentinel id, so a slot the user has since re-assigned is never clobbered.
        /// Returns <see langword="true"/> when the file was rewritten; the caller reimports the asset.
        /// </summary>
        public static bool TryRestoreArrayElementReference(string assetPath, long fileId, string elementPath,
            IReadOnlyList<string> entryLines)
        {
            try
            {
                if (entryLines is null || entryLines.Count < 2) return false;
                if (!TryParseTopLevelArrayElement(elementPath, out var fieldName, out var elementIndex)) return false;
                if (string.IsNullOrEmpty(assetPath) || !File.Exists(assetPath)) return false;

                var lines = File.ReadAllLines(assetPath);
                if (!LooksLikeUnityYaml(lines)) return false; // never rewrite a non-Unity YAML file

                var (start, end) = FindDocumentRange(lines, fileId);
                if (start < 0) return false;

                var refIdsStart = FindRefIdsStart(lines, start, end);
                if (refIdsStart < 0) return false;

                // Only restore over a genuinely empty slot: a null (-1) or missing sentinel (-2) pointer. A positive id
                // means the user assigned a real reference after the loss — leave it be rather than overwrite their work.
                if (!TryFindArrayElementPointer(lines, start, refIdsStart, fieldName, elementIndex, out var pointerLine, out var currentRid))
                    return false;
                if (currentRid >= 0) return false;

                var freshRid = NextFreeRid(lines, start, end);

                // Re-point the element, then splice the captured entry (its header's rid rewritten to the fresh id) in as
                // the RefIds list's first entry — mirroring where Unity writes new entries and where TryNullReference
                // inserts the null sentinel.
                var pointerIndent = IndentOf(lines[pointerLine]);
                lines[pointerLine] = new string(' ', pointerIndent) + $"- rid: {freshRid}";

                var entry = RewriteEntryRid(entryLines, freshRid);

                var result = new List<string>(lines.Length + entry.Count);
                for (var i = 0; i < lines.Length; i++)
                {
                    result.Add(lines[i]);
                    if (i == refIdsStart) result.AddRange(entry);
                }

                WritePreservingNewlines(assetPath, result);
                SerializeReferenceYamlProbeCache.ClearCache();
                return true;
            }
            catch (Exception exception)
            {
                Debug.LogError($"[Aspid FastTools] Failed to restore managed reference at '{elementPath}' in '{assetPath}': {exception}");
                return false;
            }
        }

        /// <summary>
        /// Finds the top-level <c>[SerializeReference]</c> array element that points at <paramref name="rid"/> within the
        /// document anchored at <paramref name="fileId"/>, returning its field name and element index (so the missing-list
        /// guard can address it as <c>_field.Array.data[index]</c>). Confined to the object's own field block (before
        /// <c>references:</c>) and to element pointers sitting at their field's indent, so a same-shaped <c>RefIds</c>
        /// entry or a nested pointer is never mistaken for a top-level element. Returns <see langword="false"/> when no
        /// top-level array element holds the rid (e.g. it is a single field or a nested pointer — neither of which a list
        /// resize destroys).
        /// </summary>
        public static bool TryFindTopLevelArrayElementForRid(string assetPath, long fileId, long rid,
            out string field, out int index)
        {
            field = null;
            index = -1;

            try
            {
                if (string.IsNullOrEmpty(assetPath) || !File.Exists(assetPath)) return false;

                var lines = SerializeReferenceYamlProbeCache.ReadAllLines(assetPath);
                var (start, end) = FindDocumentRange(lines, fileId);
                if (start < 0) return false;

                var fieldsEnd = FindFieldsEnd(lines, start, end);

                var headerPattern = new Regex(@"^(?<lead>\s*)(?<name>[^\s:#-][^:]*):\s*$");
                var itemPattern = new Regex(@"^(?<lead>\s*)-\s+rid:\s*(?<rid>-?\d+)\s*$");

                string currentField = null;
                var fieldIndent = -1;
                var count = 0;

                for (var i = start; i < fieldsEnd; i++)
                {
                    if (lines[i].Trim().Length == 0) continue;

                    var item = itemPattern.Match(lines[i]);
                    if (item.Success && currentField != null && item.Groups["lead"].Length == fieldIndent)
                    {
                        if (long.TryParse(item.Groups["rid"].Value, out var elementRid) && elementRid == rid)
                        {
                            field = currentField;
                            index = count;
                            return true;
                        }

                        count++;
                        continue;
                    }

                    var header = headerPattern.Match(lines[i]);
                    if (header.Success)
                    {
                        currentField = header.Groups["name"].Value;
                        fieldIndent = header.Groups["lead"].Length;
                        count = 0;
                    }
                }

                return false;
            }
            catch (Exception exception)
            {
                Debug.LogError($"[Aspid FastTools] Failed to locate array element for rid {rid} in '{assetPath}': {exception}");
                return false;
            }
        }

        // Parses "_field.Array.data[N]" into (field, index). Returns false for a nested path (e.g. an array inside a
        // managed reference's data block) — the restore only re-points a direct top-level array element, which is the
        // shape Unity's default list "+" destroys; deeper paths are left to a future pass rather than risk a mis-target.
        private static bool TryParseTopLevelArrayElement(string elementPath, out string fieldName, out int index)
        {
            fieldName = null;
            index = -1;
            if (string.IsNullOrEmpty(elementPath)) return false;

            var segments = ParsePathSegments(elementPath.Replace(".Array.data", string.Empty));
            if (segments is null || segments.Count != 1) return false;

            var segment = segments[0];
            if (!segment.HasIndex || segment.Index < 0) return false;

            fieldName = segment.Name;
            index = segment.Index;
            return true;
        }

        // Locates the index-th "- rid: N" element line of the top-level array field, scanning only the object's own
        // field block (before "references:") so a same-shaped RefIds entry is never mistaken for an element. Unity
        // writes the "- rid:" items at the field key's own indent.
        private static bool TryFindArrayElementPointer(string[] lines, int start, int fieldsEnd, string fieldName,
            int index, out int pointerLine, out long currentRid)
        {
            pointerLine = -1;
            currentRid = 0;

            var fieldPattern = new Regex($@"^(?<lead>\s*){Regex.Escape(fieldName)}:\s*$");
            var itemPattern = new Regex(@"^(?<lead>\s*)-\s+rid:\s*(?<rid>-?\d+)\s*$");

            for (var i = start; i < fieldsEnd; i++)
            {
                var field = fieldPattern.Match(lines[i]);
                if (!field.Success) continue;

                var fieldIndent = field.Groups["lead"].Length;
                var count = 0;

                for (var j = i + 1; j < fieldsEnd; j++)
                {
                    if (lines[j].Trim().Length == 0) continue;

                    var item = itemPattern.Match(lines[j]);
                    if (!item.Success || item.Groups["lead"].Length != fieldIndent)
                    {
                        // A line at or above the field indent that is not one of our items ends the sequence.
                        if (IndentOf(lines[j]) <= fieldIndent) break;
                        continue;
                    }

                    if (count == index)
                    {
                        pointerLine = j;
                        return long.TryParse(item.Groups["rid"].Value, out currentRid);
                    }

                    count++;
                }

                return false; // field found but the index-th element is not present
            }

            return false;
        }

        // The smallest positive id greater than every "rid: N" in the document. Scans both field pointers and RefIds
        // entries so a reused id can never alias a surviving reference.
        private static long NextFreeRid(string[] lines, int start, int end)
        {
            var ridPattern = new Regex(@"rid:\s*(?<rid>-?\d+)");
            var max = 0L;

            for (var i = start; i < end; i++)
            {
                foreach (Match match in ridPattern.Matches(lines[i]))
                    if (long.TryParse(match.Groups["rid"].Value, out var value) && value > max)
                        max = value;
            }

            return max + 1;
        }

        // Copies the captured entry, rewriting only its header's rid to freshRid (the type / data lines are preserved
        // verbatim). The header is the first line — a "- rid: N" — so its indentation and dash are kept intact.
        private static List<string> RewriteEntryRid(IReadOnlyList<string> entryLines, long freshRid)
        {
            var result = new List<string>(entryLines.Count);
            var headerPattern = new Regex(@"^(?<indent>\s*-\s+rid:\s*)-?\d+(?<trailer>\s*)$");

            for (var i = 0; i < entryLines.Count; i++)
            {
                if (i == 0)
                {
                    var match = headerPattern.Match(entryLines[0]);
                    result.Add(match.Success
                        ? match.Groups["indent"].Value + freshRid + match.Groups["trailer"].Value
                        : entryLines[0]);
                }
                else
                {
                    result.Add(entryLines[i]);
                }
            }

            return result;
        }
    }
}
