using System;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static class EnumFieldExtensions
    {
        /// <summary>
        /// Initializes the field with a default enum value via <see cref="EnumField.Init(Enum, bool)"/>
        /// and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="defaultValue">The default enum value to display.</param>
        /// <param name="includeObsoleteValues">Whether to include obsolete enum values in the choices.</param>
        /// <returns>The element, for chaining.</returns>
        public static T Initialize<T>(this T element, Enum defaultValue, bool includeObsoleteValues = false)
            where T : EnumField
        {
            element.Init(defaultValue, includeObsoleteValues);
            return element;
        }
    }
}
