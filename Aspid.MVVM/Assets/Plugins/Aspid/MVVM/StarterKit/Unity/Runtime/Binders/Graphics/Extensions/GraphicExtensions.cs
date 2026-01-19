using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public static class GraphicExtensions
    {
        public static void SetColor(this Graphic graphic, ColorComponent component, float value)
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
    }
}
