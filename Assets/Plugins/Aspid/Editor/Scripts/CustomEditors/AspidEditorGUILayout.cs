using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.CustomEditors
{
    public static class AspidEditorGUILayout
    {
        #region Vertical
        public static VerticalScope BeginVertical(params GUILayoutOption[] options) =>
            VerticalScope.Begin(options);

        public static VerticalScope BeginVertical(GUIStyle style, params GUILayoutOption[] options) =>
            VerticalScope.Begin(style, options);
        #endregion

        #region Horizontal
        public static HorizontalScope BeginHorizontal(params GUILayoutOption[] options) =>
            HorizontalScope.Begin(options);

        public static HorizontalScope BeginHorizontal(GUIStyle style, params GUILayoutOption[] options) =>
            HorizontalScope.Begin(style, options);
        #endregion

        #region Scroll View
        public static ScrollViewScope BeginScrollView(ref Vector2 scrollPosition, params GUILayoutOption[] options)
        {
            return ScrollViewScope.Begin(ref scrollPosition, options);
        }

        public static ScrollViewScope BeginScrollView(
            ref Vector2 scrollPosition,
            bool alwaysShowHorizontal,
            bool alwaysShowVertical,
            params GUILayoutOption[] options)
        {
            return ScrollViewScope.Begin(ref scrollPosition, alwaysShowHorizontal, alwaysShowVertical, options);
        }

        public static ScrollViewScope BeginScrollView(
            ref Vector2 scrollPosition,
            GUIStyle horizontalScrollbar,
            GUIStyle verticalScrollbar,
            params GUILayoutOption[] options)
        {
            return ScrollViewScope.Begin(ref scrollPosition, horizontalScrollbar, verticalScrollbar, options);
        }

        public static ScrollViewScope BeginScrollView(
            ref Vector2 scrollPosition,
            GUIStyle style,
            params GUILayoutOption[] options)
        {
            return ScrollViewScope.Begin(ref scrollPosition, style, options);
        }

        public static ScrollViewScope BeginScrollView(
            ref Vector2 scrollPosition,
            bool alwaysShowHorizontal,
            bool alwaysShowVertical,
            GUIStyle horizontalScrollbar,
            GUIStyle verticalScrollbar,
            GUIStyle background,
            params GUILayoutOption[] options)
        {
            return ScrollViewScope.Begin(ref scrollPosition, alwaysShowHorizontal, alwaysShowVertical,
                horizontalScrollbar, verticalScrollbar, background, options);
        }
        #endregion

        public readonly ref struct VerticalScope
        {
            public static VerticalScope Begin(params GUILayoutOption[] options)
            {
                EditorGUILayout.BeginVertical(options);
                return default;
            }

            public static VerticalScope Begin(GUIStyle style, params GUILayoutOption[] options)
            {
                EditorGUILayout.BeginVertical(style, options);
                return default;
            }

            public void Dispose() => EditorGUILayout.EndVertical();
        }

        public readonly ref struct HorizontalScope
        {
            public static HorizontalScope Begin(params GUILayoutOption[] options)
            {
                EditorGUILayout.BeginHorizontal(options);
                return default;
            }

            public static HorizontalScope Begin(GUIStyle style, params GUILayoutOption[] options)
            {
                EditorGUILayout.BeginHorizontal(style, options);
                return default;
            }

            public void Dispose() => EditorGUILayout.EndHorizontal();
        }

        public readonly ref struct ScrollViewScope
        {
            public static ScrollViewScope Begin(ref Vector2 scrollPosition, params GUILayoutOption[] options)
            {
                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, options);
                return default;
            }

            public static ScrollViewScope Begin(
                ref Vector2 scrollPosition,
                bool alwaysShowHorizontal,
                bool alwaysShowVertical,
                params GUILayoutOption[] options)
            {
                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal,
                    alwaysShowVertical, options);
                return default;
            }

            public static ScrollViewScope Begin(
                ref Vector2 scrollPosition,
                GUIStyle horizontalScrollbar,
                GUIStyle verticalScrollbar,
                params GUILayoutOption[] options)
            {
                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, horizontalScrollbar,
                    verticalScrollbar, options);
                return default;
            }

            public static ScrollViewScope Begin(
                ref Vector2 scrollPosition,
                GUIStyle style,
                params GUILayoutOption[] options)
            {
                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, style, options);
                return default;
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
                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal,
                    alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background, options);
                return default;
            }

            public void Dispose() => EditorGUILayout.EndScrollView();
        }
    }
}
