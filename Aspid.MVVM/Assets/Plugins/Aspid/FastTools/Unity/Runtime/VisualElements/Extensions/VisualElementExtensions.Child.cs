using System;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static partial class VisualElementExtensions
    {
        /// <summary>
        /// Adds an element to the <see cref="VisualElement.contentContainer"/> of this element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="child">The child element to add.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddChild<T>(this T element, VisualElement child)
            where T : VisualElement
        {
            element.Add(child);
            return element;
        }

        /// <summary>
        /// Inserts a child element at the specified index in the <see cref="VisualElement.contentContainer"/> of this element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="index">The index at which to insert the child.</param>
        /// <param name="child">The child element to insert.</param>
        /// <returns>The element, for chaining.</returns>
        public static T InsertChild<T>(this T element, int index, VisualElement child)
            where T : VisualElement
        {
            element.Insert(index, child);
            return element;
        }

        /// <summary>
        /// Adds a span of child elements to the <see cref="VisualElement.contentContainer"/> of this element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="children">The children to add.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddChildren<T>(this T element, Span<VisualElement> children)
            where T : VisualElement
        {
            foreach (var child in children)
                element.Add(child);

            return element;
        }
        
        /// <summary>
        /// Inserts a span of child elements starting at the specified index in the <see cref="VisualElement.contentContainer"/> of this element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="index">The index at which to start inserting children.</param>
        /// <param name="children">The children to insert.</param>
        /// <returns>The element, for chaining.</returns>
        public static T InsertChildren<T>(this T element, int index, Span<VisualElement> children)
            where T : VisualElement
        {
            foreach (var child in children)
                element.Insert(index++, child);

            return element;
        }

        /// <summary>
        /// Adds a list of child elements to the <see cref="VisualElement.contentContainer"/> of this element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="children">The children to add.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddChildren<T>(this T element, List<VisualElement> children)
            where T : VisualElement
        {
            if (children is null) return element;

            foreach (var child in children)
                element.Add(child);

            return element;
        }
        
        /// <summary>
        /// Inserts a list of child elements starting at the specified index in the <see cref="VisualElement.contentContainer"/> of this element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="index">The index at which to start inserting children.</param>
        /// <param name="children">The children to insert.</param>
        /// <returns>The element, for chaining.</returns>
        public static T InsertChildren<T>(this T element, int index, List<VisualElement> children)
            where T : VisualElement
        {
            if (children is null) return element;

            foreach (var child in children)
                element.Insert(index++, child);

            return element;
        }

        /// <summary>
        /// Adds an array of child elements to the <see cref="VisualElement.contentContainer"/> of this element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="children">The children to add.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddChildren<T>(this T element, params VisualElement[] children)
            where T : VisualElement
        {
            if (children is null) return element;

            foreach (var child in children)
                element.Add(child);

            return element;
        }
        
        /// <summary>
        /// Inserts an array of child elements starting at the specified index in the <see cref="VisualElement.contentContainer"/> of this element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="index">The index at which to start inserting children.</param>
        /// <param name="children">The children to insert.</param>
        /// <returns>The element, for chaining.</returns>
        public static T InsertChildren<T>(this T element, int index, params VisualElement[] children)
            where T : VisualElement
        {
            if (children is null) return element;

            foreach (var child in children)
                element.Insert(index++, child);

            return element;
        }

        /// <summary>
        /// Adds an enumerable of child elements to the <see cref="VisualElement.contentContainer"/> of this element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="children">The children to add.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddChildren<T>(this T element, IEnumerable<VisualElement> children)
            where T : VisualElement
        {
            if (children is null) return element;

            foreach (var child in children)
                element.Add(child);

            return element;
        }
        
        /// <summary>
        /// Inserts an enumerable of child elements starting at the specified index in the <see cref="VisualElement.contentContainer"/> of this element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="index">The index at which to start inserting children.</param>
        /// <param name="children">The children to insert.</param>
        /// <returns>The element, for chaining.</returns>
        public static T InsertChildren<T>(this T element, int index, IEnumerable<VisualElement> children)
            where T : VisualElement
        {
            if (children is null) return element;

            foreach (var child in children)
                element.Insert(index++, child);

            return element;
        }

        /// <summary>
        /// Adds a read-only span of child elements to the <see cref="VisualElement.contentContainer"/> of this element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="children">The children to add.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddChildren<T>(this T element, ReadOnlySpan<VisualElement> children)
            where T : VisualElement
        {
            foreach (var child in children)
                element.Add(child);

            return element;
        }
        
        /// <summary>
        /// Inserts a read-only span of child elements starting at the specified index in the <see cref="VisualElement.contentContainer"/> of this element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="index">The index at which to start inserting children.</param>
        /// <param name="children">The children to insert.</param>
        /// <returns>The element, for chaining.</returns>
        public static T InsertChildren<T>(this T element, int index, ReadOnlySpan<VisualElement> children)
            where T : VisualElement
        {
            foreach (var child in children)
                element.Insert(index++, child);

            return element;
        }
    }
}
