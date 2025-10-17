using UnityEngine;

// ReSharper disable CheckNamespace
namespace Aspid.UnityFastTools.Editors
{
    public static class AspidEditorGUILayout
    {
        #region Vertical
        public static VerticalScope BeginVertical(params GUILayoutOption[] options) =>
            VerticalScope.Begin(options);

        public static VerticalScope BeginVertical(GUIStyle style, params GUILayoutOption[] options) =>
            VerticalScope.Begin(style, options);
        
        public static VerticalScope BeginVertical(out Rect rect, params GUILayoutOption[] options) =>
            VerticalScope.Begin(out rect, options);

        public static VerticalScope BeginVertical(out Rect rect, GUIStyle style, params GUILayoutOption[] options) =>
            VerticalScope.Begin(out rect, style, options);
        #endregion

        #region Horizontal
        public static HorizontalScope BeginHorizontal(params GUILayoutOption[] options) =>
            HorizontalScope.Begin(options);

        public static HorizontalScope BeginHorizontal(GUIStyle style, params GUILayoutOption[] options) =>
            HorizontalScope.Begin(style, options);
        
        public static HorizontalScope BeginHorizontal(out Rect rect, params GUILayoutOption[] options) =>
            HorizontalScope.Begin(out rect, options);

        public static HorizontalScope BeginHorizontal(out Rect rect, GUIStyle style, params GUILayoutOption[] options) =>
            HorizontalScope.Begin(out rect, style, options);
        #endregion
        
        #region Scroll View
        public static ScrollViewScope BeginScrollView(
            Vector2 scrollPosition, 
            params GUILayoutOption[] options)
        {
            return ScrollViewScope.Begin(scrollPosition, options);
        }

        public static ScrollViewScope BeginScrollView(
            Vector2 scrollPosition,
            bool alwaysShowHorizontal,
            bool alwaysShowVertical,
            params GUILayoutOption[] options)
        {
            return ScrollViewScope.Begin(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, options);
        }

        public static ScrollViewScope BeginScrollView(
            Vector2 scrollPosition,
            GUIStyle horizontalScrollbar,
            GUIStyle verticalScrollbar,
            params GUILayoutOption[] options)
        {
            return ScrollViewScope.Begin(scrollPosition, horizontalScrollbar, verticalScrollbar, options);
        }

        public static ScrollViewScope BeginScrollView(
            Vector2 scrollPosition,
            GUIStyle style,
            params GUILayoutOption[] options)
        {
            return ScrollViewScope.Begin(scrollPosition, style, options);
        }

        public static ScrollViewScope BeginScrollView(
            Vector2 scrollPosition,
            bool alwaysShowHorizontal,
            bool alwaysShowVertical,
            GUIStyle horizontalScrollbar,
            GUIStyle verticalScrollbar,
            GUIStyle background,
            params GUILayoutOption[] options)
        {
            return ScrollViewScope.Begin(scrollPosition, alwaysShowHorizontal, alwaysShowVertical,
                horizontalScrollbar, verticalScrollbar, background, options);
        }
        
        public static ScrollViewScope BeginScrollView(
            ref Vector2 scrollPosition, 
            params GUILayoutOption[] options)
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
    }
}