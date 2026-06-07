using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static partial class VisualElementExtensions
    {
        #region Class
        /// <summary>
        /// Removes all classes from the class list of this element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <returns>The element, for chaining.</returns>
        public static T ClearClasses<T>(this T element)
            where T : VisualElement
        {
            element.ClearClassList();
            return element;
        }

        /// <summary>
        /// Adds a class to the class list of the element in order to assign styles from USS. Note the class name is case-sensitive.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The USS class name to add.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddClass<T>(this T element, string value)
            where T : VisualElement
        {
            element.AddToClassList(value);
            return element;
        }

        /// <summary>
        /// Removes a class from the class list of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The USS class name to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveClass<T>(this T element, string value)
            where T : VisualElement
        {
            element.RemoveFromClassList(value);
            return element;
        }

        /// <summary>
        /// Toggles between adding and removing the given class name from the class list.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The USS class name to toggle.</param>
        /// <returns>The element, for chaining.</returns>
        public static T ToggleInClass<T>(this T element, string value)
            where T : VisualElement
        {
            element.ToggleInClassList(value);
            return element;
        }

        /// <summary>
        /// Enables or disables the class with the given name.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="className">The USS class name to enable or disable.</param>
        /// <param name="enable">Whether to enable or disable the class.</param>
        /// <returns>The element, for chaining.</returns>
        public static T EnableInClass<T>(this T element, string className, bool enable)
            where T : VisualElement
        {
            element.EnableInClassList(className, enable);
            return element;
        }
        #endregion

        #region StyleSheets
        /// <summary>
        /// Adds a USS style sheet to the element's style sheet list.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The style sheet to add.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddStyleSheets<T>(this T element, StyleSheet value)
            where T : VisualElement
        {
            element.styleSheets.Add(value);
            return element;
        }

        /// <summary>
        /// Loads and adds a USS style sheet from a Resources path.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="path">The Resources-relative path to the style sheet asset.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddStyleSheetsFromResource<T>(this T element, string path)
            where T : VisualElement
        {
            var styleSheet = Resources.Load<StyleSheet>(path);
            if (styleSheet == null)
            {
                Debug.LogWarning($"Failed to load StyleSheet from Resources path: '{path}'");
                return element;
            }

            return element.AddStyleSheets(styleSheet);
        }

        /// <summary>
        /// Removes a style sheet for the owner element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The style sheet to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveStyleSheets<T>(this T element, StyleSheet value)
            where T : VisualElement
        {
            element.styleSheets.Remove(value);
            return element;
        }

        /// <summary>
        /// Loads and removes a USS style sheet identified by its Resources path.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="path">The Resources-relative path to the style sheet asset.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveStyleSheetsFromResource<T>(this T element, string path)
            where T : VisualElement
        {
            var styleSheet = Resources.Load<StyleSheet>(path);
            if (styleSheet == null)
            {
                Debug.LogWarning($"Failed to load StyleSheet from Resources path: '{path}'");
                return element;
            }

            return element.RemoveStyleSheets(styleSheet);
        }
        #endregion
    }
}
