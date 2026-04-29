using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.UIElements.Editors.Internal
{
    /// <summary>
    /// Fluent extension methods for <see cref="AspidAnimatedTitle"/>.
    /// </summary>
    public static class AspidAnimatedTitleExtensions
    {
        /// <summary>
        /// Sets <see cref="AspidAnimatedTitle.Text"/> and returns the element for chaining.
        /// </summary>
        public static AspidAnimatedTitle SetText(this AspidAnimatedTitle element, string value)
        {
            element.Text = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidAnimatedTitle.ColorStride"/> and returns the element for chaining.
        /// </summary>
        public static AspidAnimatedTitle SetColorStride(this AspidAnimatedTitle element, float value)
        {
            element.ColorStride = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidAnimatedTitle.ColorSpeed"/> and returns the element for chaining.
        /// </summary>
        public static AspidAnimatedTitle SetColorSpeed(this AspidAnimatedTitle element, float value)
        {
            element.ColorSpeed = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidAnimatedTitle.WaveStride"/> and returns the element for chaining.
        /// </summary>
        public static AspidAnimatedTitle SetWaveStride(this AspidAnimatedTitle element, float value)
        {
            element.WaveStride = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidAnimatedTitle.WaveSpeed"/> and returns the element for chaining.
        /// </summary>
        public static AspidAnimatedTitle SetWaveSpeed(this AspidAnimatedTitle element, float value)
        {
            element.WaveSpeed = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidAnimatedTitle.WaveAmplitude"/> and returns the element for chaining.
        /// </summary>
        public static AspidAnimatedTitle SetWaveAmplitude(this AspidAnimatedTitle element, float value)
        {
            element.WaveAmplitude = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidAnimatedTitle.Color1"/> and returns the element for chaining.
        /// </summary>
        public static AspidAnimatedTitle SetColor1(this AspidAnimatedTitle element, Color value)
        {
            element.Color1 = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidAnimatedTitle.Color2"/> and returns the element for chaining.
        /// </summary>
        public static AspidAnimatedTitle SetColor2(this AspidAnimatedTitle element, Color value)
        {
            element.Color2 = value;
            return element;
        }

        /// <summary>
        /// Sets <see cref="AspidAnimatedTitle.Color3"/> and returns the element for chaining.
        /// </summary>
        public static AspidAnimatedTitle SetColor3(this AspidAnimatedTitle element, Color value)
        {
            element.Color3 = value;
            return element;
        }
    }
}
