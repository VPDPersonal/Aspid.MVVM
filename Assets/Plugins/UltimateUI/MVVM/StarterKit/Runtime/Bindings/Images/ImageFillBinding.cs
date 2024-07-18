// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Bindings.Images
{
    public partial class ImageFillBinding : ImageBindingBase, ITargetBinding<float>
    {
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [Unity.BindingLog]
#endif
        public void SetValue(float value) =>
            CachedImage.fillAmount = value;
    }
}