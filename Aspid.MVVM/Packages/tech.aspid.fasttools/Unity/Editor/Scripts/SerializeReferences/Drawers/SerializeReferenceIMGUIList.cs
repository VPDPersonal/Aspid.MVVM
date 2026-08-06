using System;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.SerializeReferences.Editors
{
    /// <summary>
    /// IMGUI parity for the UIToolkit ListView's picker-backed "+": draws a <c>[SerializeReference]</c> list/array whose
    /// add button opens the type picker and appends a fresh typed instance (or an empty <c>&lt;None&gt;</c> element),
    /// mirroring <see cref="SerializeReferenceListAddBehavior"/>.
    /// </summary>
    /// <remarks>
    /// Unity applies a <c>[TypeSelector]</c> <see cref="PropertyDrawer"/> to array <b>elements</b> in the IMGUI path, so
    /// the drawer can never reach the list's own "+" button — the UIToolkit side only manages it by walking up to the
    /// live <c>ListView</c>, which immediate-mode IMGUI has no equivalent of. A custom <see cref="Editor"/> that forces
    /// IMGUI (overrides <c>OnInspectorGUI</c> without <c>CreateInspectorGUI</c>) therefore gets Unity's default add on its
    /// <c>[SerializeReference]</c> lists — duplicating the last element and leaving it rid-aliased. Call
    /// <see cref="Draw"/> for those lists instead to restore the picker-backed, de-aliased add. Elements are drawn with
    /// <see cref="EditorGUI.PropertyField(Rect, SerializedProperty, GUIContent, bool)"/>, so each still routes through the
    /// <c>[TypeSelector]</c> drawer exactly as the default list drawing would.
    /// </remarks>
    public static class SerializeReferenceIMGUIList
    {
        // ReorderableList caches per-list UI state (selection, drag), so it must persist across OnInspectorGUI calls —
        // keyed by (target instance id + property path), which is stable for a given field across repaints.
        private static readonly Dictionary<string, ReorderableList> Lists = new();

        // Minimum picker width, matching TypeSelectorWindow.Show's own floor, so the right-aligned anchor below reflects
        // the picker's true footprint.
        private const float PickerWidth = 350f;

        // Unity's default array UI flags its list with the internal m_HasPropertyDrawer, insetting element rects by
        // Defaults.propertyDrawerPadding (8) past the drag handle. That flag is unreachable from package code, so the
        // same inset is applied manually in drawElementCallback to keep the geometry the property drawer is tuned for.
        private const float PropertyDrawerPadding = 8f;

        /// <summary>
        /// The right edge of the list box whose element is currently being drawn through <see cref="Draw"/> —
        /// <see cref="float.NaN"/> outside any element. The drawer's group-navigation pulse stops its band there (the
        /// box border) instead of stretching to the inspector's right edge as it does for a root-level field. Stacked,
        /// so a list nested inside another list's element restores the outer box's edge when it finishes.
        /// </summary>
        internal static float CurrentElementRightLimit =>
            _elementRightLimits.Count > 0 ? _elementRightLimits.Peek() : float.NaN;

        private static readonly Stack<float> _elementRightLimits = new();

        /// <summary>
        /// Draws <paramref name="listProperty"/> (a <c>[SerializeReference]</c> list/array) with a picker-backed "+".
        /// </summary>
        /// <param name="listProperty">The array/list property to draw. Its elements must be managed references.</param>
        /// <param name="label">Header label for the list.</param>
        /// <param name="elementType">The declared element type constraining the picker (e.g. the list's <c>T</c>). Needed
        /// up front because an empty list has no element to read it from.</param>
        /// <param name="baseTypes">Optional base types that narrow the candidate list below <paramref name="elementType"/>,
        /// mirroring the <c>[TypeSelector(...)]</c> arguments.</param>
        public static void Draw(SerializedProperty listProperty, GUIContent label, Type elementType, params Type[] baseTypes)
        {
            if (listProperty is null || !listProperty.isArray) return;

            var list = GetOrCreate(listProperty, label, elementType, baseTypes);

            // The SerializedProperty instance is rebuilt every OnInspectorGUI (a fresh iterator/FindProperty); re-point
            // the cached list at the current one so its callbacks never touch a disposed property.
            list.serializedProperty = listProperty;
            list.DoLayoutList();
        }

        private static ReorderableList GetOrCreate(SerializedProperty listProperty, GUIContent label, Type elementType, Type[] baseTypes)
        {
            var serializedObject = listProperty.serializedObject;

            // The SerializedObject's identity is part of the key: an Inspector plus a locked Inspector hold two
            // DISTINCT SerializedObjects for one (target, path) — under a shared key the staleness check below would
            // rebuild the list on every alternating repaint, resetting its drag / selection state.
            var key = $"{RuntimeHelpers.GetHashCode(serializedObject)}/" +
                      $"{serializedObject.targetObject.GetInstanceID()}/{listProperty.propertyPath}";

            // A cached list bound to a stale SerializedObject (e.g. after a domain reload) must be rebuilt, not reused.
            if (Lists.TryGetValue(key, out var cached) && cached.serializedProperty.serializedObject == serializedObject)
                return cached;

            // Entries pin their SerializedObject (the ReorderableList holds its property), so a closed editor's entry
            // would otherwise live until the next domain reload. Swept on cache misses only — already the slow path.
            EvictDeadEntries();

            // Capture the append target/path once: both are stable for the field's lifetime, and Append opens its own
            // fresh SerializedObject anyway (so no stale-binding hazard from the captured object).
            var target = serializedObject.targetObject;
            var arrayPath = listProperty.propertyPath;

            // Declared and assigned before the callbacks so their lambdas can close over `list` (an object-initializer
            // self-reference under `var` fails to compile — type inference cycle — so build it, then wire callbacks).
            var list = new ReorderableList(serializedObject, listProperty,
                draggable: true, displayHeader: true, displayAddButton: true, displayRemoveButton: true);

            list.drawHeaderCallback = rect => EditorGUI.LabelField(rect, label);

            // The element background rect spans the box's full inner width (the row content rect below is inset past
            // the drag handle and paddings), so it carries the box border the pulse band should stop at. Only drawn
            // on Repaint — exactly when the pulse itself paints — so the captured edge is always fresh.
            var boxRightEdge = 0f;
            list.drawElementBackgroundCallback = (rect, index, active, focused) =>
            {
                boxRightEdge = rect.xMax;
                ReorderableList.defaultBehaviours.DrawElementBackground(rect, index, active, focused, draggable: true);
            };

            list.elementHeightCallback = index =>
            {
                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                return EditorGUI.GetPropertyHeight(element, includeChildren: true) +
                       EditorGUIUtility.standardVerticalSpacing * 2f;
            };

            list.drawElementCallback = (rect, index, _, _) =>
            {
                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                rect.xMin += PropertyDrawerPadding;
                rect.y += EditorGUIUtility.standardVerticalSpacing;
                rect.height = EditorGUI.GetPropertyHeight(element, includeChildren: true);

                // Drawn through the standard field, so the element still routes through the [TypeSelector] drawer;
                // the pushed limit tells that drawer where this row's box ends (see CurrentElementRightLimit).
                _elementRightLimits.Push(boxRightEdge);
                try
                {
                    EditorGUI.PropertyField(rect, element, new GUIContent($"Element {index}"), includeChildren: true);
                }
                finally
                {
                    _elementRightLimits.Pop();
                }
            };

            // Replace Unity's default add (which duplicates the last element, leaving it rid-aliased) with the picker,
            // matching the UIToolkit ListView override. onAddDropdownCallback hands us the "+" button rect to anchor.
            list.onAddDropdownCallback = (buttonRect, _) =>
            {
                // Anchor the picker's RIGHT edge to the button and open flush below it, so a "+" near the inspector's
                // right edge grows the dropdown leftward into the window instead of spilling off to the right.
                var topLeft = GUIUtility.GUIToScreenPoint(new Vector2(buttonRect.xMax - PickerWidth, buttonRect.yMin));
                var screenRect = new Rect(topLeft.x, topLeft.y, PickerWidth, buttonRect.height);
                SerializeReferenceListAddBehavior.ShowAppendPicker(target, arrayPath, elementType, baseTypes, screenRect);
            };

            Lists[key] = list;
            return list;
        }

        private static void EvictDeadEntries()
        {
            List<string> dead = null;

            foreach (var pair in Lists)
            {
                bool alive;
                try
                {
                    alive = pair.Value.serializedProperty.serializedObject.targetObject != null;
                }
                catch (Exception)
                {
                    // A disposed SerializedObject throws on access — the entry is dead either way.
                    alive = false;
                }

                if (!alive) (dead ??= new List<string>()).Add(pair.Key);
            }

            if (dead is null) return;
            foreach (var key in dead) Lists.Remove(key);
        }
    }
}
