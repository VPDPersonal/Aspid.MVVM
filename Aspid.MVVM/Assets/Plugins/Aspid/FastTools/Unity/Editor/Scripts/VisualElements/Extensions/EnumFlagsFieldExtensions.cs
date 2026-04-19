using System;
using UnityEditor.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors
{
    public static class EnumFlagsFieldExtensions
    {
        /// <summary>
        /// Initializes the field with a default enum flags value.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="defaultValue">The default enum flags value to display.</param>
        /// <param name="includeObsoleteValues">Whether to include obsolete enum values in the choices.</param>
        /// <returns>The element, for chaining.</returns>
        public static T Initialize<T>(this T element, Enum defaultValue, bool includeObsoleteValues = false)
            where T : EnumFlagsField
        {
            element.Init(defaultValue, includeObsoleteValues);
            return element;
        }
    }
}
