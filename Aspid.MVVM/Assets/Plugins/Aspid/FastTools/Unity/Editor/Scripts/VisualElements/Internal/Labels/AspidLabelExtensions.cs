using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Fluent extension methods for <see cref="AspidLabel"/>.
    /// </summary>
    public static class AspidLabelExtensions
    {
        /// <summary>
        /// Sets <see cref="AspidLabel.Text"/> and returns the element for chaining.
        /// </summary>
        public static AspidLabel SetText(this AspidLabel element, string value)
        {
            element.Text = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidLabel.LabelTheme"/> and returns the element for chaining.
        /// </summary>
        public static AspidLabel SetLabelTheme(this AspidLabel element, ThemeStyle value)
        {
            element.LabelTheme = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidLabel.LabelStatus"/> and returns the element for chaining.
        /// </summary>
        public static AspidLabel SetLabelStatus(this AspidLabel element, StatusStyle value)
        {
            element.LabelStatus = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidLabel.LabelSize"/> and returns the element for chaining.
        /// </summary>
        public static AspidLabel SetLabelSize(this AspidLabel element, AspidLabelSizeStyle value)
        {
            element.LabelSize = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidLabel.UnityFontStyleAndWeight"/> and returns the element for chaining.
        /// </summary>
        public static AspidLabel SetUnityFontStyleAndWeight(this AspidLabel element, StyleEnum<FontStyle> value)
        {
            element.UnityFontStyleAndWeight = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidLabel.LineTheme"/> and returns the element for chaining.
        /// </summary>
        public static AspidLabel SetLineTheme(this AspidLabel element, ThemeStyle value)
        {
            element.LineTheme = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidLabel.LineStatus"/> and returns the element for chaining.
        /// </summary>
        public static AspidLabel SetLineStatus(this AspidLabel element, StatusStyle value)
        {
            element.LineStatus = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidLabel.LineSize"/> and returns the element for chaining.
        /// </summary>
        public static AspidLabel SetLineSize(this AspidLabel element, DividingLineSize value)
        {
            element.LineSize = value;
            return element;
        }
    }
}
