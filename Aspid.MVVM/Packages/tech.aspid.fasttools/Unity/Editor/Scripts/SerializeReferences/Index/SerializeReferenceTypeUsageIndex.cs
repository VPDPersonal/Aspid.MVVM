using System;
using System.Linq;
using UnityEditor;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Lazy, incrementally-updated project-wide index mapping each managed-reference stored-type identity to the assets,
    /// documents and rids that use it. Modeled on the Id system's <c>UniqueIdIndex</c>: a cold null sentinel rebuilt on
    /// first lookup, patched per-asset on import and fully reset on delete/move (see
    /// <see cref="SerializeReferenceTypeUsageIndexInvalidator"/>). Per-asset extraction reuses
    /// <see cref="SerializeReferenceGraphScanner.Build"/>, which already reports every rid's stored type and whether it
    /// resolves, at any nesting depth.
    /// </summary>
    /// <remarks>
    /// Powers Find Usages, MonoScript delete protection and the Repair window's fast project scan. Usages carry their
    /// per-site <see cref="Usage.Resolves"/> flag and <see cref="Usage.StoredType"/> so consumers never re-read the file.
    /// </remarks>
    internal static class SerializeReferenceTypeUsageIndex
    {
        /// <summary>
        /// A single managed-reference use site. Identity is (asset, document, rid); the rest is payload.
        /// </summary>
        public readonly struct Usage : IEquatable<Usage>
        {
            public readonly string Guid;
            public readonly long FileId;
            public readonly long Rid;
            public readonly bool Resolves;
            public readonly ManagedTypeName StoredType;

            public Usage(string guid, long fileId, long rid, bool resolves, ManagedTypeName storedType)
            {
                Guid = guid ?? string.Empty;
                FileId = fileId;
                Rid = rid;
                Resolves = resolves;
                StoredType = storedType;
            }

            public bool Equals(Usage other) =>
                string.Equals(Guid, other.Guid, StringComparison.Ordinal) && FileId == other.FileId && Rid == other.Rid;

            public override bool Equals(object obj) => obj is Usage other && Equals(other);

            public override int GetHashCode() => unchecked((Guid.GetHashCode() * 397 ^ FileId.GetHashCode()) * 397 ^ Rid.GetHashCode());
        }

        // null = cold sentinel; rebuilt lazily on first lookup, exactly like the Id indices.
        private static Dictionary<string, HashSet<Usage>> _index;

        /// <summary>
        /// Whether the index is already built (warm). Consumers on the import / domain-reload path (the breakage
        /// detector, the delete guard) must check this and NOT warm a cold index — warming runs a modal full-project
        /// YAML sweep, which must never be triggered by a routine import (risk register items 3 and 10).
        /// </summary>
        public static bool IsWarm => _index is not null;

        /// <summary>
        /// Every use site of the type identified by <paramref name="storedTypeKey"/> (warms the index if cold).
        /// </summary>
        public static IReadOnlyCollection<Usage> FindUsages(string storedTypeKey)
        {
            if (string.IsNullOrEmpty(storedTypeKey)) return Array.Empty<Usage>();
            EnsureBuilt();
            return _index.TryGetValue(storedTypeKey, out var set) ? set : Array.Empty<Usage>();
        }

        /// <summary>
        /// Every use site of <paramref name="type"/> by its open-generic stored-type identity. A generic type's script
        /// resolves to the open definition (<c>Modifier`1[[T]]</c>), while YAML stores each closed instantiation
        /// (<c>Modifier`1[[System.Single, …]]</c>) under its own key — so the lookup keys on the open identity to gather
        /// every closed form. A non-generic type has a single key and behaves exactly as a direct lookup.
        /// </summary>
        public static IReadOnlyCollection<Usage> FindUsages(Type type) =>
            type is null ? Array.Empty<Usage>() : FindUsagesByOpenKey(SerializeReferenceHelpers.OpenTypeKey(ManagedTypeName.FromType(type)));

        /// <summary>
        /// Every use site whose stored type shares the open-generic identity <paramref name="openTypeKey"/> (warms the
        /// index if cold). Aggregates across the closed-form keys a generic type splits into; for a non-generic identity
        /// the open key equals the stored key and at most one entry contributes.
        /// </summary>
        public static IReadOnlyCollection<Usage> FindUsagesByOpenKey(string openTypeKey)
        {
            if (string.IsNullOrEmpty(openTypeKey)) return Array.Empty<Usage>();
            EnsureBuilt();

            HashSet<Usage> result = null;
            foreach (var (key, set) in _index)
            {
                if (!string.Equals(SerializeReferenceHelpers.OpenTypeKey(key), openTypeKey, StringComparison.Ordinal))
                    continue;

                (result ??= new HashSet<Usage>()).UnionWith(set);
            }

            return (IReadOnlyCollection<Usage>)result ?? Array.Empty<Usage>();
        }

        /// <summary>
        /// Number of use sites of <paramref name="type"/>.
        /// </summary>
        public static int CountUsages(Type type) => FindUsages(type).Count;

        /// <summary>
        /// Every use site whose stored type no longer resolves — the fast-scan source for the Repair window.
        /// </summary>
        public static IEnumerable<Usage> EnumerateUnresolved()
        {
            EnsureBuilt();
            foreach (var set in _index.Values)
                foreach (var usage in set)
                    if (!usage.Resolves)
                        yield return usage;
        }

        /// <summary>
        /// Every indexed use site (warms the index) — the source the Find Usages search provider filters.
        /// </summary>
        public static IEnumerable<Usage> AllUsages()
        {
            EnsureBuilt();
            foreach (var set in _index.Values)
                foreach (var usage in set)
                    yield return usage;
        }

        /// <summary>
        /// Drops the whole index; the next lookup rebuilds it. Alias <see cref="ClearCache"/> for the rewrite sites.
        /// </summary>
        public static void Reset() => _index = null;

        /// <inheritdoc cref="Reset"/>
        public static void ClearCache() => Reset();

        /// <summary>
        /// Re-extracts one asset's usages in place (strip then re-add). No-op while the index is cold.
        /// </summary>
        public static void RebuildAsset(string path)
        {
            if (_index is null) return;

            var guid = AssetDatabase.AssetPathToGUID(path);
            if (string.IsNullOrEmpty(guid)) return;

            RemoveGuid(guid);
            AddAsset(path, guid);
        }

        private static void EnsureBuilt()
        {
            if (_index is not null) return;

            _index = new Dictionary<string, HashSet<Usage>>(StringComparer.Ordinal);

            var paths = AssetDatabase.GetAllAssetPaths()
                .Where(SerializeReferenceHelpers.IsScanCandidate)
                .ToArray();

            // Non-cancelable bar: cancelling could leave a partial index marked warm (the null sentinel is already replaced).
            try
            {
                for (var i = 0; i < paths.Length; i++)
                {
                    EditorUtility.DisplayProgressBar(
                        "Indexing Managed References",
                        $"{paths[i]}  ({i + 1}/{paths.Length})",
                        (float)i / Math.Max(1, paths.Length));

                    var guid = AssetDatabase.AssetPathToGUID(paths[i]);
                    if (string.IsNullOrEmpty(guid)) continue;

                    AddAsset(paths[i], guid);
                }
            }
            catch (Exception)
            {
                // A failed warm-up must not masquerade as warm: reset to cold so the next lookup rebuilds from scratch.
                _index = null;
                throw;
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }
        }

        private static void AddAsset(string path, string guid)
        {
            // Data-only scan: resolving display TypeNames would load every asset (LoadAllAssetsAtPath).
            foreach (var document in SerializeReferenceGraphScanner.Build(path, resolveTypeNames: false))
            {
                foreach (var node in document.Nodes)
                {
                    // No recorded type identity — cannot be looked up by type and is not a "missing type"; not indexed.
                    if (node.StoredType.IsEmpty) continue;

                    var key = SerializeReferenceHelpers.StoredTypeKey(node.StoredType);
                    AddUsage(key, new Usage(guid, document.FileId, node.Rid, node.Resolves, node.StoredType));
                }
            }
        }

        private static void AddUsage(string key, Usage usage)
        {
            if (!_index.TryGetValue(key, out var set))
            {
                set = new HashSet<Usage>();
                _index[key] = set;
            }

            // Remove a prior equal-identity entry first so a changed Resolves/StoredType payload replaces the stale one.
            set.Remove(usage);
            set.Add(usage);
        }

        private static void RemoveGuid(string guid)
        {
            List<string> emptied = null;
            foreach (var (key, set) in _index)
            {
                if (set.RemoveWhere(u => string.Equals(u.Guid, guid, StringComparison.Ordinal)) > 0 && set.Count == 0)
                    (emptied ??= new List<string>()).Add(key);
            }

            if (emptied is null) return;
            foreach (var key in emptied) _index.Remove(key);
        }
    }
}
