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
        // The inline type mapping Unity writes for the null sentinel RefIds entry — an empty type identity.
        private const string NullSentinelType = "type: {class: , ns: , asm: }";

        /// <summary>
        /// Replaces the <c>type:</c> mapping of the <c>RefIds</c> entry identified by <paramref name="rid"/> within
        /// the object document anchored at <paramref name="fileId"/>. Returns <see langword="true"/> when the file
        /// was rewritten; the caller is responsible for reimporting the asset.
        /// </summary>
        public static bool TryRewriteType(string assetPath, long fileId, long rid, ManagedTypeName newType)
        {
            // Single scan shared with the diff preview: compute the edit, then apply exactly that line so the preview
            // and the applied result can never diverge.
            if (!TryComputeRewrite(assetPath, fileId, rid, newType, out var edit)) return false;

            try
            {
                var lines = File.ReadAllLines(assetPath);
                if (edit.LineNumber < 0 || edit.LineNumber >= lines.Length || lines[edit.LineNumber] != edit.OldLine)
                    return false; // the file changed since the edit was computed — abort rather than write a stale line

                lines[edit.LineNumber] = edit.NewLine;
                WritePreservingNewlines(assetPath, lines);
                // Same-tick writes can leave the modification-time key unchanged, so bust the probe cache explicitly.
                SerializeReferenceYamlProbeCache.ClearCache();
                return true;
            }
            catch (Exception exception)
            {
                Debug.LogError($"[TypeSelector] Failed to rewrite managed-reference type in '{assetPath}': {exception}");
                return false;
            }
        }

        /// <summary>
        /// Computes — without writing — the single line change a <see cref="TryRewriteType"/> would make to re-point the
        /// <paramref name="rid"/> entry to <paramref name="newType"/>. Drives the bulk-fix diff preview; the rewrite
        /// applies the returned edit verbatim, so what the preview shows is exactly what is written.
        /// </summary>
        public static bool TryComputeRewrite(string assetPath, long fileId, long rid, ManagedTypeName newType, out RewriteEdit edit)
        {
            edit = default;

            try
            {
                if (string.IsNullOrEmpty(assetPath) || !File.Exists(assetPath)) return false;

                var lines = File.ReadAllLines(assetPath);
                if (!LooksLikeUnityYaml(lines)) return false; // never offer (or apply) a rewrite on a non-Unity YAML file
                var (start, end) = FindDocumentRange(lines, fileId);
                if (start < 0) return false;

                // Field pointers ("_sidearms:\n  - rid: 1002") share the "- rid:" shape with RefIds entries, so confine
                // the search to the RefIds block — the entries are the only ones with a following type:.
                var refIdsStart = FindRefIdsStart(lines, start, end);
                if (refIdsStart < 0) return false;

                var ridPattern = new Regex($@"^\s*-\s+rid:\s*{rid}\s*$");
                var typePattern = new Regex(@"^(?<indent>\s*type:\s*)\{.*\}\s*$");

                for (var i = refIdsStart; i < end; i++)
                {
                    if (!ridPattern.IsMatch(lines[i])) continue;

                    // The type mapping follows the rid line; scan a few lines to tolerate formatting variance.
                    for (var j = i + 1; j < end && j <= i + 4; j++)
                    {
                        var match = typePattern.Match(lines[j]);
                        if (!match.Success) continue;

                        edit = new RewriteEdit(assetPath, j, lines[j], match.Groups["indent"].Value + newType.ToYamlType());
                        return true;
                    }

                    return false;
                }

                return false;
            }
            catch (Exception exception)
            {
                Debug.LogError($"[TypeSelector] Failed to compute managed-reference rewrite in '{assetPath}': {exception}");
                return false;
            }
        }

        /// <summary>
        /// Deletes a whole <c>- rid: N</c> entry (its type and data block) from the <c>RefIds</c> list of the document
        /// anchored at <paramref name="fileId"/>. Used to drop an orphaned managed-reference payload that no field points
        /// at. Confined to the <c>RefIds</c> block so a same-shaped field pointer is never touched. Returns whether an
        /// entry was removed. The edit is not undoable — callers confirm first.
        /// </summary>
        public static bool TryRemoveEntry(string assetPath, long fileId, long rid)
        {
            try
            {
                if (string.IsNullOrEmpty(assetPath) || !File.Exists(assetPath)) return false;

                var lines = File.ReadAllLines(assetPath);
                if (!LooksLikeUnityYaml(lines)) return false; // never rewrite a non-Unity YAML file
                var (start, end) = FindDocumentRange(lines, fileId);
                if (start < 0) return false;

                var refIdsStart = FindRefIdsStart(lines, start, end);
                if (refIdsStart < 0) return false;

                var ridPattern = new Regex($@"^(?<indent>\s*)-\s+rid:\s*{rid}\s*$");

                for (var i = refIdsStart; i < end; i++)
                {
                    var match = ridPattern.Match(lines[i]);
                    if (!match.Success) continue;

                    // The entry runs until the next list item at its own indent, or until the block dedents out of it —
                    // the same bounding rule the data-block reader uses.
                    var entryIndent = match.Groups["indent"].Length;
                    var entryEnd = FindEntryEnd(lines, i, end, entryIndent);

                    // Unexpected (tab / mixed) indentation in the entry block means IndentOf and the "- rid:" \s* regex
                    // can disagree on where the block ends — bail rather than write a possibly mis-bounded deletion.
                    if (!BlockIndentIsTrusted(lines, i, entryEnd)) return false;

                    var remaining = new List<string>(lines.Length - (entryEnd - i));
                    for (var k = 0; k < i; k++) remaining.Add(lines[k]);
                    for (var k = entryEnd; k < lines.Length; k++) remaining.Add(lines[k]);

                    WritePreservingNewlines(assetPath, remaining);
                    SerializeReferenceYamlProbeCache.ClearCache();
                    return true;
                }

                return false;
            }
            catch (Exception exception)
            {
                Debug.LogError($"[Aspid FastTools] Failed to remove RefIds entry rid {rid} in '{assetPath}': {exception}");
                return false;
            }
        }

        /// <summary>
        /// Nulls a managed reference in the document anchored at <paramref name="fileId"/>: every field / array-element
        /// pointer that holds <paramref name="rid"/> is rewritten to the null id (<c>-2</c>), the now-orphaned
        /// <c>RefIds</c> entry is removed, and — when a null pointer was introduced — the <c>RefIds</c> null sentinel
        /// entry (<c>- rid: -2 / type: {class: , ns: , asm: }</c>) is added if absent. This reproduces exactly what Unity
        /// writes when a <c>[SerializeReference]</c> field is set to <see langword="null"/>: an array element cannot be
        /// dropped, so it must point at <c>-2</c>, and that pointer is only valid when the sentinel entry exists —
        /// without it the load errors "serialized array … is missing entry for Refid -2". Removing the broken entry
        /// clears the object's missing-types flag. Not undoable: the broken payload is discarded. Returns whether the
        /// file was rewritten.
        /// </summary>
        public static bool TryNullReference(string assetPath, long fileId, long rid)
        {
            try
            {
                if (string.IsNullOrEmpty(assetPath) || !File.Exists(assetPath)) return false;

                var lines = File.ReadAllLines(assetPath);
                if (!LooksLikeUnityYaml(lines)) return false; // never rewrite a non-Unity YAML file
                var (start, end) = FindDocumentRange(lines, fileId);
                if (start < 0) return false;

                var refIdsStart = FindRefIdsStart(lines, start, end);
                if (refIdsStart < 0) return false;

                // RefIds entry headers sit at the shallowest "- rid:" indent under RefIds; a pointer to the rid lives
                // anywhere else — a field/array element before "references:" or a nested reference inside another entry's
                // data block. The header for this rid is removed; every pointer to it becomes the null id.
                var entryIndent = FindRefIdsEntryIndent(lines, refIdsStart, end);
                if (entryIndent < 0) return false;

                var headerPattern = new Regex($@"^(?<indent>\s*)-\s+rid:\s*{rid}\s*$");
                var pointerToken = BuildPointerPattern(rid);

                var headerIndex = -1;
                var pointerNulled = false;

                for (var i = start; i < end; i++)
                {
                    // This rid's own RefIds entry header (a "- rid: N" under RefIds at the entry indent) is removed
                    // below, not nulled — skip it so it isn't rewritten to the null id.
                    if (headerIndex < 0 && i > refIdsStart)
                    {
                        var header = headerPattern.Match(lines[i]);
                        if (header.Success && header.Groups["indent"].Length == entryIndent)
                        {
                            headerIndex = i;
                            continue;
                        }
                    }

                    // Null every pointer to the rid — a "- rid: N" array element, a "rid: N" scalar field or an inline
                    // "{rid: N}" — so no dangling pointer survives the entry's removal (which errors on array fields).
                    // The anchored pattern preserves each pointer's structural prefix/suffix and only rewrites the id.
                    if (pointerToken.IsMatch(lines[i]))
                    {
                        lines[i] = pointerToken.Replace(lines[i], $"${{prefix}}rid: {NullRid}${{suffix}}");
                        pointerNulled = true;
                    }
                }

                // Nothing referenced or stored this rid — leave the file untouched. (When an entry exists but is already
                // unreferenced this still drops it; when only a dangling pointer remains this still nulls it.)
                if (headerIndex < 0 && !pointerNulled) return false;

                var blockStart = headerIndex;
                var blockEnd = headerIndex >= 0 ? FindEntryEnd(lines, headerIndex, end, entryIndent) : -1;

                // The entry block we're about to drop must use Unity's space-only indentation; a tab / mixed prefix can
                // mis-bound it (IndentOf vs the "- rid:" \s* regex), so bail before this non-undoable rewrite. (Pointer
                // nulling above is line-local and indent-agnostic, so no write has reached disk yet.)
                if (headerIndex >= 0 && !BlockIndentIsTrusted(lines, blockStart, blockEnd)) return false;

                // A "- rid: -2" pointer is valid only while the RefIds list carries Unity's null sentinel entry; add it
                // when we just introduced a null pointer and the document does not already have one (a shared singleton).
                var needsNullEntry = pointerNulled && !HasNullSentinelEntry(lines, refIdsStart, end, entryIndent);
                var dash = new string(' ', entryIndent);
                var typeIndent = new string(' ', entryIndent + 2);

                var result = new List<string>(lines.Length + 2);
                for (var i = 0; i < lines.Length; i++)
                {
                    if (headerIndex >= 0 && i >= blockStart && i < blockEnd) continue; // drop the broken entry block

                    result.Add(lines[i]);

                    // Insert the sentinel as the RefIds list's first entry, mirroring where Unity writes it.
                    if (needsNullEntry && i == refIdsStart)
                    {
                        result.Add($"{dash}- rid: {NullRid}");
                        result.Add($"{typeIndent}{NullSentinelType}");
                    }
                }

                WritePreservingNewlines(assetPath, result);
                SerializeReferenceYamlProbeCache.ClearCache();
                return true;
            }
            catch (Exception exception)
            {
                Debug.LogError($"[Aspid FastTools] Failed to null managed-reference rid {rid} in '{assetPath}': {exception}");
                return false;
            }
        }

        /// <summary>
        /// Counts how many field / array-element pointers in the document anchored at <paramref name="fileId"/> hold
        /// <paramref name="rid"/> — the number of slots a <see cref="TryNullReference"/> would null. A missing reference
        /// can be aliased across several slots (all sharing one rid); clearing it nulls every one of them, so the confirm
        /// dialog calls this first to name the count before the irreversible rewrite. The rid's own <c>RefIds</c> entry
        /// header is excluded (it is the entry, not a pointer to it). Returns <c>0</c> when the document / <c>RefIds</c>
        /// list / entry indent cannot be located — the same guards <see cref="TryNullReference"/> uses — so a non-positive
        /// result means "count unknown" and the caller can fall back to unnumbered wording.
        /// </summary>
        public static int CountPointersTo(string assetPath, long fileId, long rid)
        {
            try
            {
                if (string.IsNullOrEmpty(assetPath) || !File.Exists(assetPath)) return 0;

                var lines = File.ReadAllLines(assetPath);
                var (start, end) = FindDocumentRange(lines, fileId);
                if (start < 0) return 0;

                var refIdsStart = FindRefIdsStart(lines, start, end);
                if (refIdsStart < 0) return 0;

                var entryIndent = FindRefIdsEntryIndent(lines, refIdsStart, end);
                if (entryIndent < 0) return 0;

                var headerPattern = new Regex($@"^(?<indent>\s*)-\s+rid:\s*{rid}\s*$");
                var pointerToken = BuildPointerPattern(rid);

                var headerSkipped = false;
                var count = 0;

                for (var i = start; i < end; i++)
                {
                    // Skip this rid's own RefIds entry header exactly once — it is the entry, not a pointer. Mirrors
                    // the header skip in TryNullReference so the count equals the pointers that path would rewrite.
                    if (!headerSkipped && i > refIdsStart)
                    {
                        var header = headerPattern.Match(lines[i]);
                        if (header.Success && header.Groups["indent"].Length == entryIndent)
                        {
                            headerSkipped = true;
                            continue;
                        }
                    }

                    if (pointerToken.IsMatch(lines[i])) count++;
                }

                return count;
            }
            catch (Exception exception)
            {
                Debug.LogError($"[Aspid FastTools] Failed to count managed-reference pointers to rid {rid} in '{assetPath}': {exception}");
                return 0;
            }
        }

        // Anchored matcher for a real "rid: N" pointer — never a bare "rid: N" substring inside a string field value.
        // Only Unity's three pointer shapes match: a line-anchored "- rid: N" item, a line-anchored "rid: N" scalar,
        // or an inline "{rid: N}" mapping; the structural prefix/suffix are captured so a rewrite replaces only the id.
        private static Regex BuildPointerPattern(long rid) => new(
            $@"(?<prefix>^\s*(?:-\s+)?)rid:\s*{rid}(?<suffix>\s*$)|(?<prefix>\{{\s*)rid:\s*{rid}(?<suffix>\s*\}})");

        // Whether the RefIds list already carries Unity's null sentinel entry ("- rid: -2"). The sentinel is a shared
        // singleton — at most one per object — so a second null pointer reuses it rather than adding another.
        private static bool HasNullSentinelEntry(string[] lines, int refIdsStart, int end, int entryIndent)
        {
            var sentinel = new Regex($@"^(?<indent>\s*)-\s+rid:\s*{NullRid}\s*$");
            for (var i = refIdsStart + 1; i < end; i++)
            {
                var match = sentinel.Match(lines[i]);
                if (match.Success && match.Groups["indent"].Length == entryIndent) return true;
            }

            return false;
        }

        // The indent of the RefIds list's entry headers: the first "- rid:" line under RefIds. Entries sit at this
        // shallowest dash indent; nested reference pointers inside their data blocks are deeper. -1 when the block is empty.
        private static int FindRefIdsEntryIndent(string[] lines, int refIdsStart, int end)
        {
            var entry = new Regex(@"^(?<indent>\s*)-\s+rid:\s*-?\d+\s*$");
            for (var i = refIdsStart + 1; i < end; i++)
            {
                var match = entry.Match(lines[i]);
                if (match.Success) return match.Groups["indent"].Length;
            }

            return -1;
        }
    }
}
