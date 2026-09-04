using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Extension methods for <see cref="LineRenderer"/> used by the line renderer binders.
    /// </summary>
    public static class LineRendererExtensions
    {
        /// <summary>
        /// Writes <see cref="LineRenderer.startColor"/>, <see cref="LineRenderer.endColor"/> or both.
        /// </summary>
        /// <param name="lineRenderer">The renderer to update.</param>
        /// <param name="value">The color to apply.</param>
        /// <param name="mode">Which end colors <paramref name="value"/> writes.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="mode"/> is not a known value.</exception>
        public static void SetColor(this LineRenderer lineRenderer, Color value, LineRendererColorMode mode)
        {
            switch (mode)
            {
                case LineRendererColorMode.Start:
                    lineRenderer.startColor = value;
                    break;

                case LineRendererColorMode.End:
                    lineRenderer.endColor = value;
                    break;

                case LineRendererColorMode.StartAndEnd:
                    lineRenderer.startColor = value;
                    lineRenderer.endColor = value;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        /// <summary>
        /// Reads the end color selected by <paramref name="mode"/>; <see cref="LineRendererColorMode.StartAndEnd"/>
        /// reads the start color.
        /// </summary>
        /// <param name="lineRenderer">The renderer to read.</param>
        /// <param name="mode">Which end color to read.</param>
        /// <returns>The selected color.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="mode"/> is not a known value.</exception>
        public static Color GetColor(this LineRenderer lineRenderer, LineRendererColorMode mode) => mode switch
        {
            LineRendererColorMode.Start or LineRendererColorMode.StartAndEnd => lineRenderer.startColor,
            LineRendererColorMode.End => lineRenderer.endColor,
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };
    }
}
