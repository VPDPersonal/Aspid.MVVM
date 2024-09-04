using UnityEngine;
using UnityEngine.UI;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.Images
{
    [AddComponentMenu("UI/Binders/Image/Image Binder - Sprite Switcher")]
    public sealed class ImageSpriteSwitcherBinder : SwitcherMonoBinder<Image, Sprite>
    {
        protected override void SetValue(Sprite value) =>
            CachedComponent.sprite = value;
    }
}