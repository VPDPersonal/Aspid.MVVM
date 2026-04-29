using System;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements
{
    /// <summary>
    /// Extension methods for <see cref="ICustomStyle"/> that bridge USS string-typed custom
    /// properties to strongly-typed C# values.
    /// </summary>
    public static class ICustomStyleExtensions
    {
        /// <summary>
        /// Resolves a <see cref="CustomStyleProperty{T}"/> whose USS value is a string and parses
        /// it as the enum <typeparamref name="T"/>. Parsing is case-insensitive.
        /// </summary>
        /// <typeparam name="T">The enum type to parse the USS value as.</typeparam>
        /// <param name="style">The resolved custom-style container, typically obtained from
        /// <see cref="CustomStyleResolvedEvent.customStyle"/>.</param>
        /// <param name="property">The custom style property whose string value should be parsed.</param>
        /// <param name="value">When this method returns <see langword="true"/>, the parsed enum
        /// value; otherwise <see langword="default"/>.</param>
        /// <returns><see langword="true"/> if the property was resolved and successfully parsed
        /// as <typeparamref name="T"/>; otherwise <see langword="false"/>.</returns>
        public static bool TryGetByEnum<T>(this ICustomStyle style, CustomStyleProperty<string> property, out T value)
            where T : struct, Enum
        {
            value = default;

            return style.TryGetValue(property, out var propertyValue)
                && Enum.TryParse(propertyValue, ignoreCase: true, out value);
        }
    }
}
