using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.RawImages
{
    public partial class RawImageTextureSwitcherBinder : RawImageBinderBase, IBinder<bool>
    {
        [Header("Parameters")]
        [SerializeField] private Texture2D _trueTexture;
        [SerializeField] private Texture2D _falseTexture;

        protected Texture2D TrueTexture => _trueTexture;
        
        protected Texture2D FalseTexture => _falseTexture;
        
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [BinderLog]
#endif
        public void SetValue(bool value) =>
            CachedImage.texture = GetTexture2D(value);

        protected Texture2D GetTexture2D(bool value) =>
            value ? TrueTexture : FalseTexture;
    }
}