using System;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static class MultiColumnListViewExtensions
    {
        /// <summary>
        /// Sets <see cref="MultiColumnListView.sortingMode"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Indicates how to sort columns. To enable sorting, set it to ColumnSortingMode.Default or ColumnSortingMode.Custom.
        /// The Default mode uses the sorting algorithm provided by MultiColumnController, acting on indices. You can also implement your own sorting with the Custom mode, by responding to the columnSortingChanged event.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The sorting mode to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetSortingMode<T>(this T element, ColumnSortingMode value)
            where T : MultiColumnListView
        {
            element.sortingMode = value;
            return element;
        }

        /// <summary>
        /// Subscribes to the <see cref="MultiColumnListView.columnSortingChanged"/> event and returns the element for chaining.
        /// </summary>
        /// <typeparam name="T">The element type.</typeparam>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddColumnSortingChanged<T>(this T element, Action callback)
            where T : MultiColumnListView
        {
            element.columnSortingChanged += callback;
            return element;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="MultiColumnListView.columnSortingChanged"/> event and returns the element for chaining.
        /// </summary>
        /// <typeparam name="T">The element type.</typeparam>
        /// <param name="element">The element to modify.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveColumnSortingChanged<T>(this T element, Action callback)
            where T : MultiColumnListView
        {
            element.columnSortingChanged -= callback;
            return element;
        }
    }
}
