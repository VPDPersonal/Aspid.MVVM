using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;
using Aspid.FastTools.Types.Editors;
using Aspid.FastTools.UIElements.Editors;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// Replaces the default "+" on a <c>List&lt;[SerializeReference]&gt;</c> / array (which duplicates the last element
    /// and leaves it rid-aliased) with one that opens the type picker and appends a fresh typed instance — killing the
    /// alias at the source. <see cref="SerializeReferenceDuplicateGuard"/> remains the fallback for the native add path
    /// (Ctrl+D, paste, multi-object selections).
    /// </summary>
    internal static class SerializeReferenceListAddBehavior
    {
        /// <summary>
        /// Installs the picker-backed add behavior on the ListView hosting <paramref name="elementField"/>, once. No-op
        /// for non-list elements, multi-object selections (handled by the de-alias guard), or when an override is
        /// already present.
        /// </summary>
        /// <remarks>
        /// The base types come through a provider consulted when the picker opens — a member-referenced
        /// <c>[TypeSelector]</c> constraint can re-resolve long after the "+" override is installed.
        /// </remarks>
        public static void TryInstall(VisualElement elementField, SerializedProperty elementProperty, Type elementType, Func<Type[]> baseTypesProvider)
        {
            if (elementField is null || elementProperty is null) return;

            var serializedObject = elementProperty.serializedObject;
            if (serializedObject is null || serializedObject.isEditingMultipleObjects) return;

            var path = elementProperty.propertyPath;
            var arrayMarker = path.IndexOf(".Array.data[", StringComparison.Ordinal);
            if (arrayMarker < 0) return; // not a list/array element

            var arrayPath = path[..arrayMarker];
            var target = serializedObject.targetObject;
            if (target == null) return;

            var listView = elementField.GetFirstAncestorOfType<ListView>();
            if (listView is null || listView.overridingAddButtonBehavior != null) return;

            // Assigning overridingAddButtonBehavior calls RefreshItems() — a hierarchy change — but TryInstall runs
            // from AttachToPanelEvent, mid-attach, where mutating the subtree synchronously throws. Defer one tick
            // (anchored to the long-lived ListView) and re-check the guard: sibling elements queue their own installs.
            listView.schedule.Execute(() =>
            {
                if (listView.overridingAddButtonBehavior != null) return;

                listView.overridingAddButtonBehavior = (_, button) =>
                    OpenAppendPicker(target, arrayPath, elementType, baseTypesProvider(), button);
            });
        }

        // Shared with SerializeReferenceListField, whose "+" needs the same picker anchored the same way.
        public static void OpenAppendPicker(Object target, string arrayPath, Type elementType, Type[] baseTypes, VisualElement anchor)
        {
            var window = anchor.GetOwnerWindow();
            if (window == null) return;

            // Anchor the picker to the ListView (spanning its width) rather than the small "+" button, so it opens as
            // a wide dropdown flush below the add row; fall back to the button when the ListView is unreachable.
            var reference = anchor.GetFirstAncestorOfType<ListView>() ?? anchor;

            // Match TypeSelectorWindow.Show's minimum width so the clamp below reflects the picker's real footprint.
            var width = Mathf.Max(350f, reference.worldBound.width);

            // x: clamp so the picker's right edge never crosses the inspector window's right edge — a picker anchored at
            // the narrow button's own left would spill off to the right of the window.
            var x = Mathf.Max(
                window.position.x,
                Mathf.Min(window.position.x + reference.worldBound.xMin, window.position.xMax - width));

            // y: anchor from the "+" button's TOP + its height, so ShowAsDropDown opens flush below its bottom edge —
            // anchoring from yMax double-counted the button height and dropped the picker a full row lower.
            var screenRect = new Rect(
                x,
                window.position.y + anchor.worldBound.yMin,
                width,
                anchor.worldBound.height);

            ShowAppendPicker(target, arrayPath, elementType, baseTypes, screenRect);
        }

        /// <summary>
        /// Opens the type picker anchored to <paramref name="screenRect"/> and appends the chosen type (or an empty
        /// <c>&lt;None&gt;</c> element) to the array at <paramref name="arrayPath"/>. Shared by the UIToolkit ListView add
        /// override and the IMGUI list drawer (<see cref="SerializeReferenceIMGUIList"/>), whose only difference is how
        /// each computes the anchor rect — so both add paths offer the same picker and the same de-aliased append.
        /// </summary>
        public static void ShowAppendPicker(Object target, string arrayPath, Type elementType, Type[] baseTypes, Rect screenRect)
        {
            TypeSelectorWindow.Show(
                screenRect: screenRect,
                filter: new TypeSelectorFilter
                {
                    Types = new[] { elementType },
                    Predicate = SerializeReferenceHelpers.BuildAssignableFilter(baseTypes),
                    AdditionalTypes = GenericTypeResolver.GetAssignableGenericDefinitions(elementType, baseTypes),
                    ArgumentFilter = SerializeReferenceHelpers.IsValidGenericArgument,
                },
                currentAqn: null, // a "+" append has no current value — nothing (not even <None>) wears the check
                onSelected: aqn => Append(target, arrayPath, aqn));
        }

        private static void Append(Object target, string arrayPath, string assemblyQualifiedName)
        {
            if (target == null) return;

            // A <None> pick is a valid choice: the "+" always grows the list, appending an empty (null) element the user
            // can type later — so type is left null here rather than aborting the add.
            var type = string.IsNullOrEmpty(assemblyQualifiedName) ? null : Type.GetType(assemblyQualifiedName, throwOnError: false);

            // A fresh SerializedObject avoids any stale-binding hazard from a captured one; the bound inspector ListView
            // refreshes from the changed target on its next update.
            using var serializedObject = new SerializedObject(target);
            var array = serializedObject.FindProperty(arrayPath);
            if (array is null || !array.isArray) return;

            // arraySize++ copies the previous last element's managedReferenceId, so overwrite it in the same
            // modification — a fresh instance for a picked type, an explicit null for <None> (a bare arraySize++ there
            // would alias the previous element) — collapsing both into one Undo step.
            var index = array.arraySize;
            array.arraySize = index + 1;
            array.GetArrayElementAtIndex(index).SetManagedReference(type is null ? null : SerializeReferenceHelpers.CreateInstance(type));
            serializedObject.ApplyModifiedProperties();
        }
    }
}
