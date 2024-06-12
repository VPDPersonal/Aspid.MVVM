using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Images
{
    public class ImageSpriteSwitcherBinder : ImageBinderBase, IBinder<bool>
    {
        [Header("Parameters")]
        [SerializeField] private Sprite _trueSprite;
        [SerializeField] private Sprite _falseSprite;

        public void SetValue(bool value) =>
            CachedImage.sprite = value ? _trueSprite : _falseSprite;
    }
}