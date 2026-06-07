// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Fluent extension methods for <see cref="AspidDividingLine"/>.
    /// </summary>
    internal static class AspidDividingLineExtensions
    {
        /// <summary>
        /// Sets <see cref="AspidDividingLine.Theme"/> and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to configure.</param>
        /// <param name="value">The new theme.</param>
        public static AspidDividingLine SetTheme(this AspidDividingLine element, ThemeStyle.Type value)
        {
            element.Theme = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidDividingLine.Status"/> and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to configure.</param>
        /// <param name="value">The new status.</param>
        public static AspidDividingLine SetStatus(this AspidDividingLine element, StatusStyle.Type value)
        {
            element.Status = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidDividingLine.Size"/> and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to configure.</param>
        /// <param name="value">The new size.</param>
        public static AspidDividingLine SetSize(this AspidDividingLine element, AspidDividingLineSizeStyle.Type value)
        {
            element.Size = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidDividingLine.Direction"/> and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to configure.</param>
        /// <param name="value">The new direction.</param>
        public static AspidDividingLine SetDirection(this AspidDividingLine element, AspidDividingLineDirectionStyle.Type value)
        {
            element.Direction = value;
            return element;
        }
    }
}
