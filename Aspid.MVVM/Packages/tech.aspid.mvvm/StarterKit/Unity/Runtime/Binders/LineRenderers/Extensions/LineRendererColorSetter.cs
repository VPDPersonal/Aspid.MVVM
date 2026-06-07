using System;
using UnityEngine;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Provides extension methods for reading and writing the color properties of a <see cref="UnityEngine.LineRenderer"/>
    /// using a <see cref="LineRendererColorMode"/>.
    /// </summary>
    public static class LineRendererSetters
    {
        /// <summary>
        /// Sets the color on the <see cref="LineRenderer"/> according to the specified <see cref="LineRendererColorMode"/>.
        /// </summary>
        /// <param name="lineRenderer">The <see cref="LineRenderer"/> to update.</param>
        /// <param name="value">The color to apply.</param>
        /// <param name="mode">The endpoint(s) to set.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="mode"/> is not a valid <see cref="LineRendererColorMode"/> value.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetColor(this LineRenderer lineRenderer, Color value, LineRendererColorMode mode)
        {
            switch (mode)
            {
                case LineRendererColorMode.Start: lineRenderer.startColor = value; break;
                case LineRendererColorMode.End: lineRenderer.endColor = value; break;

                case LineRendererColorMode.StartAndEnd:
                    {
                        lineRenderer.startColor = value;
                        lineRenderer.endColor = value;
                        break;
                    }

                default: throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Gets the color from the <see cref="LineRenderer"/> for the specified <see cref="LineRendererColorMode"/>.
        /// </summary>
        /// <param name="lineRenderer">The <see cref="LineRenderer"/> to read from.</param>
        /// <param name="mode">The endpoint to read. Must not be <see cref="LineRendererColorMode.StartAndEnd"/>.</param>
        /// <returns>The <see cref="Color"/> of the specified endpoint.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="mode"/> is <see cref="LineRendererColorMode.StartAndEnd"/> or not a valid value.</exception>
        public static Color GetColor(this LineRenderer lineRenderer, LineRendererColorMode mode)
        {
            switch (mode)
            {
                case LineRendererColorMode.Start: return lineRenderer.startColor;
                case LineRendererColorMode.End: return lineRenderer.endColor;

                case LineRendererColorMode.StartAndEnd:
                default: throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
    }
}