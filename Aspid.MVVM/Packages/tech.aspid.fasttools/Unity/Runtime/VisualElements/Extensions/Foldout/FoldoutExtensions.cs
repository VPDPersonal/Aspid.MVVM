using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static class FoldoutExtensions
    {
        /// <summary>
        /// Sets <see cref="Foldout.text"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// The label text for the toggle.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The text to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetText<T>(this T element, string value)
            where T : Foldout
        {
            element.text = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="Foldout.toggleOnLabelClick"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Whether to toggle the element state when the user clicks the label.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether clicking the label toggles the foldout.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetToggleOnLabelClick<T>(this T element, bool value)
            where T : Foldout
        {
            element.toggleOnLabelClick = value;
            return element;
        }
    }
}
