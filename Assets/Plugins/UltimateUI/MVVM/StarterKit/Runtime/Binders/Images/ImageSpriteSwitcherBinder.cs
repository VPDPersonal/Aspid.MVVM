using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Images
{
    public partial class ImageSpriteSwitcherBinder : ImageBinderBase, IBinder<bool>
    {
        [Header("Parameters")]
        [SerializeField] private Sprite _trueSprite;
        [SerializeField] private Sprite _falseSprite;
        
        protected Sprite TrueTexture => _trueSprite;
        
        protected Sprite FalseTexture => _falseSprite;
        
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [BinderLog]
#endif
        public void SetValue(bool value) =>
            CachedImage.sprite = GetSprite(value);
        
        protected Sprite GetSprite(bool value) =>
            value ? TrueTexture : FalseTexture;
    }
}