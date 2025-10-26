using UnityEditor;
using UnityEngine;

// ReSharper disable CheckNamespace
namespace Aspid.UnityFastTools.Editors
{
    public readonly ref struct VerticalScope
    {
        public readonly Rect Rect;
            
        private VerticalScope(Rect rect)
        {
            Rect = rect;
        }
            
        public static VerticalScope Begin(params GUILayoutOption[] options) =>
            new(EditorGUILayout.BeginVertical(options));

        public static VerticalScope Begin(GUIStyle style, params GUILayoutOption[] options) =>
            new(EditorGUILayout.BeginVertical(style, options));

        public static VerticalScope Begin(out Rect rect, params GUILayoutOption[] options)
        {
            rect = EditorGUILayout.BeginVertical(options);
            return new VerticalScope(rect);
        }
            
        public static VerticalScope Begin(out Rect rect, GUIStyle style, params GUILayoutOption[] options)
        {
            rect = EditorGUILayout.BeginVertical(style, options);
            return new VerticalScope(rect);
        }

        public void Dispose() => EditorGUILayout.EndVertical();
    }
}