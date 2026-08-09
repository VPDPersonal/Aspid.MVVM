using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors
{
    public static partial class VisualElementExtensions
    {
        /// <summary>
        /// Returns the <see cref="EditorWindow"/> whose panel hosts <paramref name="element"/>, falling back to
        /// <see cref="EditorWindow.focusedWindow"/> / <see cref="EditorWindow.mouseOverWindow"/> when no window's
        /// panel matches (e.g. the element is detached).
        /// </summary>
        /// <remarks>
        /// Use this instead of <see cref="EditorWindow.focusedWindow"/> when anchoring a dropdown to an element:
        /// a click into an unfocused floating window dispatches its pointer event before focus moves, so
        /// <c>focusedWindow</c> still points at the previously focused window and a rect built from its
        /// <see cref="EditorWindow.position"/> lands in the wrong window's coordinate space.
        /// </remarks>
        public static EditorWindow GetOwnerWindow(this VisualElement element)
        {
            var panel = element?.panel;

            if (panel is not null)
            {
                foreach (var window in Resources.FindObjectsOfTypeAll<EditorWindow>())
                {
                    if (window && window.rootVisualElement?.panel == panel)
                        return window;
                }
            }

            return EditorWindow.focusedWindow != null
                ? EditorWindow.focusedWindow
                : EditorWindow.mouseOverWindow;
        }
    }
}
