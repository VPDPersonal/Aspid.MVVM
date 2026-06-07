using System;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static class BaseListViewExtensions
    {
        #region OnAdd
        /// <summary>
        /// Sets <see cref="BaseListView.onAdd"/>, replacing any existing callback, and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// This callback allows the user to implement their own code to be executed when the Add Button is clicked.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetOnAdd<T>(this T element, Action<BaseListView> value)
            where T : BaseListView
        {
            element.onAdd = value;
            return element;
        }

        /// <summary>
        /// Subscribes to the <see cref="BaseListView.onAdd"/> callback.
        /// </summary>
        /// <remarks>
        /// This callback allows the user to implement their own code to be executed when the Add Button is clicked.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to add.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddOnAdd<T>(this T element, Action<BaseListView> value)
            where T : BaseListView
        {
            element.onAdd += value;
            return element;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="BaseListView.onAdd"/> callback.
        /// </summary>
        /// <remarks>
        /// This callback allows the user to implement their own code to be executed when the Add Button is clicked.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveOnAdd<T>(this T element, Action<BaseListView> value)
            where T : BaseListView
        {
            element.onAdd -= value;
            return element;
        }
        #endregion

        #region OnRemove
        /// <summary>
        /// Sets <see cref="BaseListView.onRemove"/>, replacing any existing callback, and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// This callback allows the user to implement their own code to be executed when the Remove Button is clicked.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetOnRemove<T>(this T element, Action<BaseListView> value)
            where T : BaseListView
        {
            element.onRemove = value;
            return element;
        }

        /// <summary>
        /// Subscribes to the <see cref="BaseListView.onRemove"/> callback.
        /// </summary>
        /// <remarks>
        /// This callback allows the user to implement their own code to be executed when the Remove Button is clicked.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to add.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddOnRemove<T>(this T element, Action<BaseListView> value)
            where T : BaseListView
        {
            element.onRemove += value;
            return element;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="BaseListView.onRemove"/> callback.
        /// </summary>
        /// <remarks>
        /// This callback allows the user to implement their own code to be executed when the Remove Button is clicked.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveOnRemove<T>(this T element, Action<BaseListView> value)
            where T : BaseListView
        {
            element.onRemove -= value;
            return element;
        }
        #endregion

        #region ItemsAdded
        /// <summary>
        /// Subscribes to the <see cref="BaseListView.itemsAdded"/> event.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddItemsAdded<T>(this T element, Action<IEnumerable<int>> value)
            where T : BaseListView
        {
            element.itemsAdded += value;
            return element;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="BaseListView.itemsAdded"/> event.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveItemsAdded<T>(this T element, Action<IEnumerable<int>> value)
            where T : BaseListView
        {
            element.itemsAdded -= value;
            return element;
        }
        #endregion

        #region ItemsRemoved
        /// <summary>
        /// Subscribes to the <see cref="BaseListView.itemsRemoved"/> event.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddItemsRemoved<T>(this T element, Action<IEnumerable<int>> value)
            where T : BaseListView
        {
            element.itemsRemoved += value;
            return element;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="BaseListView.itemsRemoved"/> event.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveItemsRemoved<T>(this T element, Action<IEnumerable<int>> value)
            where T : BaseListView
        {
            element.itemsRemoved -= value;
            return element;
        }
        #endregion

        #region OverridingAddButtonBehavior
        /// <summary>
        /// Sets <see cref="BaseListView.overridingAddButtonBehavior"/>, replacing any existing callback, and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// This callback allows the user to implement a DropdownMenu when the Add Button is clicked.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetOverridingAddButtonBehavior<T>(this T element, Action<BaseListView, Button> value)
            where T : BaseListView
        {
            element.overridingAddButtonBehavior = value;
            return element;
        }

        /// <summary>
        /// Subscribes to the <see cref="BaseListView.overridingAddButtonBehavior"/> callback.
        /// </summary>
        /// <remarks>
        /// This callback allows the user to implement a DropdownMenu when the Add Button is clicked.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to add.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddOverridingAddButtonBehavior<T>(this T element, Action<BaseListView, Button> value)
            where T : BaseListView
        {
            element.overridingAddButtonBehavior += value;
            return element;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="BaseListView.overridingAddButtonBehavior"/> callback.
        /// </summary>
        /// <remarks>
        /// This callback allows the user to implement a DropdownMenu when the Add Button is clicked.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveOverridingAddButtonBehavior<T>(this T element, Action<BaseListView, Button> value)
            where T : BaseListView
        {
            element.overridingAddButtonBehavior -= value;
            return element;
        }
        #endregion

        /// <summary>
        /// Sets <see cref="BaseListView.allowRemove"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// This property allows the user to allow or block the removal of an item when clicking on the Remove Button. It must return true or false.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether item removal is allowed.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetAllowRemove<T>(this T element, bool value)
            where T : BaseListView
        {
            element.allowRemove = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="BaseListView.allowAdd"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// This property allows the user to allow or block the addition of an item when clicking on the Add Button. It must return true or false.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether item addition is allowed.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetAllowAdd<T>(this T element, bool value)
            where T : BaseListView
        {
            element.allowAdd = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="BaseListView.headerTitle"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// This property controls the text of the foldout header when using showFoldoutHeader.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The header title to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetHeaderTitle<T>(this T element, string value)
            where T : BaseListView
        {
            element.headerTitle = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="BaseListView.showFoldoutHeader"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// This property controls whether the element view displays a header, in the form of a foldout that can be expanded or collapsed.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether to show the foldout header.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetShowFoldoutHeader<T>(this T element, bool value)
            where T : BaseListView
        {
            element.showFoldoutHeader = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="BaseListView.showAddRemoveFooter"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// This property controls whether a footer will be added to the list view.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether to show the add/remove footer.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetShowAddRemoveFooter<T>(this T element, bool value)
            where T : BaseListView
        {
            element.showAddRemoveFooter = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="BaseListView.showBoundCollectionSize"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// This property controls whether the element view displays the collection size (number of items).
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether to show the bound collection size.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetShowBoundCollectionSize<T>(this T element, bool value)
            where T : BaseListView
        {
            element.showBoundCollectionSize = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="BaseListView.reorderMode"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// This property controls the drag and drop mode for the element view.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The reorder mode to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetReorderMode<T>(this T element, ListViewReorderMode value)
            where T : BaseListView
        {
            element.reorderMode = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="BaseListView.bindingSourceSelectionMode"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// This property controls whether every element in the element will get its data source setup automatically to the correct item in the collection's source.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The binding source selection mode to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetBindingSourceSelectionMode<T>(this T element, BindingSourceSelectionMode value)
            where T : BaseListView
        {
            element.bindingSourceSelectionMode = value;
            return element;
        }

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
        #endregion
#endif
    }
}
