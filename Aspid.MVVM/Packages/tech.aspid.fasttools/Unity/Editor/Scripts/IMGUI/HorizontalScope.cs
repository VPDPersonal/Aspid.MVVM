using UnityEditor;
using UnityEngine;

// ReSharper disable CheckNamespace
namespace Aspid.FastTools.Editors
{
    /// <summary>
    /// Disposable ref struct wrapper around <see cref="EditorGUILayout.BeginHorizontal"/> /
    /// <see cref="EditorGUILayout.EndHorizontal"/> that exposes the resulting <see cref="Rect"/>.
    /// Use in a <c>using</c> statement to automatically close the horizontal group.
    /// </summary>
    public readonly ref struct HorizontalScope
    {
        /// <summary>
        /// The <see cref="Rect"/> returned by <see cref="EditorGUILayout.BeginHorizontal"/> for this group.
        /// </summary>
        public readonly Rect Rect;

        private HorizontalScope(Rect rect)
        {
            Rect = rect;
        }

        /// <summary>
        /// Begins a horizontal layout group with the given layout options.
        /// </summary>
        /// <param name="options">Optional layout options passed to <see cref="EditorGUILayout.BeginHorizontal"/>.</param>
        /// <returns>A new <see cref="HorizontalScope"/> whose <see cref="Rect"/> reflects the group bounds.</returns>
        public static HorizontalScope Begin(params GUILayoutOption[] options) =>
            new(EditorGUILayout.BeginHorizontal(options));

        /// <summary>
        /// Begins a horizontal layout group with a specific <see cref="GUIStyle"/> and layout options.
        /// </summary>
        /// <param name="style">The style to apply to the horizontal group.</param>
        /// <param name="options">Optional layout options passed to <see cref="EditorGUILayout.BeginHorizontal"/>.</param>
        /// <returns>A new <see cref="HorizontalScope"/> whose <see cref="Rect"/> reflects the group bounds.</returns>
        public static HorizontalScope Begin(GUIStyle style, params GUILayoutOption[] options) =>
            new(EditorGUILayout.BeginHorizontal(style, options));

        /// <summary>
        /// Begins a horizontal layout group and outputs the resulting rect via an <c>out</c> parameter.
        /// </summary>
        /// <param name="rect">Receives the <see cref="Rect"/> of the horizontal group.</param>
        /// <param name="options">Optional layout options passed to <see cref="EditorGUILayout.BeginHorizontal"/>.</param>
        /// <returns>A new <see cref="HorizontalScope"/> whose <see cref="Rect"/> reflects the group bounds.</returns>
        public static HorizontalScope Begin(out Rect rect, params GUILayoutOption[] options)
        {
            rect = EditorGUILayout.BeginHorizontal(options);
            return new HorizontalScope(rect);
        }

        /// <summary>
        /// Begins a horizontal layout group with a specific <see cref="GUIStyle"/> and outputs the resulting rect via an <c>out</c> parameter.
        /// </summary>
        /// <param name="rect">Receives the <see cref="Rect"/> of the horizontal group.</param>
        /// <param name="style">The style to apply to the horizontal group.</param>
        /// <param name="options">Optional layout options passed to <see cref="EditorGUILayout.BeginHorizontal"/>.</param>
        /// <returns>A new <see cref="HorizontalScope"/> whose <see cref="Rect"/> reflects the group bounds.</returns>
        public static HorizontalScope Begin(out Rect rect, GUIStyle style, params GUILayoutOption[] options)
        {
            rect = EditorGUILayout.BeginHorizontal(style, options);
            return new HorizontalScope(rect);
        }

        /// <summary>
        /// Ends the horizontal layout group by calling <see cref="EditorGUILayout.EndHorizontal"/>.
        /// </summary>
        public void Dispose() =>
            EditorGUILayout.EndHorizontal();
    }
}
