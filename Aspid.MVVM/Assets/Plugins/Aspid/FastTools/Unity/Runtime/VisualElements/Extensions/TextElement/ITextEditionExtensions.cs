using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static class ITextEditionExtensions
    {
        /// <summary>
        /// Sets <see cref="ITextEdition.maxLength"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Maximum number of characters for that element.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The maximum character count to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetMaxLength<T>(this T element, int value)
            where T : ITextEdition
        {
            element.maxLength = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="ITextEdition.maskChar"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// The character used for masking when in password mode.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The mask character to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetMaskChar<T>(this T element, char value)
            where T : ITextEdition
        {
            element.maskChar = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="ITextEdition.isDelayed"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// If set to true, the value property isn't updated until either the user presses Enter or the element loses focus.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether the element update is delayed.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetIsDelayed<T>(this T element, bool value)
            where T : ITextEdition
        {
            element.isDelayed = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="ITextEdition.isReadOnly"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Returns true if the element is read only.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether the element is read-only.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetIsReadOnly<T>(this T element, bool value)
            where T : ITextEdition
        {
            element.isReadOnly = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="ITextEdition.isPassword"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Returns true if the field is used to edit a password.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether the field is in password mode.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetIsPassword<T>(this T element, bool value)
            where T : ITextEdition
        {
            element.isPassword = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="ITextEdition.placeholder"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// A short hint to help users understand what to enter in the field.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The placeholder text to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetPlaceholder<T>(this T element, string value)
            where T : ITextEdition
        {
            element.placeholder = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="ITextEdition.autoCorrection"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Determines if the soft keyboard auto correction is turned on or off.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether auto correction is enabled.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetAutoCorrection<T>(this T element, bool value)
            where T : ITextEdition
        {
            element.autoCorrection = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="ITextEdition.hideMobileInput"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Hides or shows the mobile input field.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether to hide the mobile input field.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetHideMobileInput<T>(this T element, bool value)
            where T : ITextEdition
        {
            element.hideMobileInput = value;
            return element;
        }

#if UNITY_6000_4_OR_NEWER
        /// <summary>
        /// Sets <see cref="ITextEdition.hideSoftKeyboard"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Should hide soft / virtual keyboard.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether to hide the soft keyboard.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetHideSoftKeyboard<T>(this T element, bool value)
            where T : ITextEdition
        {
            element.hideSoftKeyboard = value;
            return element;
        }
#endif

        /// <summary>
        /// Sets <see cref="ITextEdition.hidePlaceholderOnFocus"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Hides the placeholder on focus.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether to hide the placeholder when the field is focused.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetHidePlaceholderOnFocus<T>(this T element, bool value)
            where T : ITextEdition
        {
            element.hidePlaceholderOnFocus = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="ITextEdition.keyboardType"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// The type of mobile keyboard that will be used.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The keyboard type to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetKeyboardType<T>(this T element, TouchScreenKeyboardType value)
            where T : ITextEdition
        {
            element.keyboardType = value;
            return element;
        }
    }
}
