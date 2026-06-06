using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors
{
    public static partial class VisualElementExtensions
    {
        /// <summary>
        /// Binds the element to the specified <see cref="SerializedObject"/>.
        /// </summary>
        /// <param name="element">The element to bind.</param>
        /// <param name="obj">The serialized object to bind to.</param>
        /// <returns>The element, for chaining.</returns>
        public static T BindTo<T>(this T element, SerializedObject obj)
            where T : VisualElement
        {
            element.Bind(obj);
            return element;
        }

        /// <summary>
        /// Unbinds the element from its serialized object.
        /// </summary>
        /// <typeparam name="T">The element type.</typeparam>
        /// <param name="element">The element to unbind.</param>
        /// <returns>The element, for chaining.</returns>
        public static T UnbindFrom<T>(this T element)
            where T : VisualElement
        {
            element.Unbind();
            return element;
        }
    }
}
