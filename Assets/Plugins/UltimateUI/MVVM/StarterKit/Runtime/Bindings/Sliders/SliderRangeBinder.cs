using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Bindings.Sliders
{
    public partial class SliderRangeBinder : SliderBinderBase, ITargetBinding<Vector2>
    {
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [Unity.BindingLog]
#endif
        public void SetValue(Vector2 value)
        {
            CachedSlider.minValue = value.x;
            CachedSlider.maxValue = value.y;
        }
    }
}