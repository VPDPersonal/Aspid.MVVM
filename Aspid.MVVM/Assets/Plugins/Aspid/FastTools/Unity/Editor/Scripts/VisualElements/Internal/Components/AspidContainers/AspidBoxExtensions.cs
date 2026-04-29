// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Fluent extension methods for <see cref="AspidBox"/>.
    /// </summary>
    public static class AspidBoxExtensions
    {
        /// <summary>
        /// Sets <see cref="AspidBox.Theme"/> and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to configure.</param>
        /// <param name="value">The new theme.</param>
        public static AspidBox SetTheme(this AspidBox element, ThemeStyle.Type value)
        {
            element.Theme = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidBox.Status"/> and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to configure.</param>
        /// <param name="value">The new status.</param>
        public static AspidBox SetStatus(this AspidBox element, StatusStyle.Type value)
        {
            element.Status = value;
            return element;
        }
    }
}
