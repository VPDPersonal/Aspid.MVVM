using UnityEditor;
using UnityEngine;

// ReSharper disable CheckNamespace
namespace Aspid.FastTools.Editors
{
    /// <summary>
    /// Disposable ref struct wrapper around <see cref="EditorGUILayout.BeginVertical"/> /
    /// <see cref="EditorGUILayout.EndVertical"/> that exposes the resulting <see cref="Rect"/>.
    /// Use in a <c>using</c> statement to automatically close the vertical group.
    /// </summary>
    public readonly ref struct VerticalScope
    {
        /// <summary>
        /// The <see cref="Rect"/> returned by <see cref="EditorGUILayout.BeginVertical"/> for this group.
        /// </summary>
        public readonly Rect Rect;

        private VerticalScope(Rect rect)
        {
            Rect = rect;
        }

        /// <summary>
        /// Begins a vertical layout group with the given layout options.
        /// </summary>
        /// <param name="options">Optional layout options passed to <see cref="EditorGUILayout.BeginVertical"/>.</param>
        /// <returns>A new <see cref="VerticalScope"/> whose <see cref="Rect"/> reflects the group bounds.</returns>
        public static VerticalScope Begin(params GUILayoutOption[] options) =>
            new(EditorGUILayout.BeginVertical(options));

        /// <summary>
        /// Begins a vertical layout group with a specific <see cref="GUIStyle"/> and layout options.
        /// </summary>
        /// <param name="style">The style to apply to the vertical group.</param>
        /// <param name="options">Optional layout options passed to <see cref="EditorGUILayout.BeginVertical"/>.</param>
        /// <returns>A new <see cref="VerticalScope"/> whose <see cref="Rect"/> reflects the group bounds.</returns>
        public static VerticalScope Begin(GUIStyle style, params GUILayoutOption[] options) =>
            new(EditorGUILayout.BeginVertical(style, options));

        /// <summary>
        /// Begins a vertical layout group and outputs the resulting rect via an <c>out</c> parameter.
        /// </summary>
        /// <param name="rect">Receives the <see cref="Rect"/> of the vertical group.</param>
        /// <param name="options">Optional layout options passed to <see cref="EditorGUILayout.BeginVertical"/>.</param>
        /// <returns>A new <see cref="VerticalScope"/> whose <see cref="Rect"/> reflects the group bounds.</returns>
        public static VerticalScope Begin(out Rect rect, params GUILayoutOption[] options)
        {
            rect = EditorGUILayout.BeginVertical(options);
            return new VerticalScope(rect);
        }

        /// <summary>
        /// Begins a vertical layout group with a specific <see cref="GUIStyle"/> and outputs the resulting rect via an <c>out</c> parameter.
        /// </summary>
        /// <param name="rect">Receives the <see cref="Rect"/> of the vertical group.</param>
        /// <param name="style">The style to apply to the vertical group.</param>
        /// <param name="options">Optional layout options passed to <see cref="EditorGUILayout.BeginVertical"/>.</param>
        /// <returns>A new <see cref="VerticalScope"/> whose <see cref="Rect"/> reflects the group bounds.</returns>
        public static VerticalScope Begin(out Rect rect, GUIStyle style, params GUILayoutOption[] options)
        {
            rect = EditorGUILayout.BeginVertical(style, options);
            return new VerticalScope(rect);
        }

        /// <summary>
        /// Ends the vertical layout group by calling <see cref="EditorGUILayout.EndVertical"/>.
        /// </summary>
        public void Dispose() =>
            EditorGUILayout.EndVertical();
    }
}
