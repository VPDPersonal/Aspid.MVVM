using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Bindings.Graphics
{
    public partial class GraphicColorBinding : GraphicBinderBase, IColorTargetBinding
    {
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [Unity.BindingLog]
#endif
        public void SetValue(Color value) =>
            CachedGraphic.color = value;
    }
}