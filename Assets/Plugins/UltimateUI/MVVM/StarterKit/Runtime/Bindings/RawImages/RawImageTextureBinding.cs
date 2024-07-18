using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Bindings.RawImages
{
    public partial class RawImageTextureBinding : RawImageBindingBase, ITargetBinding<Texture2D>
    {
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [Unity.BindingLog]
#endif
        public void SetValue(Texture2D value) =>
            CachedImage.texture = value;
    }
}