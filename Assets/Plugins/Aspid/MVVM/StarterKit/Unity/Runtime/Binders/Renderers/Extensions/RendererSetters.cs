#nullable enable
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public static class RendererSetters
    {
        public static void SetMaterials(this Renderer renderer, IConverter<Material?, Material?>? converter, params Material[]? values)
        {
            if (converter is null) renderer.materials = values;
            else SetMaterials(renderer, converter, (IReadOnlyCollection<Material>?)values);
        }
          
        public static void SetMaterials(this Renderer renderer, IConverter<Material?, Material?>? converter, IReadOnlyCollection<Material>? values)
        {
            if (values is null || values.Count == 0)
            {
                renderer.materials = null;
            }
            else if (values.Count == 1)
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