using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static class FocusableExtensions
    {
        /// <summary>
        /// Tells the element to release the focus and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBlur<T>(this T element)
            where T : Focusable
        {
            element.Blur();
            return element;
        }

        /// <summary>
        /// Attempts to give the focus to this element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetFocus<T>(this T element)
            where T : Focusable
        {
            element.Focus();
            return element;
        }

        /// <summary>
        /// Returns <see langword="true"/> if this element currently has keyboard focus.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns><see langword="true"/> if the element holds keyboard focus; otherwise <see langword="false"/>.</returns>
        public static bool IsFocus(this Focusable element) =>
            element.focusController?.focusedElement == element;

        /// <summary>
        /// Sets <see cref="Focusable.tabIndex"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// An integer used to sort focusable elements in the focus ring. Must be greater than or equal to zero.
        /// </remarks>
        /// <param name="focusable">The element to modify.</param>
        /// <param name="value">The tab index to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetTabIndex<T>(this T focusable, int value)
            where T : Focusable
        {
            focusable.tabIndex = value;
            return focusable;
        }

        /// <summary>
        /// Sets <see cref="Focusable.focusable"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Whether an element can potentially receive focus.
        /// </remarks>
        /// <param name="focusable">The element to modify.</param>
        /// <param name="value">Whether this element can receive focus.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetFocusable<T>(this T focusable, bool value)
            where T : Focusable
        {
            focusable.focusable = value;
            return focusable;
        }

        /// <summary>
        /// Sets <see cref="Focusable.delegatesFocus"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Whether the element delegates the focus to its children.
        /// </remarks>
        /// <param name="focusable">The element to modify.</param>
        /// <param name="value">Whether focus is delegated to children.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetDelegatesFocus<T>(this T focusable, bool value)
            where T : Focusable
        {
            focusable.delegatesFocus = value;
            return focusable;
        }
    }
}
