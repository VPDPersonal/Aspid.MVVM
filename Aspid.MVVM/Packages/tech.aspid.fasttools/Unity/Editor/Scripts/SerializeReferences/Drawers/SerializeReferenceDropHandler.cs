using System;
using UnityEditor;
using Aspid.FastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Shared logic for assigning a managed reference by dropping a <see cref="MonoScript"/> onto a
    /// <c>[SerializeReference]</c> field. Resolves the dropped script's class, checks it is an assignable
    /// managed-reference type, and writes an instance (per-target under a multi-object selection so the drop never
    /// aliases). Used by both the UIToolkit field and the IMGUI drawer.
    /// </summary>
    internal static class SerializeReferenceDropHandler
    {
        /// <summary>
        /// Resolves the type of the first dragged <see cref="MonoScript"/> when it is assignable to the field, honoring
        /// the optional <c>[TypeSelector]</c> base-type narrowing. Returns false (and a null type) otherwise.
        /// </summary>
        public static bool TryResolveDroppedType(Type fieldType, Type[] baseTypes, out Type type)
        {
            type = null;

            foreach (var dragged in DragAndDrop.objectReferences)
            {
                if (dragged is not MonoScript script) continue;

                var candidate = script.GetClass();
                if (candidate is null) continue;
                if (!SerializeReferenceHelpers.IsAssignableManagedReference(candidate)) continue;
                if (fieldType != null && !fieldType.IsAssignableFrom(candidate)) continue;
                if (!SerializeReferenceHelpers.BuildAssignableFilter(baseTypes)(candidate)) continue;

                type = candidate;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Assigns a fresh instance of <paramref name="type"/> to the field (per-target on a multi-selection).
        /// </summary>
        public static void Assign(SerializedProperty property, Type type)
        {
            if (property is null || type is null) return;

            var persistent = property.Persistent();
            var previous = persistent.managedReferenceValue;

            if (SerializeReferenceHelpers.IsEditingMultipleObjects(persistent))
            {
                SerializeReferenceHelpers.ApplyManagedReferencePerTarget(persistent,
                    target => SerializeReferenceHelpers.CreateInstancePreservingData(type, target));
            }
            else persistent.SetManagedReferenceAndApply(SerializeReferenceHelpers.CreateInstancePreservingData(type, previous));
        }
    }
}
