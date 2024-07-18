using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Bindings.Images
{
    public partial class ImageSpriteBinding : ImageBindingBase, ITargetBinding<Sprite>
    {
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [Unity.BindingLog]
#endif
        public void SetValue(Sprite value) =>
            CachedImage.sprite = value;
    }
}