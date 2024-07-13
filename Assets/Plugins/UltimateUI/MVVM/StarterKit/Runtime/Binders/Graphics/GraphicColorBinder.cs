using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Graphics
{
    public partial class GraphicColorBinder : GraphicBinderBase, IColorBinder
    {
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [BinderLog]
#endif
        public void SetValue(Color value) =>
            CachedGraphic.color = value;
    }
}