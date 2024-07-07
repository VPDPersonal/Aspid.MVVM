using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Graphics
{
    public partial class GraphicColorBinder : GraphicBinderBase, IColorBinder
    {
        [BinderLog]
        public void SetValue(Color value) =>
            CachedGraphic.color = value;
    }
}