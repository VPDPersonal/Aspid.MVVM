using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static class ProgressBarExtensions
    {
        /// <summary>
        /// Sets the title of the ProgressBar that displays in the center of the control and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The title text to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetTitle<T>(this T element, string value)
            where T : AbstractProgressBar
        {
            element.title = value;
            return element;
        }

        /// <summary>
        /// Sets the minimum value of the ProgressBar and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The minimum value to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetLowValue<T>(this T element, float value)
            where T : AbstractProgressBar
        {
            element.lowValue = value;
            return element;
        }

        /// <summary>
        /// Sets the maximum value of the ProgressBar and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The maximum value to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetHighValue<T>(this T element, float value)
            where T : AbstractProgressBar
        {
            element.highValue = value;
            return element;
        }

        /// <summary>
        /// Sets the current value of the progress bar via <see cref="AbstractProgressBar.value"/>.
        /// </summary>
        /// <typeparam name="T">The element type.</typeparam>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The value to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetValue<T>(this T element, float value)
            where T : AbstractProgressBar
        {
            element.value = value;
            return element;
        }
    }
}
