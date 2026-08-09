using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Ranking engine behind the missing-type <b>Smart Fix</b> suggestion: given the stored (now unloadable) type
    /// identity of a <c>[SerializeReference]</c>, the field names recorded for it and the field's declared base
    /// constraint, it computes ordered repair candidates and surfaces the best one as a one-click suggestion. The
    /// candidate pool is the same set the type picker would offer (concrete managed-reference types assignable to the
    /// constraint), so a surfaced suggestion can never be a type the picker itself would refuse. The suggestion is
    /// never auto-applied — the user always clicks (an explicit product decision).
    /// </summary>
    internal static class SerializeReferenceRepairSuggestions
    {
        /// <summary>
        /// A scored repair candidate for a missing managed reference: the existing <see cref="Type"/> the reference
        /// could be re-pointed to, the heuristic <see cref="Score"/> (highest wins) and a short human <see cref="Reason"/>.
        /// </summary>
        public readonly struct RepairCandidate
        {
            public readonly Type Type;
            public readonly float Score;
            public readonly string Reason;

            public RepairCandidate(Type type, float score, string reason)
            {
                Type = type;
                Score = score;
                Reason = reason;
            }
        }

        // Only suggestions at or above this confidence are surfaced — below it the heuristics are too weak to offer.
        public const float MinScore = 0.6f;

        // The field-shape overlap can add up to this much, lifting a marginal name match over the threshold and
        // breaking ties between equally-named candidates.
        private const float FieldShapeBonus = 0.2f;

        /// <summary>
        /// Returns up to <paramref name="max"/> repair candidates for a missing managed reference, ordered by descending
        /// score (ties broken by field-shape overlap). Only candidates scoring at least <see cref="MinScore"/> are
        /// returned. The pool is every concrete managed-reference type assignable to <paramref name="baseConstraint"/>
        /// (the field's declared element type), filtered by the same eligibility rules the picker uses, so a returned
        /// type always satisfies the field's constraint.
        /// </summary>
        /// <param name="stored">The stored, now-unresolvable type identity read from the asset YAML.</param>
        /// <param name="storedFieldNames">Top-level serialized field names recorded for the missing reference, or empty.</param>
        /// <param name="baseConstraint">The field's declared element type; <c>typeof(object)</c> for an unconstrained field.</param>
        /// <param name="max">Maximum number of candidates to return.</param>
        public static IReadOnlyList<RepairCandidate> Rank(
            ManagedTypeName stored,
            IReadOnlyCollection<string> storedFieldNames,
            Type baseConstraint,
            int max = 3)
        {
            if (stored.IsEmpty || max <= 0) return Array.Empty<RepairCandidate>();

            var constraint = baseConstraint ?? typeof(object);
            var storedClass = SerializeReferenceMovedFromResolver.NormalizeClassName(stored.Class);
            if (string.IsNullOrEmpty(storedClass)) return Array.Empty<RepairCandidate>();

            var hasFieldNames = storedFieldNames is { Count: > 0 };
            var storedFields = hasFieldNames
                ? new HashSet<string>(storedFieldNames, StringComparer.Ordinal)
                : null;

            var scored = new List<RepairCandidate>();

            foreach (var candidate in EnumerateCandidates(constraint))
            {
                var baseScore = ScoreCandidate(stored, storedClass, candidate, out var reason);
                if (baseScore <= 0f) continue;

                var bonus = hasFieldNames ? FieldShapeOverlap(storedFields, candidate) * FieldShapeBonus : 0f;
                var score = baseScore + bonus;
                if (score < MinScore) continue;

                scored.Add(new RepairCandidate(candidate, score, reason));
            }

            if (scored.Count == 0) return Array.Empty<RepairCandidate>();

            // Ties broken ordinally by full name, then assembly — otherwise the surfaced fix would depend on
            // TypeCache order and could flip across domain reloads.
            scored.Sort(static (a, b) =>
            {
                var byScore = b.Score.CompareTo(a.Score);
                if (byScore != 0) return byScore;

                var byName = string.CompareOrdinal(a.Type.FullName, b.Type.FullName);
                if (byName != 0) return byName;

                return string.CompareOrdinal(a.Type.Assembly.GetName().Name, b.Type.Assembly.GetName().Name);
            });
            return scored.Count <= max ? scored : scored.GetRange(0, max);
        }

        // Same eligibility rules the type picker enforces, so a suggestion can never be a type the field would refuse.
        private static IEnumerable<Type> EnumerateCandidates(Type constraint)
        {
            var pool = constraint == typeof(object)
                ? TypeCache.GetTypesDerivedFrom<object>()
                : TypeCache.GetTypesDerivedFrom(constraint);

            foreach (var type in pool)
            {
                if (!SerializeReferenceHelpers.IsAssignableManagedReference(type)) continue;
                if (constraint != typeof(object) && !constraint.IsAssignableFrom(type)) continue;
                yield return type;
            }
        }

        // Base score for a candidate against the stored identity, before the field-shape bonus. Returns 0 for no match.
        private static float ScoreCandidate(ManagedTypeName stored, string storedClass, Type candidate, out string reason)
        {
            // A declared [MovedFrom] whose recorded old identity matches the stored one is an authoritative rename — top score.
            if (SerializeReferenceMovedFromResolver.MatchesOldIdentity(candidate, stored, storedClass))
            {
                reason = "declared [MovedFrom]";
                return 1f;
            }

            var candidateClass = SerializeReferenceMovedFromResolver.NormalizeClassName(candidate.Name);

            // Same simple class name, different namespace and/or assembly — the class was moved/renamed-by-namespace.
            if (string.Equals(candidateClass, storedClass, StringComparison.Ordinal))
            {
                reason = "same type name";
                return 0.8f;
            }

            if (string.Equals(candidateClass, storedClass, StringComparison.OrdinalIgnoreCase))
            {
                reason = "same name (case-insensitive)";
                return 0.6f;
            }

            // A near-miss name — only surfaced once the field-shape bonus lifts it over the threshold.
            if (LevenshteinAtMost(candidateClass, storedClass, 2))
            {
                reason = "similar name";
                return 0.5f;
            }

            reason = null;
            return 0f;
        }

        // Fraction (0..1) of stored field names that exist on the candidate's serialized fields.
        private static float FieldShapeOverlap(HashSet<string> storedFields, Type candidate)
        {
            var candidateFields = GetSerializedFieldNames(candidate);
            if (candidateFields.Count == 0 || storedFields.Count == 0) return 0f;

            var matched = storedFields.Count(candidateFields.Contains);
            return (float)matched / storedFields.Count;
        }

        // Mirrors Unity's serialization rule: public instance fields plus private [SerializeField] ones, base chain included.
        private static HashSet<string> GetSerializedFieldNames(Type type)
        {
            var names = new HashSet<string>(StringComparer.Ordinal);

            for (var current = type; current is not null && current != typeof(object); current = current.BaseType)
            {
                const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
                foreach (var field in current.GetFields(flags))
                {
                    if (field.IsStatic || field.IsLiteral || field.IsInitOnly) continue;
                    if (field.IsNotSerialized) continue;

                    var serialized = field.IsPublic || field.IsDefined(typeof(SerializeField), inherit: false);
                    if (serialized) names.Add(field.Name);
                }
            }

            return names;
        }

        // Bounded Levenshtein with early bail-out once a row's best distance exceeds the bound.
        private static bool LevenshteinAtMost(string a, string b, int maxDistance)
        {
            if (a is null || b is null) return false;
            if (Math.Abs(a.Length - b.Length) > maxDistance) return false;
            if (a.Length == 0) return b.Length <= maxDistance;
            if (b.Length == 0) return a.Length <= maxDistance;

            var previous = new int[b.Length + 1];
            var current = new int[b.Length + 1];
            for (var j = 0; j <= b.Length; j++) previous[j] = j;

            for (var i = 1; i <= a.Length; i++)
            {
                current[0] = i;
                var rowBest = current[0];

                for (var j = 1; j <= b.Length; j++)
                {
                    var cost = a[i - 1] == b[j - 1] ? 0 : 1;
                    current[j] = Math.Min(Math.Min(previous[j] + 1, current[j - 1] + 1), previous[j - 1] + cost);
                    if (current[j] < rowBest) rowBest = current[j];
                }

                if (rowBest > maxDistance) return false;
                (previous, current) = (current, previous);
            }

            return previous[b.Length] <= maxDistance;
        }

        #region Cached ranking
        // IMGUI repaints every frame, so the TypeCache-scanning ranking is cached per (asset, document, rid) with a
        // FIFO cap. The document file id is part of the key because a rid is only unique within one host object
        // (Prefab Mode components of the same prefab share an asset path). Cleared whenever a repair lands.
        private const int CacheCapacity = 64;

        private static readonly Dictionary<(string assetPath, long fileId, long rid), IReadOnlyList<RepairCandidate>> Cache = new();
        private static readonly Queue<(string assetPath, long fileId, long rid)> CacheOrder = new();

        /// <summary>
        /// Cached <see cref="Rank"/> keyed by <paramref name="assetPath"/>, the host document's <paramref name="fileId"/>
        /// and the reference's <paramref name="rid"/>, so a per-frame IMGUI repaint never re-scans the type cache. The
        /// factory runs only on a cache miss.
        /// </summary>
        public static IReadOnlyList<RepairCandidate> GetCached(
            string assetPath,
            long fileId,
            long rid,
            Func<IReadOnlyList<RepairCandidate>> rank)
        {
            var key = (assetPath ?? string.Empty, fileId, rid);
            if (Cache.TryGetValue(key, out var cached)) return cached;

            var result = rank() ?? Array.Empty<RepairCandidate>();
            Cache[key] = result;
            CacheOrder.Enqueue(key);

            while (CacheOrder.Count > CacheCapacity)
            {
                var evicted = CacheOrder.Dequeue();
                Cache.Remove(evicted);
            }

            return result;
        }

        /// <summary>
        /// Drops every cached ranking — called after a repair, since the candidate set has changed.
        /// </summary>
        public static void ClearCache()
        {
            Cache.Clear();
            CacheOrder.Clear();
        }
        #endregion
    }
}
