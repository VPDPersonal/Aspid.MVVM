using System;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Internal extension methods for UIToolkit types used by Aspid visual elements.
    /// </summary>
    public static class AspidVisualElementExtensions
    {
        /// <summary>
        /// Reads a USS custom property of type <c>string</c> and attempts to parse it as an enum value.
        /// </summary>
        /// <typeparam name="T">The enum type to parse into.</typeparam>
        /// <param name="style">The custom style resolver from a <c>CustomStyleResolvedEvent</c>.</param>
        /// <param name="property">The custom property to read.</param>
        /// <param name="value">The parsed enum value, or <c>default</c> if parsing fails.</param>
        /// <returns><see langword="true"/> if the property was found and parsed successfully.</returns>
        public static bool TryGetByEnum<T>(this ICustomStyle style, CustomStyleProperty<string> property, out T value)
            where T : struct, Enum
        {
            value = default;

            return style.TryGetValue(property, out var propertyValue)
                && Enum.TryParse(propertyValue, ignoreCase: true, out value);
        }

        #region ToUss
        /// <summary>
        /// Returns the USS class name corresponding to the given <see cref="ThemeStyle"/>.
        /// </summary>
        /// <param name="style">The theme style to convert.</param>
        /// <returns>A USS class name string.</returns>
        public static string ToUss(this ThemeStyle style) => style switch
        {
            ThemeStyle.Darkness => StyleClasses.Theme.Darkness,
            ThemeStyle.Dark => StyleClasses.Theme.Dark,
            ThemeStyle.Light => StyleClasses.Theme.Light,
            ThemeStyle.Lightness => StyleClasses.Theme.Lightness,
            _ => throw new ArgumentOutOfRangeException(nameof(style), style, null)
        };

        /// <summary>
        /// Returns the USS class name corresponding to the given <see cref="StatusStyle"/>,
        /// or <see cref="string.Empty"/> for <see cref="StatusStyle.None"/>.
        /// </summary>
        /// <param name="status">The status style to convert.</param>
        /// <returns>A USS class name string, or an empty string for <see cref="StatusStyle.None"/>.</returns>
        public static string ToUss(this StatusStyle status) => status switch
        {
            StatusStyle.None => string.Empty,
            StatusStyle.Success => StyleClasses.Status.Success,
            StatusStyle.Warning => StyleClasses.Status.Warning,
            StatusStyle.Error => StyleClasses.Status.Error,
            StatusStyle.Info => StyleClasses.Status.Info,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
        #endregion
    }
}
