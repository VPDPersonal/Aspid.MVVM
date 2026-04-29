using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Fluent extension methods for <see cref="AspidGradientButton"/>.
    /// </summary>
    public static class AspidGradientButtonExtensions
    {
        /// <summary>
        /// Sets <see cref="AspidGradientButton.Text"/> and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to configure.</param>
        /// <param name="value">The new label text.</param>
        public static AspidGradientButton SetText(this AspidGradientButton element, string value)
        {
            element.Text = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidGradientButton.Gradient"/> and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to configure.</param>
        /// <param name="value">The new gradient background color.</param>
        public static AspidGradientButton SetGradient(this AspidGradientButton element, Color value)
        {
            element.Gradient = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidGradientButton.Accent"/> and returns the element for chaining.
        /// </summary>
        /// <param name="element">The element to configure.</param>
        /// <param name="value">The new accent (hover) color.</param>
        public static AspidGradientButton SetAccent(this AspidGradientButton element, Color value)
        {
            element.Accent = value;
            return element;
        }
    }
}
