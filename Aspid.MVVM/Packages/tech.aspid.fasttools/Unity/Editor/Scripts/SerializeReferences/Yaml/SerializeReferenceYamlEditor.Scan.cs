using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    internal static partial class SerializeReferenceYamlEditor
    {
        // "--- !u!114 &11400000" — a MonoBehaviour document header (class id 114), the only kind that carries m_Script
        // and serialized user fields, so the scene required-field scan iterates these alone.
        private static readonly Regex _monoBehaviourHeader = new(@"^--- !u!114 &(\d+)", RegexOptions.Compiled);

        // "  m_Script: {fileID: 11500000, guid: <guid>, type: 3}" — the script reference whose guid maps to the C# type;
        // its indent is the document's top-level field indent (every direct field of the MonoBehaviour aligns with it).
        private static readonly Regex _scriptGuidPattern =
            new(@"^(?<indent>\s*)m_Script:\s*\{.*\bguid:\s*(?<guid>[0-9a-fA-F]+).*\}", RegexOptions.Compiled);

        // "  references:" — the managed-reference block; the object's own serialized fields all precede it.
        private static readonly Regex _referencesKey = new(@"^\s*references:\s*$", RegexOptions.Compiled);

        /// <summary>
        /// Scans every object document in the asset and returns each <c>RefIds</c> entry whose stored type fails
        /// the <paramref name="resolves"/> predicate. Because <c>RefIds</c> is a flat per-object list, this finds
        /// missing references at any nesting depth and on any child object — without navigating the Inspector.
        /// </summary>
        public static List<MissingReferenceEntry> FindMissingReferences(string assetPath, Func<ManagedTypeName, bool> resolves)
        {
            var result = new List<MissingReferenceEntry>();

            try
            {
                if (string.IsNullOrEmpty(assetPath) || !File.Exists(assetPath)) return result;

                var lines = File.ReadAllLines(assetPath);

                var headers = new List<(long fileId, int start)>();
                for (var i = 0; i < lines.Length; i++)
                {
                    var match = DocumentHeader.Match(lines[i]);
                    if (match.Success && long.TryParse(match.Groups["id"].Value, out var fileId))
                        headers.Add((fileId, i));
                }

                var ridPattern = new Regex(@"^(?<indent>\s*)-\s+rid:\s*(?<rid>-?\d+)\s*$");
                var typePattern = new Regex(@"^\s*type:\s*\{(?<body>.*)\}\s*$");

                for (var h = 0; h < headers.Count; h++)
                {
                    var (fileId, start) = headers[h];
                    var end = h + 1 < headers.Count ? headers[h + 1].start : lines.Length;

                    var refIdsStart = FindRefIdsStart(lines, start, end);
                    if (refIdsStart < 0) continue;

                    // Entry headers sit at the shallowest "- rid:" indent under RefIds; a deeper "- rid:" is a nested
                    // array-element pointer, not an entry — matching it would report a phantom missing entry and aim
                    // a repair at the wrong line. Same entry-indent discrimination TryNullReference uses.
                    var entryIndent = FindRefIdsEntryIndent(lines, refIdsStart, end);
                    if (entryIndent < 0) continue;

                    for (var i = refIdsStart + 1; i < end; i++)
                    {
                        var ridMatch = ridPattern.Match(lines[i]);
                        if (!ridMatch.Success || ridMatch.Groups["indent"].Length != entryIndent ||
                            !long.TryParse(ridMatch.Groups["rid"].Value, out var rid)) continue;

                        for (var j = i + 1; j < end && j <= i + 4; j++)
                        {
                            var typeMatch = typePattern.Match(lines[j]);
                            if (!typeMatch.Success) continue;

                            if (TryParseInlineType(typeMatch.Groups["body"].Value, out var type) &&
                                !type.IsEmpty && !resolves(type))
                            {
                                result.Add(new MissingReferenceEntry(fileId, rid, type));
                            }

                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Best effort — a parse failure simply yields no repair candidates.
            }

            return result;
        }

        /// <summary>
        /// Pure-YAML scan for unset <c>[TypeSelector(Required = true)]</c> fields — the scene-safe counterpart of the
        /// object-load required check, which cannot read scene objects (<see cref="SerializeReferenceHelpers.IsScene"/>).
        /// Walks every MonoBehaviour document, resolves its required fields through <paramref name="requiredFieldsForScript"/>
        /// (keyed by the <c>m_Script</c> guid, so this method stays reflection-free), and reports each top-level required
        /// field that is <em>present but empty</em>: a managed reference at the null id (<c>-2</c>), or an empty string field.
        /// A present managed reference (<c>rid &gt;= 0</c>) counts as set even when its type is missing — that mirrors
        /// <see cref="SerializeReferenceRequiredGate.IsViolation"/>, where a missing type is the missing-type gate's
        /// concern, not a required violation. A field whose key is <em>absent</em> from the document is NOT a violation:
        /// Unity omits a serialized field from YAML when the object was last saved before the field was added (and for
        /// stripped / nested-prefab docs), so flagging it would fail a project that is valid once reopened/reserialized.
        /// A required field nested inside plain <c>[Serializable]</c> containers is read by first narrowing the window
        /// to the innermost container block along <see cref="RequiredFieldDescriptor.Parents"/>; an absent ancestor key
        /// counts as absent for the same reserialize reason.
        /// </summary>
        public static List<RequiredViolationEntry> FindUnsetRequiredFields(
            string assetPath, Func<string, IReadOnlyList<RequiredFieldDescriptor>> requiredFieldsForScript)
        {
            var result = new List<RequiredViolationEntry>();
            if (requiredFieldsForScript is null) return result;

            try
            {
                if (string.IsNullOrEmpty(assetPath) || !File.Exists(assetPath)) return result;

                // One-shot bulk read like FindMissingReferences — bypass the probe cache so large scene files don't evict
                // the interactive per-property entries (see SerializeReferenceYamlProbeCache remarks).
                var lines = File.ReadAllLines(assetPath);

                for (var i = 0; i < lines.Length; i++)
                {
                    var header = _monoBehaviourHeader.Match(lines[i]);
                    if (!header.Success || !long.TryParse(header.Groups[1].Value, out var fileId)) continue;

                    var docEnd = NextDocumentStart(lines, i + 1);
                    if (!TryReadScriptGuid(lines, i + 1, docEnd, out var guid, out var fieldIndent)) continue;

                    var required = requiredFieldsForScript(guid);
                    if (required is null || required.Count == 0) continue;

                    var fieldsEnd = FindFieldsEnd(lines, i + 1, docEnd);

                    foreach (var descriptor in required)
                    {
                        // A nested field is read inside its container block; an absent ancestor key means the whole
                        // chain is absent (needs a reserialize), so the descriptor is skipped like an absent leaf.
                        var start = i + 1;
                        var end = fieldsEnd;
                        var indent = fieldIndent;
                        if (!TryEnterContainers(lines, ref start, ref end, ref indent, descriptor.Parents)) continue;

                        // An absent key is not a violation (see FieldState); only a present-but-empty key is reported.
                        var rid = 0L;
                        var state = descriptor.Kind switch
                        {
                            RequiredFieldKind.String =>
                                IsStringFieldUnset(lines, start, end, indent, descriptor.FieldName),
                            RequiredFieldKind.SerializableType =>
                                IsSerializableTypeFieldUnset(lines, start, end, indent, descriptor.FieldName),
                            _ => IsManagedReferenceUnset(lines, start, end, indent, descriptor.FieldName, out rid),
                        };

                        if (state == FieldState.PresentUnset)
                            result.Add(new RequiredViolationEntry(fileId, descriptor.Path, rid));
                    }
                }
            }
            catch (Exception)
            {
                // Best effort — a parse failure simply yields no violations (matches FindMissingReferences).
            }

            return result;
        }

        // The line index of the next "--- " document separator at or after `from`, or the line count when none follows.
        private static int NextDocumentStart(string[] lines, int from)
        {
            for (var i = from; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("--- ", StringComparison.Ordinal))
                    return i;
            }

            return lines.Length;
        }

        // Reads the m_Script guid (and the document's top-level field indent) within [start, end). False for a stripped
        // component or any document carrying no script reference.
        private static bool TryReadScriptGuid(string[] lines, int start, int end, out string guid, out int fieldIndent)
        {
            guid = null;
            fieldIndent = 0;

            for (var i = start; i < end; i++)
            {
                var match = _scriptGuidPattern.Match(lines[i]);
                if (!match.Success) continue;

                guid = match.Groups["guid"].Value;
                fieldIndent = match.Groups["indent"].Length;
                return true;
            }

            return false;
        }

        // Narrows the [start, end) window and key indent to the innermost container block along `parents` — each hop
        // finds the container key at the current indent and descends to its children's indent. False when any ancestor
        // key is absent or the container block is empty; the caller treats that as an absent leaf (needs reserialize).
        private static bool TryEnterContainers(string[] lines, ref int start, ref int end, ref int indent, string[] parents)
        {
            if (parents is not { Length: > 0 }) return true;

            foreach (var parent in parents)
            {
                var pattern = new Regex($@"^(?<indent>\s*){Regex.Escape(parent)}:\s*$");
                var entered = false;

                for (var i = start; i < end; i++)
                {
                    var match = pattern.Match(lines[i]);
                    if (!match.Success || match.Groups["indent"].Length != indent) continue;

                    // The block spans until the first line dedented back to (or above) the container key; its direct
                    // children sit at the first child line's indent — deeper lines belong to sub-blocks and are
                    // filtered out by the leaf matchers' exact-indent checks.
                    var blockEnd = i + 1;
                    var childIndent = -1;

                    for (var j = i + 1; j < end; j++)
                    {
                        if (lines[j].Trim().Length == 0) { blockEnd = j + 1; continue; }
                        if (IndentOf(lines[j]) <= indent) break;

                        if (childIndent < 0) childIndent = IndentOf(lines[j]);
                        blockEnd = j + 1;
                    }

                    if (childIndent < 0) return false; // container key present but the block carries no children

                    start = i + 1;
                    end = blockEnd;
                    indent = childIndent;
                    entered = true;
                    break;
                }

                if (!entered) return false;
            }

            return true;
        }

        // The exclusive end of a MonoBehaviour's own serialized fields: the "references:" line, or the document end when
        // the object holds no managed references. Confines field lookups so a key inside a RefIds data block is never read.
        private static int FindFieldsEnd(string[] lines, int start, int end)
        {
            for (var i = start; i < end; i++)
            {
                if (_referencesKey.IsMatch(lines[i]))
                    return i;
            }

            return end;
        }

        // Classifies the top-level managed-reference field `name`: PresentUnset when its pointer is the null id (-2) or it
        // carries no rid, PresentSet when a real rid (>= 0) points at a reference, Absent when the key is not in the doc.
        // `rid` returns the read id (or -2 when present-unset/absent).
        private static FieldState IsManagedReferenceUnset(string[] lines, int start, int end, int fieldIndent, string name, out long rid)
        {
            rid = NullRid;
            var pattern = new Regex($@"^(?<indent>\s*){Regex.Escape(name)}:\s*(?<inline>.*)$");

            for (var i = start; i < end; i++)
            {
                var field = pattern.Match(lines[i]);
                if (!field.Success || field.Groups["indent"].Length != fieldIndent) continue;

                // Inline pointer (e.g. "_weapon: {rid: 5}") — Unity writes the block form, but tolerate either.
                var inline = Regex.Match(field.Groups["inline"].Value, @"rid:\s*(-?\d+)");
                if (inline.Success && long.TryParse(inline.Groups[1].Value, out var inlineRid))
                {
                    rid = inlineRid;
                    return inlineRid == NullRid ? FieldState.PresentUnset : FieldState.PresentSet;
                }

                // Block form: the rid scalar is the field's first indented child.
                for (var j = i + 1; j < end; j++)
                {
                    if (lines[j].Trim().Length == 0) continue;
                    if (IndentOf(lines[j]) <= fieldIndent) break; // dedented out of the field without a rid

                    var child = Regex.Match(lines[j].Trim(), @"^rid:\s*(-?\d+)$");
                    if (child.Success && long.TryParse(child.Groups[1].Value, out var childRid))
                    {
                        rid = childRid;
                        return childRid == NullRid ? FieldState.PresentUnset : FieldState.PresentSet;
                    }

                    break; // first child is not a rid scalar — not a recognised managed-reference pointer
                }

                return FieldState.PresentUnset; // field present but no rid — treat as unset
            }

            return FieldState.Absent; // field absent — not a violation (needs reserialize)
        }

        // Classifies the top-level string field `name`: PresentUnset when empty or an empty quoted scalar ('' / ""),
        // PresentSet when it holds a value, Absent when the key is not in the doc.
        private static FieldState IsStringFieldUnset(string[] lines, int start, int end, int fieldIndent, string name)
        {
            var pattern = new Regex($@"^(?<indent>\s*){Regex.Escape(name)}:\s*(?<value>.*)$");

            for (var i = start; i < end; i++)
            {
                var field = pattern.Match(lines[i]);
                if (!field.Success || field.Groups["indent"].Length != fieldIndent) continue;

                var value = field.Groups["value"].Value.Trim();
                return value.Length == 0 || value == "''" || value == "\"\""
                    ? FieldState.PresentUnset
                    : FieldState.PresentSet;
            }

            return FieldState.Absent; // field absent — not a violation (needs reserialize)
        }

        // Classifies a SerializableType field `name`: PresentUnset when its nested _assemblyQualifiedName scalar is empty
        // (or the block carries no such child), PresentSet when it holds a value, Absent when the field key is not in the
        // doc. Mirrors IsStringFieldUnset but reads the wrapper's one indented child rather than an inline scalar.
        private static FieldState IsSerializableTypeFieldUnset(string[] lines, int start, int end, int fieldIndent, string name)
        {
            var pattern = new Regex($@"^(?<indent>\s*){Regex.Escape(name)}:\s*$");

            for (var i = start; i < end; i++)
            {
                var field = pattern.Match(lines[i]);
                if (!field.Success || field.Groups["indent"].Length != fieldIndent) continue;

                // The wrapper's _assemblyQualifiedName scalar is the field's first indented child.
                for (var j = i + 1; j < end; j++)
                {
                    if (lines[j].Trim().Length == 0) continue;
                    if (IndentOf(lines[j]) <= fieldIndent) break; // dedented out of the field without the child

                    var child = Regex.Match(lines[j].Trim(), @"^_assemblyQualifiedName:\s*(?<value>.*)$");
                    if (!child.Success) break; // first child is not the backing scalar — unrecognised shape

                    var value = child.Groups["value"].Value.Trim();
                    return value.Length == 0 || value == "''" || value == "\"\""
                        ? FieldState.PresentUnset
                        : FieldState.PresentSet;
                }

                return FieldState.PresentUnset; // field present but no _assemblyQualifiedName child — treat as unset
            }

            return FieldState.Absent; // field absent — not a violation (needs reserialize)
        }

        // Whether a required field key was found and, if so, whether it carries a value. An absent key is distinguished
        // from a present-but-empty one so the gate never flags a field Unity simply hasn't written yet (object saved
        // before the field was added, or a stripped / nested-prefab doc) — that needs a reserialize, not a build failure.
        private enum FieldState
        {
            Absent,
            PresentSet,
            PresentUnset
        }
    }
}
