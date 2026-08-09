using System;
using UnityEditor;
using System.Collections.Generic;
using Aspid.FastTools.Editors;
using Aspid.FastTools.Types.Editors;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    // The inline type pickers and what a pick does. Three flavours, one per edit route: a missing entry is repaired
    // through the YAML, a healthy or empty slot through the live serialization API, and a required string field
    // through its backing string property. Each reads its candidate set and current value from the same source the
    // matching apply writes to, so the picker can never offer a type the apply would reject. The edits themselves
    // belong to SerializeReferenceGraphEditor — everything here only decides what to open and when to re-render.
    internal sealed partial class SerializeReferenceGraphView
    {
        private const string PickerClass = RootClass + "__picker";
        private const string PickerAttachedClass = PickerClass + "--attached";
        private const string NodePickingClass = NodeClass + "--picking";

        private static readonly AuditPickerHost.PickerClasses _pickerClassSet =
            new(PickerClass, PickerAttachedClass, NodePickingClass);

        // Missing card: constrained to the rid's declared field type so a repair cannot pick an incompatible type that
        // would null on import; an unresolvable field type falls back to unconstrained.
        private void OpenMissingPicker(string assetPath, long fileId, long rid, AspidGradientButton anchor) =>
            TogglePicker(anchor, ManagedReferenceFilter.For(_constraints.Resolve(assetPath, fileId, rid)),
                currentAqn: null, // a missing entry has no current value — nothing (not even <None>) wears the check
                assemblyQualifiedName => ApplyFix(assetPath, fileId, rid, assemblyQualifiedName));

        // Healthy / empty card: constraint and current type are read from the live property at the field path. A field
        // the API cannot reach opens an unconstrained picker and surfaces the failure on apply.
        private void OpenLivePicker(string assetPath, long fileId, string graphPath, AspidGradientButton anchor)
        {
            var constraint = typeof(object);
            var currentAqn = string.Empty;

            if (SerializeReferenceGraphEditor.TryResolveLiveProperty(assetPath, fileId, graphPath, out var serializedObject, out var property))
            {
                using (serializedObject)
                {
                    constraint = SerializeReferenceHelpers.GetFieldType(property);
                    currentAqn = property.managedReferenceValue?.GetType().AssemblyQualifiedName ?? string.Empty;
                }
            }

            TogglePicker(anchor, ManagedReferenceFilter.For(constraint), currentAqn,
                assemblyQualifiedName => ApplyLive(assetPath, fileId, graphPath, assemblyQualifiedName));
        }

        // Required string / SerializableType card: constraint and current value are read from the live string
        // property; a field the API cannot reach opens an unconstrained picker and surfaces the failure on apply
        // (mirrors OpenLivePicker).
        private void OpenRequiredStringPicker(GateViolation violation, AspidGradientButton anchor)
        {
            var filter = default(TypeSelectorFilter);
            var currentAqn = string.Empty;

            if (SerializeReferenceGraphEditor.TryResolveRequiredStringProperty(violation, out var serializedObject, out var property))
            {
                using (serializedObject)
                {
                    currentAqn = property.stringValue ?? string.Empty;
                    filter = BuildRequiredStringFilter(serializedObject, property);
                }
            }

            TogglePicker(anchor, filter, currentAqn,
                assemblyQualifiedName => ApplyRequiredString(violation, assemblyQualifiedName));
        }

        // The same candidate set the field's own [TypeSelector] dropdown offers: the attribute's constraints resolved
        // member-first against the owning object (TypeSelectorConstraintResolver), the wrapper's T for a
        // SerializableType<T> field, and the attribute's kind filter. Resolution warnings are the Inspector notice's
        // concern — here an unresolvable constraint just widens the picker.
        private static TypeSelectorFilter BuildRequiredStringFilter(SerializedObject serializedObject, SerializedProperty property)
        {
            if (!SerializeReferenceRequiredGate.TryGetRequired(property, out var selector)) return default;

            var types = new List<Type>();

            // The backing string of a SerializableType<T> wrapper carries the wrapper's generic constraint.
            var path = property.propertyPath;
            var lastDotIndex = path.LastIndexOf('.');
            if (lastDotIndex >= 0)
            {
                using var parentProperty = serializedObject.FindProperty(path[..lastDotIndex]);
                var parentField = parentProperty?.GetFieldInfo();
                if (parentField is not null &&
                    SerializableTypeUtility.TryGetBaseType(parentField.FieldType, out var wrapperBase) &&
                    wrapperBase is not null && wrapperBase != typeof(object))
                    types.Add(wrapperBase);
            }

            types.AddRange(TypeSelectorConstraintResolver.Resolve(
                serializedObject.targetObject, selector.AssemblyQualifiedNames).Types);

            return new TypeSelectorFilter
            {
                Types = types.Count > 0 ? types.ToArray() : null,
                Allow = selector.Allow,
            };
        }

        // The picker expands inline under the clicked card's band, one panel at a time. Generic over the source of
        // truth: the caller supplies the candidate filter, the type to pre-navigate to, and what a pick does.
        private void TogglePicker(AspidGradientButton anchor, TypeSelectorFilter filter, string currentAqn, Action<string> onSelected)
        {
            if (_picker.ToggleClosed(anchor)) return;

            _picker.Open(anchor, new TypeSelectorView(
                filter: filter,
                currentAqn: currentAqn, // null (no current-value concept) and "" (holds <None>) both pass through as-is
                onSelected: onSelected,
                onDismiss: _picker.Close));
        }

        // ---------------------------------------------------------------------------------------------------------
        // Applying a pick — the edit is the editor's; only the re-render is this view's
        // ---------------------------------------------------------------------------------------------------------

        private void ApplyFix(string assetPath, long fileId, long rid, string assemblyQualifiedName)
        {
            if (SerializeReferenceGraphEditor.ApplyFix(assetPath, fileId, rid, assemblyQualifiedName)) Rescan();
        }

        private void ApplyLive(string assetPath, long fileId, string graphPath, string assemblyQualifiedName)
        {
            if (SerializeReferenceGraphEditor.ApplyLive(assetPath, fileId, graphPath, assemblyQualifiedName)) Rescan();
        }

        private void ApplyRequiredString(GateViolation violation, string assemblyQualifiedName)
        {
            if (SerializeReferenceGraphEditor.ApplyRequiredString(violation, assemblyQualifiedName)) Rescan();
        }

        private void ClearOrphan(string assetPath, long fileId, long rid)
        {
            if (SerializeReferenceGraphEditor.TryClearOrphan(assetPath, fileId, rid, out var staleRescan))
            {
                Rescan();
                return;
            }

            // The on-screen graph was stale (the rid is no longer an orphan); re-render from the scan the editor
            // already built instead of reading the unchanged file a second time.
            if (staleRescan is not null) Rescan(staleRescan);
        }
    }
}
