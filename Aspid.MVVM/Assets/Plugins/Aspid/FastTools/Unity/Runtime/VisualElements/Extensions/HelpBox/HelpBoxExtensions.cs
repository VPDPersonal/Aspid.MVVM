using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace Aspid.FastTools.UIElements
{
    public static class HelpBoxExtensions
    {
        /// <summary>
        /// Sets <see cref="HelpBox.text"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// The message text.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The text to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetText<T>(this T element, string value)
            where T : HelpBox
        {
            element.text = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="HelpBox.messageType"/> and returns the element for chaining.
        /// </summary>
        /// <remarks>
        /// The type of message.
        /// </remarks>
        /// <param name="element">The element to modify.</param>
        /// <param name="value">The message type to set.</param>
        /// <returns>The element, for chaining.</returns>
        public static T SetMessageType<T>(this T element, HelpBoxMessageType value)
            where T : HelpBox
        {
            element.messageType = value;
            return element;
        }
    }
}
