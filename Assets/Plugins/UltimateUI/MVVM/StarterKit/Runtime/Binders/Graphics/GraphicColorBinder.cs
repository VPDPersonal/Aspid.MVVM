using UnityEngine;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Graphics
{
    [AddComponentMenu("UI/Binders/Graphic/Graphic Binder - Color")]
    public partial class GraphicColorBinder : GraphicBinderBase, IColorBinder
    {
        [BinderLog]
        public void SetValue(Color value) =>
            CachedGraphic.color = value;
    }
}