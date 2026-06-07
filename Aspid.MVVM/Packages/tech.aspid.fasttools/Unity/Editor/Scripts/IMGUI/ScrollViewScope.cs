using UnityEditor;
using UnityEngine;

// ReSharper disable CheckNamespace
namespace Aspid.FastTools.Editors
{
    /// <summary>
    /// Disposable ref struct wrapper around <see cref="EditorGUILayout.BeginScrollView"/> /
    /// <see cref="EditorGUILayout.EndScrollView"/> that updates the caller's scroll position in place
    /// and exposes it via <see cref="ScrollPosition"/>. Use in a <c>using</c> statement to automatically
    /// close the scroll view.
    /// </summary>
    public readonly ref struct ScrollViewScope
    {
        /// <summary>
        /// The updated scroll position returned by <see cref="EditorGUILayout.BeginScrollView"/>.
        /// </summary>
        public readonly Vector2 ScrollPosition;

        private ScrollViewScope(Vector2 scrollPosition)
        {
            ScrollPosition = scrollPosition;
        }

        /// <summary>
        /// Begins a scroll view, updating the caller's scroll position variable in place.
        /// </summary>
        /// <param name="scrollPosition">Reference to the current scroll position; updated to the new value.</param>
        /// <param name="options">Optional layout options.</param>
        /// <returns>A new <see cref="ScrollViewScope"/> with the updated <see cref="ScrollPosition"/>.</returns>
        public static ScrollViewScope Begin(
            ref Vector2 scrollPosition,
            params GUILayoutOption[] options)
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, options);
            return new ScrollViewScope(scrollPosition);
        }

        /// <summary>
        /// Begins a scroll view with explicit scrollbar visibility flags, updating the caller's variable in place.
        /// </summary>
        /// <param name="scrollPosition">Reference to the current scroll position; updated to the new value.</param>
        /// <param name="alwaysShowHorizontal">Always show the horizontal scrollbar.</param>
        /// <param name="alwaysShowVertical">Always show the vertical scrollbar.</param>
        /// <param name="options">Optional layout options.</param>
        /// <returns>A new <see cref="ScrollViewScope"/> with the updated <see cref="ScrollPosition"/>.</returns>
        public static ScrollViewScope Begin(
            ref Vector2 scrollPosition,
            bool alwaysShowHorizontal,
            bool alwaysShowVertical,
            params GUILayoutOption[] options)
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, options);
            return new ScrollViewScope(scrollPosition);
        }

        /// <summary>
        /// Begins a scroll view with custom scrollbar styles, updating the caller's variable in place.
        /// </summary>
        /// <param name="scrollPosition">Reference to the current scroll position; updated to the new value.</param>
        /// <param name="horizontalScrollbar">Style for the horizontal scrollbar.</param>
        /// <param name="verticalScrollbar">Style for the vertical scrollbar.</param>
        /// <param name="options">Optional layout options.</param>
        /// <returns>A new <see cref="ScrollViewScope"/> with the updated <see cref="ScrollPosition"/>.</returns>
        public static ScrollViewScope Begin(
            ref Vector2 scrollPosition,
            GUIStyle horizontalScrollbar,
            GUIStyle verticalScrollbar,
            params GUILayoutOption[] options)
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, horizontalScrollbar, verticalScrollbar, options);
            return new ScrollViewScope(scrollPosition);
        }

        /// <summary>
        /// Begins a scroll view with a single style, updating the caller's variable in place.
        /// </summary>
        /// <param name="scrollPosition">Reference to the current scroll position; updated to the new value.</param>
        /// <param name="style">Style applied to the scroll view.</param>
        /// <param name="options">Optional layout options.</param>
        /// <returns>A new <see cref="ScrollViewScope"/> with the updated <see cref="ScrollPosition"/>.</returns>
        public static ScrollViewScope Begin(
            ref Vector2 scrollPosition,
            GUIStyle style,
            params GUILayoutOption[] options)
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, style, options);
            return new ScrollViewScope(scrollPosition);
        }

        /// <summary>
        /// Begins a scroll view with full control over scrollbar visibility, styles, and background,
        /// updating the caller's variable in place.
        /// </summary>
        /// <param name="scrollPosition">Reference to the current scroll position; updated to the new value.</param>
        /// <param name="alwaysShowHorizontal">Always show the horizontal scrollbar.</param>
        /// <param name="alwaysShowVertical">Always show the vertical scrollbar.</param>
        /// <param name="horizontalScrollbar">Style for the horizontal scrollbar.</param>
        /// <param name="verticalScrollbar">Style for the vertical scrollbar.</param>
        /// <param name="background">Background style for the scroll view.</param>
        /// <param name="options">Optional layout options.</param>
        /// <returns>A new <see cref="ScrollViewScope"/> with the updated <see cref="ScrollPosition"/>.</returns>
        public static ScrollViewScope Begin(
            ref Vector2 scrollPosition,
            bool alwaysShowHorizontal,
            bool alwaysShowVertical,
            GUIStyle horizontalScrollbar,
            GUIStyle verticalScrollbar,
            GUIStyle background,
            params GUILayoutOption[] options)
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background, options);
            return new ScrollViewScope(scrollPosition);
        }

        /// <summary>
        /// Ends the scroll view by calling <see cref="EditorGUILayout.EndScrollView"/>.
        /// </summary>
        public void Dispose() => EditorGUILayout.EndScrollView();
    }
}
