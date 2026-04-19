using System;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static class ITextSelectionExtensions
    {
        #region OnCursorIndexChange
#if UNITY_6000_3_OR_NEWER
        /// <summary>
        /// Subscribes to the <see cref="ITextSelection.OnCursorIndexChange"/> event.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddOnCursorIndexChange<T>(this T element, Action value)
            where T : ITextSelection
        {
            element.OnCursorIndexChange += value;
            return element;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="ITextSelection.OnCursorIndexChange"/> event.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveOnCursorIndexChange<T>(this T element, Action value)
            where T : ITextSelection
        {
            element.OnCursorIndexChange -= value;
            return element;
        }
#endif
        #endregion

        #region OnSelectIndexChange
#if UNITY_6000_3_OR_NEWER
        /// <summary>
        /// Subscribes to the <see cref="ITextSelection.OnSelectIndexChange"/> event.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddOnSelectIndexChange<T>(this T element, Action value)
            where T : ITextSelection
        {
            element.OnSelectIndexChange += value;
            return element;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="ITextSelection.OnSelectIndexChange"/> event.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveOnSelectIndexChange<T>(this T element, Action value)
            where T : ITextSelection
        {
            element.OnSelectIndexChange -= value;
            return element;
        }
#endif
        #endregion

        /// <summary>
        /// Sets <see cref="ITextSelection.cursorIndex"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// This is the cursor index in the text presented.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The cursor index to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetCursorIndex<T>(this T element, int value)
            where T : ITextSelection
        {
            element.cursorIndex = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="ITextSelection.selectIndex"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// This is the selection index in the text presented.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The selection index to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetSelectIndex<T>(this T element, int value)
            where T : ITextSelection
        {
            element.selectIndex = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="ITextSelection.isSelectable"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Returns true if the field is selectable.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether the field is selectable.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetIsSelectable<T>(this T element, bool value)
            where T : ITextSelection
        {
            element.isSelectable = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="ITextSelection.selectAllOnFocus"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Controls whether the element's content is selected upon receiving focus.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether to select all content on focus.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetSelectAllOnFocus<T>(this T element, bool value)
            where T : ITextSelection
        {
            element.selectAllOnFocus = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="ITextSelection.selectAllOnMouseUp"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Controls whether the element's content is selected when you mouse up for the first time.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether to select all content on the first mouse up.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetSelectAllOnMouseUp<T>(this T element, bool value)
            where T : ITextSelection
        {
            element.selectAllOnMouseUp = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="ITextSelection.doubleClickSelectsWord"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Controls whether double-clicking selects the word under the mouse pointer.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether double-clicking selects a word.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetDoubleClickSelectsWord<T>(this T element, bool value)
            where T : ITextSelection
        {
            element.doubleClickSelectsWord = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="ITextSelection.tripleClickSelectsLine"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Controls whether triple-clicking selects the entire line under the mouse pointer.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether triple-clicking selects a line.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetTripleClickSelectsLine<T>(this T element, bool value)
            where T : ITextSelection
        {
            element.tripleClickSelectsLine = value;
            return element;
        }
    }
}
