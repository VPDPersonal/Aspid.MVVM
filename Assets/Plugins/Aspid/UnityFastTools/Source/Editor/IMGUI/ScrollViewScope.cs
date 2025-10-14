using UnityEditor;
using UnityEngine;

// ReSharper disable CheckNamespace
namespace Aspid.UnityFastTools.Editors
{
    public readonly ref struct ScrollViewScope
    {
        public readonly Vector2 ScrollPosition;

        private ScrollViewScope(Vector2 scrollPosition)
        {
            ScrollPosition = scrollPosition;
        }
        
        public static ScrollViewScope Begin(
            Vector2 scrollPosition,
            params GUILayoutOption[] options)
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, options);
            return new ScrollViewScope(scrollPosition);
        }

        public static ScrollViewScope Begin(
            Vector2 scrollPosition,
            bool alwaysShowHorizontal,
            bool alwaysShowVertical,
            params GUILayoutOption[] options)
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, options);
            return new ScrollViewScope(scrollPosition);
        }

        public static ScrollViewScope Begin(
            Vector2 scrollPosition,
            GUIStyle horizontalScrollbar,
            GUIStyle verticalScrollbar,
            params GUILayoutOption[] options)
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, horizontalScrollbar, verticalScrollbar, options);
            return new ScrollViewScope(scrollPosition);
        }

        public static ScrollViewScope Begin(
            Vector2 scrollPosition,
            GUIStyle style,
            params GUILayoutOption[] options)
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, style, options);
            return new ScrollViewScope(scrollPosition);
        }

        public static ScrollViewScope Begin(
            Vector2 scrollPosition,
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

        public static ScrollViewScope Begin(
            ref Vector2 scrollPosition,
            params GUILayoutOption[] options)
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, options);
            return new ScrollViewScope(scrollPosition);
        }

        public static ScrollViewScope Begin(
            ref Vector2 scrollPosition,
            bool alwaysShowHorizontal,
            bool alwaysShowVertical,
            params GUILayoutOption[] options)
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, options);
            return new ScrollViewScope(scrollPosition);
        }

        public static ScrollViewScope Begin(
            ref Vector2 scrollPosition,
            GUIStyle horizontalScrollbar,
            GUIStyle verticalScrollbar,
            params GUILayoutOption[] options)
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, horizontalScrollbar, verticalScrollbar, options);
            return new ScrollViewScope(scrollPosition);
        }

        public static ScrollViewScope Begin(
            ref Vector2 scrollPosition,
            GUIStyle style,
            params GUILayoutOption[] options)
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, style, options);
            return new ScrollViewScope(scrollPosition);
        }

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

        public void Dispose() => EditorGUILayout.EndScrollView();
    }
}