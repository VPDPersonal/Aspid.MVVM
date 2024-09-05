using UnityEngine;
using UnityEngine.UI;

namespace UltimateUI.MVVM.StarterKit.Binders.Mono.Images
{
    [AddComponentMenu("UI/Binders/Image/Image Binder - Sprite Switcher")]
    public sealed class ImageSpriteSwitcherMonoBinder : Mono.SwitcherMonoBinder<Image, Sprite>
    {
        protected override void SetValue(Sprite value) =>
            CachedComponent.sprite = value;
    }
}