using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Extension methods for <see cref="Graphic"/> that provide per-channel color access.
    /// </summary>
    public static class GraphicExtensions
    {
        /// <summary>
        /// Sets a single <see cref="ColorComponent"/> channel of <paramref name="graphic"/>'s color.
        /// </summary>
        /// <param name="graphic">The <see cref="Graphic"/> whose color channel is updated.</param>
        /// <param name="component">The color channel to set.</param>
        /// <param name="value">The new channel value.</param>
        public static void SetColorComponent(this Graphic graphic, ColorComponent component, float value)
        {
            var color = graphic.color;

            switch (component)
            {
                case ColorComponent.R: color.r = value; break;
                case ColorComponent.G: color.g = value; break;
                case ColorComponent.B: color.b = value; break;
                case ColorComponent.A: color.a = value; break;
                default: Debug.LogError($"Invalid color component {component}", context: graphic); return;
            }
            
            graphic.color = color;
        }

        /// <summary>
        /// Returns the value of a single <see cref="ColorComponent"/> channel of <paramref name="graphic"/>'s color.
        /// </summary>
        /// <param name="graphic">The <see cref="Graphic"/> whose color channel is read.</param>
        /// <param name="component">The color channel to read.</param>
        /// <returns>The current channel value, or <c>0</c> if <paramref name="component"/> is invalid.</returns>
        public static float GetColorComponent(this Graphic graphic, ColorComponent component)
        {
            switch (component)
            {
                case ColorComponent.R: return graphic.color.r;
                case ColorComponent.G: return graphic.color.g;
                case ColorComponent.B: return graphic.color.b;
                case ColorComponent.A: return graphic.color.a;
                default: Debug.LogError($"Invalid color component {component}", context: graphic); break;
            }

            return 0;
        }
    }
}