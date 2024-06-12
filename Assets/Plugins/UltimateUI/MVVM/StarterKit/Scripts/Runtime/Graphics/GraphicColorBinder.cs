using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Graphics
{
    public class GraphicColorBinder : GraphicBinderBase, IColorBinder
    {
        public void SetValue(Color value) =>
            CachedGraphic.color = value;
    }
}