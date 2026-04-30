#nullable enable
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Provides extension methods for setting materials on a <see cref="Renderer"/> with an optional converter.
    /// </summary>
    public static class RendererSetters
    {
        /// <summary>
        /// Sets the materials on a <see cref="Renderer"/> from a params array, applying an optional converter to each element.
        /// </summary>
        /// <param name="renderer">The <see cref="Renderer"/> to update.</param>
        /// <param name="converter">The converter applied to each material before assignment, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="values">The materials to assign.</param>
        public static void SetMaterials(this Renderer renderer, IConverter<Material?, Material?>? converter, params Material[]? values)
        {
            if (converter is null) renderer.materials = values;
            else SetMaterials(renderer, converter, (IReadOnlyCollection<Material>?)values);
        }

        /// <summary>
        /// Sets the materials on a <see cref="Renderer"/> from a collection, applying an optional converter to each element.
        /// When the collection is <see langword="null"/> or empty, clears the materials.
        /// When the collection contains a single element, assigns it via <see cref="Renderer.material"/>.
        /// </summary>
        /// <param name="renderer">The <see cref="Renderer"/> to update.</param>
        /// <param name="converter">The converter applied to each material before assignment, or <see langword="null"/> to use the value as-is.</param>
        /// <param name="values">The materials to assign, or <see langword="null"/> to clear.</param>
        public static void SetMaterials(this Renderer renderer, IConverter<Material?, Material?>? converter, IReadOnlyCollection<Material>? values)
        {
            if (values is null || values.Count is 0)
            {
                renderer.materials = null;
            }
            else if (values.Count is 1)
            {
                Material? convertedValue = null;

                foreach (var value in values)
                    convertedValue = converter?.Convert(value) ?? value;
                
                renderer.materials = null;
                renderer.material = convertedValue;
            }
            else
            {
                var i = 0;
                var convertedValue = new Material[values.Count];

                foreach (var value in values)
                {
                    convertedValue[i] = converter?.Convert(value) ?? value;
                    i++;
                }
                
                renderer.materials = convertedValue;
            }
        }
    }
}