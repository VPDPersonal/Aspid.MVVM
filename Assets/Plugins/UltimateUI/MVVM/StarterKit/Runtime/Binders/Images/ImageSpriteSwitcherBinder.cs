using UnityEngine;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Images
{
    [AddComponentMenu("UI/Binders/Image/Image Binder - Sprite Switcher")]
    public partial class ImageSpriteSwitcherBinder : ImageBinderBase, IBinder<bool>
    {
        [Header("Parameters")]
        [SerializeField] private Sprite _trueSprite;
        [SerializeField] private Sprite _falseSprite;
        
        protected Sprite TrueTexture => _trueSprite;
        
        protected Sprite FalseTexture => _falseSprite;
        
        [BinderLog]
        public void SetValue(bool value) =>
            CachedImage.sprite = GetSprite(value);
        
        protected Sprite GetSprite(bool value) =>
            value ? TrueTexture : FalseTexture;
    }
}