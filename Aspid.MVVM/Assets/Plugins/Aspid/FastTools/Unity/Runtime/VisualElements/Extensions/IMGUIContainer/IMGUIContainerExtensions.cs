using System;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static class IMGUIContainerExtensions
    {
        #region OnGUIHandler
        /// <summary>
        /// Sets the <see cref="IMGUIContainer.onGUIHandler"/> callback, replacing any existing handler.
        /// </summary>
        /// <remarks>
        /// The function that's called to render and handle IMGUI events.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The handler to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetOnGUIHandler<T>(this T element, Action value)
            where T : IMGUIContainer
        {
            element.onGUIHandler = value;
            return element;
        }

        /// <summary>
        /// Subscribes to the <see cref="IMGUIContainer.onGUIHandler"/> callback.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The handler to add.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddOnGUIHandler<T>(this T element, Action value)
            where T : IMGUIContainer
        {
            element.onGUIHandler += value;
            return element;
        }

        /// <summary>
        /// Unsubscribes from the <see cref="IMGUIContainer.onGUIHandler"/> callback.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The handler to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveOnGUIHandler<T>(this T element, Action value)
            where T : IMGUIContainer
        {
            element.onGUIHandler -= value;
            return element;
        }
        #endregion

        /// <summary>
        /// Sets <see cref="IMGUIContainer.cullingEnabled"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// When this property is set to true, onGUIHandler is not called when the Element is outside the viewport.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">Whether culling is enabled.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SeCullingEnabled<T>(this T element, bool value)
            where T : IMGUIContainer
        {
            element.cullingEnabled = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="IMGUIContainer.contextType"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// ContextType of this IMGUIContainer. Currently only supports ContextType.Editor.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The context type to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetContextType<T>(this T element, ContextType value)
            where T : IMGUIContainer
        {
            element.contextType = value;
            return element;
        }
    }
}
