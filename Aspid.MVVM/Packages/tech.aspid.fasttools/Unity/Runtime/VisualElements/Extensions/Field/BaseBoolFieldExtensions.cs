using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static class BaseBoolFieldExtensions
    {
        /// <summary>
        /// Sets <see cref="BaseBoolField.text"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Optional text that appears after the BaseBoolField.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The text to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetText<T>(this T element, string value)
            where T : BaseBoolField
        {
            element.text = value;
            return element;
        }

        /// <summary>
        /// Sets the <see cref="BaseBoolField.label"/> property and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The label text to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetLabel<T>(this T element, string value)
            where T : BaseBoolField
        {
            element.label = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="BaseBoolField.toggleOnLabelClick"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Whether to activate the toggle when the user clicks the label.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether clicking the label activates the toggle.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetToggleOnLabelClick<T>(this T element, bool value)
            where T : BaseBoolField
        {
            element.toggleOnLabelClick = value;
            return element;
        }
    }
}
