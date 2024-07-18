// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Bindings.Sliders
{
    public partial class SliderValue : SliderBinderBase, INumberTargetBinding
    {
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [Unity.BindingLog]
#endif
        public void SetValue(int value) =>
            CachedSlider.value = value;
        
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [Unity.BindingLog]
#endif
        public void SetValue(long value) =>
            CachedSlider.value = value;
        
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [Unity.BindingLog]
#endif
        public void SetValue(float value) =>
            CachedSlider.value = value;
        
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [Unity.BindingLog]
#endif
        public void SetValue(double value) =>
            SetValue((float)value);
    }
}