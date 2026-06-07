using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Fluent extension methods for <see cref="AspidAnimatedDotsBackground"/>.
    /// </summary>
    internal static class AspidAnimatedDotsBackgroundExtensions
    {
        /// <summary>
        /// Sets <see cref="AspidAnimatedDotsBackground.Color1"/> and returns the element for chaining.
        /// </summary>
        public static AspidAnimatedDotsBackground SetColor1(this AspidAnimatedDotsBackground element, Color value)
        {
            element.Color1 = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidAnimatedDotsBackground.Color2"/> and returns the element for chaining.
        /// </summary>
        public static AspidAnimatedDotsBackground SetColor2(this AspidAnimatedDotsBackground element, Color value)
        {
            element.Color2 = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidAnimatedDotsBackground.Color3"/> and returns the element for chaining.
        /// </summary>
        public static AspidAnimatedDotsBackground SetColor3(this AspidAnimatedDotsBackground element, Color value)
        {
            element.Color3 = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidAnimatedDotsBackground.DotRadius"/> and returns the element for chaining.
        /// </summary>
        public static AspidAnimatedDotsBackground SetDotRadius(this AspidAnimatedDotsBackground element, float value)
        {
            element.DotRadius = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidAnimatedDotsBackground.DotSpacing"/> and returns the element for chaining.
        /// </summary>
        public static AspidAnimatedDotsBackground SetDotSpacing(this AspidAnimatedDotsBackground element, float value)
        {
            element.DotSpacing = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidAnimatedDotsBackground.ScaleReferenceSize"/> and returns the element for chaining.
        /// </summary>
        public static AspidAnimatedDotsBackground SetScaleReferenceSize(this AspidAnimatedDotsBackground element, float value)
        {
            element.ScaleReferenceSize = value;
            return element;
        }
    }
}
