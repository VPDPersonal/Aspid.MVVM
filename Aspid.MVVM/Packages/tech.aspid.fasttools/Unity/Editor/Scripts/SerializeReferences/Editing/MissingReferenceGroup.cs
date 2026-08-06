using System;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>One broken managed-reference entry plus the asset it lives in.</summary>
    internal readonly struct MissingReferenceLocation
    {
        public readonly string AssetPath;
        public readonly MissingReferenceEntry Entry;

        public MissingReferenceLocation(string assetPath, MissingReferenceEntry entry)
        {
            AssetPath = assetPath;
            Entry = entry;
        }
    }

    /// <summary>
    /// Every broken reference sharing one stored type across the project — the unit the Project References audit lists
    /// and bulk-fixes. Resolves a single picker constraint by intersecting the entries' declared field types, falling
    /// back to <see cref="object"/> when they disagree.
    /// </summary>
    internal sealed class MissingReferenceGroup
    {
        public readonly ManagedTypeName StoredType;
        public readonly List<MissingReferenceLocation> Entries = new();

        private readonly HashSet<string> _files = new(StringComparer.Ordinal);
        private readonly SerializeReferenceConstraintCache _constraints = new();

        public MissingReferenceGroup(ManagedTypeName storedType)
        {
            StoredType = storedType;
        }

        public int FileCount => _files.Count;

        public string DisplayName => StoredType.DisplayName;

        /// <summary>
        /// Groups every unresolved managed reference in the project by stored type, backed by the shared usage index,
        /// biggest group first. Cheap once the index is warm — it is an in-memory filter, not a sweep.
        /// </summary>
        public static List<MissingReferenceGroup> CollectFromIndex()
        {
            var byType = new Dictionary<string, MissingReferenceGroup>(StringComparer.Ordinal);

            foreach (var usage in SerializeReferenceTypeUsageIndex.EnumerateUnresolved())
            {
                var path = AssetDatabase.GUIDToAssetPath(usage.Guid);
                if (string.IsNullOrEmpty(path)) continue;

                var key = SerializeReferenceHelpers.StoredTypeKey(usage.StoredType);
                if (!byType.TryGetValue(key, out var group))
                {
                    group = new MissingReferenceGroup(usage.StoredType);
                    byType.Add(key, group);
                }

                group.Add(path, new MissingReferenceEntry(usage.FileId, usage.Rid, usage.StoredType));
            }

            var groups = byType.Values.ToList();
            groups.Sort((a, b) => b.Entries.Count.CompareTo(a.Entries.Count));
            return groups;
        }

        public void Add(string assetPath, MissingReferenceEntry entry)
        {
            Entries.Add(new MissingReferenceLocation(assetPath, entry));
            _files.Add(assetPath);
        }

        /// <summary>
        /// The ranked Smart Fix for this group's broken type, or <see langword="false"/> when nothing clears the
        /// confidence threshold.
        /// </summary>
        /// <remarks>
        /// Ranked against the constraint-filtered pool, so the suggestion is always assignable — which is what lets a
        /// quick-apply bypass the picker. The field names come from the first entry: every entry in a group stores the
        /// same broken type, so any of them ranks the same candidates.
        /// </remarks>
        public bool TryGetSuggestion(Type constraint, out SerializeReferenceRepairSuggestions.RepairCandidate suggestion)
        {
            suggestion = default;

            var first = Entries[0];
            var fieldNames = SerializeReferenceYamlEditor.GetReferenceFieldNames(first.AssetPath, first.Entry.FileId, first.Entry.Rid);

            var ranked = SerializeReferenceRepairSuggestions.Rank(StoredType, fieldNames, constraint);
            if (ranked.Count == 0) return false;

            suggestion = ranked[0];
            return true;
        }

        /// <summary>The type every entry's field can hold, or <see cref="object"/> when that cannot be narrowed.</summary>
        /// <remarks>Per-file constraint maps are built once and cached, so the intersection costs one scan per distinct asset.</remarks>
        public Type ResolveConstraint() => ResolveConstraint(out _);

        /// <inheritdoc cref="ResolveConstraint()"/>
        /// <param name="mixedFieldTypes">
        /// Whether the <see cref="object"/> fallback came from the field types disagreeing (vs. one being
        /// unrecoverable) — the bulk-fix confirmation warns on that case.
        /// </param>
        public Type ResolveConstraint(out bool mixedFieldTypes)
        {
            mixedFieldTypes = false;
            Type common = null;

            foreach (var entry in Entries)
            {
                // A field type we cannot recover (a reference nested in a missing parent, or an orphaned rid no
                // field points at) leaves the group unconstrained — a tighter guess could hide a valid pick.
                var fieldType = _constraints.Resolve(entry.AssetPath, entry.Entry.FileId, entry.Entry.Rid);
                if (fieldType is null) return typeof(object);

                if (common is null)
                {
                    common = fieldType;
                }
                else if (common != fieldType)
                {
                    mixedFieldTypes = true;
                    return typeof(object);
                }
            }

            return common ?? typeof(object);
        }
    }

    /// <summary>
    /// A group's picker constraint and whether it reads as a one-click <c>[MovedFrom]</c> migration, resolved once so
    /// the audit's partition, card body and picker label share one computation and can never disagree.
    /// </summary>
    /// <remarks>
    /// A migration is an authoritative <c>[MovedFrom]</c> rename whose target also fits the group's field constraint —
    /// <c>Migrate all</c> bypasses the picker's assignability guarantee, and an incompatible target would be nulled by
    /// Unity at load, so the constraint gate matters.
    /// </remarks>
    internal readonly struct MissingReferenceMigration
    {
        public readonly Type Constraint;
        public readonly bool IsMigration;

        /// <summary>The <c>[MovedFrom]</c> target when <see cref="IsMigration"/>; otherwise <see langword="null"/>.</summary>
        public readonly Type Target;

        public MissingReferenceMigration(MissingReferenceGroup group)
        {
            Constraint = group.ResolveConstraint();
            IsMigration = SerializeReferenceMovedFromResolver.TryResolve(group.StoredType, out var target) &&
                (Constraint == typeof(object) || Constraint.IsAssignableFrom(target));
            Target = IsMigration ? target : null;
        }
    }
}
