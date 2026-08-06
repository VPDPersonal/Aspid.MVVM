using System;
using System.Linq;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Reads a scanned reference graph without drawing it: what is broken, what is merely a pending
    /// <c>[MovedFrom]</c> rename, which slots sit empty and what a broken node's best repair guess is.
    /// </summary>
    /// <remarks>
    /// The migration checks need the declared field type behind a rid, so they take the caller's
    /// <see cref="SerializeReferenceConstraintCache"/> rather than building a constraint map per call — one asset scan
    /// is shared by every question asked about that asset in one render pass.
    /// </remarks>
    internal static class SerializeReferenceGraphAnalysis
    {
        /// <summary>Joins a parent field path with a child edge label, tolerating either side being empty.</summary>
        public static string CombinePath(string parent, string child)
        {
            if (string.IsNullOrEmpty(child)) return parent;
            return string.IsNullOrEmpty(parent) ? child : $"{parent}.{child}";
        }

        /// <summary>
        /// Every empty managed-reference slot's normalized field path across every document, root and nested edge.
        /// </summary>
        /// <remarks>
        /// Mirrors the card-building walk minus the cards. It is the lookup set an empty slot's required badge is
        /// checked against, and the way the graph tells "already badged on a card" apart from "no card exists for this
        /// field at all" — a required string / <c>SerializableType</c> field has no rid and so no node.
        /// </remarks>
        public static HashSet<(long fileId, string path)> CollectEmptySlotPaths(List<ReferenceGraphDocument> documents)
        {
            var paths = new HashSet<(long, string)>();

            foreach (var document in documents)
            {
                foreach (var root in document.Roots)
                {
                    if (root.IsEmpty)
                        paths.Add((document.FileId, SerializeReferenceGraphEditor.ToSerializedPropertyPath(root.Label)));
                    else
                        WalkForEmptySlots(document, root.Rid, root.Label, new HashSet<long>(), paths);
                }
            }

            return paths;
        }

        /// <summary>How many slots in this document are unassigned. Used only for the overview hint; empty slots are not "issues".</summary>
        public static int CountEmptySlots(ReferenceGraphDocument document)
        {
            var count = document.Roots.Count(root => root.IsEmpty);

            foreach (var pair in document.Edges)
            {
                count += pair.Value.Count(edge => edge.IsEmpty);
            }

            return count;
        }

        /// <summary>
        /// Splits a document's unresolved nodes into genuinely broken ones and pending <c>[MovedFrom]</c> migrations.
        /// </summary>
        /// <remarks>
        /// An orphaned rid always counts as broken — nothing loads an orphan, so in-memory migration does not apply.
        /// It is also excluded from the migration tally because the orphan counters already own it; counting it here
        /// too would double it in the overview headline and hints.
        /// </remarks>
        public static (int broken, int migrations) CountUnresolved(string assetPath, ReferenceGraphDocument document,
            SerializeReferenceConstraintCache constraints)
        {
            var broken = 0;
            var migrations = 0;

            foreach (var node in document.Nodes)
            {
                if (node.Resolves || node.StoredType.IsEmpty) continue;
                if (document.Orphans.Contains(node.Rid)) continue;

                if (IsPendingMigration(assetPath, document.FileId, node.Rid, node.StoredType, constraints, out _))
                    migrations++;
                else
                    broken++;
            }

            return (broken, migrations);
        }

        /// <summary>The missing-predicate the amber tint uses; also drives the missing-first root ordering.</summary>
        public static bool RootIsMissing(ReferenceGraphDocument document, long rid)
        {
            var node = document.FindNode(rid);
            return node is { Resolves: false, StoredType: { IsEmpty: false } };
        }

        /// <summary>
        /// Whether a missing node's stored type is claimed by exactly one <c>[MovedFrom]</c> target that fits the
        /// field's declared type — Unity already migrates it in memory, so only the file is stale.
        /// </summary>
        /// <remarks>An unrecoverable constraint lets the migration through.</remarks>
        public static bool IsPendingMigration(string assetPath, long fileId, long rid, ManagedTypeName storedType,
            SerializeReferenceConstraintCache constraints, out Type target)
        {
            if (!SerializeReferenceMovedFromResolver.TryResolve(storedType, out target)) return false;

            var constraint = constraints.Resolve(assetPath, fileId, rid);
            return constraint is null || constraint == typeof(object) || constraint.IsAssignableFrom(target);
        }

        /// <summary>
        /// The ranked Smart Fix for a missing node, via the shared per-<c>(path, fileId, rid)</c> cache so a rescan and
        /// the inline drawer reuse one computation.
        /// </summary>
        /// <remarks>Best-effort: a parse miss just means no suggestion row.</remarks>
        public static bool TryGetSuggestion(string assetPath, long fileId, long rid, ManagedTypeName storedType,
            SerializeReferenceConstraintCache constraints, out SerializeReferenceRepairSuggestions.RepairCandidate suggestion)
        {
            suggestion = default;

            try
            {
                var fieldNames = SerializeReferenceYamlEditor.GetReferenceFieldNames(assetPath, fileId, rid);
                var constraint = constraints.Resolve(assetPath, fileId, rid) ?? typeof(object);

                var ranked = SerializeReferenceRepairSuggestions.GetCached(assetPath, fileId, rid,
                    () => SerializeReferenceRepairSuggestions.Rank(storedType, fieldNames, constraint));
                if (ranked.Count == 0) return false;

                suggestion = ranked[0];
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static void WalkForEmptySlots(ReferenceGraphDocument document, long rid, string pathLabel,
            HashSet<long> visited, HashSet<(long fileId, string path)> paths)
        {
            if (!visited.Add(rid)) return;

            foreach (var edge in document.ChildrenOf(rid))
            {
                var childPath = CombinePath(pathLabel, edge.Label);
                if (edge.IsEmpty)
                    paths.Add((document.FileId, SerializeReferenceGraphEditor.ToSerializedPropertyPath(childPath)));
                else
                    WalkForEmptySlots(document, edge.Rid, childPath, visited, paths);
            }

            visited.Remove(rid);
        }
    }
}
