using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Fluent extension methods for <see cref="AspidLabel"/>.
    /// </summary>
    internal static class AspidLabelExtensions
    {
        /// <summary>
        /// Sets <see cref="AspidLabel.Text"/> and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to configure.</param>
        /// <param name="value">The new text.</param>
        public static AspidLabel SetText(this AspidLabel element, string value)
        {
            element.Text = value;
            return element;
        }
        
        /// <summary>
        /// Sets <see cref="AspidLabel.Selectable"/> and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to configure.</param>
        /// <param name="value">Whether the label text can be selected by the user.</param>
        public static AspidLabel SetSelectable(this AspidLabel element, bool value)
        {
            element.Selectable = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidLabel.LabelTheme"/> and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to configure.</param>
        /// <param name="value">The new theme.</param>
        public static AspidLabel SetLabelTheme(this AspidLabel element, ThemeStyle.Type value)
        {
            element.LabelTheme = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidLabel.LabelStatus"/> and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to configure.</param>
        /// <param name="value">The new status.</param>
        public static AspidLabel SetLabelStatus(this AspidLabel element, StatusStyle.Type value)
        {
            element.LabelStatus = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidLabel.LabelSize"/> and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to configure.</param>
        /// <param name="value">The new size.</param>
        public static AspidLabel SetLabelSize(this AspidLabel element, AspidLabelSizeStyle.Type value)
        {
            element.LabelSize = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidLabel.LabelFontStyle"/> and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to configure.</param>
        /// <param name="value">The new font style.</param>
        public static AspidLabel SetLabelFontStyle(this AspidLabel element, FontStyle value)
        {
            element.LabelFontStyle = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidLabel.LineTheme"/> and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to configure.</param>
        /// <param name="value">The new line theme.</param>
        public static AspidLabel SetLineTheme(this AspidLabel element, ThemeStyle.Type value)
        {
            element.LineTheme = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidLabel.LineStatus"/> and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to configure.</param>
        /// <param name="value">The new line status.</param>
        public static AspidLabel SetLineStatus(this AspidLabel element, StatusStyle.Type value)
        {
            element.LineStatus = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidLabel.LineSize"/> and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to configure.</param>
        /// <param name="value">The new line size.</param>
        public static AspidLabel SetLineSize(this AspidLabel element, AspidDividingLineSizeStyle.Type value)
        {
            element.LineSize = value;
            return element;
        }
    }
}
