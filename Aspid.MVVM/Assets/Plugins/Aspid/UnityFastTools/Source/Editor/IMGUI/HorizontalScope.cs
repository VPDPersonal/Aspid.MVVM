using UnityEditor;
using UnityEngine;

// ReSharper disable CheckNamespace
namespace Aspid.UnityFastTools.Editors
{
    public readonly ref struct HorizontalScope
    {
        public readonly Rect Rect;
            
        private HorizontalScope(Rect rect)
        {
            Rect = rect;
        }
            
        public static HorizontalScope Begin(params GUILayoutOption[] options) =>
            new(EditorGUILayout.BeginHorizontal(options));

        public static HorizontalScope Begin(GUIStyle style, params GUILayoutOption[] options) =>
            new(EditorGUILayout.BeginHorizontal(style, options));

        public static HorizontalScope Begin(out Rect rect, params GUILayoutOption[] options)
        {
            rect = EditorGUILayout.BeginHorizontal(options);
            return new HorizontalScope(rect);
        }
            
        public static HorizontalScope Begin(out Rect rect, GUIStyle style, params GUILayoutOption[] options)
        {
            rect = EditorGUILayout.BeginHorizontal(style, options);
            return new HorizontalScope(rect);
        }

        public void Dispose() => 
            EditorGUILayout.EndHorizontal();
    }
}