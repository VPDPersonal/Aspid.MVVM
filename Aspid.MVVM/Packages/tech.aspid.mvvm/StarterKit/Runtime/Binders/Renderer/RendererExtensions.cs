#nullable enable
using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Extension methods that write validated values to a <see cref="Renderer"/>.
    /// </summary>
    public static class RendererExtensions
    {
        /// <summary>
        /// Sets <see cref="Renderer.materials"/>, passing each material through <paramref name="converter"/>.
        /// </summary>
        /// <remarks>
        /// <see cref="Renderer.materials"/> rejects <see langword="null"/>, so a missing or empty collection clears
        /// the array.
        /// </remarks>
        /// <param name="renderer">The renderer whose materials are set.</param>
        /// <param name="converter">
        /// The converter applied to each material, or <see langword="null"/> to use it as-is.
        /// </param>
        /// <param name="values">The materials to assign, or <see langword="null"/> to clear.</param>
        public static void SetMaterials(
            this Renderer renderer,
            IConverter<Material?, Material?>? converter,
            IReadOnlyCollection<Material>? values)
        {
            if (values is null || values.Count is 0)
            {
                renderer.materials = Array.Empty<Material>();
                return;
            }

            var i = 0;
            var converted = new Material[values.Count];

            foreach (var value in values)
                converted[i++] = converter?.Convert(value) ?? value;

            renderer.materials = converted;
        }
    }
}
