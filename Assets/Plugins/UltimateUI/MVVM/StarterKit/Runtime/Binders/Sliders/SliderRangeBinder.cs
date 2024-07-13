using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Sliders
{
    public partial class SliderRangeBinder : SliderBinderBase, IBinder<Vector2>
    {
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [BinderLog]
#endif
        public void SetValue(Vector2 value)
        {
            CachedSlider.minValue = value.x;
            CachedSlider.maxValue = value.y;
        }
    }
}