using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    internal static partial class SerializeReferenceYamlEditor
    {
        /// <summary>
        /// Reads the managed-reference id (<c>rid</c>) stored at <paramref name="propertyPath"/> within the object
        /// document anchored at <paramref name="fileId"/>. Needed because Unity reports an invalid id for a property
        /// whose type is missing — the real id only survives in the YAML. Resolves the path at any depth, walking each
        /// segment either into a managed reference's <c>RefIds</c> data block (a <c>rid:</c> pointer) or down through a
        /// plain serializable container (a nested struct/class mapping or a <c>List&lt;T&gt;</c> of them), so paths
        /// such as <c>_weapon._chargeEffect</c>, <c>_config._weapon</c> and <c>_slots.Array.data[0]._weapon</c> all
        /// resolve. An unresolvable segment returns <see langword="false"/> so the caller can fall back.
        /// </summary>
        public static bool TryReadReferenceId(string assetPath, long fileId, string propertyPath, out long rid)
        {
            rid = 0;
            try
            {
                if (string.IsNullOrEmpty(assetPath) || !File.Exists(assetPath)) return false;

                var lines = SerializeReferenceYamlProbeCache.ReadAllLines(assetPath);
                var (start, end) = FindDocumentRange(lines, fileId);
                if (start < 0) return false;

                // Field pointers (and the object's inline serializable data) live before the "references:" block; the
                // RefIds entries and the nested data each managed reference stores live after it.
                var fieldsEnd = end;
                var references = new Regex(@"^\s*references:\s*$");
                for (var i = start; i < end; i++)
                    if (references.IsMatch(lines[i])) { fieldsEnd = i; break; }

                var segments = ParsePathSegments(propertyPath.Replace(".Array.data", string.Empty));
                if (segments is null) return false;

                var refIdsStart = FindRefIdsStart(lines, start, end);

                // Cursor over the lines the current segment is resolved against. It starts on the object's own field
                // block, then for each segment either descends into a plain serializable container (a nested mapping
                // or sequence item, by indent) or jumps into a managed reference's RefIds data block (by rid).
                var cursorStart = start;
                var cursorEnd = fieldsEnd;

                // The object's top-level fields all align with the m_Script line's indent (see TryReadScriptGuid), so
                // the first segment is matched at exactly that indent — otherwise a same-named key nested inside an
                // earlier field's serializable container would shadow the real top-level field. A document without a
                // readable script guid falls back to matching at any indent.
                var cursorIndent = TryReadScriptGuid(lines, start + 1, fieldsEnd, out _, out var fieldIndent)
                    ? fieldIndent
                    : -1;

                for (var s = 0; s < segments.Count; s++)
                {
                    var kind = ResolveSegment(lines, cursorStart, cursorEnd, cursorIndent, segments[s],
                        out var segmentRid, out var valueStart, out var valueEnd, out var valueIndent);

                    if (kind == SegmentKind.NotFound) return false;

                    if (s == segments.Count - 1)
                    {
                        if (kind != SegmentKind.Reference) return false;
                        rid = segmentRid;
                        return true;
                    }

                    if (kind == SegmentKind.Reference)
                    {
                        if (refIdsStart < 0) return false;
                        if (!TryGetDataBlockRange(lines, refIdsStart, end, segmentRid, out cursorStart, out cursorEnd, out cursorIndent))
                            return false;
                    }
                    else
                    {
                        cursorStart = valueStart;
                        cursorEnd = valueEnd;
                        cursorIndent = valueIndent;
                    }
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Reads the managed-reference id stored at <paramref name="propertyPath"/> and the type recorded for it in
        /// the <c>RefIds</c> block, in a single pass over the asset YAML. This is how a missing reference is found
        /// even when Unity has dropped it from the live object (notably on prefabs / GameObjects): the orphaned
        /// id, type identity and payload all survive in the file.
        /// </summary>
        public static bool TryReadStoredType(string assetPath, long fileId, string propertyPath, out long rid, out ManagedTypeName type)
        {
            rid = 0;
            type = default;

            if (!TryReadReferenceId(assetPath, fileId, propertyPath, out rid)) return false;

            try
            {
                var lines = SerializeReferenceYamlProbeCache.ReadAllLines(assetPath);
                var (start, end) = FindDocumentRange(lines, fileId);
                if (start < 0) return false;

                var refIdsStart = FindRefIdsStart(lines, start, end);
                if (refIdsStart < 0) return false;

                var ridPattern = new Regex($@"^\s*-\s+rid:\s*{rid}\s*$");
                var typePattern = new Regex(@"^\s*type:\s*\{(?<body>.*)\}\s*$");

                for (var i = refIdsStart; i < end; i++)
                {
                    if (!ridPattern.IsMatch(lines[i])) continue;

                    for (var j = i + 1; j < end && j <= i + 4; j++)
                    {
                        var match = typePattern.Match(lines[j]);
                        if (!match.Success) continue;

                        return TryParseInlineType(match.Groups["body"].Value, out type);
                    }

                    return false;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Parses the top-level field names from the flat YAML payload Unity exposes for an in-memory missing reference
        /// (<see cref="UnityEditor.SerializationUtility.GetManagedReferencesWithMissingTypes"/>
        /// <c>serializedData</c>). Mirrors the in-memory data-recovery parser: only top-level <c>key: value</c> scalars
        /// (and mapping/sequence headers) are reported; indented and sequence-item lines are skipped.
        /// </summary>
        public static List<string> ParseTopLevelFieldNames(string serializedData)
        {
            var result = new List<string>();
            if (string.IsNullOrEmpty(serializedData)) return result;

            foreach (var raw in serializedData.Split('\n'))
            {
                var line = raw.TrimEnd('\r');
                if (line.Length == 0 || char.IsWhiteSpace(line[0]) || line[0] == '-') continue;

                var separator = line.IndexOf(':');
                if (separator <= 0) continue;

                var key = line[..separator].Trim();
                if (key.Length > 0) result.Add(key);
            }

            return result;
        }

        /// <summary>
        /// Reads the top-level serialized field names recorded for the managed reference <paramref name="rid"/> within
        /// the object document anchored at <paramref name="fileId"/> — the direct keys of the entry's <c>data:</c>
        /// block. Used by the Smart Fix suggestion's field-shape heuristic to compare the orphaned payload's shape
        /// against a candidate type's serialized fields. Nested mappings and sequences are reported by their key only.
        /// </summary>
        public static List<string> GetReferenceFieldNames(string assetPath, long fileId, long rid)
        {
            var result = new List<string>();

            try
            {
                if (string.IsNullOrEmpty(assetPath) || !File.Exists(assetPath)) return result;

                var lines = SerializeReferenceYamlProbeCache.ReadAllLines(assetPath);
                var (start, end) = FindDocumentRange(lines, fileId);
                if (start < 0) return result;

                var refIdsStart = FindRefIdsStart(lines, start, end);
                if (refIdsStart < 0 || !TryGetDataBlockRange(lines, refIdsStart, end, rid, out var blockStart, out var blockEnd, out var childIndent)) return result;
                CollectTopLevelKeys(lines, blockStart, blockEnd, childIndent, result);
            }
            catch (Exception)
            {
                // Best effort — an unreadable block simply yields no field-shape signal.
            }

            return result;
        }

        // Splits a normalized property path ("_weapon._chargeEffect", "_alternates[3]") into ordered segments.
        private static List<PathSegment> ParsePathSegments(string path)
        {
            var result = new List<PathSegment>();
            foreach (var raw in path.Split('.'))
            {
                var match = Regex.Match(raw, @"^(?<name>[^\[\]\.]+)(\[(?<idx>\d+)\])?$");
                if (!match.Success) return null;

                var hasIndex = match.Groups["idx"].Success;
                result.Add(new PathSegment(match.Groups["name"].Value, hasIndex, hasIndex ? int.Parse(match.Groups["idx"].Value) : -1));
            }

            return result.Count > 0 ? result : null;
        }

        // Resolves one path segment within [rangeStart, rangeEnd): a "rid:" value is a managed reference, anything
        // else a container to descend into; an indexed segment resolves the Index-th sequence item. A non-negative
        // requiredIndent matches only the block's direct children (a leading "- " counts toward the indent), so a
        // deeper object that reuses the name is never picked up.
        private static SegmentKind ResolveSegment(string[] lines, int rangeStart, int rangeEnd, int requiredIndent,
            PathSegment segment, out long rid, out int valueStart, out int valueEnd, out int valueIndent)
        {
            rid = 0;
            valueStart = valueEnd = valueIndent = -1;

            var fieldPattern = new Regex($@"^(?<lead>\s*)(?<dash>-\s+)?{Regex.Escape(segment.Name)}:\s*(?<inline>.*)$");

            for (var i = rangeStart; i < rangeEnd; i++)
            {
                var field = fieldPattern.Match(lines[i]);
                if (!field.Success) continue;

                var fieldIndent = field.Groups["lead"].Length + field.Groups["dash"].Length;
                if (requiredIndent >= 0 && fieldIndent != requiredIndent) continue;

                return segment.HasIndex
                    ? ResolveSequenceItem(lines, i + 1, rangeEnd, segment.Index, out rid, out valueStart, out valueEnd, out valueIndent)
                    : ClassifyValue(lines, i, fieldIndent, field.Groups["inline"].Value, rangeEnd, out rid, out valueStart, out valueEnd, out valueIndent);
            }

            return SegmentKind.NotFound;
        }

        // Classifies the value of a plain (non-indexed) field at line i with effective indent fieldIndent: a managed
        // reference when the value is a lone "rid:" scalar (inline or as the only following child), otherwise the
        // indented mapping block to descend into.
        private static SegmentKind ClassifyValue(string[] lines, int i, int fieldIndent, string inline, int rangeEnd,
            out long rid, out int valueStart, out int valueEnd, out int valueIndent)
        {
            rid = 0;
            valueStart = valueEnd = valueIndent = -1;

            var inlineMatch = Regex.Match(inline, @"rid:\s*(-?\d+)");
            if (inlineMatch.Success)
                return long.TryParse(inlineMatch.Groups[1].Value, out rid) ? SegmentKind.Reference : SegmentKind.NotFound;

            // Gather the indented value block (lines more indented than the field).
            var blockStart = i + 1;
            var blockEnd = rangeEnd;
            var firstChild = -1;

            for (var j = blockStart; j < rangeEnd; j++)
            {
                if (lines[j].Trim().Length == 0) continue;
                if (IndentOf(lines[j]) <= fieldIndent) { blockEnd = j; break; }
                if (firstChild < 0) firstChild = j;
            }

            if (firstChild < 0)
                return SegmentKind.NotFound; // scalar/empty field: no managed reference here

            // A managed reference's value block is exactly a "rid:" scalar; anything else is a container.
            var ridScalar = Regex.Match(lines[firstChild].Trim(), @"^rid:\s*(-?\d+)$");
            if (ridScalar.Success)
                return long.TryParse(ridScalar.Groups[1].Value, out rid) ? SegmentKind.Reference : SegmentKind.NotFound;

            valueStart = blockStart;
            valueEnd = blockEnd;
            valueIndent = IndentOf(lines[firstChild]);

            return SegmentKind.Container;
        }

        // Locates the index-th "- " item of a sequence whose items begin at [itemsStart, rangeEnd). A "- rid: N" item
        // is a managed reference; a "- field: …" item is a mapping container whose fields begin on the dash line.
        private static SegmentKind ResolveSequenceItem(string[] lines, int itemsStart, int rangeEnd, int index, out long rid, out int valueStart, out int valueEnd, out int valueIndent)
        {
            rid = 0;
            valueStart = valueEnd = valueIndent = -1;

            var itemPattern = new Regex(@"^(?<lead>\s*)-\s");
            var itemIndent = -1;
            var count = 0;

            for (var j = itemsStart; j < rangeEnd; j++)
            {
                if (lines[j].Trim().Length == 0) continue;

                var item = itemPattern.Match(lines[j]);
                if (!item.Success)
                {
                    if (itemIndent >= 0 && IndentOf(lines[j]) <= itemIndent) break; // dedented out of the sequence
                    continue;
                }

                var indent = item.Groups["lead"].Length;

                if (itemIndent < 0) itemIndent = indent;
                else if (indent < itemIndent) break;    // dedented out of the sequence
                else if (indent > itemIndent) continue; // item of a nested sequence, not ours

                if (count == index)
                {
                    var ridMatch = Regex.Match(lines[j].TrimStart(), @"^-\s+rid:\s*(-?\d+)\s*$");
                    if (ridMatch.Success)
                        return long.TryParse(ridMatch.Groups[1].Value, out rid) ? SegmentKind.Reference : SegmentKind.NotFound;

                    // Mapping item: it runs until the next sibling "- " or a dedent; its fields start one "- " past
                    // the item indent.
                    var itemEnd = rangeEnd;
                    for (var k = j + 1; k < rangeEnd; k++)
                    {
                        if (lines[k].Trim().Length == 0) continue;
                        var ind = IndentOf(lines[k]);

                        if (ind < itemIndent || (ind == itemIndent && itemPattern.IsMatch(lines[k])))
                        {
                            itemEnd = k;
                            break;
                        }
                    }

                    valueStart = j;
                    valueEnd = itemEnd;
                    valueIndent = itemIndent + 2;
                    return SegmentKind.Container;
                }

                count++;
            }

            return SegmentKind.NotFound;
        }

        // Locates the RefIds entry for rid and returns the line range of its "data:" block plus the indent of that
        // block's direct children, so a nested segment can be resolved within the right scope.
        private static bool TryGetDataBlockRange(string[] lines, int refIdsStart, int docEnd, long rid, out int blockStart, out int blockEnd, out int childIndent)
        {
            blockStart = blockEnd = childIndent = -1;
            var ridPattern = new Regex($@"^(?<indent>\s*)-\s+rid:\s*{rid}\s*$");
            var dataPattern = new Regex(@"^\s*data:\s*$");

            for (var i = refIdsStart; i < docEnd; i++)
            {
                var match = ridPattern.Match(lines[i]);
                if (!match.Success) continue;

                // The entry runs until the next list item at its own indent, or until the block dedents out of it.
                var entryIndent = match.Groups["indent"].Length;
                var entryEnd = docEnd;

                for (var j = i + 1; j < docEnd; j++)
                {
                    if (lines[j].Trim().Length == 0) continue;

                    var indent = IndentOf(lines[j]);
                    if (indent < entryIndent || (indent == entryIndent && lines[j].TrimStart().StartsWith("- ")))
                    {
                        entryEnd = j;
                        break;
                    }
                }

                for (var j = i + 1; j < entryEnd; j++)
                {
                    if (!dataPattern.IsMatch(lines[j])) continue;

                    blockStart = j + 1;
                    blockEnd = entryEnd;

                    for (var k = blockStart; k < blockEnd; k++)
                    {
                        if (lines[k].Trim().Length > 0)
                        {
                            childIndent = IndentOf(lines[k]);
                            break;
                        }
                    }

                    return blockStart < blockEnd && childIndent >= 0;
                }

                return false;
            }

            return false;
        }

        // Collects the keys of "name: …" entries at exactly childIndent within [blockStart, blockEnd), skipping the
        // deeper lines of nested mappings/sequences so only the block's own top-level fields are reported. Sequence
        // items ("- rid: 7", "- _damage: 5") sit at the parent key's own indent, so the key pattern excludes the dash
        // — they are items of an already-reported field, not keys ("- rid" / "- _damage" would be pseudo-keys).
        private static void CollectTopLevelKeys(string[] lines, int blockStart, int blockEnd, int childIndent, List<string> result)
        {
            var keyPattern = new Regex(@"^(?<indent>\s*)(?<key>[^\s:#-][^:]*):(\s.*|\s*)$");

            for (var i = blockStart; i < blockEnd; i++)
            {
                if (lines[i].Trim().Length == 0) continue;
                if (IndentOf(lines[i]) != childIndent) continue; // a nested line, not a direct field of this block

                var match = keyPattern.Match(lines[i]);
                if (!match.Success) continue;
                if (match.Groups["indent"].Length != childIndent) continue;

                result.Add(match.Groups["key"].Value.Trim());
            }
        }

        // What a resolved segment points at: a managed reference (a "rid:" pointer) or a plain serializable container
        // (a nested mapping/sequence to descend into), or nothing.
        private enum SegmentKind
        {
            NotFound,
            Reference,
            Container
        }

        // A single managed-reference path segment: a field name with an optional sequence index ("_alternates[3]").
        private readonly struct PathSegment
        {
            public readonly int Index;
            public readonly string Name;
            public readonly bool HasIndex;

            public PathSegment(string name, bool hasIndex, int index)
            {
                Name = name;
                Index = index;
                HasIndex = hasIndex;
            }
        }
    }
}
