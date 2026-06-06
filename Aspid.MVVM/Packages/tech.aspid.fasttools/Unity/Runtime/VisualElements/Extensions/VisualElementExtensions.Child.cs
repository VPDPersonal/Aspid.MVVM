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
        /// Removes the specified child from the element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="child">The child element to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveChild<T>(this T element, VisualElement child)
            where T : VisualElement
        {
            element.Remove(child);
            return element;
        }

        /// <summary>
        /// Removes the child at the specified index from the element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="index">The index of the child to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveChildAt<T>(this T element, int index)
            where T : VisualElement
        {
            element.RemoveAt(index);
            return element;
        }

        /// <summary>
        /// Removes all children from the element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <returns>The element, for chaining.</returns>
        public static T ClearChildren<T>(this T element)
            where T : VisualElement
        {
            element.Clear();
            return element;
        }
        
        /// <summary>
        /// Conditionally adds an element to the <see cref="VisualElement.contentContainer"/> of this element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="condition">If true, performs the operation; otherwise skips it and returns the element unchanged.</param>
        /// <param name="child">The child element to add.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddChildIf<T>(this T element, bool condition, VisualElement child)
            where T : VisualElement
        {
            if (condition) element.Add(child);
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
        /// Conditionally inserts a child element at the specified index in the <see cref="VisualElement.contentContainer"/> of this element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="condition">If true, performs the operation; otherwise skips it and returns the element unchanged.</param>
        /// <param name="index">The index at which to insert the child.</param>
        /// <param name="child">The child element to insert.</param>
        /// <returns>The element, for chaining.</returns>
        public static T InsertChildIf<T>(this T element, bool condition, int index, VisualElement child)
            where T : VisualElement
        {
            if (condition) element.Insert(index, child);
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
        /// Conditionally adds a span of child elements to the <see cref="VisualElement.contentContainer"/> of this element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="condition">If true, performs the operation; otherwise skips it and returns the element unchanged.</param>
        /// <param name="children">The children to add.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddChildrenIf<T>(this T element, bool condition, Span<VisualElement> children)
            where T : VisualElement
        {
            if (condition)
            {
                foreach (var child in children)
                    element.Add(child);
            }

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
        /// Conditionally inserts a span of child elements starting at the specified index in the <see cref="VisualElement.contentContainer"/> of this element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="condition">If true, performs the operation; otherwise skips it and returns the element unchanged.</param>
        /// <param name="index">The index at which to start inserting children.</param>
        /// <param name="children">The children to insert.</param>
        /// <returns>The element, for chaining.</returns>
        public static T InsertChildrenIf<T>(this T element, bool condition, int index, Span<VisualElement> children)
            where T : VisualElement
        {
            if (condition)
            {
                foreach (var child in children)
                    element.Insert(index++, child);
            }

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
        /// Conditionally adds a list of child elements to the <see cref="VisualElement.contentContainer"/> of this element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="condition">If true, performs the operation; otherwise skips it and returns the element unchanged.</param>
        /// <param name="children">The children to add.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddChildrenIf<T>(this T element, bool condition, List<VisualElement> children)
            where T : VisualElement
        {
            if (children is null) return element;

            if (condition)
            {
                foreach (var child in children)
                    element.Add(child);
            }

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
        /// Conditionally inserts a list of child elements starting at the specified index in the <see cref="VisualElement.contentContainer"/> of this element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="condition">If true, performs the operation; otherwise skips it and returns the element unchanged.</param>
        /// <param name="index">The index at which to start inserting children.</param>
        /// <param name="children">The children to insert.</param>
        /// <returns>The element, for chaining.</returns>
        public static T InsertChildrenIf<T>(this T element, bool condition, int index, List<VisualElement> children)
            where T : VisualElement
        {
            if (children is null) return element;

            if (condition)
            {
                foreach (var child in children)
                    element.Insert(index++, child);
            }

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
        /// Conditionally adds an array of child elements to the <see cref="VisualElement.contentContainer"/> of this element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="condition">If true, performs the operation; otherwise skips it and returns the element unchanged.</param>
        /// <param name="children">The children to add.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddChildrenIf<T>(this T element, bool condition, params VisualElement[] children)
            where T : VisualElement
        {
            if (children is null) return element;

            if (condition)
            {
                foreach (var child in children)
                    element.Add(child);
            }

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
        /// Conditionally inserts an array of child elements starting at the specified index in the <see cref="VisualElement.contentContainer"/> of this element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="condition">If true, performs the operation; otherwise skips it and returns the element unchanged.</param>
        /// <param name="index">The index at which to start inserting children.</param>
        /// <param name="children">The children to insert.</param>
        /// <returns>The element, for chaining.</returns>
        public static T InsertChildrenIf<T>(this T element, bool condition, int index, params VisualElement[] children)
            where T : VisualElement
        {
            if (children is null) return element;

            if (condition)
            {
                foreach (var child in children)
                    element.Insert(index++, child);
            }

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
        /// Conditionally adds an enumerable of child elements to the <see cref="VisualElement.contentContainer"/> of this element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="condition">If true, performs the operation; otherwise skips it and returns the element unchanged.</param>
        /// <param name="children">The children to add.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddChildrenIf<T>(this T element, bool condition, IEnumerable<VisualElement> children)
            where T : VisualElement
        {
            if (children is null) return element;

            if (condition)
            {
                foreach (var child in children)
                    element.Add(child);
            }

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
        /// Conditionally inserts an enumerable of child elements starting at the specified index in the <see cref="VisualElement.contentContainer"/> of this element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="condition">If true, performs the operation; otherwise skips it and returns the element unchanged.</param>
        /// <param name="index">The index at which to start inserting children.</param>
        /// <param name="children">The children to insert.</param>
        /// <returns>The element, for chaining.</returns>
        public static T InsertChildrenIf<T>(this T element, bool condition, int index, IEnumerable<VisualElement> children)
            where T : VisualElement
        {
            if (children is null) return element;

            if (condition)
            {
                foreach (var child in children)
                    element.Insert(index++, child);
            }

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
        /// Conditionally adds a read-only span of child elements to the <see cref="VisualElement.contentContainer"/> of this element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="condition">If true, performs the operation; otherwise skips it and returns the element unchanged.</param>
        /// <param name="children">The children to add.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddChildrenIf<T>(this T element, bool condition, ReadOnlySpan<VisualElement> children)
            where T : VisualElement
        {
            if (condition)
            {
                foreach (var child in children)
                    element.Add(child);
            }

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
        
        /// <summary>
        /// Conditionally inserts a read-only span of child elements starting at the specified index in the <see cref="VisualElement.contentContainer"/> of this element and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="condition">If true, performs the operation; otherwise skips it and returns the element unchanged.</param>
        /// <param name="index">The index at which to start inserting children.</param>
        /// <param name="children">The children to insert.</param>
        /// <returns>The element, for chaining.</returns>
        public static T InsertChildrenIf<T>(this T element, bool condition, int index, ReadOnlySpan<VisualElement> children)
            where T : VisualElement
        {
            if (condition)
            {
                foreach (var child in children)
                    element.Insert(index++, child);
            }

            return element;
        }
    }
}
