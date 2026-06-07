using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static class TextElementExtensions
    {
        /// <summary>
        /// Sets <see cref="TextElement.text"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// The text to be displayed.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The text to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetText<T>(this T element, string value)
            where T : TextElement
        {
            element.text = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="TextElement.enableRichText"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// When false, rich text tags will not be parsed.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether rich text parsing is enabled.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetEnableRichText<T>(this T element, bool value)
            where T : TextElement
        {
            element.enableRichText = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="TextElement.emojiFallbackSupport"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Specifies the order in which the system should look for Emoji characters when rendering text.
        /// If this setting is enabled, the global Emoji Fallback list will be searched first for characters defined as Emoji in the Unicode 14.0 standard.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether emoji fallback support is enabled.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetEmojiFallbackSupport<T>(this T element, bool value)
            where T : TextElement
        {
            element.emojiFallbackSupport = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="TextElement.parseEscapeSequences"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Determines how escape sequences are displayed. When set to true, escape sequences (such as \n, \t) are parsed and transformed into their corresponding characters.
        /// For example, '\n' will insert a new line. When set to false, escape sequences are displayed as raw text (for example, \n is shown as the characters '\' followed by 'n').
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether escape sequences are parsed.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetParseEscapeSequences<T>(this T element, bool value)
            where T : TextElement
        {
            element.parseEscapeSequences = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="TextElement.displayTooltipWhenElided"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// When true, a tooltip displays the full version of elided text, and also if a tooltip had been previously provided, it will be overwritten.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether to display a tooltip when text is elided.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetDisplayTooltipWhenElided<T>(this T element, bool value)
            where T : TextElement
        {
            element.displayTooltipWhenElided = value;
            return element;
        }
    }
}
