using UnityEngine.UIElements;
using UnityEditor.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors
{
    public static class PropertyFieldExtensions
    {
        /// <summary>
        /// Subscribes to the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to subscribe.</param>
        /// <returns>The element, for chaining.</returns>
        public static T AddValueChanged<T>(this T element, EventCallback<SerializedPropertyChangeEvent> value)
            where T : PropertyField
        {
            element.RegisterValueChangeCallback(value);
            return element;
        }
        
        /// <summary>
        /// Unsubscribes from the value-changed event of the element.
        /// </summary>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The callback to remove.</param>
        /// <returns>The element, for chaining.</returns>
        public static T RemoveValueChanged<T>(this T element, EventCallback<SerializedPropertyChangeEvent> value)
            where T : PropertyField
        {
            element.UnregisterCallback(value);
            return element;
        }
    }
}
