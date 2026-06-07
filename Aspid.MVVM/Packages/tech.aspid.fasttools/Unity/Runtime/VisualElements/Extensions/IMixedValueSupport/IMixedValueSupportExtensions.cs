using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static class IMixedValueSupportExtensions
    {
        /// <summary>
        /// Sets <see cref="IMixedValueSupport.showMixedValue"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Indicates whether to enable the mixed value state on the value field.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether to show the mixed value state.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetShowMixedValue<T>(this T element, bool value)
            where T : IMixedValueSupport
        {
            element.showMixedValue = value;
            return element;
        }
    }
}
