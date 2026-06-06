using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Fluent extension methods for <see cref="AspidHelpBox"/>.
    /// </summary>
    internal static class AspidHelpBoxExtensions
    {
        /// <summary>
        /// Sets <see cref="AspidHelpBox.Title"/> and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to configure.</param>
        /// <param name="value">The new title text.</param>
        public static AspidHelpBox SetTitle(this AspidHelpBox element, string value)
        {
            element.Title = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidHelpBox.Message"/> and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to configure.</param>
        /// <param name="value">The new message text.</param>
        public static AspidHelpBox SetMessage(this AspidHelpBox element, string value)
        {
            element.Message = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidHelpBox.Status"/> and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to configure.</param>
        /// <param name="value">The new status.</param>
        public static AspidHelpBox SetStatus(this AspidHelpBox element, StatusStyle.Type value)
        {
            element.Status = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidHelpBox.MessageType"/> and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to configure.</param>
        /// <param name="value">The new message type.</param>
        public static AspidHelpBox SetMessageType(this AspidHelpBox element, HelpBoxMessageType value)
        {
            element.MessageType = value;
            return element;
        }
    }
}
