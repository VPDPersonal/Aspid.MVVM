using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Fluent extension methods for <see cref="AspidAnimatedLogo"/>.
    /// </summary>
    internal static class AspidAnimatedLogoExtensions
    {
        /// <summary>
        /// Sets <see cref="AspidAnimatedLogo.ColorCycleIntervalMs"/> and returns the element for chaining.
        /// </summary>
        public static AspidAnimatedLogo SetColorCycleIntervalMs(this AspidAnimatedLogo element, long value)
        {
            element.ColorCycleIntervalMs = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidAnimatedLogo.PulseSpeed"/> and returns the element for chaining.
        /// </summary>
        public static AspidAnimatedLogo SetPulseSpeed(this AspidAnimatedLogo element, float value)
        {
            element.PulseSpeed = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidAnimatedLogo.PulseHoverAmplitude"/> and returns the element for chaining.
        /// </summary>
        public static AspidAnimatedLogo SetPulseHoverAmplitude(this AspidAnimatedLogo element, float value)
        {
            element.PulseHoverAmplitude = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidAnimatedLogo.Image1"/> and returns the element for chaining.
        /// </summary>
        public static AspidAnimatedLogo SetImage1(this AspidAnimatedLogo element, Texture2D value)
        {
            element.Image1 = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidAnimatedLogo.Image2"/> and returns the element for chaining.
        /// </summary>
        public static AspidAnimatedLogo SetImage2(this AspidAnimatedLogo element, Texture2D value)
        {
            element.Image2 = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidAnimatedLogo.Image3"/> and returns the element for chaining.
        /// </summary>
        public static AspidAnimatedLogo SetImage3(this AspidAnimatedLogo element, Texture2D value)
        {
            element.Image3 = value;
            return element;
        }
    }
}
