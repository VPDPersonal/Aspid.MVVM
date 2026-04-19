using System;
using System.Collections;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static class BaseVerticalCollectionViewExtensions
    {
        #region ItemsChosen
        /// <summary>
        /// Subscribes to the <see cref="BaseVerticalCollectionView.itemsChosen"/> event.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddItemsChosen<T>(this T element, Action<IEnumerable<object>> value)
            where T : BaseVerticalCollectionView
        {
            element.itemsChosen += value;
            return element;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="BaseVerticalCollectionView.itemsChosen"/> event.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveItemsChosen<T>(this T element, Action<IEnumerable<object>> value)
            where T : BaseVerticalCollectionView
        {
            element.itemsChosen -= value;
            return element;
        }
        #endregion

        #region CanStartDrag
        /// <summary>
        /// Subscribes to the <see cref="BaseVerticalCollectionView.canStartDrag"/> callback.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddCanStartDrag<T>(this T element, Func<CanStartDragArgs, bool> value)
            where T : BaseVerticalCollectionView
        {
            element.canStartDrag += value;
            return element;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="BaseVerticalCollectionView.canStartDrag"/> callback.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveCanStartDrag<T>(this T element, Func<CanStartDragArgs, bool> value)
            where T : BaseVerticalCollectionView
        {
            element.canStartDrag -= value;
            return element;
        }
        #endregion

        #region SelectionChanged
        /// <summary>
        /// Subscribes to the <see cref="BaseVerticalCollectionView.selectionChanged"/> event.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddSelectionChanged<T>(this T element, Action<IEnumerable<object>> value)
            where T : BaseVerticalCollectionView
        {
            element.selectionChanged += value;
            return element;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="BaseVerticalCollectionView.selectionChanged"/> event.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveSelectionChanged<T>(this T element, Action<IEnumerable<object>> value)
            where T : BaseVerticalCollectionView
        {
            element.selectionChanged -= value;
            return element;
        }
        #endregion

        #region ItemIndexChanged
        /// <summary>
        /// Subscribes to the <see cref="BaseVerticalCollectionView.itemIndexChanged"/> event.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddItemIndexChanged<T>(this T element, Action<int, int> value)
            where T : BaseVerticalCollectionView
        {
            element.itemIndexChanged += value;
            return element;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="BaseVerticalCollectionView.itemIndexChanged"/> event.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveItemIndexChanged<T>(this T element, Action<int, int> value)
            where T : BaseVerticalCollectionView
        {
            element.itemIndexChanged -= value;
            return element;
        }
        #endregion

        #region SetupDragAndDrop
        /// <summary>
        /// Subscribes to the <see cref="BaseVerticalCollectionView.setupDragAndDrop"/> callback.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddSetupDragAndDrop<T>(this T element, Func<SetupDragAndDropArgs, StartDragArgs> value)
            where T : BaseVerticalCollectionView
        {
            element.setupDragAndDrop += value;
            return element;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="BaseVerticalCollectionView.setupDragAndDrop"/> callback.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveSetupDragAndDrop<T>(this T element, Func<SetupDragAndDropArgs, StartDragArgs> value)
            where T : BaseVerticalCollectionView
        {
            element.setupDragAndDrop -= value;
            return element;
        }
        #endregion

        #region DragAndDropUpdate
        /// <summary>
        /// Subscribes to the <see cref="BaseVerticalCollectionView.dragAndDropUpdate"/> callback.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddSetupDragAndDrop<T>(this T element, Func<HandleDragAndDropArgs, DragVisualMode> value)
            where T : BaseVerticalCollectionView
        {
            element.dragAndDropUpdate += value;
            return element;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="BaseVerticalCollectionView.dragAndDropUpdate"/> callback.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveSetupDragAndDrop<T>(this T element, Func<HandleDragAndDropArgs, DragVisualMode> value)
            where T : BaseVerticalCollectionView
        {
            element.dragAndDropUpdate -= value;
            return element;
        }
        #endregion

        #region DragAndDropUpdate
        /// <summary>
        /// Subscribes to the <see cref="BaseVerticalCollectionView.handleDrop"/> callback.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddHandleDrop<T>(this T element, Func<HandleDragAndDropArgs, DragVisualMode> value)
            where T : BaseVerticalCollectionView
        {
            element.handleDrop += value;
            return element;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="BaseVerticalCollectionView.handleDrop"/> callback.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveHandleDrop<T>(this T element, Func<HandleDragAndDropArgs, DragVisualMode> value)
            where T : BaseVerticalCollectionView
        {
            element.handleDrop -= value;
            return element;
        }
        #endregion

        #region ItemsSourceChanged
        /// <summary>
        /// Subscribes to the <see cref="BaseVerticalCollectionView.itemsSourceChanged"/> event.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddItemsSourceChanged<T>(this T element, Action value)
            where T : BaseVerticalCollectionView
        {
            element.itemsSourceChanged += value;
            return element;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="BaseVerticalCollectionView.itemsSourceChanged"/> event.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveItemsSourceChanged<T>(this T element, Action value)
            where T : BaseVerticalCollectionView
        {
            element.itemsSourceChanged -= value;
            return element;
        }
        #endregion

        #region SelectedIndicesChanged
        /// <summary>
        /// Subscribes to the <see cref="BaseVerticalCollectionView.selectedIndicesChanged"/> event.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddSelectedIndicesChanged<T>(this T element, Action<IEnumerable<int>> value)
            where T : BaseVerticalCollectionView
        {
            element.selectedIndicesChanged += value;
            return element;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="BaseVerticalCollectionView.selectedIndicesChanged"/> event.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveSelectedIndicesChanged<T>(this T element, Action<IEnumerable<int>> value)
            where T : BaseVerticalCollectionView
        {
            element.selectedIndicesChanged -= value;
            return element;
        }
        #endregion

#if UNITY_6000_0_OR_NEWER
        #region MakeFooter
        /// <summary>
        /// Sets <see cref="BaseListView.makeFooter"/>, replacing any existing callback, and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// This callback allows the user to make their own footer for this control.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetMakeFooter<T>(this T element, Func<VisualElement> value)
            where T : BaseListView
        {
            element.makeFooter = value;
            return element;
        }

        /// <summary>
        /// Subscribes to the <see cref="BaseListView.makeFooter"/> callback.
        /// </summary>
        /// <remarks>
        /// This callback allows the user to make their own footer for this control.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to add.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddMakeFooter<T>(this T element, Func<VisualElement> value)
            where T : BaseListView
        {
            element.makeFooter += value;
            return element;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="BaseListView.makeFooter"/> callback.
        /// </summary>
        /// <remarks>
        /// This callback allows the user to make their own footer for this control.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveMakeFooter<T>(this T element, Func<VisualElement> value)
            where T : BaseListView
        {
            element.makeFooter -= value;
            return element;
        }
        #endregion

        #region MakeHeader
        /// <summary>
        /// Sets <see cref="BaseListView.makeHeader"/>, replacing any existing callback, and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// This callback allows the user to make their own header for this control.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetMakeHeader<T>(this T element, Func<VisualElement> value)
            where T : BaseListView
        {
            element.makeHeader = value;
            return element;
        }

        /// <summary>
        /// Subscribes to the <see cref="BaseListView.makeHeader"/> callback.
        /// </summary>
        /// <remarks>
        /// This callback allows the user to make their own header for this control.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to add.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddMakeHeader<T>(this T element, Func<VisualElement> value)
            where T : BaseListView
        {
            element.makeHeader += value;
            return element;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="BaseListView.makeHeader"/> callback.
        /// </summary>
        /// <remarks>
        /// This callback allows the user to make their own header for this control.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveMakeHeader<T>(this T element, Func<VisualElement> value)
            where T : BaseListView
        {
            element.makeHeader -= value;
            return element;
        }
        #endregion

        #region MakeNoneElement
        /// <summary>
        /// Sets <see cref="BaseListView.makeNoneElement"/>, replacing any existing callback, and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// This callback allows the user to set a Visual Element to replace the "List is empty" Label shown when the ListView is empty.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetMakeNoneElement<T>(this T element, Func<VisualElement> value)
            where T : BaseListView
        {
            element.makeNoneElement = value;
            return element;
        }

        /// <summary>
        /// Subscribes to the <see cref="BaseListView.makeNoneElement"/> callback.
        /// </summary>
        /// <remarks>
        /// This callback allows the user to set a Visual Element to replace the "List is empty" Label shown when the ListView is empty.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to add.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddMakeNoneElement<T>(this T element, Func<VisualElement> value)
            where T : BaseListView
        {
            element.makeNoneElement += value;
            return element;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="BaseListView.makeNoneElement"/> callback.
        /// </summary>
        /// <remarks>
        /// This callback allows the user to set a Visual Element to replace the "List is empty" Label shown when the ListView is empty.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveMakeNoneElement<T>(this T element, Func<VisualElement> value)
            where T : BaseListView
        {
            element.makeNoneElement -= value;
            return element;
        }
        #endregion
#endif

        /// <summary>
        /// Sets <see cref="BaseVerticalCollectionView.itemsSource"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// The data source for collection items.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The items source to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetItemsSource<T>(this T element, IList value)
            where T : BaseVerticalCollectionView
        {
            element.itemsSource = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="BaseVerticalCollectionView.reorderable"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Gets or sets a value that indicates whether the user can drag list items to reorder them.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether items can be reordered by dragging.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetReorderable<T>(this T element, bool value)
            where T : BaseVerticalCollectionView
        {
            element.reorderable = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="BaseVerticalCollectionView.selectedIndex"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Returns or sets the selected item's index in the data source. If multiple items are selected, returns the first selected item's index. If multiple items are provided, sets them all as selected. If no item is selected, returns -1.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The selected index to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetSelectedIndex<T>(this T element, int value)
            where T : BaseVerticalCollectionView
        {
            element.selectedIndex = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="BaseVerticalCollectionView.fixedItemHeight"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// The height of a single item in the list, in pixels.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The fixed item height in pixels.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetFixedItemHeight<T>(this T element, float value)
            where T : BaseVerticalCollectionView
        {
            element.fixedItemHeight = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="BaseVerticalCollectionView.selectionType"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// Controls the selection type.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The selection type to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetSelectionType<T>(this T element, SelectionType value)
            where T : BaseVerticalCollectionView
        {
            element.selectionType = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="BaseVerticalCollectionView.horizontalScrollingEnabled"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// This property controls whether the collection view shows a horizontal scroll bar when its content does not fit in the visible area.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether horizontal scrolling is enabled.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetHorizontalScrollingEnabled<T>(this T element, bool value)
            where T : BaseVerticalCollectionView
        {
            element.horizontalScrollingEnabled = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="BaseVerticalCollectionView.virtualizationMethod"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// The virtualization method to use for this collection when a scroll bar is visible. Takes a value from the CollectionVirtualizationMethod enum.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The virtualization method to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetVirtualizationMethod<T>(this T element, CollectionVirtualizationMethod value)
            where T : BaseVerticalCollectionView
        {
            element.virtualizationMethod = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="BaseVerticalCollectionView.showAlternatingRowBackgrounds"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// This property controls whether the background colors of collection view rows alternate. Takes a value from the AlternatingRowBackground enum.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The alternating row background mode to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetShowAlternatingRowBackgrounds<T>(this T element, AlternatingRowBackground value)
            where T : BaseVerticalCollectionView
        {
            element.showAlternatingRowBackgrounds = value;
            return element;
        }
    }
}
