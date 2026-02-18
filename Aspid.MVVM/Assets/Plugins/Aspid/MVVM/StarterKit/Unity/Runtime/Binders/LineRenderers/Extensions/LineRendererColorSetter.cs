using System;
using UnityEngine;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public static class LineRendererSetters
    {
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

        public static Color GetColor(this LineRenderer lineRenderer, LineRendererColorMode mode)
        {
            switch (mode)
            {
                case LineRendererColorMode.Start: return lineRenderer.startColor;
                case LineRendererColorMode.End:  return lineRenderer.endColor;
                
                case LineRendererColorMode.StartAndEnd:
                default: throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
    }
}