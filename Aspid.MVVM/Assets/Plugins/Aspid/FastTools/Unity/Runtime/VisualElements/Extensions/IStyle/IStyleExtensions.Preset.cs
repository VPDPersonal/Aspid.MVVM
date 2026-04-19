using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static partial class IStyleExtensions
    {
        /// <summary>
        /// Sets <see cref="IStyle.unityFontStyleAndWeight"/> to <see cref="FontStyle.Normal"/>, removing bold and italic.
        /// </summary>
        /// <remarks>
        /// Sets the value unconditionally, regardless of any current bold or italic style.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <returns>The style, for chaining.</returns>
        public static T SetNormalUnityFontStyleAndWeight<T>(this T style)
            where T : IStyle
        {
            return style.SetUnityFontStyleAndWeight(FontStyle.Normal);
        }

        /// <summary>
        /// Adds bold to <see cref="IStyle.unityFontStyleAndWeight"/>, preserving any existing italic style.
        /// </summary>
        /// <remarks>
        /// Transitions: <see cref="FontStyle.Normal"/> → <see cref="FontStyle.Bold"/>,
        /// <see cref="FontStyle.Italic"/> → <see cref="FontStyle.BoldAndItalic"/>.
        /// Other values are left unchanged.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <returns>The style, for chaining.</returns>
        public static T AddBoldUnityFontStyleAndWeight<T>(this T style)
            where T : IStyle
        {
            return style.unityFontStyleAndWeight.value switch
            {
                FontStyle.Normal => style.SetUnityFontStyleAndWeight(FontStyle.Bold),
                FontStyle.Italic => style.SetUnityFontStyleAndWeight(FontStyle.BoldAndItalic),
                _ => style
            };
        }

        /// <summary>
        /// Removes bold from <see cref="IStyle.unityFontStyleAndWeight"/>, preserving any existing italic style.
        /// </summary>
        /// <remarks>
        /// Transitions: <see cref="FontStyle.Bold"/> → <see cref="FontStyle.Normal"/>,
        /// <see cref="FontStyle.BoldAndItalic"/> → <see cref="FontStyle.Italic"/>.
        /// Other values are left unchanged.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <returns>The style, for chaining.</returns>
        public static T RemoveBoldUnityFontStyleAndWeight<T>(this T style)
            where T : IStyle
        {
            return style.unityFontStyleAndWeight.value switch
            {
                FontStyle.Bold => style.SetUnityFontStyleAndWeight(FontStyle.Normal),
                FontStyle.BoldAndItalic => style.SetUnityFontStyleAndWeight(FontStyle.Italic),
                _ => style
            };
        }

        /// <summary>
        /// Adds italic to <see cref="IStyle.unityFontStyleAndWeight"/>, preserving any existing bold style.
        /// </summary>
        /// <remarks>
        /// Transitions: <see cref="FontStyle.Normal"/> → <see cref="FontStyle.Italic"/>,
        /// <see cref="FontStyle.Bold"/> → <see cref="FontStyle.BoldAndItalic"/>.
        /// Other values are left unchanged.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <returns>The style, for chaining.</returns>
        public static T AddItalicUnityFontStyleAndWeight<T>(this T style)
            where T : IStyle
        {
            return style.unityFontStyleAndWeight.value switch
            {
                FontStyle.Normal => style.SetUnityFontStyleAndWeight(FontStyle.Italic),
                FontStyle.Bold => style.SetUnityFontStyleAndWeight(FontStyle.BoldAndItalic),
                _ => style
            };
        }

        /// <summary>
        /// Removes italic from <see cref="IStyle.unityFontStyleAndWeight"/>, preserving any existing bold style.
        /// </summary>
        /// <remarks>
        /// Transitions: <see cref="FontStyle.Italic"/> → <see cref="FontStyle.Normal"/>,
        /// <see cref="FontStyle.BoldAndItalic"/> → <see cref="FontStyle.Bold"/>.
        /// Other values are left unchanged.
        /// </remarks>
        /// <param name="style">The style to modify.</param>
        /// <returns>The style, for chaining.</returns>
        public static T RemoveItalicUnityFontStyleAndWeight<T>(this T style)
            where T : IStyle
        {
            return style.unityFontStyleAndWeight.value switch
            {
                FontStyle.Italic => style.SetUnityFontStyleAndWeight(FontStyle.Normal),
                FontStyle.BoldAndItalic => style.SetUnityFontStyleAndWeight(FontStyle.Bold),
                _ => style
            };
        }
    }
}
