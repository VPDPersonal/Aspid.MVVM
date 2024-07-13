using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.RawImages
{
    public partial class RawImageTextureBinder : RawImageBinderBase, IBinder<Texture2D>
    {
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [BinderLog]
#endif
        public void SetValue(Texture2D value) =>
            CachedImage.texture = value;
    }
}