using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static partial class VisualElementExtensions
    {
        /// <summary>
        /// Sets <see cref="IStyle.unityFontStyleAndWeight"/> to <see cref="FontStyle.Normal"/>, removing bold and italic.
        /// </summary>
        /// <remarks>
        /// Sets the value unconditionally, regardless of any current bold or italic style.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetNormalUnityFontStyleAndWeight<T>(this T element)
            where T : VisualElement
        {
            element.style.SetNormalUnityFontStyleAndWeight();
            return element;
        }

        /// <summary>
        /// Adds bold to <see cref="IStyle.unityFontStyleAndWeight"/>, preserving any existing italic style.
        /// </summary>
        /// <remarks>
        /// Transitions: <see cref="FontStyle.Normal"/> → <see cref="FontStyle.Bold"/>,
        /// <see cref="FontStyle.Italic"/> → <see cref="FontStyle.BoldAndItalic"/>.
        /// Other values are left unchanged.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddBoldUnityFontStyleAndWeight<T>(this T element)
            where T : VisualElement
        {
            element.style.AddBoldUnityFontStyleAndWeight();
            return element;
        }

        /// <summary>
        /// Removes bold from <see cref="IStyle.unityFontStyleAndWeight"/>, preserving any existing italic style.
        /// </summary>
        /// <remarks>
        /// Transitions: <see cref="FontStyle.Bold"/> → <see cref="FontStyle.Normal"/>,
        /// <see cref="FontStyle.BoldAndItalic"/> → <see cref="FontStyle.Italic"/>.
        /// Other values are left unchanged.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveBoldUnityFontStyleAndWeight<T>(this T element)
            where T : VisualElement
        {
            element.style.RemoveBoldUnityFontStyleAndWeight();
            return element;
        }

        /// <summary>
        /// Adds italic to <see cref="IStyle.unityFontStyleAndWeight"/>, preserving any existing bold style.
        /// </summary>
        /// <remarks>
        /// Transitions: <see cref="FontStyle.Normal"/> → <see cref="FontStyle.Italic"/>,
        /// <see cref="FontStyle.Bold"/> → <see cref="FontStyle.BoldAndItalic"/>.
        /// Other values are left unchanged.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddItalicUnityFontStyleAndWeight<T>(this T element)
            where T : VisualElement
        {
            element.style.AddItalicUnityFontStyleAndWeight();
            return element;
        }

        /// <summary>
        /// Removes italic from <see cref="IStyle.unityFontStyleAndWeight"/>, preserving any existing bold style.
        /// </summary>
        /// <remarks>
        /// Transitions: <see cref="FontStyle.Italic"/> → <see cref="FontStyle.Normal"/>,
        /// <see cref="FontStyle.BoldAndItalic"/> → <see cref="FontStyle.Bold"/>.
        /// Other values are left unchanged.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveItalicUnityFontStyleAndWeight<T>(this T element)
            where T : VisualElement
        {
            element.style.RemoveItalicUnityFontStyleAndWeight();
            return element;
        }
    }
}
