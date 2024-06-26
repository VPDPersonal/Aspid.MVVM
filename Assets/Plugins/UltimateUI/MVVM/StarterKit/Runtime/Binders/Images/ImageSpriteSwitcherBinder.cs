using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Images
{
    public class ImageSpriteSwitcherBinder : ImageBinderBase, IBinder<bool>
    {
        [Header("Parameters")]
        [SerializeField] private Sprite _trueSprite;
        [SerializeField] private Sprite _falseSprite;
        
        protected Sprite TrueTexture => _trueSprite;
        
        protected Sprite FalseTexture => _falseSprite;

        public void SetValue(bool value) =>
            CachedImage.sprite = GetSprite(value);
        
        protected Sprite GetSprite(bool value) =>
            value ? TrueTexture : FalseTexture;
    }
}