using System;
using UnityEditor;
using UnityEngine;
using Aspid.FastTools.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    /// <summary>
    /// Editor window that displays a hierarchical type selector dropdown, allowing the user to browse and select a <see cref="System.Type"/> from a filtered list.
    /// </summary>
    /// <remarks>
    /// A thin dropdown host around <see cref="TypeSelectorView"/>, which owns the search, navigation and
    /// generic-argument flow. Embedding hosts (e.g. the Repair References window) use the view directly.
    /// </remarks>
    public sealed class TypeSelectorWindow : EditorWindow
    {
        /// <summary>
        /// Opens the type selector window as a dropdown anchored to <paramref name="screenRect"/>.
        /// </summary>
        /// <param name="screenRect">The screen-space rectangle the dropdown is anchored to.</param>
        /// <param name="filter">Defines which types the selector offers: base types, kind constraints, the per-type predicate, extra entries and the open-generic argument predicate. See <see cref="TypeSelectorFilter"/>.</param>
        /// <param name="currentAqn">Assembly-qualified name of the currently selected type, used to pre-navigate to that type's location. Pass <c>null</c> or empty to start at the root.</param>
        /// <param name="onSelected">Callback invoked with the assembly-qualified name of the selected type, or <c>null</c> if the user chose <c>&lt;None&gt;</c>. When an open generic is resolved, the assembly-qualified name of the constructed closed type is passed.</param>
        public static void Show(
            Rect screenRect,
            TypeSelectorFilter filter = default,
            string currentAqn = "",
            Action<string> onSelected = null)
        {
            var window = CreateInstance<TypeSelectorWindow>();
            var view = new TypeSelectorView(filter, currentAqn, onSelected, onDismiss: window.Close);

            window.rootVisualElement.AddChild(view);

            // 400 keeps the footer's full keyboard hint visible alongside the settings gear in the common states;
            // longer variants (e.g. the favorite toggle hint) still ellipsize rather than push the gear out.
            var size = new Vector2(Mathf.Max(400, screenRect.width), 320);
            window.ShowAsDropDown(screenRect, size);

            view.FocusPicker();
        }
    }
}
