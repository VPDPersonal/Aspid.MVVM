using System;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static class TreeViewExtensions
    {
        #region BindItem
        /// <summary>
        /// Sets <see cref="TreeView.bindItem"/>, replacing any existing callback, and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Callback for binding a data item to the visual element.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBindItem<T>(this T element, Action<VisualElement, int> value)
            where T : TreeView
        {
            element.bindItem = value;
            return element;
        }

        /// <summary>
        /// Subscribes to the <see cref="TreeView.bindItem"/> callback.
        /// </summary>
        /// <remarks>
        /// Callback for binding a data item to the visual element.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to add.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddBindItem<T>(this T element, Action<VisualElement, int> value)
            where T : TreeView
        {
            element.bindItem += value;
            return element;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="TreeView.bindItem"/> callback.
        /// </summary>
        /// <remarks>
        /// Callback for binding a data item to the visual element.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveBindItem<T>(this T element, Action<VisualElement, int> value)
            where T : TreeView
        {
            element.bindItem -= value;
            return element;
        }
        #endregion

        #region UnbindItem
        /// <summary>
        /// Sets <see cref="TreeView.unbindItem"/>, replacing any existing callback, and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Callback for unbinding a data item from the VisualElement.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetUnbindItem<T>(this T element, Action<VisualElement, int> value)
            where T : TreeView
        {
            element.unbindItem = value;
            return element;
        }

        /// <summary>
        /// Subscribes to the <see cref="TreeView.unbindItem"/> callback.
        /// </summary>
        /// <remarks>
        /// Callback for unbinding a data item from the VisualElement.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback invoked to release bindings from a tree item element.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddUnbindItem<T>(this T element, Action<VisualElement, int> value)
            where T : TreeView
        {
            element.unbindItem += value;
            return element;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="TreeView.unbindItem"/> callback.
        /// </summary>
        /// <remarks>
        /// Callback for unbinding a data item from the VisualElement.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveUnbindItem<T>(this T element, Action<VisualElement, int> value)
            where T : TreeView
        {
            element.unbindItem -= value;
            return element;
        }
        #endregion

        #region MakeItem
        /// <summary>
        /// Sets <see cref="TreeView.makeItem"/>, replacing any existing callback, and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Callback for constructing the VisualElement that is the template for each recycled and re-bound element in the list.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetMakeItem<T>(this T element, Func<VisualElement> value)
            where T : TreeView
        {
            element.makeItem = value;
            return element;
        }

        /// <summary>
        /// Subscribes to the <see cref="TreeView.makeItem"/> callback.
        /// </summary>
        /// <remarks>
        /// Callback for constructing the VisualElement that is the template for each recycled and re-bound element in the list.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to add.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddMakeItem<T>(this T element, Func<VisualElement> value)
            where T : TreeView
        {
            element.makeItem += value;
            return element;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="TreeView.makeItem"/> callback.
        /// </summary>
        /// <remarks>
        /// Callback for constructing the VisualElement that is the template for each recycled and re-bound element in the list.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveMakeItem<T>(this T element, Func<VisualElement> value)
            where T : TreeView
        {
            element.makeItem -= value;
            return element;
        }
        #endregion

        #region DestroyItem
        /// <summary>
        /// Sets <see cref="TreeView.destroyItem"/>, replacing any existing callback, and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Callback invoked when a VisualElement created via makeItem is no longer needed and will be destroyed.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetDestroyItem<T>(this T element, Action<VisualElement> value)
            where T : TreeView
        {
            element.destroyItem = value;
            return element;
        }

        /// <summary>
        /// Subscribes to the <see cref="TreeView.destroyItem"/> callback.
        /// </summary>
        /// <remarks>
        /// Callback invoked when a VisualElement created via makeItem is no longer needed and will be destroyed.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to add.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddDestroyItem<T>(this T element, Action<VisualElement> value)
            where T : TreeView
        {
            element.destroyItem += value;
            return element;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="TreeView.destroyItem"/> callback.
        /// </summary>
        /// <remarks>
        /// Callback invoked when a VisualElement created via makeItem is no longer needed and will be destroyed.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveDestroyItem<T>(this T element, Action<VisualElement> value)
            where T : TreeView
        {
            element.destroyItem -= value;
            return element;
        }
        #endregion

        /// <summary>
        /// Sets <see cref="TreeView.itemTemplate"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// A UXML template that constructs each recycled and rebound element within the tree. This template is designed to replace the makeItem definition.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The UXML template to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetItemTemplate<T>(this T element, VisualTreeAsset value)
            where T : TreeView
        {
            element.itemTemplate = value;
            return element;
        }
    }
}
