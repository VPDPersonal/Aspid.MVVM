using System;
using UnityEditor;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// The inverse of Make Unique: deliberately shares one managed-reference instance across several fields of the same
    /// object. Because there is no rid setter, sharing is achieved by assigning the SAME instance to both paths —
    /// Unity then keeps them on a single <see cref="SerializedProperty.managedReferenceId"/> (exactly the aliasing the
    /// shared-reference notice detects), so the rid stripe and notice light up automatically.
    /// </summary>
    internal static class SerializeReferenceLinker
    {
        /// <summary>
        /// A sibling managed reference this field could be linked to.
        /// </summary>
        public readonly struct LinkCandidate
        {
            public readonly long Rid;
            public readonly Type Type;
            public readonly string Path;

            public LinkCandidate(long rid, Type type, string path)
            {
                Rid = rid;
                Type = type;
                Path = path;
            }
        }

        /// <summary>
        /// Every other managed reference in the same object that is assignable to this field and is neither this property
        /// nor one of its ancestors/descendants (which would form a self-cycle).
        /// </summary>
        public static List<LinkCandidate> CollectLinkCandidates(SerializedProperty property)
        {
            var result = new List<LinkCandidate>();
            if (property is null) return result;

            var fieldType = SerializeReferenceHelpers.GetFieldType(property);
            var selfPath = property.propertyPath;
            var seen = new HashSet<long>();

            // The ancestor exclusion must work by IDENTITY, not path: an aliased sibling IS an ancestor instance
            // under another name, and linking a child field to it would tie the object's graph into a cycle the
            // path-prefix checks below cannot see (the alias's path shares no prefix with this property's).
            var ancestorRids = CollectAncestorRids(property);

            using var iterator = property.serializedObject.GetIterator();
            if (!iterator.Next(enterChildren: true)) return result;

            // Cycle-safe walk: never re-enter an instance already seen on this walk (a cyclic graph would loop the
            // iterator forever — see SerializeReferenceHelpers.BuildConstraintMap for the same guard).
            var visitedChildren = new HashSet<long>();
            bool enterChildren;

            do
            {
                enterChildren = true;
                if (iterator.propertyType != SerializedPropertyType.ManagedReference) continue;

                var rid = iterator.managedReferenceId;
                if (rid >= 0 && !visitedChildren.Add(rid)) enterChildren = false;

                var path = iterator.propertyPath;
                if (path == selfPath) continue;
                if (IsDescendant(path, selfPath) || IsDescendant(selfPath, path)) continue;
                if (ancestorRids.Contains(rid)) continue;

                var value = iterator.managedReferenceValue;
                if (value is null) continue;

                var type = value.GetType();
                if (fieldType != null && !fieldType.IsAssignableFrom(type)) continue;

                if (!seen.Add(rid)) continue; // one representative per shared instance

                result.Add(new LinkCandidate(rid, type, path));
            }
            while (iterator.Next(enterChildren));

            return result;
        }

        // The rids held by every managed-reference ancestor of the property (walking its path prefixes).
        private static HashSet<long> CollectAncestorRids(SerializedProperty property)
        {
            var rids = new HashSet<long>();
            var serializedObject = property.serializedObject;
            var path = property.propertyPath;

            for (var dot = path.LastIndexOf('.'); dot > 0; dot = path.LastIndexOf('.'))
            {
                path = path[..dot];

                using var ancestor = serializedObject.FindProperty(path);
                if (ancestor is { propertyType: SerializedPropertyType.ManagedReference })
                {
                    var rid = ancestor.managedReferenceId;
                    if (rid >= 0) rids.Add(rid);
                }
            }

            return rids;
        }

        /// <summary>
        /// Points this field at the instance held by <paramref name="sourcePath"/>, sharing its rid.
        /// </summary>
        public static bool LinkTo(SerializedProperty property, string sourcePath)
        {
            if (property is null || string.IsNullOrEmpty(sourcePath)) return false;

            // Read AND write through the SAME SerializedObject: Unity only keeps two fields on a single
            // managedReferenceId when the same instance is assigned within one SerializedObject — a value pulled through
            // a separate one (e.g. property.Persistent()) deserialises a fresh copy that gets a NEW rid on apply.
            var serializedObject = property.serializedObject;
            var value = serializedObject.FindProperty(sourcePath)?.managedReferenceValue;
            if (value is null) return false;

            property.managedReferenceValue = value;
            serializedObject.ApplyModifiedProperties();
            return true;
        }

        private static bool IsDescendant(string candidate, string ancestor) =>
            candidate.StartsWith(ancestor + ".", StringComparison.Ordinal);
    }
}
