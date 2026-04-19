using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static class MultiColumnTreeViewExtensions
    {
        /// <summary>
        /// Sets <see cref="MultiColumnTreeView.sortingMode"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Indicates how to sort columns. To enable sorting, set it to ColumnSortingMode.Default or ColumnSortingMode.Custom.
        /// The Default mode uses the sorting algorithm provided by MultiColumnController, acting on indices. You can also implement your own sorting with the Custom mode, by responding to the columnSortingChanged event.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The sorting mode to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetSortingMode<T>(this T element, ColumnSortingMode value)
            where T : MultiColumnTreeView
        {
            element.sortingMode = value;
            return element;
        }
    }
}
